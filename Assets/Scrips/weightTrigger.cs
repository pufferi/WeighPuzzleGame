using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class weightTrigger : MonoBehaviour
{

    public int all = 0;

    public GameObject[] items;

    public bool isFish=false;

    private void Update()
    {
        giveWeight();
    }

    public int giveWeight()
    {
        all = 0;
        for (int i = 0; i < items.Count(); i++)
        {
            if (isFish&&items[i].GetComponent<Flag>().fishInRange )
            {
                all += (int)items[i].GetComponent<Rigidbody>().mass;
            }
            else if (!isFish && items[i].GetComponent<Flag>().weightInRange)
            {
                all+=(int)(int)items[i].GetComponent<Rigidbody>().mass;
            }
        }
        return all;
    }


}
