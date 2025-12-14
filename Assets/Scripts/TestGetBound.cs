using UnityEngine;

public class TestGetBound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;
    void Start()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f;
        Debug.Log($"Bounds center: {bounds.center}, size: {bounds.size}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
