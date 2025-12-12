using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeasuringController : MonoBehaviour
{
    public float MEASURED_weight = 0;
    public float MEASURED_fish = 0;


    public Rigidbody[] fishes;
    public Rigidbody[] weights;

    private static MeasuringController _instance;


    public static MeasuringController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MeasuringController>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("MeasuringController");
                    _instance = singletonObject.AddComponent<MeasuringController>();
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



    public float GetFishWeight(Rigidbody f)
    {
        foreach (var r in fishes)
        {
            if(f==r) return r.mass;
        }
        Debug.Log("no this fish");
        return 0;
    }

    public float GetCounterWeight(Rigidbody c)
    {
        foreach(var r in weights)
        {
            if(c==r) return r.mass;
        }
        Debug.Log("no this weight");
        return 0;
    }

    public float GetAllPufferWeight()
    {
        MEASURED_fish = 0;
        foreach(var f in fishes)
        {
            //Debug.Log(f.mass);
            MEASURED_fish += f.mass;
        }
        return MEASURED_fish;
    }

    public float GetAllCounterWeight()
    {
        MEASURED_weight = 0;
        foreach( var w in weights)
        {
            //Debug.Log(w.mass);
            MEASURED_weight += w.mass;
        }
        return MEASURED_weight;
    }

}
