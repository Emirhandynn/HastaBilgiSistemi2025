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

namespace hastaTAkipSistemi
{
    public partial class frmİstatistic : Form
    {
        public frmİstatistic()
        {
            InitializeComponent();
        }
        frmSqlbaglanti bgl = new frmSqlbaglanti();
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmİstatistic_Load(object sender, EventArgs e)
        {
            toplamHasta();
            YasOrtalama();
            ErkekSayi();
            KadınSayi();
            toplamExSayi();
        }
        private void toplamHasta()

        {
            SqlCommand toplam = new SqlCommand("select COUNT(*) from tbl_HastaBilgi", bgl.baglan());
            SqlDataReader dr = toplam.ExecuteReader();
            while (dr.Read()) {
            lblToplamHasta.Text = dr[0].ToString();
            }
        }
        private void YasOrtalama()

        {
            SqlCommand ortalama = new SqlCommand("select AVG(hYas) from tbl_HastaBilgi", bgl.baglan());
            SqlDataReader dr = ortalama.ExecuteReader();
            while (dr.Read())
            {
                lblYasOrtalama.Text = dr[0].ToString();
            }
        }
        private void ErkekSayi()

        {
            SqlCommand ErkekSayi = new SqlCommand("select COUNT(*) from tbl_HastaBilgi where hCinsiyet = 'Erkek'", bgl.baglan());
            SqlDataReader dr = ErkekSayi.ExecuteReader();
            while (dr.Read())
            {
                lblErkekSayi.Text = dr[0].ToString();
            }
        }
        private void KadınSayi()

        {
            SqlCommand KadınSayi = new SqlCommand("select COUNT(*) from tbl_HastaBilgi where hCinsiyet = 'Kadın'", bgl.baglan());
            SqlDataReader dr = KadınSayi.ExecuteReader();
            while (dr.Read())
            {
                lblKadınSayi.Text = dr[0].ToString();
            }
        }
        private void toplamExSayi()

        {
            SqlCommand ExSayi = new SqlCommand("select COUNT(*) from tbl_HastaBilgi where hExMi = 1", bgl.baglan());
            SqlDataReader dr = ExSayi.ExecuteReader();
            while (dr.Read())
            {
                lblExSayi.Text = dr[0].ToString();
            }
        }
    }
}
