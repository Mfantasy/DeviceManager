﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.CustomControl
{
    public partial class PASListView : ListView
    {
        public PASListView()
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
        }

        public PASListView(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
        }


    }
}
