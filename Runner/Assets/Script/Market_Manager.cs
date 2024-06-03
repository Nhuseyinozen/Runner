using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Huseyin;
using TMPro;
using UnityEngine.SceneManagement;

public class Market_Manager : MonoBehaviour
{
    VeriKontrol _VeriKontrol = new VeriKontrol();
    BellekYonetimi _BellekYonetimi = new BellekYonetimi();

    public List<DilVerileriAnaObje> _DilverileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _OkunacakDilVerileri = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    void Start()
    {
        _VeriKontrol.DilVerileriLoad();
        _OkunacakDilVerileri = _VeriKontrol.DilVerileriListeyeAktar();
        _DilverileriAnaObje.Add(_OkunacakDilVerileri[3]);
        DilKontrol();
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

    public void GeriDon()
    {
        SceneManager.LoadScene(0);
    }
}
