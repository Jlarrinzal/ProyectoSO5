using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Cliente
{
    public partial class Form3 : Form
    {
        int Tiempo = 0;
        int seleccion,seleccion2;
        int ID_Partida;
        Socket server;
        delegate void DelegadochatLbl(string nombreMsg, string chat);

        public Form3(int ID_Partida, Socket server)
        {
            InitializeComponent();
            this.ID_Partida = ID_Partida;
            this.server = server;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            partidaIdLbl.Text = "Número de la partida: " + ID_Partida;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                timer1.Interval = 1000;
                Tiempo++;
                label3.Text = (Convert.ToInt32(Tiempo).ToString());
                
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            seleccion = 2;
            pictureBox4.Image = pictureBox2.Image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            seleccion = 1;
            pictureBox4.Image = pictureBox1.Image;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            seleccion = 3;
            pictureBox4.Image = pictureBox3.Image;
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            Random numero= new Random();
            seleccion2 = (numero.Next(1,4));
            if (seleccion2 == 1)
            {
                pictureBox5.Image = pictureBox1.Image;

            }
            if (seleccion2 == 2)
            {
                pictureBox5.Image = pictureBox2.Image;

            }
            if (seleccion2 == 3)
            {
                pictureBox5.Image = pictureBox3.Image;

            }
            
            //piedra =1 papel=2 tijera= 3
            if (seleccion == seleccion2)
                label5.Text = "Empate";
            if ((seleccion == 1 && seleccion2 == 3) || (seleccion == 2 && seleccion2 == 1) || (seleccion == 3 && seleccion2 == 2))
                label5.Text = "Has ganado";
            if((seleccion==1 && seleccion2 ==2)|| (seleccion==2 && seleccion2==3)||(seleccion==3 && seleccion2== 1))
                label5.Text = "Has perdido";
            
        }

        private void enviarBtn_Click(object sender, EventArgs e)
        {
            string mensaje = "8/" + Form1.N + "/" + ID_Partida + "/" + textBox1.Text;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            Form1.server.Send(msg);
        }

        public void tomaRespuesta(string[] respuesta)
        {
            switch (Convert.ToInt32(respuesta[0]))
            {
                case 9:
                    DelegadochatLbl delegado = new DelegadochatLbl(EscribirMensaje);
                    this.Invoke(delegado, new object[] { respuesta[1], respuesta[3] });
                    break;
            }
        }

        public void EscribirMensaje(string nombreMsg, string mensajeChat)
        {

            chatLbl.Text = chatLbl.Text + Environment.NewLine + nombreMsg + ": " + mensajeChat;
        }
    }
}
