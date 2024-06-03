using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Huseyin;
using TMPro;

public class AyarlarManager : MonoBehaviour
{
    [Header("----------Slider------------")]
    public AudioSource butonSes;
    public Slider menuSes;
    public Slider menuFx;
    public Slider oyunSes;

    [Header("-----Dil Objeleri-----")]
    public TextMeshProUGUI dilText;
    public Button[] DilButonlari;
    public int aktifDilIndex;


    BellekYonetimi _BellekYonetimi = new BellekYonetimi();
    VeriKontrol _VeriKontrol = new VeriKontrol();


    public List<DilVerileriAnaObje> _DilverileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _OkunacakDilVerileri = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    void Start()
    {
        _VeriKontrol.DilVerileriLoad();
        _OkunacakDilVerileri = _VeriKontrol.DilVerileriListeyeAktar();
        _DilverileriAnaObje.Add(_OkunacakDilVerileri[4]);
        DilKontrol();
        DilDurumuKontrolEt();

        menuSes.value = _BellekYonetimi.VeriOku_Float("MenuSes");
        menuFx.value = _BellekYonetimi.VeriOku_Float("MenuFx");
        oyunSes.value = _BellekYonetimi.VeriOku_Float("OyunSes");

        butonSes.volume = _BellekYonetimi.VeriOku_Float("MenuFx");

    }
    public void DilKontrol()
    {
        if (_BellekYonetimi.VeriOku_String("Dil") == "TR")
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

    public void SliderIslemleri(string islem)
    {
        switch (islem)
        {
            case "menuses":

                _BellekYonetimi.VeriKaydet_Float("MenuSes", menuSes.value);
                break;

            case "menufx":

                _BellekYonetimi.VeriKaydet_Float("MenuFx", menuFx.value);
                break;

            case "oyunses":

                _BellekYonetimi.VeriKaydet_Float("OyunSes", oyunSes.value);
                break;
        }
    }

    public void GeriDon()
    {
        butonSes.Play();
        SceneManager.LoadScene(0);

    }

    public void DilDurumuKontrolEt()
    {
        if (_BellekYonetimi.VeriOku_String("Dil") == "TR")
        {
            aktifDilIndex = 0;
            dilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
        }
        else
        {
            aktifDilIndex = 1;
            dilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;

        }
    }


    public void DilDegistir(string islem)
    {
        if (islem == "ileri")
        {

            aktifDilIndex = 1;
            dilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;
            DilButonlari[0].interactable = true;
            _BellekYonetimi.VeriKaydet_String("Dil", "EN");
            DilKontrol();

        }
        else
        {
            aktifDilIndex = 0;
            dilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
            DilButonlari[1].interactable = true;
            _BellekYonetimi.VeriKaydet_String("Dil", "TR");
            DilKontrol();
        }

        butonSes.Play();
    }


}
