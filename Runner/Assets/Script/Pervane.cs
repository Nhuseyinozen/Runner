using System.Collections;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator _Animator;
    public float BeklemeSuresi;
    public BoxCollider _Ruzgar;


    // Animasyon baþlagoçta "Calistir" deðeri true dönücek, animasyon bittikten sonra fonksiyon ile "Calistir" False yapýcaz.


    public void AnimasyonDurum(string durum)
    {
        if (durum == "true")
        {
            _Animator.SetBool("Calistir", true);
            _Ruzgar.enabled = true;
        }
        else
        {
            _Animator.SetBool("Calistir", false);
            StartCoroutine(AnimasyonTetikle());
            _Ruzgar.enabled = false;
        }
    }

    IEnumerator AnimasyonTetikle()
    {
        yield return new WaitForSeconds(BeklemeSuresi);
        // Üsteki döngü bittikten sonra "Calistir" deðerini tekrar true döndürerek animasyonu tekrarlýycak.
        AnimasyonDurum("true");


    }


}
