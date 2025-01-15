using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mermiKutusu : MonoBehaviour
{
    // silah türü
    // mermi sayýsý

    string[] silahlar =
        {
            "magnum",
            "ak47",
            "pompali",
            "sniper"
        };

    int[] mermiSayisi =
    {
            20,
            30,
            40,
            50,
        };

    // oluþan random deðerleri bu deðiþkenlerde tutarak iþlem yapýcaz.
    public string olusanSilahTuru;
    public int olusanMermiSayisi;

    // eþitliðin sol tarafý liste türünde deðiþken tanýmlamasý, sað taraf ise liste oluþturma.
    // listeye atýlan resimler silahlar dizisinde ki sýra ile ayný olmalý, çünkü ayný indisler ile iþlem yapýlacak.
    public List<Sprite> silahResimleri = new List<Sprite>();

    // mermi kutusunun üstünde göstericeðimiz canvas elemanýný buraya tanýmlayacaðým.
    // canvas elemanýna, listemde ki ilgili sprite'ý atýcam.
    public Image silahResmi;

    // kutunun oluþtuðu noktayý tutucaz.
    public int kutununOlustuguNokta;

    void Start()
    {
        // silahlar dizisi ile silah resimleri listesinde ki indisleri eþleþtirmek için, oluþsan anahtarý alýyoruz.
        // Random.range 0 ile 10 ise 10u almaz. 0 ile 10 aralýðýnda 9 u alýr. 4 deðer varsa 0 1 2 3 indis numara çýktýsý olur.
        // bu nedenle -1 yazmaya gerek yok.
        int gelenAnahtar = Random.Range(0, silahlar.Length);
        olusanSilahTuru = silahlar[gelenAnahtar];
        olusanMermiSayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length)];

        // silahlar dizisi için random oluþturulan anahtarý, silah resimleri listesi içinde kullanýyoruz.
        // resimleri silahlar dizisi ile ayný sýrada oluþturduðumuz için
        // böylece mermi kutusunda random oluþan silah türüne göre, kutunun üstünde o türe ait resim çýkýcak.
        silahResmi.sprite = silahResimleri[gelenAnahtar];
        
        //olusanSilahTuru = "ak47";
        /*
        Debug.Log(olusanMermiSayisi);
        Debug.Log(olusanSilahTuru);
        */
    }

   
}
