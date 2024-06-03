using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;



namespace Huseyin
{
    public class Kutuphane
    {
        public static void Carpma(int GelenSayi, List<GameObject> KarakterHavuzu, Vector3 OlusmaPoz, List<GameObject> OlusmaEfektleri)
        {


            int DonguSayisi = (GameManager.AnlikKarakterSayisi * GelenSayi) - GameManager.AnlikKarakterSayisi;
            //  Ornek                10                            2                  10  =  10 kere dönecek.
            //  Ornek2               6                             3                  6   =  12 kere dönecek.

            int olusucakKarakterSayisi = 0;
            foreach (var item in KarakterHavuzu)
            {

                if (olusucakKarakterSayisi < DonguSayisi) // Çarpma iþlemi döngüyle yapýldý.
                {

                    if (!item.activeInHierarchy) // aktif deðilse bu iþlemi yap ! , aktif olmayan ilk indeksdeki objeyi iþleme sokar;
                    {

                        foreach (var item2 in OlusmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = OlusmaPoz;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }



                        item.transform.position = OlusmaPoz; // onTrigger islemindeki objenin pozisyonu.
                        item.SetActive(true);
                        olusucakKarakterSayisi++;
                    }
                } // Eger olusucakKarakterSayisi , donguSayisina geldiyse bitir.
                else
                {
                    olusucakKarakterSayisi = 0;
                    break;
                }
            }
            // Mevcut karakter sayisini gelen sayiyla yani carpýlacak sayýyla carp .
            GameManager.AnlikKarakterSayisi *= GelenSayi;


        }

        public static void Toplama(int GelenSayi, List<GameObject> KarakterHavuzu, Vector3 OlusmaPoz, List<GameObject> OlusmaEfektleri)
        {
            int donguSayisi = 0;
            foreach (var item in KarakterHavuzu)
            {

                if (donguSayisi < GelenSayi)
                {

                    if (!item.activeInHierarchy) // aktif deðilse bu iþlemi yap ! , aktif olmayan ilk indeksdeki objeyi iþleme sokar;
                    {
                        foreach (var item2 in OlusmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = OlusmaPoz;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = OlusmaPoz;
                        item.SetActive(true);
                        donguSayisi++;
                    }
                }
                else
                {   // Eðer olusucakKarakterSayisi mevcut karakter sayýsýna eþitse döngüyü bitir;
                    donguSayisi = 0;
                    break;
                }
            }

            GameManager.AnlikKarakterSayisi = GameManager.AnlikKarakterSayisi + GelenSayi;


        }

        public static void Cikartma(int GelenSayi, List<GameObject> KarakterHavuzu, List<GameObject> YokOlmaEfektleri)
        {


            if (GameManager.AnlikKarakterSayisi < GelenSayi)
            {

                foreach (var item in KarakterHavuzu)
                {

                    foreach (var item2 in YokOlmaEfektleri)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                            item2.SetActive(true);
                            item2.transform.position = yeniPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {

                int karakterEksilt = GelenSayi;
                foreach (var item in KarakterHavuzu)
                {
                    foreach (var item2 in YokOlmaEfektleri)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                            item2.SetActive(true);
                            item2.transform.position = yeniPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }

                    if (karakterEksilt > 0)
                    {

                        if (item.activeInHierarchy) // aktif deðilse bu iþlemi yap ! , aktif olmayan ilk indeksdeki objeyi iþleme sokar;
                        {
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            karakterEksilt--;
                        }
                    }
                    else
                    {   // Eðer olusucakKarakterSayisi mevcut karakter sayýsýna eþitse döngüyü bitir;
                        karakterEksilt = 0;
                        break;
                    }
                }
                GameManager.AnlikKarakterSayisi -= GelenSayi;
            }




        }

        public static void Bolme(int GelenSayi, List<GameObject> KarakterHavuzu, List<GameObject> YokOlmaEfektleri)
        {
            int karakterBol = GameManager.AnlikKarakterSayisi / GelenSayi;

            foreach (var item in KarakterHavuzu)
            {
                foreach (var item2 in YokOlmaEfektleri)
                {
                    if (!item2.activeInHierarchy)
                    {
                        Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                        item2.SetActive(true);
                        item2.transform.position = yeniPoz;
                        item2.GetComponent<ParticleSystem>().Play();
                        item2.GetComponent<AudioSource>().Play();
                        break;
                    }
                }

                if (karakterBol >= 1)
                {
                    if (item.activeInHierarchy) // aktif deðilse bu iþlemi yap ! , aktif olmayan ilk indeksdeki objeyi iþleme sokar;
                    {
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                        karakterBol--;
                    }
                }
                else
                {   // Eðer olusucakKarakterSayisi mevcut karakter sayýsýna eþitse döngüyü bitir;
                    karakterBol = 0;
                    break;
                }

            }
            if (GelenSayi == 2)
            {
                if (GameManager.AnlikKarakterSayisi % GelenSayi == 0)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 1)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi++;
                }
            }

            if (GelenSayi == 3)
            {
                if (GameManager.AnlikKarakterSayisi % GelenSayi == 0)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 1)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi += 2;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 2)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi += 4;
                    Debug.Log(GameManager.AnlikKarakterSayisi);
                }
            }
        }

    }

    public class BellekYonetimi
    {

        public void VeriKaydet_String(string Key, string Value)
        {
            PlayerPrefs.SetString(Key, Value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_Int(string Key, int Value)
        {
            PlayerPrefs.SetInt(Key, Value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_Float(string Key, float Value)
        {
            PlayerPrefs.SetFloat(Key, Value);
            PlayerPrefs.Save();
        }


        public string VeriOku_String(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int VeriOku_Int(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float VeriOku_Float(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void KontrolEtVeTanimla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 5);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifTema", -1);
                PlayerPrefs.SetFloat("MenuSes", 1);
                PlayerPrefs.SetFloat("MenuFx", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);
                PlayerPrefs.SetString("Dil", "TR");


            }
        }
    }

    //public class Verilerimiz
    //{  
    //    public static List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri> (); // Sýnýftan ornek alýndý.
    //}



    [Serializable] // Dosya okuma ve kaydetme iþlemleri için serileþtirme yaptýk.
    public class ItemBilgileri
    {
        public int GrupIndex;
        public int ItemIndex;
        public string ItemAd;
        public int Puan;
        public bool SatinAlmaDurumu;

    }

    public class VeriKontrol
    {

        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd"); // Dosya oluþturma.
            bf.Serialize(file, _ItemBilgileri);
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd")) // Dosya varmý ?.
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _ItemIcLýste = (List<ItemBilgileri>)bf.Deserialize(file); // Dosya cozme.
                file.Close();
            }
        }

        public void IlkDosyaKaydetme(List<ItemBilgileri> _ItemBilgileri, List<DilVerileriAnaObje> _DilBilgileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd")) // Ilk kurulumda bu dosya yoksa calýsýr.
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd"); // Dosya oluþturma.
                bf.Serialize(file, _ItemBilgileri);
                file.Close();
            }

            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd")) // Ilk kurulumda bu dosya yoksa calýsýr.
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd"); // Dosya oluþturma.
                bf.Serialize(file, _DilBilgileri);
                file.Close();
            }
        }

        List<ItemBilgileri> _ItemIcLýste;
        public List<ItemBilgileri> ListeyeAktar()
        {
            return _ItemIcLýste;
        }

        //------------------------------------------------------------------------

        List<DilVerileriAnaObje> _DilVerileriIcListe;

        public void DilVerileriLoad()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd")) // Dosya varmý ?.
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                _DilVerileriIcListe = (List<DilVerileriAnaObje>)bf.Deserialize(file); // Dosya cozme.
                file.Close();
            }
        }
        public List<DilVerileriAnaObje> DilVerileriListeyeAktar()
        {
            return _DilVerileriIcListe;
        }
    }


    //-----DÝL YÖNETÝMÝ
    [Serializable]
    public class DilVerileriAnaObje
    {

        public List<DilVerileri_TR> _DýlVerileri_TR = new List<DilVerileri_TR>();
        public List<DilVerileri_TR> _DýlVerileri_EN = new List<DilVerileri_TR>();
    }
    [Serializable]
    public class DilVerileri_TR // ALT CLASS
    {
        public string kelime;
    }


    //-----REKLAM YÖNETÝMÝ

    //public class ReklamYonetimi
//    {
//        private InterstitialAd interstitial;

//        public void RequestInterstitial()
//        {
//            string adUnitId;


//#if UNITY_ANDROID
//            adUnitId = "ca-app-pub-3940256099942544/1033173712";
//#elif UNITY_IPHONE
//            adUnitId = "ca-app-pub-3940256099942544/4411468910"; 
//#else
//              adUnitId ="unexpected_platform";
//#endif

//            this.interstitial = new InterstitialAd(adUnitId);

//            //AdRequest request = new AdRequest.Builder().Build();
//        }

//    }



}


