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
namespace ExpensesManager
{
    public partial class InvoicesManager : Form
    {
        public InvoicesManager()
        {
            InitializeComponent();
        }
        private void openButton_Click(object sender, EventArgs e)
        {
            var pathToFile = pathTextBox.Text;
            if (!File.Exists(pathToFile))
            {
                MessageBox.Show("The File is not exist... Please Try again");
                return;
            }
            var lines = File.ReadAllLines(pathToFile);
            List<Decimal> listTotal = new List<decimal>();
            for (int i = 1; i < lines.Length; i++) 
            {
                var line = lines[i];
                var split = line.Split('|');
                var price = split[1];
                decimal addp = Convert.ToDecimal(price);
                listTotal.Add(addp);
            }
            Dictionary<string, string>  category_and_date = new Dictionary<string, string>();
            int categories = 0;
            for (int i = 1; i < lines.Length; i++) 
            {
                var line = lines[i];
                var split_array = line.Split('|');
                var date = split_array[0];
                var category = split_array[2];
                if (category_and_date.ContainsKey(category))
                {
                    category_and_date[category] += date;
                }
                else
                {
                    category_and_date[category] = date;
                    categories++;
                }
            }
            Dictionary<string, decimal>  datePrice = new Dictionary<string, decimal>();                         
            for (int i = 1; i < lines.Length; i++) 
            {
                var line = lines[i];
                var split = line.Split('|');
                var date = split[0];
                var price = Convert.ToDecimal(split[1]);
                if (datePrice.ContainsKey(date))
                {
                    datePrice[date] += price;
                }
                else
                {
                    datePrice[date] = price;
                }
            }
            decimal total = 0;
            foreach (var prc in listTotal)
            {
                total += prc;               
            }
            foreach (var ln in lines)
            {
                resultTextBox.Text += ln + "\r\n";
            } 
            resultTextBox.Text += "Total expenses: " + Convert.ToString(total) + "\r\n";
            resultTextBox.Text += "Number of categories: " + Convert.ToString(categories) + "\r\n";
            foreach (var dp in datePrice)
            {
                resultTextBox.Text += dp.Key + '\t' + dp.Value + "\r\n";
            }
        }
    }
}
