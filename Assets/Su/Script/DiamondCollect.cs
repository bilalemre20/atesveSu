using UnityEngine;

public class DiamondCollect : MonoBehaviour
{
    public enum DiamondType { Fire, Water }
    public DiamondType diamondType;

    private void OnTriggerEnter(Collider other)
    {
        if (diamondType == DiamondType.Water && other.CompareTag("WaterPlayer"))
        {
            Collect();
        }

        if (diamondType == DiamondType.Fire && other.CompareTag("FirePlayer"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        // Unity 6 için güncellenmiş komut:
        DiamondManager manager = Object.FindFirstObjectByType<DiamondManager>();
        
        if (manager != null)
        {
            manager.AddDiamond(diamondType);
        }

        Destroy(gameObject);
    }
}