using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Huseyin;
using UnityEngine.UI;

public class AnaMenu_Manager : MonoBehaviour
{
    public AudioSource butonSes;
    public GameObject CikisPanel;

    VeriKontrol _VeriKontrol = new VeriKontrol();
    BellekYonetimi AnaMenuBellek = new BellekYonetimi();

    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    public List<DilVerileriAnaObje> Varsayilan_DilVerileri = new List<DilVerileriAnaObje>();

    public List<DilVerileriAnaObje> _DilverileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _OkunacakDilVerileri = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;


    [Header("---Yükleme Ekraný Ýþlemleri---")]
    public GameObject YukleniyorPanel;
    public Slider YukleniyorSlider;
    public TextMeshProUGUI YukleniyorText;

    void Start()
    {

       
        AnaMenuBellek.KontrolEtVeTanimla();
        _VeriKontrol.IlkDosyaKaydetme(_ItemBilgileri, Varsayilan_DilVerileri);
        butonSes.volume = AnaMenuBellek.VeriOku_Float("MenuFx");


        _VeriKontrol.DilVerileriLoad();
        _OkunacakDilVerileri = _VeriKontrol.DilVerileriListeyeAktar();
        _DilverileriAnaObje.Add(_OkunacakDilVerileri[0]);
        DilKontrol();

    }
    public void DilKontrol()
    {
        if (AnaMenuBellek.VeriOku_String("Dil") == "TR")
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

    public void SahneYukle(int Index)
    {
        butonSes.Play();
        SceneManager.LoadScene(Index);
    }

    public void Oyna()
    {
        butonSes.Play();
        StartCoroutine(SahneYukleniyor(AnaMenuBellek.VeriOku_Int("SonLevel")));
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

    public void CikisÝslemi(string islem)
    {
        butonSes.Play();
        if (islem == "Cikis")
        {
            CikisPanel.SetActive(true);
        }
        else if (islem == "Evet")
        {
            Application.Quit();
        }
        else
        {
            CikisPanel.SetActive(false);
        }
    }

}
