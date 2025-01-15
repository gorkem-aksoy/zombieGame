using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthKutusuOlustur : MonoBehaviour
{
    public List<GameObject> saglikKutusuNoktalari = new List<GameObject>();

    public GameObject saglikKutusuObje;

    public static bool saglikKutusuVarmi;

    public float kutuCikmaSuresi;

    // List<float> noktalar = new List<float>();

    void Start()
    {
        saglikKutusuVarmi = false;
        StartCoroutine(saglikKutusuYap());

    }

    IEnumerator saglikKutusuYap()
    {
        while (true)
        {
            yield return new WaitForSeconds(kutuCikmaSuresi);
            if (!saglikKutusuVarmi)
            {
                int randomSayi = Random.Range(0, 5);

                Vector3 kutuPozis = saglikKutusuNoktalari[randomSayi].transform.position;
                Quaternion kutuRotas = saglikKutusuNoktalari[randomSayi].transform.rotation;

                Instantiate(saglikKutusuObje, kutuPozis, kutuRotas);

                // random oluþan sayýyý, oluþan objenin ilgili deðiþkenine atadýk.
                // oluþan kutunun noktasý belli oldu.
                // saglik  kutu ana objesini oluþturduðum için, saglik kutusu scriptim alt objesinde olduðu için çocuk objesinin bileþenlerine eriþmeliyim
                // saglikKutumAna.transform.gameObject.GetComponentInChildren<saglikKutusu>().kutununOlustuguNokta = randomSayi;

                saglikKutusuVarmi = true;
            }

        }

    }

    // ak47 scriptinden saglik alma iþlemleri gerçekleþtiðinde kutuya atanan ilgili nokta numarasýný buraya göndericez.
    /*public void noktalariKaldir(int deger)
    {
        noktalar.Remove(deger);
    }
    */
}
