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

namespace hastaTAkipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        frmSqlbaglanti bgl = new frmSqlbaglanti();

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnKayıt_Click(object sender, EventArgs e)
        {
            frmkayit fr = new frmkayit();
            fr.Show();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtKulAdi.Text != "" && txtSifre.Text != "")
            {
                try
                {
                    SqlCommand giris = new SqlCommand("girisYap", bgl.baglan());
                    giris.CommandType = CommandType.StoredProcedure; // ❗️Boşluk silindi
                    giris.Parameters.AddWithValue("@kulAdi", txtKulAdi.Text); // ❗️isim düzeltildi
                    giris.Parameters.AddWithValue("@sifre", txtSifre.Text);

                    SqlDataReader dr = giris.ExecuteReader(); // ❗️eşittir işareti eklendi

                    if (dr.Read())
                    {
                        MessageBox.Show("Giriş İşlemi Başarılı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmAnaSayfa fr = new frmAnaSayfa();
                        this.Hide();
                        fr.Show();
                    }
                    else
                    {
                        MessageBox.Show("Giriş İşlemi Başarısız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    dr.Close();
                    bgl.baglan().Close(); // bağlantıyı kapatmak önemli
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
