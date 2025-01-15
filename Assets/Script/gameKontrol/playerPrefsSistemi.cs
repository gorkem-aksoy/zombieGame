using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPrefsSistemi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

        // e�er bu anahtar olu�turulmam�� ise de�erleri set et, e�er olu�turulmu� ise de�erler zaten anahtarlara tan�mlanm�� demektir.
        if (!PlayerPrefs.HasKey("yeniOyunBaslangici"))
        {
            // toplam mermi
            PlayerPrefs.SetInt("ak47_mermi", 900);
            PlayerPrefs.SetInt("pompali_mermi", 200);
            PlayerPrefs.SetInt("magnum_mermi", 450);
            PlayerPrefs.SetInt("sniper_mermi", 400);

            // kalan mermi
            PlayerPrefs.SetInt("ak47_kalanMermi", 30);
            PlayerPrefs.SetInt("pompali_kalanMermi", 2);
            PlayerPrefs.SetInt("magnum_kalanMermi", 9);
            PlayerPrefs.SetInt("sniper_kalanMermi", 10);

            // bomba ve sa�l�k say�s�
            PlayerPrefs.SetInt("bomba_sayisi", 5);
            PlayerPrefs.SetInt("saglik_sayisi", 1);
            // if'in i�inde girdi�inde bu anahtar� set ediyoruz ki, tekrardan bu� if'e girmesin
            PlayerPrefs.SetInt("yeniOyunBaslangici", 1);
            PlayerPrefs.Save();  // De�erleri kaydediyoruz.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
