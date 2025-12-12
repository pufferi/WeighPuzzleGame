using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScaleTheFish : MonoBehaviour
{
    private string TIP_NOSCALETIMES = "Waitwaitwait,you've ran out of the scaling chances.";

    private string TIP_MINWEIGHTOFFFISH = "The minimum weight of a pufferfish is 10, we don¡¯t pursue a slim figure.";

    public TextMeshProUGUI tips;

    public void scaleup()
    {
        if (CheckifNOScaleTimes()) return;
        Transform tf= GetComponent<Transform>();
        Rigidbody rb = GetComponent<Rigidbody>();
        tf.localScale *= 1.5f;
        rb.mass *= 2;

        ScaleTimesController.Instance.subtractone();
    }
    public void scaledown()
    {
        if (CheckifNOScaleTimes()) return;
        if (CheckifTheWeightisMin()) return;
        Transform tf = GetComponent<Transform>();
        Rigidbody rb= GetComponent<Rigidbody>();
        tf.localScale /= 1.5f;
        rb.mass /= 2;

        ScaleTimesController.Instance.subtractone();
    }

    private bool CheckifNOScaleTimes()
    {
        if (ScaleTimesController.Instance.scaleTimes <= 0)
        { 
            tips.text= TIP_NOSCALETIMES;
            return true; 
        }
        return false;
    }

    private bool CheckifTheWeightisMin()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.mass <= 10)
        {
            tips.text = TIP_MINWEIGHTOFFFISH;
            return true;
        }
        return false;
    }

}
