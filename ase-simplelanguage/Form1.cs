using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase_simplelanguage
{
    public partial class Form1 : Form
    {

        // initial pen colour and size
        const string penColour = "Black";
        const int penSize = 1;

        // bitmap size in pixels
        const int bmpX = 384;
        const int bmpY = 384;
        
        // bitmap to draw, using size constants defined above
        Bitmap Outputbmp = new Bitmap(bmpX, bmpY);

        // loading a canvas and parser
        Canvas MyCanvas;
        Parser p = new Parser();

        public Form1()
        {
            InitializeComponent();
            MyCanvas = new Canvas(Graphics.FromImage(Outputbmp), penColour, penSize);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edTextBox.Clear();
            MyCanvas.Clear();
            Refresh();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog to request a path and file name to save to.
            SaveFileDialog saveFile = new SaveFileDialog();

            // Initialize the SaveFileDialog to specify the RTF extension for the file.
            saveFile.FileName = "program";
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "Text files (*.txt)|*.txt";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DialogResult result = saveFile.ShowDialog();

            // Determine if the user selected a file name from the saveFileDialog.
            if (result == System.Windows.Forms.DialogResult.OK && saveFile.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                edTextBox.SaveFile(saveFile.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog to request a file to open.
            OpenFileDialog openFile = new OpenFileDialog();

            // Initialize the OpenFileDialog to look for RTF files.
            openFile.DefaultExt = "*.txt";
            openFile.Filter = "Text Files (*.txt)|*.txt";

            // Determine whether the user selected a file from the OpenFileDialog.
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Load the contents of the file into the RichTextBox.
                edTextBox.LoadFile(openFile.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void cmdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmdTextBox.Text == "run")
                {
                    string[] commands = edTextBox.Lines;

                    for (int i = 0; i < commands.Length; i++)
                    {
                        p.parseCommand(commands[i], MyCanvas);
                    }

                    cmdTextBox.Text = "";
                    Refresh();
                }
                else
                {
                    p.parseCommand(cmdTextBox.Text, MyCanvas);
                    cmdTextBox.Text = "";
                    Refresh();
                }
            }
        }

        private void outputBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.DrawImageUnscaled(Outputbmp, 0, 0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
