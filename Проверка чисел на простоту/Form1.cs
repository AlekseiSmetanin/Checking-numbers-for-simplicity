using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Numerics;
using System.IO;

namespace Проверка_чисел_на_простоту
{
    public partial class Form1 : Form
    {
        private PrimalityTest primalityTest;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
                button1_Click(sender, e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                primalityTest = PrimalityTests.IsProbablyPrimeFermatTest;
            }
            if (radioButton2.Checked)
            {
                primalityTest = PrimalityTests.IsProbablyPrimeSolovayStrassenTest;
            }
            if (radioButton3.Checked)
            {
                primalityTest = PrimalityTests.IsProbablyPrimeRabinMillerTest;
            }

            BigInteger n, a;
            label2.Text = "";

            try
            {
                n = BigInteger.Parse(textBox1.Text);
                if (n == 0)
                    return;

                if (n<=3)
                {
                    label2.Text = String.Format("Число {0}  простое\n", n);

                    return;
                }
                RandomBigInteger randomBigInteger = new RandomBigInteger();

                bool isPrime = true;

                for (int i = 0; i < 5; i++)
                {
                    a = randomBigInteger.Next(2, n - 1);
                    isPrime &= primalityTest(n, a);
                }

                if (isPrime)
                    label2.Text = String.Format("Число {0} вероятно простое\n", n);
                else
                    label2.Text = String.Format("Число {0} составное", n);
            }
            catch (ArgumentOutOfRangeException aoorExc)
            {
                label2.Text = aoorExc.Message;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
