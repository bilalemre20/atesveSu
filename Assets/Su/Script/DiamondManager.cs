using UnityEngine;
using TMPro;

public class DiamondManager : MonoBehaviour
{
    [Header("UI Referanslari")]
    public TextMeshProUGUI fireText;  // Ateş elması yazısı
    public TextMeshProUGUI waterText; // Su elması yazısı
    
    private int fireCount = 0;
    private int waterCount = 0;

    void Start()
    {
        UpdateUI();
    }

    // Elmas toplandığında türüne göre buraya haber verilecek
    public void AddDiamond(DiamondCollect.DiamondType type)
    {
        if (type == DiamondCollect.DiamondType.Fire)
        {
            fireCount++;
        }
        else if (type == DiamondCollect.DiamondType.Water)
        {
            waterCount++;
        }
        
        UpdateUI();
    }

    void UpdateUI()
    {
        if(fireText != null) fireText.text = "Ateş: " + fireCount;
        if(waterText != null) waterText.text = "Su: " + waterCount;
    }
}