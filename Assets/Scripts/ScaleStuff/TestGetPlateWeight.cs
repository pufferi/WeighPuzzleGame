using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Test
{

    public class TestGetPlateWeight : MonoBehaviour
    {

        public ScalePlateSensor leftup;
        public ScalePlateSensor rightup;
        public ScalePlateSensor leftb;
        public ScalePlateSensor rightb;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("leftup  " + leftup.TotalMass);
            Debug.Log("rightup  " + rightup.TotalMass);
            Debug.Log("leftb  " + leftb.TotalMass);
            Debug.Log("rightb  " + rightb.TotalMass);
            Debug.Log("----------------");
            Debug.Log("----------------");
            Debug.Log("----------------");
            Debug.Log("----------------");
            Debug.Log("----------------");
        }
    }
}