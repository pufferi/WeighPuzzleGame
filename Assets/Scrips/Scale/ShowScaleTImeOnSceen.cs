using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowScaleTImeOnSceen : MonoBehaviour
{
    public TextMeshProUGUI t;
    void ShowTimesOnSceen()
    {
        int times=ScaleTimesController.Instance.scaleTimes;
        t.text= "scale times : "+times.ToString();
    }

    private void Update()
    {
        ShowTimesOnSceen();
    }

}
