using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightOnBalanceController : MonoBehaviour
{
    public static int fishWeight = 0;
    public static int weighWeight = 0;

    public weightTrigger fishplate;
    public weightTrigger weightplate;

    private static WeightOnBalanceController _instance;


    public static WeightOnBalanceController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WeightOnBalanceController>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("WeightOnBalanceController");
                    _instance = singletonObject.AddComponent<WeightOnBalanceController>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

   

    public int getFishWeight()
    {
        //
        return fishplate.all;
    }

    public int getWeighWeight()
    {
        
        return weightplate.all;
    }

}
