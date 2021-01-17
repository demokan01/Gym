using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymT
{
    public partial class AddMember : Form
    {
        public AddMember()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Gym_Management;Integrated Security=True");

        private void AddMember_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || PhoneTb.Text == "" || AmountTb.Text == "" || AgeTb.Text =="")
            {
                MessageBox.Show("Bilgileri Kontrol Ediniz.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into MemberTbl values('" + NameTb.Text + "','" + PhoneTb.Text + "','" + GenCb.SelectedItem.ToString() + "','" + AgeTb.Text + "','" + AmountTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Üye Başarıyla Eklendi.");
                    Con.Close();
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    AgeTb.Text = "";
                    AmountTb.Text = "";
                    GenCb.Text = "";

                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NameTb.Text = "";
            PhoneTb.Text= "";
            AgeTb.Text = "";
            AmountTb.Text = "";
            GenCb.Text = "";
            

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }
    }
}
