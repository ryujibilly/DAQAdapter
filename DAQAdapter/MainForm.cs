using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace DAQAdapter
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// SerialPort selections
        /// </summary>
        String[] Ports { get; set; }
        SerialPort OpenedPort=new SerialPort();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            //Ports = SerialPort.GetPortNames();
            //for(int i=0;i<Ports.Length;i++ )
            //{
            //    vListBox1.Items.Add(Ports[i]);
            //}
        }
        private void cOM1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenedPort = serialPort1;
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    serialPort2.Close();
                    serialPort3.Close();
                    serialPort4.Close();
                    toolStripSplitButton1.Text = "COM1:";
                    toolStripSplitButton1.BackColor = Color.Lime;
                    toolStripStatusLabel1.Text = "Opened!";
                    toolStripStatusLabel1.BackColor = Color.Lime;
                }
                Trace.WriteLine(OpenedPort.PortName + "打开，波特率变为" + OpenedPort.BaudRate.ToString());
            }
            catch (System.Exception ex)
            {
                toolStripStatusLabel1.Text = "Closed!";
                toolStripStatusLabel1.BackColor = Color.Red;
                Trace.WriteLine(ex.Message);
            }
        }

        private void cOM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenedPort = serialPort2;
                if (!serialPort2.IsOpen)
                {
                    serialPort1.Close();
                    serialPort2.Open();
                    serialPort3.Close();
                    serialPort4.Close();
                    toolStripSplitButton1.Text = "COM2:";
                    toolStripSplitButton1.BackColor = Color.Lime;
                    toolStripStatusLabel1.Text = "Opened!";
                    toolStripStatusLabel1.BackColor = Color.Lime;
                }
                Trace.WriteLine(OpenedPort.PortName + "打开，波特率变为" + OpenedPort.BaudRate.ToString());
            }
            catch (System.Exception ex)
            {
                toolStripStatusLabel1.Text = "Closed!";
                toolStripStatusLabel1.BackColor = Color.Red;
                Trace.WriteLine(ex.Message);
            }
        }

        private void cOM3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenedPort = serialPort3;
                if (!serialPort3.IsOpen)
                {
                    serialPort1.Close();
                    serialPort2.Close();
                    serialPort3.Open();
                    serialPort4.Close();                       
                    toolStripSplitButton1.Text = "COM3:";
                    toolStripSplitButton1.BackColor = Color.Lime;
                    toolStripStatusLabel1.Text = "Opened!";
                    toolStripStatusLabel1.BackColor = Color.Lime;
                }

                Trace.WriteLine(OpenedPort.PortName + "打开，波特率变为" + OpenedPort.BaudRate.ToString());
            }
            catch (System.Exception ex)
            {

                toolStripStatusLabel1.Text = "Closed!";
                toolStripStatusLabel1.BackColor = Color.Red; Trace.WriteLine(ex.Message);
            }
        }

        private void cOM4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenedPort = serialPort4;
                if (!serialPort4.IsOpen )
                {
                    serialPort1.Close();
                    serialPort2.Close();
                    serialPort3.Close();
                    serialPort4.Open();
                    toolStripSplitButton1.Text = "COM4:";
                    toolStripSplitButton1.BackColor = Color.Lime;
                    toolStripStatusLabel1.Text = "Opened!";
                    toolStripStatusLabel1.BackColor = Color.Lime;
                }

                Trace.WriteLine(OpenedPort.PortName + "打开，波特率变为" + OpenedPort.BaudRate.ToString());
            }
            catch (System.Exception ex)
            {
                toolStripStatusLabel1.Text = "Closed!";
                toolStripStatusLabel1.BackColor = Color.Red;
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
