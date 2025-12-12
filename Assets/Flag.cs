using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{

    public bool fishInRange = false;
    public bool weightInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rightplate"))
        {
            fishInRange = true;
        }
        if (other.CompareTag("Leftplate"))
        {
            weightInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rightplate") )
        {
            fishInRange=false;
        }
        else if (other.CompareTag("Leftplate"))
        {
            weightInRange = false;
        }
    }
   
}
