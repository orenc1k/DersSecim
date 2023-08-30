using ODTUDersSecim.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Helpers
{
    public class KayitBilgi
    {
        public Guid KayitKullaniciKod { get; set; }

        public DateTime KayitTarih { get; set; }

        [StringUzunluk(100)]
        [Column(TypeName = "VARCHAR(100)")]
        public string? KayitKullaniciAd { get; set; }

        public Guid? GuncellemeKullaniciKod { get; set; }

        public DateTime? GuncellemeTarih { get; set; }

        [StringUzunluk(100)]
        [Column(TypeName = "VARCHAR(100)")]
        public string? GuncellemeKullaniciAd { get; set; }

        public Guid IslemKod { get; set; }
    }
}
