using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Input : Form
    {
        public Input()
        {
            InitializeComponent();
            for (int i = 0; i < 81; i++)
            {
                textBox(i).MaxLength = 1;
                textBox(i).Enabled = true;
            }
        }

        private TextBox textBox(int i)
        {
            if (i == 0)
                return textBox0;
            else if (i == 1)
                return textBox1;
            else if (i == 2)
                return textBox2;
            else if (i == 3)
                return textBox3;
            else if (i == 4)
                return textBox4;
            else if (i == 5)
                return textBox5;
            else if (i == 6)
                return textBox6;
            else if (i == 7)
                return textBox7;
            else if (i == 8)
                return textBox8;
            else if (i == 9)
                return textBox9;
            else if (i == 10)
                return textBox10;
            else if (i == 11)
                return textBox11;
            else if (i == 12)
                return textBox12;
            else if (i == 13)
                return textBox13;
            else if (i == 14)
                return textBox14;
            else if (i == 15)
                return textBox15;
            else if (i == 16)
                return textBox16;
            else if (i == 17)
                return textBox17;
            else if (i == 18)
                return textBox18;
            else if (i == 19)
                return textBox19;
            else if (i == 20)
                return textBox20;
            else if (i == 21)
                return textBox21;
            else if (i == 22)
                return textBox22;
            else if (i == 23)
                return textBox23;
            else if (i == 24)
                return textBox24;
            else if (i == 25)
                return textBox25;
            else if (i == 26)
                return textBox26;
            else if (i == 27)
                return textBox27;
            else if (i == 28)
                return textBox28;
            else if (i == 29)
                return textBox29;
            else if (i == 30)
                return textBox30;
            else if (i == 31)
                return textBox31;
            else if (i == 32)
                return textBox32;
            else if (i == 33)
                return textBox33;
            else if (i == 34)
                return textBox34;
            else if (i == 35)
                return textBox35;
            else if (i == 36)
                return textBox36;
            else if (i == 37)
                return textBox37;
            else if (i == 38)
                return textBox38;
            else if (i == 39)
                return textBox39;
            else if (i == 40)
                return textBox40;
            else if (i == 41)
                return textBox41;
            else if (i == 42)
                return textBox42;
            else if (i == 43)
                return textBox43;
            else if (i == 44)
                return textBox44;
            else if (i == 45)
                return textBox45;
            else if (i == 46)
                return textBox46;
            else if (i == 47)
                return textBox47;
            else if (i == 48)
                return textBox48;
            else if (i == 49)
                return textBox49;
            else if (i == 50)
                return textBox50;
            else if (i == 51)
                return textBox51;
            else if (i == 52)
                return textBox52;
            else if (i == 53)
                return textBox53;
            else if (i == 54)
                return textBox54;
            else if (i == 55)
                return textBox55;
            else if (i == 56)
                return textBox56;
            else if (i == 57)
                return textBox57;
            else if (i == 58)
                return textBox58;
            else if (i == 59)
                return textBox59;
            else if (i == 60)
                return textBox60;
            else if (i == 61)
                return textBox61;
            else if (i == 62)
                return textBox62;
            else if (i == 63)
                return textBox63;
            else if (i == 64)
                return textBox64;
            else if (i == 65)
                return textBox65;
            else if (i == 66)
                return textBox66;
            else if (i == 67)
                return textBox67;
            else if (i == 68)
                return textBox68;
            else if (i == 69)
                return textBox69;
            else if (i == 70)
                return textBox70;
            else if (i == 71)
                return textBox71;
            else if (i == 72)
                return textBox72;
            else if (i == 73)
                return textBox73;
            else if (i == 74)
                return textBox74;
            else if (i == 75)
                return textBox75;
            else if (i == 76)
                return textBox76;
            else if (i == 77)
                return textBox77;
            else if (i == 78)
                return textBox78;
            else if (i == 79)
                return textBox79;
            else
                return textBox80;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            //filter bad data
            for (int i = 0; i < 81; i++)
            {
                if (textBox(i).Text == "" || textBox(i).Text == " ")
                    continue;

                if (Convert.ToChar(textBox(i).Text) < 49 || Convert.ToChar(textBox(i).Text) > 57)
                {
                    MessageBox.Show("One or more of the textboxes contain incorrect data. The number must be 1 to 9! You can also leave them blank..", "Bad Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox(i).Text = "";
                    return;
                }
            }

            //input data
            for (int i = 0; i < 81; i++)
                if (textBox(i).Text == "" || textBox(i).Text == " ")
                    Sudoku.Sudoku_arr[i] = 0;
                else
                    Sudoku.Sudoku_arr[i] = Convert.ToInt32(textBox(i).Text);

            Sudoku.Input_cancel = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sudoku.Input_cancel = true;
            return;
        }

    }
}
