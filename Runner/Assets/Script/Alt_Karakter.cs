using UnityEngine;
using UnityEngine.AI;

public class Alt_Karakter : MonoBehaviour
{

    GameObject Target;
    NavMeshAgent _NavMesh;
    public GameManager _GameManager;

    void Start()
    {
        _NavMesh = GetComponent<NavMeshAgent>();
        Target = _GameManager.VarisNoktasi;
    }

    void Update()
    {
        _NavMesh.SetDestination(Target.transform.position);
    }

    Vector3 YeniPoz()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ignelikutu"))
        {
            _GameManager.YokOlmaEfekt(YeniPoz());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("testere"))
        {
            _GameManager.YokOlmaEfekt(YeniPoz());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("PervaneIgneler"))
        {
            _GameManager.YokOlmaEfekt(YeniPoz());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Balyoz"))
        {
            Vector3 LekePoz = new Vector3(transform.position.x, 0.05f, transform.position.z);
            _GameManager.YokOlmaEfekt(YeniPoz());
            _GameManager.AdamLekeleriEfekt(LekePoz);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Dusman"))
        {
            _GameManager.YokOlmaEfekt(YeniPoz(),false);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("BosKarakter"))
        {
            _GameManager.KarakterHavuzu.Add(other.gameObject);
        }
    }
}
