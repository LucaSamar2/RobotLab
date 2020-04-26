using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : MetroForm
    {
        Form2 f = new Form2();
        List<Programma> Linee = new List<Programma>();
        List<Punti3D> Punti = new List<Punti3D>();
        List<int> tempi = new List<int>();
        Punti3D UltimoPunto = new Punti3D();
        Punti3D PuntoIniziale = new Punti3D();

        double[] coordinate = new double[6];
        double[] Posizione = new double[6];
        double[] Zero = new double[6];
        int spessore = 3;
        double delta = 10;
        public int numeroPunti = 0;

        public Form1(string arg)
        {
            InitializeComponent();
            this.caricamento(arg);
        }
        public Form1()
        {
            InitializeComponent();
         
        }

        private void caricamento(string arg)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(arg))
            {
                Punti.Clear();
                for (int i = 0; i < this.numeroPunti; i++) dataGridView1.Rows.RemoveAt(0);
                this.numeroPunti = 0;

                Matematica Cinematica = new Matematica();
                double[] giunti = new double[6];

                giunti[0] = 0;
                giunti[1] = 90;
                giunti[2] = 90;
                giunti[3] = 0;
                giunti[4] = 20;
                giunti[5] = 0;  
                Robot.g1 = giunti[0];
                Robot.g2 = giunti[1];
                Robot.g3 = giunti[2];
                Robot.g4 = giunti[3];
                Robot.g5 = giunti[4];
                Robot.g6 = giunti[5];
                Cinematica.diretta(giunti[0], giunti[1], giunti[2], giunti[3], giunti[4], giunti[5], ref Posizione);
                Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
                userControl11.refresh();
                metroTextBox1.Text = 2.ToString();
                metroTextBox2.Text = 2.ToString();
                metroTextBox3.Text = 0.ToString();

                Punti3D punto = new Punti3D(this.Posizione);
                double[] arr = new double[6];
                arr = Punti3D.P2Arr(punto);
                PuntoIniziale = punto;
                for (int i = 0; i < 6; i++) dataGridView1.Rows[0].Cells[i].Value = (int)this.Posizione[i];

                double[] buffer = new double[6];
                double[] Pbuffer = new double[5];
                string linea;

                while ((linea = sr.ReadLine()) != null)
                {
                    Char divisore = ':';
                    String[] valori = linea.Split(divisore);
                    int cont = 0;
                    if (valori[0] == "i")
                    {
                        Punti3D nuovo = new Punti3D();
                        this.dataGridView1.Rows.Add();

                        for (int i = 0; i < 9; i++)
                        {
                            nuovo._default[i] = Convert.ToInt32(valori[i + 1]);
                            nuovo.Pin1[i] = Convert.ToInt32(valori[i + 1 + 9]);
                            nuovo.Pin2[i] = Convert.ToInt32(valori[i + 1 + 9 + 9]);                 // ogni 9 valori indico un parametro diverso
                            nuovo.Pin3[i] = Convert.ToInt32(valori[i + 1 + 9 + 9 + 9]);
                            nuovo.Pin4[i] = Convert.ToInt32(valori[i + 1 + 9 + 9 + 9 + 9]);
                            nuovo.Pin5[i] = Convert.ToInt32(valori[i + 1 + 9 + 9 + 9 + 9 + 9]);
                        }

                        nuovo.contatore[0] = Convert.ToInt32(valori[55]);
                        nuovo.contatore[1] = Convert.ToInt32(valori[56]);
                        nuovo.tipo = 3;
                        Punti.Add(nuovo);

                    }
                    else
                    {
                        foreach (var substring in valori)
                        {
                            if (cont < 6) buffer[cont] = Convert.ToDouble(substring);
                            else Pbuffer[cont - 6] = Convert.ToDouble(substring);
                            cont++;
                        }
                        this.dataGridView1.Rows.Add();
                        Punti.Add(Punti3D.Arr2P(buffer));
                        Punti[numeroPunti].vel = Pbuffer[0];
                        Punti[numeroPunti].acc = Pbuffer[1];
                        Punti[numeroPunti].tool = Pbuffer[2];
                        Punti[numeroPunti].tipo = (int)Pbuffer[3];
                        Punti[numeroPunti].tempo = (int)Pbuffer[4];
                    }
                    this.numeroPunti++;
                }
                RefreshTabella();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            f.Hide();
            base.OnLoad(e);

            double[] motori = new double[6];

            List<double[]> C = new List<double[]>();

            Matematica Cinematica = new Matematica();
            double[] giunti = new double[6];

            giunti[0] = 0;
            giunti[1] = 90;
            giunti[2] = 90;
            giunti[3] = 0;
            giunti[4] = 20;
            giunti[5] = 0;
            Robot.g1 = giunti[0];
            Robot.g2 = giunti[1];
            Robot.g3 = giunti[2];
            Robot.g4 = giunti[3];
            Robot.g5 = giunti[4];
            Robot.g6 = giunti[5];
            Cinematica.diretta(giunti[0], giunti[1], giunti[2], giunti[3], giunti[4], giunti[5], ref Posizione);
            Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
            userControl11.refresh();
            metroTextBox1.Text = 2.ToString();
            metroTextBox2.Text = 2.ToString();
            metroTextBox3.Text = 0.ToString();

            Punti3D punto = new Punti3D(this.Posizione);
            double[] arr = new double[6];
            arr = Punti3D.P2Arr(punto);
            PuntoIniziale = punto;
            for (int i = 0; i < 6; i++) dataGridView1.Rows[0].Cells[i].Value = (int)this.Posizione[i];

            for (int i = 0; i < 6; i++) this.Zero[i] = this.Posizione[i];
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            metroTextBox4.Text = ((int)Posizione[0]).ToString();
            metroTextBox5.Text = ((int)Posizione[1]).ToString();
            metroTextBox8.Text = ((int)Posizione[2]).ToString();
            metroTextBox7.Text = ((int)Posizione[3]).ToString();
            metroTextBox6.Text = ((int)Posizione[4]).ToString();
            metroTextBox9.Text = ((int)Posizione[5]).ToString();

            metroTextBox10.Text = this.delta.ToString();

        }

        private void spostamentoC(int a, double shift)
        {
            double[] Pos = new double[6];
            for (int i = 0; i < 6; i++) Pos[i] = this.Posizione[i];
            try
            {
                Pos[a - 1] += shift;
                Matematica Cinematica = new Matematica();
                double[] c = new double[6];
                double[] d = new double[6];
               
                Cinematica.inversa(Pos, ref c, ref d);
                //Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
                Posizione = Pos;
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                Robot.g1 = d[0];
                Robot.g2 = d[1];
                Robot.g3 = d[2];
                Robot.g4 = d[3];
                Robot.g5 = d[4];
                Robot.g6 = d[5];
                Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
                userControl11.refresh();
                metroTextBox4.Text = ((int)Posizione[0]).ToString();
                metroTextBox5.Text = ((int)Posizione[1]).ToString();
                metroTextBox8.Text = ((int)Posizione[2]).ToString();
                metroTextBox7.Text = ((int)Posizione[3]).ToString();
                metroTextBox6.Text = ((int)Posizione[4]).ToString();
                metroTextBox9.Text = ((int)Posizione[5]).ToString();
            }
            catch (EccezioneMatematica) { }

        }
        private void spostamentoJ(int a, double shift)
        {
            double[] Pos = new double[6];
            for (int i = 0; i < 6; i++) Pos[i] = this.Posizione[i];
            try
            {
                Matematica Cinematica = new Matematica();
                double[] c = new double[6];
                double[] d = new double[6];
                //Cinematica.inversa(Pos, ref c, ref d);
                d[0] = Robot.g1;
                d[1] = Robot.g2;
                d[2] = Robot.g3;
                d[3] = Robot.g4;
                d[4] = Robot.g5;
                d[5] = Robot.g6;

                d[a - 1] += shift;

                Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
                Robot.g1 = d[0];
                Robot.g2 = d[1];
                Robot.g3 = d[2];
                Robot.g4 = d[3];
                Robot.g5 = d[4];
                Robot.g6 = d[5];
                Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
                userControl11.refresh();
                pictureBox1.Refresh();
                pictureBox2.Refresh();

                metroTextBox4.Text = ((int)Posizione[0]).ToString();
                metroTextBox5.Text = ((int)Posizione[1]).ToString();
                metroTextBox8.Text = ((int)Posizione[2]).ToString();
                metroTextBox7.Text = ((int)Posizione[3]).ToString();
                metroTextBox6.Text = ((int)Posizione[4]).ToString();
                metroTextBox9.Text = ((int)Posizione[5]).ToString();
            }
            catch (EccezioneMatematica) { }
        }

        private void spostamentoT(int a, double shift)
        {
            double[] Pos = new double[6];
            for (int i = 0; i < 6; i++) Pos[i] = this.Posizione[i];
            try
            {
                Matematica Cinematica = new Matematica();
                double[] c = new double[6];
                double[] d = new double[6];
                double[] RPY = new double[3];

                RPY[0] = Pos[3] * 0.01745329251994;
                RPY[1] = Pos[4] * 0.01745329251994;
                RPY[2] = Pos[5] * 0.01745329251994;

                double[,] R = new double[3, 3];

                double cos_rollψ = Math.Cos(RPY[0]);   // ψ               // Seni e coseni
                double cos_pitchθ = Math.Cos(RPY[1]); // θ
                double cos_yawφ = Math.Cos(RPY[2]);     // φ

                double sen_rollψ = Math.Sin(RPY[0]);   // ψ
                double sen_pitchθ = Math.Sin(RPY[1]); // θ
                double sen_yawφ = Math.Sin(RPY[2]);     // φ

                R[0, 0] = cos_yawφ * cos_pitchθ;
                R[0, 1] = (cos_yawφ * sen_pitchθ * sen_rollψ) - (sen_yawφ * cos_rollψ);          // matrice di rotazione
                R[0, 2] = (cos_yawφ * sen_pitchθ * cos_rollψ) + (sen_yawφ * sen_rollψ);
                R[1, 0] = sen_yawφ * cos_pitchθ;
                R[1, 1] = (sen_pitchθ * sen_yawφ * sen_rollψ) + (cos_yawφ * cos_rollψ);
                R[1, 2] = (sen_yawφ * sen_pitchθ * cos_rollψ) - (cos_yawφ * sen_rollψ);
                R[2, 0] = -sen_pitchθ;
                R[2, 1] = cos_pitchθ * sen_rollψ;
                R[2, 2] = cos_pitchθ * cos_rollψ;

                Pos[0] += R[0, a - 1] * shift;
                Pos[1] += R[1, a - 1] * shift;
                Pos[2] += R[2, a - 1] * shift;

                Cinematica.inversa(Pos, ref c, ref d);
                //Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
                pictureBox1.Refresh();
                pictureBox2.Refresh();

                Robot.g1 = d[0];
                Robot.g2 = d[1];
                Robot.g3 = d[2];
                Robot.g4 = d[3];
                Robot.g5 = d[4];
                Robot.g6 = d[5];
                Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
                userControl11.refresh();
                metroTextBox4.Text = ((int)Posizione[0]).ToString();
                metroTextBox5.Text = ((int)Posizione[1]).ToString();
                metroTextBox8.Text = ((int)Posizione[2]).ToString();
                metroTextBox7.Text = ((int)Posizione[3]).ToString();
                metroTextBox6.Text = ((int)Posizione[4]).ToString();
                metroTextBox9.Text = ((int)Posizione[5]).ToString();
            }
            catch (EccezioneMatematica) { }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            int x0 = 60;
            int y0 = pictureBox1.Height - 50;

            Pen pen1 = new Pen(Brushes.Green);
            Pen pen2 = new Pen(Brushes.Brown);
            Pen pen3 = new Pen(Brushes.Blue);
            Pen pen4 = new Pen(Brushes.Red);
            Pen pen5 = new Pen(Brushes.Black);
            Pen pen6 = new Pen(Brushes.Black);

            int Ascala = Robot.scala;

            pen1.Width = spessore;
            pen2.Width = spessore;
            pen3.Width = spessore;
            pen4.Width = spessore;
            pen5.Width = spessore;
            pen6.Width = spessore;

            e.Graphics.DrawLine(pen1, x0, y0, (int)Robot.T01[0, 3] / Ascala + x0, -(int)Robot.T01[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen2, (int)Robot.T01[0, 3] / Ascala + x0, -(int)Robot.T01[2, 3] / Ascala + y0, (int)Robot.T02[0, 3] / Ascala + x0, -(int)Robot.T02[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen3, (int)Robot.T02[0, 3] / Ascala + x0, -(int)Robot.T02[2, 3] / Ascala + y0, (int)Robot.T03[0, 3] / Ascala + x0, -(int)Robot.T03[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen4, (int)Robot.T03[0, 3] / Ascala + x0, -(int)Robot.T03[2, 3] / Ascala + y0, (int)Robot.T04[0, 3] / Ascala + x0, -(int)Robot.T04[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen5, (int)Robot.T04[0, 3] / Ascala + x0, -(int)Robot.T04[2, 3] / Ascala + y0, (int)Robot.T06[0, 3] / Ascala + x0, -(int)Robot.T06[2, 3] / Ascala + y0);

        }

        public double[] C = new double[6];
        public double[] giunti = new double[6];
        public int cont = 0;
        public int cont2 = 0;
        int p = 0;
        bool tipoIstruzione = false;
        bool OLDtipoIstruzione = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
          
            if (cont < numeroPunti)
            {
                if (Linee[cont].ist)
                {
                    tipoIstruzione = true;
                    if(cont == 0)
                    {
                        for (int i = 0; i < 6; i++) C[i] = this.Posizione[i];
                    }
                    if(!RobotEnabled.Checked)
                    {
                        try
                        {
                            serialPort1.Open();
                            RobotEnabled.Checked = true;
                        }catch(Exception)
                        {
                            stop();
                            MessageBox.Show("Porta USB scollegata");
                        }
                    }
                    comunicazione();
                }
                else
                {
                    tipoIstruzione = false;
                    if (cont2 < Linee[cont].numStep)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            C[i] = Linee[cont].P[cont2][i];
                        }
                        cont2++;
                        if (Linee[cont].tempo == 0) p++;
                    }
                    else
                    {
                        cont2 = 0;
                        cont++;
                    }
                }
                try
                {
                    dataGridView1.Rows[cont].Selected = true;
                }catch(Exception)
                {
                    cont = numeroPunti;
                    MessageBox.Show("Istruzione oltre al limite");
                }
                dataGridView1.Rows[cont].Selected = true;
            }
            else
            {
                if (Loop)
                {
                    cont = 0;
                }
                else
                {
                    for (int i = 0; i < 6; i++) this.Posizione[i] = C[i];
                    serialPort1.Close();
                    timer1.Stop();
                    abilitazioneTasti(true);
                    metroProgressBar1.Visible = false;
                    try
                    {
                        dataGridView1.Rows[cont].Selected = false;
                    }catch(Exception)
                    { 
                        cont = numeroPunti;
                        dataGridView1.Rows[cont].Selected = false;
                        MessageBox.Show("Istruzione oltre al limite");
                    }
                }
                p = 0;
            }

            metroProgressBar1.Maximum = progres;
            metroProgressBar1.Value = p;

            double[] a = new double[6];

            Matematica Cinematica = new Matematica();

            Cinematica.inversa(C, ref a, ref giunti);
            Cinematica.diretta(giunti[0], giunti[1], giunti[2], giunti[3], giunti[4], giunti[5], ref a);
            Robot.g1 = giunti[0];
            Robot.g2 = giunti[1];
            Robot.g3 = giunti[2];
            Robot.g4 = giunti[3];
            Robot.g5 = giunti[4];
            Robot.g6 = giunti[5];
            Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
            userControl11.refresh();
            try
            {
                if (this.R && serialPort1.IsOpen && ricevuto == true && tipoIstruzione == OLDtipoIstruzione)
                    serialPort1.WriteLine(((int)Robot.g1 + 90).ToString() + ':' + (180 - (int)Robot.g2).ToString()
                + ':' + (-(int)Robot.ga1 - 90).ToString() + ':' + ((int)giunti[3]+90).ToString() + ':' + (200-(int)giunti[4]-90).ToString() + ':' + ((int)giunti[5]+90).ToString() );
                else if (!tipoIstruzione && OLDtipoIstruzione && serialPort1.IsOpen)
                {
                    Thread.Sleep(10);
                    serialPort1.Write("R"+uscite);
                }
            }
            catch (Exception)
            {
                RobotEnabled.Checked = false;
                MessageBox.Show("Porta USB scollegata");
            }
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            OLDtipoIstruzione = tipoIstruzione;
        }
        bool ricevuto = true;
        String uscite = "00000";
        private void comunicazione()
        {
            
            if (serialPort1.IsOpen)
                try
                {
                    ricevuto = false;
                    serialPort1.WriteLine("R"+uscite);
                }            
                catch (Exception) { MessageBox.Show("Porta USB scollegata"); RobotEnabled.Checked = false; }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            ricevuto = true;
            if(cont >= numeroPunti||cont <0)
            { }
            else
            {
                if ((Linee[cont].conteggio == Linee[cont].contatore[1]) && Linee[cont].contatore[0] == 1)
                {
                    Linee[cont].conteggio = 0;
                    cont++;
                }
                else
                {
                    if (Linee[cont].contatore[0] == 1) Linee[cont].conteggio++;
                    String ingressi = serialPort1.ReadLine();
                    uscite = "";
                    if (Linee[cont].ist && ingressi.Length == 6)
                    {

                        if (Linee[cont].Pin1[0] == 1 && ingressi[0] == '1')
                        {
                            if (Linee[cont].Pin1[3] == 1) uscite += Linee[cont].Pin1[4].ToString() + Linee[cont].Pin1[5].ToString() + Linee[cont].Pin1[6].ToString() + Linee[cont].Pin1[7].ToString() + Linee[cont].Pin1[8].ToString();
                            if (Linee[cont].Pin1[1] == 0) cont = Linee[cont].Pin1[2];
                            else if (Linee[cont].Pin1[1] == 1) cont += Linee[cont].Pin1[2];
                            else if (Linee[cont].Pin1[1] == 2) cont -= Linee[cont].Pin1[2];
                        }
                        else if (Linee[cont].Pin2[0] == 1 && ingressi[1] == '1')
                        {
                            if (Linee[cont].Pin2[3] == 1) uscite += Linee[cont].Pin2[4].ToString() + Linee[cont].Pin2[5].ToString() + Linee[cont].Pin2[6].ToString() + Linee[cont].Pin2[7].ToString() + Linee[cont].Pin2[8].ToString();
                            if (Linee[cont].Pin2[1] == 0) cont = Linee[cont].Pin2[2];
                            else if (Linee[cont].Pin2[1] == 1) cont += Linee[cont].Pin2[2];
                            else if (Linee[cont].Pin2[1] == 2) cont -= Linee[cont].Pin2[2];
                        }
                        else if (Linee[cont].Pin3[0] == 1 && ingressi[2] == '1')
                        {
                            if (Linee[cont].Pin3[3] == 1) uscite += Linee[cont].Pin3[4].ToString() + Linee[cont].Pin3[5].ToString() + Linee[cont].Pin3[6].ToString() + Linee[cont].Pin3[7].ToString() + Linee[cont].Pin3[8].ToString();
                            if (Linee[cont].Pin3[1] == 0) cont = Linee[cont].Pin3[2];
                            else if (Linee[cont].Pin3[1] == 1) cont += Linee[cont].Pin3[2];
                            else if (Linee[cont].Pin3[1] == 2) cont -= Linee[cont].Pin3[2];
                        }
                        else if (Linee[cont].Pin4[0] == 1 && ingressi[3] == '1')
                        {
                            if (Linee[cont].Pin4[3] == 1) uscite += Linee[cont].Pin4[4].ToString() + Linee[cont].Pin4[5].ToString() + Linee[cont].Pin4[6].ToString() + Linee[cont].Pin4[7].ToString() + Linee[cont].Pin4[8].ToString();
                            if (Linee[cont].Pin4[1] == 0) cont = Linee[cont].Pin4[2];
                            else if (Linee[cont].Pin4[1] == 1) cont += Linee[cont].Pin4[2];
                            else if (Linee[cont].Pin4[1] == 2) cont -= Linee[cont].Pin4[2];
                        }
                        else if (Linee[cont].Pin5[0] == 1 && ingressi[4] == '1')
                        {
                            if (Linee[cont].Pin5[3] == 1) uscite += Linee[cont].Pin5[4].ToString() + Linee[cont].Pin5[5].ToString() + Linee[cont].Pin5[6].ToString() + Linee[cont].Pin5[7].ToString() + Linee[cont].Pin5[8].ToString();
                            if (Linee[cont].Pin5[1] == 0) cont = Linee[cont].Pin5[2];
                            else if (Linee[cont].Pin5[1] == 1) cont += Linee[cont].Pin5[2];
                            else if (Linee[cont].Pin5[1] == 2) cont -= Linee[cont].Pin5[2];
                        }
                        else
                        {
                            if (Linee[cont]._default[3] == 1) uscite += Linee[cont]._default[4].ToString() + Linee[cont]._default[5].ToString() + Linee[cont]._default[6].ToString() + Linee[cont]._default[7].ToString() + Linee[cont]._default[8].ToString();
                            switch (Linee[cont]._default[1])
                            {
                                case 0: cont = Linee[cont]._default[2]; break;
                                case 1: cont += Linee[cont]._default[2]; break;
                                case 2: cont -= Linee[cont]._default[2]; break;
                            }

                        }
                    }
                    if (cont < 0) cont = 0;

                }
                if (!RobotEnabled.Checked) serialPort1.Close();
                
            }
            
        }
        private void pictureBox2_Paint_1(object sender, PaintEventArgs e)
        {
            int x0 = pictureBox2.Width / 2;
            int y0 = pictureBox2.Height - 50;
            int Ascala = Robot.scala;

            Pen pen1 = new Pen(Brushes.Green);
            Pen pen2 = new Pen(Brushes.Brown);
            Pen pen3 = new Pen(Brushes.Blue);
            Pen pen4 = new Pen(Brushes.Red);
            Pen pen5 = new Pen(Brushes.Black);

            pen1.Width = spessore;
            pen2.Width = spessore;
            pen3.Width = spessore;
            pen4.Width = spessore;
            pen5.Width = spessore;

            e.Graphics.DrawLine(pen1, x0, y0, (int)Robot.T01[1, 3] / Ascala + x0, -(int)Robot.T01[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen2, (int)Robot.T01[1, 3] / Ascala + x0, -(int)Robot.T01[2, 3] / Ascala + y0, (int)Robot.T02[1, 3] / Ascala + x0, -(int)Robot.T02[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen3, (int)Robot.T02[1, 3] / Ascala + x0, -(int)Robot.T02[2, 3] / Ascala + y0, (int)Robot.T03[1, 3] / Ascala + x0, -(int)Robot.T03[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen4, (int)Robot.T03[1, 3] / Ascala + x0, -(int)Robot.T03[2, 3] / Ascala + y0, (int)Robot.T04[1, 3] / Ascala + x0, -(int)Robot.T04[2, 3] / Ascala + y0);
            e.Graphics.DrawLine(pen5, (int)Robot.T04[1, 3] / Ascala + x0, -(int)Robot.T04[2, 3] / Ascala + y0, (int)Robot.T06[1, 3] / Ascala + x0, -(int)Robot.T06[2, 3] / Ascala + y0);

        }


        int progres;
        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (numeroPunti != 0)
            {
                int count = 0;
                UltimoPunto = PuntoIniziale;
                Linee.Clear();

                foreach (Punti3D punto in Punti)
                {
                    Linee.Add(new Programma(punto.vel, punto.acc) { });
                    if (punto.tipo == 0)
                    {
                        Linee[count].linea(UltimoPunto, punto);
                        UltimoPunto = punto;
                    }
                    else if (punto.tipo == 1)
                    {
                        Linee[count].joint(UltimoPunto, punto);
                        UltimoPunto = punto;
                    }
                    else if (punto.tipo == 2)
                    {
                        Linee[count].delay(punto.tempo, UltimoPunto);
                    }
                    else if(punto.tipo == 3)
                    {
                        Linee[count].istruzione(punto.Pin1, punto.Pin2, punto.Pin3, punto.Pin4, punto.Pin5, punto._default, punto.contatore , UltimoPunto);
                    }
                    count++;
                }

                if (R && !serialPort1.IsOpen)
                {
                    try
                    {
                        serialPort1.Open();
                    }
                    catch (Exception)
                    {
                        RobotEnabled.Checked = false;
                        MessageBox.Show("Porta USB scollegata");
                    }
                }
                abilitazioneTasti(false);
                progres = 0;
                for (int i = 0; i < numeroPunti; i++)
                {
                    if (Linee[i].tempo == 0)
                        for (int u = 0; u < Linee[i].numStep; u++)
                        {
                            progres++;
                        }
                }
                cont = 0;
                cont2 = 0;
                p = 0;
                timer1.Start();
                metroProgressBar1.Show();
            }
            else MessageBox.Show("Nessuna istruzione!");
            metroButton29.Hide();
            metroButton30.Hide();
            metroButton35.Hide();
            metroButton31.Hide();
            metroButton36.Hide();
            metroButton37.Hide();
            label1.Select();

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            stop();
        }

        private void stop()
        {
            serialPort1.Close();
            abilitazioneTasti(true);
            timer1.Stop();
            metroProgressBar1.Hide();
            p = 0;
            label1.Select();
        }
        private void metroButton11_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(1, this.delta);
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(1, -this.delta);
        }

        private void metroButton12_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(2, this.delta);
        }


        private void metroButton39_Click(object sender, EventArgs e)
        {
            this.Posizione[1] -= 20;
            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(3, this.delta);
        }

        private void metroButton38_Click(object sender, EventArgs e)
        {
            this.Posizione[2] -= 20;
            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();

        }

        private void metroButton14_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(4, this.delta);
        }

        private void metroButton37_Click(object sender, EventArgs e)
        {
            this.Posizione[3] -= 20;
            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();

        }

        private void metroButton15_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(5, this.delta);
        }

        private void metroButton36_Click(object sender, EventArgs e)
        {
            this.Posizione[4] -= 20;
            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void metroButton16_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(6, this.delta);
        }

        private void metroButton35_Click(object sender, EventArgs e)
        {
            this.Posizione[2] -= 20;
            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(6, -this.delta);
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(2, -this.delta);
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(3, -this.delta);
        }

        private void metroButton31_Click(object sender, EventArgs e)
        {
            this.Posizione[3] -= 20;
            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(4, -this.delta);
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoC(5, -this.delta);
        }

        private void metroButton29_Click(object sender, EventArgs e)
        {
            this.Posizione[5] += 20;
            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void metroButton17_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++) this.Posizione[i] = this.Zero[i];
            label1.Select();

            Matematica Cinematica = new Matematica();
            double[] c = new double[6];
            double[] d = new double[6];
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            Robot.g1 = d[0];
            Robot.g2 = d[1];
            Robot.g3 = d[2];
            Robot.g4 = d[3];
            Robot.g5 = d[4];
            Robot.g6 = d[5];
            Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
            userControl11.refresh();
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            metroTextBox4.Text = ((int)Posizione[0]).ToString();
            metroTextBox5.Text = ((int)Posizione[1]).ToString();
            metroTextBox8.Text = ((int)Posizione[2]).ToString();
            metroTextBox7.Text = ((int)Posizione[3]).ToString();
            metroTextBox6.Text = ((int)Posizione[4]).ToString();
            metroTextBox9.Text = ((int)Posizione[5]).ToString();
        }



        private void metroTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox10.Text != "")
                try
                {
                    this.delta = Convert.ToDouble(metroTextBox10.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire un numero corretto!");
                }
        }

        private void metroButton22_Click(object sender, EventArgs e)
        {

            if (!timer1.Enabled) spostamentoJ(1, this.delta);

        }

        private void metroButton40_Click(object sender, EventArgs e)
        {

            if (!timer1.Enabled) spostamentoT(1, -this.delta);

        }

        private void metroButton33_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoT(2, this.delta);

        }

        private void metroButton39_Click_1(object sender, EventArgs e)
        {

            if (!timer1.Enabled) spostamentoT(2, -this.delta);

        }

        private void metroButton32_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoT(3, this.delta);
        }

        private void metroButton38_Click_1(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoT(3, -this.delta);
        }

        private void metroButton28_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(1, -this.delta);
        }

        private void metroButton21_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(2, this.delta);
        }

        private void metroButton27_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(2, -this.delta);
        }

        private void metroButton20_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(3, this.delta);
        }

        private void metroButton26_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(3, -this.delta);
        }

        private void metroButton19_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(4, this.delta);
        }

        private void metroButton25_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(4, -this.delta);
        }

        private void metroButton18_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(5, this.delta);
        }

        private void metroButton24_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(5, -this.delta);
        }

        private void metroButton11_Click_1(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(6, this.delta);
        }

        private void metroButton23_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoJ(6, -this.delta);
        }

        private void metroLabel25_Click(object sender, EventArgs e)
        {

        }

        private void metroButton34_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) spostamentoT(1, this.delta);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
            Punti3D punto = new Punti3D(Convert.ToDouble(metroTextBox4.Text), Convert.ToDouble(metroTextBox5.Text), Convert.ToDouble(metroTextBox8.Text), Convert.ToDouble(metroTextBox7.Text), Convert.ToDouble(metroTextBox6.Text), Convert.ToDouble(metroTextBox9.Text));
            Punti.Add(new Punti3D(Convert.ToDouble(metroTextBox4.Text), Convert.ToDouble(metroTextBox5.Text), Convert.ToDouble(metroTextBox8.Text), Convert.ToDouble(metroTextBox7.Text), Convert.ToDouble(metroTextBox6.Text), Convert.ToDouble(metroTextBox9.Text)) { });
            Punti[numeroPunti].tipo = 0;
            Punti[numeroPunti].acc = Convert.ToDouble(metroTextBox1.Text);
            Punti[numeroPunti].vel = Convert.ToDouble(metroTextBox2.Text);
            Punti[numeroPunti].tool = Convert.ToDouble(metroTextBox3.Text);
            double[] Arr;
            Arr = Punti3D.P2Arr(punto);
            for (int i = 0; i < 6; i++) dataGridView1.Rows[numeroPunti].Cells[i].Value = Arr[i];
            dataGridView1.Rows[numeroPunti].Cells[6].Value = metroTextBox1.Text;
            dataGridView1.Rows[numeroPunti].Cells[7].Value = metroTextBox2.Text;
            dataGridView1.Rows[numeroPunti].Cells[8].Value = metroTextBox3.Text;
            this.dataGridView1.Rows[numeroPunti].HeaderCell.Value = (numeroPunti + 1).ToString() + "    L";
            numeroPunti++;
            label1.Select();
        }


        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
            Punti3D punto = new Punti3D(Convert.ToDouble(metroTextBox4.Text), Convert.ToDouble(metroTextBox5.Text), Convert.ToDouble(metroTextBox8.Text), Convert.ToDouble(metroTextBox7.Text), Convert.ToDouble(metroTextBox6.Text), Convert.ToDouble(metroTextBox9.Text));
            Punti.Add(new Punti3D(Convert.ToDouble(metroTextBox4.Text), Convert.ToDouble(metroTextBox5.Text), Convert.ToDouble(metroTextBox8.Text), Convert.ToDouble(metroTextBox7.Text), Convert.ToDouble(metroTextBox6.Text), Convert.ToDouble(metroTextBox9.Text)) { });
            Punti[numeroPunti].tipo = 1;
            Punti[numeroPunti].acc = Convert.ToDouble(metroTextBox1.Text);
            Punti[numeroPunti].vel = Convert.ToDouble(metroTextBox2.Text);
            Punti[numeroPunti].tool = Convert.ToDouble(metroTextBox3.Text);
            double[] Arr;
            Arr = Punti3D.P2Arr(punto);

            for (int i = 0; i < 6; i++) dataGridView1.Rows[numeroPunti].Cells[i].Value = Arr[i];
            dataGridView1.Rows[numeroPunti].Cells[6].Value = metroTextBox1.Text;
            dataGridView1.Rows[numeroPunti].Cells[7].Value = metroTextBox2.Text;
            dataGridView1.Rows[numeroPunti].Cells[8].Value = metroTextBox3.Text;
            this.dataGridView1.Rows[numeroPunti].HeaderCell.Value = (numeroPunti + 1).ToString() + "    J";
            numeroPunti++;
            label1.Select();
        }
        bool R = false;
        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            R = !R;
        }

        private void metroLabel29_Click(object sender, EventArgs e)
        {

        }
        bool Loop = false;
        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Loop = !Loop;
        }

        private void metroButton29_Click_2(object sender, EventArgs e)
        {
            numeroPunti--;
            Punti.RemoveAt(RigaS);
            metroButton29.Hide();
            metroButton30.Hide();
            metroButton35.Hide();
            metroButton31.Hide();
            metroButton36.Hide();
            metroButton37.Hide();
            dataGridView1.Rows.RemoveAt(RigaS);
            RefreshTabella();
            label1.Select();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            for (int i = 0; i < numeroPunti; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    RigaS = i;
                    break;
                }
            }
            if (RigaS < numeroPunti && !timer1.Enabled)
            {
                metroButton29.Show();
                metroButton30.Show();
                metroButton31.Show();
                metroButton35.Show();
                metroButton36.Show();
                metroButton37.Show();

                if (Punti[RigaS].tipo != 2 && Punti[RigaS].tipo != 3)
                {
                    this.Posizione = Punti3D.P2Arr(Punti[RigaS]);
                   // RefreshDisegno();
                    RefreshRobotPos();
                    RefreshDati();
                }
                if (Punti[RigaS].tipo == 1)
                {
                    metroButton36.Enabled = false;
                    metroButton37.Enabled = true;
                }
                else if (Punti[RigaS].tipo == 0)
                {
                    metroButton36.Enabled = true;
                    metroButton37.Enabled = false;
                }
                else if (Punti[RigaS].tipo == 2)
                {
                    metroButton36.Enabled = false;
                    metroButton37.Enabled = false;
                }
                else if(Punti[RigaS].tipo == 3)
                {
                    metroButton36.Enabled = false;
                    metroButton37.Enabled = false;
                }
            }

        }

        int RigaS = 0;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            metroButton29.Hide();
            metroButton30.Hide();
            metroButton31.Hide();
            metroButton35.Hide();
            metroButton36.Hide();
            metroButton37.Hide();
        }

        private void RefreshTabella()
        {
            int count = 0;
            foreach (Punti3D punto in this.Punti)
            {
                double[] Arr;
                Arr = Punti3D.P2Arr(punto);
                if (punto.tipo == 2)
                {
                    dataGridView1.Rows[count].Cells[0].Value = punto.tempo;
                    this.dataGridView1.Rows[count].HeaderCell.Value = (count + 1).ToString() + "    T";
                    for (int i = 1; i < 9; i++) dataGridView1.Rows[count].Cells[i].Value = "";
                }
                else if (punto.tipo == 3)
                {
                    for (int i = 0; i < 9; i++) dataGridView1.Rows[count].Cells[i].Value = "";
                    this.dataGridView1.Rows[count].HeaderCell.Value = (count + 1).ToString() + "    I";
                    if (punto._default[1] == 0) dataGridView1.Rows[count].Cells[0].Value = "DEF  " + punto._default[2].ToString();
                    else dataGridView1.Rows[count].Cells[0].Value = "DEF  " + (punto._default[1] == 1 ? "+" : "-").ToString() + punto._default[2].ToString();

                    if (punto.Pin1[0] != 0)
                    {
                        if (punto.Pin1[1] == 0) dataGridView1.Rows[count].Cells[1].Value = "Pin1  " + punto.Pin1[2].ToString();
                        else dataGridView1.Rows[count].Cells[1].Value = "Pin1  " + (punto.Pin1[1] == 1 ? "+" : "-").ToString() + punto.Pin1[2].ToString();
                    }


                    if (punto.Pin2[0] != 0)
                    {
                        if (punto.Pin2[1] == 0) dataGridView1.Rows[count].Cells[2].Value = "Pin2  " + punto.Pin2[2].ToString();
                        else dataGridView1.Rows[count].Cells[2].Value = "Pin2  " + (punto.Pin2[1] == 1 ? "+" : "-").ToString() + punto.Pin2[2].ToString();
                    }


                    if (punto.Pin3[0] != 0)
                    {
                        if (punto.Pin3[1] == 0) dataGridView1.Rows[count].Cells[3].Value = "Pin3  " + punto.Pin3[2].ToString();
                        else dataGridView1.Rows[count].Cells[3].Value = "Pin3  " + (punto.Pin3[1] == 1 ? "+" : "-").ToString() + punto.Pin3[2].ToString();
                    }

                    if (punto.Pin4[0] != 0)
                    {
                        if (punto.Pin4[1] == 0) dataGridView1.Rows[count].Cells[4].Value = "Pin4  " + punto.Pin4[2].ToString();
                        else dataGridView1.Rows[count].Cells[4].Value = "Pin4  " + (punto.Pin4[1] == 1 ? "+" : "-").ToString() + punto.Pin4[2].ToString();
                    }

                    if (punto.Pin5[0] != 0)
                    {
                        if (punto.Pin5[1] == 0) dataGridView1.Rows[count].Cells[5].Value = "Pin5  " + punto.Pin5[2].ToString();
                        else dataGridView1.Rows[count].Cells[5].Value = "Pin5  " + (punto.Pin5[1] == 1 ? "+" : "-").ToString() + punto.Pin5[2].ToString();
                    }
                }
                else
                {
                    for (int i = 0; i < 6; i++) dataGridView1.Rows[count].Cells[i].Value = (int)Arr[i];
                    dataGridView1.Rows[count].Cells[6].Value = punto.vel;
                    dataGridView1.Rows[count].Cells[7].Value = punto.acc;
                    dataGridView1.Rows[count].Cells[8].Value = punto.tool;
                    if (punto.tipo == 0) this.dataGridView1.Rows[count].HeaderCell.Value = (count + 1).ToString() + "    L";
                    else if (punto.tipo == 1) this.dataGridView1.Rows[count].HeaderCell.Value = (count + 1).ToString() + "    J";
                }
                count++;
            }
        }
        Punti3D buffer = new Punti3D();
        private void metroButton30_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 9; i++)
            {
                buffer.Pin1[i] = Punti[RigaS].Pin1[i];
                buffer.Pin2[i] = Punti[RigaS].Pin2[i];
                buffer.Pin3[i] = Punti[RigaS].Pin3[i];
                buffer.Pin4[i] = Punti[RigaS].Pin4[i];
                buffer.Pin5[i] = Punti[RigaS].Pin5[i];
                buffer._default[i] = Punti[RigaS]._default[i];
            }
            for (int i = 0; i < 2; i++) buffer.contatore[i] = Punti[RigaS].contatore[i];

            buffer.A = Punti[RigaS].A;
            buffer.B = Punti[RigaS].B;
            buffer.C = Punti[RigaS].C;
            buffer.X = Punti[RigaS].X;
            buffer.Y = Punti[RigaS].Y;
            buffer.Z = Punti[RigaS].Z;

            buffer.acc = Punti[RigaS].acc;
            buffer.vel = Punti[RigaS].vel;
            buffer.tempo = Punti[RigaS].tempo;
            buffer.tipo = Punti[RigaS].tipo;
            buffer.tool = Punti[RigaS].tool;

            metroButton31.Enabled = true;
            label1.Select();
        }

        private void metroButton31_Click_1(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
            this.numeroPunti++;
            Punti3D nuovo = new Punti3D();

            for (int i = 0; i < 9; i++)
            {
                nuovo.Pin1[i] = buffer.Pin1[i];
                nuovo.Pin2[i] = buffer.Pin2[i];
                nuovo.Pin3[i] = buffer.Pin3[i];
                nuovo.Pin4[i] = buffer.Pin4[i];
                nuovo.Pin5[i] = buffer.Pin5[i];
                nuovo._default[i] = buffer._default[i];
            }
            for (int i = 0; i < 2; i++) nuovo.contatore[i] = buffer.contatore[i];

            nuovo.A =buffer.A;
            nuovo.B = buffer.B;
            nuovo.C = buffer.C;
            nuovo.X = buffer.X;
            nuovo.Y = buffer.Y;
            nuovo.Z = buffer.Z;

            nuovo.acc = buffer.acc;
            nuovo.vel = buffer.vel;
            nuovo.tempo = buffer.tempo;
            nuovo.tipo = buffer.tipo;
            nuovo.tool = buffer.tool;

            Punti.Insert(RigaS + 1, nuovo);
            this.RefreshTabella();
            metroButton29.Hide();
            metroButton30.Hide();
            metroButton31.Hide();
            metroButton35.Hide();
            metroButton36.Hide();
            metroButton37.Hide();
            label1.Select();
        }

        private void metroButton35_Click_1(object sender, EventArgs e)
        {
            if (Punti[RigaS].tipo == 3)
            {
                Form3 modifica = new Form3(Punti[RigaS]._default, Punti[RigaS].Pin1, Punti[RigaS].Pin2, Punti[RigaS].Pin3, Punti[RigaS].Pin4, Punti[RigaS].Pin5, Punti[RigaS].contatore);
                if (modifica.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    for (int i = 0; i < 9; i++)
                    {
                        Punti[RigaS].Pin1[i] = Istruzione.Pin1[i];
                        Punti[RigaS].Pin2[i] = Istruzione.Pin2[i];
                        Punti[RigaS].Pin3[i] = Istruzione.Pin3[i];
                        Punti[RigaS].Pin4[i] = Istruzione.Pin4[i];
                        Punti[RigaS].Pin5[i] = Istruzione.Pin5[i];
                        Punti[RigaS]._default[i] = Istruzione.Default[i];
                    }
                    for (int i = 0; i < 2; i++) Punti[RigaS].contatore[i] = Istruzione.Conteggio[i];

                }
            }
            else
            {
                int tipo = Punti[RigaS].tipo;
                Punti[RigaS] = Punti3D.Arr2P(this.Posizione);
                Punti[RigaS].vel = Convert.ToDouble(metroTextBox1.Text);
                Punti[RigaS].acc = Convert.ToDouble(metroTextBox2.Text);
                Punti[RigaS].tipo = tipo;
                if (tipo == 2) Punti[RigaS].tempo = Convert.ToInt32(metroTextBox11.Text);
            }
            this.RefreshTabella();
            metroButton29.Hide();
            metroButton30.Hide();
            metroButton31.Hide();
            metroButton35.Hide();
            metroButton36.Hide();
            metroButton37.Hide();
            label1.Select();
        }

        private void metroTextBox4_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox4.Text != "" && metroTextBox4.Text != "-" && !timer1.Enabled)
            {
                try
                {
                    this.Posizione[0] = Convert.ToDouble(metroTextBox4.Text);
                    RefreshDisegno();
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire un numero corretto!");
                }

            }
        }


        private void metroTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox5.Text != "" && metroTextBox5.Text != "-" && !timer1.Enabled)
            {
                try
                {
                    this.Posizione[1] = Convert.ToDouble(metroTextBox5.Text);
                    RefreshDisegno();
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire un numero corretto!");
                }

            }
        }



        private void metroTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox8.Text != "" && metroTextBox8.Text != "-" && !timer1.Enabled)
            {
                try
                {
                    this.Posizione[2] = Convert.ToDouble(metroTextBox8.Text);
                    RefreshDisegno();
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire un numero corretto!");
                }
            }

        }

        private void metroTextBox7_TextChanged(object sender, EventArgs e)
        {

            if (metroTextBox7.Text != "" && metroTextBox7.Text != "-" && !timer1.Enabled)
            {
                try
                {
                    this.Posizione[3] = Convert.ToDouble(metroTextBox7.Text);
                    RefreshDisegno();
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire un numero corretto!");
                }
            }
        }

        private void metroTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox6.Text != "" && metroTextBox6.Text != "-" && !timer1.Enabled)
            {
                try
                {
                    this.Posizione[4] = Convert.ToDouble(metroTextBox6.Text);
                    RefreshDisegno();
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire un numero corretto!");
                }
            }
        }

        private void metroTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox9.Text != "" && metroTextBox9.Text != "-" && !timer1.Enabled)
            {
                try
                {
                    this.Posizione[5] = Convert.ToDouble(metroTextBox9.Text);
                    RefreshDisegno();
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire un numero corretto!");
                }
            }
        }

        private void RefreshDisegno()
        {

            return;
            double[] c = new double[6];
            double[] d = new double[6];
            Matematica Cinematica = new Matematica();
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            Robot.g1 = d[0];
            Robot.g2 = d[1];
            Robot.g3 = d[2];
            Robot.g4 = d[3];
            Robot.g5 = d[4];
            Robot.g6 = d[5];
            Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
            userControl11.refresh();
        }

        private void RefreshRobotPos()
        {
            double[] c = new double[6];
            double[] d = new double[6];
            Matematica Cinematica = new Matematica();
            Cinematica.inversa(Posizione, ref c, ref d);
            Cinematica.diretta(d[0], d[1], d[2], d[3], d[4], d[5], ref Posizione);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            Robot.g1 = d[0];
            Robot.g2 = d[1];
            Robot.g3 = d[2];
            Robot.g4 = d[3];
            Robot.g5 = d[4];
            Robot.g6 = d[5];
            Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
            userControl11.refresh();
        }
    
        private void RefreshDati()
        {
            metroTextBox4.Text = ((int)Posizione[0]).ToString();
            metroTextBox5.Text = ((int)Posizione[1]).ToString();
            metroTextBox8.Text = ((int)Posizione[2]).ToString();
            metroTextBox7.Text = ((int)Posizione[3]).ToString();
            metroTextBox6.Text = ((int)Posizione[4]).ToString();
            metroTextBox9.Text = ((int)Posizione[5]).ToString();
        }

        private void metroButton36_Click_1(object sender, EventArgs e)
        {
            Punti[RigaS].tipo = 1;
            metroButton29.Hide();
            metroButton30.Hide();
            metroButton31.Hide();
            metroButton35.Hide();
            metroButton36.Hide();
            metroButton37.Hide();
            RefreshTabella();
            label1.Select();
        }

        private void metroButton37_Click_1(object sender, EventArgs e)
        {
            Punti[RigaS].tipo = 0;
            metroButton29.Hide();
            metroButton30.Hide();
            metroButton31.Hide();
            metroButton35.Hide();
            metroButton36.Hide();
            metroButton37.Hide();
            RefreshTabella();
            label1.Select();
        }

        private void metroButton41_Click(object sender, EventArgs e)
        {
            Punti.Clear();
            for (int i = 0; i < this.numeroPunti; i++) dataGridView1.Rows.RemoveAt(0);
            this.numeroPunti = 0;
            label1.Select();
        }

        private void metroButton42_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Robot Studio Files|*.rbt";
            openFileDialog1.Title = "Scegli un Robot Studio File";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.OpenFile()))
                {
                    Punti.Clear();
                    for (int i = 0; i < this.numeroPunti; i++) dataGridView1.Rows.RemoveAt(0);
                    this.numeroPunti = 0;

                    Matematica Cinematica = new Matematica();
                    double[] giunti = new double[6];

                    giunti[0] = 0;
                    giunti[1] = 90;
                    giunti[2] = 90;
                    giunti[3] = 0;
                    giunti[4] = 20;
                    giunti[5] = 0;
                    Robot.g1 = giunti[0];
                    Robot.g2 = giunti[1];
                    Robot.g3 = giunti[2];
                    Robot.g4 = giunti[3];
                    Robot.g5 = giunti[4];
                    Robot.g6 = giunti[5];
                    Cinematica.diretta(giunti[0], giunti[1], giunti[2], giunti[3], giunti[4], giunti[5], ref Posizione);
                    Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
                    userControl11.refresh();
                    metroTextBox1.Text = 2.ToString();
                    metroTextBox2.Text = 2.ToString();
                    metroTextBox3.Text = 0.ToString();

                    Punti3D punto = new Punti3D(this.Posizione);
                    double[] arr = new double[6];
                    arr = Punti3D.P2Arr(punto);
                    PuntoIniziale = punto;
                    for (int i = 0; i < 6; i++) dataGridView1.Rows[0].Cells[i].Value = (int)this.Posizione[i];

                    double[] buffer = new double[6];
                    double[] Pbuffer = new double[5];
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        Char divisore = ':';
                        String[] valori = linea.Split(divisore);
                        int cont = 0;
                        if (valori[0] == "i")
                        {
                            Punti3D nuovo = new Punti3D();
                            this.dataGridView1.Rows.Add();

                            for (int i = 0; i < 9; i++)
                            {
                                nuovo._default[i] = Convert.ToInt32(valori[i + 1]);
                                nuovo.Pin1[i] = Convert.ToInt32(valori[i + 1 + 9]);
                                nuovo.Pin2[i] = Convert.ToInt32(valori[i + 1 + 9 + 9]);                 // ogni 9 valori indico un parametro diverso
                                nuovo.Pin3[i] = Convert.ToInt32(valori[i + 1 + 9 + 9 + 9]);
                                nuovo.Pin4[i] = Convert.ToInt32(valori[i + 1 + 9 + 9 + 9 + 9]);
                                nuovo.Pin5[i] = Convert.ToInt32(valori[i + 1 + 9 + 9 + 9 + 9 + 9]);
                            }

                            nuovo.contatore[0] = Convert.ToInt32(valori[55]);
                            nuovo.contatore[1] = Convert.ToInt32(valori[56]);
                            nuovo.tipo = 3;
                            Punti.Add(nuovo);

                        }
                        else
                        {
                            foreach (var substring in valori)
                            {
                                if (cont < 6) buffer[cont] = Convert.ToDouble(substring);
                                else Pbuffer[cont - 6] = Convert.ToDouble(substring);
                                cont++;
                            }
                            this.dataGridView1.Rows.Add();
                            Punti.Add(Punti3D.Arr2P(buffer));
                            Punti[numeroPunti].vel = Pbuffer[0];
                            Punti[numeroPunti].acc = Pbuffer[1];
                            Punti[numeroPunti].tool = Pbuffer[2];
                            Punti[numeroPunti].tipo = (int)Pbuffer[3];
                            Punti[numeroPunti].tempo = (int)Pbuffer[4];
                        }
                        this.numeroPunti++;
                    }
                    RefreshTabella();
                }
            }
            label1.Select();
        }

        private void metroButton43_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Robot Studio Files|*.rbt";
            saveFileDialog1.Title = "Salva Robot Studio File";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (System.IO.StreamWriter scrittore = new System.IO.StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (Punti3D punto in Punti)
                    {
                        String stringa = "";

                        if (punto.tipo == 3)
                        {
                            stringa = "i" + ':' + punto._default[0].ToString() + ':' + punto._default[1].ToString() + ':' + punto._default[2].ToString() + ':' + punto._default[3].ToString() + ':' + punto._default[4].ToString() + ':' + punto._default[5].ToString() + ':' + punto._default[6].ToString() + ':' + punto._default[7].ToString() + ':' + punto._default[8].ToString() + ':'
                            + punto.Pin1[0].ToString() + ':' + punto.Pin1[1].ToString() + ':' + punto.Pin1[2].ToString() + ':' + punto.Pin1[3].ToString() + ':' + punto.Pin1[4].ToString() + ':' + punto.Pin1[5].ToString() + ':' + punto.Pin1[6].ToString() + ':' + punto.Pin1[7].ToString() + ':' + punto.Pin1[8].ToString() + ':'
                            + punto.Pin2[0].ToString() + ':' + punto.Pin2[1].ToString() + ':' + punto.Pin2[2].ToString() + ':' + punto.Pin2[3].ToString() + ':' + punto.Pin2[4].ToString() + ':' + punto.Pin2[5].ToString() + ':' + punto.Pin2[6].ToString() + ':' + punto.Pin2[7].ToString() + ':' + punto.Pin2[8].ToString() + ':'
                            + punto.Pin3[0].ToString() + ':' + punto.Pin3[1].ToString() + ':' + punto.Pin3[2].ToString() + ':' + punto.Pin3[3].ToString() + ':' + punto.Pin3[4].ToString() + ':' + punto.Pin3[5].ToString() + ':' + punto.Pin3[6].ToString() + ':' + punto.Pin3[7].ToString() + ':' + punto.Pin3[8].ToString() + ':'
                            + punto.Pin4[0].ToString() + ':' + punto.Pin4[1].ToString() + ':' + punto.Pin4[2].ToString() + ':' + punto.Pin4[3].ToString() + ':' + punto.Pin4[4].ToString() + ':' + punto.Pin4[5].ToString() + ':' + punto.Pin4[6].ToString() + ':' + punto.Pin4[7].ToString() + ':' + punto.Pin4[8].ToString() + ':'
                            + punto.Pin5[0].ToString() + ':' + punto.Pin5[1].ToString() + ':' + punto.Pin5[2].ToString() + ':' + punto.Pin5[3].ToString() + ':' + punto.Pin5[4].ToString() + ':' + punto.Pin5[5].ToString() + ':' + punto.Pin5[6].ToString() + ':' + punto.Pin5[7].ToString() + ':' + punto.Pin5[8].ToString() + ':'
                            + punto.contatore[0].ToString() + ':' + punto.contatore[1].ToString();
                        }
                        else
                        {
                            stringa = ((int)punto.X).ToString() + ':' + ((int)punto.Y).ToString() +
                            ':' + ((int)punto.Z).ToString() + ':' + ((int)punto.A).ToString() + ':' + ((int)punto.B).ToString()
                            + ':' + ((int)punto.C).ToString() + ':' + ((int)punto.vel).ToString() + ':' + ((int)punto.acc).ToString()
                            + ':' + ((int)punto.tool).ToString() + ':' + (punto.tipo).ToString();
                        }
                    
                        if (punto.tipo == 2) stringa += ':' + (punto.tempo).ToString();

                        scrittore.WriteLine(stringa);
                    }
                }
            }
            label1.Select();
        }

        private void abilitazioneTasti(bool stato)
        {
            RobotEnabled.Enabled = stato;
            metroButton1.Enabled = stato;
            metroButton2.Enabled = stato;
            metroButton17.Enabled = stato;
            metroButton41.Enabled = stato;
            metroButton42.Enabled = stato;
            metroButton43.Enabled = stato;
            metroButton44.Enabled = stato;
            metroButton45.Enabled = stato;
            metroButton46.Enabled = stato;
            Arduino.Enabled = stato;
        }

        private void metroButton44_Click(object sender, EventArgs e)
        {
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Robot.visione2D) elementHost2.Hide();
                else elementHost2.Show();
            }
                label1.Select();
        }

        private void metroButton45_Click(object sender, EventArgs e)
        {
            if (metroTextBox11.Text != "" && Convert.ToDouble(metroTextBox11.Text) != 0)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[numeroPunti].HeaderCell.Value = (numeroPunti + 1).ToString() + "    T";
                dataGridView1.Rows[numeroPunti].Cells[0].Value = Convert.ToInt32(metroTextBox11.Text);
                Punti3D t = new Punti3D();
                t.tipo = 2;
                t.tempo = Convert.ToInt32(metroTextBox11.Text);
                Punti.Add(t);
                numeroPunti++;
            }
            label1.Select();
        }


        private void metroButton46_Click(object sender, EventArgs e)
        {
            Form3 fh = new Form3();
            
            if(fh.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[numeroPunti].HeaderCell.Value = (numeroPunti + 1).ToString() + "    I";
                if (Istruzione.Default[1] == 0) dataGridView1.Rows[numeroPunti].Cells[0].Value = "DEF  " + Istruzione.Default[2].ToString();
                else dataGridView1.Rows[numeroPunti].Cells[0].Value = "DEF  "+(Istruzione.Default[1] == 1 ? "+" : "-").ToString() + Istruzione.Default[2].ToString();

                if(Istruzione.Pin1[0] != 0) 
                {
                    if (Istruzione.Pin1[1] == 0) dataGridView1.Rows[numeroPunti].Cells[1].Value = "Pin1  " + Istruzione.Pin1[2].ToString();
                    else dataGridView1.Rows[numeroPunti].Cells[1].Value = "Pin1  " + (Istruzione.Pin1[1] == 1 ? "+" : "-").ToString() + Istruzione.Pin1[2].ToString();
                }


                if (Istruzione.Pin2[0] != 0)
                {
                    if (Istruzione.Pin2[1] == 0) dataGridView1.Rows[numeroPunti].Cells[2].Value = "Pin2  " + Istruzione.Pin2[2].ToString();
                    else dataGridView1.Rows[numeroPunti].Cells[2].Value = "Pin2  " + (Istruzione.Pin2[1] == 1 ? "+" : "-").ToString() + Istruzione.Pin2[2].ToString();
                }


                if (Istruzione.Pin3[0] != 0)
                {
                    if (Istruzione.Pin3[1] == 0) dataGridView1.Rows[numeroPunti].Cells[3].Value = "Pin3  " + Istruzione.Pin3[2].ToString();
                    else dataGridView1.Rows[numeroPunti].Cells[3].Value = "Pin3  " + (Istruzione.Pin3[1] == 1 ? "+" : "-").ToString() + Istruzione.Pin3[2].ToString();
                }

                if (Istruzione.Pin4[0] != 0)
                {
                    if (Istruzione.Pin4[1] == 0) dataGridView1.Rows[numeroPunti].Cells[4].Value = "Pin4  " + Istruzione.Pin4[2].ToString();
                    else dataGridView1.Rows[numeroPunti].Cells[4].Value = "Pin4  " + (Istruzione.Pin4[1] == 1 ? "+" : "-").ToString() + Istruzione.Pin4[2].ToString();
                }

                if (Istruzione.Pin5[0] != 0)
                {
                    if (Istruzione.Pin5[1] == 0) dataGridView1.Rows[numeroPunti].Cells[5].Value = "Pin5  " + Istruzione.Pin5[2].ToString();
                    else dataGridView1.Rows[numeroPunti].Cells[5].Value = "Pin5  " + (Istruzione.Pin5[1] == 1 ? "+" : "-").ToString() + Istruzione.Pin5[2].ToString();
                }

                Punti3D i = new Punti3D();
                i.tipo = 3;
                for (int u = 0; u < 9; u++) i.Pin1[u] = Istruzione.Pin1[u];
                for (int u = 0; u < 9; u++) i.Pin2[u] = Istruzione.Pin2[u];
                for (int u = 0; u < 9; u++) i.Pin3[u] = Istruzione.Pin3[u];
                for (int u = 0; u < 9; u++) i.Pin4[u] = Istruzione.Pin4[u];
                for (int u = 0; u < 9; u++) i.Pin5[u] = Istruzione.Pin5[u];
                for (int u = 0; u < 9; u++) i._default[u] = Istruzione.Default[u];
                for (int u = 0; u < 2; u++) i.contatore[u] = Istruzione.Conteggio[u];
                Punti.Add(i);
                numeroPunti++;

            }
            label1.Select();
        }


        private void elementHost2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void Arduino_Click(object sender, EventArgs e)
        {
            if (numeroPunti != 0) // calcolo punti
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Arduino file|*.ino";
                saveFileDialog1.Title = "Salva un file Arduino";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (System.IO.StreamWriter scrittore = new System.IO.StreamWriter(saveFileDialog1.FileName))
                    {
                        scrittore.WriteLine("//                                                                          Robot Studio");
                        scrittore.WriteLine("//                                                                     Developed by Luca Samar");
                        scrittore.WriteLine("//                                                                         MITS Malignani");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("#include <Servo.h>");
                        scrittore.WriteLine("Servo motore[6];");
                        scrittore.WriteLine("const int Input[] = { A1, A2, A3, A4, A5 };");
                        scrittore.WriteLine("const int Output[] = { 4, 7, 8, 12, 13 };");
                        scrittore.WriteLine("bool lettureIngressi[5];");
                        scrittore.WriteLine("bool setUscite[] = { 0, 0, 0, 0, 0 };");

                        int count = 0;
                        bool ancheist = false;
                        UltimoPunto = PuntoIniziale;
                        Linee.Clear();

                        foreach (Punti3D punto in Punti)
                        {
                            Linee.Add(new Programma(punto.vel, punto.acc) { });
                            if (punto.tipo == 0)
                            {
                                Linee[count].linea(UltimoPunto, punto);
                                UltimoPunto = punto;
                            }
                            else if (punto.tipo == 1)
                            {
                                Linee[count].joint(UltimoPunto, punto);
                                UltimoPunto = punto;
                            }
                            else if (punto.tipo == 2)
                            {
                                Linee[count].delay(punto.tempo, UltimoPunto);
                            }
                            else if (punto.tipo == 3)
                            {
                                Linee[count].istruzione(punto.Pin1, punto.Pin2, punto.Pin3, punto.Pin4, punto.Pin5, punto._default, punto.contatore, UltimoPunto);
                                ancheist = true;
                            }
                            count++;
                        }
                        
                 
                        
                        String stringa = "const int numeroPunti = " + (numeroPunti).ToString() + ";";
                        scrittore.WriteLine(stringa);
                        stringa = "const int numeroStep[] = { ";
                        cont = 0;
                        int piualto = 0;
                        foreach(Programma punto in Linee )
                        {
                            cont++;
                            stringa += (!punto.ist?punto.numStep.ToString():1.ToString()) + (cont < numeroPunti ? ", " : " };");
                            if (punto.numStep > piualto) piualto = (int)punto.numStep;
                        }
                        piualto++;
                        scrittore.WriteLine(stringa);

                        cont = 0;
                        cont2 = 0;

                        List<double[]> quote_j = new List<double[]>();

                        foreach (Programma istruzione in Linee)
                        {
                            foreach( double[] punto in istruzione.P )
                            {
                                double[] a = new double[6];
                                Matematica Cinematica = new Matematica();
                                Cinematica.inversa(punto, ref a, ref giunti);
                                Cinematica.diretta(giunti[0], giunti[1], giunti[2], giunti[3], giunti[4], giunti[5], ref a);
                                Robot.g1 = giunti[0];
                                Robot.g2 = giunti[1];
                                Robot.g3 = giunti[2];
                                Robot.g4 = giunti[3];
                                Robot.g5 = giunti[4];
                                Robot.g6 = giunti[5];
                                Cinematica.leva(ref Robot.ga1, ref Robot.ga2);
                                quote_j.Add(new double[] { Robot.g1, Robot.g2, Robot.ga1, Robot.g4, Robot.g5, Robot.g6 });
                            }
                        }
                        int step = Convert.ToInt16(metroTextBox12.Text);

                        Programma.process_points(ref quote_j, step);

                        String M1 = "const float posizioneMotore1[] = {";
                        String M2 = "const float posizioneMotore2[] = {";
                        String M3 = "const float posizioneMotore3[] = {";
                        String M4 = "const byte posizioneMotore4[] = {";
                        String M5 = "const byte posizioneMotore5[] = {";
                        String M6 = "const byte posizioneMotore6[] = {";

                        foreach (double[] p in quote_j)
                        {
                            M1 += ((float)p[0] + 90).ToString() + ",";
                            M2 += (180 - (float)p[1]).ToString() + ",";
                            M3 += (-(float)p[2] - 100).ToString() + ",";
                            M4 += ((float)p[3] + 90).ToString() + ",";
                            M5 += (200 - (float)p[4] - 90).ToString() + ",";
                            M6 += ((float)p[5] + 90).ToString() + ",";
                        }

                        scrittore.WriteLine(M1 + "0};");
                        scrittore.WriteLine(M2 + "0};");
                        scrittore.WriteLine(M3 + "0};");
                        scrittore.WriteLine(M4 + "0};");
                        scrittore.WriteLine(M5 + "0};");
                        scrittore.WriteLine(M6 + "0};");


                        Robot.g1 = 0;
                        Robot.g2 = 0;
                        Robot.g3 = 0;
                        Robot.g4 = 0;
                        Robot.g5 = 0;
                        Robot.g6 = 0;

                        //while (cont < numeroPunti)   //  esecuzione
                        //{
                        //    if (cont2 < Linee[cont].numStep)
                        //    {
                        //        for (int i = 0; i < 6; i++)
                        //        {
                        //            C[i] = Linee[cont].P[cont2][i];
                        //        }
                        //        cont2++;
                        //    }
                        //    else
                        //    {
                        //        cont2 = 0;
                        //        cont++;
                        //        if (cont < numeroPunti)
                        //        {
                        //            M1 += "},{";
                        //            M2 += "},{";
                        //            M3 += "},{";
                        //            M4 += "},{";
                        //            M5 += "},{";
                        //            M6 += "},{";
                        //        }
                        //    }
                        //    double[] a = new double[6];

                        //    Matematica Cinematica = new Matematica();
                        //    Cinematica.inversa(C, ref a, ref giunti);
                        //    Cinematica.diretta(giunti[0], giunti[1], giunti[2], giunti[3], giunti[4], giunti[5], ref a);
                        //    Robot.g1 = giunti[0];
                        //    Robot.g2 = giunti[1];
                        //    Robot.g3 = giunti[2];
                        //    Robot.g4 = giunti[3];
                        //    Robot.g5 = giunti[4];
                        //    Robot.g6 = giunti[5];
                        //    Cinematica.leva(ref Robot.ga1, ref Robot.ga2);

                        //    if (cont < numeroPunti)
                        //    {
                        //        M1 += ((int)giunti[0] + 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                        //        M2 += (180-(int)giunti[1]).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                        //        M3 += (-(int)Robot.ga1-100).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                        //        M4 += ((int)giunti[3] + 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                        //        M5 += (200-(int)giunti[4] - 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                        //        M6 += ((int)giunti[5] + 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                        //    }
                        //}
                        //M1 += "}};";
                        //M2 += "}};";
                        //M3 += "}};";
                        //M4 += "}};";
                        //M5 += "}};";
                        //M6 += "}};";
                        //scrittore.WriteLine(M1);
                        //scrittore.WriteLine(M2);
                        //scrittore.WriteLine(M3);
                        //scrittore.WriteLine(M4);
                        //scrittore.WriteLine(M5);
                        //scrittore.WriteLine(M6);

                        int conteggioMomentaneo = 0;
                        int numeroIstruzioni = 0;
                        List<Programma> istruzioni = new List<Programma>();
                        for (int i = 0; i < numeroPunti; i++)
                        {
                            if (Linee[i].ist)
                            {
                                istruzioni.Add(Linee[i]);
                                numeroIstruzioni++;
                                scrittore.WriteLine("const int Istruzione" + numeroIstruzioni.ToString() + " = " + i.ToString()+";");
                                conteggioMomentaneo++;
                            }
                            else 
                            for (int u = 0; u < Linee[i].numStep; u++)
                            {                              
                                conteggioMomentaneo++;
                            }
                        }
                        conteggioMomentaneo = 0;
                        numeroIstruzioni = 0;
                        for (int i = 0; i < numeroPunti; i++)
                        {
                            if (Linee[i].ist)
                            {
                                numeroIstruzioni++;
                                if (Linee[i].contatore[0] == 1) scrittore.WriteLine("int Contatore" + numeroIstruzioni.ToString() + " = 0;");
                                conteggioMomentaneo++;
                            }
                            else
                                for (int u = 0; u < Linee[i].numStep; u++)
                                {
                                    conteggioMomentaneo++;
                                }
                        }


                        scrittore.WriteLine("int conteggio1 = 0;");
                        scrittore.WriteLine("int conteggio2 = 0;");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("/*********************************************************************************************************************************************************************/");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("void setup() ");
                        scrittore.WriteLine("{");
                        scrittore.WriteLine("  for(int i = 0; i < 5; i++)");
                        scrittore.WriteLine("  {");
                        scrittore.WriteLine("    pinMode( Input[i], INPUT_PULLUP);");
                        scrittore.WriteLine("    pinMode( Output[i], OUTPUT);");
                        scrittore.WriteLine("  }");
                        scrittore.WriteLine("  motore[0].attach(3);");
                        scrittore.WriteLine("  motore[1].attach(5);");
                        scrittore.WriteLine("  motore[2].attach(6);");
                        scrittore.WriteLine("  motore[3].attach(9);");
                        scrittore.WriteLine("  motore[4].attach(10);");
                        scrittore.WriteLine("  motore[5].attach(11);");
                        scrittore.WriteLine("  motore[0].write(90);");
                        scrittore.WriteLine("  motore[1].write(90);");
                        scrittore.WriteLine("  motore[2].write(90);");
                        scrittore.WriteLine("  motore[3].write(90);");
                        scrittore.WriteLine("  motore[4].write(90);");
                        scrittore.WriteLine("  motore[5].write(90);");
                        scrittore.WriteLine("  delay(20);");
                        scrittore.WriteLine("}");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("/*********************************************************************************************************************************************************************/");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("void loop() ");
                        scrittore.WriteLine("{");
                       // scrittore.WriteLine("  if (conteggio2 >= numeroStep[conteggio1])");
                       // scrittore.WriteLine("  {");
                       // scrittore.WriteLine("    conteggio2 = 0;");
                       // scrittore.WriteLine("    conteggio1++;");
                       // scrittore.WriteLine("  }");
                       // scrittore.WriteLine("  if (conteggio1 >= numeroPunti || conteggio1 < 0) conteggio1 = 0;");
                        scrittore.WriteLine("");
                        if (numeroIstruzioni != 0)
                        {
                            scrittore.WriteLine("  for(int i = 0; i < 5 ; i++)  lettureIngressi[i] = !digitalRead(Input[i]);");
                            scrittore.WriteLine("");
                            scrittore.WriteLine("  switch(conteggio1)");
                            scrittore.WriteLine("  {");
                            for (int i = 0; i < numeroIstruzioni; i++)
                            {
                                scrittore.WriteLine("    case Istruzione" + (i + 1).ToString() + ":");
                                String spazio2 = "";
                                if (istruzioni[i].contatore[0] == 1)
                                {
                                    scrittore.WriteLine("      Contatore" + (i + 1).ToString() + "++;");
                                    scrittore.WriteLine("      if(Contatore" + (i + 1).ToString() + " > " + istruzioni[i].contatore[1] + ")");
                                    scrittore.WriteLine("      {");
                                    scrittore.WriteLine("        conteggio1++;");
                                    scrittore.WriteLine("        Contatore" + (i + 1).ToString() + " = 0;");
                                    scrittore.WriteLine("      }");
                                    scrittore.WriteLine("      else");
                                    scrittore.WriteLine("      {");
                                    spazio2 = "  ";
                                }
                                bool almenouno = false;
                                if (istruzioni[i].Pin1[0] == 1)
                                {
                                    almenouno = true;
                                    scrittore.WriteLine("      " + spazio2 + "if(lettureIngressi[0])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin1[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin1[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin1[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin1[2].ToString() + ";"); break;
                                    }
                                    if (istruzioni[i].Pin1[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin1[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin1[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin1[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin1[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin1[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                }
                                if (istruzioni[i].Pin2[0] == 1)             // pin2
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[1])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin2[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin2[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin2[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin2[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin2[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin2[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin2[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin2[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin2[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin2[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                if (istruzioni[i].Pin3[0] == 1)             // pin3
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[2])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin3[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin3[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin3[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin3[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin3[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin3[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin3[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin3[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin3[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin3[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                if (istruzioni[i].Pin4[0] == 1)             // pin4
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[3])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin4[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin4[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin4[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin4[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin4[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin4[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin4[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin4[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin4[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin4[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                if (istruzioni[i].Pin5[0] == 1)             // pin5
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[4])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin5[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin5[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin5[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin5[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin5[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin5[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin5[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin5[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin5[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin5[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                String spazio3 = "";
                                if (almenouno)                                                  // default
                                {
                                    scrittore.WriteLine("     " + spazio2 + " else");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    spazio3 = "  ";
                                }
                                switch (istruzioni[i]._default[1])
                                {
                                    case 0: scrittore.WriteLine("      " + spazio2 + spazio3 + "  conteggio1 = " + istruzioni[i]._default[2].ToString() + ";"); break;
                                    case 1: scrittore.WriteLine("     " + spazio2 + spazio3 + " conteggio1 += " + istruzioni[i]._default[2].ToString() + ";"); break;
                                    case 2: scrittore.WriteLine("     " + spazio2 + spazio3 + " conteggio1 -= " + istruzioni[i]._default[2].ToString() + ";"); break;
                                }
                                if (istruzioni[i]._default[3] == 1)
                                {
                                    scrittore.WriteLine("    " + spazio2 + spazio3 + "  setUscite[0] = " + (istruzioni[i]._default[4] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("    " + spazio2 + spazio3 + "  setUscite[1] = " + (istruzioni[i]._default[5] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("   " + spazio2 + spazio3 + "   setUscite[2] = " + (istruzioni[i]._default[6] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("  " + spazio2 + spazio3 + "    setUscite[3] = " + (istruzioni[i]._default[7] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("   " + spazio2 + spazio3 + "   setUscite[4] = " + (istruzioni[i]._default[8] == 0 ? "false" : "true") + ";");
                                }
                                if (almenouno) scrittore.WriteLine("      " + spazio2 + "} ");
                                if (istruzioni[i].contatore[0] == 1) scrittore.WriteLine("      } ");
                                scrittore.WriteLine("      break;");
                                scrittore.WriteLine("");
                            }
                            scrittore.WriteLine("    default:");
                            scrittore.WriteLine("      motore[0].write(posizioneMotore1[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[1].write(posizioneMotore2[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[2].write(posizioneMotore3[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[3].write(posizioneMotore4[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[4].write(posizioneMotore5[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[5].write(posizioneMotore6[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      conteggio2++;");
                            scrittore.WriteLine("      break;");
                            scrittore.WriteLine("  }");
                        }
                        else
                        {

                                 scrittore.WriteLine("for(int i = 0; i < " +  quote_j.Count.ToString() + " ; i++ )");
                                 scrittore.WriteLine("{");
                                 scrittore.WriteLine(" long old_m = millis();");
  
                                 scrittore.WriteLine(" int pos1 = float(700.0 + (posizioneMotore1[i] * 8.889));");
                                 scrittore.WriteLine(" int pos2 = float(700.0 +(posizioneMotore2[i] - 5.0)*  8.889);");
                                 scrittore.WriteLine(" int pos3 = float(700.0 +(posizioneMotore3[i] + 11.0)*  8.889);");
                                 scrittore.WriteLine(" int pos4 = posizioneMotore4[i];");
                                 scrittore.WriteLine(" int pos5 = posizioneMotore5[i] + 10;");
                                scrittore.WriteLine(" int pos6 = posizioneMotore6[i] - 5;");
  
                                 scrittore.WriteLine(" motore[0].writeMicroseconds(pos1);");
                                scrittore.WriteLine(" motore[1].writeMicroseconds(pos2);");
                                 scrittore.WriteLine(" motore[2].writeMicroseconds(pos3);");
                                scrittore.WriteLine(" motore[3].write(pos4);");
                                scrittore.WriteLine(" motore[4].write(pos5);");
                                 scrittore.WriteLine(" motore[5].write(pos6); ");
                                 scrittore.WriteLine(" while( millis() - old_m < 20 ){}");
                                 scrittore.WriteLine(" }");

                            //scrittore.WriteLine("  conteggio2++;");
                        }
                        if(numeroIstruzioni != 0)scrittore.WriteLine("  for(int i = 0; i < 5 ; i++)  digitalWrite(Output[i], setUscite[i]);");
                        //scrittore.WriteLine("  delay(20);");                    
                        scrittore.WriteLine("}");
                        scrittore.WriteLine("");

                    }
                }
            }
            else MessageBox.Show("Nessuna istruzione!");
            label1.Select();
        }

        private void Arduino_Click_old(object sender, EventArgs e)
        {

            if (numeroPunti != 0) // calcolo punti
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Arduino file|*.ino";
                saveFileDialog1.Title = "Salva un file Arduino";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (System.IO.StreamWriter scrittore = new System.IO.StreamWriter(saveFileDialog1.FileName))
                    {
                        scrittore.WriteLine("//                                                                          Robot Studio");
                        scrittore.WriteLine("//                                                                     Developed by Luca Samar");
                        scrittore.WriteLine("//                                                                         MITS Malignani");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("#include <Servo.h>");
                        scrittore.WriteLine("Servo motore[6];");
                        scrittore.WriteLine("const int Input[] = { A1, A2, A3, A4, A5 };");
                        scrittore.WriteLine("const int Output[] = { 4, 7, 8, 12, 13 };");
                        scrittore.WriteLine("bool lettureIngressi[5];");
                        scrittore.WriteLine("bool setUscite[] = { 0, 0, 0, 0, 0 };");

                        int count = 0;
                        bool ancheist = false;
                        UltimoPunto = PuntoIniziale;
                        Linee.Clear();

                        foreach (Punti3D punto in Punti)
                        {
                            Linee.Add(new Programma(punto.vel, punto.acc) { });
                            if (punto.tipo == 0)
                            {
                                Linee[count].linea(UltimoPunto, punto);
                                UltimoPunto = punto;
                            }
                            else if (punto.tipo == 1)
                            {
                                Linee[count].joint(UltimoPunto, punto);
                                UltimoPunto = punto;
                            }
                            else if (punto.tipo == 2)
                            {
                                Linee[count].delay(punto.tempo, UltimoPunto);
                            }
                            else if (punto.tipo == 3)
                            {
                                Linee[count].istruzione(punto.Pin1, punto.Pin2, punto.Pin3, punto.Pin4, punto.Pin5, punto._default, punto.contatore, UltimoPunto);
                                ancheist = true;
                            }
                            count++;
                        }



                        String stringa = "const int numeroPunti = " + (numeroPunti).ToString() + ";";
                        scrittore.WriteLine(stringa);
                        stringa = "const int numeroStep[] = { ";
                        cont = 0;
                        int piualto = 0;
                        foreach (Programma punto in Linee)
                        {
                            cont++;
                            stringa += (!punto.ist ? punto.numStep.ToString() : 1.ToString()) + (cont < numeroPunti ? ", " : " };");
                            if (punto.numStep > piualto) piualto = (int)punto.numStep;
                        }
                        piualto++;
                        scrittore.WriteLine(stringa);

                        cont = 0;
                        cont2 = 0;

                        String M1 = "const byte posizioneMotore1[][" + piualto.ToString() + "] = {{";
                        String M2 = "const byte posizioneMotore2[][" + piualto.ToString() + "] = {{";
                        String M3 = "const byte posizioneMotore3[][" + piualto.ToString() + "] = {{";
                        String M4 = "const byte posizioneMotore4[][" + piualto.ToString() + "] = {{";
                        String M5 = "const byte posizioneMotore5[][" + piualto.ToString() + "] = {{";
                        String M6 = "const byte posizioneMotore6[][" + piualto.ToString() + "] = {{";

                        Robot.g1 = 0;
                        Robot.g2 = 0;
                        Robot.g3 = 0;
                        Robot.g4 = 0;
                        Robot.g5 = 0;
                        Robot.g6 = 0;

                        while (cont < numeroPunti)   //  esecuzione
                        {
                            if (cont2 < Linee[cont].numStep)
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    C[i] = Linee[cont].P[cont2][i];
                                }
                                cont2++;
                            }
                            else
                            {
                                cont2 = 0;
                                cont++;
                                if (cont < numeroPunti)
                                {
                                    M1 += "},{";
                                    M2 += "},{";
                                    M3 += "},{";
                                    M4 += "},{";
                                    M5 += "},{";
                                    M6 += "},{";
                                }
                            }
                            double[] a = new double[6];

                            Matematica Cinematica = new Matematica();
                            Cinematica.inversa(C, ref a, ref giunti);
                            Cinematica.diretta(giunti[0], giunti[1], giunti[2], giunti[3], giunti[4], giunti[5], ref a);
                            Robot.g1 = giunti[0];
                            Robot.g2 = giunti[1];
                            Robot.g3 = giunti[2];
                            Robot.g4 = giunti[3];
                            Robot.g5 = giunti[4];
                            Robot.g6 = giunti[5];
                            Cinematica.leva(ref Robot.ga1, ref Robot.ga2);

                            if (cont < numeroPunti)
                            {
                                M1 += ((int)giunti[0] + 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                                M2 += (180 - (int)giunti[1]).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                                M3 += (-(int)Robot.ga1 - 100).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                                M4 += ((int)giunti[3] + 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                                M5 += (200 - (int)giunti[4] - 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                                M6 += ((int)giunti[5] + 90).ToString() + (cont2 < Linee[cont].numStep ? "," : "");
                            }
                        }
                        M1 += "}};";
                        M2 += "}};";
                        M3 += "}};";
                        M4 += "}};";
                        M5 += "}};";
                        M6 += "}};";
                        scrittore.WriteLine(M1);
                        scrittore.WriteLine(M2);
                        scrittore.WriteLine(M3);
                        scrittore.WriteLine(M4);
                        scrittore.WriteLine(M5);
                        scrittore.WriteLine(M6);

                        int conteggioMomentaneo = 0;
                        int numeroIstruzioni = 0;
                        List<Programma> istruzioni = new List<Programma>();
                        for (int i = 0; i < numeroPunti; i++)
                        {
                            if (Linee[i].ist)
                            {
                                istruzioni.Add(Linee[i]);
                                numeroIstruzioni++;
                                scrittore.WriteLine("const int Istruzione" + numeroIstruzioni.ToString() + " = " + i.ToString() + ";");
                                conteggioMomentaneo++;
                            }
                            else
                                for (int u = 0; u < Linee[i].numStep; u++)
                                {
                                    conteggioMomentaneo++;
                                }
                        }
                        conteggioMomentaneo = 0;
                        numeroIstruzioni = 0;
                        for (int i = 0; i < numeroPunti; i++)
                        {
                            if (Linee[i].ist)
                            {
                                numeroIstruzioni++;
                                if (Linee[i].contatore[0] == 1) scrittore.WriteLine("int Contatore" + numeroIstruzioni.ToString() + " = 0;");
                                conteggioMomentaneo++;
                            }
                            else
                                for (int u = 0; u < Linee[i].numStep; u++)
                                {
                                    conteggioMomentaneo++;
                                }
                        }


                        scrittore.WriteLine("int conteggio1 = 0;");
                        scrittore.WriteLine("int conteggio2 = 0;");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("/*********************************************************************************************************************************************************************/");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("void setup() ");
                        scrittore.WriteLine("{");
                        scrittore.WriteLine("  for(int i = 0; i < 5; i++)");
                        scrittore.WriteLine("  {");
                        scrittore.WriteLine("    pinMode( Input[i], INPUT_PULLUP);");
                        scrittore.WriteLine("    pinMode( Output[i], OUTPUT);");
                        scrittore.WriteLine("  }");
                        scrittore.WriteLine("  motore[0].attach(3);");
                        scrittore.WriteLine("  motore[1].attach(5);");
                        scrittore.WriteLine("  motore[2].attach(6);");
                        scrittore.WriteLine("  motore[3].attach(9);");
                        scrittore.WriteLine("  motore[4].attach(10);");
                        scrittore.WriteLine("  motore[5].attach(11);");
                        scrittore.WriteLine("  motore[0].write(90);");
                        scrittore.WriteLine("  motore[1].write(90);");
                        scrittore.WriteLine("  motore[2].write(90);");
                        scrittore.WriteLine("  motore[3].write(90);");
                        scrittore.WriteLine("  motore[4].write(90);");
                        scrittore.WriteLine("  motore[5].write(90);");
                        scrittore.WriteLine("  delay(20);");
                        scrittore.WriteLine("}");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("/*********************************************************************************************************************************************************************/");
                        scrittore.WriteLine("");
                        scrittore.WriteLine("void loop() ");
                        scrittore.WriteLine("{");
                        scrittore.WriteLine("  if (conteggio2 >= numeroStep[conteggio1])");
                        scrittore.WriteLine("  {");
                        scrittore.WriteLine("    conteggio2 = 0;");
                        scrittore.WriteLine("    conteggio1++;");
                        scrittore.WriteLine("  }");
                        scrittore.WriteLine("  if (conteggio1 >= numeroPunti || conteggio1 < 0) conteggio1 = 0;");
                        scrittore.WriteLine("");
                        if (numeroIstruzioni != 0)
                        {
                            scrittore.WriteLine("  for(int i = 0; i < 5 ; i++)  lettureIngressi[i] = !digitalRead(Input[i]);");
                            scrittore.WriteLine("");
                            scrittore.WriteLine("  switch(conteggio1)");
                            scrittore.WriteLine("  {");
                            for (int i = 0; i < numeroIstruzioni; i++)
                            {
                                scrittore.WriteLine("    case Istruzione" + (i + 1).ToString() + ":");
                                String spazio2 = "";
                                if (istruzioni[i].contatore[0] == 1)
                                {
                                    scrittore.WriteLine("      Contatore" + (i + 1).ToString() + "++;");
                                    scrittore.WriteLine("      if(Contatore" + (i + 1).ToString() + " > " + istruzioni[i].contatore[1] + ")");
                                    scrittore.WriteLine("      {");
                                    scrittore.WriteLine("        conteggio1++;");
                                    scrittore.WriteLine("        Contatore" + (i + 1).ToString() + " = 0;");
                                    scrittore.WriteLine("      }");
                                    scrittore.WriteLine("      else");
                                    scrittore.WriteLine("      {");
                                    spazio2 = "  ";
                                }
                                bool almenouno = false;
                                if (istruzioni[i].Pin1[0] == 1)
                                {
                                    almenouno = true;
                                    scrittore.WriteLine("      " + spazio2 + "if(lettureIngressi[0])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin1[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin1[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin1[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin1[2].ToString() + ";"); break;
                                    }
                                    if (istruzioni[i].Pin1[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin1[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin1[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin1[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin1[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin1[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                }
                                if (istruzioni[i].Pin2[0] == 1)             // pin2
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[1])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin2[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin2[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin2[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin2[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin2[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin2[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin2[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin2[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin2[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin2[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                if (istruzioni[i].Pin3[0] == 1)             // pin3
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[2])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin3[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin3[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin3[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin3[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin3[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin3[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin3[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin3[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin3[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin3[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                if (istruzioni[i].Pin4[0] == 1)             // pin4
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[3])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin4[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin4[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin4[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin4[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin4[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin4[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin4[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin4[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin4[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin4[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                if (istruzioni[i].Pin5[0] == 1)             // pin5
                                {
                                    scrittore.WriteLine("      " + spazio2 + (almenouno ? "else " : "") + "if(lettureIngressi[4])");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    switch (istruzioni[i].Pin5[1])
                                    {
                                        case 0: scrittore.WriteLine("      " + spazio2 + "  conteggio1 = " + istruzioni[i].Pin5[2].ToString() + ";"); break;
                                        case 1: scrittore.WriteLine("       " + spazio2 + " conteggio1 += " + istruzioni[i].Pin5[2].ToString() + ";"); break;
                                        case 2: scrittore.WriteLine("       " + spazio2 + " conteggio1 -= " + istruzioni[i].Pin5[2].ToString() + ";"); break;
                                    }

                                    if (istruzioni[i].Pin5[3] == 1)
                                    {
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[0] = " + (istruzioni[i].Pin5[4] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("      " + spazio2 + "  setUscite[1] = " + (istruzioni[i].Pin5[5] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[2] = " + (istruzioni[i].Pin5[6] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("    " + spazio2 + "    setUscite[3] = " + (istruzioni[i].Pin5[7] == 0 ? "false" : "true") + ";");
                                        scrittore.WriteLine("     " + spazio2 + "   setUscite[4] = " + (istruzioni[i].Pin5[8] == 0 ? "false" : "true") + ";");
                                    }
                                    scrittore.WriteLine("      " + spazio2 + "} ");
                                    almenouno = true;
                                }
                                String spazio3 = "";
                                if (almenouno)                                                  // default
                                {
                                    scrittore.WriteLine("     " + spazio2 + " else");
                                    scrittore.WriteLine("      " + spazio2 + "{ ");
                                    spazio3 = "  ";
                                }
                                switch (istruzioni[i]._default[1])
                                {
                                    case 0: scrittore.WriteLine("      " + spazio2 + spazio3 + "  conteggio1 = " + istruzioni[i]._default[2].ToString() + ";"); break;
                                    case 1: scrittore.WriteLine("     " + spazio2 + spazio3 + " conteggio1 += " + istruzioni[i]._default[2].ToString() + ";"); break;
                                    case 2: scrittore.WriteLine("     " + spazio2 + spazio3 + " conteggio1 -= " + istruzioni[i]._default[2].ToString() + ";"); break;
                                }
                                if (istruzioni[i]._default[3] == 1)
                                {
                                    scrittore.WriteLine("    " + spazio2 + spazio3 + "  setUscite[0] = " + (istruzioni[i]._default[4] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("    " + spazio2 + spazio3 + "  setUscite[1] = " + (istruzioni[i]._default[5] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("   " + spazio2 + spazio3 + "   setUscite[2] = " + (istruzioni[i]._default[6] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("  " + spazio2 + spazio3 + "    setUscite[3] = " + (istruzioni[i]._default[7] == 0 ? "false" : "true") + ";");
                                    scrittore.WriteLine("   " + spazio2 + spazio3 + "   setUscite[4] = " + (istruzioni[i]._default[8] == 0 ? "false" : "true") + ";");
                                }
                                if (almenouno) scrittore.WriteLine("      " + spazio2 + "} ");
                                if (istruzioni[i].contatore[0] == 1) scrittore.WriteLine("      } ");
                                scrittore.WriteLine("      break;");
                                scrittore.WriteLine("");
                            }
                            scrittore.WriteLine("    default:");
                            scrittore.WriteLine("      motore[0].write(posizioneMotore1[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[1].write(posizioneMotore2[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[2].write(posizioneMotore3[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[3].write(posizioneMotore4[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[4].write(posizioneMotore5[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      motore[5].write(posizioneMotore6[conteggio1][conteggio2]);");
                            scrittore.WriteLine("      conteggio2++;");
                            scrittore.WriteLine("      break;");
                            scrittore.WriteLine("  }");
                        }
                        else
                        {
                            scrittore.WriteLine("  motore[0].write(posizioneMotore1[conteggio1][conteggio2]);");
                            scrittore.WriteLine("  motore[1].write(posizioneMotore2[conteggio1][conteggio2]);");
                            scrittore.WriteLine("  motore[2].write(posizioneMotore3[conteggio1][conteggio2]);");
                            scrittore.WriteLine("  motore[3].write(posizioneMotore4[conteggio1][conteggio2]);");
                            scrittore.WriteLine("  motore[4].write(posizioneMotore5[conteggio1][conteggio2]);");
                            scrittore.WriteLine("  motore[5].write(posizioneMotore6[conteggio1][conteggio2]);");
                            scrittore.WriteLine("  conteggio2++;");
                        }
                        if (numeroIstruzioni != 0) scrittore.WriteLine("  for(int i = 0; i < 5 ; i++)  digitalWrite(Output[i], setUscite[i]);");
                        scrittore.WriteLine("  delay(20);");
                        scrittore.WriteLine("}");
                        scrittore.WriteLine("");

                    }
                }
            }
            else MessageBox.Show("Nessuna istruzione!");
            label1.Select();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public static class Robot
    {
        public static bool visione2D = false;
        public static int scala = 15;


        public static double a1 = 450;
        public static double a2 = 1170; // lunghezza DH 
        public static double a3 = 180;
        public static double a4 = 0;
        public static double a5 = 0;
        public static double a6 = 0;
        public static double d1 = 802.33;
        public static double d2 = 0;
        public static double d3 = 0;
        public static double d4 = 1176;
        public static double d5 = 0;
        public static double d6 = 220; // lunghezza DH
        public static double asta1 = 380; // leva inferiore
        public static double asta2 = 1170; // asta parallela
        public static double asta3 = 310.64; // leva superior
        public static double lato1; // distanza tra la fine del link2 e il centro polso
        public static int tool = 90; // lunghezza tool

        public static double[,] T01 = new double[4, 4];
        public static double[,] T12 = new double[4, 4];
        public static double[,] T23 = new double[4, 4];
        public static double[,] T34 = new double[4, 4];
        public static double[,] T45 = new double[4, 4];
        public static double[,] T56 = new double[4, 4];

        public static double[,] T02 = new double[4, 4];
        public static double[,] T03 = new double[4, 4];
        public static double[,] T04 = new double[4, 4];
        public static double[,] T05 = new double[4, 4];
        public static double[,] T06 = new double[4, 4];

        public static double g1;
        public static double g2;
        public static double g3;
        public static double g4;
        public static double g5;
        public static double g6;

        public static double ga1; // angolo del asta del asse tre
        public static double ga2; // angolo tra le due aste

    }

    public class Matematica
    {

        public Matematica()
        {
            Robot.lato1 = Math.Sqrt(Math.Pow(Robot.a3, 2) + Math.Pow(Robot.d4, 2));
        }

        public void diretta(double q1, double q2, double q3,
            double q4,double q5, double q6, ref double[] C)
        {
            //    da Denavit Hartemberg a matrici di rototraslazione 
            //      A       D      𝜗𝑖     𝛼𝑖      riferimento
            Q2T(Robot.a1, Robot.d1, q1, 90, ref Robot.T01);
            Q2T(Robot.a2, 0, q2, 0, ref Robot.T12);
            Q2T(Robot.a3, 0, q3 - 90, 90, ref Robot.T23);
            Q2T(0, Robot.d4, q4, -90, ref Robot.T34);
            Q2T(0, 0, q5, 90, ref Robot.T45);
            Q2T(0, (Robot.d6 + Robot.tool), q6, 0, ref Robot.T56);

            // moltiplicazioni
            TxT(Robot.T01, Robot.T12, ref Robot.T02);
            TxT(Robot.T02, Robot.T23, ref Robot.T03);
            TxT(Robot.T03, Robot.T34, ref Robot.T04);
            TxT(Robot.T04, Robot.T45, ref Robot.T05);
            TxT(Robot.T05, Robot.T56, ref Robot.T06);

            C[0] = Robot.T06[0, 3];
            C[1] = Robot.T06[1, 3];
            C[2] = Robot.T06[2, 3];

            R2RPY(Robot.T06, ref C);
        }

        public void inversa(double[] qc, ref double[] M, ref double[] qj, double[] last_qj = null )// X , Y , Z , ψ , θ , φ , M
        {
            double[] q = new double[6];
            double[] RPY = new double[3];

            RPY[0] = qc[3] * 0.01745329251994;   // ψ
            RPY[1] = qc[4] * 0.01745329251994;   // θ
            RPY[2] = qc[5] * 0.01745329251994;   // φ

            double[,] R06 = new double[3, 3];
            RPY2R(RPY, ref R06);

            Punti3D PW = new Punti3D();
            double tool_d6 = Robot.tool + Robot.d6;

            PW.X = qc[0] - (R06[0, 2] * (tool_d6));          //  calcolo centro polso
            PW.Y = qc[1] - (R06[1, 2] * (tool_d6));
            PW.Z = qc[2] - (R06[2, 2] * (tool_d6));

            q[0] = Math.Atan(PW.Y / PW.X);       // trovo j1

            Punti3D P0 = new Punti3D(); // punto di j2
            P0.X = Robot.a1 * Math.Cos(q[0]);
            P0.Y = Robot.a1 * Math.Sin(q[0]);
            P0.Z = Robot.d1;

            double lato2 = Punti3D.norm(PW, P0); // normale tra centropolso e punti di j2

            double q3_1 = Carnot(Robot.lato1, Robot.a2, lato2);
            double q2_1 = Math.Asin((PW.Z - P0.Z) / lato2);
            double q2_2 = Carnot(Robot.a2, lato2, Robot.lato1);
            q[1] = q2_1 + q2_2;
            double q3_2 = Math.Atan(Robot.a3 / Robot.d4);
            q[2] = q3_1 - q3_2;

            double[,] R03 = new double[3, 3];

            Q2R(q[0], q[1], q[2], ref R03);

            double[,] R03t = new double[3, 3];
            RT(R03, ref R03t);

            double[,] R36 = new double[3, 3];
            RxR(R03t, R06, ref R36);

            double[] Eulero = new double[3];
            R2E(R36, ref Eulero);


            double[] qj_temp1 = new double[6];
            qj_temp1[3] = Eulero[0];
            qj_temp1[4] = Eulero[1];
            qj_temp1[5] = Eulero[2];

            double d1 = 0;
            double d2 = 0;
            for (int i = 3; i < 6; i++)
            {
                if (qj_temp1[i] > Math.PI)
                    qj_temp1[i] -= 2 * Math.PI;

                if (qj_temp1[i] < -Math.PI)
                    qj_temp1[i] += 2 * Math.PI;

                d1 += Math.Abs(qj_temp1[i]);
            }

            double[] qj_temp2 = new double[6];
            qj_temp2[3] = Eulero[0] +  Math.PI;
            qj_temp2[4] = -Eulero[1];
            qj_temp2[5] = Eulero[2] + Math.PI;

            for (int i = 3; i < 6; i++)
            {
                if (qj_temp2[i] > Math.PI)
                    qj_temp2[i] -= 2 * Math.PI;

                if (qj_temp2[i] < -Math.PI)
                    qj_temp2[i] += 2 * Math.PI;

                d2 += Math.Abs(qj_temp2[i]);
            }
            if (d1 > d2)
            {
                q[3] = qj_temp2[3];
                q[4] = qj_temp2[4];
                q[5] = qj_temp2[5];
            }
            else
            {
                q[3] = qj_temp1[3];
                q[4] = qj_temp1[4];
                q[5] = qj_temp1[5];
            }


            qj = q;

            Q2M(q, ref M);

        }

        double j3_1, xy, l1, l2, d, ang1, ang2, ang3, x, y, z;

        public void leva(ref double g1, ref double g2)
        {
            x = Robot.T02[0, 3] - Robot.T01[0, 3];
            y = Robot.T02[1, 3] - Robot.T01[1, 3];
            z = Robot.T02[2, 3] - Robot.T01[2, 3];
            j3_1 = Robot.g2 * 0.01745329251994 + (Robot.g3) *
                0.01745329251994 + 3.69 * 0.01745329251994;
            xy = Math.Sqrt(x * x + y * y);
            if (Robot.g2 > 90) xy = -xy;
            l1 = xy + Math.Cos(j3_1) * Robot.asta3;
            l2 = z + Math.Sin(j3_1) * Robot.asta3;
            d = Math.Sqrt(l1 * l1 + l2 * l2); 
            ang1 = Carnot(d, Robot.a2, Robot.asta3);
            ang2 = Carnot(d, Robot.asta1, Robot.asta2); 
            ang3 = Carnot(Robot.asta2, Robot.asta1, d); 
            g1 = ang1 * 57.2957795131 + ang2 * 57.2957795131 
                + Robot.g2;
            g2 = ang3 * 57.2957795131;
            g2 += 90;   
            g1 = -g1;   
            g2 = -g2;   
        }

        public static double Carnot(double a, double b, double c)
        {
            if (a >= b + c || b >= a + c || c >= a + b)
                throw new EccezioneMatematica();
            double cos = (Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2)) / (2 * b * a);
            return Math.Acos(cos);
        }

        public static void R2E(double[,] R, ref double[] E)
        {
            E[0] = Math.Atan2(R[1, 2], R[0, 2]);
            E[1] = Math.Atan2(Math.Sqrt(Math.Pow(R[0, 2], 2) + Math.Pow(R[1, 2], 2)), R[2, 2]);
            E[2] = Math.Atan2(R[2, 1], -R[2, 0]);
        }

        public static void RPY2R(double[] E, ref double[,] R)
        {

            double cos_rollψ = Math.Cos(E[0]);   // ψ               // Seni e coseni
            double cos_pitchθ = Math.Cos(E[1]); // θ
            double cos_yawφ = Math.Cos(E[2]);     // φ

            double sen_rollψ = Math.Sin(E[0]);   // ψ
            double sen_pitchθ = Math.Sin(E[1]); // θ
            double sen_yawφ = Math.Sin(E[2]);     // φ

            R[0, 0] = cos_yawφ * cos_pitchθ;
            R[0, 1] = (cos_yawφ * sen_pitchθ * sen_rollψ) - (sen_yawφ * cos_rollψ);          // matrice di rotazione
            R[0, 2] = (cos_yawφ * sen_pitchθ * cos_rollψ) + (sen_yawφ * sen_rollψ);
            R[1, 0] = sen_yawφ * cos_pitchθ;
            R[1, 1] = (sen_pitchθ * sen_yawφ * sen_rollψ) + (cos_yawφ * cos_rollψ);
            R[1, 2] = (sen_yawφ * sen_pitchθ * cos_rollψ) - (cos_yawφ * sen_rollψ);
            R[2, 0] = -sen_pitchθ;
            R[2, 1] = cos_pitchθ * sen_rollψ;
            R[2, 2] = cos_pitchθ * cos_rollψ;

            R = Ry(R, 90);

        }

        public static void ZYZ2R(double[] E, ref double[,] R)
        {
            double cos_rollψ = Math.Cos(E[2]);   // ψ               // Seni e coseni
            double cos_pitchθ = Math.Cos(E[1]); // θ
            double cos_yawφ = Math.Cos(E[0]);     // φ

            double sen_rollψ = Math.Sin(E[2]);   // ψ
            double sen_pitchθ = Math.Sin(E[1]); // θ
            double sen_yawφ = Math.Sin(E[0]);     // φ

            R[0, 0] = (cos_yawφ * cos_pitchθ * cos_rollψ) - (sen_yawφ * sen_rollψ);
            R[0, 1] = -(cos_yawφ * cos_pitchθ * sen_rollψ) - (sen_yawφ * cos_rollψ);          // matrice di rotazione
            R[0, 2] = cos_yawφ * sen_pitchθ;
            R[1, 0] = (sen_yawφ * cos_pitchθ * cos_rollψ) + (cos_yawφ * sen_rollψ);
            R[1, 1] = -(sen_yawφ * cos_pitchθ * sen_rollψ) + (cos_yawφ * cos_rollψ);
            R[1, 2] = sen_yawφ + sen_pitchθ;
            R[2, 0] = -sen_pitchθ * cos_rollψ;
            R[2, 1] = sen_pitchθ * sen_rollψ;
            R[2, 2] = cos_pitchθ;

        }
        public static void R2ZYZ(double[,] R, ref double[] ZYZ)
        {
            ZYZ[4] = Math.Atan2(Math.Sqrt(Math.Pow(R[1, 2], 2) + Math.Pow(R[0, 2], 2)), R[2, 2]);
            if (ZYZ[4] < 1.5707 && ZYZ[4] >= 0)
            {
                ZYZ[4] = ZYZ[4] * 57.2957795131;
                ZYZ[3] = Math.Atan2(R[1, 2], R[0, 2]) * 57.2957795131;
                ZYZ[5] = Math.Atan2(R[2, 1], -R[2, 0]) * 57.2957795131;
            }
            else if (ZYZ[4] > -1.5707 && ZYZ[4] < 0)
            {
                ZYZ[4] = Math.Atan2(-Math.Sqrt(Math.Pow(R[1, 2], 2) + Math.Pow(R[0, 2], 2)), R[2, 2]) * 57.2957795131;
                ZYZ[3] = Math.Atan2(-R[1, 2], -R[0, 2]) * 57.2957795131;
                ZYZ[5] = Math.Atan2(-R[2, 1], -R[2, 0]) * 57.2957795131;
            }
        }

        public static void R2RPY(double[,] R, ref double[] RPY)
        {
            R = Ry(R, -90);
            RPY[4] = Math.Atan2(-R[2, 0], Math.Sqrt(Math.Pow(R[2, 1], 2)
                + Math.Pow(R[2, 2], 2))) * 57.2957795131; 
            RPY[3] = Math.Atan2(R[2, 1], R[2, 2]) * 57.2957795131;
            RPY[5] = Math.Atan2(R[1, 0], R[0, 0]) * 57.2957795131;
        }

        public void Q2M(double[] J, ref double[] M)
        {

            for (int i = 0; i < 6; i++) J[i] = J[i] * 57.2957795131;     // passo da radianti in gradi    

            M[0] = (int)(J[0] + 90);// c2 = a2 + b2 -2AB cosy
            M[1] = (int)J[1];

            double D = Math.Sqrt(Math.Pow(Robot.a2, 2) + Math.Pow(Robot.asta1, 2) + (2 * Robot.a2 * Robot.asta1 * Math.Cos((180 - J[3]) * 0.017453292519943))); // c2 = a2 + b2 -2AB cosy  (formula due lati e angolo compreso)

        }

        public static void RxR(double[,] R1, double[,] R2, ref double[,] R3)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int u = 0; u < 3; u++)
                {
                    R3[i, u] = R1[i, 0] * R2[0, u] + R1[i, 1] * R2[1, u] + R1[i, 2] * R2[2, u];
                }
            }
        }

        public static double[,] RxR(double[,] R1, double[,] R2)
        {
            double[,] R3 = new double[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int u = 0; u < 3; u++)
                {
                    R3[i, u] = R1[i, 0] * R2[0, u] + R1[i, 1] * R2[1, u] + R1[i, 2] * R2[2, u];
                }
            }

            return R3;
        }

        public static void RT(double[,] R, ref double[,] Rt)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int u = 0; u < 3; u++)
                {
                    Rt[i, u] = R[u, i];
                }
            }
        }

        public void Q2R(double q1, double q2, double q3, ref double[,] R)
        {
            q1 = q1 * 57.2957795131;
            q2 = q2 * 57.2957795131;
            q3 = q3 * 57.2957795131;

            Q2T(Robot.a1, Robot.d1, q1, 90, ref Robot.T01);
            Q2T(Robot.a2, 0, q2, 0, ref Robot.T12);
            Q2T(Robot.a3, 0, q3 - 90, 90, ref Robot.T23);

            TxT(Robot.T01, Robot.T12, ref Robot.T02);
            TxT(Robot.T02, Robot.T23, ref Robot.T03);

            for (int i = 0; i < 3; i++)
            {
                for (int u = 0; u < 3; u++)
                {
                    R[i, u] = Robot.T03[i, u];
                }
            }

        }

        public static void Q2T(double ai, double di, double θi,
            double αi, ref double[,] T)
        {
            double cos_θ = Math.Cos(θi * 0.01745329251994);
            double sin_θ = Math.Sin(θi * 0.01745329251994);
            double cos_α = Math.Cos(αi * 0.01745329251994);
            double sin_α = Math.Sin(αi * 0.01745329251994);

            T[0, 0] = cos_θ;
            T[0, 1] = -cos_α * sin_θ;
            T[0, 2] = sin_α * sin_θ;
            T[0, 3] = ai * cos_θ;
            T[1, 0] = sin_θ;
            T[1, 1] = cos_θ * cos_α;
            T[1, 2] = -sin_α * cos_θ;
            T[1, 3] = ai * sin_θ;
            T[2, 0] = 0;
            T[2, 1] = sin_α;
            T[2, 2] = cos_α;
            T[2, 3] = di;
            T[3, 0] = 0;
            T[3, 1] = 0;
            T[3, 2] = 0;
            T[3, 3] = 1;
        }

        public static void TxT(double[,] T1, double[,] T2, ref double[,] T3)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int u = 0; u < 4; u++)
                {
                    T3[i, u] = T1[i, 0] * T2[0, u] + T1[i, 1] * T2[1, u] + 
                        T1[i, 2] * T2[2, u] + T1[i, 3] * T2[3, u];
                }
            }
        }

        public static void Tt(double[,] T1, ref double[,] T2)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int u = 0; u < 4; u++)
                {
                    T1[i, u] = T2[u, i];
                }
            }
        }

        public static void Rx(double[,] R, double angolo, ref double[,] Rx)
        {
            double rad_angolo = angolo * 0.01745329251994;
            double cos_angolo = Math.Cos(rad_angolo);
            double sin_angolo = Math.Sin(rad_angolo);

            double[,] RX = new double[3, 3];
            RX[0, 0] = 1;
            RX[0, 1] = 0;
            RX[0, 2] = 0;
            RX[1, 0] = 0;
            RX[1, 1] = cos_angolo;
            RX[1, 2] = -sin_angolo;
            RX[2, 0] = 0;
            RX[2, 1] = sin_angolo;
            RX[2, 2] = cos_angolo;

            RxR(R, RX, ref Rx);
        }
        public static double[,] Rx(double[,] R, double angolo)
        {
            double rad_angolo = angolo * 0.01745329251994;
            double cos_angolo = Math.Cos(rad_angolo);
            double sin_angolo = Math.Sin(rad_angolo);

            double[,] RX = new double[3, 3];
            RX[0, 0] = 1;
            RX[0, 1] = 0;
            RX[0, 2] = 0;
            RX[1, 0] = 0;
            RX[1, 1] = cos_angolo;
            RX[1, 2] = -sin_angolo;
            RX[2, 0] = 0;
            RX[2, 1] = sin_angolo;
            RX[2, 2] = cos_angolo;

            return RxR(R, RX);
        }
        public static void Ry(double[,] R, double angolo, ref double[,] Ry)
        {
            double rad_angolo = angolo * 0.01745329251994;
            double cos_angolo = Math.Cos(rad_angolo);
            double sin_angolo = Math.Sin(rad_angolo);

            double[,] RY = new double[3, 3];
            RY[0, 0] = cos_angolo;
            RY[0, 1] = 0;
            RY[0, 2] = sin_angolo;
            RY[1, 0] = 0;
            RY[1, 1] = 1;
            RY[1, 2] = 0;
            RY[2, 0] = -sin_angolo;
            RY[2, 1] = 0;
            RY[2, 2] = cos_angolo;

            RxR(R, RY, ref Ry);
        }
        public static double[,] Ry(double[,] R, double angolo)
        {
            double rad_angolo = angolo * 0.01745329251994;
            double cos_angolo = Math.Cos(rad_angolo);
            double sin_angolo = Math.Sin(rad_angolo);

            double[,] RY = new double[3, 3];
            RY[0, 0] = cos_angolo;
            RY[0, 1] = 0;
            RY[0, 2] = sin_angolo;
            RY[1, 0] = 0;
            RY[1, 1] = 1;
            RY[1, 2] = 0;
            RY[2, 0] = -sin_angolo;
            RY[2, 1] = 0;
            RY[2, 2] = cos_angolo;

            return RxR(R, RY);
        }
        public static void Rz(double[,] R, double angolo, ref double[,] Rz)
        {
            double rad_angolo = angolo * 0.01745329251994;
            double cos_angolo = Math.Cos(rad_angolo);
            double sin_angolo = Math.Sin(rad_angolo);

            double[,] RZ = new double[3, 3];
            RZ[0, 0] = cos_angolo;
            RZ[0, 1] = -sin_angolo;
            RZ[0, 2] = 0;
            RZ[1, 0] = sin_angolo;
            RZ[1, 1] = cos_angolo;
            RZ[1, 2] = 0;
            RZ[2, 0] = 0;
            RZ[2, 1] = 0;
            RZ[2, 2] = 1;

            RxR(R, RZ, ref Rz);
        }
        public static double[,] Rz(double[,] R, double angolo)
        {
            double rad_angolo = angolo * 0.01745329251994;
            double cos_angolo = Math.Cos(rad_angolo);
            double sin_angolo = Math.Sin(rad_angolo);

            double[,] RZ = new double[3, 3];
            RZ[0, 0] = cos_angolo;
            RZ[0, 1] = -sin_angolo;
            RZ[0, 2] = 0;
            RZ[1, 0] = sin_angolo;
            RZ[1, 1] = cos_angolo;
            RZ[1, 2] = 0;
            RZ[2, 0] = 0;
            RZ[2, 1] = 0;
            RZ[2, 2] = 1;

            return RxR(R, RZ);
        }
    }

    public class Punti3D
    {
        public int tempo;
        public int tipo; // 0 linea   1 joint  2 timer 3 istruzione 
        public double acc;
        public double vel;
        public double tool;
        // prima cifra se è abilitato , secondo se incrementa o no , terzo il valore di incrementazione , quarto se abilitato DO altri le uscite
        public int[] Pin1 = new int[9]; 
        public int[] Pin2 = new int[9]; 
        public int[] Pin3 = new int[9]; 
        public int[] Pin4 = new int[9]; 
        public int[] Pin5 = new int[9]; 
        public int[] _default = new int[9];
        public int[] contatore = new int[2];

        public static double[] P2Arr(Punti3D P)
        {
            double[] Arr = new double[6];
            Arr[0] = P.X;
            Arr[1] = P.Y;
            Arr[2] = P.Z;
            Arr[3] = P.A;
            Arr[4] = P.B;
            Arr[5] = P.C;

            return Arr;
        }

        public static void Arr2P(double[] Arr, ref Punti3D P)
        {
            P.X = Arr[0];
            P.Y = Arr[1];
            P.Z = Arr[2];
            P.A = Arr[3];
            P.B = Arr[4];
            P.C = Arr[5];
        }
        public static Punti3D Arr2P(double[] Arr)
        {
            Punti3D P = new Punti3D();
            P.X = Arr[0];
            P.Y = Arr[1];
            P.Z = Arr[2];
            P.A = Arr[3];
            P.B = Arr[4];
            P.C = Arr[5];
            return P;
        }
        public Punti3D()
        {
        }

        public Punti3D(double[] Coordinate)
        {
            this.X = Coordinate[0];
            this.Y = Coordinate[1];
            this.Z = Coordinate[2];
            this.A = Coordinate[3];
            this.B = Coordinate[4];
            this.C = Coordinate[5];
        }

        public Punti3D(double x, double y, double z, double a, double b, double c)
        {
            X = x;
            Y = y;
            Z = z;
            A = a;
            B = b;
            C = c;
        }

        public double X, Y, Z, A, B, C;

        public static double norm(Punti3D P0, Punti3D P1)
        {
            double l = Math.Sqrt(Math.Pow(P1.X - P0.X, 2) + Math.Pow(P1.Y - P0.Y, 2) + Math.Pow(P1.Z - P0.Z, 2));
            return l;
        }

        public static double normJ(Punti3D P0, Punti3D P1)
        {
            double[] RPY = new double[3];
            double[] qc;
            qc = P2Arr(P0);
            RPY[0] = qc[3] * 0.01745329251994;    // ψ
            RPY[1] = qc[4] * 0.01745329251994;   // θ
            RPY[2] = qc[5] * 0.01745329251994;     // φ

            double[,] R06 = new double[3, 3];
            Matematica.RPY2R(RPY, ref R06);

            Punti3D PW = new Punti3D();
            double tool_d6 = Robot.tool + Robot.d6;

            PW.X = qc[0] - (R06[0, 2] * (tool_d6));          //  calcolo centro polso
            PW.Y = qc[1] - (R06[1, 2] * (tool_d6));
            PW.Z = qc[2] - (R06[2, 2] * (tool_d6));


            double[] RPY2 = new double[3];
            double[] qc2;
            qc2 = P2Arr(P1);
            RPY2[0] = qc2[3] * 0.01745329251994;    // ψ
            RPY2[1] = qc2[4] * 0.01745329251994;   // θ
            RPY2[2] = qc2[5] * 0.01745329251994;     // φ

            double[,] R062 = new double[3, 3];
            Matematica.RPY2R(RPY2, ref R062);

            Punti3D PW2 = new Punti3D();

            PW2.X = qc2[0] - (R062[0, 2] * (tool_d6));          //  calcolo centro polso
            PW2.Y = qc2[1] - (R062[1, 2] * (tool_d6));
            PW2.Z = qc2[2] - (R062[2, 2] * (tool_d6));

            return norm(PW, PW2);

        }
    }

    public class Programma
    {

        private double acc, vel,tool;
        public double numStep;
        public List<double[]> P = new List<double[]>();

        public bool ist = false;
        public int[] Pin1 = new int[9];  // prima cifra se è abilitato , secondo se incrementa o no , terzo il valore di incrementazione , quarto se abilitato DO altri le uscite
        public int[] Pin2 = new int[9];
        public int[] Pin3 = new int[9]; 
        public int[] Pin4 = new int[9];
        public int[] Pin5 = new int[9]; 
        public int[] _default = new int[9];
        public int[] contatore = new int[2];
        public int tempo;
        public int conteggio = 0;

        public Programma(double a, double v)
        {
            this.acc = a;
            this.vel = v;
        }

        public void linea(Punti3D p0, Punti3D p1)
        {
            double Tacc, Sacc, linea, lineaPw, Pacc, Preg;
            double Dx, Dy, Dz, Da, Db, Dc,Dt;
            double ultimapos = 0;
            double ultimostep = 0;
            double step = 0;
            byte M = 0;
            double X, Y, Z, A, B, C,Tool;

            Tacc = this.vel / this.acc;
            Sacc = 0.5 * this.acc * Math.Pow(Tacc, 2);

            lineaPw = Punti3D.normJ(p0, p1) / 1000;
            linea = Punti3D.norm(p0, p1) / 1000;  // porto in metri 
            Pacc = Sacc / (linea > lineaPw ? linea : lineaPw);
            if (Pacc >= 0.5)
            {
                Pacc = 0.5;
                Preg = Pacc;
                this.vel = Math.Sqrt(2 * this.acc * ((linea > lineaPw ? linea : lineaPw) / 2));
            }
            else Preg = 1 - Pacc;

            Dx = p1.X - p0.X;
            Dy = p1.Y - p0.Y;
            Dz = p1.Z - p0.Z;
            Dt = p1.tool - p0.tool;
         
            Da = p1.A - p0.A;
            Db = p1.B - p0.B;
            Dc = p1.C - p0.C;

            if (Da > 180 ) Da = (-(360 - Da)) % 360;
            else if (Da < -180) Da = (360 + Da) %360;

            if (Db > 180) Db = (-(360 - Db)) % 360;
            else if( Db < -180) Db = (360 + Db) % 360;

            if (Dc > 180) Dc = (-(360 - Dc)) % 360;
            else if(Dc < -180) Dc = (360 + Dc) % 360;

            double pos = 0;

            List<double[]> temp_instr = new List<double[]>();

            while (pos < 1)
            {
                double p;

                switch (M)
                {
                    case 0:
                        p = 0.5 * this.acc * Math.Pow(step * 0.02, 2);
                        pos = p / (linea > lineaPw ? linea : lineaPw);

                        X = pos * Dx + p0.X;
                        Y = pos * Dy + p0.Y;
                        Z = pos * Dz + p0.Z;
                        A = pos * Da + p0.A;
                        B = pos * Db + p0.B;
                        C = pos * Dc + p0.C;
                        step++;

                        if (pos >= Pacc)
                        {
                            M++;
                            ultimapos = p;
                            ultimostep = step;
                        }
                        temp_instr.Add(new double[6] { X, Y, Z, A, B, C });
                        break;

                    case 1:
                        p = vel * (step - ultimostep) * 0.02 + ultimapos;
                        pos = p / (linea > lineaPw ? linea : lineaPw);

                        X = pos * Dx + p0.X;
                        Y = pos * Dy + p0.Y;
                        Z = pos * Dz + p0.Z;
                        A = pos * Da + p0.A;
                        B = pos * Db + p0.B;
                        C = pos * Dc + p0.C;

                        step++;

                        if (pos >= Preg)
                        {
                            M++;
                            ultimapos = p;
                            ultimostep = step;
                        }
                        temp_instr.Add(new double[6] { X, Y, Z, A, B, C });
                        break;

                    case 2:
                        p = (vel * (step - ultimostep) * 0.02) - (0.5 * (this.acc * Math.Pow((step - ultimostep) * 0.02, 2))) + ultimapos;
                        pos = p / (linea > lineaPw ? linea : lineaPw);

                        X = pos * Dx + p0.X;
                        Y = pos * Dy + p0.Y;
                        Z = pos * Dz + p0.Z;
                        A = pos * Da + p0.A;
                        B = pos * Db + p0.B;
                        C = pos * Dc + p0.C;

                        step++;
                        temp_instr.Add(new double[6] { X, Y, Z, A, B, C });
                        break;
                }

            }

            //process_points(ref temp_instr, n_instr_for_rac);
            this.P.AddRange(temp_instr);
            this.P.Add(new double[6] { p1.X, p1.Y, p1.Z, p1.A, p1.B, p1.C });
            this.numStep = ++step;

        }
        int n_instr_for_rac = 3;

        public void joint(Punti3D p0, Punti3D p1)
        {
            this.vel = this.vel * 100;
            this.acc = this.acc * 100;

            double Tacc, Sacc, joint = 0, Pacc, Preg;
            double D_1, D_2, D_3, D_4, D_5, D_6;
            double D1, D2, D3, D4, D5, D6;
            double[] Cart = new double[6];

            double ultimapos = 0;
            double ultimostep = 0;
            double step = 0;
            byte M = 0;
            double J1, J2, J3, J4, J5, J6;

            Matematica j = new Matematica();
            double[] a = new double[6];
            double[] c1 = new double[6];
            double[] c2 = new double[6];
            double[] P1 = new double[6];
            double[] P2 = new double[6];

            P1[0] = p0.X;
            P1[1] = p0.Y;
            P1[2] = p0.Z;
            P1[3] = p0.A;
            P1[4] = p0.B;
            P1[5] = p0.C;

            P2[0] = p1.X;
            P2[1] = p1.Y;
            P2[2] = p1.Z;
            P2[3] = p1.A;
            P2[4] = p1.B;
            P2[5] = p1.C;

            j.inversa(P1, ref a, ref c1);
            j.inversa(P2, ref a, ref c2);

            D_1 = Math.Pow(c2[0] - c1[0], 2);
            D_2 = Math.Pow(c2[1] - c1[1], 2);
            D_3 = Math.Pow(c2[2] - c1[2], 2);
            D_4 = Math.Pow(c2[3] - c1[3], 2);
            D_5 = Math.Pow(c2[4] - c1[4], 2);
            D_6 = Math.Pow(c2[5] - c1[5], 2);

            D1 = c2[0] - c1[0];
            D2 = c2[1] - c1[1];
            D3 = c2[2] - c1[2];
            D4 = c2[3] - c1[3];
            D5 = c2[4] - c1[4];
            D6 = c2[5] - c1[5];

            if (D_1 >= D_2 && D_1 >= D_3 && D_1 >= D_4 && D_1 >= D_5 && D_1 >= D_6) joint = D1;
            else if (D_2 >= D_1 && D_2 >= D_3 && D_2 >= D_4 && D_2 >= D_5 && D_2 >= D_6) joint = D2;
            else if (D_3 >= D_1 && D_3 >= D_2 && D_3 >= D_4 && D_3 >= D_5 && D_3 >= D_6) joint = D3;
            else if (D_4 >= D_1 && D_4 >= D_2 && D_4 >= D_3 && D_4 >= D_5 && D_4 >= D_6) joint = D4;
            else if (D_5 >= D_1 && D_5 >= D_2 && D_5 >= D_3 && D_5 >= D_4 && D_5 >= D_6) joint = D5;
            else if (D_6 >= D_1 && D_6 >= D_2 && D_6 >= D_3 && D_6 >= D_4 && D_6 >= D_5) joint = D6;

            if (joint < 0) joint = -joint;

            Tacc = this.vel / this.acc;
            Sacc = 0.5 * this.acc * Math.Pow(Tacc, 2);
            Pacc = Sacc / joint;

            List<double[]> temp_instr = new List<double[]>();

            if (Pacc >= 0.5)
            {
                Pacc = 0.5;
                Preg = Pacc;
                this.vel = Math.Sqrt(2 * this.acc * (joint / 2));
            }
            else Preg = 1 - Pacc;

            double pos = 0;

            while (pos < 1)
            {
                double p;

                switch (M)
                {
                    case 0:
                        p = 0.5 * this.acc * Math.Pow(step * 0.02, 2);
                        pos = p / joint;

                        J1 = pos * D1 + c1[0];
                        J2 = pos * D2 + c1[1];
                        J3 = pos * D3 + c1[2];
                        J4 = pos * D4 + c1[3];
                        J5 = pos * D5 + c1[4];
                        J6 = pos * D6 + c1[5];
                        step++;

                        if (pos >= Pacc)
                        {
                            M++;
                            ultimapos = p;
                            ultimostep = step;
                        }
                        j.diretta(J1, J2, J3, J4, J5, J6, ref Cart);
                        temp_instr.Add(new double[6] { Cart[0], Cart[1], Cart[2], Cart[3], Cart[4], Cart[5] });
                        break;

                    case 1:
                        p = vel * (step - ultimostep) * 0.02 + ultimapos;
                        pos = p / joint;

                        J1 = pos * D1 + c1[0];
                        J2 = pos * D2 + c1[1];
                        J3 = pos * D3 + c1[2];
                        J4 = pos * D4 + c1[3];
                        J5 = pos * D5 + c1[4];
                        J6 = pos * D6 + c1[5];

                        step++;

                        if (pos >= Preg)
                        {
                            M++;
                            ultimapos = p;
                            ultimostep = step;
                        }

                        j.diretta(J1, J2, J3, J4, J5, J6, ref Cart);
                        temp_instr.Add(new double[6] { Cart[0], Cart[1], Cart[2], Cart[3], Cart[4], Cart[5] });
                        break;

                    case 2:
                        p = (vel * (step - ultimostep) * 0.02) - (0.5 * (this.acc * Math.Pow((step - ultimostep) * 0.02, 2))) + ultimapos;
                        pos = p / joint;

                        J1 = pos * D1 + c1[0];
                        J2 = pos * D2 + c1[1];
                        J3 = pos * D3 + c1[2];
                        J4 = pos * D4 + c1[3];
                        J5 = pos * D5 + c1[4];
                        J6 = pos * D6 + c1[5];
                        step++;

                        j.diretta(J1, J2, J3, J4, J5, J6, ref Cart);
                        temp_instr.Add(new double[6] { Cart[0], Cart[1], Cart[2], Cart[3], Cart[4], Cart[5] });
                        break;
                }
            }

            //process_points(ref temp_instr, n_instr_for_rac);
            this.P.AddRange(temp_instr);
            this.P.Add(new double[6] { p1.X, p1.Y, p1.Z, p1.A, p1.B, p1.C });
            this.numStep = ++step;
        }

        public static void process_points(ref List<double[]> points, int n_instr_rac)
        {
            List<double[]> points_sorted = new List<double[]>();
            for( int i = 0; i < points.Count ; i++ )
            {
                double[] sum = new double[6];
                int count = 0;
                int start_idx = i > n_instr_rac ? i - n_instr_rac : 0;
                for (int u = start_idx; u < i; u++)
                {
                    sum[0] += points[u][0];
                    sum[1] += points[u][1];
                    sum[2] += points[u][2];
                    sum[3] += points[u][3];
                    sum[4] += points[u][4];
                    sum[5] += points[u][5];
                    count++;
                }
                int end_idx = i + n_instr_rac > points.Count? points.Count : i + n_instr_rac;
                for( int u = i; u < end_idx;u++)
                {
                    sum[0] += points[u][0];
                    sum[1] += points[u][1];
                    sum[2] += points[u][2];
                    sum[3] += points[u][3];
                    sum[4] += points[u][4];
                    sum[5] += points[u][5];
                    count++;
                }

                double[] point = new double[6];

                point[0] = sum[0] / count;
                point[1] = sum[1] / count;
                point[2] = sum[2] / count;
                point[3] = sum[3] / count;
                point[4] = sum[4] / count;
                point[5] = sum[5] / count;
                points_sorted.Add(point);
            }
            points = points_sorted;
        }

        public void delay(int t, Punti3D punto)
        {
            this.tempo = t;
            numStep = t / 20;
            for (int i = 0; i < numStep; i++)
            {
                this.P.Add(new double[6] { punto.X, punto.Y, punto.Z, punto.A, punto.B, punto.C });
            }
        }
        public void istruzione(int[] pin1, int[] pin2, int[] pin3, int[] pin4, int[] pin5, int[] def, int[] cont , Punti3D punto)
        {
            this.ist = true;
            this.Pin1 = pin1;
            this.Pin2 = pin2;
            this.Pin3 = pin3;
            this.Pin4 = pin4;
            this.Pin5 = pin5;
            this._default= def;
            this.contatore = cont;           
            this.P.Add(new double[6] { punto.X, punto.Y, punto.Z, punto.A, punto.B, punto.C });
        }
    }

    public class EccezioneMatematica : Exception
    {
        public EccezioneMatematica()
        {
            MessageBox.Show("Fuori dal limite");
        }
    }

    public static class Istruzione
    {
        public static int[] Pin1 = new int[9];
        public static int[] Pin2 = new int[9];
        public static int[] Pin3 = new int[9];
        public static int[] Pin4 = new int[9];
        public static int[] Pin5 = new int[9];
        public static int[] Default = new int[9];
        public static int[] Conteggio = new int[2];   
    }
}       

