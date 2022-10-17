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

namespace BanHangDT
{
    public partial class Cart : Form
    {
        private int userCARTID;
        SqlConnection conn = null;
        string strcnn = @"Data Source=LAPTOP-J7E06IKN;Initial Catalog=QLDT;Integrated Security=True";
        public Cart(int cartID)
        {
            InitializeComponent();
            userCARTID = cartID;
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(strcnn);
            conn.Open();
            string sqlSP = ("Select * from product");
            SqlDataAdapter adapterProduct = new SqlDataAdapter(sqlSP, conn);
            DataTable tableProduct = new DataTable();
            adapterProduct.Fill(tableProduct);
            Dictionary<int, string> comboSource = new Dictionary<int, string>();
            string sql = ("Select * from cart where userCARTID = '" + userCARTID.ToString() + "'");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                int index = dtCart.Rows.Add();
                dtCart.Rows[index].Cells[0].Value = dr["userCARTID"].ToString();
                for (int j = 0; j < tableProduct.Rows.Count; j++)
                {
                    DataRow drProduct = tableProduct.Rows[j];
                    if (drProduct["productID"].ToString() == dr["productID"].ToString())
                    {
                        dtCart.Rows[index].Cells[1].Value = drProduct["productNAME"].ToString();
                    }
                }
                dtCart.Rows[index].Cells[2].Value = dr["productAMOUNT"].ToString();
            }
        }

        private void Cart_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            var mainForm = new Form1(false);
            mainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new Form1(false);
            mainForm.Show();
        }
    }
}
