using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Huseyin;
using TMPro;
using System;
using UnityEngine.SceneManagement;




public class Ozellestir_Manager : MonoBehaviour
{
    public TextMeshProUGUI PuanText;
    public GameObject[] islemPanelleri;
    public GameObject islemCanvas;
    public GameObject[] GenelPaneller;
    public Button[] islemButonlari;
    public TextMeshProUGUI kaydedildi;
    public int aktifIslemPaneliIndex;

    [Header("---SAPKALAR---")]
    public TextMeshProUGUI SapkaText;
    public GameObject[] Sapkalar;
    public Button[] SapkaButonlari;
    int sapkaIndex = -1;

    [Header("---SOPALAR---")]
    public TextMeshProUGUI SopaText;
    public GameObject[] Sopalar;
    public Button[] SopaButonlari;
    int sopaIndex = -1;

    [Header("---MATERIAL---")]
    public TextMeshProUGUI MateryalText;
    public Material[] Materyaller;
    public Button[] MateryalButonlari;
    public SkinnedMeshRenderer _Renderer;
    int materyalIndex = -1;

    // Item objelerinin isimleri farkl� oldu�u i�in yeniden de�i�ken tan�mlay�p duruma g�re dilkontrol metodunda atama yap�yoruz.
    string satinAlmaText;
    string itemText;

    public AudioSource[] Sesler;

    BellekYonetimi _BellekYonetimi = new BellekYonetimi();

    VeriKontrol _VeriKontrol = new VeriKontrol();

    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();

    public List<DilVerileriAnaObje> _DilverileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _OkunacakDilVerileri = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;


    void Start()
    {
        
        _BellekYonetimi.VeriKaydet_Int("Puan", 1500);
        PuanText.text = _BellekYonetimi.VeriOku_Int("Puan").ToString();
        _VeriKontrol.Load();
        _ItemBilgileri = _VeriKontrol.ListeyeAktar();

        // Ozellestir men�s� a��ld���nda skinlerin g�r�nmesi i�in durumukontrol et metodunu her b�l�m i�in �a��r�yoruz.
        DurumuKontrolEt(0, true);
        DurumuKontrolEt(1, true);
        DurumuKontrolEt(2, true);

        foreach (var item in Sesler)
        {
            item.volume = _BellekYonetimi.VeriOku_Float("MenuFx");
        }


        _VeriKontrol.DilVerileriLoad();
        _OkunacakDilVerileri = _VeriKontrol.DilVerileriListeyeAktar();
        _DilverileriAnaObje.Add(_OkunacakDilVerileri[1]);
        DilKontrol();
    }

    public void DilKontrol()
    {
        if (_BellekYonetimi.VeriOku_String("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilverileriAnaObje[0]._D�lVerileri_TR[i].kelime;
            }

            satinAlmaText = _DilverileriAnaObje[0]._D�lVerileri_TR[5].kelime;
            itemText = _DilverileriAnaObje[0]._D�lVerileri_TR[4].kelime;
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilverileriAnaObje[0]._D�lVerileri_EN[i].kelime;
            }

            satinAlmaText = _DilverileriAnaObje[0]._D�lVerileri_EN[5].kelime;
            itemText = _DilverileriAnaObje[0]._D�lVerileri_EN[4].kelime;
        }
    }
    public void SapkaButon(string islem)
    {
        Sesler[0].Play();
        if (islem == "ileri") // ileri butonuna bas�ld�ysa.
        {

            if (sapkaIndex == -1)
            {
                sapkaIndex = 0;
                Sapkalar[sapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[sapkaIndex].ItemAd;

                // Sat�n al buton text i�lemi
                if (!_ItemBilgileri[sapkaIndex].SatinAlmaDurumu)
                {
                    TextObjeleri[5].text = _ItemBilgileri[sapkaIndex].Puan + " - " + satinAlmaText;

                    islemButonlari[1].interactable = false;

                    if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[sapkaIndex].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }

            }
            else
            {
                Sapkalar[sapkaIndex].SetActive(false); // �nceki �apkay� pasif yap.
                sapkaIndex++;
                Sapkalar[sapkaIndex].SetActive(true); // sonraki �apka * ;
                SapkaText.text = _ItemBilgileri[sapkaIndex].ItemAd;

                // Sat�n al buton text i�lemi
                if (!_ItemBilgileri[sapkaIndex].SatinAlmaDurumu)
                {
                    TextObjeleri[5].text = _ItemBilgileri[sapkaIndex].Puan + " - " + satinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[sapkaIndex].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;

                }
                else
                {
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }

            // ---------------------------------------------------

            if (sapkaIndex == Sapkalar.Length - 1) // Sapka index ile sapkalar dizisinin uzunlu�u e�itse sona gelindi.
            {
                SapkaButonlari[1].interactable = false;

            }
            else
            {
                SapkaButonlari[1].interactable = true; // ��lem yap�l�p dizide geri gidilirse butonu aktifle�tir.
            }

            // ----------------------------------------------------

            if (sapkaIndex != -1) // Dizi s�ras� ba�lag��ta de�ilse geri butonunu aktifle�tir.
            {
                SapkaButonlari[0].interactable = true;
            }
        }

        // ---------------------------------------------------------------------------
        else
        {

            if (sapkaIndex != -1) // Dizide 0.index gelmediyse.
            {

                Sapkalar[sapkaIndex].SetActive(false);
                sapkaIndex--;

                if (sapkaIndex != -1)
                {
                    Sapkalar[sapkaIndex].SetActive(true);
                    SapkaButonlari[0].interactable = true;
                    SapkaText.text = _ItemBilgileri[sapkaIndex].ItemAd;

                    // Sat�n al buton text i�lemi
                    if (!_ItemBilgileri[sapkaIndex].SatinAlmaDurumu)
                    {
                        TextObjeleri[5].text = _ItemBilgileri[sapkaIndex].Puan + " - " + satinAlmaText;
                        islemButonlari[0].interactable = true;
                        islemButonlari[1].interactable = false;

                    }
                    else
                    {
                        TextObjeleri[5].text = satinAlmaText; ;
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    SapkaButonlari[0].interactable = false;
                    SapkaText.text = itemText; 
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                }


            }
            else // dizi sonu.
            {
                SapkaButonlari[0].interactable = false;
                SapkaText.text = itemText; 
                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
            }


            // ----------------------------------------------------
            if (sapkaIndex != Sapkalar.Length - 1)
            {
                SapkaButonlari[1].interactable = true;

            }
        }






    }
    public void SopaButon(string islem)
    {
        Sesler[0].Play();
        if (islem == "ileri") // ileri butonuna bas�ld�ysa.
        {

            if (sopaIndex == -1)
            {
                sopaIndex = 0;
                Sopalar[sopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[sopaIndex + 3].ItemAd;

                // Sat�n al buton text i�lemi
                if (!_ItemBilgileri[sopaIndex + 3].SatinAlmaDurumu)
                {
                    TextObjeleri[5].text = _ItemBilgileri[sopaIndex + 3].Puan + " - " + satinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[sopaIndex + 3].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;

                }
                else
                {
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {
                Sopalar[sopaIndex].SetActive(false); // �nceki sopay� pasif yap.
                sopaIndex++;
                Sopalar[sopaIndex].SetActive(true); // sonraki sopa * ;
                SopaText.text = _ItemBilgileri[sopaIndex + 3].ItemAd;


                // Sat�n al buton text i�lemi
                if (!_ItemBilgileri[sopaIndex + 3].SatinAlmaDurumu)
                {
                    TextObjeleri[5].text = _ItemBilgileri[sopaIndex + 3].Puan + " - " + satinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[sopaIndex + 3].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;

                }
                else
                {
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }

            // ---------------------------------------------------

            if (sopaIndex == Sopalar.Length - 1) // Sapka index ile Sopalar dizisinin uzunlu�u e�itse sona gelindi.
            {
                SopaButonlari[1].interactable = false;

            }
            else
            {
                SopaButonlari[1].interactable = true; // ��lem yap�l�p dizide geri gidilirse butonu aktifle�tir.
            }

            // ----------------------------------------------------

            if (sopaIndex != -1) // Dizi s�ras� ba�lag��ta de�ilse geri butonunu aktifle�tir.
            {
                SopaButonlari[0].interactable = true;
            }
        }

        // ---------------------------------------------------------------------------
        else
        {

            if (sopaIndex != -1)
            {

                Sopalar[sopaIndex].SetActive(false);
                sopaIndex--;

                if (sopaIndex != -1)
                {
                    Sopalar[sopaIndex].SetActive(true);
                    SopaButonlari[0].interactable = true;
                    SopaText.text = _ItemBilgileri[sopaIndex + 3].ItemAd;

                    // Sat�n al buton text i�lemi
                    if (!_ItemBilgileri[sopaIndex + 3].SatinAlmaDurumu)
                    {
                        TextObjeleri[5].text = _ItemBilgileri[sopaIndex + 3].Puan + " - " + satinAlmaText;
                        islemButonlari[1].interactable = false;

                        if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[sopaIndex + 3].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;

                    }
                    else
                    {
                        TextObjeleri[5].text = satinAlmaText; ;
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    SopaButonlari[0].interactable = false;
                    SopaText.text = itemText;
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                }


            }
            else // dizi sonu.
            {
                SopaButonlari[0].interactable = false;
                SopaText.text = itemText; 
                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
            }


            // ----------------------------------------------------
            if (sopaIndex != Sopalar.Length - 1)
            {
                SopaButonlari[1].interactable = true;

            }
        }
    }
    public void TemaButon(string islem)
    {
        Sesler[0].Play();
        if (islem == "ileri") // ileri butonuna bas�ld�ysa.
        {

            if (materyalIndex == -1)
            {
                materyalIndex = 0;
                _Renderer.material = Materyaller[materyalIndex];
                MateryalText.text = _ItemBilgileri[materyalIndex + 6].ItemAd;

                // Sat�n al buton text i�lemi
                if (!_ItemBilgileri[materyalIndex + 6].SatinAlmaDurumu)
                {
                    TextObjeleri[5].text = _ItemBilgileri[materyalIndex + 6].Puan + " - " + satinAlmaText;

                    islemButonlari[1].interactable = false;

                    if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[materyalIndex + 6].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;

                }
                else
                {
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {

                materyalIndex++;
                _Renderer.material = Materyaller[materyalIndex];
                MateryalText.text = _ItemBilgileri[materyalIndex + 6].ItemAd;

                // Sat�n al buton text i�lemi
                if (!_ItemBilgileri[materyalIndex + 6].SatinAlmaDurumu)
                {
                    TextObjeleri[5].text = _ItemBilgileri[materyalIndex + 6].Puan + " - " + satinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[materyalIndex + 6].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;

                }
                else
                {
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }

            // ---------------------------------------------------

            if (materyalIndex == Materyaller.Length - 1) // Sapka index ile Sopalar dizisinin uzunlu�u e�itse sona gelindi.
            {
                MateryalButonlari[1].interactable = false;

            }
            else
            {
                MateryalButonlari[1].interactable = true; // ��lem yap�l�p dizide geri gidilirse butonu aktifle�tir.
            }

            // ----------------------------------------------------

            if (materyalIndex != -1) // Dizi s�ras� ba�lag��ta de�ilse geri butonunu aktifle�tir.
            {
                MateryalButonlari[0].interactable = true;
            }
        }

        // ---------------------------------------------------------------------------
        else
        {

            if (materyalIndex != -1)
            {
                materyalIndex--;

                if (materyalIndex != -1)
                {
                    _Renderer.material = Materyaller[materyalIndex];
                    MateryalButonlari[0].interactable = true;
                    MateryalText.text = _ItemBilgileri[materyalIndex + 6].ItemAd;

                    // Sat�n al buton text i�lemi
                    if (!_ItemBilgileri[materyalIndex + 6].SatinAlmaDurumu)
                    {
                        TextObjeleri[5].text = _ItemBilgileri[materyalIndex + 6].Puan + " - " + satinAlmaText;
                        islemButonlari[1].interactable = false;

                        if (_BellekYonetimi.VeriOku_Int("Puan") < _ItemBilgileri[materyalIndex + 6].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;

                    }
                    else
                    {
                        TextObjeleri[5].text = satinAlmaText; ;
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    MateryalButonlari[0].interactable = false;
                    MateryalText.text = itemText;
                    TextObjeleri[5].text = satinAlmaText; ;
                    islemButonlari[0].interactable = false;
                }


            }
            else // dizi sonu.
            {
                MateryalButonlari[0].interactable = false;
                MateryalText.text = itemText;
                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
            }


            // ----------------------------------------------------
            if (materyalIndex != Materyaller.Length - 1)
            {
                MateryalButonlari[1].interactable = true;

            }
        }
    }

    public void SatinAl()
    {

        if (aktifIslemPaneliIndex != -1)
        {
            Sesler[1].Play();
            switch (aktifIslemPaneliIndex)
            {
                case 0:
                    SatinAlmaDurum(sapkaIndex);

                    break;

                case 1:

                    int gelenDeger = sopaIndex + 3;
                    SatinAlmaDurum(gelenDeger);
                    break;

                case 2:

                    int gelenDeger2 = materyalIndex + 6;
                    SatinAlmaDurum(gelenDeger2);
                    break;
            }
        }
    }
    public void Kaydet()
    {

        if (aktifIslemPaneliIndex != -1)
        {
            Sesler[2].Play();
            switch (aktifIslemPaneliIndex)
            {
                case 0:
                    _BellekYonetimi.VeriKaydet_Int("AktifSapka", sapkaIndex);

                    break;

                case 1:
                    _BellekYonetimi.VeriKaydet_Int("AktifSopa", sopaIndex);

                    break;

                case 2:
                    _BellekYonetimi.VeriKaydet_Int("AktifTema", materyalIndex);

                    break;
            }
        }
    }


    void DurumuKontrolEt(int index, bool islem = false) // Metot paramtertesine g�re i�lem yapar .
    {
        if (index == 0)
        {
            #region �APKA
            //-----------------�APKA------------------------
            if (_BellekYonetimi.VeriOku_Int("AktifSapka") == -1)
            {

                foreach (var item in Sapkalar)
                {
                    item.SetActive(false);
                }

                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = false;

                if (!islem)
                {
                    sapkaIndex = -1;
                    SapkaText.text = itemText;
                }

            }
            else
            {
                // aktif olan item �zerine item ekledigi icin t�m itemleri false yap�yoruz.
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false);
                }

                sapkaIndex = _BellekYonetimi.VeriOku_Int("AktifSapka");
                Sapkalar[sapkaIndex].SetActive(true);

                SapkaText.text = _ItemBilgileri[sapkaIndex].ItemAd;
                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;

            }
            #endregion

        }
        else if (index == 1)
        {
            #region SOPA
            //-----------------------SOPA-----------------------------


            if (_BellekYonetimi.VeriOku_Int("AktifSopa") == -1)
            {

                foreach (var item in Sopalar)
                {
                    item.SetActive(false);
                }
                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = false;

                if (!islem)
                {
                    sopaIndex = -1;
                    SopaText.text = itemText;
                }


            }
            else
            {
                // aktif olan item �zerine item ekledigi icin t�m itemleri false yap�yoruz.
                foreach (var item in Sopalar)
                {
                    item.SetActive(false);
                }

                sopaIndex = _BellekYonetimi.VeriOku_Int("AktifSopa");

                Sopalar[sopaIndex].SetActive(true);

                SopaText.text = _ItemBilgileri[sopaIndex + 3].ItemAd;
                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;

            }

            #endregion

        }
        else if (index == 2)
        {
            #region TEMA
            if (_BellekYonetimi.VeriOku_Int("AktifTema") == -1)
            {

                if (!islem)
                {
                    materyalIndex = -1;
                    TextObjeleri[5].text = satinAlmaText; ;
                    MateryalText.text = itemText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = false;

                }
                else
                {
                    _Renderer.material = Materyaller[0];
                }

            }
            else
            {
                materyalIndex = _BellekYonetimi.VeriOku_Int("AktifTema");

                _Renderer.material = Materyaller[materyalIndex];

                MateryalText.text = _ItemBilgileri[materyalIndex + 6].ItemAd;
                TextObjeleri[5].text = satinAlmaText; ;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;

            }
            #endregion
        }
    }

    public void islemPaneliCikart(int index)
    {
        Sesler[0].Play();
        DurumuKontrolEt(index);
        aktifIslemPaneliIndex = index;
        GenelPaneller[0].SetActive(true);
        islemPanelleri[index].SetActive(true);
        GenelPaneller[1].SetActive(true);
        islemCanvas.SetActive(false);

    }

    public void GeriDonIslemi()
    {
        Sesler[0].Play();
        islemCanvas.SetActive(true);
        GenelPaneller[0].SetActive(false);
        GenelPaneller[1].SetActive(false);
        islemPanelleri[aktifIslemPaneliIndex].SetActive(false);
        DurumuKontrolEt(aktifIslemPaneliIndex, true);
        aktifIslemPaneliIndex = -1;

    }

    public void AnaMenuDon()
    {
        Sesler[0].Play();
        _VeriKontrol.Save(_ItemBilgileri);
        SceneManager.LoadScene(0);
    }

    //___________________________________________________ Islem fonksiyonlar�.
    public void SatinAlmaDurum(int index)
    {
        _ItemBilgileri[index].SatinAlmaDurumu = true;

        _BellekYonetimi.VeriKaydet_Int("Puan", _BellekYonetimi.VeriOku_Int("Puan") -
        _ItemBilgileri[index].Puan);

        TextObjeleri[5].text = satinAlmaText; ;
        islemButonlari[0].interactable = false;
        islemButonlari[1].interactable = true;
        PuanText.text = _BellekYonetimi.VeriOku_Int("Puan").ToString();
    }
}
