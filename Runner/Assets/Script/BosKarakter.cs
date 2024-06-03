using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{
    public GameManager _GameManager;
    public NavMeshAgent _NavMesh;
    public SkinnedMeshRenderer _Renderer;
    public GameObject _Target;
    public Animator _Animator;
    public Material _AtanacakMaterial;
    bool TemasVarmi;

    void LateUpdate()
    {
        if (TemasVarmi)
            _NavMesh.SetDestination(_Target.transform.position);
    }
    Vector3 YeniPoz()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }

    void MaterialVeAnimasyonTetikle()
    {
        _Renderer.material = _AtanacakMaterial;
        _Animator.SetBool("Saldir", true);
        GameManager.AnlikKarakterSayisi++;
        gameObject.tag = "AltKarakter";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakter") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BosKarakter"))
            {
                MaterialVeAnimasyonTetikle();
                TemasVarmi = true;
                GetComponent<AudioSource>().Play();
            }
        }
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
            _GameManager.YokOlmaEfekt(YeniPoz(), false);
            gameObject.SetActive(false);
        }
    }




}
