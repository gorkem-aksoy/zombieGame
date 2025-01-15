using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaKutusuOlustur : MonoBehaviour
{
    public List<GameObject> bombaKutusuNoktalari = new List<GameObject>();

    public GameObject bombaKutusuObje;

    public static bool bombaKutusuVarmi;

    public float kutuCikmaSuresi;


    // List<float> noktalar = new List<float>();

    void Start()
    {
        bombaKutusuVarmi = false;
        StartCoroutine(bombaKutusuYap());

    }

    IEnumerator bombaKutusuYap()
    {
        while (true)
        {
            yield return new WaitForSeconds(kutuCikmaSuresi);
            if (!bombaKutusuVarmi)
            {
                int randomSayi = Random.Range(0, 5);

                Vector3 kutuPozis = bombaKutusuNoktalari[randomSayi].transform.position;
                Quaternion kutuRotas = bombaKutusuNoktalari[randomSayi].transform.rotation;

                Instantiate(bombaKutusuObje, kutuPozis, kutuRotas);

                // random oluþan sayýyý, oluþan objenin ilgili deðiþkenine atadýk.
                // oluþan kutunun noktasý belli oldu.
                // bomba  kutu ana objesini oluþturduðum için, bomba kutusu scriptim alt objesinde olduðu için çocuk objesinin bileþenlerine eriþmeliyim
                // bombaKutumAna.transform.gameObject.GetComponentInChildren<bombaKutusu>().kutununOlustuguNokta = randomSayi;

                bombaKutusuVarmi = true;
            }

        }

    }

    // ak47 scriptinden bomba alma iþlemleri gerçekleþtiðinde kutuya atanan ilgili nokta numarasýný buraya göndericez.
    /*public void noktalariKaldir(int deger)
    {
        noktalar.Remove(deger);
    }
    */
}
