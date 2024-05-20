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
            dataGridView1.Columns.Add("Category", "Category");
            dataGridView1.Columns.Add("Value", "Value");
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
            if (double.TryParse(textBox2.Text, out double value))
            {
                // Agregar la venta al DataGridView
                dataGridView1.Rows.Add(category, value);

                // Limpiar los TextBoxes
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid number for the value.");
            }
        }

        private void UpdateChart()
        {
            chart1.Series[0].Points.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    string category = row.Cells[0].Value.ToString();
                    double value;
                    if (double.TryParse(row.Cells[1].Value.ToString(), out value))
                    {
                        chart1.Series[0].Points.AddXY(category, value);
                    }
                }
            }

            chart1.Invalidate();
        }
    }
}
