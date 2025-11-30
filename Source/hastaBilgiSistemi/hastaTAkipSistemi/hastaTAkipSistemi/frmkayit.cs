using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace hastaTAkipSistemi
{
    public partial class frmkayit : Form
    {
        public frmkayit()
        {
            InitializeComponent();
        }

        frmSqlbaglanti bgl = new frmSqlbaglanti();

        private void btnKayıt_Click(object sender, EventArgs e)
        {
            if (txtKulAdi.Text != "" && txtSifre.Text != "")
            {
                try
                {
                    using (SqlCommand kayit = new SqlCommand("kayitOl", bgl.baglan()))
                    {
                        kayit.CommandType = CommandType.StoredProcedure;
                        kayit.Parameters.AddWithValue("@kulAdi", txtKulAdi.Text);
                        kayit.Parameters.AddWithValue("@sifre", txtSifre.Text);

                        kayit.ExecuteNonQuery();
                    }

                    MessageBox.Show("Kayıt işlemi başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kayıt sırasında bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bgl.baglan().Close(); // Bağlantı her durumda kapatılır
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // label1_Click artık sadece tasarımsal amaçlı kullanılabilir (örneğin label tıklanmasın)
        private void label1_Click(object sender, EventArgs e)
        {
            // Boş bırakıldı
        }
    }
}
