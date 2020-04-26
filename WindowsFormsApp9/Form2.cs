using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form2 :MetroForm
    {

        public Form2()
        {
            InitializeComponent();
        }

        private double Bd1 = 0;
        private double Bd2 = 0;
        private double Bd3 = 0;
        private double Bd4 = 0;
        private double Bd5 = 0;
        private double Bd6 = 0;
        private double Ba1 = 0;
        private double Ba2 = 0;
        private double Ba3 = 0;
        private double Ba4 = 0;
        private double Ba5 = 0;
        private double Ba6 = 0;
        private double Bleva1 = 0;
        private double Bleva2 = 0;
        private double Basta = 0;
        private double Btool = 0;

        private void metroButton2_Click(object sender, EventArgs e)
        { 
            this.Close();
            label1.Select();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            D1.Text = ((int)Robot.d1).ToString();
            D2.Text = ((int)Robot.d2).ToString();
            D3.Text = ((int)Robot.d3).ToString();
            D4.Text = ((int)Robot.d4).ToString();
            D5.Text = ((int)Robot.d5).ToString();
            D6.Text = ((int)Robot.d6).ToString();
            A1.Text = ((int)Robot.a1).ToString();
            A2.Text = ((int)Robot.a2).ToString();
            A3.Text = ((int)Robot.a3).ToString();
            A4.Text = ((int)Robot.a4).ToString();
            A5.Text = ((int)Robot.a5).ToString();
            A6.Text = ((int)Robot.a6).ToString();
            Modifiche2D.Checked = Robot.visione2D;

            Leva1.Text = ((int)Robot.asta1).ToString();
            Asta.Text = ((int)Robot.asta2).ToString();
            Leva2.Text = ((int)Robot.asta3).ToString();
            Tool.Text = (Robot.tool).ToString();

        }


        private void metroButton1_Click(object sender, EventArgs e)
        {

            if (Modifiche2D.Checked)
            {
                try
                {
                    Bd1 = Convert.ToDouble(D1.Text);
                    Bd2 = Convert.ToDouble(D2.Text);
                    Bd3 = Convert.ToDouble(D3.Text);
                    Bd4 = Convert.ToDouble(D4.Text);
                    Bd5 = Convert.ToDouble(D5.Text);
                    Bd6 = Convert.ToDouble(D6.Text);
                    Ba1 = Convert.ToDouble(A1.Text);
                    Ba2 = Convert.ToDouble(A2.Text);
                    Ba3 = Convert.ToDouble(A3.Text);
                    Ba4 = Convert.ToDouble(A4.Text);
                    Ba5 = Convert.ToDouble(A5.Text);
                    Ba6 = Convert.ToDouble(A6.Text);
                    Bleva1 = Convert.ToDouble(Leva1.Text);
                    Bleva2 = Convert.ToDouble(Leva2.Text);
                    Basta = Convert.ToDouble(Asta.Text);
                    Btool = Convert.ToDouble(Tool.Text);

                    Robot.a1 = this.Ba1;
                    Robot.a2 = this.Ba2;
                    Robot.a3 = this.Ba3;
                    Robot.a4 = this.Ba4;
                    Robot.a5 = this.Ba5;
                    Robot.a6 = this.Ba6;

                    Robot.d1 = this.Bd1;
                    Robot.d2 = this.Bd2;
                    Robot.d3 = this.Bd3;
                    Robot.d4 = this.Bd4;
                    Robot.d5 = this.Bd5;
                    Robot.d6 = this.Bd6;

                    Robot.asta1 = this.Bleva1;
                    Robot.asta2 = this.Basta;
                    Robot.asta3 = this.Bleva2;
                    Robot.tool = (int)this.Btool;

                    Robot.lato1 = Math.Sqrt(Math.Pow(Robot.a3, 2) + Math.Pow(Robot.d4, 2));
                    Robot.scala = (int)((Robot.a1 + Robot.a2 + Robot.a3 + Robot.a4 + Robot.a5 + Robot.a6 + Robot.d1 + Robot.d2 + Robot.d3 + Robot.d4 + Robot.d5 + Robot.d6 + Robot.asta1 + Robot.asta2 + Robot.asta3 + Robot.tool) / 400);
                }
                catch (Exception) { MessageBox.Show("Inserire numeri corretti"); };
            }
            else
            {
                try
                {
                    Btool = Convert.ToDouble(Tool.Text);
                    Robot.tool = (int)this.Btool;
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserire numero corretto");
                }
            }
            this.Close();
            label1.Select();

        }

        private void Modifiche2D_CheckedChanged(object sender, EventArgs e)
        {
            Robot.visione2D = Modifiche2D.Checked;
            D1.Enabled = Modifiche2D.Checked;
            D2.Enabled = Modifiche2D.Checked;
            D3.Enabled = Modifiche2D.Checked;
            D4.Enabled = Modifiche2D.Checked;
            D5.Enabled = Modifiche2D.Checked;
            D6.Enabled = Modifiche2D.Checked;
            A1.Enabled = Modifiche2D.Checked;
            A2.Enabled = Modifiche2D.Checked;
            A3.Enabled = Modifiche2D.Checked;
            A4.Enabled = Modifiche2D.Checked;
            A5.Enabled = Modifiche2D.Checked;
            A6.Enabled = Modifiche2D.Checked;
            Leva1.Enabled = Modifiche2D.Checked;
            Leva2.Enabled = Modifiche2D.Checked;
            Asta.Enabled = Modifiche2D.Checked;
        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }
    }  
}
