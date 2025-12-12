using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject fish;
    void Start()
    {
       
        Instantiate(fish);
        Instantiate(fish);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
