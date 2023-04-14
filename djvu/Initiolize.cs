using djvu.models;
using Newtonsoft.Json;
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
    public partial class Initiolize : Form
    {

        Setup setup = new Setup();
        Configs configs = new Configs();

        public Initiolize()
        {
            InitializeComponent();
            this.Load += Setup_Load;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Setup setup = new Setup();
            this.Hide();
            setup.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {
                label5.Visible = true;
                vscuipText.Visible = true;
            }
            else
            {
                label5.Visible = false;
                vscuipText.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 2) {

                
                Init(vscuipText.Text, textBox2.Text, textBox3.Text, textBox4.Text);


            }
            else
            {
                Template template = new Template();
                this.Hide();
                template.Show();
            }
           // setup.AddConnRecord(setup.dbname, comboBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, vscuipText.Text, null, null, DateTime.Now);

           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Setup_Load(object sender, EventArgs e)
        {
            if (setup.HasRecords(setup.dbname, "Conn"))
            {
                Template template = new Template();
                template.Shown += (s, args) => this.Hide();
                template.Show();
            }
        }


        public async void Init(string url, string tin, string bhfId, string dvcSrlNo)
        {
           

            var payload = new
            {
                tin ,
                bhfId ,
                dvcSrlNo
            };

            var json = JsonConvert.SerializeObject(payload);
            string urls = url + ":" + configs.initiolization_endponint;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(urls, new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<InitResponse>(responseBody);

                    if (result.resultCd == "000")
                    {
                        setup.AddConnRecord(setup.dbname, comboBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, vscuipText.Text, null, null, DateTime.Now);
                        MessageBox.Show("Innitiolization Successfull!", "Innitiolization Successfull",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Template template = new Template();
                        this.Hide();
                        template.Show();
                    }
                    else
                    {
                        MessageBox.Show(result.resultMsg, "Innitiolization Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: " + response.ReasonPhrase);
                }
            }
        }

    }
}
