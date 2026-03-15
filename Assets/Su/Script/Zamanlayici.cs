using UnityEngine;
using TMPro; // TextMeshPro bileşenine erişmek için

public class Zamanlayici : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Ekrandaki yazı objesini buraya sürükleyeceğiz
    private float gecenSure = 0f;

    void Update()
    {
        // Her karede geçen süreyi ekle
        gecenSure += Time.deltaTime;

        // Süreyi dakika ve saniye cinsinden hesapla
        int dakika = Mathf.FloorToInt(gecenSure / 60);
        int saniye = Mathf.FloorToInt(gecenSure % 60);

        // Yazıyı 00:00 formatında güncelle
        timerText.text = string.Format("{0:00}:{1:00}", dakika, saniye);
    }
}