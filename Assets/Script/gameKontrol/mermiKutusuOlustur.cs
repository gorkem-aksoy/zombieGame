using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermiKutusuOlustur : MonoBehaviour
{
    public List<GameObject> mermiKutusuNoktalari = new List<GameObject>();

    public GameObject mermiKutusuAnaObje;

    public float kutuCikmaSuresi;


    List<float> noktalar = new List<float>();

    void Start()
    {

        StartCoroutine(mermiKutusuYap());

    }

    IEnumerator mermiKutusuYap()
    {
        while (true)
        {
            yield return new WaitForSeconds(kutuCikmaSuresi);
            int randomSayi = Random.Range(0, 10);

            // noktalar listesinde olu�an random say� yoksa, random say�y� listeye ekle
            if(!noktalar.Contains(randomSayi))
            {
                noktalar.Add(randomSayi);
            }
            // e�er ki random say� listede varsa tekrar bir say� olu�tur.
            else
            {
                // random.range de 0 ile 10 aras�nda say� �retir. 0 dahil 10 dahil de�ildir. 
                // indis numaras� max 9 ise 10 girmemiz laz�m
                randomSayi = Random.Range(0, 10);
                // bu kod ile random say� tekrar belirlendikten sonra, ba�a d�nmesi ve listeye random say�y� eklemek
                continue; 
            }

            Vector3 kutuPozis = mermiKutusuNoktalari[randomSayi].transform.position;
            Quaternion kutuRotas = mermiKutusuNoktalari[randomSayi].transform.rotation;

           GameObject mermiKutumAna = Instantiate(mermiKutusuAnaObje, kutuPozis, kutuRotas);

            // random olu�an say�y�, olu�an objenin ilgili de�i�kenine atad�k.
            // olu�an kutunun noktas� belli oldu.
            // mermi  kutu ana objesini olu�turdu�um i�in, mermi kutusu scriptim alt objesinde oldu�u i�in �ocuk objesinin bile�enlerine eri�meliyim
            mermiKutumAna.transform.gameObject.GetComponentInChildren<mermiKutusu>().kutununOlustuguNokta = randomSayi;

        }

    }

    // ak47 scriptinden mermi alma i�lemleri ger�ekle�ti�inde kutuya atanan ilgili nokta numaras�n� buraya g�ndericez.
    public void noktalariKaldir(int deger)
    {
        noktalar.Remove(deger);
    }
}
