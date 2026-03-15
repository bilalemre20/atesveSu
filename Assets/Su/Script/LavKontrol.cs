using UnityEngine;
using UnityEngine.SceneManagement; // Sahneyi yeniden yüklemek için

public class LavKontrol : MonoBehaviour
{
    // Inspector'da etiketi yanlış yazma ihtimaline karşı burayı string olarak tutuyoruz
    private string hedefEtiket = "WaterPlayer";

    private void OnTriggerEnter(Collider other)
    {
        // Temas eden objenin etiketi "WaterPlayer" mı?
        if (other.CompareTag(hedefEtiket))
        {
            Debug.Log("Su karakteri lava düştü!");
            YenidenBaslat();
        }
    }

    void YenidenBaslat()
    {
        // Mevcut sahne hangisiyse onu en baştan yükler
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}