using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dusman : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject hedef;

    GameObject gameKontrol;

    public float saglik;
    public float dusmanHasari;

    Animator animator;

    private bool dusmanOldumu = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameKontrol = GameObject.FindWithTag("anaKontrolcum");
        animator = GetComponent<Animator>();

    }

    public void hedefBelirle(GameObject objem)
    {
        hedef = objem;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(hedef.transform.position);
    }

    // silah scriptlerinden silah hasar�n� g�nderiyoruz.
    public void darbeAl(float alinanDarbe)
    {
        if (dusmanOldumu) return;

        saglik -= alinanDarbe;

        if(saglik <= 0)
        {
            oldun();
        }

    }

    // can� biterse �al��acak fonksiyon
    // bomba scriptinden �a��rarak, bomba �arparsa zombileri �ld�rmek i�in
    public void oldun()
    {

        if (!dusmanOldumu)
        {
            gameKontrol.GetComponent<dusmanOlustur>().dusmanSayisiGuncelle();
            animator.Play("olme");
            Destroy(gameObject, 2f);
            Debug.Log("d��man yok edildi");

            dusmanOldumu = true;
        }
    }

    // hedef Objeye hasar verme
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("hedefObje"))
        {
            Debug.Log("de�di");
            gameKontrol.GetComponent<korunacakObjeSaglik>().canAzalt(dusmanHasari);
            oldun();
        }
    }
}
