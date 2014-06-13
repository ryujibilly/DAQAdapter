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
using System.IO;
using System.Net.Sockets;

namespace DAQAdapter
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// SerialPort selections
        /// </summary>
        String[] Ports { get; set; }
        SerialPort OpenedPort=new SerialPort();
        public delegate void invokeDelegate();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            btnChangePort_Click(null, null);
            timer1.Enabled = true;
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

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            try
            {
                openTcpPort();
            }
            catch (FormatException)
            {
                MessageBox.Show("Port must be an integer", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Port is too large", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void openTcpPort()
        {
            tcpServer1.Close();
            tcpServer1.Port = Convert.ToInt32(txtPort.Text);
            txtPort.Text = tcpServer1.Port.ToString();
            tcpServer1.Open();

            displayTcpServerStatus();
        }

        private void displayTcpServerStatus()
        {
            if (tcpServer1.IsOpen)
            {
                lblStatus.Text = "PORT OPEN";
                lblStatus.BackColor = Color.Lime;
            }
            else
            {
                lblStatus.Text = "PORT NOT OPEN";
                lblStatus.BackColor = Color.Red;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            send();
            this.txtText.Clear();
        }

        private void send()
        {
            string data = "";

            foreach (string line in txtText.Lines)
            {
                data = data + line.Replace("\r", "").Replace("\n", "") + "\r\n";
            }
            data = data.Substring(0, data.Length - 2);

            tcpServer1.Send(data);

            logData(true, data);
        }

        public void logData(bool sent, string text)
        {
            txtLog.Text += "\r\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt") + (sent ? " SENT:\r\n" : " RECEIVED:\r\n");
            txtLog.Text += text;
            txtLog.Text += "\r\n";
            if (txtLog.Lines.Length > 500)
            {
                string[] temp = new string[500];
                Array.Copy(txtLog.Lines, txtLog.Lines.Length - 500, temp, 0, 500);
                txtLog.Lines = temp;
            }
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpServer1.Close();
        }

        private void tcpServer1_OnConnect(tcpServer.TcpServerConnection connection)
        {
            invokeDelegate setText = () => lblConnected.Text = tcpServer1.Connections.Count.ToString();
            Invoke(setText);
            while (tcpServer1.Connections.Count != 0)
            { toolStripStatusLabel4.Text = "Connection: 1 !"; toolStripStatusLabel4.BackColor = Color.Lime; }
        }

        private void tcpServer1_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {
            byte[] data = readStream(connection.Socket);

            if (data != null)
            {
                string dataStr = Encoding.Default.GetString(data);

                invokeDelegate del = () =>
                {
                    logData(false, dataStr);
                };
                Invoke(del);

                data = null;
            }
        }

        protected byte[] readStream(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            if (stream.DataAvailable)
            {
                byte[] data = new byte[client.Available];

                int bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(data, 0, data.Length);
                }
                catch (IOException)
                {
                }

                if (bytesRead < data.Length)
                {
                    byte[] lastData = data;
                    data = new byte[bytesRead];
                    Array.ConstrainedCopy(lastData, 0, data, 0, bytesRead);
                }
                return data;
            }
            return null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            displayTcpServerStatus();
            lblConnected.Text = tcpServer1.Connections.Count.ToString();
        }

        private void txtIdleTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int time = Convert.ToInt32(txtIdleTime.Text);
                tcpServer1.IdleTime = time;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void txtMaxThreads_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int threads = Convert.ToInt32(txtMaxThreads.Text);
                tcpServer1.MaxCallbackThreads = threads;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void txtAttempts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int attempts = Convert.ToInt32(txtAttempts.Text);
                tcpServer1.MaxSendAttempts = attempts;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void txtValidateInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int interval = Convert.ToInt32(txtValidateInterval.Text);
                tcpServer1.VerifyConnectionInterval = interval;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }
    }
}
