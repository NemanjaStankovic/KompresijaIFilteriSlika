using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMS
{
    public partial class ContrastPom : Form
    {

        public int ConTxt
        {
            get
            {
                return (Convert.ToInt32(contTxt.Text, 10));
            }
            set { contTxt.Text = value.ToString(); }
        }
        public ContrastPom()
        {
            InitializeComponent();

            OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void contTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void OK_Click(object sender, EventArgs e)
        {

        }
    }
}
