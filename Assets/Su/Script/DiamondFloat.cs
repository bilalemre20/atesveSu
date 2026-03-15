using UnityEngine;

public class DiamondFloat : MonoBehaviour
{
    public float speed = 2f;
    public float height = 0.25f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}