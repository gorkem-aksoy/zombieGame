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

                // kameran�n ileriye do�ru bakt��� y�n� kullanarak 90 derecelik bir a�� olu�turur.
                // Quaternion.AngleAxis(90, benimCam.transform.forward) : bu a��y� olu�turdu, bizim ise bir vekt�re ihtiyac�m�z var
                // * benimCam.transform.forward : bu �arp�m ile kameran�n ileri y�n�nde bir vekt�r olu�turduk.
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
