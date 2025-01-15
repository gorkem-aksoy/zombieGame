using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class karakterSaglik : MonoBehaviour
{
    public float saglik;
    public Image saglikBar;
    public GameObject gameOverPanel;   
    void Start()
    {
        saglikBar.fillAmount = 1;
    }

    // karakter sa�l�k kutusuna �arpt���nda silah scriptlerinden eri�iyoruz.
    public void canDoldur()
    {
        float olusanSaglik = saglik + 30;
        Debug.Log("artanCan : "+saglik);
        if(olusanSaglik > 100)
        {
            olusanSaglik = 100;
        }
        saglikBar.fillAmount = olusanSaglik / 100;
    }

    // sa�l���m�z� azaltma i�lemleri, d��mandan gelen hasar parametresini karakterin sa�l���ndan azalt�yoruz.
    public void canAzalt(float alinanHasar)
    {
        saglik -= alinanHasar;
        Debug.Log("azalanCan : " + saglik);

        saglikBar.fillAmount = saglik / 100;

        if(saglik < 0)
        {
            gameOver();
        }
    }

    // can�m�z bitti�inde �al��acak fonksiyon
    void gameOver()
    {
        gameOverPanel.SetActive(true);
        // oyunu duraklat�yoruz
        Time.timeScale = 0;
        // Debug.Log("oyunBitti");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.gameObject.CompareTag("saglikKutusu"))
        {
            Debug.Log("de�di");
            // saglikKutusu tag�na sahip obje ile etkile�ime girdi�inde hiyerar�ide �stlerde bulunan ana objelerinde
            // karakterSaglik scriptin de canDoldur() fonksiyonunu �al��t�r.
            // transform.gameObject.GetComponentInParent<karakterSaglik>().canDoldur();

            canDoldur();
            
            // tekrardan saglik kutusu olu�abilmesi i�in saglik kutusu varm� kontrol�n� false yap.
            healthKutusuOlustur.saglikKutusuVarmi = false;

            // i�lemleri yapt�ktan sonra objeyi yok et.
            Destroy(other.transform.gameObject);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
