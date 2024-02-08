using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 超奖_dotnet
{
    public partial class Form1 : Form
    {
        int amountEachBet = 10;
        int numOfBetOn = 0;
        Api? api;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1_Tick(this, EventArgs.Empty);
        }

        async Task<bool> UpdateList1()
        {
            listView1.Items.Clear();
            if (api == null) { return false; }
            try
            {
                var resp = await api.GetLotteryResult();
                if (!resp.status)
                {
                    timer1.Enabled = false;
                    MessageBox.Show($"获取开奖信息失败，状态码：{resp.code}");
                    return false;
                }

                if (resp.data.Count == 0)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("服务器返回的开奖信息为空！");
                    return false;
                }
                resp.data.ForEach(x =>
                {
                    // 更新开奖信息
                    var item = new ListViewItem(x.issue);
                    var date = DateTimeOffset.FromUnixTimeSeconds(int.Parse(x.open_at)).LocalDateTime.ToString("yyyy-MM-dd HH:mm");
                    item.SubItems.Add(date);
                    item.SubItems.Add(x.superNumber);
                    listView1.Items.Add(item);
                });
                return true;
            }
            catch
            {
                timer1.Enabled = false;
                MessageBox.Show("获取开奖信息失败，疑似网络问题");
                return false;
            }
        }

        async void Bet()
        {
            var issue = int.Parse(listView1.Items[0].Text) + 1;
            // 已下注则不再重复下注
            if (listView2.Items.Count != 0 && listView2.Items[0].Text == issue.ToString()) { return; }

            var map = new[] { "00", "05", "15", "20", "30", "35", "45", "50" };
            var lastTime = listView1.Items[0].SubItems[1].Text.Split(":")[1];

            var orderType = OrderType.Odd;
            if (Array.IndexOf(map, lastTime) == -1) { orderType = OrderType.Even; }

            var resp = await api.Order(issue, orderType, amountEachBet);
            if (!resp.status)
            {
                timer1.Enabled = false;
                MessageBox.Show($"下注失败，状态码：{resp.code}");
                return;
            }

            var item = new ListViewItem { Text = issue.ToString(), UseItemStyleForSubItems = false };
            item.SubItems.Add($"已下注{orderType}, {amountEachBet}元");
            listView2.Items.Insert(0, item);
        }

        void UpdateList2()
        {
            var count = 0;
            // 更新已下注的是否中奖
            var items = new List<string>();
            listView1.Items.Cast<ListViewItem>().ToList().ForEach(x =>
            {
                items.Add(x.Text);
            });

            listView2.Items.Cast<ListViewItem>().ToList().ForEach(x =>
            {
                var idx = items.IndexOf(x.Text);
                // 未开奖，跳过
                if (idx == -1 || listView1.Items[idx].SubItems[2].Text == "") { return; }

                var superNumber = listView1.Items[idx].SubItems[2].Text;
                superNumber = superNumber.Substring(1, superNumber.Length - 2);
                var color = Color.Red;

                if (x.SubItems[1].Text.IndexOf("Odd") != -1 && int.Parse(superNumber) % 2 == 1)
                {
                    color = Color.Green;
                }
                if (x.SubItems[1].Text.IndexOf("Even") != -1 && int.Parse(superNumber) % 2 == 0)
                {
                    color = Color.Green;
                }
                if (color == Color.Red) { count--; } else { count++; }
                if (x.SubItems.Count == 2)
                {
                    x.SubItems.Add(new ListViewItem.ListViewSubItem { Text = superNumber, ForeColor = color });
                }

            });

            numOfBetOn = count;

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now.Hour < 7 || (DateTime.Now.Hour >= 23 && DateTime.Now.Minute > 55))
            {
                return;
            }

            api = new Api(sid_textBox.Text); // 更新sid

            // 更新每注金额
            try
            {
                amountEachBet = int.Parse(betAmount_textBox.Text);
            }
            catch
            {
                timer1.Enabled = false;
                MessageBox.Show("每注金额仅限输入数字！");
                return;
            }


            if (!await UpdateList1()) { return; }
            UpdateList2();
            alreadyBet_textBox.Text = numOfBetOn.ToString();

            // 检查是否达到限制
            if (numOfBetOn >= 3 || numOfBetOn <= -6)
            {
                timer1.Enabled = false;
                MessageBox.Show("已中注数达到限制，停止运行");
                return;
            }
            Bet();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                label6.Text = "程序运行中";
                label6.ForeColor = Color.Green;
                if (DateTime.Now.Hour < 7 || (DateTime.Now.Hour >= 23 && DateTime.Now.Minute > 55))
                {
                    label6.Text = "封盘中……";
                    label6.ForeColor = Color.YellowGreen;
                }
            }
            else
            {
                label6.Text = "程序未运行";
                label6.ForeColor = Color.Black;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
