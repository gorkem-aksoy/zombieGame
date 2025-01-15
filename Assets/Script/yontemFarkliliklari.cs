using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yontemFarkliliklari : MonoBehaviour
{
    /*
     * --------------------------------- Y�ntem 1 --------------------------------------------
     * --------------------------------- mermiKutusuOlustur scripti -------------------------------------
     

 public class mermiKutusuOlustur : MonoBehaviour
 {
     public List<GameObject> mermiKutusuNoktalari = new List<GameObject>();

     public GameObject mermiKutusuAnaObje;

     // static de�i�kenlerde �rnekleme ye ihtiya� yoktur. script ismi ve de�i�ken ile eri�ilebilir.
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
             // her saniye kontrol et. Sayesinde bu Coroutine, her frame'de bir kez �al��t�r�l�r ve i�lemi engellemez.
             yield return null; 
             if (!mermiKutusuVarmi)
             {
                 // bekleme s�resi if in d���nda kal�rsa, kutuyu al�p almamas�na bakmaz, olu�turduktan sonra hep say�m devam eder.
                 // olu�tuktan 3 saniye sonra al�rsan, geriye 2 saniye kal�r ve ald�ktan 2 saniye sonra kutu yeniden olu�ur.
                 // b�yle ise kutu al�nmazsa bu ife giremeyece�inden dolay�, saniye i�lemez.
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

        // cameran�n pozisyonundan ileri do�ru, belirlenen menzilde ���n yolla
        // out hit ile �arpan objenin bilgilerini al
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("mermiKutusu"))
            {
                //mermiKutusu kutusunda random olarak olu�an de�erlerimizi �ektik.
               string gelenSilahTuru= hit.transform.gameObject.GetComponent<mermiKutusu>().olusanSilahTuru;
                int gelenMermiSayisi = hit.transform.gameObject.GetComponent<mermiKutusu>().olusanMermiSayisi;

                // mermi kaydet fonksiyonuna ilgili parametreleri g�nderriyorum.
                mermiKaydet(gelenSilahTuru, gelenMermiSayisi);

                mermiKutusuOlustur.mermiKutusuVarmi = false;
                Debug.Log(mermiKutusuOlustur.mermiKutusuVarmi);

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
            mermiKaydet(gelenSilahTuru, gelenMermiSayisi);

            mermiKutusuOlustur.mermiKutusuVarmi = false;
            Debug.Log(mermiKutusuOlustur.mermiKutusuVarmi);
            Destroy(other.transform.parent.gameObject);
        }
    }


    // * --------------------------------- Y�ntem 2 --------------------------------------------

    */
}
