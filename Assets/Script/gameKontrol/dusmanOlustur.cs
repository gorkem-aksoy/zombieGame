using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class dusmanOlustur : MonoBehaviour
{
    [Header("Düþman Oluþturma")]
    public GameObject[] dusmanlar;
    public GameObject[] cikicakNoktalar;
    public GameObject[] hedefObjeler;
    public float dusmanOlusmaSuresi;

    [Header("Toplam Düþman")]
    public int baslangicDusmanSayisi; // çýkacak düþman sayimiz
    public static int kalanDusmanSayisi; // düþmanlar öldükten sonra, görüntülenecek deðer
    public TextMeshProUGUI kalanDusmanSayisi_Text;

    public GameObject kazandinPanel;

    void Start()
    {
        kalanDusmanSayisi = baslangicDusmanSayisi;
        kalanDusmanSayisi_Text.text = kalanDusmanSayisi.ToString();
        
        StartCoroutine(dusmanCikar());
    }

    IEnumerator dusmanCikar()
    {
       while (true)
        {
            yield return new WaitForSeconds(dusmanOlusmaSuresi);

            if(baslangicDusmanSayisi != 0)
            {
                // dusmanlar dizisinde, 0 ile dizi uzunluðu kadar random sayý üret
                int dusman = Random.Range(0, dusmanlar.Length);

                // düþman oluþma noktalarý dizisinde, 0 ile dizi uzunluðu kadar random sayý üret
                int cikicakNokta = Random.Range(0, cikicakNoktalar.Length);

                // hedefObjeler dizisinde, 0 ile dizi uzunluðu kadar random sayý üret, random hedefleri olsun
                int hedef = Random.Range(0, hedefObjeler.Length);

                // dusmanlar dizisinde dusman deðiþkenine atanan random sayý daki objeyi,
                // cikicakNoktalar dizisinden cikicaNokta deðiþkenine atanan sayý daki objenin pozisyonunda oluþtur.
                GameObject dusmanim = Instantiate(dusmanlar[dusman], cikicakNoktalar[cikicakNokta].transform.position, Quaternion.identity);

                // oluþan objenin dusman scriptinde ki hedefBelirle fonksiyonuna hedefObjeler dizisinden random dönen
                // hedef indisde ki objeyi gönder.
                dusmanim.GetComponent<dusman>().hedefBelirle(hedefObjeler[hedef]);
                baslangicDusmanSayisi--;
            }
            
            
        }
        
    }

    // dusman scriptinde oldun() fonksiyonunda çaðýrarak ilgili iþlemleri çalýþtýrýyoruz
    public void dusmanSayisiGuncelle()
    {
        kalanDusmanSayisi--;

        if(kalanDusmanSayisi <= 0)
        {
            kalanDusmanSayisi_Text.text = kalanDusmanSayisi.ToString();
            kazandin();
        }
        else
        {
            kalanDusmanSayisi_Text.text = kalanDusmanSayisi.ToString();
        }
        
    }

    void kazandin()
    {
        kazandinPanel.SetActive(true);
        Time.timeScale = 0;

        genelKontrol.oyunDurdumu = true;

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

        GameObject.FindWithTag("karakter").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
    }
    
    
}
