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
    public partial class UpdateDelete : Form
    {
        public UpdateDelete()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Gym_Management;Integrated Security=True");
        private void populate()
        {
            Con.Open();
            string query = "select * from MemberTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            MemberData.DataSource = ds.Tables[0];
            Con.Close();
        }



        private void UpdateDelete_Load(object sender, EventArgs e)
        {
            populate();
        }
        int key = 0;
        
        private void MemberData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(MemberData.CurrentRow.Cells[0].Value.ToString());
            NameTb.Text = MemberData.CurrentRow.Cells[1].Value.ToString();
            PhoneTb.Text = MemberData.CurrentRow.Cells[2].Value.ToString();
            GenCb.Text = MemberData.CurrentRow.Cells[3].Value.ToString();
            AgeTb.Text = MemberData.CurrentRow.Cells[4].Value.ToString();
            AmountTb.Text = MemberData.CurrentRow.Cells[5].Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NameTb.Text = "";
            PhoneTb.Text = "";
            GenCb.Text = "";
            AgeTb.Text = "";
            AmountTb.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Silinecek Üyeyi Seçin");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from MemberTbl where MId='" + key + "'; ";
                    SqlCommand cmd = new SqlCommand(query,Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Üye Başarıyla Silindi");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (key == 0 || NameTb.Text==""||PhoneTb.Text==""|| AgeTb.Text==""|| GenCb.Text==""||AmountTb.Text=="")
            {
                MessageBox.Show("Bilgileri Kontrol Ediniz.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update MemberTbl set MAdsoyad = '"+NameTb.Text+"',MTelefon='"+PhoneTb.Text+"',MCinsiyet='" + GenCb.Text + "',MYas='"+AgeTb.Text+"',MTutar='"+AmountTb.Text+"'where MId="+key+";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Üye Başarıyla Güncellendi");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
