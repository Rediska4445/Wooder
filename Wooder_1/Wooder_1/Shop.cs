using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wooder_1
{
    public partial class Shop : Form
    {
        Form1 form;
        public Shop(Form1 form)
        {
            this.form = form;   
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form1().Visible = true;
            Hide();
        }
        /*        int step;
        public int money;
        int count;
        int clickerTreeCount; */
        private void button1_Click(object sender, EventArgs e)
        {
            if (form.money >= int.Parse(axe_price.Text))
            {
                form.Items.Add("a");
                form.money -= int.Parse(axe_price.Text);
                form.SetMoneyText("Money: " + form.money);
                form.axe_image.Visible = true;
                form.clickerTreeCount = 5;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (form.money >= int.Parse(saw0_price.Text))
            {
                form.Items.Add("s0");
                form.money -= int.Parse(saw0_price.Text);
                form.SetMoneyText("Money: " + form.money);
                form.saw_0_image.Visible = true;
                form.clickerWoodCount = 10;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (form.money >= int.Parse(saw_price.Text))
            {
                form.Items.Add("s");
                form.money -= int.Parse(saw_price.Text);
                form.SetMoneyText("Money: " + form.money);
                form.saw_image.Visible = true;
                form.clickerTreeCount = 2;
                form.clickerWoodCount = 6;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = form.money + " " + int.Parse(rubanok_price.Text);
            if (form.money >= int.Parse(rubanok_price.Text))
            {
                form.Items.Add("r");
                form.money -= int.Parse(rubanok_price.Text);
                form.SetMoneyText("Money: " + form.money);
                form.toSell += form.toSell;
                form.rubanok_image.Visible = true;
            }
        }

        private void Shop_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
