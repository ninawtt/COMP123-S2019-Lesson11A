using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson11A
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the event handler for the MainForm's FormClosing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            //Program.startForm.Close();  
        }

        /// <summary>
        /// This is the event handler for the exitToolStripMenuItem Click event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// This is the event handler for the aboutToolStripMenuItem Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutForm.ShowDialog(); 
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Program.aboutForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sectionADatabaseDataSet.StudentTable' table. You can move, or remove it, as needed.
            this.studentTableTableAdapter.Fill(this.sectionADatabaseDataSet.StudentTable);

        }

        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            var StudentList =
                from student in this.sectionADatabaseDataSet.StudentTable
                select student;

            foreach (var student in StudentList.ToList())
            {
                Debug.WriteLine("Student ID: " + student.StudentID +
                    " Last Name: " + student.LastName);
            }
        }

        private void StudentDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // local scope alias
            var rowIndex = StudentDataGridView.CurrentCell.RowIndex;
            var rows = StudentDataGridView.Rows;
            var columnCount = StudentDataGridView.ColumnCount;
            var cells = rows[rowIndex].Cells;

            rows[rowIndex].Selected = true;

            string outputString = String.Empty;
            for (int index = 0; index < columnCount; index++)
            {
                outputString += cells[index].Value.ToString() + " ";
            }

            SelectionLabel.Text = outputString;

            Program.student.id = int.Parse(cells[(int)StudentField.ID].Value.ToString());
            Program.student.StudentID = cells[(int)StudentField.STUDENT_ID].Value.ToString();
            Program.student.FirstName = cells[(int)StudentField.FIRST_NAME].Value.ToString();
            Program.student.LastName = cells[(int)StudentField.LAST_NAME].Value.ToString();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // configuration for saveFileDialog
            StudentSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();  // set the default
            StudentSaveFileDialog.FileName = "Student.txt";   // set the default
            StudentSaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";


            var result = StudentSaveFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                // open a stream to write
                using (StreamWriter outputStream = new StreamWriter(
                    File.Open(StudentSaveFileDialog.FileName, FileMode.Create)))
                {
                    // write stuff to the file
                    outputStream.WriteLine(Program.student.id.ToString());
                    outputStream.WriteLine(Program.student.StudentID);
                    outputStream.WriteLine(Program.student.FirstName);
                    outputStream.WriteLine(Program.student.LastName);

                    // cleanup
                    outputStream.Close();
                    outputStream.Dispose();
                }

                MessageBox.Show("File Saved Successfully!", "Saving...",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.studentInfoForm.Show();
            this.Hide();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // configuration for openFileDialog
            StudentOpenFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentOpenFileDialog.FileName = "Student.txt";
            StudentOpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            var result = StudentOpenFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                // open file stream to read
                using (StreamReader inputStream = new StreamReader(
                    File.Open(StudentOpenFileDialog.FileName, FileMode.Open)))
                {
                    // read stuff from the file into the Student object
                    Program.student.id = int.Parse(inputStream.ReadLine());
                    Program.student.StudentID = inputStream.ReadLine();
                    Program.student.FirstName = inputStream.ReadLine();
                    Program.student.LastName = inputStream.ReadLine();

                    // cleanup
                    inputStream.Close();
                    inputStream.Dispose();
                }

                NextButton_Click(sender, e);
            }
        }

        private void openBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // configuration for openFileDialog
            StudentOpenFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentOpenFileDialog.FileName = "Student.dat";
            StudentOpenFileDialog.Filter = "Text Files (*.dat)|*.dat|All Files (*.*)|*.*";

            var result = StudentOpenFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                // open file stream to read
                using (BinaryReader inputStream = new BinaryReader(
                    File.Open(StudentOpenFileDialog.FileName, FileMode.Open)))
                {
                    // read stuff from the file into the Student object
                    Program.student.id = int.Parse(inputStream.ReadString());
                    Program.student.StudentID = inputStream.ReadString();
                    Program.student.FirstName = inputStream.ReadString();
                    Program.student.LastName = inputStream.ReadString();

                    // cleanup
                    inputStream.Close();
                    inputStream.Dispose();
                }
                NextButton_Click(sender, e);
            }
        }

        private void saveBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // configuration for saveFileDialog
            StudentSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentSaveFileDialog.FileName = "Student.dat";
            StudentSaveFileDialog.Filter = "Binary Files (*.dat)|*.dat|All Files (*.*)|*.*";

            var result = StudentSaveFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                // open a binary stream to write
                using(BinaryWriter outputStream = new BinaryWriter(
                    File.Open(StudentSaveFileDialog.FileName, FileMode.Create)))
                {
                    // write stuff to the file
                    outputStream.Write(Program.student.id.ToString());
                    outputStream.Write(Program.student.StudentID);
                    outputStream.Write(Program.student.FirstName);
                    outputStream.Write(Program.student.LastName);
                    
                    //cleanup
                    outputStream.Flush();
                    outputStream.Close();
                    outputStream.Dispose();
                }

                MessageBox.Show("Binary File Saved Successfully!", "Saving Binary File...",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
