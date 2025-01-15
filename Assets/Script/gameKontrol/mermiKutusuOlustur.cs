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

            // noktalar listesinde oluþan random sayý yoksa, random sayýyý listeye ekle
            if(!noktalar.Contains(randomSayi))
            {
                noktalar.Add(randomSayi);
            }
            // eðer ki random sayý listede varsa tekrar bir sayý oluþtur.
            else
            {
                // random.range de 0 ile 10 arasýnda sayý üretir. 0 dahil 10 dahil deðildir. 
                // indis numarasý max 9 ise 10 girmemiz lazým
                randomSayi = Random.Range(0, 10);
                // bu kod ile random sayý tekrar belirlendikten sonra, baþa dönmesi ve listeye random sayýyý eklemek
                continue; 
            }

            Vector3 kutuPozis = mermiKutusuNoktalari[randomSayi].transform.position;
            Quaternion kutuRotas = mermiKutusuNoktalari[randomSayi].transform.rotation;

           GameObject mermiKutumAna = Instantiate(mermiKutusuAnaObje, kutuPozis, kutuRotas);

            // random oluþan sayýyý, oluþan objenin ilgili deðiþkenine atadýk.
            // oluþan kutunun noktasý belli oldu.
            // mermi  kutu ana objesini oluþturduðum için, mermi kutusu scriptim alt objesinde olduðu için çocuk objesinin bileþenlerine eriþmeliyim
            mermiKutumAna.transform.gameObject.GetComponentInChildren<mermiKutusu>().kutununOlustuguNokta = randomSayi;

        }

    }

    // ak47 scriptinden mermi alma iþlemleri gerçekleþtiðinde kutuya atanan ilgili nokta numarasýný buraya göndericez.
    public void noktalariKaldir(int deger)
    {
        noktalar.Remove(deger);
    }
}
