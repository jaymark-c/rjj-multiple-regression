using Accord.Statistics.Models.Regression.Linear;
using Accord.Statistics.Models.Regression.Fitting;
using Accord.Statistics.Testing;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using Accord.Statistics.Kernels;

namespace multipleregression
{
    public partial class Form1 : Form
    {
        private MultipleLinearRegression _regression;
        private string fileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the input data from the form

            var coursesTaken = double.Parse(coursesText.Text);
            var hoursStudied = double.Parse(hoursText.Text);

            // Make a prediction using the multiple linear regression model
            var prediction = _regression.Transform(new[] { coursesTaken, hoursStudied });

            // Update the output label
            label5.Text = prediction.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                label4.Text = fileName;
                List<double> hoursStudied = new List<double>();
                List<double> coursesTaken = new List<double>();
                List<double> marks = new List<double>();

                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        double hours, courses, mark;

                        if (double.TryParse(fields[0], out courses) &&
                            double.TryParse(fields[1], out hours) &&
                            double.TryParse(fields[2], out mark))
                        {
                            hoursStudied.Add(hours);
                            coursesTaken.Add(courses);
                            marks.Add(mark);
                        }
                    }
                }

                // Now you can use the hoursStudied, coursesTaken, and marks lists
                // to train your machine learning model or perform other data analysis.


                var learner = new OrdinaryLeastSquares();
                _regression = learner.Learn(coursesTaken.Zip(hoursStudied, (x, y) => new[] { x, y }).ToArray(), marks.ToArray());
            }

            
        }
    }
}