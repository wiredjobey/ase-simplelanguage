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

        // pointer bitmap and canvas
        Bitmap pointerbmp = new Bitmap(bmpX, bmpY);
        Canvas pointer;

        // loading a canvas and parser
        Canvas MyCanvas;
        Parser p = new Parser();

        // initialise the form and canvas
        public Form1()
        {
            InitializeComponent();
            MyCanvas = new Canvas(Graphics.FromImage(Outputbmp), penColour, penSize);

            pointer = new Canvas(Graphics.FromImage(pointerbmp), "Red", 3);
            pointer.DrawRectangle(1, 1);
        }

        // reset the output and editor
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edTextBox.Clear();
            errorLabel.Text = "";
            MyCanvas.Clear();
            MyCanvas.MoveTo(0, 0);

            pointer.MoveTo(0, 0);
            pointer.Clear();
            pointer.DrawRectangle(1, 1);

            Refresh();
        }

        // handles the save dialog
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // creates a dialog to choose save path and filename
            SaveFileDialog saveFile = new SaveFileDialog();

            // initialising defaults for the dialog
            saveFile.FileName = "program";
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "Text files (*.txt)|*.txt";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DialogResult result = saveFile.ShowDialog();

            // determines if the dialog result is successful and saves the file as a plaintext document
            if (result == DialogResult.OK)
            {
                edTextBox.SaveFile(saveFile.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        // handles the load dialog
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create a dialog to choose a file to open
            OpenFileDialog openFile = new OpenFileDialog();

            // initialise defaults for the dialog
            openFile.DefaultExt = "*.txt";
            openFile.Filter = "Text Files (*.txt)|*.txt";

            DialogResult result = openFile.ShowDialog();

            // determines if the dialog result is successful and loads the file into the editor
            if (result == DialogResult.OK)
            {
                edTextBox.LoadFile(openFile.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        // exit the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // checks whether the user has finished typing by checking for an enter key press in the commandline box
        private void cmdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                errorLabel.Text = "";
                List<string> errors = new List<string>();

                // if the commandline text is "run", parse the editor text, else parse the commandline text
                if (cmdTextBox.Text == "run")
                {
                    // split each line of the editor text into an array of commands to be parsed
                    string[] commands = edTextBox.Lines;

                    for (int i = 0; i < commands.Length; i++)
                    {
                        try
                        {
                            p.parseCommand(commands[i], MyCanvas, pointer);
                        }
                        catch (Exception ex)
                        {
                            errors.Add("Line " + (i + 1) + ": " + ex.Message);
                        }
                    }

                    if (errors.Count != 0)
                    {
                        errorLabel.Text = String.Join("\n", errors);
                    }

                    cmdTextBox.Text = "";
                    Refresh();
                }
                else
                {
                    try
                    {
                        p.parseCommand(cmdTextBox.Text, MyCanvas, pointer);
                    }
                    catch (Exception ex)
                    {
                        errorLabel.Text = ex.Message;
                    }
                    cmdTextBox.Text = "";
                    Refresh();
                }
            }
        }

        // draws the canvas image in the output box
        private void outputBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.DrawImageUnscaled(Outputbmp, 0, 0);
            gfx.DrawImageUnscaled(pointerbmp, 0, 0);
        }
    }
}
