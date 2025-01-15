using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosKovan : MonoBehaviour
{
    AudioSource yereDusme;
    void Start()
    {
        yereDusme = GetComponent<AudioSource>();
        Destroy(gameObject, 2f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("beton"))
        {
            yereDusme.Play();

        }
    }
}
