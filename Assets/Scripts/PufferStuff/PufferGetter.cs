using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PufferGetter : MonoBehaviour
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

    public List<Color> GetAllPufferColor()
    {
        List<Color> realPufferColors = new List<Color>();
        PufferBehaviour[] pufferItems = parent_fishes.GetComponentsInChildren<PufferBehaviour>();
        foreach (PufferBehaviour item in pufferItems)
        {
            realPufferColors.Add(item.GetPufferColor());
        }
        return realPufferColors;
    }
}
