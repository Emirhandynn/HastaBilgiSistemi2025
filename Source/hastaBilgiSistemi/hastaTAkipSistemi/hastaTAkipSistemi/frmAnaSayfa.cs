using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace hastaTAkipSistemi
{
    public partial class frmAnaSayfa : Form
    {
        public frmAnaSayfa()
        {
            InitializeComponent();
        }

        frmSqlbaglanti bgl = new frmSqlbaglanti();

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            Listele();
            durumDoldur();
            bolumDoldur();
        }

        private void Listele()
        {
            SqlCommand komut = new SqlCommand("listele", bgl.baglan());
            komut.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            bgl.baglan().Close();
        }

        private void durumDoldur()
        {
            SqlCommand durum = new SqlCommand("durumDoldur", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(durum);
            DataTable dt = new DataTable();
            da.Fill(dt);

            txtDurum.DataSource = dt;
            txtDurum.DisplayMember = "durumAd";
            txtDurum.ValueMember = "durumID";
        }

        private void bolumDoldur()
        {
            SqlCommand bolum = new SqlCommand("bolumDoldur", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(bolum);
            DataTable dt = new DataTable();
            da.Fill(dt);

            txtBölüm.DataSource = dt;
            txtBölüm.DisplayMember = "bolumAd";
            txtBölüm.ValueMember = "bolumID";
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[secilen].Cells[11].Value.ToString();
            txtAD.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtTc.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtYas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtCinsiyet.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSikayet.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtTarih.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            txtDurum.SelectedValue = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            txtBölüm.SelectedValue = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
            lblEx.Text = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
        }

        private void rbEvet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEvet.Checked)
            {
                lblEx.Text = "True";
            }
            else
            {
                lblEx.Text = "False";
            }
        }

        private void label11_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblEx_TextChanged(object sender, EventArgs e)
        {
            if (lblEx.Text == "True")

            {

                rbEvet.Checked = true;
            }
            else
            {
                rbHayır.Checked = true;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAD.Text != "" && txtBölüm.Text != "" && txtCinsiyet.Text != "" && txtDurum.Text != "" && txtSikayet.Text != "" && txtSoyad.Text != "" && txtTc.Text != "" && txtTelefon.Text != "" && txtYas.Text != "")
            {
                kaydet();
            }

            else
            {
                MessageBox.Show("Lütfen İlgili Tüm Alanları Doldurunuz!", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kaydet ()
        {
            SqlCommand kaydet = new SqlCommand("kaydet", bgl.baglan());
            kaydet.CommandType = CommandType.StoredProcedure;
            kaydet.Parameters.AddWithValue("ad", txtAD.Text.ToString());
            kaydet.Parameters.AddWithValue("soyad", txtSoyad.Text.ToString());
            kaydet.Parameters.AddWithValue("tc", txtTc.Text.ToString());
            kaydet.Parameters.AddWithValue("tel", txtTelefon.Text.ToString());
            kaydet.Parameters.AddWithValue("yas", int.Parse(txtYas.Text.ToString()));
            kaydet.Parameters.AddWithValue("cins", txtCinsiyet.Text.ToString());
            kaydet.Parameters.AddWithValue("sikayet", txtSikayet.Text.ToString ());
            kaydet.Parameters.AddWithValue("tarih", DateTime.Now);
            kaydet.Parameters.AddWithValue("durum", txtDurum.SelectedValue);
            kaydet.Parameters.AddWithValue("bolum", txtBölüm.SelectedValue);

            if (lblEx.Text == "True")

            {
                kaydet.Parameters.AddWithValue("ex", 1);
            }
            else
            {
                kaydet.Parameters.AddWithValue("ex", 0);
            }
            kaydet.ExecuteNonQuery();

            MessageBox.Show("Kayıt Başarıyla Eklendi", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            sil();
        }
        private void sil()
        {
            DialogResult dr = MessageBox.Show(
                $"{txtID.Text} numaralı kayıt silinecek. Onaylıyor musunuz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dr == DialogResult.Yes)
            {
                SqlCommand sil = new SqlCommand("sil", bgl.baglan());
                sil.CommandType = CommandType.StoredProcedure;
                sil.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                sil.ExecuteNonQuery();

                MessageBox.Show(
                    "Kayıt başarıyla silindi.",
                    "Kayıt Silme Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                Listele();
                bgl.baglan().Close();
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                $"{txtID.Text} numaralı kayıt güncellenecek. Onaylıyor musunuz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (dr == DialogResult.Yes) 
                guncelle();
        }
        private void guncelle()
        {
            SqlCommand guncelle = new SqlCommand("guncelle", bgl.baglan());
            guncelle.CommandType = CommandType.StoredProcedure;
            guncelle.Parameters.AddWithValue("id", int.Parse(txtID.Text));
            guncelle.Parameters.AddWithValue("ad", txtAD.Text.ToString());
            guncelle.Parameters.AddWithValue("soyad", txtSoyad.Text.ToString());
            guncelle.Parameters.AddWithValue("tc", txtTc.Text.ToString());
            guncelle.Parameters.AddWithValue("tel", txtTelefon.Text.ToString());
            guncelle.Parameters.AddWithValue("yas", int.Parse(txtYas.Text.ToString()));
            guncelle.Parameters.AddWithValue("cins", txtCinsiyet.Text.ToString());
            guncelle.Parameters.AddWithValue("sikayet", txtSikayet.Text.ToString());
            guncelle.Parameters.AddWithValue("tarih", DateTime.Now);
            guncelle.Parameters.AddWithValue("durum", txtDurum.SelectedValue);
            guncelle.Parameters.AddWithValue("bolum", txtBölüm.SelectedValue);

            if (lblEx.Text == "True")

            {
                guncelle.Parameters.AddWithValue("ex", 1);
            }
            else
            {
                guncelle.Parameters.AddWithValue("ex", 0);
            }
            guncelle.ExecuteNonQuery();

            MessageBox.Show("Güncelleme İşlemi Başarlı", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information );
        }
        private void temizle()
        {
            txtAD.Text= "";
            txtBölüm.Text = "";
            txtCinsiyet.Text = "";
            txtDurum.Text = "";
            txtID.Text = "";
            txtSikayet.Text = "";
            txtSoyad.Text = "";
            txtTarih.Text = "";
            txtTc.Text = "";
            txtTelefon.Text = "";
            txtYas.Text = "";
            rbHayır.Checked = true;
            lblEx.Text = "False";
        }

        private void btnFormuTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnİstatistik_Click(object sender, EventArgs e)
        {
            frmİstatistic fr = new frmİstatistic();
            fr.Show();
        }
    }
}