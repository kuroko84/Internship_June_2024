using System;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Data;


namespace TestWF
{
    public partial class TestWF : Form
    {
        string connectionString = @"Data Source=desktop-nr76il4;Initial Catalog=test;
                                    Integrated Security=True;Encrypt=False";
        public TestWF()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from users", sqlCon);
                DataTable dtbl = new DataTable();

                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;

            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
