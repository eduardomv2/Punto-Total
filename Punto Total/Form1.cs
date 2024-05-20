using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Punto_Total
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeChart();
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void InitializeDataGridView()
        {
            // Configurar columnas del DataGridView
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Value", "Value");
            dataGridView1.Columns.Add("Quantity", "Quantity");
        }
        private void InitializeChart()
        {
            // Configurar la serie de la gráfica
            chart1.Series.Clear();
            Series series = new Series("Sales");
            series.ChartType = SeriesChartType.Column;
            chart1.Series.Add(series);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                UpdateChart();
            }
        }

        private void ButtonAddSale_Click(object sender, EventArgs e)
        {
            string category = textBox1.Text;
            if (double.TryParse(textBox2.Text, out double price) && int.TryParse(textBox3.Text, out int quantity))
            {
                // Agregar la venta al DataGridView
                dataGridView1.Rows.Add(category, price, quantity);

                // Limpiar los TextBoxes
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Please enter valid values for price and quantity.");
            }
        }

        private void UpdateChart()
        {
            chart1.Series[0].Points.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    string category = row.Cells[0].Value.ToString();
                    if (double.TryParse(row.Cells[1].Value.ToString(), out double price) && int.TryParse(row.Cells[2].Value.ToString(), out int quantity))
                    {
                        double total = price * quantity;
                        chart1.Series[0].Points.AddXY(category, total);
                    }
                }
            }

            chart1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double totalEarnings = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    if (double.TryParse(row.Cells[1].Value.ToString(), out double price) && int.TryParse(row.Cells[2].Value.ToString(), out int quantity))
                    {
                        totalEarnings += price * quantity;
                    }
                }
            }

            // Mostrar el total de ganancias en un MessageBox
            MessageBox.Show($"Total Earnings: {totalEarnings:C}", "Total Earnings");

            // También podemos mostrar el total en un Label
            labelTotalEarnings.Text = $"Total Earnings: {totalEarnings:C}";
        }
    }
}
