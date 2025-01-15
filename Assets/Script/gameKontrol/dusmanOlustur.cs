using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class dusmanOlustur : MonoBehaviour
{
    [Header("D��man Olu�turma")]
    public GameObject[] dusmanlar;
    public GameObject[] cikicakNoktalar;
    public GameObject[] hedefObjeler;
    public float dusmanOlusmaSuresi;

    [Header("Toplam D��man")]
    public int baslangicDusmanSayisi; // ��kacak d��man sayimiz
    public static int kalanDusmanSayisi; // d��manlar �ld�kten sonra, g�r�nt�lenecek de�er
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
                // dusmanlar dizisinde, 0 ile dizi uzunlu�u kadar random say� �ret
                int dusman = Random.Range(0, dusmanlar.Length);

                // d��man olu�ma noktalar� dizisinde, 0 ile dizi uzunlu�u kadar random say� �ret
                int cikicakNokta = Random.Range(0, cikicakNoktalar.Length);

                // hedefObjeler dizisinde, 0 ile dizi uzunlu�u kadar random say� �ret, random hedefleri olsun
                int hedef = Random.Range(0, hedefObjeler.Length);

                // dusmanlar dizisinde dusman de�i�kenine atanan random say� daki objeyi,
                // cikicakNoktalar dizisinden cikicaNokta de�i�kenine atanan say� daki objenin pozisyonunda olu�tur.
                GameObject dusmanim = Instantiate(dusmanlar[dusman], cikicakNoktalar[cikicakNokta].transform.position, Quaternion.identity);

                // olu�an objenin dusman scriptinde ki hedefBelirle fonksiyonuna hedefObjeler dizisinden random d�nen
                // hedef indisde ki objeyi g�nder.
                dusmanim.GetComponent<dusman>().hedefBelirle(hedefObjeler[hedef]);
                baslangicDusmanSayisi--;
            }
            
            
        }
        
    }

    // dusman scriptinde oldun() fonksiyonunda �a��rarak ilgili i�lemleri �al��t�r�yoruz
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
