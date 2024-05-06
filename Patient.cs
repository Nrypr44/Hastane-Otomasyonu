
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.DirectoryServices.ActiveDirectory;

namespace HOS
{
    public partial class Patient : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection(" ");
        public Patient()
        {
            InitializeComponent();
            LoadComboBoxData();
        }
        private void Patient_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from patient";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].HeaderText = "Hasta ID";
            dataGridView1.Columns[1].HeaderText = "Hasta İsmi";
            dataGridView1.Columns[2].HeaderText = "Hasta Soyisim";
            dataGridView1.Columns[3].HeaderText = "Hasta TC";
            dataGridView1.Columns[4].HeaderText = "Doktor ID";
            dataGridView1.Columns[5].HeaderText = "Doktor Ad";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            int selectedIndex = comboBox1.SelectedIndex;
            string selectedValue = comboBox1.Items[selectedIndex].ToString();
            string[] values = selectedValue.Split('-');
            int firstValue = Convert.ToInt32(values[0]);
            string secondValue = Convert.ToString(values[1]);

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO patient (pat_id,pat_name,pat_sname,pat_tc,pat_doc_id,pat_doc_name) " +
                                         "VALUES (@p1, @p2,@p3,@p4,@p5,@p6)", connection);

            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox3.Text);
            cmd.Parameters.AddWithValue("@p3", textBox2.Text);
            cmd.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
            if (firstValue != null)
            {
                cmd.Parameters.AddWithValue("@p5", firstValue);
            }
            if (secondValue != null)
            {
                cmd.Parameters.AddWithValue("@p6", secondValue);
            }

            cmd.ExecuteNonQuery();
            connection.Close();

        }

        private void LoadComboBoxData()
        {
            NpgsqlCommand command = null;
            NpgsqlDataReader reader = null;

            try
            {

                connection.Open();

                string query = "SELECT d_id,d_name,d_sname FROM doctor";
                command = new NpgsqlCommand(query, connection);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetInt32(0) + "-" + reader.GetString(1)+" "+reader.GetString(2));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                connection?.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            var selected = comboBox1.SelectedItem;

            NpgsqlCommand cmd = new NpgsqlCommand("update patient set pat_name = @p2,pat_sname=@p3,pat_tc=@p4,pat_doc_name=@p5 where pat_id=@p1", connection);

            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox3.Text);
            cmd.Parameters.AddWithValue("@p3", textBox2.Text);
            cmd.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
            if (selected != null)
            {
                cmd.Parameters.AddWithValue("@p5", selected);
            }

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("delete from patient where pat_id = @p1", connection);
            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.ExecuteNonQuery();
            connection.Close();

        }
    }
}
