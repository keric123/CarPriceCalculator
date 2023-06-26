using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TextBox = System.Windows.Controls.TextBox;

namespace CarPriceCalculator
{
    public partial class MainWindow : Window
    {
        private List<decimal> carPrices = new List<decimal>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SumButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateAverage();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateDesiredPrice();
        }

        private void CalculateAverage()
        {
            try
            {
                carPrices.Clear();
                carPrices.AddRange(GetPricesFromTextBoxes());

                decimal sum = carPrices.Sum();
                decimal average = sum / carPrices.Count;
                averageLabel.Content = "Average Car Price: " + average.ToString("F2") + " EUR";
                desiredPriceLabel.Content = "Desired Price:";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating average: " + ex.Message);
            }
        }

        private void CalculateDesiredPrice()
        {
            try
            {
                decimal marginPercentage;
                if (decimal.TryParse(marginTextBox.Text, out marginPercentage))
                {
                    decimal sum = carPrices.Sum();
                    decimal average = sum / carPrices.Count;
                    decimal desiredPrice = average + (average * (marginPercentage / 100));
                    desiredPriceLabel.Content = "Desired Price: " + desiredPrice.ToString("C") + " EUR (Avg: " + average.ToString("C") + " EUR + " + marginPercentage + "%)";
                }
                else
                {
                    MessageBox.Show("Invalid profit margin percentage.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating desired price: " + ex.Message);
            }
        }




        private IEnumerable<decimal> GetPricesFromTextBoxes()
        {
            List<TextBox> textBoxes = new List<TextBox>
            {
                priceTextBox1, priceTextBox2, priceTextBox3, priceTextBox4, priceTextBox5, priceTextBox6, priceTextBox7
            };

            return textBoxes
                .Where(textBox => !string.IsNullOrEmpty(textBox.Text))
                .Select(textBox => decimal.Parse(textBox.Text));
        }
    }
}
