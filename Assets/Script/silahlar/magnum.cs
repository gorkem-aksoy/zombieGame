using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class magnum : MonoBehaviour
{
    [Header("Ateþ Sistemi")]
    public bool atesEdebilirmi; // oyuncunun bazý durumlarda ateþ etmesini engellemek için kullanýcaz.
    float iceridenAtesEtmeSikliði;
    public float disaridanAtesEtmeSikliði; // silahýn kaç saniyede bir ateþ edeceði
    public float menzil;
    public Camera benimCam;
    public float silahHasari;

    [Header("Zoom Sistemi")]
    float camFieldPov; // yakýnlaþma için, default deðeri aldýk
    public float yakinlasmaPov; // zoom yapýldýðýnda kameranýn yakýnlaþma deðeri
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
        // set edilen mermi sayýsýný baþlangýçda toplam mermimize atýyoruz.
        // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý dinamikleþtiriyoruz.
        toplamMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_mermi");

        // kalan mermi sayýsýný ve toplam mermi sayýsýný durumuna göre baþlangýç mermi iþlemleri.
        baslangicMermiDoldurma();

        // þarjor sisteminde deðiþkenleri ilgili canvas elemanlarýna atýyoruz.
        sarjorDoldurmaTeknik("normalYaz");

        // animator compenentine eriþip içerisinde ki animasyonlarý kullanýcaz.
        benimAnimator = GetComponent<Animator>();

        // kameranýn yakýnlaþma deðerini atadýk.
        // zoom yapmayý býraktýðýnda tekrar bu deðere döndürülecek.
        camFieldPov = benimCam.fieldOfView;

        // baþlangýçta zoomVarmi deðeri false olucak, mouse 1 e týkladýðýnda true olucak
        zoomVarmi = false;
    }

    void Update()
    {
        /* time.time : 5
        iceridenAtesEtmeSikliði : 0 
        atesEtmeSikliði : 1

        baþlangýçta time.time, iceridenAtesEtmeSikliði ndan büyük olduðu için çalýþýr.
        içine girdiðinde iceridenAtesEtmeSikliði deðiþkenine 5 deðerini ve 1 deðerini ekliyoruz.
        iceridenAtesEtmeSikliði deðiþkeni 6 oldu, time.time deðeri 5 di, mouse0 a basýlý tuttuðunu varsayalým
        karakter ateþ edemeyecek. Time.time deðerinin 6 dan büyük olmasýný bekleyecek. yani ateþ edebilmek 
        için 1 saniye beklemesi gerekecek. yani atesEtmeSikliði deðiþkeninde belirttiðimiz gibi.
       */
        // normal ateþ edebilmesi için zoomVarmi false olmalý, eðer false deðilse zaten true olan blok çalýþacak
        if (!zoomVarmi && Input.GetKey(KeyCode.Mouse0) && !genelKontrol.oyunDurdumu)
        {

            // kalan mermi sayýsý 0 deðilse ateþ edebilsin
            if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikliði && kalanMermiSayisi != 0)
            {

                // yakinlasma yok false gönder, normal atesEt animasyonu çalýþsýn
                atesEt(false);
                iceridenAtesEtmeSikliði = Time.time + disaridanAtesEtmeSikliði;
            }
            // else yazdýðýmýzda ateþ ederken bir aralýk verildiði için, her o aralýða düþtüðünde else kýsmý çalýþýyor ve
            // her ateþ etme sesinin yanýnda mermi bitis sesi duyuluyor.
            // bu nedenle else if kullanmak mantýklý
            else if (kalanMermiSayisi == 0)
            {
                //  ses oynatýlmýyorsa oynat demektir.
                if (!mermiBitis.isPlaying)
                {
                    mermiBitis.Play();
                }

            }

        }


        // zoomYapma
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // zoom yapýldýysa, ateþ edebilmesi için deðiþkeni true yapýyoruz.
            zoomVarmi = true;
            benimAnimator.SetBool("zoomYap", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            cross.SetActive(true);
            // býraktýðýnda normal ates edebilmesi için false yapýyoruz
            zoomVarmi = false;
            benimAnimator.SetBool("zoomYap", false);
            benimCam.fieldOfView = camFieldPov;
        }

        // mouse 1 e týkladýðýnda zoomVarmi true gelicek, ondan sonra mouse0 a týklandýysa çalýþ
        if (zoomVarmi && Input.GetKey(KeyCode.Mouse0) && !genelKontrol.oyunDurdumu)
        {

            // zoom sýrasýnda ateþ edebilmesi için kontrol yapýlýr
            if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikliði && kalanMermiSayisi != 0)
            {

                // Zoom yapýlýrken yakýnlaþtýrma olmadýðý için atesEt fonksiyonuna true gönderiliyor
                atesEt(true);
                iceridenAtesEtmeSikliði = Time.time + disaridanAtesEtmeSikliði;
            }
            else if (kalanMermiSayisi == 0)
            {
                // Mermi bitmiþse, ses oynatýlmýyorsa ses oynat
                if (!mermiBitis.isPlaying)
                {
                    mermiBitis.Play();
                }
            }
        }

        // reload iþlemleri
        else if (Input.GetKey(KeyCode.R))
        {
            // reload iþleminde animasyonun ve matematiksel iþlemin doðru zamanlarda iþlem görmesi için kullandýk.
            StartCoroutine(reloadYapZamanlayici());
        }

        else if (Input.GetKey(KeyCode.E))
        {
            mermiAl();
        }
    }

    // þarjor deðiþtirme animasyonun da event olarak çaðýrýlýyor.
    void sarjorDegisSesi()
    {
        sarjorDoldurma.Play();
    }

    // mouse 0 a týklandýðýnda çaðýrýlacak
    void atesEt(bool yakinlasmaVarmi)
    {
        // silahýn ucundan mermi çýkarma
        Instantiate(mermiObjesi, mermiCikisNoktasi.position, mermiCikisNoktasi.rotation);

        atesEtme.Play();
        atesEfekti.Play();


        if (!yakinlasmaVarmi)
        {
            // yakýnlaþma yoksa oynatýlacak aniamsyon
            benimAnimator.Play("atesEt");
        }
        else
        {
            // yakýnlaþma varsa oynatýlacak animasyon
            benimAnimator.Play("zoomAtesEt");
        }

        // kalan mermi kontrol
        kalanMermiSayisi--;
        PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);
        kalanMermi_Text.text = kalanMermiSayisi.ToString();
        // kalan mermi kontrol

        RaycastHit hit;

        // cameranýn pozisyonundan ileri doðru, belirlenen menzilde ýþýn yolla
        // out hit ile çarpan objenin bilgilerini al
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("dusman"))
            {
                Instantiate(kanEfekti, hit.point, Quaternion.LookRotation(hit.normal));

                hit.transform.gameObject.GetComponent<dusman>().darbeAl(silahHasari);
            }

            // beton efektini ýþýnýn çarptýðý noktada ve Quaternion.LookRotation, yani objenin bize bakan yönünde oluþtur.
            // hit.normal ise rotation deðerini normalleþtiriyor. 0-1 aralýðýnda tutuyor.
            /*
             hit.normal: Raycast ile bir objeye çarptýðýnýzda, çarpma noktasýndaki yüzeyin normal vektörünü döndürür. 
             Normal vektör, o yüzeye dik olan yönü ifade eder. Yani, objenin yüzeyine tam dik bir vektör olarak 
             düþünülebilir.
             Örneðin, düz bir zemine vurduysanýz, hit.normal yukarýya doðru olan (0, 1, 0) bir vektördür.
             Eðer bir duvara çarptýysanýz, hit.normal o duvara dik olan yönü verecektir.
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
                // compenent alýnacaðý zaman, hit.transform olarak býrakýldýðýnda transform bileþenine eriþir
                // ancak transform bileþeni bir obje deðildir. rigidbody bileþenini bu þekilde alamayýz.
                // bunun yerine hit.transform.gameObject olarak transform bileþenine sahip obje diyerek bileþen almalýyýz.
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                // belirli bir konuma kuvvet uygulama
                // rg.AddForce(new Vector3(4f, 3f, 2f) * 10f);

                // objeye ileri odðru kuvvet uygulama
                // rg.AddForce(transform.forward * 500f);

                // objenin hiti aldýðý yerin tam tersine kuvvet uygulama
                rg.AddForce(-hit.normal * 50f);

                // Instantiate(metalEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }

            // Debug.Log(hit.transform.name);
        }



    }

    // kalan merminin durumuna göre yapýlmasý gereken reload matematiksel iþlemler
    void sarjorDoldurmaTeknik(string tur)
    {
        // kalan merminin durumunu alýyoruz. kalan mermi yok yada kalan mermi var.
        switch (tur)
        {
            //kalan mermi sayýsý 0 deðilse
            case "mermiVar":
                // toplam mermi sayýsý sarjor kapasitesinden az ise son reload olucak demektir.
                if (toplamMermiSayisi <= sarjorKapasitesi)
                {
                    /*
                     * sarjor 10
                     * toplam mermi sayýsý 8, þarjor kapasitesinden az 
                     * kalan mermi sayýsý da 5 diyelim.
                     * bu durumda kalan mermi sayýsý þarjor kapasitesini aþamaz.
                     * bu durumda kalan mermi sayýmý kaybetmiyor olmam gerekmekte.
                    */

                    // kalan mermi sayýsý ve toplam mermi sayýsýnýn toplamlarýný aldýk.    
                    int olusanToplamDeger = kalanMermiSayisi + toplamMermiSayisi;

                    // oluþan deðer þarjor kapasitesinden fazla mý kontrol edilmeli.
                    // oluþan deðer diyelimki;
                    // 13 oldu, þarjor kapasitesinden fazla.
                    // 3 mermi fazlam var.

                    if (olusanToplamDeger > sarjorKapasitesi)
                    {
                        // burada oluþan deðerden þarjor kapasitesini çýkarýrsam toplam mermi
                        // sayýsýnda kalmasý gereken deðeri bulabilirim.
                        toplamMermiSayisi = olusanToplamDeger - sarjorKapasitesi;

                        // kalan mermi sayýsýda þarjor kapasitesini aþtýðý için direkt olarak
                        // þarjor kapasitesine eþitlenebilir.
                        kalanMermiSayisi = sarjorKapasitesi;
    
                    }
                    else
                    {
                        // eðerki kalan mermi sayýsý sarjor kapasitesini aþmýyorsa direkt olarak
                        // toplam mermi sayýsýný ekleyebilirim
                        kalanMermiSayisi += toplamMermiSayisi;

                        // toplam mermi sayýsýnýn hepsi kalan mermi sayýsýna ekleneceði için 0 yapýyoruz.
                        toplamMermiSayisi = 0;
   
                    }

                }
                // toplam mermi sayýsý sarjor kapasitesinden fazla ise, birden fazla kez reload yapýlabilir demektir.
                // þarjor 10 , toplam mermi 20, kalan mermi 8
                else
                {
                    // atýlan mermi sayýsýný, þarjor kapasitesinden kalan mermi sayýsýný çýkartarak bulduk.
                    // þarjor 10
                    // kalan mermi 8
                    // atýlan mermi 2 oldu.
                    int atýlanMermi = sarjorKapasitesi - kalanMermiSayisi;

                    // toplam mermi sayýsýndan atýlan mermiyi çýkartýyoruz.
                    // yani kalan merminin sarjor kapasitesine eþit olmasý için gerekli mermi sayýsý.
                    toplamMermiSayisi -= atýlanMermi;

                    // kalan mermiyi, þarjor kapasitesine eþitliyoruz
                    kalanMermiSayisi = sarjorKapasitesi;
    
                }

                mermiSetEt();
                toplamMermi_Text.text = toplamMermiSayisi.ToString();
                kalanMermi_Text.text = kalanMermiSayisi.ToString();
                break;

            // kalan mermi sayýsý 0'sa
            case "mermiYok":

                // toplam mermi sayýsý sarjor kapasitesinden az ise son reload olucak demektir.
                // þarjor 10, toplam mermi 5, kalan mermi 0
                if (toplamMermiSayisi <= sarjorKapasitesi)
                {
                    kalanMermiSayisi = toplamMermiSayisi;
                    toplamMermiSayisi = 0;

                }
                // toplam mermi sayýsý sarjor kapasitesinden fazla ise, birden fazla kez reload yapýlabilir demektir.
                // þarjor 10 , toplam mermi 20, kalan mermi 0
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
                Debug.Log("iþlem yapýlamadý");
                break;
        }
    }

    // matematiksel iþlem ile animasyonun doðru zamanlý çalýþabilmesi için coroutine kullandýk.
    IEnumerator reloadYapZamanlayici()
    {
        // kalan mermi sayýsý þarjor kapasitesinden az ise ve toplam mermi sayýsý 0'a eþit deðilse reload'a izin ver
        if (kalanMermiSayisi < sarjorKapasitesi && toplamMermiSayisi != 0)
        {
            benimAnimator.Play("sarjorDegistir");
            atesEdebilirmi = false;
            
        }

        yield return new WaitForSeconds(2f);

        // kalan mermi sayýsý þarjor kapasitesinden az ise ve toplam mermi sayýsý 0'a eþit deðilse reload'a izin ver
        if (kalanMermiSayisi < sarjorKapasitesi && toplamMermiSayisi != 0)
        {
            // kalan mermi sayýsý 0 deðilse, hala atýlacak mermin varsa, yapýlacak iþlemler
            if (kalanMermiSayisi != 0)
            {
                sarjorDoldurmaTeknik("mermiVar");
            }

            // kalan mermi sayýsý 0'sa yapýlacak iþlemler
            else
            {
                sarjorDoldurmaTeknik("mermiYok");
            }

        }
        yield return new WaitForSeconds(.3f);
        atesEdebilirmi = true;


    }

    // e tuþuna basarak mermi alma
    void mermiAl()
    {
        RaycastHit hit;

        // cameranýn pozisyonundan ileri doðru, belirlenen menzilde ýþýn yolla
        // out hit ile çarpan objenin bilgilerini al
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("mermiKutusu"))
            {
                //mermiKutusu kutusunda random olarak oluþan deðerlerimizi çektik.
                string gelenSilahTuru = hit.transform.gameObject.GetComponent<mermiKutusu>().olusanSilahTuru;
                int gelenMermiSayisi = hit.transform.gameObject.GetComponent<mermiKutusu>().olusanMermiSayisi;

                // mermi kaydet fonksiyonuna ilgili parametreleri gönderriyorum.
                mermiEkle(gelenSilahTuru, gelenMermiSayisi);

                // karakter mermi aldýðýnda ilgili noktayý yeni kutu oluþabilmesi için kaldýrmak üzere kutunun sahip olduðu noktayý gönderiyoruz.
                gameKontrol.noktalariKaldir(hit.transform.gameObject.GetComponent<mermiKutusu>().kutununOlustuguNokta);

                Destroy(hit.transform.parent.gameObject);
            }
        }
    }

    // fiziksel çarpýþma ile mermi alma
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("mermiKutusu"))
        {
            //mermiKutusu kutusunda random olarak oluþan deðerlerimizi çektik.
            string gelenSilahTuru = other.transform.gameObject.GetComponent<mermiKutusu>().olusanSilahTuru;
            int gelenMermiSayisi = other.transform.gameObject.GetComponent<mermiKutusu>().olusanMermiSayisi;

            // mermi kaydet fonksiyonuna ilgili parametreleri gönderiyorum.
            mermiEkle(gelenSilahTuru, gelenMermiSayisi);

            // karakter mermi aldýðýnda ilgili noktayý yeni kutu oluþabilmesi için kaldýrmak üzere kutunun sahip olduðu noktayý gönderiyoruz.
            gameKontrol.noktalariKaldir(other.transform.gameObject.GetComponent<mermiKutusu>().kutununOlustuguNokta);

            Destroy(other.transform.parent.gameObject);
        }

        if (other.transform.gameObject.CompareTag("saglikKutusu"))
        {
            Debug.Log("deðdi");
            // saglikKutusu tagýna sahip obje ile etkileþime girdiðinde hiyerarþide üstlerde bulunan ana objelerinde
            // karakterSaglik scriptin de canDoldur() fonksiyonunu çalýþtýr.
            // transform.gameObject.GetComponentInParent<karakterSaglik>().canDoldur();

            gameKontrol.GetComponent<envanterKontrol>().canAl();

            // tekrardan saglik kutusu oluþabilmesi için saglik kutusu varmý kontrolünü false yap.
            healthKutusuOlustur.saglikKutusuVarmi = false;

            // iþlemleri yaptýktan sonra objeyi yok et.
            Destroy(other.transform.gameObject);


        }

        if (other.transform.gameObject.CompareTag("bombaKutusu"))
        {
            Debug.Log("deðdi");
            // saglikKutusu tagýna sahip obje ile etkileþime girdiðinde hiyerarþide üstlerde bulunan ana objelerinde
            // karakterSaglik scriptin de canDoldur() fonksiyonunu çalýþtýr.
            // transform.gameObject.GetComponentInParent<karakterSaglik>().canDoldur();

            //gameKontrol.GetComponent<korunacakObjeSaglik>().canDoldur();

            gameKontrol.GetComponent<envanterKontrol>().bombaAl();

            // tekrardan saglik kutusu oluþabilmesi için saglik kutusu varmý kontrolünü false yap.
            bombaKutusuOlustur.bombaKutusuVarmi = false;

            // iþlemleri yaptýktan sonra objeyi yok et.
            Destroy(other.transform.gameObject);
        }
    }

    // mermi kutusu ile etkileþime girildiðinde mermiyi silah türüne göre alabilmek için
    // mermi alma fonksiyonlarýndan çaðýrýlacak
    void mermiEkle(string silahTuru, int mermiSayisi)
    {
        int gelenMevcutMermi;
        mermiAlma.Play();
        switch (silahTuru)
        {
            
            case "ak47":

                // diyelim ki ak 47 silahýný yani bu script kullanýlýyor, ancak mermi kutusundan pompalýya ait mermi alýnabilir.
                // bu nedenle set edilmiþ olan pompali deðerine kutudan gelen mermi sayýsýný ekleyip yeniden set etmeliyiz.

                gelenMevcutMermi = PlayerPrefs.GetInt("ak47_mermi"); // pompali_mermi key'inden mermi sayýsýný alýyoruz, eðer key yoksa 0 döner
                gelenMevcutMermi += mermiSayisi; // Yeni mermiyi ekliyoruz
                PlayerPrefs.SetInt("ak47_mermi", gelenMevcutMermi); // Güncellenen mermi sayýsýný kaydediyoruz

                break;
            case "pompali":

                // diyelim ki ak 47 silahýný yani bu script kullanýlýyor, ancak mermi kutusundan magnuma ait mermi alýnabilir.
                // bu nedenle set edilmiþ olan magnum deðerine kutudan gelen mermi sayýsýný ekleyip yeniden set etmeliyiz.

                gelenMevcutMermi = PlayerPrefs.GetInt("pompali_mermi"); // pompali_mermi key'inden mermi sayýsýný alýyoruz, eðer key yoksa 0 döner
                gelenMevcutMermi += mermiSayisi; // Yeni mermiyi ekliyoruz
                PlayerPrefs.SetInt("pompali_mermi", gelenMevcutMermi); // Güncellenen mermi sayýsýný kaydediyoruz

                break;
            case "magnum":

                toplamMermiSayisi += mermiSayisi;
                // mermi kutusundan her mermi aldýðýmýzda, toplam mermimizi tekrardan set ediyoruz.
                PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);
                sarjorDoldurmaTeknik("normalYaz");
                
                break;
            case "sniper":

                // diyelim ki ak 47 silahýný yani bu script kullanýlýyor, ancak mermi kutusundan sniper'a ait mermi alýnabilir.
                // bu nedenle set edilmiþ olan sniper'a deðerine kutudan gelen mermi sayýsýný ekleyip yeniden set etmeliyiz.

                gelenMevcutMermi = PlayerPrefs.GetInt("sniper_mermi"); // pompali_mermi key'inden mermi sayýsýný alýyoruz, eðer key yoksa 0 döner
                gelenMevcutMermi += mermiSayisi; // Yeni mermiyi ekliyoruz
                PlayerPrefs.SetInt("sniper_mermi", gelenMevcutMermi); // Güncellenen mermi sayýsýný kaydediyoruz

                break;
        }
    }

    /*
     * senaryolar;
     * þarjor 20
     * kalan 5
     * toplam 19 veya 12
     * ------------
     * þarjor 20
     * kalan 0
     * toplam 19 
     * ------------
     * þarjor 20
     * kalan 5
     * toplam 30
     * ---------------
     * þarjor 20
     * kalan 0
     * toplam 30
     * -----------------
     *
     */

    void mermiSetEt()
    {
        // iþlem sonrasýnda oluþan toplam mermi sayýsýný tekrardan anahtarýmýza set ediyoruz.
        // ayrýca her scripte anahtar deðiþtirmekten kurtulmak için silahinAdi deðiþkeni ile anahtarýmýzý
        // dinamikleþtiriyoruz.
        PlayerPrefs.SetInt(silahinAdi + "_mermi", toplamMermiSayisi);

        PlayerPrefs.SetInt(silahinAdi + "_kalanMermi", kalanMermiSayisi);


    }

    void baslangicMermiDoldurma()
    {
        kalanMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "_kalanMermi");


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
                 */

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
                */
            }
            mermiSetEt();
        }

        // toplam mermi sayýsý þarjor kapasitesinden fazla
        else
        {
            // kalan mermi sayýsý 0'dan fazla
            if (kalanMermiSayisi != 0)
            {
                /*
                 * þarjor 20
                 * kalan 5
                 * toplam 30
                 */
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
