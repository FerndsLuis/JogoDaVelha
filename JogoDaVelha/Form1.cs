using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace JogoDaVelha
{
    public partial class Form1 : Form
    {
        System.Timers.Timer t;
        int h, m, s;

        int Xponto = 0, Oponto = 0, empate = 0, rodadas = 0;
        bool turno = true;
        bool jogo_final = true;
        string[] texto = new string[9];
       

        private void btnClean_Click(object sender, EventArgs e)
        {
            btn.Text = "";
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";

            rodadas = 0;           

            for(int x = 0; x < 9; x++)
            {
                texto[x] = "";
            }

            jogo_final = true;

        }


        private void btnIniciar_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < 9; x++)
            {
                texto[x] = "";
            }

            btn.Text = "";
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";

            rodadas = 0;

            t.Start();

            jogo_final = false;
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
        }

        //timer
        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (jogo_final)
                {
                    s = 0;
                    m = 0;
                    h = 0;
                }
                else
                {
                    s += 1;
                    if (s == 60)
                    {
                        s = 0;
                        m += 1;
                    }

                    if (m == 60)
                    {
                        m = 0;
                        h += 1;
                    }
                }

                lblTimer.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), 
                                                             m.ToString().PadLeft(2, '0'),
                                                             s.ToString().PadLeft(2, '0'));
            }));
        }

        public Form1()
        {
            InitializeComponent();
            lblQuemJoga.Text = "X";

            //timer
            t = new System.Timers.Timer();
            t.Interval = 1000; //1s
            t.Elapsed += OnTimeEvent;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int buttonIndex = btn.TabIndex;

            if (btn.Text == "" && jogo_final == false)
            {            
                if(turno)
                {
                    btn.Text = "X";
                    turno = !turno;
                    rodadas++;
                    texto[buttonIndex] = btn.Text;
                    Checagem(1);
                }
                else
                {
                    btn.Text = "O";
                    turno = !turno;
                    rodadas++;
                    texto[buttonIndex] = btn.Text;
                    Checagem(2);
                }
            }            
        }

        public void Vencedor(int PlayQueGanhou)
        {
            jogo_final = true;

            if (PlayQueGanhou == 1)
            {
                MessageBox.Show("Vencedor é: X");
                turno = true;
                Xponto++;
                Xpontos.Text = Convert.ToString(Xponto);              
            }
            else
            {
                MessageBox.Show("Vencedor é: O");
                turno = false;
                Oponto++;
                Opontos.Text = Convert.ToString(Oponto);
            }
        }

        public void Checagem(int ChecagemPlayer)
        {
            string suporte = "";

            if(ChecagemPlayer == 1)
            {
                suporte = "X";
                lblQuemJoga.Text = "O";
            }
            else
            {
                suporte = "O";
                lblQuemJoga.Text = "X";
            }

            for (int horizontal = 0; horizontal < 8; horizontal += 3)
            {
                if(suporte == texto[horizontal])
                {
                    if(texto[horizontal] == texto[horizontal + 1] && texto[horizontal] == texto[horizontal + 2])
                    {
                        Vencedor(ChecagemPlayer);
                        return;
                    }
                }
            }

            for (int vertical = 0; vertical < 3; vertical++)
            {
                if (suporte == texto[vertical])
                {
                    if (texto[vertical] == texto[vertical + 3] && texto[vertical] == texto[vertical + 6])
                    {
                        Vencedor(ChecagemPlayer);
                        return;
                    }
                }
            }

            //diagonal
            if (texto[0] == suporte)
            {
                if (texto[0] == texto[4] && texto[0] == texto[8])
                {
                    Vencedor(ChecagemPlayer);
                    return;
                }
            }


            if (texto[2] == suporte)
            {
                if (texto[2] == texto[4] && texto[2] == texto[6])
                {
                    Vencedor(ChecagemPlayer);
                    return;
                }
            }

            if(rodadas == 9 && jogo_final == false)
            {
                MessageBox.Show("Empate!!");
                empate++;
                empates.Text = Convert.ToString(empate);
                jogo_final = true;
                t.Stop();
                return;
            }
        }
    }
}
