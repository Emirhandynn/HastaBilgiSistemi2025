using System.Data.SqlClient;

namespace hastaTAkipSistemi
{
    internal class frmSqlbaglanti
    {
        string adres = @"Data Source=DESKTOP-H6DMK6F;Initial Catalog=db_HastaneYonetim;Integrated Security=True;Encrypt=False;";

        public SqlConnection baglan()
        {
            SqlConnection baglanti = new SqlConnection(adres);
            baglanti.Open();
            return baglanti;
        }
    }
}
