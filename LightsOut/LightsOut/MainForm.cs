using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{ 
    public partial class MainForm : Form
    {
        private const int GridOffset = 25;
        private const int GridLength = 200;
        private const int NumCells = 3;
        private const int CellLength = GridLength / NumCells;

        private bool[,] grid;
        private Random rand;
        public MainForm()
        {
            InitializeComponent();

            rand = new Random();

            grid = new bool[NumCells, NumCells];

            //Turn entire grid on
            for (int r = 0; r < NumCells; r++){
                for (int c = 0; c < NumCells; c++){
                    grid[r, c] = true;
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int r = 0; r < NumCells; r++){
                for (int c = 0; c < NumCells; c++){


                    //Get proper pen and brush for on/off
                    //grid section

                    Brush brush;
                    Pen pen;

                    if (grid[r, c]) {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    } else {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    //Determin (x,y) coord of row and col to draw rectangle
                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;

                    //Draw outline and inner Rectangle
                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private bool PlayerWon()
        {
            bool won = true;

            for (int r = 0; r < NumCells || won == false; r++){
                for (int c = 0; c < NumCells || won == false; c++){
                    if (grid[r, c])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            //Make sure click was inside the grid
            if (e.X < GridOffset || e.X > CellLength * NumCells + GridOffset ||
                e.Y < GridOffset || e.Y > CellLength * NumCells + GridOffset){
                return;
            }

            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;

            for (int i = r - 1; i <= r + 1; i++){
                for (int j = c - 1; j <= c + 1; j++){
                    if (i >= 0 && i < NumCells && j >= 0 && j < NumCells) {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }

            this.Invalidate();

            if (PlayerWon()) {
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            // Fill grid with either white or black
            for (int r = 0; r < NumCells; r++){
                for (int c = 0; c < NumCells; c++) {
                    grid[r, c] = rand.Next(2) == 1;
                }
            }

            this.Invalidate();
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
