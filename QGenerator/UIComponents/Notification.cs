using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QGenerator.UIComponents
{
    public partial class Notification : Form
    {
        public string MessageText;

        public Notification(string messageText)
        {
            InitializeComponent();
            MessageText = messageText;
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = MessageText;
        }
    }
}
