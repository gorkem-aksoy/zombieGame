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

        // silah de�i�imleri envanter panelinde g�sterebilmek i�in scripti dahil ettik.
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
        // q ile silah de�i�tirme ile numara ile silah de�i�tirmenin beraber kullan�m�.
        aktifSira = siraNumarasi;
        silahlar[siraNumarasi].SetActive(true);
        if (!silahDegistirmeSesi.isPlaying)
        {
            silahDegistirmeSesi.Play();
        }

        // silah dizisini ve silah resimleri listesinde ayn� s�rada olu�turuldu�u i�in hangi silaha ge�ilirse o indisi
        // ilgili fonksiyona parametre olarak g�nderiyoruz.
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
            // e�er aktif s�ra 3 gelirse aktif siray� 0 la taramal�ya geri d�nd�r.
            aktifSira = 0;
            silahlar[aktifSira].SetActive(true);
        }
        else
        {
            // aktif s�ra ba�lang��ta 0, taramal� aktif, o y�zden else blo�una girdi.
            // q ya bast���nda else blo�unda 1 oldu. pompal� aktif
            // q ya bast� aktif sira 2 oldu sniper aktif
            // q ya bast� aktif sira 3 oldu magnum aktif.
            aktifSira++;
            silahlar[aktifSira].SetActive(true);
        }
        // silah dizisini ve silah resimleri listesinde ayn� s�rada olu�turuldu�u i�in hangi silaha ge�ilirse o indisi
        // ilgili fonksiyona parametre olarak g�nderiyoruz.
        envanterKontrol.silahResmiBelirle(aktifSira);

        

    }
}
