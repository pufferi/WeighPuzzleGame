using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CulculateTheGrade : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public TextMeshProUGUI fish;
    public TextMeshProUGUI grade;
    private void WeightONConterweight()
    {
        float w=    MeasuringController.Instance.GetAllCounterWeight();
        counter.text = "the weight of counterweight is " + Math.Round(w, 0).ToString();
    }

    private void WeightONfishs()
    {
        float a = MeasuringController.Instance.GetAllPufferWeight();
        fish.text = "the weight of fishes is " + Math.Round(a, 0).ToString();
    }

    private void theGrade()
    {
        float f = MeasuringController.Instance.GetAllPufferWeight();
        float w = MeasuringController.Instance.GetAllCounterWeight();

        float r1 = f - w;
        float r2 = f + w;
        float r3=1-Math.Abs(r1)/r2;
        grade.text = "Grade : " + ((int)(r3 * 100)).ToString();
    }

    public void work()
    {
        WeightONConterweight();
        WeightONfishs();
        theGrade();
    }

}
