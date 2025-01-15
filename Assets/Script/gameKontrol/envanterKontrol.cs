using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class envanterKontrol : MonoBehaviour
{
    public Image mevcutSilahResmi;
    public List<Sprite> silahResimleri = new List<Sprite>();
    public TextMeshProUGUI ilacSayisi;
    public TextMeshProUGUI bombaSayisi;

    void Start()
    {
        ilacSayisi.text = PlayerPrefs.GetInt("saglik_sayisi").ToString();
        bombaSayisi.text = PlayerPrefs.GetInt("bomba_sayisi").ToString();
    }

    // silah degi�tir scriptinden parametre gelicek
    public void silahResmiBelirle(int deger)
    {
        mevcutSilahResmi.sprite = silahResimleri[deger];
    }

    // silah scriptlerinden eri�ilecek
    public void canAl()
    {
        PlayerPrefs.SetInt("saglik_sayisi", PlayerPrefs.GetInt("saglik_sayisi") + 1);
        ilacSayisi.text = PlayerPrefs.GetInt("saglik_sayisi").ToString();
    }

    // silah scriptlerinden eri�ilecek
    public void bombaAl()
    {
        PlayerPrefs.SetInt("bomba_sayisi", PlayerPrefs.GetInt("bomba_sayisi") + 1);
        bombaSayisi.text = PlayerPrefs.GetInt("bomba_sayisi").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
