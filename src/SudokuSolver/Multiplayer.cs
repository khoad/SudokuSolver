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
    public partial class Multiplayer : Form
    {
        public Multiplayer()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sudoku.connected = false;
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Sudoku.connected = true;
            this.Close();
        }
 
    }
}
