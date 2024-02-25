using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Уровень_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pole = Start(pole);
            Read(pole);
            textBox1.Text = "Игрок 1:   " + pobed1;
            textBox2.Text = "Игрок 2:   " + pobed2;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        static int[,] pole =new int[3, 3];
        int x;
        int y;
        int step = 0;
        int pobed1 = 0;
        int pobed2 = 0;
        string s;
        int n = 0;
            public void Read(int[,] pole)
            {
                StreamReader sr = new StreamReader("История.txt");
                string[] s = sr.ReadLine().Split(" ");
            if (s[s.Length - 1] != "end")
            {
                pobed1 =Int32.Parse(s[s.Length - 3]);
                pobed2 = Int32.Parse(s[s.Length - 2]);
                step = Int32.Parse(s[s.Length - 1]);
                for (int i = 0; i < step; i++)
                {
                    pole[Int32.Parse(s[s.Length - 6 - i * 3]), Int32.Parse(s[s.Length - 5 - i * 3])] =Convert.ToInt32(s[s.Length - 4 - i * 3]);
                }
            }      
                sr.Close();
            }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 1; i < 3; i++)
            {
                g.DrawLine(new Pen(Color.White, 2.0f), 0, 120 * i, 360, 120 * i);
                g.DrawLine(new Pen(Color.White, 2.0f), 120 * i, 0, 120 * i, 360);
            }
            xod(e);
        }

        public static int[,] Start(int[,] pole)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    pole[i, j] = 0;
            return (pole);
        }

        private void xod(PaintEventArgs e)
        {
            if (n != 0)
            {
                if (pole[x, y] == 0)
                {
                    if (step % 2 == 1)

                        pole[x, y] = 1;
                    else
                        pole[x, y] = 2;
                }
                else
                    step = step - 1;
                s = s + " " + x + " " + y + " " + pole[x, y];
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (pole[i, j] == 1)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Red, 4.0f), 20 + i * 120, 20 + j * 120, 100 + i * 120, 100 + j * 120);
                        e.Graphics.DrawLine(new Pen(Color.Red, 4.0f), 100 + i * 120, 20 + j * 120, 20 + i * 120, 100 + j * 120);
                    }
                    if (pole[i, j] == 2)
                    {
                        e.Graphics.DrawEllipse(new Pen(Color.Blue, 4.0f), 24 * (1 + i * 5), 24 * (1 + j * 5), 70, 80);
                    }
                }
            }
            end(e);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            step++;
            n++;
            int clx = e.Location.X;
            int cly = e.Location.Y;
            for (int i = 0; i < 3; i++)
            {
                if ((i * 120 < cly) & (cly < (i + 1) * 120))
                {
                    y = i;
                }
                if ((i * 120 < clx) & (clx < (i + 1) * 120))
                {
                    x = i;
                }
                panel1.Invalidate();
            }
        }
        private void end(PaintEventArgs e)
        {
            bool nichya=true;
            for (int i = 0; i < 3; i++)
            {
                if ((pole[i, 0] == pole[i, 1]) & (pole[i, 1] == pole[i, 2]) & (pole[i, 0] != 0) |
                    (pole[0, i] == pole[1, i]) & (pole[1, i] == pole[2, i]) & (pole[0, i] != 0))
                {
                    if ((pole[i, 0] == pole[i, 1]) & (pole[i, 1] == pole[i, 2]))
                        e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 60 + i * 120, 10, 60 + i * 120, 350);
                    else
                        e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 10, 60 + i * 120, 350, 60 + i * 120);
                    s = s + " " +pobed1+" "+pobed2+" "+ "end";
                    nichya = false;
                    if ((pole[i, 0] == 1) & (pole[i, 0] == pole[i, 1]) & (pole[i, 1] == pole[i, 2]) |
                    (pole[0, i] == 1) & (pole[0, i] == pole[1, i]) & (pole[1, i] == pole[2, i]))
                    {
                        MessageBox.Show("Победил игрок 1!");
                        pobed1++;
                    }
                    else
                    {
                        MessageBox.Show("Победил игрок 2!");
                        pobed2++;
                    }
                }
            }
            
            if ((pole[0, 0] == pole[1, 1]) & (pole[1, 1] == pole[2, 2])& (pole[0, 0] !=0))
            {
                e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 10, 10, 350, 350);
                s = s + " " + pobed1 + " " + pobed2 + " " + "end";
                nichya = false;
                if (pole[1, 1] == 1)
                {
                    pobed1++;
                    MessageBox.Show("Победил игрок 1!");
                }
                if (pole[1, 1] == 2)
                {
                    pobed2++;
                    MessageBox.Show("Победил игрок 2!");
                }
            }
            
            if ((pole[0, 2] == pole[1, 1]) & (pole[1, 1] == pole[2, 0])&(pole[0, 2] !=0))
            {
                e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 350, 10, 10, 350);
                s = s + " " + pobed1 + " " + pobed2 + " " + "end";
                nichya = false;
                if (pole[1, 1] == 1)
                {
                    pobed1++;
                    MessageBox.Show("Победил игрок 1!");
                }
                if (pole[1, 1] == 2)
                {
                    pobed2++;
                    MessageBox.Show("Победил игрок 2!");
                }
            }
            
            if ((step == 9)&(nichya == true))
            {
                MessageBox.Show("Ничья!");
                s = s + " " + pobed1 + " " + pobed2 + " " + "end";
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pole = Start(pole);
            textBox1.Text = "Игрок 1:   "+Convert.ToString(pobed1);
            textBox2.Text = "Игрок 2:   " + Convert.ToString(pobed2);
            step = 0;
            n = 0;
            panel1.Invalidate();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pole = Start(pole);
            textBox1.Text = "Игрок 1:   0";
            textBox2.Text = "Игрок 2:   0";
            pobed1 = 0;
            pobed2 = 0;
            step = 0;
            n = 0;
            panel1.Invalidate();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter sw = new StreamWriter("История.txt");
            if (s == null)
            {
                s = "end";
            }
            string[] S = s.Split(" ");
            if (S[S.Length - 1] != "end")
                s = s + " "+pobed1+" "+pobed2+" " + step;
            sw.WriteLine(s);
            sw.Close();
        }
    }
}
