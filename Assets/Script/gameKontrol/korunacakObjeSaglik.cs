using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class korunacakObjeSaglik : MonoBehaviour
{
    public float saglik;
    public Image saglikBar;
    public GameObject gameOverPanel;
    envanterKontrol envanterKontrol;

    void Start()
    {
        saglikBar.fillAmount = 1;
        envanterKontrol = GetComponent<envanterKontrol>();  
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            canDoldur();
        }
    }

    void canDoldur()
    {
        // set edilen de�er 0 de�ilse i�lem yap
        if (PlayerPrefs.GetInt("saglik_sayisi") != 0)
        {
            saglik = 100;
            saglikBar.fillAmount = saglik / 100;

            // sa�l�k kullan�l�rsa daha �ncesinde set edilen de�eri 1 azalt ve yeniden set et
            PlayerPrefs.SetInt("saglik_sayisi", PlayerPrefs.GetInt("saglik_sayisi") - 1);
            envanterKontrol.ilacSayisi.text = PlayerPrefs.GetInt("saglik_sayisi").ToString();
        }
        else
        {
            // bildirim yada ses olabilir
        }
    }

    // sa�l���m�z� azaltma i�lemleri, d��mandan gelen hasar parametresini karakterin sa�l���ndan azalt�yoruz.
    public void canAzalt(float alinanHasar)
    {
        saglik -= alinanHasar;
        Debug.Log("azalanCan : " + saglik);

        saglikBar.fillAmount = saglik / 100;

        if (saglik <= 0)
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

        genelKontrol.oyunDurdumu = true;

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

        GameObject.FindWithTag("karakter").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
    }

}
