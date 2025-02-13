using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using SPS01CalibrateAndTestNewModeApp.Mode;
using SPS01CalibrateAndTestNewModeApp.ViewMode;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.SubForm;

namespace SPS01CalibrateAndTestNewModeApp
{
    public partial class Form1 : Form
    {
        private readonly EvbViewModel _evbViewModel;
        private readonly IndividualCalibrateViewModel _individualCalibrateViewModel;
        private readonly RegViewModel _regViewModel;
        private readonly SpsCalibration _spsCalibration;
        private readonly OutViewModel _outViewModel;
        private readonly RegDisplayModel _regDisplayModel;
        
        private ReaLTaiizor.Controls.AloneTextBox[] _textBox;
        private ReaLTaiizor.Controls.AloneTextBox[] _textBoxTarget;
        private ReaLTaiizor.Controls.AloneTextBox[] _textBoxTemp;
        private ReaLTaiizor.Controls.AloneTextBox[] _textBoxTempTarget;
        
        private int _focusIndex;
        private int _focusTempIndex;
        private string _regReadTimeMsg;
        private string _nvmReadTimeMsg;


        public Form1()
        {
            InitializeComponent();
            
            _spsCalibration = ServiceContainer.Resolve<SpsCalibration>();
            // 设置子选项卡的名字
            tabPage1.Text = "设置";
            tabPage2.Text = "标定";
            tabPage3.Text = "寄存器";
            tabPage4.Text = "输出";
            
            
            _textBox = new ReaLTaiizor.Controls.AloneTextBox[10];
            _textBox[0] = textBoxT0P1;
            _textBox[1] = textBoxT0P2;
            _textBox[2] = textBoxT0P3;
            _textBox[3] = textBoxT0P4;
            _textBox[4] = textBoxT1P1;
            _textBox[5] = textBoxT1P2;
            _textBox[6] = textBoxT2P1;
            _textBox[7] = textBoxT2P2;
            _textBox[8] = textBoxT3P1;
            _textBox[9] = textBoxT3P2;
            
            _textBoxTemp = new ReaLTaiizor.Controls.AloneTextBox[4];
            _textBoxTemp[0] = textBoxT0;
            _textBoxTemp[1] = textBoxT1;
            _textBoxTemp[2] = textBoxT2;
            _textBoxTemp[3] = textBoxT3;
            
            _textBoxTarget = new ReaLTaiizor.Controls.AloneTextBox[4];
            _textBoxTarget[0] = textBoxO1;
            _textBoxTarget[1] = textBoxO2;
            _textBoxTarget[2] = textBoxO3;
            _textBoxTarget[3] = textBoxO4;

            _textBoxTempTarget = new ReaLTaiizor.Controls.AloneTextBox[4];
            _textBoxTempTarget[0] = TextBoxTT0;
            _textBoxTempTarget[1] = TextBoxTT1;
            _textBoxTempTarget[2] = TextBoxTT2;
            _textBoxTempTarget[3] = TextBoxTT3;

            // 选项卡1 设置
            _evbViewModel = new EvbViewModel();
            ComboxEvbPortList.DataSource = _evbViewModel.PortNames;
            ComboxEvbBps.DataSource = _evbViewModel.BaudRates;
            ComboxConnMode.DataSource = _evbViewModel.ProductConnMode;
            
            ComboxEvbPortList.DataBindings.Add("SelectedValue", _evbViewModel, "SelectedPortName", true, DataSourceUpdateMode.OnPropertyChanged);
            ComboxEvbBps.DataBindings.Add("SelectedValue", _evbViewModel, "SelectedBaudRate", true, DataSourceUpdateMode.OnPropertyChanged);
            ComboxConnMode.DataBindings.Add("SelectedValue", _evbViewModel, "SelectedConnMode", true, DataSourceUpdateMode.OnPropertyChanged);
                
            BottonEvbConnect.Click += async (sender, e) =>
            {
                if (BottonEvbConnect.Text == "连接")
                {
                    try
                    {
                        _evbViewModel.OpenPort();
                        bool success = await Task.Run(() => _evbViewModel.ConnectTest());
                        
                        if (!success)
                        {
                            MessageBox.Show("连接失败");
                            _evbViewModel.ClosePort();
                        }
                        else
                        {
                            BottonEvbConnect.Text = "断开";
                            // 可以在这里添加连接成功的UI反馈
                            BottonEvbConnect.InactiveColor = Color.LightGreen;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"连接失败: {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        _evbViewModel.ClosePort();
                        BottonEvbConnect.Text = "连接";
                        // 可以在这里添加断开连接的UI反馈
                        BottonEvbConnect.InactiveColor = Color.Black;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"断开连接失败: {ex.Message}");
                    }
                }
            };

            ButtonProductConn.Click += async (sender, e) =>
            {
                try
                {
                    bool success = await Task.Run(() => _evbViewModel.ConnPd());
                    if (!success)
                    {
                        MessageBox.Show("产品连接失败");
                    }
                    else
                    {
                        ButtonProductConn.InactiveColor = Color.Green;
                        // MessageBox.Show("产品连接成功");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            
            ComboxEvbPortList.SelectedIndexChanged += async (sender, e) => 
            {
                if (ComboxEvbPortList.SelectedItem?.ToString() == "刷新")
                {
                    // 禁用ComboBox防止重复点击
                    ComboxEvbPortList.Enabled = false;
            
                    try
                    {
                        await Task.Run(() => _evbViewModel.RefreshPortNames());
                        // 重置选择
                        ComboxEvbPortList.SelectedIndex = -1;
                        
                        ComboxEvbPortList.DataSource = null;
                        ComboxEvbPortList.DataSource = _evbViewModel.PortNames;
                    }
                    finally
                    {
                        ComboxEvbPortList.Enabled = true;
                    }
                }
            };
            

            // 选项卡2 标定
            _individualCalibrateViewModel = new IndividualCalibrateViewModel();
            ComRawDataAvg.DataSource = _individualCalibrateViewModel.RawDataAvgPoint;
            ComRawDataAvg.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawDataAvgPoint", true, DataSourceUpdateMode.OnPropertyChanged);
            ComRawDataJumpPoint.DataSource = _individualCalibrateViewModel.RawDataJumpPoint;
            ComRawDataJumpPoint.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawDataJumpPoint", true, DataSourceUpdateMode.OnPropertyChanged);
            ComRawMode.DataSource = _individualCalibrateViewModel.RawDataMode;
            ComRawMode.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawDataMode", true, DataSourceUpdateMode.OnPropertyChanged);
            ComCalibrateMode.DataSource = _individualCalibrateViewModel.RawCalibrateMode;
            ComCalibrateMode.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawCalibrateMode", true, DataSourceUpdateMode.OnPropertyChanged);

            textBoxT0P1.DataBindings.Add("Visible", _individualCalibrateViewModel, "T0P1Visible");
            textBoxT0P2.DataBindings.Add("Visible", _individualCalibrateViewModel, "T0P2Visible");
            textBoxT0P3.DataBindings.Add("Visible", _individualCalibrateViewModel, "T0P3Visible");
            textBoxT0P4.DataBindings.Add("Visible", _individualCalibrateViewModel, "T0P4Visible");
            textBoxT1P1.DataBindings.Add("Visible", _individualCalibrateViewModel, "T1P1Visible");
            textBoxT1P2.DataBindings.Add("Visible", _individualCalibrateViewModel, "T1P2Visible");
            textBoxT2P1.DataBindings.Add("Visible", _individualCalibrateViewModel, "T2P1Visible");
            textBoxT2P2.DataBindings.Add("Visible", _individualCalibrateViewModel, "T2P2Visible");
            textBoxT3P1.DataBindings.Add("Visible", _individualCalibrateViewModel, "T3P1Visible");
            textBoxT3P2.DataBindings.Add("Visible", _individualCalibrateViewModel, "T3P2Visible");
            
            textBoxO1.DataBindings.Add("Visible", _individualCalibrateViewModel, "O1Visible");
            textBoxO2.DataBindings.Add("Visible", _individualCalibrateViewModel, "O2Visible");
            textBoxO3.DataBindings.Add("Visible", _individualCalibrateViewModel, "O3Visible");
            textBoxO4.DataBindings.Add("Visible", _individualCalibrateViewModel, "O4Visible");
            
            textBoxT0.DataBindings.Add("Visible", _individualCalibrateViewModel, "T0Visible");
            textBoxT1.DataBindings.Add("Visible", _individualCalibrateViewModel, "T1Visible");
            textBoxT2.DataBindings.Add("Visible", _individualCalibrateViewModel, "T2Visible");
            textBoxT3.DataBindings.Add("Visible", _individualCalibrateViewModel, "T3Visible");
            
            TextBoxTT0.DataBindings.Add("Visible", _individualCalibrateViewModel, "TT0Visible");
            TextBoxTT1.DataBindings.Add("Visible", _individualCalibrateViewModel, "TT1Visible");
            TextBoxTT2.DataBindings.Add("Visible", _individualCalibrateViewModel, "TT2Visible");
            TextBoxTT3.DataBindings.Add("Visible", _individualCalibrateViewModel, "TT3Visible");
            
            ComCalibrateMode.SelectedIndexChanged += (sender, e) =>
            {
                _individualCalibrateViewModel.UpdateTextboxVisibilities();
            };

            ButtonGetRawData.Click += async (sender, e) =>
            {
                if (ComRawMode.SelectedItem?.ToString() == "全桥")
                {
                    var data = await Task.Run(() => _individualCalibrateViewModel.GetP1Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    _textBox[_focusIndex].Text = data.ToString();
                    var data2 = await Task.Run(() => _individualCalibrateViewModel.GetTsiData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    var data3 = await Task.Run(() => _individualCalibrateViewModel.GetTseData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    _textBoxTemp[_focusTempIndex].Text = data2.ToString();

                }
                else if (ComRawMode.SelectedItem?.ToString() == "半桥")
                {
                    var data = await Task.Run(() => _individualCalibrateViewModel.GetP2Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    _textBox[_focusIndex].Text = data.ToString();
                    var data2 = await Task.Run(() => _individualCalibrateViewModel.GetTsiData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    var data3 = await Task.Run(() => _individualCalibrateViewModel.GetTseData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    _textBoxTemp[_focusTempIndex].Text = data2.ToString();
                }
                else if (ComRawMode.SelectedItem?.ToString() == "全桥+半桥")
                {
                    var data1 = await Task.Run(() => _individualCalibrateViewModel.GetP1Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    var data2 = await Task.Run(() => _individualCalibrateViewModel.GetP2Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    _textBox[_focusIndex].Text = data1.ToString();
                    var data3 = await Task.Run(() => _individualCalibrateViewModel.GetTsiData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    var data4 = await Task.Run(() => _individualCalibrateViewModel.GetTseData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    _textBoxTemp[_focusTempIndex].Text = data3.ToString();
                }
                
            };
            
            ButtonGetId.Click += async (sender, e) =>
            {
                await Task.Run(() => _individualCalibrateViewModel.GetId());
                LabelID.Text = _individualCalibrateViewModel.Id;
            };

            ButtonCalibration.Click += async (sender, e) =>
            {
                var pressTargetList = new List<double>();
                foreach (var target in _textBoxTarget)
                {
                    if (target.Visible)
                    {
                        if (target.Text == "")
                        {
                            MessageBox.Show("请输入"+target.Name+"压力目标值");
                            return;
                        }
                        pressTargetList.Add(double.Parse(target.Text));
                    }
                    else
                    {
                        pressTargetList.Add(0);
                    }
                }
                
                _individualCalibrateViewModel.UpdatePressTarget(pressTargetList);

                var tempTargetList = new List<double>();
                foreach (var target in _textBoxTempTarget)
                {
                    if (target.Visible)
                    {
                        if (target.Text == "")
                        {
                            MessageBox.Show("请输入"+target.Name+"温度目标值");
                            return;
                        }
                        tempTargetList.Add(double.Parse(target.Text));
                    }
                    else
                    {
                        tempTargetList.Add(0);
                    }
                }
                
                _individualCalibrateViewModel.UpdateTempTarget(tempTargetList);
                
                foreach (var target in _textBox)
                {
                    if (target.Visible)
                    {
                        if (target.Text == "")
                        {
                            MessageBox.Show("请采集"+target.Name+"原始数据");
                            return;
                        }
                    }
                }
                
                // 调用 网络api 进行计算
                _individualCalibrateViewModel.Calibrate();
                
            };
            
            
            // 选项卡3 寄存器
            poisonDataGridView1.RowCount = 8;
            poisonDataGridView1.RowHeadersVisible = true;

            // 设置行头宽度
            poisonDataGridView1.RowHeadersWidth = 15; // 根据需要调整
            poisonDataGridView1.RowHeadersVisible = false; // 显示行头
            // 添加自定义列

            DataGridViewColumn rowNumberColumn = new DataGridViewTextBoxColumn();
            rowNumberColumn.HeaderText = "行号";
            rowNumberColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // 居中
            rowNumberColumn.Name = "RowNumber";
            rowNumberColumn.ReadOnly = true; // 设置为只读
            rowNumberColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 居中

            // 将新列添加到列集合中
            poisonDataGridView1.Columns.Insert(0, rowNumberColumn);

            // 设置列的宽度
            rowNumberColumn.Width = 80; // 根据需要调整
            
            poisonDataGridView1.Size = new Size(80 * 9, 23 * 8 + 21);
            poisonDataGridView1.ScrollBars = ScrollBars.None;
            
            _regViewModel = new RegViewModel();
            ComBoxRegDisplayModel.DataSource = _regViewModel.RegDisplayName;
            ComBoxRegDisplayModel.DataBindings.Add("SelectedValue", _regViewModel, "SelectedRegDisplayName", true, DataSourceUpdateMode.OnPropertyChanged);

            LabelCurrentPage.DataBindings.Add("Text", _regViewModel,"CurrentPage", true, DataSourceUpdateMode.OnPropertyChanged);
            dataGridView1_CellValueAdd();
            ButtonPageAdd.Click += (sender, e) =>
            {
                _regViewModel.ButtonPageAddClick();
                LabelCurrentPage.Text = _regViewModel.CurrentPage;
                dataGridView1_CellValueAdd();
            };
            
            ButtonPageSub.Click += (sender, e) =>
            {
                _regViewModel.ButtonPageSubClick();
                LabelCurrentPage.Text = _regViewModel.CurrentPage;
                dataGridView1_CellValueAdd();
            };
            
            ButtonGetNvmData.Click += async (sender, e) =>
            {
                await Task.Run(() => _regViewModel.ButtonGetAllNvmClick());
                _nvmReadTimeMsg = "Nvm Update @"+DateTime.Now.ToString("HH:mm:ss");
                labelRegUpdateMsg.Text = _nvmReadTimeMsg;
                labelRegUpdateMsg.BackColor = Color.LightGreen;
                dataGridView1_CellValueAdd();
            };

            ButtonGetRegData.Click += async (sender, e) =>
            {
                await Task.Run(() => _regViewModel.ButtonGetAllRegClick());
                _regReadTimeMsg = "Reg Update @" + DateTime.Now.ToString("HH:mm:ss");
                labelRegUpdateMsg.Text = _regReadTimeMsg;
                labelRegUpdateMsg.BackColor = Color.LightBlue;
                dataGridView1_CellValueAdd();
            };
            
            ComBoxRegDisplayModel.SelectedIndexChanged += (sender, e) =>
            {
                dataGridView1_CellValueAdd();
                if (ComBoxRegDisplayModel.SelectedItem?.ToString() == "Nvm")
                {
                    labelRegUpdateMsg.Text = _nvmReadTimeMsg;
                    labelRegUpdateMsg.BackColor = Color.LightGreen;
                }
                else
                {
                    labelRegUpdateMsg.Text = _regReadTimeMsg;
                    labelRegUpdateMsg.BackColor = Color.LightBlue;
                }
            };
            
            // 选项卡4 输出
            _outViewModel = new OutViewModel();
            ComboxOutName.DataSource = _outViewModel.OutputNames;
            ComboxOutName.DataBindings.Add("SelectedValue", _outViewModel, "SelectedOutputName", true, DataSourceUpdateMode.OnPropertyChanged);
            textOutValue.DataBindings.Add("Text", _outViewModel, "OutputValue", true, DataSourceUpdateMode.OnPropertyChanged);
            
            // 添加滚动条
            ListBoxAllOutValue.ScrollAlwaysVisible = true;
            ListBoxAllOutValue.HorizontalScrollbar = true;

            ButtonGetOutData.Click += async (sender, e) =>
            {
                await Task.Run(() => _outViewModel.ButtonOutputClick());
                textOutValue.Text = _outViewModel.OutputValue.ToString();
            };
            
            ButtonGetAllOutData.Click += async (sender, e) =>
            {
                ListBoxAllOutValue.Items.Clear();
                ListBoxAllOutValue.Items.Add("数据获取中.....");
                await Task.Run(() => _outViewModel.ButtonAllOutputClick());
                ListBoxAllOutValue.Items.Clear();
                ListBoxAllOutValue.Items.AddRange(_outViewModel.AllOutputValue.ToArray());
            };
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            // throw new System.NotImplementedException();
            toolStripStatusLabel1.Text = DateTime.Now.ToString("u");

            for (int i = 0; i < 10; i++)
            {
                if (!_textBox[i].ContainsFocus) continue;
                _focusIndex = i;
                _focusTempIndex = int.Parse(_textBox[i].Name.Substring(8, 1));
                break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
            
            timer1.Interval = 100;
            timer1.Start();
        }
        
        private void dataGridView1_CellValueAdd()
        {
            for (var i = 0; i < 8; i++)
            {
                poisonDataGridView1.Rows[i].Cells["RowNumber"].Value =
                    "0x" + ((int.Parse(LabelCurrentPage.Text) - 1) * 64 + i * 8).ToString("X2");
            }
            switch (ComBoxRegDisplayModel.Text)
            { 
                // 选项卡3 数据改变
                case "Nvm":
                {
                    for (var i = 0; i < 8; i++)
                    for (var j = 1; j < 9; j++)
                        poisonDataGridView1.Rows[i].Cells["Column" + j].Value =
                            _spsCalibration.NvmData[(int.Parse(LabelCurrentPage.Text) - 1) * 64 + i * 8 + j - 1].ToString("X2");
                    //nvm[(int.Parse(textBox2.Text) - 1) * 64 + e.RowIndex * 8 + e.ColumnIndex] = Convert.ToByte(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    break;
                }
                case "Reg":
                {
                    for (var i = 0; i < 8; i++)
                    for (var j = 1; j < 9; j++)
                        poisonDataGridView1.Rows[i].Cells["Column" + j].Value =
                            _spsCalibration.RegData[(int.Parse(LabelCurrentPage.Text) - 1) * 64 + i * 8 + j - 1].ToString("X2");
                    //nvmReg[(int.Parse(textBox2.Text) - 1) * 64 + e.RowIndex * 8 + e.ColumnIndex] = Convert.ToByte(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    break;
                }
            }
        }
        
        private void MenuItemDataView_Click(object sender, EventArgs e)
        {
            //打开FormDisplaySpsData 窗口
            FormDisplaySpsdata formDisplaySpsData = new FormDisplaySpsdata();
            formDisplaySpsData.Show();
        }
    }
}