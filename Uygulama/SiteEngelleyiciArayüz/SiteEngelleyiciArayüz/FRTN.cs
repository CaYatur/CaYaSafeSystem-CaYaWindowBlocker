using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteEngelleyiciArayüz
{
    public partial class FRTN : Form
    {
        private static FRTN instance3;

        public static FRTN Instance3
        {
            get
            {
                if (instance3 == null || instance3.IsDisposed)
                {
                    instance3 = new FRTN();
                }
                return instance3;
            }
        }

        public FRTN()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void FRTN_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}
