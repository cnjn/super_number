namespace 超奖_dotnet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ListViewItem listViewItem1 = new ListViewItem(new ListViewItem.ListViewSubItem[] { new ListViewItem.ListViewSubItem(null, "113007916", Color.Black, SystemColors.Window, new Font("Microsoft YaHei UI", 9F)), new ListViewItem.ListViewSubItem(null, "2024-02-08 23:50"), new ListViewItem.ListViewSubItem(null, "[20]", Color.Red, SystemColors.Window, new Font("Microsoft YaHei UI", 9F)) }, -1);
            label1 = new Label();
            sid_textBox = new TextBox();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            label2 = new Label();
            label3 = new Label();
            betAmount_textBox = new TextBox();
            listView2 = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            button1 = new Button();
            button2 = new Button();
            label4 = new Label();
            alreadyBet_textBox = new TextBox();
            label5 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            label6 = new Label();
            button3 = new Button();
            label7 = new Label();
            label8 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button4 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 15);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(37, 20);
            label1.TabIndex = 0;
            label1.Text = "SID:";
            // 
            // sid_textBox
            // 
            sid_textBox.Location = new Point(45, 12);
            sid_textBox.Name = "sid_textBox";
            sid_textBox.Size = new Size(322, 27);
            sid_textBox.TabIndex = 1;
            sid_textBox.Text = "9d728e6d-3eb1-4e38-98e4-2a4272d9a0d8";
            // 
            // listView1
            // 
            listView1.AutoArrange = false;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Items.AddRange(new ListViewItem[] { listViewItem1 });
            listView1.Location = new Point(5, 69);
            listView1.Margin = new Padding(5, 3, 5, 3);
            listView1.Name = "listView1";
            listView1.Size = new Size(367, 377);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "期数";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "下单时间";
            columnHeader2.Width = 203;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "超奖";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 46);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 3;
            label2.Text = "开奖信息：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(387, 46);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 4;
            label3.Text = "每注金额：";
            // 
            // betAmount_textBox
            // 
            betAmount_textBox.Location = new Point(387, 69);
            betAmount_textBox.Name = "betAmount_textBox";
            betAmount_textBox.Size = new Size(94, 27);
            betAmount_textBox.TabIndex = 5;
            betAmount_textBox.Text = "10";
            betAmount_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // listView2
            // 
            listView2.AutoArrange = false;
            listView2.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6 });
            listView2.FullRowSelect = true;
            listView2.GridLines = true;
            listView2.Location = new Point(499, 69);
            listView2.Name = "listView2";
            listView2.Size = new Size(292, 377);
            listView2.TabIndex = 6;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "期数";
            columnHeader4.Width = 91;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "下注";
            columnHeader5.Width = 139;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "结果";
            // 
            // button1
            // 
            button1.Location = new Point(387, 11);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 7;
            button1.Text = "测试";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(387, 316);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 8;
            button2.Text = "开始运行";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(387, 108);
            label4.Name = "label4";
            label4.Size = new Size(84, 20);
            label4.TabIndex = 9;
            label4.Text = "已中注数：";
            // 
            // alreadyBet_textBox
            // 
            alreadyBet_textBox.Location = new Point(387, 131);
            alreadyBet_textBox.Name = "alreadyBet_textBox";
            alreadyBet_textBox.ReadOnly = true;
            alreadyBet_textBox.Size = new Size(94, 27);
            alreadyBet_textBox.TabIndex = 10;
            alreadyBet_textBox.Text = "0";
            alreadyBet_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(499, 46);
            label5.Name = "label5";
            label5.Size = new Size(73, 20);
            label5.TabIndex = 11;
            label5.Text = "下注信息:";
            // 
            // timer1
            // 
            timer1.Interval = 60000;
            timer1.Tick += timer1_Tick;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Tick += timer2_Tick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(700, 11);
            label6.Name = "label6";
            label6.Size = new Size(84, 20);
            label6.TabIndex = 12;
            label6.Text = "程序未运行";
            // 
            // button3
            // 
            button3.Location = new Point(387, 365);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 13;
            button3.Text = "停止运行";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(387, 176);
            label7.Name = "label7";
            label7.Size = new Size(54, 20);
            label7.TabIndex = 14;
            label7.Text = "上限：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(387, 220);
            label8.Name = "label8";
            label8.Size = new Size(54, 20);
            label8.TabIndex = 15;
            label8.Text = "下限：";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(432, 173);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(49, 27);
            textBox1.TabIndex = 16;
            textBox1.Text = "3";
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(432, 217);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(49, 27);
            textBox2.TabIndex = 17;
            textBox2.Text = "-6";
            textBox2.TextAlign = HorizontalAlignment.Right;
            // 
            // button4
            // 
            button4.Location = new Point(387, 267);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 18;
            button4.Text = "清空已下注";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 450);
            Controls.Add(button4);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(button3);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(alreadyBet_textBox);
            Controls.Add(label4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listView2);
            Controls.Add(betAmount_textBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(listView1);
            Controls.Add(sid_textBox);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "Form1";
            Text = "SUPER NUMBER";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox sid_textBox;
        private ListView listView1;
        private Label label2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Label label3;
        private TextBox betAmount_textBox;
        private ListView listView2;
        private Button button1;
        private Button button2;
        private Label label4;
        private TextBox alreadyBet_textBox;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private Label label6;
        private Button button3;
        private Label label7;
        private Label label8;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button4;
    }
}