using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degistirilenKodlar : MonoBehaviour
{
    /* baslangicMermiDoldurma() fonksiyonu olcay hoca 
     *
     * burada ki sorun hi� mermi harcamam�za ra�men oyuna gir ��k yapt���m�zda toplam mermi 
     * say�m�zdan kalan mermi say�m�za mermi aktar�lmas�.
     * 
    void baskangicMermiDoldurma()
    {
        if (toplammermisayisi <= sarjorkapasite)

        {

            kalanmermi = toplammermisayisi;

            toplammermisayisi = 0;

            PlayerPrefs.SetInt(Silahin_Adi + "_Mermi", 0);

        }

        else

        {

            kalanmermi = sarjorkapasite;

            toplammermisayisi -= sarjorkapasite;

            PlayerPrefs.SetInt(Silahin_Adi + "_Mermi", toplammermisayisi);



        }
    }
    */

    //--------------------------------------------------------------------------------------------

    /* sorunu ��zmek i�in ilk denemem �al���yor fakat �ok fazla i� i�e if else var optimiz de�il san�r�m.
   
    // atesEt() fonksiyonunda her kalan mermi azald���nda tekrardan set ediyorum ki azald���n� anl�y�m.
    PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
    
    // player prefs de ise ba�lang�� kalan mermi say�m�z� veriyoruz
    
    void baskangicMermiDoldurma()
    {
        kalanMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_kalanMermi");
        if (toplamMermiSayisi <= sarjorKapasitesi)
        {

            if (kalanMermiSayisi > 0)
            {
                /*
                 * �arjor 20 30
                 * kalan 5 25
                 * toplam 19 da olabilir 12 de olabilir 28
                 
                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                int yeniToplam = kalanMermiSayisi + toplamMermiSayisi;

                if (yeniToplam > sarjorKapasitesi)
                {
                    // kalan 20 oldu toplam mermi 4 oldu
                    kalanMermiSayisi += yeniKalan;
                    toplamMermiSayisi -= yeniKalan;
                    // i�lem sonras�nda olu�an toplam mermi say�s�n� tekrardan anahtar�m�za set ediyoruz.
                    // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z�
                    // dinamikle�tiriyoruz.
                    PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                    PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
                }
                else
                {
                    kalanMermiSayisi += toplamMermiSayisi;
                    toplamMermiSayisi = 0;
                    // i�lem sonras�nda olu�an toplam mermi say�s�n� tekrardan anahtar�m�za set ediyoruz.
                    // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z�
                    // dinamikle�tiriyoruz.
                    PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                    PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);

                }
            }
            else
            {
                /*
                 * �arjor 20
                 * kalan 0
                 * toplam 19
                 
                kalanMermiSayisi = toplamMermiSayisi;
                toplamMermiSayisi = 0;

                // i�lem sonras�nda olu�an toplam mermi say�s�n� tekrardan anahtar�m�za set ediyoruz.
                // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z�
                // dinamikle�tiriyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);

            }
        }

        // ba�lang��ta toplam mermi say�m �arjor kapasitesinden fazla
        else
        {
            // kalan mermi say�s� 0'dan fazla
            if (kalanMermiSayisi > 0)
            {
                /*
                 * �arjor 20
                 * kalan 5
                 * toplam 30
                 
                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                kalanMermiSayisi += yeniKalan;
                toplamMermiSayisi -= yeniKalan;

                // i�lem sonras�nda olu�an toplam mermi say�s�n� tekrardan anahtar�m�za set ediyoruz.
                // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z�
                // dinamikle�tiriyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
            }
            // kalan mermi say�m 0
            else
            {
                /*
                 * �arjor 20
                 * kalan 0
                 * toplam 30
                 
                kalanMermiSayisi = sarjorKapasitesi;
                toplamMermiSayisi -= sarjorKapasitesi;

                // i�lem sonras�nda olu�an toplam mermi say�s�n� tekrardan anahtar�m�za set ediyoruz.
                // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z�
                // dinamikle�tiriyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);

            }

        }


    }

*/

    //--------------------------------------------------------------------------------------------

    /*ikinci denemem de mermi set etme kodlar� �ok fazla tekrarl�yordu, bu kodlar� fonksiyona ba�lad�m ve gereksiz blokdan kurtulduk


     void baslangicMermiDoldurma()
    {
        kalanMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_kalanMermi");

        // mermi set etme kodlar� �ok fazla kullan�l�yor, bu y�zden kod kalabal���n� azaltmak ad�na fonksiyona ba�lad�k
        void mermiSetEt()
        {
            // i�lem sonras�nda olu�an toplam mermi say�s�n� tekrardan anahtar�m�za set ediyoruz.
            // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z�
            // dinamikle�tiriyoruz.
            PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

            PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
        }


        if (toplamMermiSayisi <= sarjorKapasitesi)
        {
            // toplam mermi �arjor kapasitesinden az
            // ayn� zaman da kalan mermi 0' a e�it ve 0'da b�y�kse, bu iki durum a�a��daki kodlar ile sa�lan�yor
            if (kalanMermiSayisi >= 0)
            {
                /*
                 * �arjor 20 30
                 * kalan 5 
                 * toplam 19 da olabilir 12 de olabilir 
                 

                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                int yeniToplam = kalanMermiSayisi + toplamMermiSayisi;

                // kalan mermi ile toplam mermi �arjor kapasitesini a��yorsa
                if (yeniToplam > sarjorKapasitesi)
                {

                    kalanMermiSayisi += yeniKalan;
                    toplamMermiSayisi -= yeniKalan;
                }

                // kalan mermi 0 oldu�unda ve kalan mermi 0 olmad���nda yine blok �al��acak

                // kalan mermi ile toplam mermi �arjor kapasitesini a�m�yorsa
                else
                {
                    kalanMermiSayisi += toplamMermiSayisi;
                    toplamMermiSayisi = 0;
                }

                // burada ki bloklar toplam mermi say�s� �arjor kapasitesinden az ise �al���yor
                // kalan mermi say�s� 0 ise zaten �arjor kapasitesini a�amaz
                // bu nedenle kalan mermi say�s� 0 dan b�y�k, sonra else a��p 0 oldu�u durumu ayr� almaya gerek yok

                // yani bu alttaki durumu �stteki else kar��l�yor, bunun i�in ekstra bir else a�maya  gerek yok
                /*
                *�arjor 20
                * kalan 0
                * toplam 19
                
            }
            mermiSetEt();
        }

        // toplam mermi say�s� �arjor kapasitesinden fazla
        else
        {
            // kalan mermi say�s� 0'dan fazla
            if (kalanMermiSayisi > 0)
            {
                /*
                 * �arjor 20
                 * kalan 5
                 * toplam 30
                 
                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                kalanMermiSayisi += yeniKalan;
                toplamMermiSayisi -= yeniKalan;
            }
            // kalan mermi say�m 0
            else
            {
                /*
                 * �arjor 20
                 * kalan 0
                 * toplam 30
                 
                kalanMermiSayisi = sarjorKapasitesi;
                toplamMermiSayisi -= sarjorKapasitesi;

            }
            mermiSetEt();
        }


    }

    */


}
