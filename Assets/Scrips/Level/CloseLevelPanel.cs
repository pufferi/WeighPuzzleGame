using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLevelPanel : MonoBehaviour
{
    public GameObject canvas;

    

    public void closethelevelpannel()
    {
        StartCoroutine(DelayedActionCoroutine());
    }

    private IEnumerator DelayedActionCoroutine()
    {
       
        yield return new WaitForSeconds(1);

        canvas.SetActive(false);

    }

}
