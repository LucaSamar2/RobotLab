using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form3 : MetroForm
    {
        public Form3()
        {
            InitializeComponent();
            Vpin1.Text = "+0";
            Vpin2.Text = "+0";
            Vpin3.Text = "+0";
            Vpin4.Text = "+0";
            Vpin5.Text = "+0";
            _default.Text = "+0";
            Vcont.Text = "1";

        }

        public Form3( int[] def, int[] pin1, int[] pin2, int[] pin3, int[] pin4, int[] pin5 , int[] cont)
        {
            InitializeComponent();


            Pin1.Checked = (pin1[0] == 1?true:false);          
            switch(pin1[1])
            {
                case 0: Vpin1.Text = pin1[2].ToString();break;
                case 1: Vpin1.Text = "+" + pin1[2].ToString(); break;
                case 2: Vpin1.Text = "-" + pin1[2].ToString(); break;
            }
            Pin1DOenable.Checked = (pin1[3] == 1 ? true : false);
            P1D1.Checked = (pin1[4] == 1 ? true : false);
            P1D2.Checked = (pin1[5] == 1 ? true : false);
            P1D3.Checked = (pin1[6] == 1 ? true : false);
            P1D4.Checked = (pin1[7] == 1 ? true : false);
            P1D5.Checked = (pin1[8] == 1 ? true : false);


            Pin2.Checked = (pin2[0] == 1 ? true : false);
            switch (pin2[1])
            {
                case 0: Vpin2.Text = pin2[2].ToString(); break;
                case 1: Vpin2.Text = "+" + pin2[2].ToString(); break;
                case 2: Vpin2.Text = "-" + pin2[2].ToString(); break;
            }
            Pin2DOenable.Checked = (pin2[3] == 1 ? true : false);
            P2D1.Checked = (pin2[4] == 1 ? true : false);
            P2D2.Checked = (pin2[5] == 1 ? true : false);
            P2D3.Checked = (pin2[6] == 1 ? true : false);
            P2D4.Checked = (pin2[7] == 1 ? true : false);
            P2D5.Checked = (pin2[8] == 1 ? true : false);


            Pin3.Checked = (pin3[0] == 1 ? true : false);
            switch (pin3[1])
            {
                case 0: Vpin3.Text = pin3[2].ToString(); break;
                case 1: Vpin3.Text = "+" + pin3[2].ToString(); break;
                case 2: Vpin3.Text = "-" + pin3[2].ToString(); break;
            }
            Pin3DOenable.Checked = (pin3[3] == 1 ? true : false);
            P3D1.Checked = (pin3[4] == 1 ? true : false);
            P3D2.Checked = (pin3[5] == 1 ? true : false);
            P3D3.Checked = (pin3[6] == 1 ? true : false);
            P3D4.Checked = (pin3[7] == 1 ? true : false);
            P3D5.Checked = (pin3[8] == 1 ? true : false);


            Pin4.Checked = (pin4[0] == 1 ? true : false);
            switch (pin4[1])
            {
                case 0: Vpin4.Text = pin4[2].ToString(); break;
                case 1: Vpin4.Text = "+" + pin4[2].ToString(); break;
                case 2: Vpin4.Text = "-" + pin4[2].ToString(); break;
            }
            Pin4DOenable.Checked = (pin4[3] == 1 ? true : false);
            P4D1.Checked = (pin4[4] == 1 ? true : false);
            P4D2.Checked = (pin4[5] == 1 ? true : false);
            P4D3.Checked = (pin4[6] == 1 ? true : false);
            P4D4.Checked = (pin4[7] == 1 ? true : false);
            P4D5.Checked = (pin4[8] == 1 ? true : false);


            Pin5.Checked = (pin5[0] == 1 ? true : false);
            switch (pin5[1])
            {
                case 0: Vpin5.Text = pin5[2].ToString(); break;
                case 1: Vpin5.Text = "+" + pin5[2].ToString(); break;
                case 2: Vpin5.Text = "-" + pin5[2].ToString(); break;
            }
            Pin5DOenable.Checked = (pin5[3] == 1 ? true : false);
            P5D1.Checked = (pin5[4] == 1 ? true : false);
            P5D2.Checked = (pin5[5] == 1 ? true : false);
            P5D3.Checked = (pin5[6] == 1 ? true : false);
            P5D4.Checked = (pin5[7] == 1 ? true : false);
            P5D5.Checked = (pin5[8] == 1 ? true : false);

            switch (def[1])
            {
                case 0: _default.Text = def[2].ToString(); break;
                case 1: _default.Text = "+" + def[2].ToString(); break;
                case 2: _default.Text = "-" + def[2].ToString(); break;
            }
            DEF_DOenable.Checked = (def[3] == 1 ? true : false);
            DEF_DO1.Checked = (def[4] == 1 ? true : false);
            DEF_DO2.Checked = (def[5] == 1 ? true : false);
            DEF_DO3.Checked = (def[6] == 1 ? true : false);
            DEF_DO4.Checked = (def[7] == 1 ? true : false);
            DEF_DO5.Checked = (def[8] == 1 ? true : false);

            Contatore.Checked = (cont[0] == 1 ? true : false);
            Vcont.Text = cont[1].ToString();


            if(Pin1.Checked)
            {
                Vpin1.Enabled = true;
                Pin1DOenable.Enabled = true;
                if(Pin1DOenable.Checked)
                {
                    P1D1.Enabled = true;
                    P1D2.Enabled = true;
                    P1D3.Enabled = true;
                    P1D4.Enabled = true;
                    P1D5.Enabled = true;
                }           
            }
            if (Pin2.Checked)
            {
                Vpin2.Enabled = true;
                Pin2DOenable.Enabled = true;
                if (Pin2DOenable.Checked)
                {
                    P2D1.Enabled = true;
                    P2D2.Enabled = true;
                    P2D3.Enabled = true;
                    P2D4.Enabled = true;
                    P2D5.Enabled = true;
                }
            }
            if (Pin3.Checked)
            {
                Vpin3.Enabled = true;
                Pin3DOenable.Enabled = true;
                if (Pin3DOenable.Checked)
                {
                    P3D1.Enabled = true;
                    P3D2.Enabled = true;
                    P3D3.Enabled = true;
                    P3D4.Enabled = true;
                    P3D5.Enabled = true;
                }
            }
            if (Pin4.Checked)
            {
                Vpin4.Enabled = true;
                Pin4DOenable.Enabled = true;
                if (Pin4DOenable.Checked)
                {
                    P4D1.Enabled = true;
                    P4D2.Enabled = true;
                    P4D3.Enabled = true;
                    P4D4.Enabled = true;
                    P4D5.Enabled = true;
                }
            }
            if (Pin5.Checked)
            {
                Vpin5.Enabled = true;
                Pin5DOenable.Enabled = true;
                if (Pin5DOenable.Checked)
                {
                    P5D1.Enabled = true;
                    P5D2.Enabled = true;
                    P5D3.Enabled = true;
                    P5D4.Enabled = true;
                    P5D5.Enabled = true;
                }
            }
            if(DEF_DOenable.Checked)
            {
                DEF_DO1.Enabled = true;
                DEF_DO2.Enabled = true;
                DEF_DO3.Enabled = true;
                DEF_DO4.Enabled = true;
                DEF_DO5.Enabled = true;
            }

        }


        private void Pin1_CheckedChanged(object sender, EventArgs e)
        {
            Vpin1.Enabled = Pin1.Checked;
            Pin1DOenable.Enabled = Pin1.Checked;
            if (!Pin1.Checked) Pin1DOenable.Checked = false;

        }

        private void Pin1DOenable_CheckedChanged(object sender, EventArgs e)
        {
            P1D1.Enabled = Pin1DOenable.Checked;
            P1D2.Enabled = Pin1DOenable.Checked;
            P1D3.Enabled = Pin1DOenable.Checked;
            P1D4.Enabled = Pin1DOenable.Checked;
            P1D5.Enabled = Pin1DOenable.Checked;
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Chiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (_default.Text[0] == '+') Istruzione.Default[1] = 1;
                else if (_default.Text[0] == '-') Istruzione.Default[1] = 2;
                else Istruzione.Default[1] = 0;
                int val = Convert.ToInt32(_default.Text);
                Istruzione.Default[2] = val > 0 ? val : -val;
                Istruzione.Default[3] = DEF_DOenable.Checked ? 1 : 0;
                Istruzione.Default[4] = DEF_DO1.Checked ? 1 : 0;
                Istruzione.Default[5] = DEF_DO2.Checked ? 1 : 0;
                Istruzione.Default[6] = DEF_DO3.Checked ? 1 : 0;
                Istruzione.Default[7] = DEF_DO4.Checked ? 1 : 0;
                Istruzione.Default[8] = DEF_DO5.Checked ? 1 : 0;

                Istruzione.Pin1[0] = Pin1.Checked ? 1 : 0;
                if (Vpin1.Text[0] == '+') Istruzione.Pin1[1] = 1;
                else if (Vpin1.Text[0] == '-') Istruzione.Pin1[1] = 2;
                else Istruzione.Pin1[1] = 0;
                val = Convert.ToInt32(Vpin1.Text);
                Istruzione.Pin1[2] = val > 0 ? val : -val;
                Istruzione.Pin1[3] = Pin1DOenable.Checked ? 1 : 0;
                Istruzione.Pin1[4] = P1D1.Checked ? 1 : 0;
                Istruzione.Pin1[5] = P1D2.Checked ? 1 : 0;
                Istruzione.Pin1[6] = P1D3.Checked ? 1 : 0;
                Istruzione.Pin1[7] = P1D4.Checked ? 1 : 0;
                Istruzione.Pin1[8] = P1D5.Checked ? 1 : 0;

                Istruzione.Pin2[0] = Pin2.Checked ? 1 : 0;
                if (Vpin2.Text[0] == '+') Istruzione.Pin2[1] = 1;
                else if (Vpin2.Text[0] == '-') Istruzione.Pin2[1] = 2;
                else Istruzione.Pin2[1] = 0;
                val = Convert.ToInt32(Vpin2.Text);
                Istruzione.Pin2[2] = val > 0 ? val : -val;
                Istruzione.Pin2[3] = Pin2DOenable.Checked ? 1 : 0;
                Istruzione.Pin2[4] = P2D1.Checked ? 1 : 0;
                Istruzione.Pin2[5] = P2D2.Checked ? 1 : 0;
                Istruzione.Pin2[6] = P2D3.Checked ? 1 : 0;
                Istruzione.Pin2[7] = P2D4.Checked ? 1 : 0;
                Istruzione.Pin2[8] = P2D5.Checked ? 1 : 0;

                Istruzione.Pin3[0] = Pin3.Checked ? 1 : 0;
                if (Vpin3.Text[0] == '+') Istruzione.Pin3[1] = 1;
                else if (Vpin3.Text[0] == '-') Istruzione.Pin3[1] = 2;
                else Istruzione.Pin3[1] = 0;
                val = Convert.ToInt32(Vpin3.Text);
                Istruzione.Pin3[2] = val > 0 ? val : -val;
                Istruzione.Pin3[3] = Pin3DOenable.Checked ? 1 : 0;
                Istruzione.Pin3[4] = P3D1.Checked ? 1 : 0;
                Istruzione.Pin3[5] = P3D2.Checked ? 1 : 0;
                Istruzione.Pin3[6] = P3D3.Checked ? 1 : 0;
                Istruzione.Pin3[7] = P3D4.Checked ? 1 : 0;
                Istruzione.Pin3[8] = P3D5.Checked ? 1 : 0;

                Istruzione.Pin4[0] = Pin4.Checked ? 1 : 0;
                if (Vpin4.Text[0] == '+') Istruzione.Pin4[1] = 1;
                else if (Vpin4.Text[0] == '-') Istruzione.Pin4[1] = 2;
                else Istruzione.Pin4[1] = 0;
                val = Convert.ToInt32(Vpin4.Text);
                Istruzione.Pin4[2] = val > 0 ? val : -val;
                Istruzione.Pin4[3] = Pin4DOenable.Checked ? 1 : 0;
                Istruzione.Pin4[4] = P4D1.Checked ? 1 : 0;
                Istruzione.Pin4[5] = P4D2.Checked ? 1 : 0;
                Istruzione.Pin4[6] = P4D3.Checked ? 1 : 0;
                Istruzione.Pin4[7] = P4D4.Checked ? 1 : 0;
                Istruzione.Pin4[8] = P4D5.Checked ? 1 : 0;

                Istruzione.Pin5[0] = Pin5.Checked ? 1 : 0;
                if (Vpin5.Text[0] == '+') Istruzione.Pin5[1] = 1;
                else if (Vpin5.Text[0] == '-') Istruzione.Pin5[1] = 2;
                else Istruzione.Pin5[1] = 0;
                val = Convert.ToInt32(Vpin5.Text);
                Istruzione.Pin5[2] = val > 0 ? val : -val;
                Istruzione.Pin5[3] = Pin5DOenable.Checked ? 1 : 0;
                Istruzione.Pin5[4] = P5D1.Checked ? 1 : 0;
                Istruzione.Pin5[5] = P5D2.Checked ? 1 : 0;
                Istruzione.Pin5[6] = P5D3.Checked ? 1 : 0;
                Istruzione.Pin5[7] = P5D4.Checked ? 1 : 0;
                Istruzione.Pin5[8] = P5D5.Checked ? 1 : 0;

                Istruzione.Conteggio[0] = Contatore.Checked ? 1 : 0;
                Istruzione.Conteggio[1] = Convert.ToInt16(Vcont.Text);
            }
            catch (Exception) { MessageBox.Show("Inserire numeri corretti"); }
        
        }


        private void Pin2_CheckedChanged(object sender, EventArgs e)
        {
            Vpin2.Enabled = Pin2.Checked;
            Pin2DOenable.Enabled = Pin2.Checked;
            if (!Pin2.Checked) Pin2DOenable.Checked = false;
        }

        private void Pin2DOenable_CheckedChanged(object sender, EventArgs e)
        {
            P2D1.Enabled = Pin2DOenable.Checked;
            P2D2.Enabled = Pin2DOenable.Checked;
            P2D3.Enabled = Pin2DOenable.Checked;
            P2D4.Enabled = Pin2DOenable.Checked;
            P2D5.Enabled = Pin2DOenable.Checked;
        }

        private void Pin3_CheckedChanged(object sender, EventArgs e)
        {
            Vpin3.Enabled = Pin3.Checked;
            Pin3DOenable.Enabled = Pin3.Checked;
            if (!Pin3.Checked) Pin3DOenable.Checked = false;
        }

        private void Pin3DOenable_CheckedChanged(object sender, EventArgs e)
        {
            P3D1.Enabled = Pin3DOenable.Checked;
            P3D2.Enabled = Pin3DOenable.Checked;
            P3D3.Enabled = Pin3DOenable.Checked;
            P3D4.Enabled = Pin3DOenable.Checked;
            P3D5.Enabled = Pin3DOenable.Checked;
        }

        private void Pin4_CheckedChanged(object sender, EventArgs e)
        {
            Vpin4.Enabled = Pin4.Checked;
            Pin4DOenable.Enabled = Pin4.Checked;
            if (!Pin4.Checked) Pin4DOenable.Checked = false;
        }

        private void Pin4DOenable_CheckedChanged(object sender, EventArgs e)
        {
            P4D1.Enabled = Pin4DOenable.Checked;
            P4D2.Enabled = Pin4DOenable.Checked;
            P4D3.Enabled = Pin4DOenable.Checked;
            P4D4.Enabled = Pin4DOenable.Checked;
            P4D5.Enabled = Pin4DOenable.Checked;
        }

        private void Pin5_CheckedChanged(object sender, EventArgs e)
        {
            Vpin5.Enabled = Pin5.Checked;
            Pin5DOenable.Enabled = Pin5.Checked;
            if (!Pin5.Checked) Pin5DOenable.Checked = false;
        }

        private void Pin5DOenable_CheckedChanged(object sender, EventArgs e)
        {
            P5D1.Enabled = Pin5DOenable.Checked;
            P5D2.Enabled = Pin5DOenable.Checked;
            P5D3.Enabled = Pin5DOenable.Checked;
            P5D4.Enabled = Pin5DOenable.Checked;
            P5D5.Enabled = Pin5DOenable.Checked;
        }

        private void DEF_DOenable_CheckedChanged(object sender, EventArgs e)
        {
            DEF_DO1.Enabled = DEF_DOenable.Checked;
            DEF_DO2.Enabled = DEF_DOenable.Checked;
            DEF_DO3.Enabled = DEF_DOenable.Checked;
            DEF_DO4.Enabled = DEF_DOenable.Checked;
            DEF_DO5.Enabled = DEF_DOenable.Checked;
        }

        private void Contatore_CheckedChanged(object sender, EventArgs e)
        {
            Vcont.Enabled = Contatore.Checked;
        }
    }
}