using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Huseyin;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject VarisNoktasi;
    public static int AnlikKarakterSayisi = 1;
    public GameObject _AnaKarakter;
    public bool OyunBittimi;
    public bool SiniriGectin;

    [Header("----- OBJE HAVUZU -----")]
    public List<GameObject> KarakterHavuzu;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekeleri;

    [Header("---SAPKALAR---")]
    public GameObject[] Sapkalar;

    [Header("---SOPALAR---")]
    public GameObject[] Sopalar;

    [Header("---MATERIAL---")]
    public Material[] Materyaller;
    public SkinnedMeshRenderer _Renderer;

    [Header("---LEVEL---")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;

    [Header("-----SES OBJELERÝ-----")]
    public AudioSource[] Sesler;
    public GameObject[] islemPanelleri;
    public Slider oyunSesiAyar;

    [Header("---Yükleme Ekraný Ýþlemleri---")]
    public GameObject YukleniyorPanel;
    public Slider YukleniyorSlider;
    public TextMeshProUGUI YukleniyorText;

    BellekYonetimi BellekYonetimi = new BellekYonetimi();
    VeriKontrol _VeriKontrol = new VeriKontrol();

    Scene _scene;

    public List<DilVerileriAnaObje> _DilverileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _OkunacakDilVerileri = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    

    private void Awake()
    {
        Sesler[0].volume = BellekYonetimi.VeriOku_Float("OyunSes");
        Sesler[1].volume = BellekYonetimi.VeriOku_Float("MenuFx");
        oyunSesiAyar.value = BellekYonetimi.VeriOku_Float("OyunSes");
        Destroy(GameObject.FindWithTag("MenuSes"));
        ItemKontrol();
    }

    void Start()
    {
        DusmanAktifEt();
        _scene = SceneManager.GetActiveScene();

      
        _VeriKontrol.DilVerileriLoad();
        _OkunacakDilVerileri = _VeriKontrol.DilVerileriListeyeAktar();
        _DilverileriAnaObje.Add(_OkunacakDilVerileri[5]);
        DilKontrol();
    }
    public void DilKontrol()
    {
        if (BellekYonetimi.VeriOku_String("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilverileriAnaObje[0]._DýlVerileri_TR[i].kelime;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilverileriAnaObje[0]._DýlVerileri_EN[i].kelime;
            }
        }
    }
    public void DusmanAktifEt()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }

    public void DusmanTetikle()
    {

        foreach (var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SiniriGectin = true;
        SavasDurum();
    }

    public void KarakterYonetimi(string islemTuru, int gelenSayi, Vector3 OlusmaPoz)
    {

        switch (islemTuru)
        {
            case "Carpma":
                Kutuphane.Carpma(gelenSayi, KarakterHavuzu, OlusmaPoz, OlusmaEfektleri);
                break;

            case "Toplama":
                Kutuphane.Toplama(gelenSayi, KarakterHavuzu, OlusmaPoz, OlusmaEfektleri);
                break;

            case "Cikartma":
                Kutuphane.Cikartma(gelenSayi, KarakterHavuzu, YokOlmaEfektleri);
                break;

            case "Bolme":
                Kutuphane.Bolme(gelenSayi, KarakterHavuzu, YokOlmaEfektleri);
                break;
        }
    }

    void SavasDurum()
    {
        if (SiniriGectin)
        {
            if (AnlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittimi = true;

                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                        
                    }
                }
                foreach (var item in KarakterHavuzu)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }

                _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);


                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun || AnlikKarakterSayisi == 1)
                {
                    islemPanelleri[3].SetActive(true);
                }
                else
                {
                    if (AnlikKarakterSayisi > 5)
                    {
                        if (_scene.buildIndex == BellekYonetimi.VeriOku_Int("SonLevel"))
                        {
                            BellekYonetimi.VeriKaydet_Int("Puan", BellekYonetimi.VeriOku_Int("Puan") + 600);
                            BellekYonetimi.VeriKaydet_Int("SonLevel", BellekYonetimi.VeriOku_Int("SonLevel") + 1);
                        }
                    }
                    else
                    {
                        if (_scene.buildIndex == BellekYonetimi.VeriOku_Int("SonLevel"))
                        {
                            BellekYonetimi.VeriKaydet_Int("Puan", BellekYonetimi.VeriOku_Int("Puan") + 200);
                            BellekYonetimi.VeriKaydet_Int("SonLevel", BellekYonetimi.VeriOku_Int("SonLevel") + 1);
                        }
                    }

                    islemPanelleri[2].SetActive(true);
                }
            }
        }
    }

    public void YokOlmaEfekt(Vector3 Pozisyon, bool Durum = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();

                if (!Durum)
                    AnlikKarakterSayisi--;
                else
                    KacDusmanOlsun--;
                break;

            }
        }
        if (!OyunBittimi)
        {
            SavasDurum();
        }

    }
    public void AdamLekeleriEfekt(Vector3 Pozisyon)
    {
        foreach (var item in AdamLekeleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                break;

            }
        }
    }

    public void ItemKontrol()
    {
        if (BellekYonetimi.VeriOku_Int("AktifSapka") != -1)
        {
            Sapkalar[BellekYonetimi.VeriOku_Int("AktifSapka")].SetActive(true);
        }

        if (BellekYonetimi.VeriOku_Int("AktifSopa") != -1)
        {
            Sopalar[BellekYonetimi.VeriOku_Int("AktifSopa")].SetActive(true);
        }

        if (BellekYonetimi.VeriOku_Int("AktifTema") != -1)
        {
            _Renderer.material = Materyaller[BellekYonetimi.VeriOku_Int("AktifTema")];
        }
        else
        {
            _Renderer.material = Materyaller[0];
        }


    }

    public void CikisIslemi(string durum)
    {
        Sesler[1].Play();
        Time.timeScale = 0;
        if (durum == "durdur")
        {
            islemPanelleri[0].SetActive(true);
        }
        else if (durum == "devamet")
        {
            islemPanelleri[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (durum == "tekrar")
        {
            SceneManager.LoadScene(_scene.buildIndex);
            Time.timeScale = 1;
        }
        else if (durum == "anamenu")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void Ayarlar(string durum)
    {

        if (durum == "ayarlar")
        {
            islemPanelleri[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            islemPanelleri[1].SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SliderIslem()
    {
        BellekYonetimi.VeriKaydet_Float("OyunSes", oyunSesiAyar.value);
        Sesler[0].volume = oyunSesiAyar.value;
    }

    public void SonrakiLevel()
    {
        StartCoroutine(SahneYukleniyor(_scene.buildIndex + 1));
    }

    IEnumerator SahneYukleniyor(int sahneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sahneIndex);

        YukleniyorPanel.SetActive(true);

        while (!operation.isDone) //Eðer yükleme 1 e eþit deðilse.
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            YukleniyorSlider.value = progress;

            int textDegeri = Mathf.CeilToInt(progress * 100);
            YukleniyorText.text = textDegeri.ToString();

            yield return null;
        }

    }
}
