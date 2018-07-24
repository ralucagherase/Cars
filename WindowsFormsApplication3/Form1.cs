using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {int rand = 16, col = 6;//nr randuri, coloane ale grilei corespunzatoare pistei
        int masinax = 0, masinay = 0;
        int pozmasina = 0;
        int[,] m = new int[17, 7]; //matricea de numere corespunzatoare pistei

        public Form1()
        {
            InitializeComponent();
        }
       
       private void Form1_Load(object sender, EventArgs e)
        {

        }
        void reset()
        {
            for (int i = 0; i < rand; i++)
                for (int j = 0; j < col; j++)
                    m[i, j] = 0;
        }

         private void Form1_Paint(object sender, PaintEventArgs e)
        { Graphics g = this.CreateGraphics();
            Pen penita = new Pen(Color.Red);
            //desenam pista de carting, o grila de dreptunghiuri, expusa pe 15 randuri si 6 coloane
             for (int i = 0; i < rand; i++)
                 for (int j = 0; j < col; j++)
                 {
                     g.DrawRectangle(penita, 50 + 30 * j, 30 + i * 30, 30, 30);
                     if (m[i, j] == 1)
                         g.FillRectangle(Brushes.Orange, 50 + 30 * j, 30 + 30 * i, 30, 30);
                     if (m[i, j] == 2)
                         g.FillRectangle(Brushes.Blue, 50 + 30 * j, 30 + 30 * i, 30, 30);
                 }


        }
        void verifica(int x, int y, int val)
        {
            if (x >= 0 && x < rand && y >= 0 && y < col)
                m[x, y] = val;
        }
        void deseneaza_masina(int x, int y, int val)
        {
            verifica(x, y + 1, val);
            verifica(x + 1, y, val);
            verifica(x + 1, y + 1, val);
            verifica(x + 1, y + 2, val);
            verifica(x + 2, y + 1, val);
            verifica(x + 3, y, val);
            verifica(x + 3, y + 1, val);
            verifica(x + 3, y + 2, val);
        }
       

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //deplasarea masinii la stanga sau dreapta in functie de pozitia ei-0 sau 3 pe orizontala
            if (e.KeyCode==Keys.Left) 
                pozmasina = 0;
            else
                if (e.KeyCode==Keys.Right)
                    pozmasina=3;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            reset();
            deseneaza_masina(12, pozmasina, 2);
            deseneaza_masina(masinax, masinay, 1);
            //Redesenarea controlului invalidat:
            Invalidate();
            masinax++;//masina se deplaseaza in jos
            if (masinax == rand)
            {
                masinax = 0;
                if (r.Next() % 2 == 0)
                    masinay = 0;
                else
                    masinay = 3;
            }
            finaljoc();
        }
        void finaljoc()
        {
            if (masinax>=9 && masinay==pozmasina)
                timer1.Enabled = false;
        }
    }
}
