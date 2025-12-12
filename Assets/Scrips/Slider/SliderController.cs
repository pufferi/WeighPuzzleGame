using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
    public Slider slider; 
    public GameObject weight;

   
    private void OnScaleChanged(float value)
    {
        if (weight != null)
        {
            
            weight.transform.localScale = Vector3.one * value;
            
        }
        else
        {
            Debug.LogError("Weight is null .");
        }
    }

 


    private void Start()
    {
        //slider.value = 0.7756f;
        slider.onValueChanged.AddListener(OnScaleChanged);
    }


}

