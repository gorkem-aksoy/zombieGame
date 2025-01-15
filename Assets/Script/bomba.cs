using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{
    public float guc;
    public float menzil;
    public float yukariGuc;
    public ParticleSystem patlamaEfekti;
    AudioSource patlamaSesi;
    void Start()
    {
        patlamaSesi = GetComponent<AudioSource>();   
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.transform.gameObject.CompareTag("beton"))
        {
            Debug.Log("çarptý");
            patlama();
        }
        */

        // herhangi bir obje ile çarpýþma var ise, boþ deðilse
        if (collision != null)
        {

            patlama();
            Destroy(gameObject,1f);
        }
    }

    void patlama()
    {
        // patlamanýn gerçekleþeceði pozisyon, bombanýn kendi pozisyonu, çünkü onCollision ile çarpýþma varsa çalýþacak
        // çarptýðý yerde de patlayacak
        // Vector3 patlamaPozisyonu = collision.contacts[0].point;
        Vector3 patlamaPozisyonu = transform.position;

        Instantiate(patlamaEfekti, patlamaPozisyonu, transform.rotation);
        if (!patlamaSesi.isPlaying)
        {
            patlamaSesi.Play();
        }

        // Physics.OverlapSphere : bombanýn çarptýðý noktada ve belirlene menzil de fiziksel bir yuvarlak oluþturacak
        // yuvarlak sýnýrlarýnda ki colliderlarý diziye alacak
        Collider[] colliders = Physics.OverlapSphere(patlamaPozisyonu, menzil);

        foreach (Collider hit in colliders)
        {
            // colliders dizisindeki objelerin rigidbodylerine eriþiyoruz.
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            // eðer ki hit boþ deðilse ve rb true ise
            if (hit != null && rb)
            {
                if (hit.gameObject.CompareTag("dusman"))
                {
                    // hitin içinde düþman taglý obje var ise oldun fonksiyonunu çalýþtýr.
                    hit.gameObject.GetComponent<dusman>().oldun();
                }

                rb.AddExplosionForce(guc, patlamaPozisyonu, menzil, yukariGuc, ForceMode.Impulse);
            }
        }

        
    }
}
