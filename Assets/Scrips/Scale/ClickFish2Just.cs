using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickFish2Just : MonoBehaviour
{
    public string canvasName; 

    private Canvas targetCanvas;

    private void Start()
    {
        targetCanvas = GameObject.Find(canvasName)?.GetComponent<Canvas>();
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); 
        }
        else
        {
            Debug.LogError("not found.");
        }
    }

    private void OnMouseDown()
    {
        if (targetCanvas != null)
        {
            Debug.Log("oooo");
            targetCanvas.gameObject.SetActive(true); 
        }
    }
}
