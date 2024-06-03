using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameObject _Kamera;
    public GameManager _GameManager;
    Transform OlusmaPozisyonu;

    public GameObject KarakterYeniPoz;
    public bool SonaGelindimi;

    public Slider _Slider;
    public GameObject BitisNoktasi;


    private void Start()
    {
        float Fark = Vector3.Distance(transform.position, BitisNoktasi.transform.position);
        _Slider.maxValue = Fark;
    }

    private void FixedUpdate()
    {
        if (!SonaGelindimi)
        {
            transform.Translate(Vector3.forward * 0.75f * Time.deltaTime);
        }
    }

    void Update()
    {
        if(Time.timeScale != 0)
        {
            if (SonaGelindimi)
            {
                transform.position = Vector3.Lerp(transform.position, KarakterYeniPoz.transform.position, .025f);
                if (_Slider.value != 0)
                {
                    _Slider.value -= 0.1f;
                }
            }
            else
            {
                float Fark = Vector3.Distance(transform.position, BitisNoktasi.transform.position);
                _Slider.value = Fark;

                if (Input.GetKey(KeyCode.Mouse0)) // Mouse sol tuþuna basýlý tutulduðunda;
                {
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .06f,
                        transform.position.y, transform.position.z), 0.75f);
                    }
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .06f,
                        transform.position.y, transform.position.z), 0.75f);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Bolme") || other.CompareTag("Cikartma") || other.CompareTag("Toplama"))
        {
            Vector3 OlusmaPozisyonu = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y,
                 other.gameObject.transform.position.z + 0.25f);

            int sayi = int.Parse(other.name);
            _GameManager.KarakterYonetimi(other.tag, sayi, OlusmaPozisyonu);
        }

        if (other.CompareTag("SonaGelindi"))
        {
            _Kamera.GetComponent<Kamera>().SonaGelindimi = true;
            SonaGelindimi=true;
            _GameManager.DusmanTetikle();
        }
        if(other.CompareTag("BosKarakter"))
        {
            _GameManager.KarakterHavuzu.Add(other.gameObject);
          
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Direk") || collision.gameObject.CompareTag("ignelikutu") || collision.gameObject.CompareTag("PervaneIgneler"))
        {
            if (transform.position.x >0)
            {
                transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
            }
        }
    }
}
