using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockClock
{
    public partial class Form1 : Form
    {

        internal Status Status { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSize.SelectedIndex = 2;
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (Status == Status.Stopped)
            {
                Status = Status.Started;
            }
            else 
            {
                Status = Status.Stopped;
            }
        }
    }

    enum Status
    {
        Started,
        Stopped
    }
}
