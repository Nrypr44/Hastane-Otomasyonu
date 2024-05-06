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
    public partial class Doctor : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection(" ");
        public Doctor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from doctor";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].HeaderText = "Doktor ID";
            dataGridView1.Columns[1].HeaderText = "Doktor Ad";
            dataGridView1.Columns[2].HeaderText = "Doktor Soyad";
            dataGridView1.Columns[3].HeaderText = "Bölüm";
            dataGridView1.Columns[4].HeaderText = "Doktor TC";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();


            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO doctor (d_id,d_name,d_sname,d_spec,d_tc) " +
                                         "VALUES (@p1,@p2,@p3,@p4,@p5)", connection);

            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);
            cmd.Parameters.AddWithValue("@p4", textBox4.Text);
            cmd.Parameters.AddWithValue("@p5", int.Parse(textBox5.Text));

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();


            NpgsqlCommand cmd = new NpgsqlCommand("update doctor set d_name = @p2,d_sname=@p3,d_spec=@p4,d_tc=@p5 where d_id=@p1", connection);
            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);
            cmd.Parameters.AddWithValue("@p4", textBox4.Text);
            cmd.Parameters.AddWithValue("@p5", int.Parse(textBox5.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("delete from doctor where d_id = @p1", connection);
            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
