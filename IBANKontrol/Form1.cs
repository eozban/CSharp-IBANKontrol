using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESinTiBiLiSiM;

namespace IBANKontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyIBAN myIBAN = new MyIBAN(textBox1.Text);

            if (myIBAN.IBAN_Status)
                MessageBox.Show("IBAN DOĞRU");
            else
                MessageBox.Show("IBAN YANLIŞ");
        }
    }
}
