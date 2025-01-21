namespace SPS01CalibrateAndTestNewModeApp
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
            this.airTabPage1 = new ReaLTaiizor.Controls.AirTabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ButtonProductConn = new ReaLTaiizor.Controls.Button();
            this.LabelConnMode = new ReaLTaiizor.Controls.SkyLabel();
            this.ComboxConnMode = new ReaLTaiizor.Controls.AloneComboBox();
            this.BottonEvbConnect = new ReaLTaiizor.Controls.Button();
            this.ComboxEvbBps = new ReaLTaiizor.Controls.AloneComboBox();
            this.ComboxEvbPortList = new ReaLTaiizor.Controls.AloneComboBox();
            this.LabelEvbPort = new ReaLTaiizor.Controls.SkyLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ButtonGetRawData = new ReaLTaiizor.Controls.HopeButton();
            this.materialLabel6 = new ReaLTaiizor.Controls.MaterialLabel();
            this.LabelID = new ReaLTaiizor.Controls.MaterialLabel();
            this.materialLabel4 = new ReaLTaiizor.Controls.MaterialLabel();
            this.materialLabel3 = new ReaLTaiizor.Controls.MaterialLabel();
            this.materialLabel2 = new ReaLTaiizor.Controls.MaterialLabel();
            this.materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            this.ComCalibrateMode = new ReaLTaiizor.Controls.AloneComboBox();
            this.ComRawDataAvg = new ReaLTaiizor.Controls.AloneComboBox();
            this.ComRawDataJumpPoint = new ReaLTaiizor.Controls.AloneComboBox();
            this.ComRawMode = new ReaLTaiizor.Controls.AloneComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxT0P2 = new ReaLTaiizor.Controls.AloneTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxT0P1 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT1P1 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT1P2 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT2P1 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT2P2 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT3P1 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT3P2 = new ReaLTaiizor.Controls.AloneTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxT1 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT0 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT2 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT3 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxO1 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxO3 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxO2 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxO4 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT0P4 = new ReaLTaiizor.Controls.AloneTextBox();
            this.textBoxT0P3 = new ReaLTaiizor.Controls.AloneTextBox();
            this.miniToolStrip = new ReaLTaiizor.Controls.CrownStatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Timers.Timer();
            this.airTabPage1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.miniToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // airTabPage1
            // 
            this.airTabPage1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.airTabPage1.BaseColor = System.Drawing.Color.White;
            this.airTabPage1.Controls.Add(this.tabPage1);
            this.airTabPage1.Controls.Add(this.tabPage2);
            this.airTabPage1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.airTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.airTabPage1.ItemSize = new System.Drawing.Size(30, 115);
            this.airTabPage1.Location = new System.Drawing.Point(0, 0);
            this.airTabPage1.Multiline = true;
            this.airTabPage1.Name = "airTabPage1";
            this.airTabPage1.NormalTextColor = System.Drawing.Color.DimGray;
            this.airTabPage1.SelectedIndex = 0;
            this.airTabPage1.SelectedTabBackColor = System.Drawing.Color.White;
            this.airTabPage1.SelectedTextColor = System.Drawing.Color.Black;
            this.airTabPage1.ShowOuterBorders = false;
            this.airTabPage1.Size = new System.Drawing.Size(800, 468);
            this.airTabPage1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.airTabPage1.SquareColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(87)))), ((int)(((byte)(100)))));
            this.airTabPage1.TabCursor = System.Windows.Forms.Cursors.Hand;
            this.airTabPage1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.ButtonProductConn);
            this.tabPage1.Controls.Add(this.LabelConnMode);
            this.tabPage1.Controls.Add(this.ComboxConnMode);
            this.tabPage1.Controls.Add(this.BottonEvbConnect);
            this.tabPage1.Controls.Add(this.ComboxEvbBps);
            this.tabPage1.Controls.Add(this.ComboxEvbPortList);
            this.tabPage1.Controls.Add(this.LabelEvbPort);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.Location = new System.Drawing.Point(119, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(677, 460);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // ButtonProductConn
            // 
            this.ButtonProductConn.BackColor = System.Drawing.Color.Transparent;
            this.ButtonProductConn.BorderColor = System.Drawing.Color.Black;
            this.ButtonProductConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonProductConn.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ButtonProductConn.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ButtonProductConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ButtonProductConn.Image = null;
            this.ButtonProductConn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonProductConn.InactiveColor = System.Drawing.Color.Black;
            this.ButtonProductConn.Location = new System.Drawing.Point(35, 223);
            this.ButtonProductConn.Name = "ButtonProductConn";
            this.ButtonProductConn.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ButtonProductConn.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ButtonProductConn.Size = new System.Drawing.Size(63, 28);
            this.ButtonProductConn.TabIndex = 8;
            this.ButtonProductConn.Text = "连接";
            this.ButtonProductConn.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // LabelConnMode
            // 
            this.LabelConnMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelConnMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            this.LabelConnMode.Location = new System.Drawing.Point(35, 150);
            this.LabelConnMode.Name = "LabelConnMode";
            this.LabelConnMode.Size = new System.Drawing.Size(156, 28);
            this.LabelConnMode.TabIndex = 7;
            this.LabelConnMode.Text = "产品通信方式";
            this.LabelConnMode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ComboxConnMode
            // 
            this.ComboxConnMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboxConnMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboxConnMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboxConnMode.EnabledCalc = true;
            this.ComboxConnMode.FormattingEnabled = true;
            this.ComboxConnMode.ItemHeight = 20;
            this.ComboxConnMode.Location = new System.Drawing.Point(35, 181);
            this.ComboxConnMode.Name = "ComboxConnMode";
            this.ComboxConnMode.Size = new System.Drawing.Size(75, 26);
            this.ComboxConnMode.TabIndex = 6;
            // 
            // BottonEvbConnect
            // 
            this.BottonEvbConnect.BackColor = System.Drawing.Color.Transparent;
            this.BottonEvbConnect.BorderColor = System.Drawing.Color.Black;
            this.BottonEvbConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BottonEvbConnect.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.BottonEvbConnect.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BottonEvbConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BottonEvbConnect.Image = null;
            this.BottonEvbConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BottonEvbConnect.InactiveColor = System.Drawing.Color.Black;
            this.BottonEvbConnect.Location = new System.Drawing.Point(35, 95);
            this.BottonEvbConnect.Name = "BottonEvbConnect";
            this.BottonEvbConnect.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.BottonEvbConnect.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.BottonEvbConnect.Size = new System.Drawing.Size(63, 28);
            this.BottonEvbConnect.TabIndex = 5;
            this.BottonEvbConnect.Text = "连接";
            this.BottonEvbConnect.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // ComboxEvbBps
            // 
            this.ComboxEvbBps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboxEvbBps.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboxEvbBps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboxEvbBps.EnabledCalc = true;
            this.ComboxEvbBps.FormattingEnabled = true;
            this.ComboxEvbBps.ItemHeight = 20;
            this.ComboxEvbBps.Location = new System.Drawing.Point(131, 49);
            this.ComboxEvbBps.Name = "ComboxEvbBps";
            this.ComboxEvbBps.Size = new System.Drawing.Size(75, 26);
            this.ComboxEvbBps.TabIndex = 4;
            // 
            // ComboxEvbPortList
            // 
            this.ComboxEvbPortList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboxEvbPortList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboxEvbPortList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboxEvbPortList.EnabledCalc = true;
            this.ComboxEvbPortList.FormattingEnabled = true;
            this.ComboxEvbPortList.ItemHeight = 20;
            this.ComboxEvbPortList.Location = new System.Drawing.Point(35, 49);
            this.ComboxEvbPortList.Name = "ComboxEvbPortList";
            this.ComboxEvbPortList.Size = new System.Drawing.Size(75, 26);
            this.ComboxEvbPortList.TabIndex = 3;
            // 
            // LabelEvbPort
            // 
            this.LabelEvbPort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelEvbPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            this.LabelEvbPort.Location = new System.Drawing.Point(35, 18);
            this.LabelEvbPort.Name = "LabelEvbPort";
            this.LabelEvbPort.Size = new System.Drawing.Size(156, 28);
            this.LabelEvbPort.TabIndex = 0;
            this.LabelEvbPort.Text = "通信板端口 波特率";
            this.LabelEvbPort.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.ButtonGetRawData);
            this.tabPage2.Controls.Add(this.materialLabel6);
            this.tabPage2.Controls.Add(this.LabelID);
            this.tabPage2.Controls.Add(this.materialLabel4);
            this.tabPage2.Controls.Add(this.materialLabel3);
            this.tabPage2.Controls.Add(this.materialLabel2);
            this.tabPage2.Controls.Add(this.materialLabel1);
            this.tabPage2.Controls.Add(this.ComCalibrateMode);
            this.tabPage2.Controls.Add(this.ComRawDataAvg);
            this.tabPage2.Controls.Add(this.ComRawDataJumpPoint);
            this.tabPage2.Controls.Add(this.ComRawMode);
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage2.Location = new System.Drawing.Point(119, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(677, 460);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // ButtonGetRawData
            // 
            this.ButtonGetRawData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.ButtonGetRawData.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            this.ButtonGetRawData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonGetRawData.DangerColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.ButtonGetRawData.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ButtonGetRawData.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ButtonGetRawData.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.ButtonGetRawData.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ButtonGetRawData.Location = new System.Drawing.Point(549, 205);
            this.ButtonGetRawData.Name = "ButtonGetRawData";
            this.ButtonGetRawData.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.ButtonGetRawData.Size = new System.Drawing.Size(104, 51);
            this.ButtonGetRawData.SuccessColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(194)))), ((int)(((byte)(58)))));
            this.ButtonGetRawData.TabIndex = 11;
            this.ButtonGetRawData.Text = "采集数据";
            this.ButtonGetRawData.TextColor = System.Drawing.Color.White;
            this.ButtonGetRawData.WarningColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(162)))), ((int)(((byte)(60)))));
            // 
            // materialLabel6
            // 
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel6.Location = new System.Drawing.Point(39, 51);
            this.materialLabel6.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(119, 22);
            this.materialLabel6.TabIndex = 10;
            this.materialLabel6.Text = "ID";
            // 
            // LabelID
            // 
            this.LabelID.Depth = 0;
            this.LabelID.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LabelID.Location = new System.Drawing.Point(168, 51);
            this.LabelID.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.LabelID.Name = "LabelID";
            this.LabelID.Size = new System.Drawing.Size(244, 22);
            this.LabelID.TabIndex = 9;
            // 
            // materialLabel4
            // 
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(424, 97);
            this.materialLabel4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(119, 22);
            this.materialLabel4.TabIndex = 8;
            this.materialLabel4.Text = "选择模式";
            // 
            // materialLabel3
            // 
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(293, 97);
            this.materialLabel3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(119, 22);
            this.materialLabel3.TabIndex = 7;
            this.materialLabel3.Text = "平均点数";
            // 
            // materialLabel2
            // 
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(168, 97);
            this.materialLabel2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(119, 22);
            this.materialLabel2.TabIndex = 6;
            this.materialLabel2.Text = "跳过点数";
            // 
            // materialLabel1
            // 
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(39, 97);
            this.materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(119, 22);
            this.materialLabel1.TabIndex = 5;
            this.materialLabel1.Text = "选择类型";
            // 
            // ComCalibrateMode
            // 
            this.ComCalibrateMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComCalibrateMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComCalibrateMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComCalibrateMode.EnabledCalc = true;
            this.ComCalibrateMode.FormattingEnabled = true;
            this.ComCalibrateMode.ItemHeight = 20;
            this.ComCalibrateMode.Location = new System.Drawing.Point(424, 119);
            this.ComCalibrateMode.Name = "ComCalibrateMode";
            this.ComCalibrateMode.Size = new System.Drawing.Size(119, 26);
            this.ComCalibrateMode.TabIndex = 4;
            // 
            // ComRawDataAvg
            // 
            this.ComRawDataAvg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComRawDataAvg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComRawDataAvg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComRawDataAvg.EnabledCalc = true;
            this.ComRawDataAvg.FormattingEnabled = true;
            this.ComRawDataAvg.ItemHeight = 20;
            this.ComRawDataAvg.Location = new System.Drawing.Point(293, 119);
            this.ComRawDataAvg.Name = "ComRawDataAvg";
            this.ComRawDataAvg.Size = new System.Drawing.Size(119, 26);
            this.ComRawDataAvg.TabIndex = 3;
            // 
            // ComRawDataJumpPoint
            // 
            this.ComRawDataJumpPoint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComRawDataJumpPoint.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComRawDataJumpPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComRawDataJumpPoint.EnabledCalc = true;
            this.ComRawDataJumpPoint.FormattingEnabled = true;
            this.ComRawDataJumpPoint.ItemHeight = 20;
            this.ComRawDataJumpPoint.Location = new System.Drawing.Point(168, 119);
            this.ComRawDataJumpPoint.Name = "ComRawDataJumpPoint";
            this.ComRawDataJumpPoint.Size = new System.Drawing.Size(119, 26);
            this.ComRawDataJumpPoint.TabIndex = 2;
            // 
            // ComRawMode
            // 
            this.ComRawMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComRawMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComRawMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComRawMode.EnabledCalc = true;
            this.ComRawMode.FormattingEnabled = true;
            this.ComRawMode.ItemHeight = 20;
            this.ComRawMode.Location = new System.Drawing.Point(40, 119);
            this.ComRawMode.Name = "ComRawMode";
            this.ComRawMode.Size = new System.Drawing.Size(119, 26);
            this.ComRawMode.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66319F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxT0P2, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label9, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT0P1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT1P1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT1P2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT2P1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT2P2, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT3P1, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT3P2, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT1, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT0, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT2, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT3, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxO1, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxO3, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxO2, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxO4, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT0P4, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxT0P3, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(38, 172);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(505, 214);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxT0P2
            // 
            this.textBoxT0P2.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT0P2.EnabledCalc = true;
            this.textBoxT0P2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT0P2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT0P2.Location = new System.Drawing.Point(171, 123);
            this.textBoxT0P2.MaxLength = 32767;
            this.textBoxT0P2.MultiLine = false;
            this.textBoxT0P2.Name = "textBoxT0P2";
            this.textBoxT0P2.ReadOnly = false;
            this.textBoxT0P2.Size = new System.Drawing.Size(78, 23);
            this.textBoxT0P2.TabIndex = 9;
            this.textBoxT0P2.Text = "aloneTextBox1";
            this.textBoxT0P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT0P2.UseSystemPasswordChar = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(3, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 21);
            this.label10.TabIndex = 25;
            this.label10.Text = "温度";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(423, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 21);
            this.label9.TabIndex = 24;
            this.label9.Text = "目标值";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 24);
            this.label6.TabIndex = 3;
            this.label6.Text = "P3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(3, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "P1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(339, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "T3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(255, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "T2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(3, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 24);
            this.label7.TabIndex = 4;
            this.label7.Text = "P4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(3, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 24);
            this.label8.TabIndex = 5;
            this.label8.Text = "P2";
            // 
            // textBoxT0P1
            // 
            this.textBoxT0P1.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT0P1.EnabledCalc = true;
            this.textBoxT0P1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT0P1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT0P1.Location = new System.Drawing.Point(171, 33);
            this.textBoxT0P1.MaxLength = 32767;
            this.textBoxT0P1.MultiLine = false;
            this.textBoxT0P1.Name = "textBoxT0P1";
            this.textBoxT0P1.ReadOnly = false;
            this.textBoxT0P1.Size = new System.Drawing.Size(78, 23);
            this.textBoxT0P1.TabIndex = 6;
            this.textBoxT0P1.Text = "aloneTextBox1";
            this.textBoxT0P1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT0P1.UseSystemPasswordChar = false;
            // 
            // textBoxT1P1
            // 
            this.textBoxT1P1.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT1P1.EnabledCalc = true;
            this.textBoxT1P1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT1P1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT1P1.Location = new System.Drawing.Point(87, 33);
            this.textBoxT1P1.MaxLength = 32767;
            this.textBoxT1P1.MultiLine = false;
            this.textBoxT1P1.Name = "textBoxT1P1";
            this.textBoxT1P1.ReadOnly = false;
            this.textBoxT1P1.Size = new System.Drawing.Size(78, 23);
            this.textBoxT1P1.TabIndex = 10;
            this.textBoxT1P1.Text = "aloneTextBox1";
            this.textBoxT1P1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT1P1.UseSystemPasswordChar = false;
            // 
            // textBoxT1P2
            // 
            this.textBoxT1P2.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT1P2.EnabledCalc = true;
            this.textBoxT1P2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT1P2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT1P2.Location = new System.Drawing.Point(87, 123);
            this.textBoxT1P2.MaxLength = 32767;
            this.textBoxT1P2.MultiLine = false;
            this.textBoxT1P2.Name = "textBoxT1P2";
            this.textBoxT1P2.ReadOnly = false;
            this.textBoxT1P2.Size = new System.Drawing.Size(78, 23);
            this.textBoxT1P2.TabIndex = 11;
            this.textBoxT1P2.Text = "aloneTextBox1";
            this.textBoxT1P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT1P2.UseSystemPasswordChar = false;
            // 
            // textBoxT2P1
            // 
            this.textBoxT2P1.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT2P1.EnabledCalc = true;
            this.textBoxT2P1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT2P1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT2P1.Location = new System.Drawing.Point(255, 33);
            this.textBoxT2P1.MaxLength = 32767;
            this.textBoxT2P1.MultiLine = false;
            this.textBoxT2P1.Name = "textBoxT2P1";
            this.textBoxT2P1.ReadOnly = false;
            this.textBoxT2P1.Size = new System.Drawing.Size(78, 23);
            this.textBoxT2P1.TabIndex = 12;
            this.textBoxT2P1.Text = "aloneTextBox1";
            this.textBoxT2P1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT2P1.UseSystemPasswordChar = false;
            // 
            // textBoxT2P2
            // 
            this.textBoxT2P2.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT2P2.EnabledCalc = true;
            this.textBoxT2P2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT2P2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT2P2.Location = new System.Drawing.Point(255, 123);
            this.textBoxT2P2.MaxLength = 32767;
            this.textBoxT2P2.MultiLine = false;
            this.textBoxT2P2.Name = "textBoxT2P2";
            this.textBoxT2P2.ReadOnly = false;
            this.textBoxT2P2.Size = new System.Drawing.Size(78, 23);
            this.textBoxT2P2.TabIndex = 13;
            this.textBoxT2P2.Text = "aloneTextBox1";
            this.textBoxT2P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT2P2.UseSystemPasswordChar = false;
            // 
            // textBoxT3P1
            // 
            this.textBoxT3P1.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT3P1.EnabledCalc = true;
            this.textBoxT3P1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT3P1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT3P1.Location = new System.Drawing.Point(339, 33);
            this.textBoxT3P1.MaxLength = 32767;
            this.textBoxT3P1.MultiLine = false;
            this.textBoxT3P1.Name = "textBoxT3P1";
            this.textBoxT3P1.ReadOnly = false;
            this.textBoxT3P1.Size = new System.Drawing.Size(78, 23);
            this.textBoxT3P1.TabIndex = 14;
            this.textBoxT3P1.Text = "aloneTextBox1";
            this.textBoxT3P1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT3P1.UseSystemPasswordChar = false;
            // 
            // textBoxT3P2
            // 
            this.textBoxT3P2.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT3P2.EnabledCalc = true;
            this.textBoxT3P2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT3P2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT3P2.Location = new System.Drawing.Point(339, 123);
            this.textBoxT3P2.MaxLength = 32767;
            this.textBoxT3P2.MultiLine = false;
            this.textBoxT3P2.Name = "textBoxT3P2";
            this.textBoxT3P2.ReadOnly = false;
            this.textBoxT3P2.Size = new System.Drawing.Size(78, 23);
            this.textBoxT3P2.TabIndex = 15;
            this.textBoxT3P2.Text = "aloneTextBox1";
            this.textBoxT3P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT3P2.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(171, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "T0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(87, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "T1";
            // 
            // textBoxT1
            // 
            this.textBoxT1.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT1.EnabledCalc = true;
            this.textBoxT1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT1.Location = new System.Drawing.Point(87, 153);
            this.textBoxT1.MaxLength = 32767;
            this.textBoxT1.MultiLine = false;
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.ReadOnly = false;
            this.textBoxT1.Size = new System.Drawing.Size(78, 23);
            this.textBoxT1.TabIndex = 16;
            this.textBoxT1.Text = "aloneTextBox1";
            this.textBoxT1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT1.UseSystemPasswordChar = false;
            // 
            // textBoxT0
            // 
            this.textBoxT0.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT0.EnabledCalc = true;
            this.textBoxT0.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT0.Location = new System.Drawing.Point(171, 153);
            this.textBoxT0.MaxLength = 32767;
            this.textBoxT0.MultiLine = false;
            this.textBoxT0.Name = "textBoxT0";
            this.textBoxT0.ReadOnly = false;
            this.textBoxT0.Size = new System.Drawing.Size(78, 23);
            this.textBoxT0.TabIndex = 17;
            this.textBoxT0.Text = "aloneTextBox1";
            this.textBoxT0.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT0.UseSystemPasswordChar = false;
            // 
            // textBoxT2
            // 
            this.textBoxT2.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT2.EnabledCalc = true;
            this.textBoxT2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT2.Location = new System.Drawing.Point(255, 153);
            this.textBoxT2.MaxLength = 32767;
            this.textBoxT2.MultiLine = false;
            this.textBoxT2.Name = "textBoxT2";
            this.textBoxT2.ReadOnly = false;
            this.textBoxT2.Size = new System.Drawing.Size(78, 23);
            this.textBoxT2.TabIndex = 18;
            this.textBoxT2.Text = "aloneTextBox1";
            this.textBoxT2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT2.UseSystemPasswordChar = false;
            // 
            // textBoxT3
            // 
            this.textBoxT3.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT3.EnabledCalc = true;
            this.textBoxT3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT3.Location = new System.Drawing.Point(339, 153);
            this.textBoxT3.MaxLength = 32767;
            this.textBoxT3.MultiLine = false;
            this.textBoxT3.Name = "textBoxT3";
            this.textBoxT3.ReadOnly = false;
            this.textBoxT3.Size = new System.Drawing.Size(78, 23);
            this.textBoxT3.TabIndex = 19;
            this.textBoxT3.Text = "aloneTextBox1";
            this.textBoxT3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT3.UseSystemPasswordChar = false;
            // 
            // textBoxO1
            // 
            this.textBoxO1.BackColor = System.Drawing.Color.Transparent;
            this.textBoxO1.EnabledCalc = true;
            this.textBoxO1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxO1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxO1.Location = new System.Drawing.Point(423, 33);
            this.textBoxO1.MaxLength = 32767;
            this.textBoxO1.MultiLine = false;
            this.textBoxO1.Name = "textBoxO1";
            this.textBoxO1.ReadOnly = false;
            this.textBoxO1.Size = new System.Drawing.Size(78, 23);
            this.textBoxO1.TabIndex = 20;
            this.textBoxO1.Text = "aloneTextBox1";
            this.textBoxO1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxO1.UseSystemPasswordChar = false;
            // 
            // textBoxO3
            // 
            this.textBoxO3.BackColor = System.Drawing.Color.Transparent;
            this.textBoxO3.EnabledCalc = true;
            this.textBoxO3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxO3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxO3.Location = new System.Drawing.Point(423, 63);
            this.textBoxO3.MaxLength = 32767;
            this.textBoxO3.MultiLine = false;
            this.textBoxO3.Name = "textBoxO3";
            this.textBoxO3.ReadOnly = false;
            this.textBoxO3.Size = new System.Drawing.Size(78, 23);
            this.textBoxO3.TabIndex = 21;
            this.textBoxO3.Text = "aloneTextBox1";
            this.textBoxO3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxO3.UseSystemPasswordChar = false;
            // 
            // textBoxO2
            // 
            this.textBoxO2.BackColor = System.Drawing.Color.Transparent;
            this.textBoxO2.EnabledCalc = true;
            this.textBoxO2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxO2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxO2.Location = new System.Drawing.Point(423, 123);
            this.textBoxO2.MaxLength = 32767;
            this.textBoxO2.MultiLine = false;
            this.textBoxO2.Name = "textBoxO2";
            this.textBoxO2.ReadOnly = false;
            this.textBoxO2.Size = new System.Drawing.Size(78, 23);
            this.textBoxO2.TabIndex = 22;
            this.textBoxO2.Text = "aloneTextBox1";
            this.textBoxO2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxO2.UseSystemPasswordChar = false;
            // 
            // textBoxO4
            // 
            this.textBoxO4.BackColor = System.Drawing.Color.Transparent;
            this.textBoxO4.EnabledCalc = true;
            this.textBoxO4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxO4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxO4.Location = new System.Drawing.Point(423, 93);
            this.textBoxO4.MaxLength = 32767;
            this.textBoxO4.MultiLine = false;
            this.textBoxO4.Name = "textBoxO4";
            this.textBoxO4.ReadOnly = false;
            this.textBoxO4.Size = new System.Drawing.Size(78, 23);
            this.textBoxO4.TabIndex = 23;
            this.textBoxO4.Text = "aloneTextBox1";
            this.textBoxO4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxO4.UseSystemPasswordChar = false;
            // 
            // textBoxT0P4
            // 
            this.textBoxT0P4.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT0P4.EnabledCalc = true;
            this.textBoxT0P4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT0P4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT0P4.Location = new System.Drawing.Point(171, 93);
            this.textBoxT0P4.MaxLength = 32767;
            this.textBoxT0P4.MultiLine = false;
            this.textBoxT0P4.Name = "textBoxT0P4";
            this.textBoxT0P4.ReadOnly = false;
            this.textBoxT0P4.Size = new System.Drawing.Size(78, 23);
            this.textBoxT0P4.TabIndex = 27;
            this.textBoxT0P4.Text = "aloneTextBox1";
            this.textBoxT0P4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT0P4.UseSystemPasswordChar = false;
            // 
            // textBoxT0P3
            // 
            this.textBoxT0P3.BackColor = System.Drawing.Color.Transparent;
            this.textBoxT0P3.EnabledCalc = true;
            this.textBoxT0P3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxT0P3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.textBoxT0P3.Location = new System.Drawing.Point(171, 63);
            this.textBoxT0P3.MaxLength = 32767;
            this.textBoxT0P3.MultiLine = false;
            this.textBoxT0P3.Name = "textBoxT0P3";
            this.textBoxT0P3.ReadOnly = false;
            this.textBoxT0P3.Size = new System.Drawing.Size(78, 23);
            this.textBoxT0P3.TabIndex = 28;
            this.textBoxT0P3.Text = "aloneTextBox1";
            this.textBoxT0P3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxT0P3.UseSystemPasswordChar = false;
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.miniToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.miniToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripStatusLabel1, this.toolStripStatusLabel2 });
            this.miniToolStrip.Location = new System.Drawing.Point(0, 435);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
            this.miniToolStrip.Size = new System.Drawing.Size(800, 33);
            this.miniToolStrip.SizingGrip = false;
            this.miniToolStrip.TabIndex = 1;
            this.miniToolStrip.Text = "crownStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(131, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 468);
            this.Controls.Add(this.miniToolStrip);
            this.Controls.Add(this.airTabPage1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.airTabPage1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.miniToolStrip.ResumeLayout(false);
            this.miniToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
        }

        private ReaLTaiizor.Controls.HopeButton ButtonGetRawData;

        private ReaLTaiizor.Controls.AloneTextBox textBoxT0P3;

        private ReaLTaiizor.Controls.AloneTextBox textBoxT0P4;

        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel2;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel3;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel4;
        private ReaLTaiizor.Controls.MaterialLabel LabelID;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel6;

        private ReaLTaiizor.Controls.AloneComboBox ComRawMode;
        private ReaLTaiizor.Controls.AloneComboBox ComRawDataAvg;
        private ReaLTaiizor.Controls.AloneComboBox ComCalibrateMode;

        private ReaLTaiizor.Controls.AloneTextBox textBoxT3;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT0;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT1;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private ReaLTaiizor.Controls.AloneComboBox ComRawDataJumpPoint;

        private ReaLTaiizor.Controls.AloneTextBox textBoxO1;
        private ReaLTaiizor.Controls.AloneTextBox textBoxO3;
        private ReaLTaiizor.Controls.AloneTextBox textBoxO2;
        private ReaLTaiizor.Controls.AloneTextBox textBoxO4;

        private ReaLTaiizor.Controls.AloneTextBox textBoxT1P1;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT1P2;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT2P1;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT2P2;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT3P1;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT3P2;

        private ReaLTaiizor.Controls.AloneTextBox textBoxT0P2;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private ReaLTaiizor.Controls.AloneTextBox textBoxT0P1;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        private System.Timers.Timer timer1;

        private ReaLTaiizor.Controls.Button ButtonProductConn;

        private ReaLTaiizor.Controls.AloneComboBox ComboxConnMode;
        private ReaLTaiizor.Controls.SkyLabel LabelConnMode;

        private ReaLTaiizor.Controls.Button BottonEvbConnect;

        private ReaLTaiizor.Controls.AloneComboBox ComboxEvbBps;

        private ReaLTaiizor.Controls.AloneComboBox ComboxEvbPortList;

        private ReaLTaiizor.Controls.ComboBoxEdit comboBoxEdit1;

        // private ReaLTaiizor.Controls.AirButton BottonEvbConnect;

        private ReaLTaiizor.Controls.SkyLabel LabelEvbPort;

        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;

        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

        private ReaLTaiizor.Controls.AirTabPage airTabPage1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ReaLTaiizor.Controls.CrownStatusStrip miniToolStrip;

        #endregion
    }
}