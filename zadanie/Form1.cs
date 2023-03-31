using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace zadanie
{
    public partial class Form1 : Form
    {
        DataBase_Connection DataBase = new DataBase_Connection();

        int selectedrow;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            createcolumns();
            refreshdatagrid(dataGridView1);
        }

        private void createcolumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("Title", "Название товара");
            dataGridView1.Columns.Add("Cost", "Цена");
            dataGridView1.Columns.Add("Article", "Артикул");
        }

        private void readsinglerow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetInt32(3));
        }

        private void refreshdatagrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            String querystring = $"select * from CHART";

            SqlCommand com = new SqlCommand(querystring, DataBase.GetConnection());

            DataBase.OpenConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                readsinglerow(dgw, reader);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] category = { "100-500", "600-1000", "1100-1700", "1800-2500" };
            double[] Cost = { Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox7.Text), Convert.ToDouble(textBox8.Text) };
            
            for (int i = 0; i < category.Length; i++)
            {
                Series series = chart1.Series.Add(category[i]);

                series.Points.Add(Cost[i]);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedrow = e.RowIndex;

            if (e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedrow];

                textBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();
                textBox5.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                textBox6.Text = dataGridView1.Rows[5].Cells[2].Value.ToString();
                textBox7.Text = dataGridView1.Rows[6].Cells[2].Value.ToString();
                textBox8.Text = dataGridView1.Rows[7].Cells[2].Value.ToString();
            }

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
        }
    }
}
