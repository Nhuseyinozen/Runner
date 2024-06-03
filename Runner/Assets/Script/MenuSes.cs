using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSes : MonoBehaviour
{
    private static GameObject instance;
    public AudioSource ses;

    void Start()
    {
        ses.volume = PlayerPrefs.GetFloat("MenuSes");
        DontDestroyOnLoad(gameObject); // bu objeyi kaybetme!!

        if (instance == null) // daha once olusturulmadýysa olustur
            instance = gameObject;
        else // 2.olusursa yok et !
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ses.volume = PlayerPrefs.GetFloat("MenuSes");
    }
}
