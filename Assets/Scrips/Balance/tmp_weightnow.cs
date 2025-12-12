using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class tmp_weightnow : MonoBehaviour
{
    public TextMeshProUGUI weightnow;

    public Rigidbody rb;
    

    private void Update()
    {
        weightnow.text=rb.mass.ToString();
    }

}
