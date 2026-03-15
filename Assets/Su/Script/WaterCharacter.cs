using UnityEngine;

public class WaterCharacter : MonoBehaviour
{
    // Karakterin ilk doğduğu yeri tutacağımız değişken
    private Vector3 startPosition; 
    private CharacterController controller;

    void Start()
    {
        // Oyun başladığında karakterin olduğu yeri başlangıç pozisyonu olarak kaydet
        startPosition = transform.position;
        
        // Karakterin üzerindeki CharacterController bileşenini al
        controller = GetComponent<CharacterController>();
    }

    // Karakter "Is Trigger" olan bir objeye değdiğinde bu fonksiyon çalışır
    void OnTriggerEnter(Collider other)
    {
        // Eğer değdiğimiz objenin etiketi (Tag) "Lava" ise
        if (other.CompareTag("Lava"))
        {
            DieAndRespawn();
        }
    }

    void DieAndRespawn()
    {
        // CharacterController aktifken pozisyon değiştirmek bazen algılanmayabilir.
        // Bu yüzden ışınlanma işleminden hemen önce kapatıp, ışınladıktan sonra tekrar açıyoruz.
        controller.enabled = false;
        
        // Karakteri başlangıç noktasına geri gönder
        transform.position = startPosition;
        
        controller.enabled = true;

        Debug.Log("Su karakteri lava düştü ve başlangıç noktasına döndü!");
    }
}