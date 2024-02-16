using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        int up = 0;
        int down = 0;
        Random seed = new Random();
        Api? api;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {// 开始运行
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
                    MessageBox.Show($"获取开奖信息失败，状态码：{resp.code}，重新登录获取新的SID试试");
                    return false;
                }

                if (resp.data.Count == 0)
                {
                    //timer1.Enabled = false;
                    //MessageBox.Show("服务器返回的开奖信息为空！等到今日第一次下注之后再运行程序试试");
                    //return false;
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
            catch (Exception e)
            {
                //timer1.Enabled = false;
                MessageBox.Show($"{DateTime.Now}，获取开奖信息失败，{e.Message}");
                return true;
            }
        }

        async void Bet()
        {
            if(api ==  null) { return; }
            var issue = await api.GetLatestIssue();
            if (issue == 0)
            {
                MessageBox.Show("获取最新期号失败");
                return;
            }
            // 已下注则不再重复下注
            if (listView2.Items.Count != 0 && listView2.Items[0].Text == issue.ToString()) { return; }

            var map = new[] { "00", "05", "15", "20", "30", "35", "45", "50" };
            var lastTime = "55";
            if(listView1.Items.Count > 0)
            {
                lastTime = listView1.Items[0].SubItems[1].Text.Split(":")[1];
            }

            var orderType = OrderType.Odd;
            if (Array.IndexOf(map, lastTime) == -1) { orderType = OrderType.Even; }

            var resp = await api.Order(issue, orderType, amountEachBet);
            if (!resp.status)
            {
                //timer1.Enabled = false;
                MessageBox.Show($"下注失败，状态码：{resp.code}，可能是重复下注了，或者是SID失效");
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

        static bool checkTime()
        {// 检查封盘
            //// 23:54 - 07:07 封盘
            //if ((DateTime.Now.Hour <= 7 && DateTime.Now.Minute < 7) && (DateTime.Now.Hour >= 23 && DateTime.Now.Minute > 54))
            //{
            //    return false;
            //}


            // 0:00 - 07:01 封盘
            if ((DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 7) || (DateTime.Now.Hour == 7 && DateTime.Now.Minute < 1))
            {
                return false;
            }
            return true;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {// 每分钟运行一次

            // 封盘时直接返回
            if (!checkTime()) { return; }

            api = new Api(sid_textBox.Text); // 更新sid

            // 检查每注金额
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

            // 检查上下限
            try
            {
                up = int.Parse(textBox1.Text);
                down = int.Parse(textBox2.Text);
            }
            catch
            {
                timer1.Enabled = false;
                MessageBox.Show("请输入正确的上下限，仅限数字");
                return;
            }


            if (!await UpdateList1()) { return; }
            UpdateList2();
            alreadyBet_textBox.Text = numOfBetOn.ToString();

            // 检查是否达到限制
            if ((numOfBetOn >= up || numOfBetOn <= down))
            {
                timer1.Enabled = false;
                MessageBox.Show("已中注数达到限制，停止运行");
                return;
            }

            if (listView2.Items.Count > 0 && listView2.Items[0].SubItems.Count < 3)
            {// 确保知道结果后再下注
                return;
            }


            new Thread(() =>
            {
                Thread.Sleep(1000 * seed.Next(30)); // 下注前随机休眠
                // Bet(); // 非UI线程不可直接操控UI
                Invoke(delegate { Bet(); });
            }).Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {// 更新运行状态

            if (timer1.Enabled)
            {
                label6.Text = "程序运行中";
                label6.ForeColor = Color.Green;
                if (!checkTime())
                {
                    label6.Text = "封盘中……";
                    label6.ForeColor = Color.YellowGreen;
                }
                button4.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
                button2.Enabled = true;
                label6.Text = "程序未运行";
                label6.ForeColor = Color.Black;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {// 停止运行

            timer1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {//清空已下注

            numOfBetOn = 0;
            alreadyBet_textBox.Text = "0";
            listView2.Items.Clear();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            api = new Api(sid_textBox.Text);
            Debug.WriteLine(await api.GetLatestIssue());
        }
    }
}
