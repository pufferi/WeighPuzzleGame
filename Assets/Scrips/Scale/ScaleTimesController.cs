using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTimesController : MonoBehaviour
{
    public static ScaleTimesController Instance { get; private set; }

    public int scaleTimes;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); 
    }

    public void addone()
    {
        scaleTimes++;
    }
    public void subtractone()
    {
        scaleTimes--;
    }
}
