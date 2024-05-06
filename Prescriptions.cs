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
    public partial class Prescriptions : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection(" ");
        public Prescriptions()
        {
            InitializeComponent();
            LoadComboBoxDoctorData();
            LoadComboBoxPatienceData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from prescription";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].HeaderText = "İlaç ID";
            dataGridView1.Columns[1].HeaderText = "İlaç Adı";
            dataGridView1.Columns[2].HeaderText = "İlaç Türü";
            dataGridView1.Columns[3].HeaderText = "Hasta ID";
            dataGridView1.Columns[4].HeaderText = "Hasta Ad Soyad";
            dataGridView1.Columns[5].HeaderText = "Doktor ID";
            dataGridView1.Columns[6].HeaderText = "Doktor Ad Soyad";
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            connection.Open();
            int selectedIndex = comboBox1.SelectedIndex;
            string selectedValue = comboBox1.Items[selectedIndex].ToString();
            string[] values = selectedValue.Split('-');
            int firstValue = Convert.ToInt32(values[0]);
            string secondValue = Convert.ToString(values[1]);

            int selectedIndex1 = comboBox2.SelectedIndex;
            string selectedValue1 = comboBox2.Items[selectedIndex1].ToString();
            string[] values1 = selectedValue1.Split('-');
            int firstValue1 = Convert.ToInt32(values1[0]);
            string secondValue1 = Convert.ToString(values1[1]);

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO prescription (med_id,med_name,med_type,pid,pname,did,dname) " +
                                         "VALUES (@p1, @p2,@p3,@p4,@p5,@p6,@p7)", connection);

            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox3.Text);
            cmd.Parameters.AddWithValue("@p3", textBox2.Text);
            if (firstValue != null)
            {
                cmd.Parameters.AddWithValue("@p4", firstValue);
            }
            if (secondValue != null)
            {
                cmd.Parameters.AddWithValue("@p5", secondValue);
            }
            if (firstValue1 != null)
            {
                cmd.Parameters.AddWithValue("@p6", firstValue1);
            }
            if (secondValue1 != null)
            {
                cmd.Parameters.AddWithValue("@p7", secondValue1);
            }

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            int selectedIndex = comboBox1.SelectedIndex;
            string selectedValue = comboBox1.Items[selectedIndex].ToString();
            string[] values = selectedValue.Split('-');
            int firstValue = Convert.ToInt32(values[0]);
            string secondValue = Convert.ToString(values[1]);

            int selectedIndex1 = comboBox2.SelectedIndex;
            string selectedValue1 = comboBox2.Items[selectedIndex1].ToString();
            string[] values1 = selectedValue1.Split('-');
            int firstValue1 = Convert.ToInt32(values1[0]);
            string secondValue1 = Convert.ToString(values1[1]);

            NpgsqlCommand cmd = new NpgsqlCommand("update prescription set med_name=@p2,med_type=@p3,pid=@p4,pname=@p5,did=@p6,dname=@p7 where med_id=@p1", connection);

            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);

            if (firstValue != null)
            {
                cmd.Parameters.AddWithValue("@p4", firstValue);
            }
            if (secondValue != null)
            {
                cmd.Parameters.AddWithValue("@p5", secondValue);
            }
            if (firstValue1 != null)
            {
                cmd.Parameters.AddWithValue("@p6", firstValue1);
            }
            if (secondValue1 != null)
            {
                cmd.Parameters.AddWithValue("@p7", secondValue1);
            }

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("delete from prescription where med_id = @p1", connection);
            cmd.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        private void LoadComboBoxPatienceData()
        {
            NpgsqlCommand command = null;
            NpgsqlDataReader reader = null;

            try
            {

                connection.Open();

                string query = "SELECT pat_id,pat_name,pat_sname FROM patient";
                command = new NpgsqlCommand(query, connection);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetInt32(0) +"-"+ reader.GetString(1)+ " " +reader.GetString(2));
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

        private void LoadComboBoxDoctorData()
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
                    comboBox2.Items.Add(reader.GetInt32(0) + "-" + reader.GetString(1) + " " + reader.GetString(2));
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
    }
}
