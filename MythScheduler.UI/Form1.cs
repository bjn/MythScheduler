using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;

namespace MythScheduler.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.Click += new EventHandler(dataGridView1_Click);
            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseClick);
        }

        void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            object value = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
            Process.Start(value.ToString());
        }

        void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
