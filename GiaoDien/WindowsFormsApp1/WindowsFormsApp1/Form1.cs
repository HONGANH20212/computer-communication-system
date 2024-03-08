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
using ZedGraph;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] baud = { "6600", "7600", "8600", "9600", "10600" };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Create ZedGraph
            GraphPane Hanhong = zedGraphControl1.GraphPane;
            Hanhong.Title.Text = "ĐỒ THỊ CẬP NHẬP NHIỆT ĐỘ THEO THỜI GIAN";
            Hanhong.XAxis.Title.Text = "THỜI GIAN";
            Hanhong.YAxis.Title.Text = "NHIỆT ĐỘ";
            //Create list data
            RollingPointPairList list = new RollingPointPairList(60000);
            //Create ZedGraph for list data
            LineItem Line = Hanhong.AddCurve("NHIỆT ĐỘ", list, Color.Blue, SymbolType.Circle);
            // Setup angle and vertical line
            Hanhong.YAxis.Scale.Min = 0;
            Hanhong.YAxis.Scale.Max = 100;
            Hanhong.YAxis.Scale.MinorStep = 1;
            Hanhong.YAxis.Scale.MajorStep = 5;

            Hanhong.XAxis.Scale.Min = 0;
            Hanhong.XAxis.Scale.Max = 100;
            Hanhong.XAxis.Scale.MinorStep = 25;
            Hanhong.XAxis.Scale.MajorStep = 50;

            zedGraphControl1.AxisChange();


            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                listcom.Items.Add(port);
                listbaud.Items.AddRange(baud);
            }
        }

        //Declare list data
        int Tong = 0;
        public void draw(double NHIETDO)
        {
            if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
                return;
            LineItem Line = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            if (Line == null)
                return;
            IPointListEdit list = Line.Points as IPointListEdit;
            if (list == null)
                return;

            list.Add(Tong, NHIETDO);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            Tong += 3;
        }
        private void nutketnoi_Click(object sender, EventArgs e)
        {
            if(listcom.Text == "")
            {
                MessageBox.Show("Ban chua nhap cong COM", "Thong bao");
            }
            else if(listbaud.Text == "")
            {
                MessageBox.Show("Ban chua nhap Baud Rate", "Thong bao");
            }
            if(serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                nutketnoi.Text = "CONNECT";
                nutketnoi.BackColor = Color.White;
            } 
            else if(serialPort1.IsOpen == false)
            {
                serialPort1.PortName = listcom.Text;
                serialPort1.BaudRate = int.Parse(listbaud.Text);
                serialPort1.Open();
                nutketnoi.Text = "DISCONNECT";
                nutketnoi.BackColor = Color.DeepSkyBlue;
            }
        }
        bool LED = true;
        private void led1_Click(object sender, EventArgs e)
        {
            if(LED == true)
            {
                serialPort1.Write("a");
                led1.Text = "OFF";
                LED = false;
            }
            else
            {
                serialPort1.Write("A");
                led1.Text = "ON";
                LED = true;
            }
        }

        bool MG = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (MG == true)
            {
                serialPort1.Write("b");
                button1.Text = "OFF";
                MG = false;
            }
            else
            {
                serialPort1.Write("B");
                button1.Text = "ON";
                MG = true;
            }
        }
        bool Camera = true;
        private void button4_Click(object sender, EventArgs e)
        {
            if (Camera == true)
            {
                serialPort1.Write("c");
                button4.Text = "OFF";
                Camera = false;
            }
            else
            {
                serialPort1.Write("C");
                button4.Text = "ON";
                Camera = true;
            }
        }
        bool Cong = true;
        private void button6_Click(object sender, EventArgs e)
        {
            if (Cong == true)
            {
                serialPort1.Write("d");
                button6.Text = "OFF";
                Cong = false;
            }
            else
            {
                serialPort1.Write("D");
                button6.Text = "ON";
                Cong = true;
            }
        }
        bool Quat = true;
        private void button2_Click(object sender, EventArgs e)
        {
            if (Quat == true)
            {
                serialPort1.Write("e");
                button2.Text = "OFF";
                Quat = false;
            }
            else
            {
                serialPort1.Write("E");
                button2.Text = "ON";
                Quat = true;
            }
        }
        bool Tivi = true;
        private void button3_Click(object sender, EventArgs e)
        {
            if (Tivi == true)
            {
                serialPort1.Write("f");
                button3.Text = "OFF";
                Tivi = false;
            }
            else
            {
                serialPort1.Write("F");
                button3.Text = "ON";
                Tivi = true;
            }
        }
        bool Baochay = true;
        private void button5_Click(object sender, EventArgs e)
        {
            if (Baochay == true)
            {
                serialPort1.Write("g");
                button5.Text = "OFF";
                Baochay = false;
            }
            else
            {
                serialPort1.Write("G");
                button5.Text = "ON";
                Baochay = true;
            }
        }
        bool ChoMeoAn = true;
        private void button7_Click(object sender, EventArgs e)
        {
            if (ChoMeoAn == true)
            {
                serialPort1.Write("h");
                button7.Text = "OFF";
                ChoMeoAn = false;
            }
            else
            {
                serialPort1.Write("H");
                button7.Text = "ON";
                ChoMeoAn = true;
            }
        }
        string DuLieu = ""; 
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DuLieu += serialPort1.ReadExisting();
            if (DuLieu.Length > 3)
            {
                Invoke(new MethodInvoker(() => NHIETDO.Items.Add(DuLieu)));
                Invoke(new MethodInvoker(() => draw(int.Parse(DuLieu))));
                DuLieu = "";
            }    
        }
    }
}
