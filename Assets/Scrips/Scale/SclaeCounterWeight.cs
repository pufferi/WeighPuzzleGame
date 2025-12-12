using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SclaeCounterWeight : MonoBehaviour
{
    public GameObject thing2Hide;

    public string TIP_NOSCALETIMES = "Waitwaitwait,you've ran out of the scaling chances.";
    public TextMeshProUGUI tips;

    private bool CheckifNOScaleTimes()
    {
        if (ScaleTimesController.Instance.scaleTimes <= 0)
        {
            thing2Hide.SetActive(false);
            tips.text = TIP_NOSCALETIMES;
            return true;
        }
        return false;
    }

    public void Scale()
    {
        if (CheckifNOScaleTimes()) return;
        tips.text=string.Empty;
        
        ScaleTimesController.Instance.subtractone();
    }

}
