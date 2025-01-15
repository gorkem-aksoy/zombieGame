using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silahDegistir : MonoBehaviour
{
    public GameObject[] silahlar;
    public AudioSource silahDegistirmeSesi;
    envanterKontrol envanterKontrol;
    

    int aktifSira;
    void Start()
    {
        silahlar[0].SetActive(true);

        aktifSira = 0;

        // silah deðiþimleri envanter panelinde gösterebilmek için scripti dahil ettik.
        envanterKontrol = GetComponent<envanterKontrol>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !genelKontrol.oyunDurdumu)
        {
            silahDegistirme(0);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !genelKontrol.oyunDurdumu)
        {
            silahDegistirme(1);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !genelKontrol.oyunDurdumu)
        {
            silahDegistirme(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !genelKontrol.oyunDurdumu)
        {
            silahDegistirme(3);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !genelKontrol.oyunDurdumu)
        {
            qVersiyonsilahDegistirme();
        }
    }

    void silahDegistirme(int siraNumarasi)
    {
        for (int i = 0; i < silahlar.Length; i++)
        {
            silahlar[i].gameObject.SetActive(false);
        }
        // q ile silah deðiþtirme ile numara ile silah deðiþtirmenin beraber kullanýmý.
        aktifSira = siraNumarasi;
        silahlar[siraNumarasi].SetActive(true);
        if (!silahDegistirmeSesi.isPlaying)
        {
            silahDegistirmeSesi.Play();
        }

        // silah dizisini ve silah resimleri listesinde ayný sýrada oluþturulduðu için hangi silaha geçilirse o indisi
        // ilgili fonksiyona parametre olarak gönderiyoruz.
        envanterKontrol.silahResmiBelirle(siraNumarasi);

        
    }

    void qVersiyonsilahDegistirme()
    {
        if (!silahDegistirmeSesi.isPlaying)
        {
            silahDegistirmeSesi.Play();
        }

        for (int i = 0; i < silahlar.Length; i++)
        {
            silahlar[i].gameObject.SetActive(false);
        }

        if (aktifSira == 3)
        {
            // eðer aktif sýra 3 gelirse aktif sirayý 0 la taramalýya geri döndür.
            aktifSira = 0;
            silahlar[aktifSira].SetActive(true);
        }
        else
        {
            // aktif sýra baþlangýçta 0, taramalý aktif, o yüzden else bloðuna girdi.
            // q ya bastýðýnda else bloðunda 1 oldu. pompalý aktif
            // q ya bastý aktif sira 2 oldu sniper aktif
            // q ya bastý aktif sira 3 oldu magnum aktif.
            aktifSira++;
            silahlar[aktifSira].SetActive(true);
        }
        // silah dizisini ve silah resimleri listesinde ayný sýrada oluþturulduðu için hangi silaha geçilirse o indisi
        // ilgili fonksiyona parametre olarak gönderiyoruz.
        envanterKontrol.silahResmiBelirle(aktifSira);

        

    }
}
