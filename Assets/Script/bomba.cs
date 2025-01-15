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
            Debug.Log("�arpt�");
            patlama();
        }
        */

        // herhangi bir obje ile �arp��ma var ise, bo� de�ilse
        if (collision != null)
        {

            patlama();
            Destroy(gameObject,1f);
        }
    }

    void patlama()
    {
        // patlaman�n ger�ekle�ece�i pozisyon, bomban�n kendi pozisyonu, ��nk� onCollision ile �arp��ma varsa �al��acak
        // �arpt��� yerde de patlayacak
        // Vector3 patlamaPozisyonu = collision.contacts[0].point;
        Vector3 patlamaPozisyonu = transform.position;

        Instantiate(patlamaEfekti, patlamaPozisyonu, transform.rotation);
        if (!patlamaSesi.isPlaying)
        {
            patlamaSesi.Play();
        }

        // Physics.OverlapSphere : bomban�n �arpt��� noktada ve belirlene menzil de fiziksel bir yuvarlak olu�turacak
        // yuvarlak s�n�rlar�nda ki colliderlar� diziye alacak
        Collider[] colliders = Physics.OverlapSphere(patlamaPozisyonu, menzil);

        foreach (Collider hit in colliders)
        {
            // colliders dizisindeki objelerin rigidbodylerine eri�iyoruz.
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            // e�er ki hit bo� de�ilse ve rb true ise
            if (hit != null && rb)
            {
                if (hit.gameObject.CompareTag("dusman"))
                {
                    // hitin i�inde d��man tagl� obje var ise oldun fonksiyonunu �al��t�r.
                    hit.gameObject.GetComponent<dusman>().oldun();
                }

                rb.AddExplosionForce(guc, patlamaPozisyonu, menzil, yukariGuc, ForceMode.Impulse);
            }
        }

        
    }
}
