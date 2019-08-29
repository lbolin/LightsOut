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
        private LightsOutGame game;
        private const int GridOffset = 50; // Distance from upper-left side of window
        private const int GridLength = 200; // Size in pixels of grid
        //private const int CellLength = GridLength / game.GridSize;
     
     
       // public event EventHandler ResizeEnd;


        public MainForm()
        {
            InitializeComponent();
            game = new LightsOutGame();
            game.NewGame(); 
           
        }


        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int CellLength = GridLength / game.GridSize;

            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    // Get proper pen and brush for on/off
                    // grid section
                    Brush brush;
                    Pen pen;
                    if (game.GetGridValue(r,c))
                    {
                        pen = Pens.Black;
                        brush = Brushes.White; // On
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black; // Off
                    }
                    // Determine (x,y) coord of row and col to draw rectangle
                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;
                    // Draw outline and inner rectangle
                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int CellLength = GridLength / game.GridSize;

            // Make sure click was inside the grid
            if (e.X < GridOffset || e.X > CellLength * game.GridSize+ GridOffset ||
            e.Y < GridOffset || e.Y > CellLength * game.GridSize + GridOffset)
                return;
            // Find row, col of mouse press
            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;

 
            // Redraw grid
            this.Invalidate();
            // Check to see if puzzle has been solved
             if (game.isWinner())
             {
             //Display winner dialog box
              MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void StartNewGame()
        {
            game.NewGame();
            Invalidate();
        }

        //private void newGameButton_Click(object sender, EventArgs e)
        //{
        //    // Fill grid with either white or black
        //    for (int r = 0; r < NumCells; r++)
        //        for (int c = 0; c < NumCells; c++)
        //            grid[r, c] = rand.Next(2) == 1;
        //    // Redraw grid
        //    this.Invalidate();
        //}



        private void exitButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        //private void newToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    newGameButton_Click(sender, e);
        //}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);

        }

       
    }

}
