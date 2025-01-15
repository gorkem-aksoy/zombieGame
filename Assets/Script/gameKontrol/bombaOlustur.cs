using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaOlustur : MonoBehaviour
{
    public GameObject bombaPoint;
    public GameObject bombaObjesi;
    public Camera benimCam;

    envanterKontrol envanterKontrol;

    void Start()
    {
        envanterKontrol = GetComponent<envanterKontrol>();
    }
  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(PlayerPrefs.GetInt("bomba_sayisi") != 0)
            {
                GameObject bombam = Instantiate(bombaObjesi, bombaPoint.transform.position, bombaPoint.transform.rotation);
                Rigidbody rg = bombam.GetComponent<Rigidbody>();

                // kameranýn ileriye doðru baktýðý yönü kullanarak 90 derecelik bir açý oluþturur.
                // Quaternion.AngleAxis(90, benimCam.transform.forward) : bu açýyý oluþturdu, bizim ise bir vektöre ihtiyacýmýz var
                // * benimCam.transform.forward : bu çarpým ile kameranýn ileri yönünde bir vektör oluþturduk.
                Vector3 bombaAcisi = Quaternion.AngleAxis(90, benimCam.transform.forward) * benimCam.transform.forward;

                rg.AddForce(bombaAcisi * 700);

                PlayerPrefs.SetInt("bomba_sayisi", PlayerPrefs.GetInt("bomba_sayisi") - 1);
                envanterKontrol.bombaSayisi.text = PlayerPrefs.GetInt("bomba_sayisi").ToString();
            }
            else
            {
                // ses veya bildirim
            }
        }
    }
}
