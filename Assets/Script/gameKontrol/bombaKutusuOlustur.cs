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

                // random olu�an say�y�, olu�an objenin ilgili de�i�kenine atad�k.
                // olu�an kutunun noktas� belli oldu.
                // bomba  kutu ana objesini olu�turdu�um i�in, bomba kutusu scriptim alt objesinde oldu�u i�in �ocuk objesinin bile�enlerine eri�meliyim
                // bombaKutumAna.transform.gameObject.GetComponentInChildren<bombaKutusu>().kutununOlustuguNokta = randomSayi;

                bombaKutusuVarmi = true;
            }

        }

    }

    // ak47 scriptinden bomba alma i�lemleri ger�ekle�ti�inde kutuya atanan ilgili nokta numaras�n� buraya g�ndericez.
    /*public void noktalariKaldir(int deger)
    {
        noktalar.Remove(deger);
    }
    */
}
