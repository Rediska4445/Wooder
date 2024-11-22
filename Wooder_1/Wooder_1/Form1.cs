using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wooder_1.Properties;

namespace Wooder_1
{
    public partial class Form1 : Form
    {
        private Point locationsWoods;
        public List<PictureBox> woods;
        public int step;
        public int money;
        private int count;
        private int countWood;
        public int clickerTreeCount;
        public int clickerWoodCount;
        public int toSell;

        public int diaposone;

        private int woodWidth;
        private int woodHeight;

        public List<string> Items;
        public Form1()
        {
            InitializeComponent();
            diaposone = 50;
			woodWidth = 85;
            woodHeight = 87;
            locationsWoods = new Point(350, Background.Height - (woodHeight + 25));
            Items = new List<string>();
            woods = new List<PictureBox>();
            Items.Add("def");
            count = 0;
            toSell = 50;
            countWood = 0;
            clickerTreeCount = 7;
            clickerWoodCount = 12;
            step = 10;
            money = 100;
            Money.Text = "Money: " + money;
            Background.Controls.Add(Player);
            Background.Controls.Add(Tree);

            Thread kabans_generate = new Thread(() => {
                while (true) 
                {
                    Thread.Sleep(new Random().Next(1, 5) * 60_000);
					Entity kaban = new Entity(new PictureBox());

					kaban.pictureBox.Image = Resources.kaban__1_;
					kaban.pictureBox.Location = new Point(new Random().Next(150, Background.Width), new Random().Next(150, Background.Height));
					kaban.pictureBox.Cursor = Cursors.Hand;
					kaban.pictureBox.Size = new Size(Player.Width, Player.Height / 2);
					kaban.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
					kaban.pictureBox.BackColor = Color.Transparent;
					Background.Controls.Add(kaban.pictureBox);

					kaban.pictureBox.Click += (s, e) =>
					{
						Background.Controls.Remove(kaban.pictureBox);
						kaban.pictureBox.Dispose();
					};

					Thread svinka = new Thread(() =>
					{
						while (true)
						{
							Thread.Sleep(500);
							for (int i = 0; i < 25; i++)
							{
								Thread.Sleep(250);
								if (kaban.pictureBox.Location.X > 0 && kaban.pictureBox.Location.X < Background.Width - kaban.pictureBox.Width)
									kaban.SetLocation(kaban.pictureBox.Location.X + new Random().Next(0, step), kaban.pictureBox.Location.Y + new Random().Next(0, step));
							}
						}
					});
					svinka.Start();
				}
			});

			kabans_generate.Start();
		}

		public void SetMoneyText(string text)
        {
            Money.Text = text;
        }

        public void SetLocation(int x, int y)
        {
             Player.Location = new Point(x, y);
        }

        public void CutDownTree()
        {
            PictureBox wood = new PictureBox();

            wood.Image = Resources.wood;
            locationsWoods = new Point(locationsWoods.X + wood.Size.Width, locationsWoods.Y);
            wood.Location = locationsWoods;
            wood.BackColor = Color.Transparent;
            wood.Size = new Size(woodHeight, woodWidth);
            Background.Controls.Add(wood);
            wood.Cursor = Cursors.Hand;
            wood.Click += (s, e) => SellWood(wood);
            woods.Add(wood);
            Money.Text = "Money: " + money;
            Tree.Location = new Point(new Random().Next(285, 945), new Random().Next(165, 275));
        }

        public void SellWood(PictureBox wood)
        {
            Money.Text = Player.Location.ToString() + " " + wood.Location.ToString() + Player.Width + " " + Player.Height;
            countWood += 1;

            if(woods.Count == 0) locationsWoods = new Point(350 + wood.Width, 520 + wood.Height);

			for (int i = 0; i < woods.Count; i++)
            {
				if ((Player.Location.X + (Player.Width + diaposone) >= woods[i].Location.X
						&& Player.Location.X + (Player.Width - diaposone) <= woods[i].Location.X + wood.Width)
						&& (Player.Location.Y + (Player.Height + diaposone) >= woods[i].Location.Y
						&& Player.Location.Y + (Player.Height - diaposone) <= woods[i].Location.Y + wood.Height))
				{
                    money += toSell;
                    wood.Dispose();
                    locationsWoods = new Point(locationsWoods.X - wood.Size.Width, locationsWoods.Y);
                    countWood = 0;
                }
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            Money.Text = Player.Location.ToString();
            if (e.KeyCode == Keys.W)
            {
                if(Player.Location.Y > 0)  
                    SetLocation(Player.Location.X, Player.Location.Y - step);
            }
            if (e.KeyCode == Keys.A)
            {
                Player.Image = Properties.Resources.player_to__1_;
                if (Player.Location.X > 0)
                    SetLocation(Player.Location.X - step, Player.Location.Y);
            }
            if (e.KeyCode == Keys.S)
            {
                if (Player.Location.Y < Background.Height - 50)
                    SetLocation(Player.Location.X, Player.Location.Y + step);
            }
            if (e.KeyCode == Keys.D)
            {
                Player.Image = Properties.Resources.player_to;
                if (Player.Location.X < Background.Width - 150)
                    SetLocation(Player.Location.X + step, Player.Location.Y);
            }
        }

        private void Tree_MouseEnter(object sender, EventArgs e)
        {
            Tree.Cursor = Cursors.Hand;
        }

        private void Tree_Click(object sender, EventArgs e)
        {
            count += 1;
			if (count >= clickerTreeCount && (Player.Location.X + (Player.Width + diaposone) >= Tree.Location.X
					&& Player.Location.X + (Player.Width - diaposone) <= Tree.Location.X + Tree.Width)
					&& (Player.Location.Y + (Player.Height + diaposone) >= Tree.Location.Y
					&& Player.Location.Y + (Player.Height - diaposone) <= Tree.Location.Y + Tree.Height))
			{
				CutDownTree();
				count = 0;
			}
        }

        private void shop_Click(object sender, EventArgs e)
        {
            Shop shop = new Shop(this);
            shop.Show();
            Visible = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Background.Image = Properties.Resources.back_2;
        }
    }

    public class Entity
    {
        public PictureBox pictureBox;
        public Point Location;

        public Entity(PictureBox Entity)
        {
            pictureBox = Entity;
            Location = new Point(0, 0);
        }

        public void Remove()
        {
            pictureBox = null;
        }

        public void SetLocation(int x, int y)
        {
            pictureBox.Location = new Point(x, y);
        }

    }
}
