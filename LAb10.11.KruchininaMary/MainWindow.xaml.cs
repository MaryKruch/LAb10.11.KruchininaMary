using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAb10._11.KruchininaMary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filePath = "numbers.bin";

        public MainWindow()

        {
            InitializeComponent();
        }

        private void btnFindMin_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text;
            string[] numbers = input.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            double[] values = Array.ConvertAll(numbers, double.Parse);

            double minEvenValue = values.Where((val, index) => index % 2 == 0).Min();
            txtResult.Text = $"Наименьшее значение среди элементов с четными индексами: {minEvenValue}";
        }

        private void btnSaveAndModify_Click(object sender, RoutedEventArgs e)
        {
            string[] numbersText = txtNumbers.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            double[] numbers = numbersText.Select(s => double.Parse(s)).ToArray();

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    foreach (var number in numbers)
                    {
                        writer.Write(number);
                    }
                }
            }

            ModifyFile();
        }

        private void ModifyFile()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        using (BinaryReader reader = new BinaryReader(fs))
                        {
                            fs.Seek(0, SeekOrigin.Begin);
                            double number;
                            while (fs.Position < fs.Length)
                            {
                                number = reader.ReadDouble();
                                writer.Write(number * 1.5);
                            }
                        }
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        fs.Seek(0, SeekOrigin.Begin);
                        txtOutput.Text = "";
                        double number;
                        while (fs.Position < fs.Length)
                        {
                            number = reader.ReadDouble();
                            txtOutput.Text += number + Environment.NewLine;
                        }
                    }
                }
            }
        }
    }
}
