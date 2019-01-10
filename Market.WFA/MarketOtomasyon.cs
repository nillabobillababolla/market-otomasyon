﻿using Market.BLL.Repository;
using Market.DAL;
using Market.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market.WFA
{
    public partial class MarketOtomasyon : Form
    {
        public MarketOtomasyon()
        {
            InitializeComponent();
        }
        private Satis satisForm;
        private MalKabul MalKabulForm;
        private CRUD CrudIslemleriForm;

        private void satışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (satisForm == null || satisForm.IsDisposed)
            {
                satisForm = new Satis();
            }
            satisForm.MdiParent = this;
            satisForm.Show();
        }

        private void malGirişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MalKabulForm == null || MalKabulForm.IsDisposed)
            {
                MalKabulForm = new MalKabul();
            }
            MalKabulForm.MdiParent = this;
            MalKabulForm.Show();
        }

        private void MarketOtomasyon_Load(object sender, EventArgs e)
        {
        }

        private void işlemlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CrudIslemleriForm == null || CrudIslemleriForm.IsDisposed)
            {
                CrudIslemleriForm = new CRUD();
            }
            CrudIslemleriForm.MdiParent = this;
            CrudIslemleriForm.Show();
        }
    }
}
