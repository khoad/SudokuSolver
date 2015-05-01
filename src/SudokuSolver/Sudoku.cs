//Khoa Nguyen
//Senior Project
//Spring 2011

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace SudokuSolver
{
    public partial class Sudoku : Form
    {
        #region Disable the "X" (Close) button on the form
            
            //This part is to prevent the user from using the default close button. The reason for this is because when
            //      the users use this button, the program will close itself immediately without closing the network
            //      connection first, which will cause troubles opening the program next time if the user don't go to
            //      task manager and manually close the connection themselves.
            //Retrieved from http://www.c-sharpcorner.com/UploadFile/itsraj007/Rajendra105292008020242AM/Rajendra1.aspx
            const int MF_BYPOSITION = 0x400;
            [DllImport("User32")]
            private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
            [DllImport("User32")]
            private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
            [DllImport("User32")]
            private static extern int GetMenuItemCount(IntPtr hWnd);

        #endregion

        #region Delaration
        Multiplayer Multi = new Multiplayer(); //open form Multiplayer
            private string fileName = "";
            public static int[] Sudoku_arr = new int[81]; //the actual Sudoku array
            private string[] Sudoku_string_arr = new string[81]; //a string array to easy manipulate the Sudoku array
            private string[] tokens; //to read lines of text
            private int selected = 100; //to find out which button was pressed
            private int mistakes;
            private int tries;
            private bool Solvable = false;
            public static bool Input_cancel = false;
            public static bool Input_bad = false;
            private string Play_Cell;
            private bool saved = false;
            private bool finished; //see if the user has finished the puzzle
            public static bool connected = false; //see if the user is connected with another player
            private string msg = "Oh no! Looks like the Sudoku is impossible to solve.... You have 2 options, if: \n\n" +
                 "      a) You trust me -> Go back and check if your input is correct -Or if-\n" +
                 "      b) You don't trust me -> Feel free to solve it...you're on your own :>";
        #endregion

        public Sudoku()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            for (int i = 0; i < 81; i++) button(i).BackColor = Color.White;
            
            //Disable the "X" (Close) button (Continued)
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int menuItemCount = GetMenuItemCount(hMenu);
            RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
        }

        //Display the results
        private void Update_Status()
        {
            for (int i = 0; i < 81; i++)
            {
                if (Sudoku_arr[i] == 0)
                {
                    Sudoku_string_arr[i] = "";
                    button(i).Enabled = true;
                    button(i).UseVisualStyleBackColor = true;
                }
                else
                    Sudoku_string_arr[i] = Sudoku_arr[i].ToString();
            }

            for (int i = 0; i < 81; i++)
                button(i).Text = Sudoku_string_arr[i];

            if (Play_Cell == null)
                    return;

            for (int i = 0; i < 81; i++)
            {                
                if (Play_Cell.Substring(i, 1) == "1")
                {
                    button(i).Enabled = true;
                    button(i).UseVisualStyleBackColor = true;
                    Sudoku_arr[i] = 0;
                }
            }            
        }

        //A method to call a button
        private Button button(int i)
        {
            if (i == 0)
                return button0;
            else if (i == 1)
                return button1;
            else if (i == 2)
                return button2;
            else if (i == 3)
                return button3;
            else if (i == 4)
                return button4;
            else if (i == 5)
                return button5;
            else if (i == 6)
                return button6;
            else if (i == 7)
                return button7;
            else if (i == 8)
                return button8;
            else if (i == 9)
                return button9;
            else if (i == 10)
                return button10;
            else if (i == 11)
                return button11;
            else if (i == 12)
                return button12;
            else if (i == 13)
                return button13;
            else if (i == 14)
                return button14;
            else if (i == 15)
                return button15;
            else if (i == 16)
                return button16;
            else if (i == 17)
                return button17;
            else if (i == 18)
                return button18;
            else if (i == 19)
                return button19;
            else if (i == 20)
                return button20;
            else if (i == 21)
                return button21;
            else if (i == 22)
                return button22;
            else if (i == 23)
                return button23;
            else if (i == 24)
                return button24;
            else if (i == 25)
                return button25;
            else if (i == 26)
                return button26;
            else if (i == 27)
                return button27;
            else if (i == 28)
                return button28;
            else if (i == 29)
                return button29;
            else if (i == 30)
                return button30;
            else if (i == 31)
                return button31;
            else if (i == 32)
                return button32;
            else if (i == 33)
                return button33;
            else if (i == 34)
                return button34;
            else if (i == 35)
                return button35;
            else if (i == 36)
                return button36;
            else if (i == 37)
                return button37;
            else if (i == 38)
                return button38;
            else if (i == 39)
                return button39;
            else if (i == 40)
                return button40;
            else if (i == 41)
                return button41;
            else if (i == 42)
                return button42;
            else if (i == 43)
                return button43;
            else if (i == 44)
                return button44;
            else if (i == 45)
                return button45;
            else if (i == 46)
                return button46;
            else if (i == 47)
                return button47;
            else if (i == 48)
                return button48;
            else if (i == 49)
                return button49;
            else if (i == 50)
                return button50;
            else if (i == 51)
                return button51;
            else if (i == 52)
                return button52;
            else if (i == 53)
                return button53;
            else if (i == 54)
                return button54;
            else if (i == 55)
                return button55;
            else if (i == 56)
                return button56;
            else if (i == 57)
                return button57;
            else if (i == 58)
                return button58;
            else if (i == 59)
                return button59;
            else if (i == 60)
                return button60;
            else if (i == 61)
                return button61;
            else if (i == 62)
                return button62;
            else if (i == 63)
                return button63;
            else if (i == 64)
                return button64;
            else if (i == 65)
                return button65;
            else if (i == 66)
                return button66;
            else if (i == 67)
                return button67;
            else if (i == 68)
                return button68;
            else if (i == 69)
                return button69;
            else if (i == 70)
                return button70;
            else if (i == 71)
                return button71;
            else if (i == 72)
                return button72;
            else if (i == 73)
                return button73;
            else if (i == 74)
                return button74;
            else if (i == 75)
                return button75;
            else if (i == 76)
                return button76;
            else if (i == 77)
                return button77;
            else if (i == 78)
                return button78;
            else if (i == 79)
                return button79;
            else
                return button80;
        }

        //Event Handler for all buttons on the left side
        private void button_Click(object sender, EventArgs e)
        {
            if (Solvable) btnSBCell.Enabled = true;
            btnDoing.Enabled = false;
            btnSolve.Enabled = false;
            txtInput.Enabled = true;
            btnEnter.Enabled = true;
            txtInput.Focus();

            for (int i = 0; i < 81; i++)
                if ((Button)sender == button(i))
                    selected = i;
        }

        #region Sudoku Solving method (modified from the original: http://en.wikipedia.org/wiki/Talk:Sudoku_algorithms)

            #region private int[,] Graph = new int [81,20] ...
            private int[,] Graph = new int[81, 20] {
	                {   1,  2,  3,  4,  5,  6,  7,  8,  9, 18, 27, 36, 45, 54, 63, 72, 10, 11, 19, 20 },
	                {   0,  2,  3,  4,  5,  6,  7,  8, 10, 19, 28, 37, 46, 55, 64, 73,  9, 11, 18, 20 },
	                {   0,  1,  3,  4,  5,  6,  7,  8, 11, 20, 29, 38, 47, 56, 65, 74,  9, 10, 18, 19 },
	                {   0,  1,  2,  4,  5,  6,  7,  8, 12, 21, 30, 39, 48, 57, 66, 75, 13, 14, 22, 23 },
	                {   0,  1,  2,  3,  5,  6,  7,  8, 13, 22, 31, 40, 49, 58, 67, 76, 12, 14, 21, 23 },
	                {   0,  1,  2,  3,  4,  6,  7,  8, 14, 23, 32, 41, 50, 59, 68, 77, 12, 13, 21, 22 },
	                {   0,  1,  2,  3,  4,  5,  7,  8, 15, 24, 33, 42, 51, 60, 69, 78, 16, 17, 25, 26 },
	                {   0,  1,  2,  3,  4,  5,  6,  8, 16, 25, 34, 43, 52, 61, 70, 79, 15, 17, 24, 26 },
	                {   0,  1,  2,  3,  4,  5,  6,  7, 17, 26, 35, 44, 53, 62, 71, 80, 15, 16, 24, 25 },
	                {  10, 11, 12, 13, 14, 15, 16, 17,  0, 18, 27, 36, 45, 54, 63, 72,  1,  2, 19, 20 },
	                {   9, 11, 12, 13, 14, 15, 16, 17,  1, 19, 28, 37, 46, 55, 64, 73,  0,  2, 18, 20 },
	                {   9, 10, 12, 13, 14, 15, 16, 17,  2, 20, 29, 38, 47, 56, 65, 74,  0,  1, 18, 19 },
	                {   9, 10, 11, 13, 14, 15, 16, 17,  3, 21, 30, 39, 48, 57, 66, 75,  4,  5, 22, 23 },
	                {   9, 10, 11, 12, 14, 15, 16, 17,  4, 22, 31, 40, 49, 58, 67, 76,  3,  5, 21, 23 },
	                {   9, 10, 11, 12, 13, 15, 16, 17,  5, 23, 32, 41, 50, 59, 68, 77,  3,  4, 21, 22 },
	                {   9, 10, 11, 12, 13, 14, 16, 17,  6, 24, 33, 42, 51, 60, 69, 78,  7,  8, 25, 26 },
	                {   9, 10, 11, 12, 13, 14, 15, 17,  7, 25, 34, 43, 52, 61, 70, 79,  6,  8, 24, 26 },
	                {   9, 10, 11, 12, 13, 14, 15, 16,  8, 26, 35, 44, 53, 62, 71, 80,  6,  7, 24, 25 },
	                {  19, 20, 21, 22, 23, 24, 25, 26,  0,  9, 27, 36, 45, 54, 63, 72,  1,  2, 10, 11 },
	                {  18, 20, 21, 22, 23, 24, 25, 26,  1, 10, 28, 37, 46, 55, 64, 73,  0,  2,  9, 11 },
	                {  18, 19, 21, 22, 23, 24, 25, 26,  2, 11, 29, 38, 47, 56, 65, 74,  0,  1,  9, 10 },
	                {  18, 19, 20, 22, 23, 24, 25, 26,  3, 12, 30, 39, 48, 57, 66, 75,  4,  5, 13, 14 },
	                {  18, 19, 20, 21, 23, 24, 25, 26,  4, 13, 31, 40, 49, 58, 67, 76,  3,  5, 12, 14 },
	                {  18, 19, 20, 21, 22, 24, 25, 26,  5, 14, 32, 41, 50, 59, 68, 77,  3,  4, 12, 13 },
	                {  18, 19, 20, 21, 22, 23, 25, 26,  6, 15, 33, 42, 51, 60, 69, 78,  7,  8, 16, 17 },
	                {  18, 19, 20, 21, 22, 23, 24, 26,  7, 16, 34, 43, 52, 61, 70, 79,  6,  8, 15, 17 },
	                {  18, 19, 20, 21, 22, 23, 24, 25,  8, 17, 35, 44, 53, 62, 71, 80,  6,  7, 15, 16 },
	                {  28, 29, 30, 31, 32, 33, 34, 35,  0,  9, 18, 36, 45, 54, 63, 72, 37, 38, 46, 47 },
	                {  27, 29, 30, 31, 32, 33, 34, 35,  1, 10, 19, 37, 46, 55, 64, 73, 36, 38, 45, 47 },
	                {  27, 28, 30, 31, 32, 33, 34, 35,  2, 11, 20, 38, 47, 56, 65, 74, 36, 37, 45, 46 },
	                {  27, 28, 29, 31, 32, 33, 34, 35,  3, 12, 21, 39, 48, 57, 66, 75, 40, 41, 49, 50 },
	                {  27, 28, 29, 30, 32, 33, 34, 35,  4, 13, 22, 40, 49, 58, 67, 76, 39, 41, 48, 50 },
	                {  27, 28, 29, 30, 31, 33, 34, 35,  5, 14, 23, 41, 50, 59, 68, 77, 39, 40, 48, 49 },
	                {  27, 28, 29, 30, 31, 32, 34, 35,  6, 15, 24, 42, 51, 60, 69, 78, 43, 44, 52, 53 },
	                {  27, 28, 29, 30, 31, 32, 33, 35,  7, 16, 25, 43, 52, 61, 70, 79, 42, 44, 51, 53 },
	                {  27, 28, 29, 30, 31, 32, 33, 34,  8, 17, 26, 44, 53, 62, 71, 80, 42, 43, 51, 52 },
	                {  37, 38, 39, 40, 41, 42, 43, 44,  0,  9, 18, 27, 45, 54, 63, 72, 28, 29, 46, 47 },
	                {  36, 38, 39, 40, 41, 42, 43, 44,  1, 10, 19, 28, 46, 55, 64, 73, 27, 29, 45, 47 },
	                {  36, 37, 39, 40, 41, 42, 43, 44,  2, 11, 20, 29, 47, 56, 65, 74, 27, 28, 45, 46 },
	                {  36, 37, 38, 40, 41, 42, 43, 44,  3, 12, 21, 30, 48, 57, 66, 75, 31, 32, 49, 50 },
	                {  36, 37, 38, 39, 41, 42, 43, 44,  4, 13, 22, 31, 49, 58, 67, 76, 30, 32, 48, 50 },
	                {  36, 37, 38, 39, 40, 42, 43, 44,  5, 14, 23, 32, 50, 59, 68, 77, 30, 31, 48, 49 },
	                {  36, 37, 38, 39, 40, 41, 43, 44,  6, 15, 24, 33, 51, 60, 69, 78, 34, 35, 52, 53 },
	                {  36, 37, 38, 39, 40, 41, 42, 44,  7, 16, 25, 34, 52, 61, 70, 79, 33, 35, 51, 53 },
	                {  36, 37, 38, 39, 40, 41, 42, 43,  8, 17, 26, 35, 53, 62, 71, 80, 33, 34, 51, 52 },
	                {  46, 47, 48, 49, 50, 51, 52, 53,  0,  9, 18, 27, 36, 54, 63, 72, 28, 29, 37, 38 },
	                {  45, 47, 48, 49, 50, 51, 52, 53,  1, 10, 19, 28, 37, 55, 64, 73, 27, 29, 36, 38 },
	                {  45, 46, 48, 49, 50, 51, 52, 53,  2, 11, 20, 29, 38, 56, 65, 74, 27, 28, 36, 37 },
	                {  45, 46, 47, 49, 50, 51, 52, 53,  3, 12, 21, 30, 39, 57, 66, 75, 31, 32, 40, 41 },
	                {  45, 46, 47, 48, 50, 51, 52, 53,  4, 13, 22, 31, 40, 58, 67, 76, 30, 32, 39, 41 },
	                {  45, 46, 47, 48, 49, 51, 52, 53,  5, 14, 23, 32, 41, 59, 68, 77, 30, 31, 39, 40 },
	                {  45, 46, 47, 48, 49, 50, 52, 53,  6, 15, 24, 33, 42, 60, 69, 78, 34, 35, 43, 44 },
	                {  45, 46, 47, 48, 49, 50, 51, 53,  7, 16, 25, 34, 43, 61, 70, 79, 33, 35, 42, 44 },
	                {  45, 46, 47, 48, 49, 50, 51, 52,  8, 17, 26, 35, 44, 62, 71, 80, 33, 34, 42, 43 },
	                {  55, 56, 57, 58, 59, 60, 61, 62,  0,  9, 18, 27, 36, 45, 63, 72, 64, 65, 73, 74 },
	                {  54, 56, 57, 58, 59, 60, 61, 62,  1, 10, 19, 28, 37, 46, 64, 73, 63, 65, 72, 74 },
	                {  54, 55, 57, 58, 59, 60, 61, 62,  2, 11, 20, 29, 38, 47, 65, 74, 63, 64, 72, 73 },
	                {  54, 55, 56, 58, 59, 60, 61, 62,  3, 12, 21, 30, 39, 48, 66, 75, 67, 68, 76, 77 },
	                {  54, 55, 56, 57, 59, 60, 61, 62,  4, 13, 22, 31, 40, 49, 67, 76, 66, 68, 75, 77 },
	                {  54, 55, 56, 57, 58, 60, 61, 62,  5, 14, 23, 32, 41, 50, 68, 77, 66, 67, 75, 76 },
	                {  54, 55, 56, 57, 58, 59, 61, 62,  6, 15, 24, 33, 42, 51, 69, 78, 70, 71, 79, 80 },
	                {  54, 55, 56, 57, 58, 59, 60, 62,  7, 16, 25, 34, 43, 52, 70, 79, 69, 71, 78, 80 },
	                {  54, 55, 56, 57, 58, 59, 60, 61,  8, 17, 26, 35, 44, 53, 71, 80, 69, 70, 78, 79 },
	                {  64, 65, 66, 67, 68, 69, 70, 71,  0,  9, 18, 27, 36, 45, 54, 72, 55, 56, 73, 74 },
	                {  63, 65, 66, 67, 68, 69, 70, 71,  1, 10, 19, 28, 37, 46, 55, 73, 54, 56, 72, 74 },
	                {  63, 64, 66, 67, 68, 69, 70, 71,  2, 11, 20, 29, 38, 47, 56, 74, 54, 55, 72, 73 },
	                {  63, 64, 65, 67, 68, 69, 70, 71,  3, 12, 21, 30, 39, 48, 57, 75, 58, 59, 76, 77 },
	                {  63, 64, 65, 66, 68, 69, 70, 71,  4, 13, 22, 31, 40, 49, 58, 76, 57, 59, 75, 77 },
	                {  63, 64, 65, 66, 67, 69, 70, 71,  5, 14, 23, 32, 41, 50, 59, 77, 57, 58, 75, 76 },
	                {  63, 64, 65, 66, 67, 68, 70, 71,  6, 15, 24, 33, 42, 51, 60, 78, 61, 62, 79, 80 },
	                {  63, 64, 65, 66, 67, 68, 69, 71,  7, 16, 25, 34, 43, 52, 61, 79, 60, 62, 78, 80 },
	                {  63, 64, 65, 66, 67, 68, 69, 70,  8, 17, 26, 35, 44, 53, 62, 80, 60, 61, 78, 79 },
	                {  73, 74, 75, 76, 77, 78, 79, 80,  0,  9, 18, 27, 36, 45, 54, 63, 55, 56, 64, 65 },
	                {  72, 74, 75, 76, 77, 78, 79, 80,  1, 10, 19, 28, 37, 46, 55, 64, 54, 56, 63, 65 },
	                {  72, 73, 75, 76, 77, 78, 79, 80,  2, 11, 20, 29, 38, 47, 56, 65, 54, 55, 63, 64 },
	                {  72, 73, 74, 76, 77, 78, 79, 80,  3, 12, 21, 30, 39, 48, 57, 66, 58, 59, 67, 68 },
	                {  72, 73, 74, 75, 77, 78, 79, 80,  4, 13, 22, 31, 40, 49, 58, 67, 57, 59, 66, 68 },
	                {  72, 73, 74, 75, 76, 78, 79, 80,  5, 14, 23, 32, 41, 50, 59, 68, 57, 58, 66, 67 },
	                {  72, 73, 74, 75, 76, 77, 79, 80,  6, 15, 24, 33, 42, 51, 60, 69, 61, 62, 70, 71 },
	                {  72, 73, 74, 75, 76, 77, 78, 80,  7, 16, 25, 34, 43, 52, 61, 70, 60, 62, 69, 71 },
	                {  72, 73, 74, 75, 76, 77, 78, 79,  8, 17, 26, 35, 44, 53, 62, 71, 60, 61, 69, 70 } };
            #endregion

            private bool Solve()
            {
                for (int i = 0; i < 81; i++)
                {
                    if (Sudoku_arr[i] == 0)
                    {
                        for (int j = 1; j < 10; j++)
                        {
                            //i: cell index, j: a possible move
                            if (MoveIsLegal(i, j))
                            {
                                Sudoku_arr[i] = j;

                                if (!Solve())
                                    Sudoku_arr[i] = 0;
                            }
                        }
                        return false;
                    }
                }
                return true;
            }

            private bool MoveIsLegal(int i, int j)
            {
                for (int k = 0; k < 20; k++)
                    if (Sudoku_arr[Graph[i, k]] == j)
                        return false;

                return true;
            }

        #endregion

        private void btnSolve_Click(object sender, EventArgs e)
        {
            DialogResult DR;
            DR = MessageBox.Show("So you're giving up?", "Giving up", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (DR == DialogResult.Yes)
            {
                Update_Status();
                txtInput.Text = "";
                btnSBCell.Enabled = false;
                btnSolve.Enabled = false;
                btnDoing.Enabled = false;
                chkDoing.Enabled = false;
                chkWrong.Enabled = false;
                chkTip.Enabled = false;
                lblDoing.Text = "You shouldn't have used that button!";
                for (int i = 0; i < 81; i++)
                    button(i).Enabled = false;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            //filter bad data
            if (txtInput.Text == "")
            {
                MessageBox.Show("Input must not be blank!", "Bad Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtInput.Text = "";
                return;
            }

            if ((Convert.ToChar(txtInput.Text) < 48 || Convert.ToChar(txtInput.Text) > 57) && Convert.ToChar(txtInput.Text) != 32)
            {
                MessageBox.Show("The number should be 1 to 9. Input '0' or 'space' if you want to clear this cell!", "Bad Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtInput.Text = "";
                return;
            }
            
            button(selected).Text = txtInput.Text; //display data
            if (txtInput.Text == "0" || txtInput.Text == " ")
                button(selected).Text = "";

            //Clear off everything
            txtInput.Text = "";
            txtInput.Enabled = false;
            btnEnter.Enabled = false;
            btnSBCell.Enabled = false;
            if (Solvable)
            {
                btnSolve.Enabled = true;
                btnDoing.Enabled = true;
            }

            //check to see if the user has finished the puzzle
            finished = true;
            for (int i = 0; i < 81; i++)
            {
                if (button(i).Text == "" ||
                    Convert.ToInt32(button(i).Text) != Sudoku_arr[i])
                  finished = false;
            }

            if (connected)
                clntThread.Resume();

            if (finished)
            {
                lblDoing.Text = "You've killed it!! Congrats!!";
                this.BackColor = Color.LightGreen;
                menuStrip.BackColor = Color.LightGreen;
                btnSolve.Enabled = false;
                btnDoing.Enabled = false;
                chkDoing.Enabled = false;
                chkWrong.Enabled = false;
                chkTip.Enabled = false;
                lblDoing.Visible = true;
            }
        }

        private void btnSBCell_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            txtInput.Enabled = false;
            btnEnter.Enabled = false;
            btnSolve.Enabled = true;
            btnSBCell.Enabled = false;
            btnDoing.Enabled = true;

            button(selected).Text = Sudoku_arr[selected].ToString();
            button(selected).UseVisualStyleBackColor = true;

            //Update current mistakes
            mistakes = 0;
            for (int i = 0; i < 81; i++)
                Sudoku_string_arr[i] = button(i).Text == "" ? "0" : button(i).Text;

            for (int i = 0; i < 81; i++)
            {
                if (Sudoku_arr[i] == 0)
                    button(i).UseVisualStyleBackColor = true;

                if (Sudoku_arr[i] != Convert.ToInt32(Sudoku_string_arr[i]) && Sudoku_string_arr[i] != "0")
                {
                    mistakes++;
                    if (chkWrong.Checked)
                        button(i).BackColor = Color.Red;
                }
            }

            lblDoing.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open File";
            dlg.Filter = "All Files|*.*|Text documents|*.txt";
            DialogResult DR = dlg.ShowDialog();
            if (DR == DialogResult.Cancel)
                return;
            fileName = dlg.FileName;

            try
            {
                Play_Cell = "";

                for (int i = 0; i < 81; i++) button(i).BackColor = Color.White;                    

                this.BackColor = SystemColors.Control;
                menuStrip.BackColor = SystemColors.Control;
                tries = 0;
                for (int i = 0; i < 81; i++)
                    button(i).Enabled = false;

                //Read the file
                TextReader tr = new StreamReader(fileName);

                //input the data
                for (int i = 0; i < 81; i += 9)
                {
                    tokens = tr.ReadLine().Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
                    Sudoku_arr[i] = int.Parse(tokens[0]);
                    Sudoku_arr[i + 1] = int.Parse(tokens[1]);
                    Sudoku_arr[i + 2] = int.Parse(tokens[2]);
                    Sudoku_arr[i + 3] = int.Parse(tokens[3]);
                    Sudoku_arr[i + 4] = int.Parse(tokens[4]);
                    Sudoku_arr[i + 5] = int.Parse(tokens[5]);
                    Sudoku_arr[i + 6] = int.Parse(tokens[6]);
                    Sudoku_arr[i + 7] = int.Parse(tokens[7]);
                    Sudoku_arr[i + 8] = int.Parse(tokens[8]);

                }
                Play_Cell = tr.ReadLine();
                tr.Close();
                Update_Status();
                saved = true;
                
            }

            catch (ArgumentException) { }

            while (!Solve())
            {
                Solve();
                tries++;
                if (tries > 50)
                {
                    MessageBox.Show(msg, "The Unsolvable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Solvable = false;

                    btnSolve.Enabled = false;
                    btnDoing.Enabled = false;
                    chkDoing.Enabled = false;
                    chkWrong.Enabled = false;
                    chkTip.Enabled = false;

                    return;
                }
            }
            Solvable = true;

            btnSolve.Enabled = true;
            txtInput.Enabled = false;
            btnEnter.Enabled = false;
            btnSBCell.Enabled = false;
            btnDoing.Enabled = true;
            chkDoing.Enabled = true;
            chkWrong.Enabled = true;
            chkTip.Enabled = true;
            lblDoing.Text = "";
            saveToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem1.Enabled = true;

            if (connected)
                clntThread.Resume();
        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input Inp = new Input();
            Inp.ShowDialog();

            if (Input_cancel)
                return;

            for (int i = 0; i < 81; i++)
                button(i).BackColor = Color.White;

            this.BackColor = SystemColors.Control;
            menuStrip.BackColor = SystemColors.Control;
            tries = 0;
            for (int i = 0; i < 81; i++)
                button(i).Enabled = false;

            Update_Status();

            while (!Solve())
            {
                Solve();
                tries++;
                if (tries > 50)
                {
                    MessageBox.Show(msg, "The Unsolvable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Solvable = false;

                    btnSolve.Enabled = false;
                    btnDoing.Enabled = false;
                    chkDoing.Enabled = false;
                    chkWrong.Enabled = false;
                    chkTip.Enabled = false;

                    return;
                }
            }
            Solvable = true;

            btnSolve.Enabled = true;
            txtInput.Enabled = false;
            btnEnter.Enabled = false;
            btnSBCell.Enabled = false;
            btnDoing.Enabled = true;
            chkDoing.Enabled = true;
            chkWrong.Enabled = true;
            chkTip.Enabled = true;
            lblDoing.Text = "";

            if (connected)
                clntThread.Resume();
        }

        private void btnDoing_Click(object sender, EventArgs e)
        {
            mistakes = 0;
            for (int i = 0; i < 81; i++)
                Sudoku_string_arr[i] = button(i).Text == "" ? "0" : button(i).Text;

            for (int i = 0; i < 81; i++)
            {
                if (button(i).BackColor == Color.Red)
                    button(i).UseVisualStyleBackColor = true;

                if (Sudoku_arr[i] != Convert.ToInt32(Sudoku_string_arr[i]) && Sudoku_string_arr[i] != "0")
                {
                    mistakes++;
                    if (chkWrong.Checked)
                        button(i).BackColor = Color.Red;
                }
            }

            if (mistakes == 0)
                lblDoing.Text = "Everything looks good!";
            else if (mistakes == 1)
                lblDoing.Text = "Uh! You've made 1 mistake!";
            else
                lblDoing.Text = "OOh! You've made " + mistakes.ToString() + " mistakes!";

            lblDoing.Enabled = true;
            chkDoing.Checked = true;
        }

        private void button_MouseHover(object sender, EventArgs e)
        {
            if (!chkTip.Checked) { toolTip.Active = false; return; }

            toolTip.Active = true;
            for (int i = 0; i < 81; i++)
                if ((Button)sender == button(i)) //find out which button is hovered
                {
                    int[] pos_val = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    for (int j = 0; j < 20; j++) //indices of relating cells to the current cell
                        for (int x = 0; x < 9; x++)
                        {
                            int rel_val = button(Graph[i, j]).Text == "" ? 0 : Convert.ToInt32(button(Graph[i, j]).Text);
                            if (rel_val == pos_val[x])
                                pos_val[x] = 0;
                        }

                    string str = "Possible values: ";
                    for (int x = 0; x < 9; x++)
                    {
                        if (pos_val[x] == 0)
                            continue;
                        str += pos_val[x].ToString();
                        str += "  ";
                    }

                    toolTip.SetToolTip(button(i), str);
                }
        }

        //for Save & and Save As
        private void SaveFile()
        {
            try
            {
                TextWriter tw = new StreamWriter(fileName);
                for (int i = 0; i < 81; i++)
                {
                    if (button(i).Text == "")
                        tw.Write("0");
                    else
                        tw.Write(button(i).Text);

                    if ((i + 1) % 9 == 0)
                    {
                        tw.WriteLine();
                        continue;
                    }

                    tw.Write(" ");
                }

                for (int i = 0; i < 81; i++)
                    if (button(i).Enabled)
                        tw.Write("1");
                    else
                        tw.Write("0");

                tw.Close();
            }
            catch (ArgumentException) { } //prevent the program from crashing when the user chooses "Cancel"
        }

        //Save As
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save File";
            dlg.Filter = "Text documents|*.txt|All Files|*.*";
            dlg.ShowDialog();
            fileName = dlg.FileName;
            SaveFile();
            saved = true;
        }

        //Save
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saved)
                SaveFile();
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save File";
                dlg.Filter = "Text document|*.txt|All Files|*.*";
                dlg.ShowDialog();
                fileName = dlg.FileName;
                SaveFile();
                saved = true;
            }

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Multi.ShowDialog();
            if (connected)
            {
                StartTCP();
                disconnectToolStripMenuItem.Enabled = true;
                connectToolStripMenuItem.Enabled = false;
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connected = false;
            disconnectToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = true;
            clntThread.Resume(); //finish cleaning it up
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult DR;
            DR = MessageBox.Show("You're tired already???", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (DR == DialogResult.Yes)
            {
                if (connected) disconnectToolStripMenuItem_Click(sender, e); //clean up first
                this.Close();
            }
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Amsg =   "Thank you for using this program!\n\n" +
                            "Please send any suggestions and/or comments to kh.dang.nguyen@gmail.com\n\n" +          
                            "Developed by: Khoa Nguyen\n\n" +
                            "Version: 1.00\n\n";
            MessageBox.Show(Amsg, "About this program");
        }        

        private void chkWrong_CheckedChanged(object sender, EventArgs e)
        {
            chkWrong_ChkChg();
            if (connected)
                clntThread.Resume();
        }

        private void chkWrong_ChkChg()
        {
            for (int i = 0; i < 81; i++)
                Sudoku_string_arr[i] = button(i).Text == "" ? "0" : button(i).Text;

            for (int i = 0; i < 81; i++)
            {
                if (button(i).BackColor == Color.Red)
                    button(i).UseVisualStyleBackColor = true;

                if (Sudoku_arr[i] != Convert.ToInt32(Sudoku_string_arr[i]) && Sudoku_string_arr[i] != "0")
                {
                    if (chkWrong.Checked)
                        button(i).BackColor = Color.Red;
                }
            }
        }

        private void chkDoing_CheckedChanged(object sender, EventArgs e)
        {
            chkDoing_ChkChg();
        }

        private void chkDoing_ChkChg()
        {
            if (chkDoing.Checked)
                lblDoing.Visible = true;
            else
                lblDoing.Visible = false;
        }
        
        private void chkTip_CheckedChanged(object sender, EventArgs e)
        {
            if (connected)
                clntThread.Resume();
        }

        
        /******************************************************************************************************************************************************/
        /**************Client Server Component - modified from http://csclab.murraystate.edu/bob.pilgrim/410/projects/project_7_TCP_Client_Server.pdf *********/
        /******************************************************************************************************************************************************/

        private static Thread srvThread;
        private static Thread clntThread;
        private static TcpListener theTCPLstn;
        private static TcpClient theTCPSock;
        private static TcpClient theTCPClient;
        private static string remoteIP;
        string rcvData = "";
        [STAThread]

        private void StartTCP()
        {
            // call method to create a new server thread
            srvThread = new Thread(new ThreadStart(RunTCPServer));
            srvThread.Start();
            remoteIP = Multi.txtIP.Text; // get remote IP as a string
            // call method to create a new client thread
            clntThread = new Thread(new ThreadStart(RunTCPClient));
            clntThread.Start();
        }

        private void RunTCPServer()
        {
            int received;
            rcvData = "";
            NetworkStream netStrm;
            theTCPLstn = new TcpListener(IPAddress.Any, 9050);
            theTCPLstn.Start();
            int count = 0;
            while (count < 10)
            {
                try
                {
                    theTCPSock = theTCPLstn.AcceptTcpClient();
                    break;
                }
                catch { count += 1; Thread.Sleep(5); }
            }
            if (count < 10)
            {
                netStrm = theTCPSock.GetStream();
                while (connected)
                {
                    byte[] myData = new byte[165];
                    try
                    {
                        received = netStrm.Read(myData, 0, myData.Length);
                    }
                    catch { break; }
                    if (received != 0)
                    {
                        rcvData = Encoding.ASCII.GetString(myData, 0, received);
                        
                        //Receive data for what number to put in each cell and put it
                        for (int i = 0; i < 81; i++)
                            button(i).Text = rcvData.Substring(i, 1) == "0" ? "" : rcvData.Substring(i, 1);

                        //Receive data for whether each button is enabled
                        for (int i = 0; i < 81; i++)
                        {
                            button(i).Enabled = rcvData.Substring(i + 81, 1) == "1" ? true : false;
                            button(i).UseVisualStyleBackColor = rcvData.Substring(i + 81, 1) == "1" ? true : false;
                        }

                        //Synchronize the 3 check boxes
                        chkDoing.Checked = rcvData.Substring(162, 1) == "0" ? false : true; chkDoing_ChkChg();
                        chkWrong.Checked = rcvData.Substring(163, 1) == "0" ? false : true; chkWrong_ChkChg();
                        chkTip.Checked   = rcvData.Substring(164, 1) == "0" ? false : true;

                        btnSolve.Enabled = true;
                        btnDoing.Enabled = true;
                        chkDoing.Enabled = true;
                        chkWrong.Enabled = true;
                        chkTip.Enabled = true;

                        //Input data into the array for solving                        
                        for (int i = 0; i < 81; i++)
                            Sudoku_arr[i] = rcvData.Substring(i + 81, 1) == "0" ? Convert.ToInt32(rcvData.Substring(i, 1)) : 0;

                        //Try to solve it
                        tries = 0;
                        while (!Solve())
                        {
                            Solve();
                            tries++;
                            if (tries > 50)
                            {
                                MessageBox.Show(msg, "The Unsolvable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Solvable = false;

                                btnSolve.Enabled = false;
                                btnDoing.Enabled = false;
                                chkDoing.Enabled = false;
                                chkWrong.Enabled = false;

                                return;
                            }
                        }
                        Solvable = true;
                    }                    
                }
                netStrm.Close();
            }
            else
            {
                MessageBox.Show("Cannot find client. Closing connections.");
            }
            try
            {
                theTCPLstn.Stop();
                theTCPSock.Close();
            }
            catch { }
        }
        
        private void RunTCPClient()
        {
            string sendStr = "";
            int count = 0;
            while (true)
            {
                try
                {
                    theTCPClient = new TcpClient(remoteIP, 9050);
                    break;
                }
                catch { count += 1; Thread.Sleep(5); }
            }
            if (count < 10)
            {
                byte[] myData = new byte[165];
                // the netStrm is a "text only" stream encoded as ASCII
                NetworkStream netStrm = theTCPClient.GetStream();
                while (connected)
                {
                    sendStr = "";
                    //Get the number in each cell and send data
                    for (int i = 0; i < 81; i++)
                        sendStr += button(i).Text == "" ? "0" : button(i).Text;

                    //See whether each cell should be enable or not and send data
                    for (int i = 0; i < 81; i++)
                        sendStr += button(i).Enabled ? "1" : "0";

                    //See whether the 2 checkboxes are checked and send data
                    sendStr += chkDoing.Checked ? "1" : "0";
                    sendStr += chkWrong.Checked ? "1" : "0";
                    sendStr += chkTip.Checked ? "1" : "0";

                    myData = Encoding.ASCII.GetBytes(sendStr);
                    netStrm.Write(myData, 0, myData.Length);
                    netStrm.Flush();
                    
                    clntThread.Suspend();
                }
                netStrm.Close();                
            }
            else
            {
                MessageBox.Show("Cannot connect to the server. Closing connection");
            }
            try
            {
                theTCPClient.Close();
            }
            catch { }
        }
    }
}
