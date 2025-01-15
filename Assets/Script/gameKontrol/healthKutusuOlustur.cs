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

                // random olu�an say�y�, olu�an objenin ilgili de�i�kenine atad�k.
                // olu�an kutunun noktas� belli oldu.
                // saglik  kutu ana objesini olu�turdu�um i�in, saglik kutusu scriptim alt objesinde oldu�u i�in �ocuk objesinin bile�enlerine eri�meliyim
                // saglikKutumAna.transform.gameObject.GetComponentInChildren<saglikKutusu>().kutununOlustuguNokta = randomSayi;

                saglikKutusuVarmi = true;
            }

        }

    }

    // ak47 scriptinden saglik alma i�lemleri ger�ekle�ti�inde kutuya atanan ilgili nokta numaras�n� buraya g�ndericez.
    /*public void noktalariKaldir(int deger)
    {
        noktalar.Remove(deger);
    }
    */
}
