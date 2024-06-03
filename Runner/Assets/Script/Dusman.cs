using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    public GameObject Saldiri_Poz;
    NavMeshAgent _NavMash;
    bool SadiriBasladimi;
    void Start()
    {
        _NavMash = GetComponent<NavMeshAgent>();
    }

    public void AnimasyonTetikle()
    {
        GetComponent<Animator>().SetBool("Saldir", true);
        SadiriBasladimi = true;
    }

    Vector3 YeniPoz()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }

    void LateUpdate()
    {
        if (SadiriBasladimi)
        {
            _NavMash.SetDestination(Saldiri_Poz.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakter"))
        {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().YokOlmaEfekt(YeniPoz(),true);
            gameObject.SetActive(false);

        }
    }
}
