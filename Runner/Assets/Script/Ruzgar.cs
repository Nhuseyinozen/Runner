using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruzgar : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("AltKarakter"))
        {

            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 0, 0), ForceMode.Impulse);

        }
    }



}
