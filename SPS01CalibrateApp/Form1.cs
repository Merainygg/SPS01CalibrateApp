using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace SPS01CalibrateApp
{
   
    public partial class Form1 : Form
    {
        //SerialPort serialPort = new SerialPort();
        SPScom spscom = new SPScom();
        SubCtrlForm subCtrlForm = new SubCtrlForm();
        
        private byte[] nvm = new byte[256];
        private byte[] nvmReg = new byte[256];

        private TextBox[] textBoxes = new TextBox[10];
        private TextBox[] tempTextBoxex = new TextBox[4];
        private TextBox[] targetTextBoxex = new TextBox[4];
        private int foucsIndex = 0;
        private int foucsTempIndex = 0;
        private string path_scrippt = "";
        private Dictionary<string, int> PressDict = new Dictionary<string, int>();
        private Dictionary<string, int> HalfPressDict = new Dictionary<string, int>();
        private Dictionary<string, int> PressTarget = new Dictionary<string, int>();
        private Dictionary<string, int> HalfPressTarget = new Dictionary<string, int>();
        private Dictionary<string, int> TempDict = new Dictionary<string, int>();
        private SpsData spsData = new SpsData();

        public Form1()
        {
            InitializeComponent();
            //数据初始化
            //PressDict.Add("P1", 0);


            this.Text = "SPS01 Calibrate App "+Application.ProductVersion;
            // 子窗体


            childForm(subCtrlForm, panel1);

            // 总选项卡设置
            // 修改标签页的标题
            tabPage1.Text = "设置";
            tabPage2.Text = "手动操作";
            tabPage3.Text = "寄存器编辑";
            tabPage4.Text = "数据读取";

            //选项卡1 设置
            label1.Text = "通信协议选择";
            comboBox1.Items.AddRange(new string[] { "IIC", "OWI" });
            label2.Text = "设备地址";
            buttonConnProduct.Text = "产品连接";
            buttonProductConnState.Text = "";
            textBox1.Text = "D8";

            //选项卡2 设置

            labeltype.Text = "选择类型";
            comboBoxtype.Items.AddRange(new string[] { "FullBridge", "HalfBridge" });
            labelmode.Text = "选择模式";
            comboBoxMode.Items.AddRange(new string[] {"1T2P","1T3P","1T4P", "2T2P", "2T3P", "2T4P", "3T2P", "3T3P", "3T4P", "4T2P", "4T3P", "4T4P" });
            labelJump.Text = "跳过点数";
            comboBoxJump.Items.AddRange(new string[] { "0", "1", "2"});     
            comboBoxJump.Text = "0";
            labelAvg.Text = "平均点数";
            comboBoxAvg.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            comboBoxAvg.Text = "1";
            //comboBoxtype.Text = "FullBridge";

            buttonExportRawData.Text = "导出原始数据";
            buttonImportRawData.Text = "导入原始数据";

            labelP1.Text = "P1";
            labelP2.Text = "P2";

            labelP3.Text = "P3";
            labelP4.Text = "P4";

            labelT0.Text = "T0";
            labelT1.Text = "T1";
            labelT2.Text = "T2";
            labelT3.Text = "T3";

            labelTar.Text = "目标值";
            buttonGetRawData.Text = "获取原始数据";
            textBoxes[0] = textBoxT0P1;
            textBoxes[1] = textBoxT0P2;
            textBoxes[2] = textBoxT0P3;
            textBoxes[3] = textBoxT0P4;

            textBoxes[4] = textBoxT1P1;
            textBoxes[5] = textBoxT1P2;
            textBoxes[6] = textBoxT2P1;
            textBoxes[7] = textBoxT2P2;
            textBoxes[8] = textBoxT3P1;
            textBoxes[9] = textBoxT3P2;

            for (int i = 0; i < 10; i++)
            {
                PressDict.Add(textBoxes[i].Name, 0);
                HalfPressDict.Add(textBoxes[i].Name, 0);
            }

            targetTextBoxex[0] = textBoxO1;
            targetTextBoxex[1] = textBoxO2;
            targetTextBoxex[2] = textBoxO3;
            targetTextBoxex[3] = textBoxO4;

            for (int i = 0; i < 4; i++)
            {
                PressTarget.Add(targetTextBoxex[i].Name, 0);
                HalfPressTarget.Add(targetTextBoxex[i].Name, 0);
            }

            tempTextBoxex[0] = textBoxT0;
            tempTextBoxex[1] = textBoxT1;
            tempTextBoxex[2] = textBoxT2;
            tempTextBoxex[3] = textBoxT3;

            for (int i = 0; i < 4; i++)
            {
                TempDict.Add(tempTextBoxex[i].Name, 0);
                tempTextBoxex[i].ReadOnly = true;
            }

            labelLoadFile.Text = "点我选择脚本文件：";
            buttonRunScript.Text = "运行脚本";


            //选项卡3 寄存器操作
            buttonup.Text = "上一页";
            buttondown.Text = "下一页";
            textBox2.Text = "1";
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.Enabled = false;
            comboBoxMemMode.Items.AddRange(new string[] {"NVM","NVMReg"});
            buttonExportRegData.Text = "导出配置数据";
            //comboBox2.Text = "NVM";
            buttonreg.Text = "加载临时寄存器";
            buttonnvm.Text = "加载永久寄存器";

            // 设置dataGridView1 行数和行标签
            dataGridView1.RowCount = 8;
            dataGridView1.RowHeadersVisible = true;

            // 设置行头宽度
            dataGridView1.RowHeadersWidth = 15; // 根据需要调整
            dataGridView1.RowHeadersVisible = false; // 显示行头
                                                    // 添加自定义列

            DataGridViewColumn rowNumberColumn = new DataGridViewTextBoxColumn();
            rowNumberColumn.HeaderText = "行号";
            rowNumberColumn.Name = "RowNumber";
            rowNumberColumn.ReadOnly = true; // 设置为只读
            rowNumberColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // 右对齐

            // 将新列添加到列集合中
            dataGridView1.Columns.Insert(0, rowNumberColumn);

            // 设置列的宽度
            rowNumberColumn.Width = 80; // 根据需要调整
                

            dataGridView1.Size = new Size(80*9, 23*8+21);
            dataGridView1.ScrollBars = ScrollBars.None;

            //选项卡4 设置

            labelOutMemAddr.Text = "选择待读取内容";
            comboBoxOutMemAddr.Items.AddRange(subCtrlForm.Spscom.RawAddr.Keys.ToArray());
            buttonGetValue.Text = "获取数据";
            buttonLoop.Text = "循环测试";
            labelDataList.Text = "数据清单";
            buttonGetAllData.Text = "获取所有输出数据";


            // 底部状态栏
            toolStripStatusLabel1.Text = "";

            //SpsData spsData = new SpsData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void childForm(Form chidForm, Panel panel)
        {
            chidForm.TopLevel = false;
            chidForm.FormBorderStyle = FormBorderStyle.None;//让窗体无边界
            chidForm.Dock = DockStyle.Fill;

            panel.Controls.Add(chidForm);
            panel.Tag = chidForm;
            chidForm.BringToFront();
            chidForm.Show();//显示子窗体

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (subCtrlForm.PortName == "")
            {
                MessageBox.Show("请先选择串口");
                buttonProductConnState.Text = "";
                buttonProductConnState.BackColor = Color.White;
                return;
            }
            if (subCtrlForm.button2.Text != "已连接")
            {
                buttonProductConnState.Text = "";
                buttonProductConnState.BackColor = Color.White;

                return;
            }
            if (buttonProductConnState.Text == "已连接")
            {
                subCtrlForm.Spscom.Pd2Nm();
                buttonProductConnState.Text = "";
                buttonProductConnState.BackColor = Color.White;
                Console.WriteLine("关闭");
                return;
            }

            subCtrlForm.Spscom.DeviceAddr = textBox1.Text;

            if (subCtrlForm.Spscom.ConnPd())
            {
                    buttonProductConnState.Text = "已连接";
                    buttonProductConnState.BackColor = Color.Green;
            }
            else
            {
                buttonProductConnState.Text = "连接失败";
                buttonProductConnState.BackColor = Color.Red;
            }

            

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 选项卡3 下一页
            textBox2.Text = (int.Parse(textBox2.Text) + 1).ToString();
            dataGridView1_CellValueAdd();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 选项卡3 上一页
            textBox2.Text = (int.Parse(textBox2.Text) - 1).ToString();
            dataGridView1_CellValueAdd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 选项卡3 加载临时寄存器
            System.Diagnostics.Debug.WriteLine("加载临时寄存器");
            string reg = subCtrlForm.Spscom.GetAllReg();
            //subCtrlForm.Spscom.GetAllReg();
            for (int i = 0; i < 256; i++)
            {
                nvmReg[i] = Convert.ToByte(reg.Substring(i * 2, 2), 16);
                //reg.Substring(i * 2, 2);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            // 选项卡3 加载永久寄存器
            string result = subCtrlForm.Spscom.GetAllNvm();
            for (int i = 0; i < 256; i++)
            {
                nvm[i] = Convert.ToByte(result.Substring(i * 2, 2), 16);
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选项卡3 数据改变
            dataGridView1_CellValueAdd();
        }

        private void dataGridView1_CellValueAdd()
        {
            // 选项卡3 数据改变
            if (comboBoxMemMode.Text == "NVM")
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        dataGridView1.Rows[i].Cells["Column" + j.ToString()].Value = nvm[(int.Parse(textBox2.Text) - 1) * 64 + i * 8 + j - 1].ToString("X2");

                    }
                }
                //nvm[(int.Parse(textBox2.Text) - 1) * 64 + e.RowIndex * 8 + e.ColumnIndex] = Convert.ToByte(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
            else if (comboBoxMemMode.Text == "NVMReg")
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        dataGridView1.Rows[i].Cells["Column" + j.ToString()].Value = nvmReg[(int.Parse(textBox2.Text) - 1) * 64 + i * 8 + j-1].ToString("X2");
                    }
                }
                //nvmReg[(int.Parse(textBox2.Text) - 1) * 64 + e.RowIndex * 8 + e.ColumnIndex] = Convert.ToByte(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void buttonGetRawData_Click(object sender, EventArgs e)
        {
            // 选项卡2 获取原始数据

            // 读取P1 通道压力数据
            int value = 0;
            string raw_name = "";
            subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
            if (comboBoxtype.Text == "FullBridge")
            {
                raw_name = "P1";
            }
            else if (comboBoxtype.Text == "HalfBridge")
            {
                raw_name = "P2";
            }
            value = subCtrlForm.Spscom.getRawdata(raw_name,int.Parse(comboBoxJump.Text),int.Parse(comboBoxAvg.Text));

            if (value > 0x7FFF)
            {
                value = value - 0x10000;
            }
            //赋值
            textBoxes[foucsIndex].Text = value.ToString();
            //存入字典中
            if (comboBoxtype.Text == "FullBridge")
            {
                PressDict[textBoxes[foucsIndex].Name] = value;
            }
            else if (comboBoxtype.Text == "HalfBridge")
            {
                HalfPressDict[textBoxes[foucsIndex].Name] = value;
            }
            //PressDict.Add(textBoxes[foucsIndex].Name, value);

            //读取温度数值
            int value1 = subCtrlForm.Spscom.getRawdata("TSIO",0,1);
            if (value1 > 0x7FFF) { value1 = value1 - 0x10000; }
            value1 = value1 >> 6;
            tempTextBoxex[foucsTempIndex].Text = value1.ToString();
            //textBoxTSI.Text = value1.ToString();
            TempDict[tempTextBoxex[foucsTempIndex].Name] = value1;

            System.Diagnostics.Debug.WriteLine(textBoxes[foucsIndex].Name,value.ToString(), value1.ToString());

        }

        //加载脚本文件
        private void label3_Click(object sender, EventArgs e)
        {
            // 选项卡2 加载脚本文件

            openFileDialog1.Filter = "文本文件|*.txt";
            openFileDialog1.Title = "选择脚本文件";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            path_scrippt = path;
            if (path == "")
            {
                return;
            }

            StreamReader sr = new StreamReader(path, Encoding.Default);
            string line;
            textBoxScripts.Text = "";
            while ((line = sr.ReadLine()) != null)
            {
                //string[] str = line.Split(' ');
                textBoxScripts.AppendText(line + "\n");
            }

            labelLoadFile.Text = "点我选择脚本文件：" + "\n" + path;

        }

        //脚本执行
        private void buttonRunScript_Click(object sender, EventArgs e)
        {
            // 选项卡2 运行脚本
            StreamReader sr = new StreamReader(path_scrippt, Encoding.Default);
            string line;
            textBoxScripts.Text = "";
            while ((line = sr.ReadLine()) != null)
            {
                //string[] str = line.Split(' ');
                textBoxScripts.AppendText(line + "\n");
            }

            try
            {
                subCtrlForm.Spscom.SetComMode("STRT_CM", "01", true);
                string[] str = textBoxScripts.Text.Split('\n');
                for (int i = 0; i < str.Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine(str[i]);
                    if (str[i] == "")
                    {
                        continue;
                    }
                    else if (str[i].Substring(0, 1) == "#" || str[i].Substring(0, 2) == "//")
                    {
                        continue;
                    }
                    else if (str[i].Contains("delay"))
                    {
                        string[] str1 = str[i].Split('=');
                        System.Threading.Thread.Sleep(int.Parse(str1[1]));
                    }
                    else if (str[i].Contains("#") && str[i].Substring(0, 1) != "#")
                    {
                        string[] str1 = str[i].Split('#');
                        subCtrlForm.Spscom.runScript(str1[0]);
                        //System.Diagnostics.Debug.WriteLine(str1[1]);
                    }
                    else
                    {
                        subCtrlForm.Spscom.runScript(str[i]);
                        //System.Diagnostics.Debug.WriteLine(str[i]);

                    }
                
                }

                MessageBox.Show("脚本运行完成");
            }
            catch
            {
                MessageBox.Show("脚本存在错误");
            }
            finally
            {
                //subCtrlForm.Spscom.SetComMode("STRT_CM", "00", true);
                sr.Close();
            }
        }

        // 标定模式选择
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选项卡2 选择标定模式
            string str = comboBoxMode.Text;
            int Tcount = int.Parse(str.Substring(0, 1));
            int Pcount = int.Parse(str.Substring(2, 1));
            //string[] str2 = str1[1].Split('P');
            for (int i = 0; i < 10; i++)
            {
                //textBoxes[i].Text = "";
                if (Tcount == 1 && i > 3)
                {
                    textBoxes[i].Enabled = false;
                    textBoxes[i].Text = "";

                }
                else if (Tcount == 2 && i > 5)
                {
                    textBoxes[i].Enabled = false;
                    textBoxes[i].Text = "";
                }
                else if (Tcount == 3 && i > 7)
                {
                    textBoxes[i].Enabled = false;
                    textBoxes[i].Text = "";
                }
                else if (Pcount == 2 && (i > 1 && i < 4))
                {
                    textBoxes[i].Enabled = false;
                    textBoxes[i].Text = "";
                }
                else if (Pcount == 3 && (i > 2 && i < 4))
                {
                    textBoxes[i].Enabled = false;
                    textBoxes[i].Text = "";
                }
                else if (Pcount == 4 && (i > 3 && i < 4))
                {
                    textBoxes[i].Enabled = false;
                    textBoxes[i].Text = "";
                }
                else
                {
                    textBoxes[i].Enabled = true;
                }

            }
        }

        // 循环检测
        private void timer1_Tick(object sender, EventArgs e)
        {

            // 选项卡3 翻页检测
            if (textBox2.Text == "1")
            {
                buttonup.Enabled = false;
            }
            else if (textBox2.Text == "4")
            {
                buttondown.Enabled = false;
            }
            else
            {
                buttonup.Enabled = true;
                buttondown.Enabled = true;
            }
            // 根据当前页添加行首
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //转16进制显示
                dataGridView1.Rows[i].Cells["RowNumber"].Value = "0x"+((int.Parse(textBox2.Text) - 1) * 64+i * 8).ToString("X2");
            }

            //根据选中的框来设定哪一个需要填写数据
            for (int i = 0; i < 10; i++)
            {
                if (textBoxes[i].ContainsFocus)
                {
                    
                    foucsIndex = i;
                    foucsTempIndex = int.Parse(textBoxes[i].Name.Substring(8,1));
                    break;
                }
            }

            dataGridView1_CellValueAdd();

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        //获取指定模式下的输出
        private void buttonGetValue_Click(object sender, EventArgs e)
        {
            //选项卡4
            //判定comboBoxOutMemAddr.Text是否为空
            if (comboBoxOutMemAddr.Text == "")
            {
                MessageBox.Show("请选择待读取内容");
                return;
            }   

            subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
            int value;
            if (comboBoxOutMemAddr.Text.Contains("OFF") || comboBoxOutMemAddr.Text.Contains("FG")){
                value = subCtrlForm.Spscom.getMidData(comboBoxOutMemAddr.Text, 0, 1);
            }
            else
            {
                value = subCtrlForm.Spscom.getRawdata(comboBoxOutMemAddr.Text, 0, 1);
            }
            textBoxValue.Text = value.ToString();
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxtype.Text == "FullBridge")
            {
                foreach (var item in textBoxes)
                {
                    if( PressDict[item.Name] != 0)
                    {
                        item.Text = PressDict[item.Name].ToString();
                    }
                    else
                    {
                        item.Text = "";
                    }
                }
            }
            else if (comboBoxtype.Text == "HalfBridge")
            {
                foreach (var item in textBoxes)
                {
                    if (HalfPressDict[item.Name] != 0)
                    {
                        item.Text = HalfPressDict[item.Name].ToString();
                    }
                    else
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void buttonLoop_Click(object sender, EventArgs e)
        {
            // 选项卡四 循环读取
            // 双按钮对话框
            DialogResult dr = MessageBox.Show("是否开始循环读取数据", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }

            try
            {
                string rec = "";
                int value;
                subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
                for (int i = 0; i < 1000; i++)
                {
                    //读取压力数据
                    value = subCtrlForm.Spscom.getRawdata(comboBoxOutMemAddr.Text, 0, 1);
                    rec += value.ToString() + "\n";
                }
                value = subCtrlForm.Spscom.getRawdata(comboBoxOutMemAddr.Text, 0, 1);
                rec += value.ToString() + "\n";
                string timestr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                System.IO.File.WriteAllText(timestr + "data.txt", rec);
                //MessageBox.Show("读取完成");
            }
            //}
            catch
            {
                //MessageBox.Show("读取失败");
            }
        }

        private void buttonExportRegData_Click(object sender, EventArgs e)
        {
            if (!subCtrlForm.Spscom.serialPort.IsOpen)
            {
                MessageBox.Show("串口已关闭");
                return ;

            }
            button5_Click(null, null);
            // 将数组nvmreg输出到.csv文件
            //设置文档文件夹为基本数据存储路径
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //创建基于软件名字的文件夹
            string folderName = "SPS01CalibrateApp";
            string path = Path.Combine(documentPath, folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string timestr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string fileName = timestr + "_" +nvmReg[0xF9].ToString("X2")+ nvmReg[0xFA].ToString("X2") + nvmReg[0xFB].ToString("X2")+ nvmReg[0xFC].ToString("X2") + "_" + "NVMReg.json";
            try
            {   spsData.ID = nvmReg[0xF9].ToString("X2") + nvmReg[0xFA].ToString("X2") + nvmReg[0xFB].ToString("X2") + nvmReg[0xFC].ToString("X2");
                //spsData.NvmData = nvmReg;
                string[] nvmData = new string[0xFF];
                for (int i = 0; i < 0xFF; i++)
                {
                    nvmData[i] = nvmReg[i].ToString("X2");
                }
                spsData.NvmData = nvmData;
                P1FactorTrans();
                P2FactorTrans();
                TSIFactorTrans();
                TSEFactorTrans();


                P1compute();
                P2compute();
                TSIcompute();
                TSEcompute();
                string jsonString = JsonConvert.SerializeObject(spsData,Formatting.Indented);

                //Console.WriteLine(jsonString);
                File.WriteAllText(path+"\\"+fileName, jsonString);
                Console.WriteLine("导出成功！");
                toolStripStatusLabel1.Text = "文件路径"+ path + "\\" + fileName;
                //toolStripStatusLabel1.Text = "文件路径：" + Directory.GetCurrentDirectory();
                //Console.WriteLine("文件路径：" + Directory.GetCurrentDirectory() + "\\" + fileName);
                //toolStripStatusLabel1.Text = "OK";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("导出失败！" + ex.Message);
                Console.WriteLine("导出失败！" + ex);
            }


        }

        private void ExportRawData()
        {
             //将PressDict 和HalfPressDict转换为json格式
            string timestr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string fileName = timestr + "RawData.json";

            string pressDictJson = JsonConvert.SerializeObject(PressDict);
            string halfPressDictJson = JsonConvert.SerializeObject(HalfPressDict);
            string pressTargetJson = JsonConvert.SerializeObject(PressTarget);
            string halfPressTargetJson = JsonConvert.SerializeObject(HalfPressTarget);
            string tempDictJson = JsonConvert.SerializeObject(TempDict);

            // 如果你需要将所有字典放入一个JSON对象中
            var allDicts = new
            {
                PressDict = PressDict,
                HalfPressDict = HalfPressDict,
                PressTarget = PressTarget,
                HalfPressTarget = HalfPressTarget,
                TempDict = TempDict
            };
            string allDictsJson = JsonConvert.SerializeObject(allDicts);
            Console.WriteLine(allDictsJson);
        }

        private void buttonExportRawData_Click(object sender, EventArgs e)
        {
            ExportRawData();
        }

        private void buttonImportRawData_Click(object sender, EventArgs e)
        {

        }

        private void P1FactorTrans()
        {
            double s0 = (nvmReg[0x62]<<8)+nvmReg[0x63];
            //Console.WriteLine((nvmReg[0x62] << 8)+ (nvmReg[0x63]));
            s0 = s0 / (1 << 13);
            double tsc1 = (nvmReg[0x64]<<8)+nvmReg[0x65];
            if ((Convert.ToInt32(tsc1)& 0x8000) != 0)
            {
                tsc1 = tsc1 - 0x10000;
            }
            tsc1 = tsc1 / (1 << 22);
            double tsc2 = (nvmReg[0x66] << 8) + nvmReg[0x67];
            if ((Convert.ToInt32(tsc2)& 0x8000) != 0)
            {
                tsc2 = tsc2 - 0x10000;
            }
            tsc2 = tsc2 / (1 << 30);
            double tsc3 = (nvmReg[0x68] << 8) + nvmReg[0x69];    
            double tco1 = (nvmReg[0x6A] << 8) + nvmReg[0x6B];
            Console.WriteLine(tco1);
            if ((Convert.ToInt32(tco1)& 0x8000) != 0)
            {
                tco1 = tco1 - 0x10000;
            }
            tco1 = tco1 / (1 << 22);
            double tco2 = (nvmReg[0x6C] << 8) + nvmReg[0x6D];
            if ((Convert.ToInt32(tco2)& 0x8000) != 0)
            {
                tco2 = tco2 - 0x10000;
            }
            tco2 = tco2 / (1 << 30);
            double tco3 = (nvmReg[0x6E] << 8) + nvmReg[0x6F];
            double f0 = (nvmReg[0x70] << 8) + nvmReg[0x71];
            Console.WriteLine(f0);
            if ((Convert.ToInt32(f0)& 0x8000) != 0)
            {
                f0 = f0 - 0x10000;
            }
            f0 = f0 / (1 << 15);
            double k2 = (nvmReg[0x72] << 8) + nvmReg[0x73];
            if ((Convert.ToInt32(k2)& 0x8000) != 0)
            {
                k2 = k2 - 0x10000;
            }
            k2 = k2 / (1 << 16);
            double k3 = (((nvmReg[0x74]<<2)+nvmReg[0x75])&0xFFC0) >> 6;
            if ((Convert.ToInt32(k3)& 0x200) != 0)
            {
                k3 = k3 - 0x400;
            }
            k3 = k3 / (1 << 17);

            double baseT = ((nvmReg[0x92] << 8) + nvmReg[0x93])&0xFFF;
            if ((Convert.ToInt32(baseT)& 0x800) != 0)
            {
                baseT = baseT - 0x1000;
            }
            baseT = baseT / 8;
            spsData.P1Factor = new Dictionary<string, double>()
            {
                {"s0",s0},
                {"tsc1",tsc1},
                {"tsc2",tsc2},
                {"tsc3",tsc3},
                {"tco1",tco1},
                {"tco2",tco2},
                {"tco3",tco3},
                {"f0",f0},
                {"k2",k2},
                {"k3",k3},
                {"baseT",baseT}
            };

        }

        private void P2FactorTrans()
        {
            double s0 = (nvmReg[0x79] << 8) + nvmReg[0x7A];
            s0 = s0 / (1 << 13);
            double tsc1 = (nvmReg[0x7B] << 8) + nvmReg[0x7C];
            if ((Convert.ToInt32(tsc1)& 0x8000) != 0)
            {
                tsc1 = tsc1 - 0x10000;
            }
            tsc1 = tsc1 / (1 << 22);
            double tsc2 = (nvmReg[0x7D] << 8) + nvmReg[0x7E];
            if ((Convert.ToInt32(tsc2)& 0x8000) != 0)
            {
                tsc2 = tsc2 - 0x10000;
            }
            tsc2 = tsc2 / (1 << 30);
            double tsc3 = (nvmReg[0x7F] << 8) + nvmReg[0x80];
            double tco1 = (nvmReg[0x81] << 8) + nvmReg[0x82];
            if ((Convert.ToInt32(tco1)& 0x8000) != 0)
            {
                tco1 = tco1 - 0x10000;
            }
            tco1 = tco1 / (1 << 22);
            double tco2 = (nvmReg[0x83] << 8) + nvmReg[0x84];
            if ((Convert.ToInt32(tco2)& 0x8000) != 0)
            {
                tco2 = tco2 - 0x10000;
            }
            tco2 = tco2 / (1 << 30);
            double tco3 = (nvmReg[0x85] << 8) + nvmReg[0x86];
            double f0 = (nvmReg[0x87]<<8)+nvmReg[0x88];
            if ((Convert.ToInt32(f0)& 0x8000) != 0)
            {
                f0 = f0 - 0x10000;
            }
            Console.WriteLine("f0");
            Console.WriteLine(f0);
            f0 = f0 / (1 << 15);
            double k2 = (nvmReg[0x89] << 8) + nvmReg[0x8A];
            if ((Convert.ToInt32(k2)& 0x8000) != 0)
            {
                k2 = k2 - 0x10000;
            }
            k2 = k2 / (1 << 16);
            double k3 = (((nvmReg[0x8b] << 2) + nvmReg[0x8C])&0xFFC0) >> 6;
            if ((Convert.ToInt32(k3)& 0x200) != 0)
            {
                k3 = k3 - 0x400;
            }
            k3 = k3/ (1 << 17);
            double baseT = ((nvmReg[0x92] << 8) + nvmReg[0x93])&0xFFF;
            if ((Convert.ToInt32(baseT)& 0x800) != 0)
            {
                baseT = baseT - 0x1000;
            }
            baseT = baseT / 8;
            spsData.P2Factor = new Dictionary<string, double>()
            {
                {"s0",s0},
                {"tsc1",tsc1},
                {"tsc2",tsc2},
                {"tsc3",tsc3},
                {"tco1",tco1},
                {"tco2",tco2},
                {"tco3",tco3},
                {"f0",f0},
                {"k2",k2},
                {"k3",k3},
                {"baseT",baseT}
            };
        }

        private void TSIFactorTrans()
        {
            double k = nvmReg[0x8e];
            if ((Convert.ToInt32(k)& 0x80) != 0)
            {
                k = k - 0x100;

            }
            k = k + 1808;
            double m = nvmReg[0x8f];
            if ((Convert.ToInt32(m)& 0x80) != 0)
            {
                m = m - 0x100;
            }
            m = (m / (1 << 8))+2.63;

            double Toff = nvmReg[0x90];
            if ((Convert.ToInt32(Toff)& 0x80) != 0)
            {
                Toff = Toff - 0x100;
            }
            Toff = Toff +279;

            spsData.TSIFactor = new Dictionary<string, double>()
            {
                {"k",k},
                {"m",m},
                {"Toff",Toff}
            };
        }
        private void TSEFactorTrans()
        {
            double mt = (nvmReg[0x91]<<4)+((nvmReg[0x92]&0xF0)>>4);
            Console.WriteLine("mt");
            Console.WriteLine(mt);
            if ((Convert.ToInt32(mt)& 0x800) != 0)
            {
                mt = mt - 0x1000;
            }
            mt = mt / (1 <<11);

            double t0 = ((nvmReg[0x92]&0x0F)<<8)+nvmReg[0x93];
            if ((Convert.ToInt32(t0)& 0x800) != 0)
            {
                t0 = t0 - 0x1000;
            }
            t0 = t0 / (1 << 3);
            double kt = (nvmReg[0x94]<<2)+((nvmReg[0x95]&0xC0)>>6);
            Console.WriteLine("kt");
            Console.WriteLine(kt);
            if ((Convert.ToInt32(kt)& 0x200) != 0)
            {
                kt = kt - 0x400;
            }
            kt = 1 / (kt + 667);
            double kts = ((nvmReg[0x95]&0x03)<<8)+nvmReg[0x96];
            if ((Convert.ToInt32(kts)& 0x200) != 0)
            {
                kts = kts - 0x400;
            }
            Console.WriteLine(kts);
            kts = kts / (1 << 11);
            spsData.TSEFactor = new Dictionary<string, double>()
            {
                {"mt",mt},
                {"t0",t0},
                {"kt",kt},
                {"kts",kts}
            };
        }

        private void P1compute()
        {
            subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
            double P1raw = subCtrlForm.Spscom.getRawdata("P1", 0, 1);
            if ((Convert.ToInt32(P1raw) & 0x8000) != 0)
            {
                P1raw = P1raw - 0x10000;
            }
            Console.WriteLine(P1raw);
            //double P2raw = subCtrlForm.Spscom.getRawdata("P2", 0, 1);
            //double P2O= subCtrlForm.Spscom.getRawdata("P2O", 0, 1);
            //subCtrlForm.Spscom.SetComMode("STRT_CM", "01", true);
            double P1O= subCtrlForm.Spscom.getRawdata("P1O", 0, 1);
            double T = subCtrlForm.Spscom.getRawdata("TSIO", 0, 1) / 64;
            //Console.WriteLine(P1raw);
            T = T-spsData.P1Factor["baseT"];
            //Console.WriteLine(T);
            double Voff = spsData.P1Factor["f0"]+(spsData.P1Factor["tco1"]*T+spsData.P1Factor["tco2"]*T*T+spsData.P1Factor["tco3"]*T*T*T);
            //Console.WriteLine(Voff);
            double Sen = spsData.P1Factor["s0"]*(1+spsData.P1Factor["tsc1"]*T+spsData.P1Factor["tsc2"]*T*T+spsData.P1Factor["tsc3"]*T*T*T);
            //Console.WriteLine(Sen);
            double Btemp = Sen * (P1raw / (1 << 15) - Voff);

            double Bconv = Btemp + (spsData.P1Factor["k2"]*Btemp*Btemp + spsData.P1Factor["k3"] * Btemp*Btemp*Btemp);

            spsData.P1Raw = P1raw;
            spsData.P1O = P1O;
            spsData.P1Comp = Bconv*(1<<15);

        }
        private void P2compute()
        {
            subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
            //double P1raw = subCtrlForm.Spscom.getRawdata("P1", 0, 1);
            double P2raw = subCtrlForm.Spscom.getRawdata("P2", 0, 1);
            if ((Convert.ToInt32(P2raw) & 0x8000) != 0)
            {
                P2raw = P2raw - 0x10000;
            }
            double P2O = subCtrlForm.Spscom.getRawdata("P2O", 0, 1);
            //subCtrlForm.Spscom.SetComMode("STRT_CM", "01", true);
            //double P1O = subCtrlForm.Spscom.getRawdata("P1O", 0, 1);
            double T = subCtrlForm.Spscom.getRawdata("TSIO", 0, 1) / 64;
            //Console.WriteLine(P1raw);
            T = T - spsData.P2Factor["baseT"];
            Console.WriteLine(T);
            double Voff = spsData.P2Factor["f0"] + spsData.P2Factor["tco1"] * T + spsData.P2Factor["tco2"] * T * T + spsData.P2Factor["tco3"] * T * T * T;
            Console.WriteLine(Voff);
            double Sen = spsData.P2Factor["s0"] * (1 + spsData.P2Factor["tsc1"] * T + spsData.P2Factor["tsc2"] * T * T + spsData.P2Factor["tsc3"] * T * T * T);
            Console.WriteLine(Sen);
            double Btemp = Sen * (P2raw / (1 << 15) - Voff);
            //Console.WriteLine(P1raw/(1 <<15));
            //Console.WriteLine(Btemp);
            double Bconv = Btemp + spsData.P2Factor["k2"] * Btemp * Btemp + spsData.P2Factor["k3"] * Btemp * Btemp * Btemp;
            //Console.WriteLine(Bconv);
            //Console.WriteLine(Bconv*(1<<15));
            spsData.P2Raw = P2raw;
            spsData.P2O = P2O;
            spsData.P2Comp = Bconv * (1 << 15);
            //spsData.P1Comp = Bconv * (1 << 15);
            //spsData.P2Raw = P2raw;
            //spsData.P2O = P2O;
            //spsData.P2Comp = 
        }

        private void TSIcompute()
        {
            subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
            double TSIraw = subCtrlForm.Spscom.getRawdata("TSI", 0, 1);
            if ((Convert.ToInt32(TSIraw) & 0x8000) != 0)
            {
                TSIraw = TSIraw - 0x10000;
            }
            spsData.TSI = TSIraw;
            TSIraw = TSIraw / (1 << 15);

            double T = subCtrlForm.Spscom.getRawdata("TSIO", 0, 1) / 64;
            // TsiComp = kt*TSIraw/(1+mt*TSIraw)-toff
            double TSIComp =  (spsData.TSIFactor["k"] * TSIraw / (1 + spsData.TSIFactor["m"] * TSIraw)-spsData.TSIFactor["Toff"]);
            spsData.TSIO = T;
            spsData.TSIComp = TSIComp;


        }

        private void TSEcompute()
        {
            subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
            double TSEraw = subCtrlForm.Spscom.getRawdata("TSE", 0, 1);
            if ((Convert.ToInt32(TSEraw) & 0x8000) != 0)
            {
                TSEraw = TSEraw - 0x10000;
            }
            spsData.TSE = TSEraw;
            TSEraw = TSEraw / (1 << 15);
            double T = subCtrlForm.Spscom.getRawdata("TSEO", 0, 1) / 64;
            // TCSIComp = (TSEraw-mt)*(1+kts*(TSEraw-mt)/kt-t0
            double TSEComp = ((TSEraw - spsData.TSEFactor["mt"]) * (1 + spsData.TSEFactor["kts"] * (TSEraw - spsData.TSEFactor["mt"]))) / spsData.TSEFactor["kt"] + spsData.TSEFactor["t0"];
            spsData.TSEO = T;
            spsData.TSEComp = TSEComp;
        }

        private void buttonGetAllData_Click(object sender, EventArgs e)
        {
            //选项卡四
            //根据当前所有的outmem 获取所有的数据并添加在listbox 中
            // subCtrlForm.Spscom.RawAddr.Keys.ToArray()
            string[] items = subCtrlForm.Spscom.RawAddr.Keys.ToArray();
            subCtrlForm.Spscom.SetComMode("STRT_MEAS", "01", true);
            listBoxDataList.Items.Clear();
            //int value = 
            for (int i = 0; i < items.Length; i++)
            {
                int value;
                if (items[i].Contains("OFF") || items[i].Contains("FG"))
                {
                    value = subCtrlForm.Spscom.getMidData(items[i], 0, 1);
                }
                else
                {
                    value = subCtrlForm.Spscom.getRawdata(items[i], 0, 1);
                }
                //textBoxValue.Text = value.ToString();
                // 将项目和数据添加到listbox 中
                listBoxDataList.Items.Add(items[i] + ":\t"+value.ToString());
                
            }
        }
    }

    public class SpsData
    {
        public string ID { get; set; }
        public Dictionary<string, double> P1Factor { get; set; }
        public Dictionary<string, double> P2Factor { get; set; }
        public Dictionary<string, double> TSIFactor { get; set; }
        public Dictionary<string, double> TSEFactor { get; set; }
        public string[] NvmData { get; set; }
        public double P1Raw { get; set; }
        public double P2Raw { get; set; }
        public double P1O { get; set; }
        public double P2O { get; set; }
        public double P1Comp { get; set;}
        public double P2Comp { get; set; }
        public double TSI { get; set; }
        public double TSE { get; set; }
        public double TSIO { get; set; }
        public double TSEO { get; set; }
        public double TSIComp { get; set; }
        public double TSEComp { get; set; }



    }
}
