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

namespace Уровень_2
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
        static int[,] pole = new int[10, 10];
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
                pobed1 = Int32.Parse(s[s.Length - 3]);
                pobed2 = Int32.Parse(s[s.Length - 2]);
                step = Int32.Parse(s[s.Length - 1]);
                for (int i = 0; i < step; i++)
                {
                    pole[Int32.Parse(s[s.Length - 6 - i * 3]), Int32.Parse(s[s.Length - 5 - i * 3])] = Convert.ToInt32(s[s.Length - 4 - i * 3]);
                }
            }
            sr.Close();
        }
        public static int[,] Start(int[,] pole)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    pole[i, j] = 0;
            return (pole);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 1; i < 10; i++)
            {
                g.DrawLine(new Pen(Color.White, 2.0f), 0, 60 * i, 600, 60 * i);
                g.DrawLine(new Pen(Color.White, 2.0f), 60 * i, 0, 60 * i, 600); 
            }
            xod(e);
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

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (pole[i, j] == 1)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Red, 4.0f), 10 + i * 60, 10 + j * 60, 50 + i * 60, 50 + j * 60);
                        e.Graphics.DrawLine(new Pen(Color.Red, 4.0f), 50 + i * 60, 10 + j * 60, 10 + i * 60, 50 + j * 60);
                    }
                    if (pole[i, j] == 2)
                    {
                        e.Graphics.DrawEllipse(new Pen(Color.Blue, 4.0f), 12 * (1 + i * 5), 12 * (1 + j * 5), 35, 40);
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
            for (int i = 0; i < 10; i++)
            {
                if ((i * 60 < cly) & (cly < (i + 1) * 60))
                {
                    y = i;
                }
                if ((i * 60 < clx) & (clx < (i + 1) * 60))
                {
                    x = i;
                }
                panel1.Invalidate();
            }
        }
        private void end(PaintEventArgs e)
        {
            bool nichya = true;
            int n1 = 0;
            int n2 = 0;
            int poleX1 = 0;
            int poleY1 = 0;
            int poleX2 = 0;
            int poleY2 = 0;
            for (int i = -4; i < 4; i++)
            {
                if (pole[x, y] != 0)
                {
                    if (((x + i) >= 0) & ((x + i) < 9))
                    {
                        if ((pole[x + i, y] == pole[x + i + 1, y]))
                        {
                            n1++;
                            poleX1 = x + i + 1;
                            poleY1 = y;
                        }
                        else
                            n1 = 0;
                    }

                    if (((y + i) >= 0) & ((y + i) < 9))
                    {
                        if (pole[x, y + i] == pole[x, y + i + 1])
                        {
                            n2++;
                            poleX2 = x;
                            poleY2 = y + i + 1;
                        }
                        else
                            n2 = 0;
                    }
                }
                if ((n1 == 4) | (n2 == 4))
                {

                    if (n1 == 4)
                        e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 10 + (poleX1-4) * 60, 30 + (poleY1) * 60, 290 + (poleX1-4) * 60, 30 + (poleY1) * 60);

                    if (n2 == 4)
                        e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 30 + poleX2 * 60, 10 + (poleY2-4) * 60, 30 + poleX2 * 60, 290 + (poleY2-4) * 60);
                    nichya = false;
                    if (pole[x, y] == 1)
                    {
                        MessageBox.Show("Победил игрок 1!");
                        pobed1++;
                    }
                    else
                    {
                        MessageBox.Show("Победил игрок 2!");
                        pobed2++;
                    }
                    s = s + " " + pobed1 + " " + pobed2 + " " + "end";
                    break;
                }
            }
            n1 = 0;
            n2 = 0;
            poleX1 = 0;
            poleY1 = 0;
            poleX2 = 0;
            poleY2 = 0;
            for (int i = -4; i < 4; i++)
            {
                if (pole[x, y] != 0)
                {
                    if (((x + i) >= 0) & ((x + i) < 9) & ((y + i) >= 0) & ((y + i) < 9))
                    {
                        if ((pole[x + i, y + i] == pole[x + i + 1, y + i + 1]))
                        {
                            n1++;
                            poleX1 = x + i + 1;
                            poleY1 = y + i + 1;
                        }
                        else
                            n1 = 0;
                    }

                    if (((x - i) > 0) & ((x - i) <= 9) & ((y + i) >= 0) & ((y + i) < 9))
                    {
                        if (pole[x - i, y + i] == pole[x - i - 1, y + i + 1])
                        {
                            n2++;
                            poleX2 = x - i - 1;
                            poleY2 = y + i + 1;
                        }
                        else
                            n2 = 0;
                    }
                }
                if ((n1 == 4) | (n2 == 4))
                {

                    if (n1 == 4)
                        e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 10 + (poleX1 - 4) * 60, 10 + 60 * (poleY1 - 4),
                        290 + (poleX1 - 4) * 60, 290 + 60 * (poleY1 - 4));
                    if (n2 == 4)
                        e.Graphics.DrawLine(new Pen(Color.Green, 8.0f), 290 + (poleX2) * 60, 10 + 60 * (poleY2 - 4),
                        10 + (poleX2) * 60, 290 + 60 * (poleY2 - 4));
                    nichya = false;
                    if (pole[x, y] == 1)
                    {
                        MessageBox.Show("Победил игрок 1!");
                        pobed1++;
                    }
                    else
                    {
                        MessageBox.Show("Победил игрок 2!");
                        pobed2++;
                    }
                    s = s + " " + pobed1 + " " + pobed2 + " " + "end";
                    break;
                }
            }

            if ((step == 100) & (nichya == true))
            {
                MessageBox.Show("Ничья!");
                s = s + " " + pobed1 + " " + pobed2 + " " + "end";
            }
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
        private void button2_Click(object sender, EventArgs e)
        {
            pole = Start(pole);
            textBox1.Text = "Игрок 1:   " + Convert.ToString(pobed1);
            textBox2.Text = "Игрок 2:   " + Convert.ToString(pobed2);
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
                s = s + " " + pobed1 + " " + pobed2 + " " + step;
            sw.WriteLine(s);
            sw.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}