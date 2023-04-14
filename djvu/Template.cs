using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace djvu
{
    public partial class Template : Form
    {
        Setup setup = new Setup();
        public Template()
        {
            InitializeComponent();
            this.Load += Setup_Load;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Initiolize initiolize = new Initiolize();
            this.Hide();
            initiolize.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Initiolize initiolize = new Initiolize();
            this.Hide();
            initiolize.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setup.UpdateTempAndPsize(setup.dbname, 1, comboBox1.Text, comboBox2.Text);

            Srule form1 = new Srule();
            this.Hide();
            form1.Show();
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            if (setup.TempAndPsizeHaveValues(setup.dbname, 1))
            {
                Srule form1 = new Srule();
                form1.Shown += (s, args) => this.Hide();
                form1.Show();
            }
        }
    }
}
