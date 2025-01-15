using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degistirilenKodlar : MonoBehaviour
{
    /* baslangicMermiDoldurma() fonksiyonu olcay hoca 
     *
     * burada ki sorun hiç mermi harcamamýza raðmen oyuna gir çýk yaptýðýmýzda toplam mermi 
     * sayýmýzdan kalan mermi sayýmýza mermi aktarýlmasý.
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

    /* sorunu çözmek için ilk denemem çalýþýyor fakat çok fazla iç içe if else var optimiz deðil sanýrým.
   
    // atesEt() fonksiyonunda her kalan mermi azaldýðýnda tekrardan set ediyorum ki azaldýðýný anlýyým.
    PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
    
    // player prefs de ise baþlangýç kalan mermi sayýmýzý veriyoruz
    
    void baskangicMermiDoldurma()
    {
        kalanMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_kalanMermi");
        if (toplamMermiSayisi <= sarjorKapasitesi)
        {

            if (kalanMermiSayisi > 0)
            {
                /*
                 * þarjor 20 30
                 * kalan 5 25
                 * toplam 19 da olabilir 12 de olabilir 28
                 
                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                int yeniToplam = kalanMermiSayisi + toplamMermiSayisi;

                if (yeniToplam > sarjorKapasitesi)
                {
                    // kalan 20 oldu toplam mermi 4 oldu
                    kalanMermiSayisi += yeniKalan;
                    toplamMermiSayisi -= yeniKalan;
                    // iþlem sonrasýnda oluþan toplam mermi sayýsýný tekrardan anahtarýmýza set ediyoruz.
                    // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý
                    // dinamikleþtiriyoruz.
                    PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                    PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
                }
                else
                {
                    kalanMermiSayisi += toplamMermiSayisi;
                    toplamMermiSayisi = 0;
                    // iþlem sonrasýnda oluþan toplam mermi sayýsýný tekrardan anahtarýmýza set ediyoruz.
                    // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý
                    // dinamikleþtiriyoruz.
                    PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                    PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);

                }
            }
            else
            {
                /*
                 * þarjor 20
                 * kalan 0
                 * toplam 19
                 
                kalanMermiSayisi = toplamMermiSayisi;
                toplamMermiSayisi = 0;

                // iþlem sonrasýnda oluþan toplam mermi sayýsýný tekrardan anahtarýmýza set ediyoruz.
                // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý
                // dinamikleþtiriyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);

            }
        }

        // baþlangýçta toplam mermi sayým þarjor kapasitesinden fazla
        else
        {
            // kalan mermi sayýsý 0'dan fazla
            if (kalanMermiSayisi > 0)
            {
                /*
                 * þarjor 20
                 * kalan 5
                 * toplam 30
                 
                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                kalanMermiSayisi += yeniKalan;
                toplamMermiSayisi -= yeniKalan;

                // iþlem sonrasýnda oluþan toplam mermi sayýsýný tekrardan anahtarýmýza set ediyoruz.
                // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý
                // dinamikleþtiriyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
            }
            // kalan mermi sayým 0
            else
            {
                /*
                 * þarjor 20
                 * kalan 0
                 * toplam 30
                 
                kalanMermiSayisi = sarjorKapasitesi;
                toplamMermiSayisi -= sarjorKapasitesi;

                // iþlem sonrasýnda oluþan toplam mermi sayýsýný tekrardan anahtarýmýza set ediyoruz.
                // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý
                // dinamikleþtiriyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

                PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);

            }

        }


    }

*/

    //--------------------------------------------------------------------------------------------

    /*ikinci denemem de mermi set etme kodlarý çok fazla tekrarlýyordu, bu kodlarý fonksiyona baðladým ve gereksiz blokdan kurtulduk


     void baslangicMermiDoldurma()
    {
        kalanMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_kalanMermi");

        // mermi set etme kodlarý çok fazla kullanýlýyor, bu yüzden kod kalabalýðýný azaltmak adýna fonksiyona baðladýk
        void mermiSetEt()
        {
            // iþlem sonrasýnda oluþan toplam mermi sayýsýný tekrardan anahtarýmýza set ediyoruz.
            // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý
            // dinamikleþtiriyoruz.
            PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

            PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
        }


        if (toplamMermiSayisi <= sarjorKapasitesi)
        {
            // toplam mermi þarjor kapasitesinden az
            // ayný zaman da kalan mermi 0' a eþit ve 0'da büyükse, bu iki durum aþaðýdaki kodlar ile saðlanýyor
            if (kalanMermiSayisi >= 0)
            {
                /*
                 * þarjor 20 30
                 * kalan 5 
                 * toplam 19 da olabilir 12 de olabilir 
                 

                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                int yeniToplam = kalanMermiSayisi + toplamMermiSayisi;

                // kalan mermi ile toplam mermi þarjor kapasitesini aþýyorsa
                if (yeniToplam > sarjorKapasitesi)
                {

                    kalanMermiSayisi += yeniKalan;
                    toplamMermiSayisi -= yeniKalan;
                }

                // kalan mermi 0 olduðunda ve kalan mermi 0 olmadýðýnda yine blok çalýþacak

                // kalan mermi ile toplam mermi þarjor kapasitesini aþmýyorsa
                else
                {
                    kalanMermiSayisi += toplamMermiSayisi;
                    toplamMermiSayisi = 0;
                }

                // burada ki bloklar toplam mermi sayýsý þarjor kapasitesinden az ise çalýþýyor
                // kalan mermi sayýsý 0 ise zaten þarjor kapasitesini aþamaz
                // bu nedenle kalan mermi sayýsý 0 dan büyük, sonra else açýp 0 olduðu durumu ayrý almaya gerek yok

                // yani bu alttaki durumu üstteki else karþýlýyor, bunun için ekstra bir else açmaya  gerek yok
                /*
                *þarjor 20
                * kalan 0
                * toplam 19
                
            }
            mermiSetEt();
        }

        // toplam mermi sayýsý þarjor kapasitesinden fazla
        else
        {
            // kalan mermi sayýsý 0'dan fazla
            if (kalanMermiSayisi > 0)
            {
                /*
                 * þarjor 20
                 * kalan 5
                 * toplam 30
                 
                int yeniKalan = sarjorKapasitesi - kalanMermiSayisi;
                kalanMermiSayisi += yeniKalan;
                toplamMermiSayisi -= yeniKalan;
            }
            // kalan mermi sayým 0
            else
            {
                /*
                 * þarjor 20
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
