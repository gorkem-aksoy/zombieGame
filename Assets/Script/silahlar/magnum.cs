using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class magnum : MonoBehaviour
{
    [Header("Ate� Sistemi")]
    public bool atesEdebilirmi; // oyuncunun baz� durumlarda ate� etmesini engellemek i�in kullan�caz.
    float iceridenAtesEtmeSikli�i;
    public float disaridanAtesEtmeSikli�i; // silah�n ka� saniyede bir ate� edece�i
    public float menzil;
    public Camera benimCam;
    public float silahHasari;

    [Header("Zoom Sistemi")]
    float camFieldPov; // yak�nla�ma i�in, default de�eri ald�k
    public float yakinlasmaPov; // zoom yap�ld���nda kameran�n yak�nla�ma de�eri
    public GameObject cross;
    bool zoomVarmi;

    [Header("Mermi Sistemi")]
    public string silahinAdi;
    int toplamMermiSayisi;
    public int sarjorKapasitesi;
    int kalanMermiSayisi;
    public TextMeshProUGUI kalanMermi_Text;
    public TextMeshProUGUI toplamMermi_Text;
    public mermiKutusuOlustur gameKontrol; // game Kontrol verilecek 
    public Transform mermiCikisNoktasi;
    public GameObject mermiObjesi;

    [Header("Sesler")]
    public AudioSource atesEtme;
    public AudioSource sarjorDoldurma;
    public AudioSource mermiBitis;
    public AudioSource mermiAlma;

    [Header("Efektler")]
    public ParticleSystem atesEfekti;
    public ParticleSystem kanEfekti;
    public ParticleSystem betonEfekti;
    public ParticleSystem tahtaEfekti;
    public ParticleSystem metalEfekti;

    [Header("Genel Sistem")]
    Animator benimAnimator;


    void Start()
    {
        // set edilen mermi say�s�n� ba�lang��da toplam mermimize at�yoruz.
        // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z� dinamikle�tiriyoruz.
        toplamMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_mermi");

        // kalan mermi say�s�n� ve toplam mermi say�s�n� durumuna g�re ba�lang�� mermi i�lemleri.
        baslangicMermiDoldurma();

        // �arjor sisteminde de�i�kenleri ilgili canvas elemanlar�na at�yoruz.
        sarjorDoldurmaTeknik("normalYaz");

        // animator compenentine eri�ip i�erisinde ki animasyonlar� kullan�caz.
        benimAnimator = GetComponent<Animator>();

        // kameran�n yak�nla�ma de�erini atad�k.
        // zoom yapmay� b�rakt���nda tekrar bu de�ere d�nd�r�lecek.
        camFieldPov = benimCam.fieldOfView;

        // ba�lang��ta zoomVarmi de�eri false olucak, mouse 1 e t�klad���nda true olucak
        zoomVarmi = false;
    }

    void Update()
    {
        /* time.time : 5
        iceridenAtesEtmeSikli�i : 0 
        atesEtmeSikli�i : 1

        ba�lang��ta time.time, iceridenAtesEtmeSikli�i ndan b�y�k oldu�u i�in �al���r.
        i�ine girdi�inde iceridenAtesEtmeSikli�i de�i�kenine 5 de�erini ve 1 de�erini ekliyoruz.
        iceridenAtesEtmeSikli�i de�i�keni 6 oldu, time.time de�eri 5 di, mouse0 a bas�l� tuttu�unu varsayal�m
        karakter ate� edemeyecek. Time.time de�erinin 6 dan b�y�k olmas�n� bekleyecek. yani ate� edebilmek 
        i�in 1 saniye beklemesi gerekecek. yani atesEtmeSikli�i de�i�keninde belirtti�imiz gibi.
       */
        // normal ate� edebilmesi i�in zoomVarmi false olmal�, e�er false de�ilse zaten true olan blok �al��acak
        if (!zoomVarmi && Input.GetKey(KeyCode.Mouse0) && !genelKontrol.oyunDurdumu)
        {

            // kalan mermi say�s� 0 de�ilse ate� edebilsin
            if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikli�i && kalanMermiSayisi != 0)
            {

                // yakinlasma yok false g�nder, normal atesEt animasyonu �al��s�n
                atesEt(false);
                iceridenAtesEtmeSikli�i = Time.time + disaridanAtesEtmeSikli�i;
            }
            // else yazd���m�zda ate� ederken bir aral�k verildi�i i�in, her o aral��a d��t���nde else k�sm� �al���yor ve
            // her ate� etme sesinin yan�nda mermi bitis sesi duyuluyor.
            // bu nedenle else if kullanmak mant�kl�
            else if (kalanMermiSayisi == 0)
            {
                //  ses oynat�lm�yorsa oynat demektir.
                if (!mermiBitis.isPlaying)
                {
                    mermiBitis.Play();
                }

            }

        }


        // zoomYapma
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // zoom yap�ld�ysa, ate� edebilmesi i�in de�i�keni true yap�yoruz.
            zoomVarmi = true;
            benimAnimator.SetBool("zoomYap", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            cross.SetActive(true);
            // b�rakt���nda normal ates edebilmesi i�in false yap�yoruz
            zoomVarmi = false;
            benimAnimator.SetBool("zoomYap", false);
            benimCam.fieldOfView = camFieldPov;
        }

        // mouse 1 e t�klad���nda zoomVarmi true gelicek, ondan sonra mouse0 a t�kland�ysa �al��
        if (zoomVarmi && Input.GetKey(KeyCode.Mouse0) && !genelKontrol.oyunDurdumu)
        {

            // zoom s�ras�nda ate� edebilmesi i�in kontrol yap�l�r
            if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikli�i && kalanMermiSayisi != 0)
            {

                // Zoom yap�l�rken yak�nla�t�rma olmad��� i�in atesEt fonksiyonuna true g�nderiliyor
                atesEt(true);
                iceridenAtesEtmeSikli�i = Time.time + disaridanAtesEtmeSikli�i;
            }
            else if (kalanMermiSayisi == 0)
            {
                // Mermi bitmi�se, ses oynat�lm�yorsa ses oynat
                if (!mermiBitis.isPlaying)
                {
                    mermiBitis.Play();
                }
            }
        }

        // reload i�lemleri
        else if (Input.GetKey(KeyCode.R))
        {
            // reload i�leminde animasyonun ve matematiksel i�lemin do�ru zamanlarda i�lem g�rmesi i�in kulland�k.
            StartCoroutine(reloadYapZamanlayici());
        }

        else if (Input.GetKey(KeyCode.E))
        {
            mermiAl();
        }
    }

    // �arjor de�i�tirme animasyonun da event olarak �a��r�l�yor.
    void sarjorDegisSesi()
    {
        sarjorDoldurma.Play();
    }

    // mouse 0 a t�kland���nda �a��r�lacak
    void atesEt(bool yakinlasmaVarmi)
    {
        // silah�n ucundan mermi ��karma
        Instantiate(mermiObjesi, mermiCikisNoktasi.position, mermiCikisNoktasi.rotation);

        atesEtme.Play();
        atesEfekti.Play();


        if (!yakinlasmaVarmi)
        {
            // yak�nla�ma yoksa oynat�lacak aniamsyon
            benimAnimator.Play("atesEt");
        }
        else
        {
            // yak�nla�ma varsa oynat�lacak animasyon
            benimAnimator.Play("zoomAtesEt");
        }

        // kalan mermi kontrol
        kalanMermiSayisi--;
        PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
        kalanMermi_Text.text = kalanMermiSayisi.ToString();
        // kalan mermi kontrol

        RaycastHit hit;

        // cameran�n pozisyonundan ileri do�ru, belirlenen menzilde ���n yolla
        // out hit ile �arpan objenin bilgilerini al
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("dusman"))
            {
                Instantiate(kanEfekti, hit.point, Quaternion.LookRotation(hit.normal));

                hit.transform.gameObject.GetComponent<dusman>().darbeAl(silahHasari);
            }

            // beton efektini ���n�n �arpt��� noktada ve Quaternion.LookRotation, yani objenin bize bakan y�n�nde olu�tur.
            // hit.normal ise rotation de�erini normalle�tiriyor. 0-1 aral���nda tutuyor.
            /*
             hit.normal: Raycast ile bir objeye �arpt���n�zda, �arpma noktas�ndaki y�zeyin normal vekt�r�n� d�nd�r�r. 
             Normal vekt�r, o y�zeye dik olan y�n� ifade eder. Yani, objenin y�zeyine tam dik bir vekt�r olarak 
             d���n�lebilir.
             �rne�in, d�z bir zemine vurduysan�z, hit.normal yukar�ya do�ru olan (0, 1, 0) bir vekt�rd�r.
             E�er bir duvara �arpt�ysan�z, hit.normal o duvara dik olan y�n� verecektir.
             */
            else if (hit.transform.gameObject.CompareTag("beton"))
            {
                Instantiate(betonEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }

            else if (hit.transform.gameObject.CompareTag("tahta"))
            {
                Instantiate(tahtaEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }

            else if (hit.transform.gameObject.CompareTag("metal"))
            {
                Instantiate(metalEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }

            else if (hit.transform.gameObject.CompareTag("devrilebilirObje"))
            {
                // compenent al�naca�� zaman, hit.transform olarak b�rak�ld���nda transform bile�enine eri�ir
                // ancak transform bile�eni bir obje de�ildir. rigidbody bile�enini bu �ekilde alamay�z.
                // bunun yerine hit.transform.gameObject olarak transform bile�enine sahip obje diyerek bile�en almal�y�z.
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                // belirli bir konuma kuvvet uygulama
                // rg.AddForce(new Vector3(4f, 3f, 2f) * 10f);

                // objeye ileri od�ru kuvvet uygulama
                // rg.AddForce(transform.forward * 500f);

                // objenin hiti ald��� yerin tam tersine kuvvet uygulama
                rg.AddForce(-hit.normal * 50f);

                // Instantiate(metalEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }

            // Debug.Log(hit.transform.name);
        }



    }

    // kalan merminin durumuna g�re yap�lmas� gereken reload matematiksel i�lemler
    void sarjorDoldurmaTeknik(string tur)
    {
        // kalan merminin durumunu al�yoruz. kalan mermi yok yada kalan mermi var.
        switch (tur)
        {
            //kalan mermi say�s� 0 de�ilse
            case "mermiVar":
                // toplam mermi say�s� sarjor kapasitesinden az ise son reload olucak demektir.
                if (toplamMermiSayisi <= sarjorKapasitesi)
                {
                    /*
                     * sarjor 10
                     * toplam mermi say�s� 8, �arjor kapasitesinden az 
                     * kalan mermi say�s� da 5 diyelim.
                     * bu durumda kalan mermi say�s� �arjor kapasitesini a�amaz.
                     * bu durumda kalan mermi say�m� kaybetmiyor olmam gerekmekte.
                    */

                    // kalan mermi say�s� ve toplam mermi say�s�n�n toplamlar�n� ald�k.    
                    int olusanToplamDeger = kalanMermiSayisi + toplamMermiSayisi;

                    // olu�an de�er �arjor kapasitesinden fazla m� kontrol edilmeli.
                    // olu�an de�er diyelimki;
                    // 13 oldu, �arjor kapasitesinden fazla.
                    // 3 mermi fazlam var.

                    if (olusanToplamDeger > sarjorKapasitesi)
                    {
                        // burada olu�an de�erden �arjor kapasitesini ��kar�rsam toplam mermi
                        // say�s�nda kalmas� gereken de�eri bulabilirim.
                        toplamMermiSayisi = olusanToplamDeger - sarjorKapasitesi;

                        // kalan mermi say�s�da �arjor kapasitesini a�t��� i�in direkt olarak
                        // �arjor kapasitesine e�itlenebilir.
                        kalanMermiSayisi = sarjorKapasitesi;
    
                    }
                    else
                    {
                        // e�erki kalan mermi say�s� sarjor kapasitesini a�m�yorsa direkt olarak
                        // toplam mermi say�s�n� ekleyebilirim
                        kalanMermiSayisi += toplamMermiSayisi;

                        // toplam mermi say�s�n�n hepsi kalan mermi say�s�na eklenece�i i�in 0 yap�yoruz.
                        toplamMermiSayisi = 0;
   
                    }

                }
                // toplam mermi say�s� sarjor kapasitesinden fazla ise, birden fazla kez reload yap�labilir demektir.
                // �arjor 10 , toplam mermi 20, kalan mermi 8
                else
                {
                    // at�lan mermi say�s�n�, �arjor kapasitesinden kalan mermi say�s�n� ��kartarak bulduk.
                    // �arjor 10
                    // kalan mermi 8
                    // at�lan mermi 2 oldu.
                    int at�lanMermi = sarjorKapasitesi - kalanMermiSayisi;

                    // toplam mermi say�s�ndan at�lan mermiyi ��kart�yoruz.
                    // yani kalan merminin sarjor kapasitesine e�it olmas� i�in gerekli mermi say�s�.
                    toplamMermiSayisi -= at�lanMermi;

                    // kalan mermiyi, �arjor kapasitesine e�itliyoruz
                    kalanMermiSayisi = sarjorKapasitesi;
    
                }

                mermiSetEt();
                toplamMermi_Text.text = toplamMermiSayisi.ToString();
                kalanMermi_Text.text = kalanMermiSayisi.ToString();
                break;

            // kalan mermi say�s� 0'sa
            case "mermiYok":

                // toplam mermi say�s� sarjor kapasitesinden az ise son reload olucak demektir.
                // �arjor 10, toplam mermi 5, kalan mermi 0
                if (toplamMermiSayisi <= sarjorKapasitesi)
                {
                    kalanMermiSayisi = toplamMermiSayisi;
                    toplamMermiSayisi = 0;

                }
                // toplam mermi say�s� sarjor kapasitesinden fazla ise, birden fazla kez reload yap�labilir demektir.
                // �arjor 10 , toplam mermi 20, kalan mermi 0
                else
                {
                    toplamMermiSayisi -= sarjorKapasitesi;
                    kalanMermiSayisi = sarjorKapasitesi;

                }

                mermiSetEt();
                toplamMermi_Text.text = toplamMermiSayisi.ToString();
                kalanMermi_Text.text = kalanMermiSayisi.ToString();
                break;

            case "normalYaz":
                toplamMermi_Text.text = toplamMermiSayisi.ToString();
                kalanMermi_Text.text = kalanMermiSayisi.ToString();
                break;

            default:
                Debug.Log("i�lem yap�lamad�");
                break;
        }
    }

    // matematiksel i�lem ile animasyonun do�ru zamanl� �al��abilmesi i�in coroutine kulland�k.
    IEnumerator reloadYapZamanlayici()
    {
        // kalan mermi say�s� �arjor kapasitesinden az ise ve toplam mermi say�s� 0'a e�it de�ilse reload'a izin ver
        if (kalanMermiSayisi < sarjorKapasitesi && toplamMermiSayisi != 0)
        {
            benimAnimator.Play("sarjorDegistir");
            atesEdebilirmi = false;
            
        }

        yield return new WaitForSeconds(2f);

        // kalan mermi say�s� �arjor kapasitesinden az ise ve toplam mermi say�s� 0'a e�it de�ilse reload'a izin ver
        if (kalanMermiSayisi < sarjorKapasitesi && toplamMermiSayisi != 0)
        {
            // kalan mermi say�s� 0 de�ilse, hala at�lacak mermin varsa, yap�lacak i�lemler
            if (kalanMermiSayisi != 0)
            {
                sarjorDoldurmaTeknik("mermiVar");
            }

            // kalan mermi say�s� 0'sa yap�lacak i�lemler
            else
            {
                sarjorDoldurmaTeknik("mermiYok");
            }

        }
        yield return new WaitForSeconds(.3f);
        atesEdebilirmi = true;


    }

    // e tu�una basarak mermi alma
    void mermiAl()
    {
        RaycastHit hit;

        // cameran�n pozisyonundan ileri do�ru, belirlenen menzilde ���n yolla
        // out hit ile �arpan objenin bilgilerini al
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("mermiKutusu"))
            {
                //mermiKutusu kutusunda random olarak olu�an de�erlerimizi �ektik.
                string gelenSilahTuru = hit.transform.gameObject.GetComponent<mermiKutusu>().olusanSilahTuru;
                int gelenMermiSayisi = hit.transform.gameObject.GetComponent<mermiKutusu>().olusanMermiSayisi;

                // mermi kaydet fonksiyonuna ilgili parametreleri g�nderriyorum.
                mermiEkle(gelenSilahTuru, gelenMermiSayisi);

                // karakter mermi ald���nda ilgili noktay� yeni kutu olu�abilmesi i�in kald�rmak �zere kutunun sahip oldu�u noktay� g�nderiyoruz.
                gameKontrol.noktalariKaldir(hit.transform.gameObject.GetComponent<mermiKutusu>().kutununOlustuguNokta);

                Destroy(hit.transform.parent.gameObject);
            }
        }
    }

    // fiziksel �arp��ma ile mermi alma
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("mermiKutusu"))
        {
            //mermiKutusu kutusunda random olarak olu�an de�erlerimizi �ektik.
            string gelenSilahTuru = other.transform.gameObject.GetComponent<mermiKutusu>().olusanSilahTuru;
            int gelenMermiSayisi = other.transform.gameObject.GetComponent<mermiKutusu>().olusanMermiSayisi;

            // mermi kaydet fonksiyonuna ilgili parametreleri g�nderiyorum.
            mermiEkle(gelenSilahTuru, gelenMermiSayisi);

            // karakter mermi ald���nda ilgili noktay� yeni kutu olu�abilmesi i�in kald�rmak �zere kutunun sahip oldu�u noktay� g�nderiyoruz.
            gameKontrol.noktalariKaldir(other.transform.gameObject.GetComponent<mermiKutusu>().kutununOlustuguNokta);

            Destroy(other.transform.parent.gameObject);
        }

        if (other.transform.gameObject.CompareTag("saglikKutusu"))
        {
            Debug.Log("de�di");
            // saglikKutusu tag�na sahip obje ile etkile�ime girdi�inde hiyerar�ide �stlerde bulunan ana objelerinde
            // karakterSaglik scriptin de canDoldur() fonksiyonunu �al��t�r.
            // transform.gameObject.GetComponentInParent<karakterSaglik>().canDoldur();

            gameKontrol.GetComponent<envanterKontrol>().canAl();

            // tekrardan saglik kutusu olu�abilmesi i�in saglik kutusu varm� kontrol�n� false yap.
            healthKutusuOlustur.saglikKutusuVarmi = false;

            // i�lemleri yapt�ktan sonra objeyi yok et.
            Destroy(other.transform.gameObject);


        }

        if (other.transform.gameObject.CompareTag("bombaKutusu"))
        {
            Debug.Log("de�di");
            // saglikKutusu tag�na sahip obje ile etkile�ime girdi�inde hiyerar�ide �stlerde bulunan ana objelerinde
            // karakterSaglik scriptin de canDoldur() fonksiyonunu �al��t�r.
            // transform.gameObject.GetComponentInParent<karakterSaglik>().canDoldur();

            //gameKontrol.GetComponent<korunacakObjeSaglik>().canDoldur();

            gameKontrol.GetComponent<envanterKontrol>().bombaAl();

            // tekrardan saglik kutusu olu�abilmesi i�in saglik kutusu varm� kontrol�n� false yap.
            bombaKutusuOlustur.bombaKutusuVarmi = false;

            // i�lemleri yapt�ktan sonra objeyi yok et.
            Destroy(other.transform.gameObject);
        }
    }

    // mermi kutusu ile etkile�ime girildi�inde mermiyi silah t�r�ne g�re alabilmek i�in
    // mermi alma fonksiyonlar�ndan �a��r�lacak
    void mermiEkle(string silahTuru, int mermiSayisi)
    {
        int gelenMevcutMermi;
        mermiAlma.Play();
        switch (silahTuru)
        {
            
            case "ak47":

                // diyelim ki ak 47 silah�n� yani bu script kullan�l�yor, ancak mermi kutusundan pompal�ya ait mermi al�nabilir.
                // bu nedenle set edilmi� olan pompali de�erine kutudan gelen mermi say�s�n� ekleyip yeniden set etmeliyiz.

                gelenMevcutMermi = PlayerPrefs.GetInt("ak47_mermi"); // pompali_mermi key'inden mermi say�s�n� al�yoruz, e�er key yoksa 0 d�ner
                gelenMevcutMermi += mermiSayisi; // Yeni mermiyi ekliyoruz
                PlayerPrefs.SetInt("ak47_mermi", gelenMevcutMermi); // G�ncellenen mermi say�s�n� kaydediyoruz

                break;
            case "pompali":

                // diyelim ki ak 47 silah�n� yani bu script kullan�l�yor, ancak mermi kutusundan magnuma ait mermi al�nabilir.
                // bu nedenle set edilmi� olan magnum de�erine kutudan gelen mermi say�s�n� ekleyip yeniden set etmeliyiz.

                gelenMevcutMermi = PlayerPrefs.GetInt("pompali_mermi"); // pompali_mermi key'inden mermi say�s�n� al�yoruz, e�er key yoksa 0 d�ner
                gelenMevcutMermi += mermiSayisi; // Yeni mermiyi ekliyoruz
                PlayerPrefs.SetInt("pompali_mermi", gelenMevcutMermi); // G�ncellenen mermi say�s�n� kaydediyoruz

                break;
            case "magnum":

                toplamMermiSayisi += mermiSayisi;
                // mermi kutusundan her mermi ald���m�zda, toplam mermimizi tekrardan set ediyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);
                sarjorDoldurmaTeknik("normalYaz");
                
                break;
            case "sniper":

                // diyelim ki ak 47 silah�n� yani bu script kullan�l�yor, ancak mermi kutusundan sniper'a ait mermi al�nabilir.
                // bu nedenle set edilmi� olan sniper'a de�erine kutudan gelen mermi say�s�n� ekleyip yeniden set etmeliyiz.

                gelenMevcutMermi = PlayerPrefs.GetInt("sniper_mermi"); // pompali_mermi key'inden mermi say�s�n� al�yoruz, e�er key yoksa 0 d�ner
                gelenMevcutMermi += mermiSayisi; // Yeni mermiyi ekliyoruz
                PlayerPrefs.SetInt("sniper_mermi", gelenMevcutMermi); // G�ncellenen mermi say�s�n� kaydediyoruz

                break;
        }
    }

    /*
     * senaryolar;
     * �arjor 20
     * kalan 5
     * toplam 19 veya 12
     * ------------
     * �arjor 20
     * kalan 0
     * toplam 19 
     * ------------
     * �arjor 20
     * kalan 5
     * toplam 30
     * ---------------
     * �arjor 20
     * kalan 0
     * toplam 30
     * -----------------
     *
     */

    void mermiSetEt()
    {
        // i�lem sonras�nda olu�an toplam mermi say�s�n� tekrardan anahtar�m�za set ediyoruz.
        // ayr�ca her scripte anahtar de�i�tirmekten kurtulmak i�in silahinAdi de�i�keni ile anahtar�m�z�
        // dinamikle�tiriyoruz.
        PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

        PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);


    }

    void baslangicMermiDoldurma()
    {
        kalanMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_kalanMermi");


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
                 */

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
                */
            }
            mermiSetEt();
        }

        // toplam mermi say�s� �arjor kapasitesinden fazla
        else
        {
            // kalan mermi say�s� 0'dan fazla
            if (kalanMermiSayisi != 0)
            {
                /*
                 * �arjor 20
                 * kalan 5
                 * toplam 30
                 */
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
                 */
                kalanMermiSayisi = sarjorKapasitesi;
                toplamMermiSayisi -= sarjorKapasitesi;

            }
            mermiSetEt();
        }


    }

    void kameraYakinlasScopeCikar()
    {
        cross.SetActive(false);
        benimCam.fieldOfView = yakinlasmaPov;
    }
}
