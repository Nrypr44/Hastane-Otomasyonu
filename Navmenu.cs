namespace HOS
{
    public partial class Navmenu : Form
    {
        public Navmenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Patient();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Doctor();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Nurse();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new Room();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new Prescriptions();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new Appointment();
            form.ShowDialog();
        }
    }
}
