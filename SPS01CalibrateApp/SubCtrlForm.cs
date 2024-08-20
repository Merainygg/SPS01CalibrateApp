﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SPS01CalibrateApp
{
    public partial class SubCtrlForm : Form
    {
        SPScom spscom = new SPScom();
        public string PortName { get; set; }
        public SPScom Spscom { get => spscom; set => spscom = value; }

        private bool _flag = false;

        public SubCtrlForm()
        {
            InitializeComponent();

            toolStripStatusLabel1.Text = "串口信息";
            label1.Text = "串口端口";
            comboBox1.Items.AddRange(SerialPort.GetPortNames());

            button1.Text = "连接";
            button2.Text = "";

        }

        private void SubCtrlForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请选择串口");
                return;
            }
            else
            {
                if (button2.Text == "已连接")
                {
                    spscom.close();
                    button2.Text = "已断开";
                    button2.BackColor = Color.White;
                    return;
                }

                spscom.PortName = comboBox1.Text;
                spscom.open();
                if (spscom.ConnTs())
                {
                    button2.Text = "已连接";
                    button2.BackColor = Color.Green;
                    PortName = comboBox1.Text;

                }
                else
                {
                    button2.Text = "连接失败";
                    button2.BackColor = Color.Red;
                }
                //spscom.close();
                //button1.Text = "断开";


            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "串口信息:"+spscom.Receive;
        }

        protected override void WndProc(ref Message m)
        {
            //base.WndProc(ref m);
            base.WndProc(ref m);
            try
            {
                if (comboBox1.Items.Count < SerialPort.GetPortNames().Length)
                {
                    // 串口增加
                    string the_cur = comboBox1.Text;
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(SerialPort.GetPortNames());
                    comboBox1.Text = "";
                    comboBox1.SelectedText = the_cur;
                    //comboBox1.Text = the_cur;
                    //serialPort = new SerialPort();

                }
                else if (comboBox1.Items.Count > SerialPort.GetPortNames().Length && !_flag)
                {
                    // 串口减少
                    _flag = true;
                    string the_cur = comboBox1.Text;
                    if (!SerialPort.GetPortNames().Contains(the_cur))
                    {
                        if (button1.Text == "连接")
                        {
                            //comboBox1.Text = SerialPort.GetPortNames()[0];
                        }
                        else
                        {
                            spscom.close();
                            MessageBox.Show("串口已拔出");
                            button1.Text = "连接";
                            _flag = false;
                        }
                    }
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(SerialPort.GetPortNames());
                    //comboBox1.SelectedText = "";
                    comboBox1.Text = "";

                }

            }
            catch { }
            //base.re
        }
    }
}
