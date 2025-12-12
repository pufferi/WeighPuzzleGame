using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weight : MonoBehaviour
{
    public Slider slider;
    public float baseweight;
  
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        float mass = slider.value*2*baseweight/100f;
        rb.mass = mass ;
    }
}
