// Fusing System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Drawing;
// using System.IO;
// using System.IO.Ports;
// using System.Linq;
// using System.Text;
// using System.Threading;
// using System.Windows.Forms;
// using Newtonsoft.Json;
// using TPLoopTestSystem;
// using Timer = System.Threading.Timer;
//
// namespace SPS01CalibrateApp
// {
//     public partial class Form1 : Form
//     {
//         
//         // 选项卡1 数据
//         private readonly SpsData _spsData = new SpsData();
//         private readonly UiConfig _uiConfig = new UiConfig();
//         private const string ConfigPath = "Config/config.json";
//         
//         private readonly SubCtrlForm subCtrlForm = new SubCtrlForm();
//         private readonly UnoSubForm unoSubForm = new UnoSubForm();
//         
//         private string _pathScript = "";
//         private string _basePath = "";
//         
//         private readonly SqlLocal _sqlLocal;
//         private readonly MySqlLocal _mySqlLocal;
//         // 选项卡2 数据
//         
//         // TODO 采用类 来存储数据
//         private readonly Dictionary<string, int> _halfPressDict = new Dictionary<string, int>();
//         private readonly Dictionary<string, int> _halfPressTarget = new Dictionary<string, int>();
//         
//         private readonly Dictionary<string, int> _pressDict = new Dictionary<string, int>();
//         private readonly Dictionary<string, int> _pressTarget = new Dictionary<string, int>();
//         
//         private readonly TextBox[] _targetTextBoxex = new TextBox[4];
//         private readonly Dictionary<string, int> _tempDict = new Dictionary<string, int>();
//         private readonly TextBox[] _tempTextBoxex = new TextBox[4];
//         private readonly TextBox[] _textBoxes = new TextBox[10];
//         
//         private int _focusIndex;
//         private int _focusTempIndex;
//         // 选项卡3 数据
//         private readonly byte[] _nvm = new byte[256];
//         private readonly byte[] _nvmReg = new byte[256];
//         
//         private string _nvmUpdateMsg = "";
//         private string _regUpdateMsg = "";
//         // 选项卡4 数据
//         private bool _noiseTesting = false;
//         private Timer _noiseTimer;
//         private string _noiseFileName = "";
//         private int _noiseCount = 0;
//         private string _noiseMode = "";
//         private static object _noiseLockObj = new object();
//         
//         // 选项卡5 数据
//         private readonly Button[] _buttons = new Button[10];
//         private readonly TextBox[] _textBoxesPTarget = new TextBox[4];
//         private readonly TextBox[] _textBoxesTTarget = new TextBox[4];
//         private readonly List<SpsCalibration> _spsCalibrations = new List<SpsCalibration>(64);
//         
//
//         public Form1()
//         {
//             InitializeComponent();
//             //数据初始化
//             //PressDict.Add("P1", 0);
//
//
//             Text = "SPS01 Calibrate App " + Application.ProductVersion;
//             //设置文档文件夹为基本数据存储路径
//             var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
//             //创建基于软件名字的文件夹
//             const string folderName = "SPS01CalibrateApp";
//             var path = Path.Combine(documentPath, folderName);
//             if (!Directory.Exists(path)) Directory.CreateDirectory(path);
//             _basePath = path;
//             // 子窗体
//             
//             ChildForm(subCtrlForm, panel1);
//             ChildForm(unoSubForm, panel2);
//
//             // 总选项卡设置
//             // 修改标签页的标题
//             tabPage1.Text = "设置";
//             tabPage2.Text = "手动标定";
//             tabPage3.Text = "寄存器编辑";
//             tabPage4.Text = "数据读取";
//             tabPage5.Text = "自动标定";
//
//             //选项卡1 设置
//             label1.Text = "通信协议选择";
//             comboBox1.Items.AddRange(new object[] { "IIC", "OWI" });
//             label2.Text = "设备地址";
//             buttonConnProduct.Text = "产品连接";
//             buttonProductConnState.Text = "";
//             textBox1.Text = "D8";
//
//             //选项卡2 设置
//             labelIdName.Text = "ID";
//             labelDut.Text = "产品选择";
//             labelID.Text = "";   
//
//             comboBoxDut.Items.AddRange(new object[] {"DUT1", "DUT2", "DUT3", "DUT4", "DUT5", "DUT6", "DUT7", "DUT8", "DUT9", "DUT10", "DUT11", "DUT12", "DUT13", "DUT14", "DUT15", "DUT16", "DUT17", "DUT18", "DUT19", "DUT20", "DUT21", "DUT22", "DUT23", "DUT24", "DUT25", "DUT26", "DUT27", "DUT28", "DUT29", "DUT30", "DUT31", "DUT32", "DUT33", "DUT34", "DUT35", "DUT36", "DUT37", "DUT38", "DUT39", "DUT40", "DUT41", "DUT42", "DUT43", "DUT44", "DUT45", "DUT46", "DUT47", "DUT48", "DUT49", "DUT50", "DUT51", "DUT52", "DUT53", "DUT54", "DUT55", "DUT56", "DUT57", "DUT58", "DUT59", "DUT60", "DUT61", "DUT62", "DUT63", "DUT64"});
//             labeltype.Text = "选择类型";
//             comboBoxtype.Items.AddRange(new object[] { "FullBridge", "HalfBridge" , "BothMode"});
//             labelmode.Text = "选择模式";
//             comboBoxMode.Items.AddRange(new object[]
//                 { "1T2P", "1T3P", "1T4P", "2T2P", "2T3P", "2T4P", "3T2P", "3T3P", "3T4P", "4T2P", "4T3P", "4T4P" });
//             labelJump.Text = "跳过点数";
//             comboBoxJump.Items.AddRange(new object[] { "0", "1", "2" });
//             comboBoxJump.Text = "0";
//             labelAvg.Text = "平均点数";
//             comboBoxAvg.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
//             comboBoxAvg.Text = "1";
//             //comboBoxtype.Text = "FullBridge";
//
//             buttonExportRawData.Text = "导出原始数据";
//             buttonImportRawData.Text = "导入原始数据";
//
//             labelP1.Text = "P1";
//             labelP2.Text = "P2";
//
//             labelP3.Text = "P3";
//             labelP4.Text = "P4";
//
//             labelT0.Text = "T0";
//             labelT1.Text = "T1";
//             labelT2.Text = "T2";
//             labelT3.Text = "T3";
//
//             labelTar.Text = "目标值";
//             buttonGetRawData.Text = "获取原始数据";
//             _textBoxes[0] = textBoxT0P1;
//             _textBoxes[1] = textBoxT0P2;
//             _textBoxes[2] = textBoxT0P3;
//             _textBoxes[3] = textBoxT0P4;
//
//             _textBoxes[4] = textBoxT1P1;
//             _textBoxes[5] = textBoxT1P2;
//             _textBoxes[6] = textBoxT2P1;
//             _textBoxes[7] = textBoxT2P2;
//             _textBoxes[8] = textBoxT3P1;
//             _textBoxes[9] = textBoxT3P2;
//
//             for (var i = 0; i < 10; i++)
//             {
//                 _pressDict.Add(_textBoxes[i].Name, 0);
//                 _halfPressDict.Add(_textBoxes[i].Name, 0);
//             }
//
//             _targetTextBoxex[0] = textBoxO1;
//             _targetTextBoxex[1] = textBoxO2;
//             _targetTextBoxex[2] = textBoxO3;
//             _targetTextBoxex[3] = textBoxO4;
//
//             for (var i = 0; i < 4; i++)
//             {
//                 _pressTarget.Add(_targetTextBoxex[i].Name, 0);
//                 _halfPressTarget.Add(_targetTextBoxex[i].Name, 0);
//             }
//
//             _tempTextBoxex[0] = textBoxT0;
//             _tempTextBoxex[1] = textBoxT1;
//             _tempTextBoxex[2] = textBoxT2;
//             _tempTextBoxex[3] = textBoxT3;
//
//             for (var i = 0; i < 4; i++)
//             {
//                 _tempDict.Add(_tempTextBoxex[i].Name, 0);
//                 _tempTextBoxex[i].ReadOnly = true;
//             }
//
//             labelLoadFile.Text = "点我选择脚本文件：";
//             buttonRunScript.Text = "运行脚本";
//
//
//             //选项卡3 寄存器操作
//             buttonup.Text = "上一页";
//             buttondown.Text = "下一页";
//             labelNVMUpdatedMsg.Text = "";
//             textBox2.Text = "1";
//             textBox2.TextAlign = HorizontalAlignment.Center;
//             textBox2.Enabled = false;
//             comboBoxMemMode.Items.AddRange(new[] { "NVM", "NVMReg" });
//             buttonExportRegData.Text = "导出配置数据";
//             //comboBox2.Text = "NVM";
//             buttonreg.Text = "加载临时寄存器";
//             buttonnvm.Text = "加载永久寄存器";
//
//             // 设置dataGridView1 行数和行标签
//             dataGridView1.RowCount = 8;
//             dataGridView1.RowHeadersVisible = true;
//
//             // 设置行头宽度
//             dataGridView1.RowHeadersWidth = 15; // 根据需要调整
//             dataGridView1.RowHeadersVisible = false; // 显示行头
//             // 添加自定义列
//
//             DataGridViewColumn rowNumberColumn = new DataGridViewTextBoxColumn();
//             rowNumberColumn.HeaderText = "行号";
//             rowNumberColumn.Name = "RowNumber";
//             rowNumberColumn.ReadOnly = true; // 设置为只读
//             rowNumberColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // 右对齐
//
//             // 将新列添加到列集合中
//             dataGridView1.Columns.Insert(0, rowNumberColumn);
//
//             // 设置列的宽度
//             rowNumberColumn.Width = 80; // 根据需要调整
//
//
//             dataGridView1.Size = new Size(80 * 9, 23 * 8 + 21);
//             dataGridView1.ScrollBars = ScrollBars.None;
//
//             //选项卡4 设置
//
//             labelOutMemAddr.Text = "选择待读取内容";
//             comboBoxOutMemAddr.Items.AddRange(subCtrlForm.Spscom.RawAddr.Keys.ToArray());
//             buttonGetValue.Text = "获取数据";
//             buttonLoop.Text = "循环测试";
//             labelDataList.Text = "数据清单";
//             buttonGetAllData.Text = "获取所有输出数据";
//             
//             //选项卡5 设置
//             
//             labelTypeAuto.Text = "选择类型";
//             comboBoxTypeAuto.Items.AddRange(new object[] { "FullBridge", "HalfBridge" });
//             labelModeAuto.Text = "选择模式";
//             comboBoxModeAuto.Items.AddRange(new object[]
//                 { "1T2P", "1T3P", "1T4P", "2T2P", "2T3P", "2T4P", "3T2P", "3T3P", "3T4P", "4T2P", "4T3P", "4T4P" });
//             labelJumpAuto.Text = "跳过点数";
//             comboBoxJumpAuto.Items.AddRange(new object[] { "0", "1", "2" });
//             comboBoxJumpAuto.Text = "0";
//             labelAvgAuto.Text = "平均点数";
//             comboBoxAvgAuto.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
//             comboBoxAvgAuto.Text = "1";
//
//             _buttons[0] = ButtonT0P1;
//             _buttons[1] = ButtonT0P2;
//             _buttons[2] = ButtonT0P3;
//             _buttons[3] = ButtonT0P4;
//             _buttons[4] = ButtonT1P1;
//             _buttons[5] = ButtonT1P2;
//             _buttons[6] = ButtonT2P1;
//             _buttons[7] = ButtonT2P2;
//             _buttons[8] = ButtonT3P1;
//             _buttons[9] = ButtonT3P2;
//             ButtonT0P1.Click += GetRawData;
//             ButtonT0P2.Click += GetRawData;
//             ButtonT0P3.Click += GetRawData;
//             ButtonT0P4.Click += GetRawData;
//             ButtonT1P1.Click += GetRawData;
//             ButtonT1P2.Click += GetRawData;
//             ButtonT2P1.Click += GetRawData;
//             ButtonT2P2.Click += GetRawData;
//             ButtonT3P1.Click += GetRawData;
//             ButtonT3P2.Click += GetRawData;
//             
//             for (var i = 0; i < 10; i++)
//             {
//                 _buttons[i].Text = _buttons[i].Name.Substring(6);
//             }
//
//             _textBoxesPTarget[0] = textBoxTarP1;
//             _textBoxesPTarget[1] = textBoxTarP2;
//             _textBoxesPTarget[2] = textBoxTarP3;
//             _textBoxesPTarget[3] = textBoxTarP4;
//             
//             _textBoxesTTarget[0] = textBoxTarT0;
//             _textBoxesTTarget[1] = textBoxTarT1;
//             _textBoxesTTarget[2] = textBoxTarT2;
//             _textBoxesTTarget[3] = textBoxTarT3;
//
//             for (var i = 0; i < 64; i++)
//             {
//                 _spsCalibrations.Add(new SpsCalibration());
//             }
//             
//             // 底部状态栏
//             toolStripStatusLabel3.Text = "";
//
//             // 创建文件夹
//             if (!Directory.Exists("Config"))
//                 Directory.CreateDirectory("Config");
//
//             // 配置加载
//             ConfigLoad();
//             
//             _sqlLocal = new SqlLocal
//             {
//                 Database = "SPS01CalibrateApp.db"
//             };
//             _sqlLocal.Connect();
//             const string creatTableSql = "CREATE TABLE IF NOT EXISTS spsdata (" +
//                                          "Id varchar(8) PRIMARY KEY, " +
//                                          "Data varchar(255), " +
//                                          "P1raw varchar(255), " +
//                                          "P2raw varchar(255), " +
//                                          "TSI varchar(255), " +
//                                          "TSE varchar(255), " +
//                                          "Press varchar(255), " +
//                                          "Temp varchar(255), " +
//                                          "Tar1 varchar(255), " +
//                                          "Tar2 varchar(255), " +
//                                          "TSEMode varchar(255), " +
//                                          "Coefficient1 varchar(255), " +
//                                          "Coefficient2 varchar(255), " +
//                                          "CreatTime datetime, " +
//                                          "UpdateTime datetime)";
//           
//             _sqlLocal.CreateTable(creatTableSql);
//
//             _mySqlLocal = new MySqlLocal()
//             {
//                 Server = "172.20.100.20",
//                 User = "Link_pb",
//                 Password = "link*1234",
//                 Database = "product_base_sps01"
//             };
//             try
//             {
//                 _mySqlLocal.Connect();
//                 _mySqlLocal.IsConnected = true;
//                 const string createTableSql = "CREATE TABLE IF NOT EXISTS spsdata (" +
//                                               "Id varchar(8) PRIMARY KEY, " +
//                                               "Data varchar(255), " +
//                                               "P1raw varchar(255), " +
//                                               "P2raw varchar(255), " +
//                                               "TSI varchar(255), " +
//                                               "TSE varchar(255), " +
//                                               "Press varchar(255), " +
//                                               "Temp varchar(255), " +
//                                               "Tar1 varchar(255), " +
//                                               "Tar2 varchar(255), " +
//                                               "TSEMode varchar(255), " +
//                                               "Coefficient1 varchar(255), " +
//                                               "Coefficient2 varchar(255), " +
//                                               "CreatTime datetime, " +
//                                               "UpdateTime datetime)";
//                 _mySqlLocal.CreateTable(createTableSql);
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//                 _mySqlLocal.IsConnected = false;
//             }
//             
//         }
//
//         public sealed override string Text
//         {
//             get => base.Text;
//             set => base.Text = value;
//         }
//         
//         // 加载配置
//         private void ConfigLoad()
//         {
//             // 配置加载
//             if (!File.Exists(ConfigPath)) return;
//             // 加载Json文件
//             var json = File.ReadAllText(ConfigPath);
//             // 反序列化Json字符串
//             var config = JsonConvert.DeserializeObject<UiConfig>(json);
//             // 读取配置信息
//             comboBox1.Text = config.commMode;
//             subCtrlForm.comboBox1.Text = config.evbPort;
//             if (config.evbPort != "" && SerialPort.GetPortNames().Contains(config.evbPort))
//             {
//                 subCtrlForm.button1_Click(null, null);
//             }
//         }
//
//         // 保存配置
//         private void ConfigSave()
//         {
//             var json = JsonConvert.SerializeObject(_uiConfig, Formatting.Indented);
//
//             // 保存Json字符串到文件
//             File.WriteAllText(ConfigPath, json);
//             
//         }
//         
//         // 子窗体加载
//         private void panel1_Paint(object sender, PaintEventArgs e)
//         {
//         }
//
//         // 子窗体加载
//         private void tabPage1_Click(object sender, EventArgs e)
//         {
//         }
//
//         // 子窗体加载
//         private static void ChildForm(Form childForm, Panel panel)
//         {
//             childForm.TopLevel = false;
//             childForm.FormBorderStyle = FormBorderStyle.None; //让窗体无边界
//             childForm.Dock = DockStyle.Fill;
//
//             panel.Controls.Add(childForm);
//             panel.Tag = childForm;
//             childForm.BringToFront();
//             childForm.Show(); //显示子窗体
//         }
//
//         // 连接产品
//         private void Button1ClickForProductConnect(object sender, EventArgs e)
//         {
//             if (comboBox1.Text == "") return;
//
//             if (subCtrlForm.PortName == "")
//             {
//                 MessageBox.Show("请先选择串口");
//                 buttonProductConnState.Text = "";
//                 buttonProductConnState.BackColor = Color.White;
//                 return;
//             }
//
//             subCtrlForm.Spscom.ConnModeName = comboBox1.Text;
//             if (subCtrlForm.button2.Text != "已连接")
//             {
//                 buttonProductConnState.Text = "";
//                 buttonProductConnState.BackColor = Color.White;
//
//                 return;
//             }
//
//             if (buttonProductConnState.Text == "已连接")
//             {
//                 subCtrlForm.Spscom.Pd2Nm();
//                 buttonProductConnState.Text = "";
//                 buttonProductConnState.BackColor = Color.White;
//                 Console.WriteLine("关闭");
//                 return;
//             }
//
//             subCtrlForm.Spscom.DeviceAddr = textBox1.Text;
//
//             if (subCtrlForm.Spscom.ConnPd())
//             {
//                 buttonProductConnState.Text = "已连接";
//                 buttonProductConnState.BackColor = Color.Green;
//                 _uiConfig.evbPort = subCtrlForm.PortName;
//                 _uiConfig.commMode = comboBox1.Text;
//
//                 ConfigSave();
//                 // 读取ID
//                 labelID.Text = subCtrlForm.Spscom.GetId();
//                 if (labelID.Text != "00000000") return;
//                 var countResult = _sqlLocal.Select("SELECT count(*) FROM SpsData");
//                 var count = 0;
//                 if (countResult.Length != 0)
//                 {
//                     count= Convert.ToInt32(countResult[0][0]);
//                         
//                 }
//                 var id = "";
//                 // var count
//                 var idcount = 0;
//                 while (true){
//                     idcount++;
//                     id = (int.Parse(count.ToString())+idcount).ToString("X8");
//                     // 先查询ID是否存在
//                     var indexResult = _sqlLocal.Select("SELECT * FROM SpsData where id='" + id + "'");
//                     if (indexResult.Length == 0) break;
//                             
//                     // labelID.Text = id;
//                 }
//                             
//                 // labelID.Text = id;
//                 // 插入数据
//                 var insertSql = "INSERT INTO SpsData (Id, Data, CreatTime, UpdateTime) VALUES ('@id', '@data', '@creatTime', '@updateTime')";
//                 insertSql = insertSql.Replace("@id", id);
//                 insertSql = insertSql.Replace("@data", "");
//                 insertSql = insertSql.Replace("@creatTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//                 insertSql = insertSql.Replace("@updateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//                 _sqlLocal.Insert(insertSql);
//                 // 写入ID
//                 subCtrlForm.Spscom.SetId(labelID.Text);
//                 // 读取ID
//                 labelID.Text = subCtrlForm.Spscom.GetId();
//             }
//             else
//             {
//                 buttonProductConnState.Text = "连接失败";
//                 buttonProductConnState.BackColor = Color.Red;
//             }
//             
//             
//         }
//         
//         private void Form1_Load(object sender, EventArgs e)
//         {
//             timer1.Interval = 100;
//             timer1.Start();
//         }
//
//         private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
//         {
//         }
//
//         private void Button4ClickForNvmTableNextPage(object sender, EventArgs e)
//         {
//             // 选项卡3 下一页
//             textBox2.Text = (int.Parse(textBox2.Text) + 1).ToString();
//             dataGridView1_CellValueAdd();
//         }
//
//         private void Button3ClickForNvmTableLastPage(object sender, EventArgs e)
//         {
//             // 选项卡3 上一页
//             textBox2.Text = (int.Parse(textBox2.Text) - 1).ToString();
//             dataGridView1_CellValueAdd();
//         }
//
//         private void Button5ClickForReadNvmRegMsg(object sender, EventArgs e)
//         {
//             // 选项卡3 加载临时寄存器
//             Debug.WriteLine("加载临时寄存器");
//             var reg = subCtrlForm.Spscom.GetAllReg();
//             //subCtrlForm.Spscom.GetAllReg();
//             if (reg.Length < 512) return;
//             for (var i = 0; i < 256; i++) _nvmReg[i] = Convert.ToByte(reg.Substring(i * 2, 2), 16);
//             
//             _regUpdateMsg = "Reg Update @" + DateTime.Now.ToString("HH:mm:ss");
//             ComboBox2ForNvmModeSelectedIndexChanged(null, null);
//
//         }
//
//         private void Button6ClickForReadNvmMsg(object sender, EventArgs e)
//         {
//             // 选项卡3 加载永久寄存器
//             var result = subCtrlForm.Spscom.GetAllNvm();
//             if (result.Length < 512) return;
//             
//             for (var i = 0; i < 256; i++) _nvm[i] = Convert.ToByte(result.Substring(i * 2, 2), 16);
//             
//             _nvmUpdateMsg = "Nvm Update @" + DateTime.Now.ToString("HH:mm:ss");
//             ComboBox2ForNvmModeSelectedIndexChanged(null, null);
//
//         }
//
//         private void ComboBox2ForNvmModeSelectedIndexChanged(object sender, EventArgs e)
//         {
//             // 选项卡3 数据改变
//             dataGridView1_CellValueAdd();
//
//
//             // 设置字体样式为粗体，微软雅黑
//             labelNVMUpdatedMsg.Font = new Font("微软雅黑", 10, FontStyle.Bold);
//             switch (comboBoxMemMode.Text)
//             {
//                 case "NVM":
//                 {
//                     labelNVMUpdatedMsg.Text = _nvmUpdateMsg;
//                     labelNVMUpdatedMsg.ForeColor = Color.Blue;
//                     break;
//                 }
//                 case "NVMReg":
//                 {
//                     labelNVMUpdatedMsg.Text = _regUpdateMsg;
//                     labelNVMUpdatedMsg.ForeColor = Color.Green;
//                     break;
//                 }
//             }
//         }
//
//         private void dataGridView1_CellValueAdd()
//         {
//             switch (comboBoxMemMode.Text)
//             {
//                 // 选项卡3 数据改变
//                 case "NVM":
//                 {
//                     for (var i = 0; i < 8; i++)
//                     for (var j = 1; j < 9; j++)
//                         dataGridView1.Rows[i].Cells["Column" + j].Value =
//                             _nvm[(int.Parse(textBox2.Text) - 1) * 64 + i * 8 + j - 1].ToString("X2");
//                     //nvm[(int.Parse(textBox2.Text) - 1) * 64 + e.RowIndex * 8 + e.ColumnIndex] = Convert.ToByte(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
//                     break;
//                 }
//                 case "NVMReg":
//                 {
//                     for (var i = 0; i < 8; i++)
//                     for (var j = 1; j < 9; j++)
//                         dataGridView1.Rows[i].Cells["Column" + j].Value =
//                             _nvmReg[(int.Parse(textBox2.Text) - 1) * 64 + i * 8 + j - 1].ToString("X2");
//                     //nvmReg[(int.Parse(textBox2.Text) - 1) * 64 + e.RowIndex * 8 + e.ColumnIndex] = Convert.ToByte(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
//                     break;
//                 }
//             }
//         }
//
//         private void tabPage2_Click(object sender, EventArgs e)
//         {
//         }
//
//         private void ButtonClickForGetRawData(object sender, EventArgs e)
//         {
//             // 选项卡2 获取原始数据
//
//             // 读取P1 通道压力数据
//             var rawPressData = 0;
//             subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//             if (comboBoxtype.Text == "BothMode")
//             {
//                 rawPressData = subCtrlForm.Spscom.GetRawdata("P1", int.Parse(comboBoxJump.Text), int.Parse(comboBoxAvg.Text));
//
//                 if (rawPressData > 0x7FFF) rawPressData -= 0x10000;
//                 //赋值
//                 _textBoxes[_focusIndex].Text = rawPressData.ToString();
//                 _pressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//                 //PressDict.Add(textBoxes[foucsIndex].Name, value);
//
//                 rawPressData = subCtrlForm.Spscom.GetRawdata("P2", int.Parse(comboBoxJump.Text), int.Parse(comboBoxAvg.Text));
//
//                 if (rawPressData > 0x7FFF) rawPressData -= 0x10000;
//                 //赋值
//                 _halfPressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//
//             }
//             else
//             {
//                 var rawName = "";
//                 switch (comboBoxtype.Text)
//                 {
//                     case "FullBridge":
//                         rawName = "P1";
//                         break;
//                     case "HalfBridge":
//                         rawName = "P2";
//                         break;
//                     default:
//                         return;
//                 }
//
//                 rawPressData = subCtrlForm.Spscom.GetRawdata(rawName, int.Parse(comboBoxJump.Text), int.Parse(comboBoxAvg.Text));
//
//                 if (rawPressData > 0x7FFF) rawPressData -= 0x10000;
//                 //赋值
//                 _textBoxes[_focusIndex].Text = rawPressData.ToString();
//                 switch (comboBoxtype.Text)
//                 {
//                     //存入字典中
//                     case "FullBridge":
//                         _pressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//                         break;
//                     case "HalfBridge":
//                         _halfPressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//                         break;
//                 }
//                 //PressDict.Add(textBoxes[foucsIndex].Name, value);
//
//             }
//                 //读取温度数值
//                 var rawTempData = subCtrlForm.Spscom.GetRawdata("TSIO", 0, 1);
//                 if (rawTempData > 0x7FFF) rawTempData = rawTempData - 0x10000;
//                 rawTempData = rawTempData >> 6;
//                 _tempTextBoxex[_focusTempIndex].Text = rawTempData.ToString();
//                 //textBoxTSI.Text = value1.ToString();
//                 _tempDict[_tempTextBoxex[_focusTempIndex].Name] = rawTempData;
//
//                 Debug.WriteLine(_textBoxes[_focusIndex].Name, rawPressData.ToString(), rawTempData.ToString());
//                 
//                 toolStripStatusLabel3.Text = DateTime.Now.ToString("HH:mm:ss") + " >> " + comboBoxtype.Text + " Mode Raw Data Get @" + _textBoxes[_focusIndex].Name;
//         }
//         
//         private void GetRawData(object sender, EventArgs e)
//         {
//             // 在这里编写按钮点击后要执行的通用代码
//             Button clickedButton = (Button)sender;
//             var tempPoint = clickedButton.Name.Substring(7,1);
//             var pressPoint = clickedButton.Name.Substring(9,1);
//             var point = clickedButton.Name.Substring(6,4);
//             var rawPressData = 0;
//             for (var i = 0; i < 64; i++)
//             {
//                 
//                 if (!_spsCalibrations[i].Status)
//                     return;
//                 subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//                 
//                 if (comboBoxTypeAuto.Text == "BothMode")
//                 {
//                     rawPressData = subCtrlForm.Spscom.GetRawdata("P1", int.Parse(comboBoxJump.Text), int.Parse(comboBoxAvg.Text));
//
//                     if (rawPressData > 0x7FFF) rawPressData -= 0x10000;
//                     //赋值
//                     _spsCalibrations[i].FullBridgeRawData[pressPoint] = rawPressData;
//                     // _textBoxes[_focusIndex].Text = rawPressData.ToString();
//                     // _pressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//                     //PressDict.Add(textBoxes[foucsIndex].Name, value);
//
//                     rawPressData = subCtrlForm.Spscom.GetRawdata("P2", int.Parse(comboBoxJump.Text), int.Parse(comboBoxAvg.Text));
//
//                     if (rawPressData > 0x7FFF) rawPressData -= 0x10000;
//                     //赋值
//                     // _halfPressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//
//                     _spsCalibrations[i].HalfBridgeRawData[pressPoint] = rawPressData;
//                 }
//                 else
//                 {
//        
//                     var rawName = "";
//                     switch (comboBoxTypeAuto.Text)
//                     {
//                         case "FullBridge":
//                             rawName = "P1";
//                             break;
//                         case "HalfBridge":
//                             rawName = "P2";
//                             break;
//                         default:
//                             return;
//                     }
//
//                     rawPressData = subCtrlForm.Spscom.GetRawdata(rawName, int.Parse(comboBoxJumpAuto.Text), int.Parse(comboBoxAvgAuto.Text));
//                     if (rawPressData > 0x7FFF) rawPressData -= 0x10000;
//                     switch (rawName)
//                     {
//                         // rawPressData = _spsCalibrations[i]
//                         case "P1":
//                             _spsCalibrations[i].FullBridgeRawData[point] = rawPressData;
//                             break;
//                         case "P2":
//                             _spsCalibrations[i].HalfBridgeRawData[point] = rawPressData;
//                             break;
//                     }
//
//                     //赋值
//                     // _textBoxes[_focusIndex].Text = rawPressData.ToString();
//                     // switch (comboBoxtype.Text)
//                     // {
//                     //     //存入字典中
//                     //     case "FullBridge":
//                     //         _pressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//                     //         break;
//                     //     case "HalfBridge":
//                     //         _halfPressDict[_textBoxes[_focusIndex].Name] = rawPressData;
//                     //         break;
//                     // }
//                     // //PressDict.Add(textBoxes[foucsIndex].Name, value);
//
//                 }
//                     //读取温度数值
//                 var rawTempData = subCtrlForm.Spscom.GetRawdata("TSIO", 0, 1);
//                 if (rawTempData > 0x7FFF) rawTempData -= 0x10000;
//                 rawTempData >>= 6;
//
//                 _spsCalibrations[i].TsiTempRaw[point.Substring(0,2)] = rawTempData;
//
//
//                 Debug.WriteLine("FB"+_spsCalibrations[i].FullBridgeRawData[point].ToString());
//                 Debug.WriteLine("HB"+_spsCalibrations[i].HalfBridgeRawData[point].ToString());
//                 Debug.WriteLine("TSI"+_spsCalibrations[i].TsiTempRaw[point.Substring(0,2)].ToString());
//                 // Console.WriteLine(_textBoxes[_focusTempIndex].Name, rawPressData.ToString(), rawTempData.ToString());
//             }
//             toolStripStatusLabel3.Text = DateTime.Now.ToString("HH:mm:ss") + " >> " + comboBoxtype.Text + " Mode Raw Data Get @" + point;
//             
//         }
//
//         //加载脚本文件
//         private void label3_Click(object sender, EventArgs e)
//         {
//             // 选项卡2 加载脚本文件
//
//             openFileDialog1.Filter = "文本文件|*.txt";
//             openFileDialog1.Title = "选择脚本文件";
//             openFileDialog1.FileName = "";
//             openFileDialog1.ShowDialog();
//             var path = openFileDialog1.FileName;
//             _pathScript = path;
//             if (path == "") return;
//
//             var sr = new StreamReader(path, Encoding.Default);
//             string line;
//             textBoxScripts.Text = "";
//             while ((line = sr.ReadLine()) != null)
//                 //string[] str = line.Split(' ');
//                 textBoxScripts.AppendText(line + "\n");
//
//             labelLoadFile.Text = "点我选择脚本文件：" + "\n" + path;
//         }
//
//         //脚本执行
//         private void buttonRunScript_Click(object sender, EventArgs e)
//         {
//             // 选项卡2 运行脚本
//             var sr = new StreamReader(_pathScript, Encoding.Default);
//             string line;
//             textBoxScripts.Text = "";
//             while ((line = sr.ReadLine()) != null)
//                 //string[] str = line.Split(' ');
//                 textBoxScripts.AppendText(line + "\n");
//
//             try
//             {
//                 subCtrlForm.Spscom.SetComMode("STRT_CM", "01", true);
//                 var str = textBoxScripts.Text.Split('\n');
//                 foreach (var t in str)
//                 {
//                     Debug.WriteLine(t);
//                     if (t == "") continue;
//
//                     if (t.Substring(0, 1) == "#" || t.Substring(0, 2) == "//") continue;
//
//                     if (t.Contains("delay"))
//                     {
//                         var str1 = t.Split('=');
//                         Thread.Sleep(int.Parse(str1[1]));
//                     }
//                     else if (t.Contains("#") && t.Substring(0, 1) != "#")
//                     {
//                         var str1 = t.Split('#');
//                         subCtrlForm.Spscom.RunScript(str1[0]);
//                         //System.Diagnostics.Debug.WriteLine(str1[1]);
//                     }
//                     else
//                     {
//                         subCtrlForm.Spscom.RunScript(t);
//                         //System.Diagnostics.Debug.WriteLine(str[i]);
//                     }
//                 }
//
//                 MessageBox.Show("脚本运行完成");
//             }
//             catch
//             {
//                 MessageBox.Show("脚本存在错误");
//             }
//             finally
//             {
//                 //subCtrlForm.Spscom.SetComMode("STRT_CM", "00", true);
//                 sr.Close();
//             }
//         }
//
//         // 标定模式选择
//         private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
//         {
//             // 选项卡2 选择标定模式
//             var str = comboBoxMode.Text;
//             var tempCount = int.Parse(str.Substring(0, 1));
//             var pressCount = int.Parse(str.Substring(2, 1));
//             //string[] str2 = str1[1].Split('P');
//             for (var i = 0; i < 10; i++)
//                 switch (tempCount)
//                 {
//                     //textBoxes[i].Text = "";
//                     case 1 when i > 3:
//                         _textBoxes[i].Enabled = false;
//                         _textBoxes[i].Text = "";
//                         break;
//                     case 2 when i > 5:
//                         _textBoxes[i].Enabled = false;
//                         _textBoxes[i].Text = "";
//                         break;
//                     case 3 when i > 7:
//                         _textBoxes[i].Enabled = false;
//                         _textBoxes[i].Text = "";
//                         break;
//                     default:
//                     {
//                         switch (pressCount)
//                         {
//                             case 2 when i > 1 && i < 4:
//                                 _textBoxes[i].Enabled = false;
//                                 _textBoxes[i].Text = "";
//                                 break;
//                             case 3 when i > 2 && i < 4:
//                                 _textBoxes[i].Enabled = false;
//                                 _textBoxes[i].Text = "";
//                                 break;
//                             case 4 when i > 3 && i < 4:
//                                 _textBoxes[i].Enabled = false;
//                                 _textBoxes[i].Text = "";
//                                 break;
//                             default:
//                                 _textBoxes[i].Enabled = true;
//                                 break;
//                         }
//
//                         break;
//                     }
//                 }
//         }
//
//         // 循环检测
//         private void timer1_Tick(object sender, EventArgs e)
//         {
//             switch (textBox2.Text)
//             {
//                 // 选项卡3 翻页检测
//                 case "1":
//                     buttonup.Enabled = false;
//                     break;
//                 case "4":
//                     buttondown.Enabled = false;
//                     break;
//                 default:
//                     buttonup.Enabled = true;
//                     buttondown.Enabled = true;
//                     break;
//             }
//
//             // 根据当前页添加行首
//             for (var i = 0; i < dataGridView1.Rows.Count; i++)
//                 //转16进制显示
//                 dataGridView1.Rows[i].Cells["RowNumber"].Value =
//                     "0x" + ((int.Parse(textBox2.Text) - 1) * 64 + i * 8).ToString("X2");
//
//             //根据选中的框来设定哪一个需要填写数据
//             for (var i = 0; i < 10; i++)
//             {
//                 if (!_textBoxes[i].ContainsFocus) continue;
//                 _focusIndex = i;
//                 _focusTempIndex = int.Parse(_textBoxes[i].Name.Substring(8, 1));
//                 break;
//             }
//
//             dataGridView1_CellValueAdd();
//             
//             toolStripStatusLabel4.Text = DateTime.Now.ToString("yyyy-M-d HH:mm:ss");
//
//             if (DateTime.Now.Minute != 41 && DateTime.Now.Second == 0)
//             {
//                 DataSync();
//             }
//         }
//
//         private void tabPage4_Click(object sender, EventArgs e)
//         {
//         }
//
//         //获取指定模式下的输出
//         private void buttonGetValue_Click(object sender, EventArgs e)
//         {
//             //选项卡4
//             //判定comboBoxOutMemAddr.Text是否为空
//             if (comboBoxOutMemAddr.Text == "")
//             {
//                 MessageBox.Show("请选择待读取内容");
//                 return;
//             }
//
//             subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//             int value;
//             if (comboBoxOutMemAddr.Text.Contains("OFF") || comboBoxOutMemAddr.Text.Contains("FG"))
//                 value = subCtrlForm.Spscom.GetMidData(comboBoxOutMemAddr.Text, 0, 1);
//             else
//                 value = subCtrlForm.Spscom.GetRawdata(comboBoxOutMemAddr.Text, 0, 5);
//             textBoxValue.Text = value.ToString();
//         }
//
//         private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
//         {
//         }
//
//         private void comboBoxtype_SelectedIndexChanged(object sender, EventArgs e)
//         {
//             switch (comboBoxtype.Text)
//             {
//                 case "FullBridge":
//                 {
//                     foreach (var item in _textBoxes)
//                         item.Text = _pressDict[item.Name] != 0 ? _pressDict[item.Name].ToString() : "";
//
//                     break;
//                 }
//                 case "HalfBridge":
//                 {
//                     foreach (var item in _textBoxes)
//                         item.Text = _halfPressDict[item.Name] != 0 ? _halfPressDict[item.Name].ToString() : "";
//
//                     break;
//                 }
//             }
//         }
//
//         private void buttonLoop_Click(object sender, EventArgs e)
//         {
//             // 选项卡四 循环读取
//             // 双按钮对话框
//             
//             if (!_noiseTesting)
//             {
//
//                 var dr = MessageBox.Show("是否开始循环读取数据", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                 if (dr == DialogResult.No) return;
//                 _noiseFileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff_")+ comboBoxOutMemAddr.Text;
//                 _noiseCount = 0;
//                 _noiseTesting = true;
//                 _noiseMode = comboBoxOutMemAddr.Text;
//                 subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//                 _noiseTimer = new Timer(TimerCallback, null, 0, 80);
//                 buttonLoop.Text = "停止任务";
//             }
//             else
//             {
//                 var dr = MessageBox.Show("是否停止循环读取数据", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                 if (dr == DialogResult.No) return;
//                 _noiseTesting = false;
//                 _noiseTimer.Dispose();
//                 MessageBox.Show("噪声循环测试终止");
//                 buttonLoop.Text = "循环测试";
//                 toolStripStatusLabel3.Text = _basePath +"\\" + _noiseFileName + ".csv";
//             }
//         }
//
//         private void TimerCallback(object state)
//         {
//             if (!_noiseTesting)
//             {
//                 _noiseTimer.Change(Timeout.Infinite, Timeout.Infinite);
//
//                 return;
//             }
//             try
//             {
//                 _noiseCount++;
//                 if (_noiseCount % 1000 == 0)
//                 {
//                     
//                     _noiseTesting = false;
//                     MessageBox.Show("噪声循环测试完成");
//                     toolStripStatusLabel3.Text = _basePath +"\\" + _noiseFileName + ".csv";
//                     _noiseTimer.Dispose();
//                     // buttonLoop.Text = "循环测试";
//                     // buttonLoopText("循环测试");
//                     buttonLoop.Invoke(new Action(() =>
//                     {
//                         buttonLoop.Text = "循环测试";
//                     }));
//                     return;
//                 }
//                     
//                 var rec = "";
//                 var value = 0;
//
//
//                 lock (_noiseLockObj)
//                 {
//                     value = subCtrlForm.Spscom.GetRawdata(_noiseMode, 0, 1);
//                     // textBoxValue.Text = value.ToString();
//                     rec += value + "\n";
//                     File.AppendAllText( _basePath +"\\" + _noiseFileName+".csv", rec);
//                 }
//                 
//             }
//             
//             catch (Exception ex)
//             {
//                 Debug.WriteLine(ex.Message);
//                 toolStripStatusLabel3.Text = "噪声循环读取失败";
//                 buttonLoop.Invoke(new Action(() =>
//                 {
//                     buttonLoop.Text = "新的按钮文本";
//                 }));
//                 _noiseTesting = false;
//             }
//         }
//         
//         private void buttonExportRegData_Click(object sender, EventArgs e)
//         {
//             if (!subCtrlForm.Spscom.SerialPort.IsOpen)
//             {
//                 MessageBox.Show("串口已关闭");
//                 return;
//             }
//
//             Button5ClickForReadNvmRegMsg(null, null);
//             Button6ClickForReadNvmMsg(null, null);
//             // 将数组nvmreg输出到.csv文件
//             
//
//             var timestr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
//             var fileName = timestr + "_" + _nvmReg[0xF9].ToString("X2") + _nvmReg[0xFA].ToString("X2") +
//                            _nvmReg[0xFB].ToString("X2") + _nvmReg[0xFC].ToString("X2") + "_" + "NVMReg.json";
//             try
//             {
//                 _spsData.ID = _nvmReg[0xF9].ToString("X2") + _nvmReg[0xFA].ToString("X2") +
//                               _nvmReg[0xFB].ToString("X2") + _nvmReg[0xFC].ToString("X2");
//                 //spsData.NvmData = nvmReg;
//                 var nvmData = new string[0xF];
//                 for (var i = 0; i < 0xF; i++)
//                 {
//                     var nvmStr = "";
//                     // 每16个组成一个字符串
//                     for (var j = 0; j < 0xF; j++)
//                     {
//                         nvmStr += _nvmReg[i * 0xF + j].ToString("X2");
//                     }
//                     nvmData[i] = nvmStr;
//                 }
//                 _spsData.RegData = nvmData;
//                 for (var i = 0; i < 0xF; i++)
//                 {
//                     var nvmStr = "";
//                     for (var j = 0; j < 0xF; j++)
//                     {
//                         nvmStr += _nvmReg[i * 0xF + j].ToString("X2");
//                     }
//                     nvmData[i] = nvmStr;
//                 }
//                 _spsData.NvmData = nvmData;
//
//                 P1FactorTrans();
//                 P2FactorTrans();
//                 TSIFactorTrans();
//                 TSEFactorTrans();
//
//
//                 P1compute();
//                 P2compute();
//                 TSIcompute();
//                 TSEcompute();
//                 var jsonString = JsonConvert.SerializeObject(_spsData, Formatting.Indented);
//
//                 //Console.WriteLine(jsonString);
//                 File.WriteAllText(_basePath + "\\" + fileName, jsonString);
//                 Console.WriteLine("导出成功！");
//                 toolStripStatusLabel3.Text = "文件路径" + _basePath + "\\" + fileName;
//                 //toolStripStatusLabel3.Text = "文件路径：" + Directory.GetCurrentDirectory();
//                 //Console.WriteLine("文件路径：" + Directory.GetCurrentDirectory() + "\\" + fileName);
//                 //toolStripStatusLabel3.Text = "OK";
//             }
//             catch (Exception ex)
//             {
//                 //MessageBox.Show("导出失败！" + ex.Message);
//                 Console.WriteLine("导出失败！" + ex);
//             }
//         }
//
//         private void ExportRawData()
//         {
//             //将PressDict 和HalfPressDict转换为json格式
//             var timestr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
//             var fileName = timestr + "RawData.json";
//
//             var pressDictJson = JsonConvert.SerializeObject(_pressDict);
//             var halfPressDictJson = JsonConvert.SerializeObject(_halfPressDict);
//             var pressTargetJson = JsonConvert.SerializeObject(_pressTarget);
//             var halfPressTargetJson = JsonConvert.SerializeObject(_halfPressTarget);
//             var tempDictJson = JsonConvert.SerializeObject(_tempDict);
//
//             // 如果你需要将所有字典放入一个JSON对象中
//             var allDicts = new
//             {
//                 PressDict = _pressDict,
//                 HalfPressDict = _halfPressDict,
//                 PressTarget = _pressTarget,
//                 HalfPressTarget = _halfPressTarget,
//                 TempDict = _tempDict
//             };
//             var allDictsJson = JsonConvert.SerializeObject(allDicts);
//             Console.WriteLine(allDictsJson);
//         }
//
//         private void buttonExportRawData_Click(object sender, EventArgs e)
//         {
//             ExportRawData();
//         }
//
//         private void buttonImportRawData_Click(object sender, EventArgs e)
//         {
//         }
//
//         private void P1FactorTrans()
//         {
//             double s0 = (_nvmReg[0x62] << 8) + _nvmReg[0x63];
//             //Console.WriteLine((nvmReg[0x62] << 8)+ (nvmReg[0x63]));
//             s0 = s0 / (1 << 13);
//             double tsc1 = (_nvmReg[0x64] << 8) + _nvmReg[0x65];
//             if ((Convert.ToInt32(tsc1) & 0x8000) != 0) tsc1 = tsc1 - 0x10000;
//             tsc1 = tsc1 / (1 << 22);
//             double tsc2 = (_nvmReg[0x66] << 8) + _nvmReg[0x67];
//             if ((Convert.ToInt32(tsc2) & 0x8000) != 0) tsc2 = tsc2 - 0x10000;
//             tsc2 = tsc2 / (1 << 30);
//             double tsc3 = (_nvmReg[0x68] << 8) + _nvmReg[0x69];
//             double tco1 = (_nvmReg[0x6A] << 8) + _nvmReg[0x6B];
//             Console.WriteLine(tco1);
//             if ((Convert.ToInt32(tco1) & 0x8000) != 0) tco1 = tco1 - 0x10000;
//             tco1 = tco1 / (1 << 22);
//             double tco2 = (_nvmReg[0x6C] << 8) + _nvmReg[0x6D];
//             if ((Convert.ToInt32(tco2) & 0x8000) != 0) tco2 = tco2 - 0x10000;
//             tco2 = tco2 / (1 << 30);
//             double tco3 = (_nvmReg[0x6E] << 8) + _nvmReg[0x6F];
//             double f0 = (_nvmReg[0x70] << 8) + _nvmReg[0x71];
//             Console.WriteLine(f0);
//             if ((Convert.ToInt32(f0) & 0x8000) != 0) f0 = f0 - 0x10000;
//             f0 = f0 / (1 << 15);
//             double k2 = (_nvmReg[0x72] << 8) + _nvmReg[0x73];
//             if ((Convert.ToInt32(k2) & 0x8000) != 0) k2 = k2 - 0x10000;
//             k2 = k2 / (1 << 16);
//             double k3 = (((_nvmReg[0x74] << 2) + _nvmReg[0x75]) & 0xFFC0) >> 6;
//             if ((Convert.ToInt32(k3) & 0x200) != 0) k3 = k3 - 0x400;
//             k3 = k3 / (1 << 17);
//
//             double baseT = ((_nvmReg[0x92] << 8) + _nvmReg[0x93]) & 0xFFF;
//             if ((Convert.ToInt32(baseT) & 0x800) != 0) baseT = baseT - 0x1000;
//             baseT = baseT / 8;
//             _spsData.P1Factor = new Dictionary<string, double>
//             {
//                 { "s0", s0 },
//                 { "tsc1", tsc1 },
//                 { "tsc2", tsc2 },
//                 { "tsc3", tsc3 },
//                 { "tco1", tco1 },
//                 { "tco2", tco2 },
//                 { "tco3", tco3 },
//                 { "f0", f0 },
//                 { "k2", k2 },
//                 { "k3", k3 },
//                 { "baseT", baseT }
//             };
//         }
//
//         private void P2FactorTrans()
//         {
//             double s0 = (_nvmReg[0x79] << 8) + _nvmReg[0x7A];
//             s0 = s0 / (1 << 13);
//             double tsc1 = (_nvmReg[0x7B] << 8) + _nvmReg[0x7C];
//             if ((Convert.ToInt32(tsc1) & 0x8000) != 0) tsc1 = tsc1 - 0x10000;
//             tsc1 = tsc1 / (1 << 22);
//             double tsc2 = (_nvmReg[0x7D] << 8) + _nvmReg[0x7E];
//             if ((Convert.ToInt32(tsc2) & 0x8000) != 0) tsc2 = tsc2 - 0x10000;
//             tsc2 = tsc2 / (1 << 30);
//             double tsc3 = (_nvmReg[0x7F] << 8) + _nvmReg[0x80];
//             double tco1 = (_nvmReg[0x81] << 8) + _nvmReg[0x82];
//             if ((Convert.ToInt32(tco1) & 0x8000) != 0) tco1 = tco1 - 0x10000;
//             tco1 = tco1 / (1 << 22);
//             double tco2 = (_nvmReg[0x83] << 8) + _nvmReg[0x84];
//             if ((Convert.ToInt32(tco2) & 0x8000) != 0) tco2 = tco2 - 0x10000;
//             tco2 = tco2 / (1 << 30);
//             double tco3 = (_nvmReg[0x85] << 8) + _nvmReg[0x86];
//             double f0 = (_nvmReg[0x87] << 8) + _nvmReg[0x88];
//             if ((Convert.ToInt32(f0) & 0x8000) != 0) f0 = f0 - 0x10000;
//             Console.WriteLine("f0");
//             Console.WriteLine(f0);
//             f0 = f0 / (1 << 15);
//             double k2 = (_nvmReg[0x89] << 8) + _nvmReg[0x8A];
//             if ((Convert.ToInt32(k2) & 0x8000) != 0) k2 = k2 - 0x10000;
//             k2 = k2 / (1 << 16);
//             double k3 = (((_nvmReg[0x8b] << 2) + _nvmReg[0x8C]) & 0xFFC0) >> 6;
//             if ((Convert.ToInt32(k3) & 0x200) != 0) k3 = k3 - 0x400;
//             k3 = k3 / (1 << 17);
//             double baseT = ((_nvmReg[0x92] << 8) + _nvmReg[0x93]) & 0xFFF;
//             if ((Convert.ToInt32(baseT) & 0x800) != 0) baseT = baseT - 0x1000;
//             baseT = baseT / 8;
//             _spsData.P2Factor = new Dictionary<string, double>
//             {
//                 { "s0", s0 },
//                 { "tsc1", tsc1 },
//                 { "tsc2", tsc2 },
//                 { "tsc3", tsc3 },
//                 { "tco1", tco1 },
//                 { "tco2", tco2 },
//                 { "tco3", tco3 },
//                 { "f0", f0 },
//                 { "k2", k2 },
//                 { "k3", k3 },
//                 { "baseT", baseT }
//             };
//         }
//
//         private void TSIFactorTrans()
//         {
//             double k = _nvmReg[0x8e];
//             if ((Convert.ToInt32(k) & 0x80) != 0) k = k - 0x100;
//             k = k + 1808;
//             double m = _nvmReg[0x8f];
//             if ((Convert.ToInt32(m) & 0x80) != 0) m = m - 0x100;
//             m = m / (1 << 8) + 2.63;
//
//             double Toff = _nvmReg[0x90];
//             if ((Convert.ToInt32(Toff) & 0x80) != 0) Toff = Toff - 0x100;
//             Toff = Toff + 279;
//
//             _spsData.TSIFactor = new Dictionary<string, double>
//             {
//                 { "k", k },
//                 { "m", m },
//                 { "Toff", Toff }
//             };
//         }
//
//         private void TSEFactorTrans()
//         {
//             double mt = (_nvmReg[0x91] << 4) + ((_nvmReg[0x92] & 0xF0) >> 4);
//             Console.WriteLine("mt");
//             Console.WriteLine(mt);
//             if ((Convert.ToInt32(mt) & 0x800) != 0) mt = mt - 0x1000;
//             mt = mt / (1 << 11);
//
//             double t0 = ((_nvmReg[0x92] & 0x0F) << 8) + _nvmReg[0x93];
//             if ((Convert.ToInt32(t0) & 0x800) != 0) t0 = t0 - 0x1000;
//             t0 = t0 / (1 << 3);
//             double kt = (_nvmReg[0x94] << 2) + ((_nvmReg[0x95] & 0xC0) >> 6);
//             Console.WriteLine("kt");
//             Console.WriteLine(kt);
//             if ((Convert.ToInt32(kt) & 0x200) != 0) kt = kt - 0x400;
//             kt = 1 / (kt + 667);
//             double kts = ((_nvmReg[0x95] & 0x03) << 8) + _nvmReg[0x96];
//             if ((Convert.ToInt32(kts) & 0x200) != 0) kts = kts - 0x400;
//             Console.WriteLine(kts);
//             kts = kts / (1 << 11);
//             _spsData.TSEFactor = new Dictionary<string, double>
//             {
//                 { "mt", mt },
//                 { "t0", t0 },
//                 { "kt", kt },
//                 { "kts", kts }
//             };
//         }
//
//         private void P1compute()
//         {
//             subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//             double P1raw = subCtrlForm.Spscom.GetRawdata("P1", 0, 1);
//             if ((Convert.ToInt32(P1raw) & 0x8000) != 0) P1raw = P1raw - 0x10000;
//             Console.WriteLine(P1raw);
//             //double P2raw = subCtrlForm.Spscom.getRawdata("P2", 0, 1);
//             //double P2O= subCtrlForm.Spscom.getRawdata("P2O", 0, 1);
//             //subCtrlForm.Spscom.SetComMode("STRT_CM", "01", true);
//             double P1O = subCtrlForm.Spscom.GetRawdata("P1O", 0, 1);
//             double T = subCtrlForm.Spscom.GetRawdata("TSIO", 0, 1) / 64;
//             //Console.WriteLine(P1raw);
//             T = T - _spsData.P1Factor["baseT"];
//             //Console.WriteLine(T);
//             var Voff = _spsData.P1Factor["f0"] + (_spsData.P1Factor["tco1"] * T + _spsData.P1Factor["tco2"] * T * T +
//                                                   _spsData.P1Factor["tco3"] * T * T * T);
//             //Console.WriteLine(Voff);
//             var Sen = _spsData.P1Factor["s0"] * (1 + _spsData.P1Factor["tsc1"] * T + _spsData.P1Factor["tsc2"] * T * T +
//                                                  _spsData.P1Factor["tsc3"] * T * T * T);
//             //Console.WriteLine(Sen);
//             var Btemp = Sen * (P1raw / (1 << 15) - Voff);
//
//             var Bconv = Btemp + (_spsData.P1Factor["k2"] * Btemp * Btemp +
//                                  _spsData.P1Factor["k3"] * Btemp * Btemp * Btemp);
//
//             _spsData.P1Raw = P1raw;
//             _spsData.P1O = P1O;
//             _spsData.P1Comp = Bconv * (1 << 15);
//         }
//
//         private void P2compute()
//         {
//             subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//             //double P1raw = subCtrlForm.Spscom.getRawdata("P1", 0, 1);
//             double P2raw = subCtrlForm.Spscom.GetRawdata("P2", 0, 1);
//             if ((Convert.ToInt32(P2raw) & 0x8000) != 0) P2raw = P2raw - 0x10000;
//             double P2O = subCtrlForm.Spscom.GetRawdata("P2O", 0, 1);
//             //subCtrlForm.Spscom.SetComMode("STRT_CM", "01", true);
//             //double P1O = subCtrlForm.Spscom.getRawdata("P1O", 0, 1);
//             double T = subCtrlForm.Spscom.GetRawdata("TSIO", 0, 1) / 64;
//             //Console.WriteLine(P1raw);
//             T = T - _spsData.P2Factor["baseT"];
//             Console.WriteLine(T);
//             var Voff = _spsData.P2Factor["f0"] + _spsData.P2Factor["tco1"] * T + _spsData.P2Factor["tco2"] * T * T +
//                        _spsData.P2Factor["tco3"] * T * T * T;
//             Console.WriteLine(Voff);
//             var Sen = _spsData.P2Factor["s0"] * (1 + _spsData.P2Factor["tsc1"] * T + _spsData.P2Factor["tsc2"] * T * T +
//                                                  _spsData.P2Factor["tsc3"] * T * T * T);
//             Console.WriteLine(Sen);
//             var Btemp = Sen * (P2raw / (1 << 15) - Voff);
//             //Console.WriteLine(P1raw/(1 <<15));
//             //Console.WriteLine(Btemp);
//             var Bconv = Btemp + _spsData.P2Factor["k2"] * Btemp * Btemp +
//                         _spsData.P2Factor["k3"] * Btemp * Btemp * Btemp;
//             //Console.WriteLine(Bconv);
//             //Console.WriteLine(Bconv*(1<<15));
//             _spsData.P2Raw = P2raw;
//             _spsData.P2O = P2O;
//             _spsData.P2Comp = Bconv * (1 << 15);
//             //spsData.P1Comp = Bconv * (1 << 15);
//             //spsData.P2Raw = P2raw;
//             //spsData.P2O = P2O;
//             //spsData.P2Comp = 
//         }
//
//         private void TSIcompute()
//         {
//             subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//             double TSIraw = subCtrlForm.Spscom.GetRawdata("TSI", 0, 1);
//             if ((Convert.ToInt32(TSIraw) & 0x8000) != 0) TSIraw = TSIraw - 0x10000;
//             _spsData.TSI = TSIraw;
//             TSIraw = TSIraw / (1 << 15);
//
//             double T = subCtrlForm.Spscom.GetRawdata("TSIO", 0, 1) / 64;
//             // TsiComp = kt*TSIraw/(1+mt*TSIraw)-toff
//             var TSIComp = _spsData.TSIFactor["k"] * TSIraw / (1 + _spsData.TSIFactor["m"] * TSIraw) -
//                           _spsData.TSIFactor["Toff"];
//             _spsData.TSIO = T;
//             _spsData.TSIComp = TSIComp;
//         }
//
//         private void TSEcompute()
//         {
//             subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//             double TSEraw = subCtrlForm.Spscom.GetRawdata("TSE", 0, 1);
//             if ((Convert.ToInt32(TSEraw) & 0x8000) != 0) TSEraw = TSEraw - 0x10000;
//             _spsData.TSE = TSEraw;
//             TSEraw = TSEraw / (1 << 15);
//             double T = subCtrlForm.Spscom.GetRawdata("TSEO", 0, 1) / 64;
//             // TCSIComp = (TSEraw-mt)*(1+kts*(TSEraw-mt)/kt-t0
//             var TSEComp =
//                 (TSEraw - _spsData.TSEFactor["mt"]) *
//                 (1 + _spsData.TSEFactor["kts"] * (TSEraw - _spsData.TSEFactor["mt"])) / _spsData.TSEFactor["kt"] +
//                 _spsData.TSEFactor["t0"];
//             _spsData.TSEO = T;
//             _spsData.TSEComp = TSEComp;
//         }
//
//         private void buttonGetAllData_Click(object sender, EventArgs e)
//         {
//             //选项卡四
//             //根据当前所有的outmem 获取所有的数据并添加在listbox 中
//             // subCtrlForm.Spscom.RawAddr.Keys.ToArray()
//             var items = subCtrlForm.Spscom.RawAddr.Keys.ToArray();
//             subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
//             listBoxDataList.Items.Clear();
//             //int value = 
//             foreach (var t in items)
//             {
//                 int value;
//                 if (t.Contains("OFF") || t.Contains("FG"))
//                     value = subCtrlForm.Spscom.GetMidData(t, 0, 1);
//                 else
//                     value = subCtrlForm.Spscom.GetRawdata(t, 0, 1);
//
//                 //textBoxValue.Text = value.ToString();
//                 // 将项目和数据添加到listbox 中
//                 listBoxDataList.Items.Add(t + ":\t" + value);
//             }
//         }
//
//         private void button1_Click_1(object sender, EventArgs e)
//         {
//             if (textBoxDACTest.Text == "") return;
//
//             try
//             {
//                 subCtrlForm.Spscom.SetComMode("STRT_CM", "01", true);
//                 var str = textBoxDACTest.Text.Split('\n');
//                 foreach (var t in str)
//                 {
//                     Debug.WriteLine(t);
//                     if (t == "") continue;
//
//                     if (t.Substring(0, 1) == "#" || t.Substring(0, 2) == "//") continue;
//
//                     if (t.Contains("delay"))
//                     {
//                         var str1 = t.Split('=');
//                         Thread.Sleep(int.Parse(str1[1]));
//                     }
//                     else if (t.Contains("#") && t.Substring(0, 1) != "#")
//                     {
//                         var str1 = t.Split('#');
//                         subCtrlForm.Spscom.RunScript(str1[0]);
//                         //System.Diagnostics.Debug.WriteLine(str1[1]);
//                     }
//                     else
//                     {
//                         subCtrlForm.Spscom.RunScript(t);
//                         //System.Diagnostics.Debug.WriteLine(str[i]);
//                     }
//                 }
//
//                 //MessageBox.Show("脚本运行完成");
//             }
//             catch
//             {
//                 MessageBox.Show("脚本存在错误");
//             }
//
//             subCtrlForm.Spscom.Pd2Nm();
//             buttonProductConnState.Text = "";
//         }
//
//         private void textBoxScripts_TextChanged(object sender, EventArgs e)
//         {
//         }
//         // 数据同步功能，将服务器上mysql 数据库与本地sqlite 数据库同步
//         private void DataSync()
//         {
//             // 确保MySQL连接是有效的
//             if (!_mySqlLocal.IsConnected) return;
//             // if (_mySqlLocal.)
//
//             try
//             {
//                 _mySqlLocal.Select("select 1");
//             }
//             catch
//             {
//                 _mySqlLocal.IsConnected = false;
//                 return;
//             }
//
//             // 从SQLite数据库中读取数据
//             var allDataFromSqlite = _sqlLocal.Select("select * from spsdata");
//             foreach (var item in allDataFromSqlite)
//             {
//                 var mysqlDataExists = _mySqlLocal.Select("select count(*) from spsdata where Id = '" + item[0] + "'");
//                 if (mysqlDataExists != null && mysqlDataExists[0][0] != "0") continue;
//                 var sql =
//                     "INSERT INTO spsdata (Id, Data, P1raw, P2raw, TSI, TSE, Press, Temp, Tar1, Tar2, TSEMode, Coefficient1, Coefficient2, CreatTime, UpdateTime) VALUES ('@Id', '@Data', '@P1raw', '@P2raw', '@TSI', '@TSE', '@Press', '@Temp', '@Tar1', '@Tar2', '@TSEMode', '@Coefficient1', '@Coefficient2', '@CreatTime', '@UpdateTime')";
//                 sql = sql.Replace("@Id", item[0]);
//                 sql = sql.Replace("@Data", item[1].ToString());
//                 sql = sql.Replace("@P1raw", item[2].ToString());
//                 sql = sql.Replace("@P2raw", item[3].ToString());
//                 sql = sql.Replace("@TSI", item[4].ToString());
//                 sql = sql.Replace("@TSE", item[5].ToString());
//                 sql = sql.Replace("@Press", item[6].ToString());
//                 sql = sql.Replace("@Temp", item[7].ToString());
//                 sql = sql.Replace("@Tar1", item[8].ToString());
//                 sql = sql.Replace("@Tar2", item[9].ToString());
//                 sql = sql.Replace("@TSEMode", item[10].ToString());
//                 sql = sql.Replace("@Coefficient1", item[11].ToString());
//                 sql = sql.Replace("@Coefficient2", item[12].ToString());
//                 sql = sql.Replace("@CreatTime", item[13].ToString());
//                 sql = sql.Replace("@UpdateTime", item[14].ToString());
//
//                 _mySqlLocal.Insert(sql);
//
//             }
//
//             // 从MySQL数据库中读取数据
//             var allDataFromMySql = _mySqlLocal.Select("SELECT * FROM spsdata");
//             foreach (var item in allDataFromMySql)
//             {
//              
//                 var id = item[0].ToString();
//                 var result1 = _sqlLocal.Select("SELECT count(*) FROM spsdata where Id = '" + id + "'");
//                 if (result1 != null && result1[0][0] != "0") continue;
//                 var sql = "INSERT INTO spsdata (Id, Data, P1raw, P2raw, TSI, TSE, Press, Temp, Tar1, Tar2, TSEMode, Coefficient1, Coefficient2, CreatTime, UpdateTime) VALUES ('@Id', '@Data', '@P1raw', '@P2raw', '@TSI', '@TSE', '@Press', '@Temp', '@Tar1', '@Tar2', '@TSEMode', '@Coefficient1', '@Coefficient2', '@CreatTime', '@UpdateTime')";
//                 sql = sql.Replace("@Id", id);
//                 sql = sql.Replace("@Data", item[1].ToString());
//                 sql = sql.Replace("@P1raw", item[2].ToString());
//                 sql = sql.Replace("@P2raw", item[3].ToString());
//                 sql = sql.Replace("@TSI", item[4].ToString());
//                 sql = sql.Replace("@TSE", item[5].ToString());
//                 sql = sql.Replace("@Press", item[6].ToString());
//                 sql = sql.Replace("@Temp", item[7].ToString());
//                 sql = sql.Replace("@Tar1", item[8].ToString());
//                 sql = sql.Replace("@Tar2", item[9].ToString());
//                 sql = sql.Replace("@TSEMode", item[10].ToString());
//                 sql = sql.Replace("@Coefficient1", item[11].ToString());
//                 sql = sql.Replace("@Coefficient2", item[12].ToString());
//                 sql = sql.Replace("@CreatTime", item[13].ToString());
//                 sql = sql.Replace("@UpdateTime", item[14].ToString());
//                 _sqlLocal.Insert(sql);
//             }
//             
//             
//     
//         }
//
//         private void button7_Click(object sender, EventArgs e)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         private void comboBoxModeAuto_SelectedIndexChanged(object sender, EventArgs e)
//         {
//             var str = comboBoxModeAuto.Text;
//             var tempCount = int.Parse(str.Substring(0, 1));
//             var pressCount = int.Parse(str.Substring(2, 1));
//             //string[] str2 = str1[1].Split('P');
//             for (var i = 0; i < 10; i++)
//                 switch (tempCount)
//                 {
//                     case 1 when i > 3:
//                     case 2 when i > 5:
//                     case 3 when i > 7:
//                         _buttons[i].Hide();
//                         break;
//                     default:
//                         {
//                             switch (pressCount)
//                             {
//                                 case 2 when i > 1 && i < 4:
//                                 case 3 when i > 2 && i < 4:
//                                 case 4 when i > 3 && i < 4:
//                                     _buttons[i].Hide();
//                                     break;
//                                 default:
//                                     _buttons[i].Visible = true;
//                                     break;
//                             }
//
//                             break;
//                         }
//                 }
//
//             for (var i = 0; i < 4; i++)
//             {
//                 switch (tempCount)
//                 {
//                     case 1 when i > 0:
//                     case 2 when i > 1:
//                     case 3 when i > 2:
//                         _textBoxesTTarget[i].Hide();
//                         break;
//                     default:
//                         _textBoxesTTarget[i].Visible = true;
//                         Console.WriteLine(_textBoxesTTarget[i].Name);
//                         break;
//                 }
//
//                 switch (pressCount)
//                 {
//                     case 1 when i > 0:
//                     case 2 when i > 1:
//                     case 3 when i > 2:
//                         _textBoxesPTarget[i].Hide();
//                         break;
//                     default:
//                         _textBoxesPTarget[i].Visible = true;
//                         break;
//                 }
//                         
//             }
//                    
//         }
//
//         private void comboBoxDut_SelectedIndexChanged(object sender, EventArgs e)
//         {
//             throw new System.NotImplementedException();
//         }
//     }
//
//     public class SpsData
//     {
//         public string ID { get; set; }
//         public Dictionary<string, double> P1Factor { get; set; }
//         public Dictionary<string, double> P2Factor { get; set; }
//         public Dictionary<string, double> TSIFactor { get; set; }
//         public Dictionary<string, double> TSEFactor { get; set; }
//         public string[] RegData { get; set; }
//         public string[] NvmData { get; set; }
//         public double P1Raw { get; set; }
//         public double P2Raw { get; set; }
//         public double P1O { get; set; }
//         public double P2O { get; set; }
//         public double P1Comp { get; set; }
//         public double P2Comp { get; set; }
//         public double TSI { get; set; }
//         public double TSE { get; set; }
//         public double TSIO { get; set; }
//         public double TSEO { get; set; }
//         public double TSIComp { get; set; }
//         public double TSEComp { get; set; }
//     }
//
//     public class SpsCalibration
//     {
//         public string ID { get; set; }
//         
//         public string CalibraMode { get; set; }
//         
//         public bool Status = true;
//         
//         public Dictionary<string, double> FullBridgeTarget = new Dictionary<string, double>{{"T1",0},{"T2",0},{"T3",0},{"T4",0}};
//         public Dictionary<string, double> HalfBridgeTarget = new Dictionary<string, double>{{"T1",0},{"T2",0},{"T3",0},{"T4",0}};
//
//         public Dictionary<string, double> FullBridgeRawData = new Dictionary<string, double>{{"T0P1",0},{"T0P2",0},{"T0P3",0},{"T0P4",0},{"T1P1",0},{"T1P2",0},{"T2P1",0},{"T2P2",0},{"T3P1",0},{"T3P2",0}};
//         public Dictionary<string, double> HalfBridgeRawData = new Dictionary<string, double>{{"T0P1",0},{"T0P2",0},{"T0P3",0},{"T0P4",0},{"T1P1",0},{"T1P2",0},{"T2P1",0},{"T2P2",0},{"T3P1",0},{"T3P2",0}};
//         
//         public Dictionary<string, double> TsiTempTarget = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
//         public Dictionary<string, double> TsiTempRaw = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
//         
//         public Dictionary<string, double> TseTempTarget = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
//         public Dictionary<string, double> TseTempRaw = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
//         
//         public Dictionary<string, double> FullBridgeFactor = new Dictionary<string, double>{{"s0",0},{"tsc1",0},{"tsc2",0},{"tsc3",0},{"tco1",0},{"tco2",0},{"tco3",0},{"f0",0},{"k2",0},{"k3",0},{"baseT",0}};
//         public Dictionary<string, double> HalfBridgeFactor = new Dictionary<string, double>{{"s0",0},{"tsc1",0},{"tsc2",0},{"tsc3",0},{"tco1",0},{"tco2",0},{"tco3",0},{"f0",0},{"k2",0},{"k3",0},{"baseT",0}};
//
//         public Dictionary<string, double> TsiFactor = new Dictionary<string, double>
//             { { "k", 0 }, { "m", 0 }, { "Toff", 0 } };
//         public Dictionary<string, double> TseFactor = new Dictionary<string, double>
//             { { "k", 0 }, { "m", 0 }, { "Toff", 0 } };
//         
//         public string[] NvmData { get; set; }
//     }
//
//     public class UiConfig
//     {
//         //界面配置文件
//         public string evbPort { get; set; }
//         public string commMode { get; set; }
//     }
// }