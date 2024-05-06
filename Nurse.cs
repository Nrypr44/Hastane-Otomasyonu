using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HOS
{
    public partial class Nurse : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection(" ");
        public Nurse()
        {
            InitializeComponent();
        }

        private void Nurse_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from nurse";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].HeaderText = "Hemşire ID";
            dataGridView1.Columns[1].HeaderText = "Hemşire Ad";
            dataGridView1.Columns[2].HeaderText = "Hemşire Soyad";
            dataGridView1.Columns[3].HeaderText = "Hemşire TC";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();


            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO nurse (n_id,n_name,n_sname,n_tc) " +
                                         "VALUES (@p1,@p2,@p3,@p4)", connection);

            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox3.Text);
            cmd.Parameters.AddWithValue("@p3", textBox2.Text);
            cmd.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));


            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();


            NpgsqlCommand cmd = new NpgsqlCommand("update nurse set n_name = @p2,n_sname=@p3,n_tc=@p4 where n_id=@p1", connection);
            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox3.Text);
            cmd.Parameters.AddWithValue("@p3", textBox2.Text);
            cmd.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("delete from nurse where n_id = @p1", connection);
            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
