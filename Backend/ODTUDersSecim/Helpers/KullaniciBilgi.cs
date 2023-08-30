public class KullaniciBilgi
{
    public Guid KullaniciKod { get; set; }

    public string KullaniciAd { get; set; }

    public string? AccessToken { get; set; }

    public string? UserName { get; set; }

    public KullaniciBilgi()
    {
        this.KullaniciKod = Guid.NewGuid();
        this.KullaniciAd = "";
        this.AccessToken = "";
        this.UserName = "";
    }

    public KullaniciBilgi(Guid kullaniciKod, string kullaniciAd, string? accessToken, string? userName)
    {
        this.KullaniciKod = kullaniciKod;
        this.KullaniciAd = kullaniciAd;
        this.AccessToken = accessToken;
        this.UserName = userName;
    }

}