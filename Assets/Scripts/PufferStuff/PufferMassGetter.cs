using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PufferMassGetter : MonoBehaviour
{
    [SerializeField]
    private GameObject parent_fishes;

    void Start()
    {
    }

    void Update()
    {
        
    }


    public List<int> GetAllPufferMass()
    {
        List<int> realPufferMass = new List<int>();

        WeightItemComponent[] pufferItems = parent_fishes.GetComponentsInChildren<WeightItemComponent>();
        foreach (WeightItemComponent item in pufferItems)
        {
            realPufferMass.Add(item.realMass);
        }
        return realPufferMass;
    }
}
