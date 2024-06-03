using System.Collections;
using UnityEngine;

public class AdamLekesi : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

}
