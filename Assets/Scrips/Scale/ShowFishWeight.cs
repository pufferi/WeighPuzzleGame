using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowFishWeight : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI weight;

    public Rigidbody rb;

    private void Start()
    {
        weight.text = string.Empty;
    }

    public void Showfishweight()
    {
        weight.text = rb.mass.ToString();
    }
}
