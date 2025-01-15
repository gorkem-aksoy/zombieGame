using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yontemFarkliliklari : MonoBehaviour
{
    /*
     * --------------------------------- Yöntem 1 --------------------------------------------
     * --------------------------------- mermiKutusuOlustur scripti -------------------------------------
     

 public class mermiKutusuOlustur : MonoBehaviour
 {
     public List<GameObject> mermiKutusuNoktalari = new List<GameObject>();

     public GameObject mermiKutusuAnaObje;

     // static deðiþkenlerde örnekleme ye ihtiyaç yoktur. script ismi ve deðiþken ile eriþilebilir.
     public static bool mermiKutusuVarmi;

     void Start()
     {
         mermiKutusuVarmi = false;
         StartCoroutine(mermiKutusuYap());
         Debug.Log(mermiKutusuVarmi);
     }

     IEnumerator mermiKutusuYap()
     {
         while (true)
         {
             // her saniye kontrol et. Sayesinde bu Coroutine, her frame'de bir kez çalýþtýrýlýr ve iþlemi engellemez.
             yield return null; 
             if (!mermiKutusuVarmi)
             {
                 // bekleme süresi if in dýþýnda kalýrsa, kutuyu alýp almamasýna bakmaz, oluþturduktan sonra hep sayým devam eder.
                 // oluþtuktan 3 saniye sonra alýrsan, geriye 2 saniye kalýr ve aldýktan 2 saniye sonra kutu yeniden oluþur.
                 // böyle ise kutu alýnmazsa bu ife giremeyeceðinden dolayý, saniye iþlemez.
                 yield return new WaitForSeconds(5f);
                 int randomSayi = Random.Range(0, 9);
                 Vector3 kutuPozis = mermiKutusuNoktalari[randomSayi].transform.position;
                 Quaternion kutuRotas = mermiKutusuNoktalari[randomSayi].transform.rotation;

                 Instantiate(mermiKutusuAnaObje, kutuPozis, kutuRotas);
                 mermiKutusuVarmi = true;
                 Debug.Log(mermiKutusuVarmi);
             }
         }

         //-------------------------------------------------- temel kod --------------------------------------------------
         while (true) 
         {
             yield return new WaitForSeconds(5f);

             int randomSayi = Random.Range(0, 9);

             Instantiate(mermiKutusuAnaObje, mermiKutusuNoktalari[randomSayi].transform.position, mermiKutusuNoktalari[randomSayi].transform.rotation);

         }
}
}

    // ------------------------------------ ak47 scripti ---------------------------------------
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
               string gelenSilahTuru= hit.transform.gameObject.GetComponent<mermiKutusu>().olusanSilahTuru;
                int gelenMermiSayisi = hit.transform.gameObject.GetComponent<mermiKutusu>().olusanMermiSayisi;

                // mermi kaydet fonksiyonuna ilgili parametreleri gönderriyorum.
                mermiKaydet(gelenSilahTuru, gelenMermiSayisi);

                mermiKutusuOlustur.mermiKutusuVarmi = false;
                Debug.Log(mermiKutusuOlustur.mermiKutusuVarmi);

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
            mermiKaydet(gelenSilahTuru, gelenMermiSayisi);

            mermiKutusuOlustur.mermiKutusuVarmi = false;
            Debug.Log(mermiKutusuOlustur.mermiKutusuVarmi);
            Destroy(other.transform.parent.gameObject);
        }
    }


    // * --------------------------------- Yöntem 2 --------------------------------------------

    */
}
