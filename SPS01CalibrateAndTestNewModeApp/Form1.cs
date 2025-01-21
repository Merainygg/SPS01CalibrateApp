using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using SPS01CalibrateAndTestNewModeApp.Mode;
using SPS01CalibrateAndTestNewModeApp.ViewMode;

namespace SPS01CalibrateAndTestNewModeApp
{
    public partial class Form1 : Form
    {
        private readonly EvbViewModel _evbViewModel;
        private readonly IndividualCalibrateViewModel _individualCalibrateViewModel;
        private ReaLTaiizor.Controls.AloneTextBox[] _textBox;
        private ReaLTaiizor.Controls.AloneTextBox[] _textBoxTemp;
        
        private int _focusIndex;
        private int _focusTempIndex;


        public Form1()
        {
            InitializeComponent();
            // 设置子选项卡的名字
            tabPage1.Text = "设置";
            tabPage2.Text = "标定";
            
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
            
            // toolStripStatusLabel2.DataBindings.Add("Text", _evbViewMode, "CurMode");
            
            _individualCalibrateViewModel = new IndividualCalibrateViewModel();
            ComRawDataAvg.DataSource = _individualCalibrateViewModel.RawDataAvgPoint;
            ComRawDataAvg.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawDataAvgPoint", true, DataSourceUpdateMode.OnPropertyChanged);
            ComRawDataJumpPoint.DataSource = _individualCalibrateViewModel.RawDataJumpPoint;
            ComRawDataJumpPoint.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawDataJumpPoint", true, DataSourceUpdateMode.OnPropertyChanged);
            ComRawMode.DataSource = _individualCalibrateViewModel.RawDataMode;
            ComRawMode.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawDataMode", true, DataSourceUpdateMode.OnPropertyChanged);
            ComCalibrateMode.DataSource = _individualCalibrateViewModel.RawCalibrateMode;
            ComCalibrateMode.DataBindings.Add("SelectedValue", _individualCalibrateViewModel, "SelectedRawCalibrateMode", true, DataSourceUpdateMode.OnPropertyChanged);
            //

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
            
            ComCalibrateMode.SelectedIndexChanged += (sender, e) =>
            {
                _individualCalibrateViewModel.UpdateTextboxVisibilities();
            };

            ButtonGetRawData.Click += async (sender, e) =>
            {
                if (ComRawMode.SelectedItem?.ToString() == "全桥")
                {
                    int data = await Task.Run(() => _individualCalibrateViewModel.GetP1Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    _textBox[_focusIndex].Text = data.ToString();
                    int data2 = await Task.Run(() => _individualCalibrateViewModel.GetTSIData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    int data3 = await Task.Run(() => _individualCalibrateViewModel.GetTSEData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    _textBoxTemp[_focusTempIndex].Text = data2.ToString();

                }
                else if (ComRawMode.SelectedItem?.ToString() == "半桥")
                {
                    int data = await Task.Run(() => _individualCalibrateViewModel.GetP2Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    _textBox[_focusIndex].Text = data.ToString();
                    int data2 = await Task.Run(() => _individualCalibrateViewModel.GetTSIData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    int data3 = await Task.Run(() => _individualCalibrateViewModel.GetTSEData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    _textBoxTemp[_focusTempIndex].Text = data2.ToString();
                }
                else if (ComRawMode.SelectedItem?.ToString() == "全桥+半桥")
                {
                    int data1 = await Task.Run(() => _individualCalibrateViewModel.GetP1Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    int data2 = await Task.Run(() => _individualCalibrateViewModel.GetP2Data(_textBox[_focusIndex].Name.Substring(7, 4)));
                    _textBox[_focusIndex].Text = data1.ToString();
                    int data3 = await Task.Run(() => _individualCalibrateViewModel.GetTSIData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    int data4 = await Task.Run(() => _individualCalibrateViewModel.GetTSEData(_textBoxTemp[_focusTempIndex].Name.Substring(7, 2)));
                    _textBoxTemp[_focusTempIndex].Text = data3.ToString();
                }
                
            };

        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            // throw new System.NotImplementedException();
            toolStripStatusLabel1.Text = DateTime.Now.ToString();

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
    }
}