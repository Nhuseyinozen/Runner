using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Huseyin;
using TMPro;

public class Level_Manager : MonoBehaviour
{
    public GameObject[] Butonlar;
    public int Level;
    public Sprite KilitButon;
    public AudioSource butonSes;


    BellekYonetimi LevelBellekYonetimi = new BellekYonetimi();
    VeriKontrol _VeriKontrol = new VeriKontrol();
   

    public List<DilVerileriAnaObje> _DilverileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _OkunacakDilVerileri = new List<DilVerileriAnaObje>();
    public Text[] TextObjeleri;


    [Header("---Yükleme Ekraný Ýþlemleri---")]
    public GameObject YukleniyorPanel;
    public Slider YukleniyorSlider;
    public TextMeshProUGUI YukleniyorText;

    void Start()
    {
       
        _VeriKontrol.DilVerileriLoad();
        _OkunacakDilVerileri = _VeriKontrol.DilVerileriListeyeAktar();
        _DilverileriAnaObje.Add(_OkunacakDilVerileri[2]);
        DilKontrol();

        LevelBellekYonetimi.VeriOku_Float("MenuFx");

        int MevcutLevel = LevelBellekYonetimi.VeriOku_Int("SonLevel") - 4;

        int index = 1;

        for (int i = 0; i < Butonlar.Length; i++)
        {
            if (index <= MevcutLevel)
            {
                Butonlar[i].GetComponentInChildren<Text>().text = index.ToString();
                int SahneIndex = index + 4;
                Butonlar[i].GetComponent<Button>().onClick.AddListener(delegate { SahneYukle(SahneIndex); });

            }
            else
            {
                Butonlar[i].GetComponent<Image>().sprite = KilitButon;
                Butonlar[i].GetComponent<Button>().enabled = false;

            }
            index++;
        }
       
    }
    public void DilKontrol()
    {
        if (LevelBellekYonetimi.VeriOku_String("Dil") == "TR")
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
    public void SahneYukle(int _SahneIndex)
    {
        butonSes.Play();
        StartCoroutine(SahneYukleniyor(_SahneIndex));
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
    public void GeriDon()
    {
        butonSes.Play();
        SceneManager.LoadScene(0);
    }
}
