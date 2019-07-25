using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson11A
{
    public partial class StudentInfoForm : Form
    {
        public StudentInfoForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Program.mainForm.Show();
            this.Hide();
        }

        private void StudentInfoForm_Activated(object sender, EventArgs e)
        {
            // open file stream to read
            using (StreamReader inputString = new StreamReader(
                File.Open("student.txt", FileMode.Open)))
            {
                // read stuff from the file into the Student object
                Program.student.id = int.Parse(inputString.ReadLine());
                Program.student.StudentID = inputString.ReadLine();
                Program.student.FirstName = inputString.ReadLine();
                Program.student.LastName = inputString.ReadLine();

                // cleanup
                inputString.Close();
                inputString.Dispose();
            }

            IDDataLabel.Text = Program.student.id.ToString();
            StudentIDDataLabel.Text = Program.student.StudentID;
            FirstNameDataLabel.Text = Program.student.FirstName;
            LastNameDataLabel.Text = Program.student.LastName;
        }

        private void StudentInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
