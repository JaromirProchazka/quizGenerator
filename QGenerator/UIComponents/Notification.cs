﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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

        /// <summary>
        /// A funtion that creates a notification given a message.
        /// </summary>
        /// <param name="messageText">A message that is shown in the notification.</param>
        public static void Notice(string messageText)
        {
            var notice = new Notification(messageText);
            notice.Show();
        }
    }
}
