using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items : MonoBehaviour
{
    
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float volume = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        float density = 1.0f; 
        rb.mass = volume * density;

    }


    void Update()
    {
        
    }
}
