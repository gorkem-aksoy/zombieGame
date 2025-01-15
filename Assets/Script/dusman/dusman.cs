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

    // silah scriptlerinden silah hasarýný gönderiyoruz.
    public void darbeAl(float alinanDarbe)
    {
        if (dusmanOldumu) return;

        saglik -= alinanDarbe;

        if(saglik <= 0)
        {
            oldun();
        }

    }

    // caný biterse çalýþacak fonksiyon
    // bomba scriptinden çaðýrarak, bomba çarparsa zombileri öldürmek için
    public void oldun()
    {

        if (!dusmanOldumu)
        {
            gameKontrol.GetComponent<dusmanOlustur>().dusmanSayisiGuncelle();
            animator.Play("olme");
            Destroy(gameObject, 2f);
            Debug.Log("düþman yok edildi");

            dusmanOldumu = true;
        }
    }

    // hedef Objeye hasar verme
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("hedefObje"))
        {
            Debug.Log("deðdi");
            gameKontrol.GetComponent<korunacakObjeSaglik>().canAzalt(dusmanHasari);
            oldun();
        }
    }
}
