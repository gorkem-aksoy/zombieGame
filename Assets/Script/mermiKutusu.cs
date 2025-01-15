using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mermiKutusu : MonoBehaviour
{
    // silah t�r�
    // mermi say�s�

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

    // olu�an random de�erleri bu de�i�kenlerde tutarak i�lem yap�caz.
    public string olusanSilahTuru;
    public int olusanMermiSayisi;

    // e�itli�in sol taraf� liste t�r�nde de�i�ken tan�mlamas�, sa� taraf ise liste olu�turma.
    // listeye at�lan resimler silahlar dizisinde ki s�ra ile ayn� olmal�, ��nk� ayn� indisler ile i�lem yap�lacak.
    public List<Sprite> silahResimleri = new List<Sprite>();

    // mermi kutusunun �st�nde g�sterice�imiz canvas eleman�n� buraya tan�mlayaca��m.
    // canvas eleman�na, listemde ki ilgili sprite'� at�cam.
    public Image silahResmi;

    // kutunun olu�tu�u noktay� tutucaz.
    public int kutununOlustuguNokta;

    void Start()
    {
        // silahlar dizisi ile silah resimleri listesinde ki indisleri e�le�tirmek i�in, olu�san anahtar� al�yoruz.
        // Random.range 0 ile 10 ise 10u almaz. 0 ile 10 aral���nda 9 u al�r. 4 de�er varsa 0 1 2 3 indis numara ��kt�s� olur.
        // bu nedenle -1 yazmaya gerek yok.
        int gelenAnahtar = Random.Range(0, silahlar.Length);
        olusanSilahTuru = silahlar[gelenAnahtar];
        olusanMermiSayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length)];

        // silahlar dizisi i�in random olu�turulan anahtar�, silah resimleri listesi i�inde kullan�yoruz.
        // resimleri silahlar dizisi ile ayn� s�rada olu�turdu�umuz i�in
        // b�ylece mermi kutusunda random olu�an silah t�r�ne g�re, kutunun �st�nde o t�re ait resim ��k�cak.
        silahResmi.sprite = silahResimleri[gelenAnahtar];
        
        //olusanSilahTuru = "ak47";
        /*
        Debug.Log(olusanMermiSayisi);
        Debug.Log(olusanSilahTuru);
        */
    }

   
}
