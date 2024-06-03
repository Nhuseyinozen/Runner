using System.Collections;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator _Animator;
    public float BeklemeSuresi;
    public BoxCollider _Ruzgar;


    // Animasyon ba�lago�ta "Calistir" de�eri true d�n�cek, animasyon bittikten sonra fonksiyon ile "Calistir" False yap�caz.


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
        // �steki d�ng� bittikten sonra "Calistir" de�erini tekrar true d�nd�rerek animasyonu tekrarl�ycak.
        AnimasyonDurum("true");


    }


}
