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

    // karakter saðlýk kutusuna çarptýðýnda silah scriptlerinden eriþiyoruz.
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

    // saðlýðýmýzý azaltma iþlemleri, düþmandan gelen hasar parametresini karakterin saðlýðýndan azaltýyoruz.
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

    // canýmýz bittiðinde çalýþacak fonksiyon
    void gameOver()
    {
        gameOverPanel.SetActive(true);
        // oyunu duraklatýyoruz
        Time.timeScale = 0;
        // Debug.Log("oyunBitti");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.gameObject.CompareTag("saglikKutusu"))
        {
            Debug.Log("deðdi");
            // saglikKutusu tagýna sahip obje ile etkileþime girdiðinde hiyerarþide üstlerde bulunan ana objelerinde
            // karakterSaglik scriptin de canDoldur() fonksiyonunu çalýþtýr.
            // transform.gameObject.GetComponentInParent<karakterSaglik>().canDoldur();

            canDoldur();
            
            // tekrardan saglik kutusu oluþabilmesi için saglik kutusu varmý kontrolünü false yap.
            healthKutusuOlustur.saglikKutusuVarmi = false;

            // iþlemleri yaptýktan sonra objeyi yok et.
            Destroy(other.transform.gameObject);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
