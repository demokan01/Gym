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

namespace GymT
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Gym_Management;Integrated Security=True");

        private void FillName()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select MAdsoyad from MemberTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("MAdsoyad", typeof(string));
            dt.Load(rdr);
            NameCb.ValueMember = "MAdsoyad";
            NameCb.DataSource = dt;
            Con.Close();
        }
        private void filterByName()
        {
            Con.Open();
            string query = "select * from PaymentTbl where PUye='" + SearchName.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);
            MemberPayment.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from PaymentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            MemberPayment.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //NameTb.Text = "";
            AmountTb.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if(NameCb.Text==""||AmountTb.Text=="")
            {
                MessageBox.Show("Bilgileri Kontrol Ediniz.");
            }
            else
            {
                string payperiode = Periode.Value.Month.ToString()+Periode.Value.Year.ToString();
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from PaymentTbl where PUye ='" +NameCb.SelectedValue.ToString() +"' and PAy='"+payperiode+"'",Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString()=="1")
                {
                    MessageBox.Show("Bu Ay Ödemesi Zaten Yapıldı");
                }
                else
                {
                    string query = "insert into PaymentTbl values('" + payperiode + "','" + NameCb.SelectedValue.ToString() + "','" + AmountTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ödeme Başarılı.");
                }
                Con.Close();
                populate();
            }
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            FillName();
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            filterByName();
            SearchName.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
