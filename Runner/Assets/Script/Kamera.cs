using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target; // hedef .
    public Vector3 targer_offset; // hedef açý.
    public GameObject KameraYeniPoz;
    public bool SonaGelindimi;


    void Start()
    {
        targer_offset = transform.position - target.position; // kameranýn pozisyonundan hedef pozisyonu çýkar.
    }


    private void LateUpdate()
    {
        if (!SonaGelindimi)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targer_offset, .125f);
        }
        else if (SonaGelindimi) 
        {
            transform.position = Vector3.Lerp(transform.position, KameraYeniPoz.transform.position, .015f);
        }
        
    }
}
