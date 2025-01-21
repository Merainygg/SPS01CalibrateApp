using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.IO.Ports.SerialPort;

namespace TPLoopTestSystem
{
    public partial class UnoSubForm : Form
    {
        public string PortName { get; private set; }
        private bool _flag;
        public readonly UnoCopy8451 UnoCopy8451;
        

        public UnoSubForm()
        {
            InitializeComponent();
            UnoCopy8451 = new UnoCopy8451();
            label1.Text = "UNO设备";
            comboBox1.Items.AddRange(items: GetPortNames());
            button1.Text = "连接测试";
            button2.Text = "";
            button2.Enabled = false;
            toolStripStatusLabel2.Text = "";

        }

        public void SetComBoxCurValue(string value)
        // 设置comboBox1的值
        {
            if (comboBox1.Items.Contains(value))
            {
                comboBox1.Text = value;
            }
        }

        private void UnoSubForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();

        }

        protected override void WndProc(ref Message m)
        // 串口插拔检测
        {
            base.WndProc(ref m);
            try
            {
                if (comboBox1.Items.Count < GetPortNames().Length)
                {
                    // 串口增加
                    var theCur = comboBox1.Text;
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(items: GetPortNames());
                    comboBox1.Text = "";
                    comboBox1.SelectedText = theCur;
                    //comboBox1.Text = the_cur;
                    //serialPort = new SerialPort();

                }
                else if (comboBox1.Items.Count > GetPortNames().Length && !_flag)
                {
                    // 串口减少
                    _flag = true;
                    var theCur = comboBox1.Text;
                    if (!GetPortNames().Contains(theCur))
                    {
                        if (button1.Text == "连接测试")
                        {
                            //comboBox1.Text = SerialPort.GetPortNames()[0];
                        }
                        else
                        {
                            UnoCopy8451.Close();
                            MessageBox.Show("串口已拔出");
                            button1.Text = "连接测试";
                            _flag = false;
                        }
                    }
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(GetPortNames());
                    comboBox1.Text = "";
                }

            }
            catch { }
            //base.re
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                return;
            }

            if (button1.Text == "连接测试")
            {
                button1.Text = "断开连接";
                
                UnoCopy8451.PortName = comboBox1.Text;
                UnoCopy8451.BaudRate = 9600;
                UnoCopy8451.Open();
                if (UnoCopy8451.VersionInfo != null && UnoCopy8451.IsConnect)
                {
                    button2.Text = "连接成功";
                    button2.BackColor = Color.Green;
                    PortName = comboBox1.Text;
                    _flag = true;
                }
                else
                {
                    button1.Text = "连接测试";
                    button2.Text = "连接失败";
                    button2.BackColor = Color.Red;
                    PortName = null;
                    _flag = false;
                }
            }
            else
            {
                button1.Text = "连接测试";
                UnoCopy8451.Close();
                button2.Text = "";
                button2.BackColor = Color.White;
                PortName = null;
                _flag = false;
            }
        }

        // 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            UnoCopy8451.Close();    

            button2.Text = "";
            button2.BackColor = Color.White;
            button1.Text = "连接测试";
            PortName = null;
            _flag = false;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusLabel2.Text = UnoCopy8451.ReceiveData.Trim();
            }
            catch (Exception exception)
            {
                // Console.WriteLine(exception);
                // throw;
            }
            
        }
        
    }
}
