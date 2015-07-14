using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockClock.core;

namespace BlockClock
{
    public partial class Form1 : Form
    {

        internal Status Status { get; private set; }
        private Clock _clock;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSize.SelectedIndex = 2;
            Status = Status.Stopped;
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            DetermineStatus();
        }

        private void DetermineStatus()
        {
            if (Status == Status.Stopped)
            {
                Status = Status.Started;
                Start();
            }
            else
            {
                Status = Status.Stopped;
                Stop();
            }
        }

        private void Stop()
        {
            btnToggle.Text = "Start";
            timer1.Stop();
        }

        private void Start()
        {
            btnToggle.Text = "Stop";
            const int secondsInMinute = 60;
            _clock = null;
            string item = (string) cmbSize.SelectedItem;
            int size = Convert.ToInt32(item);
            _clock = new Clock(size);

            timer1.Interval = size*secondsInMinute*1000;
            timer1.Start();
            ShowClock();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowClock();
        }

        private void ShowClock()
        {
            rtbClock.Text = _clock.ToString();
        }
    }

    enum Status
    {
        Started,
        Stopped
    }
}
