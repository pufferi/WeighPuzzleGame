using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Test
{
    public class TestTimer : MonoBehaviour
    {
        public bool start = false;
        public bool stop = false;
        public bool reset = false;

        Timer t;


        void Start()
        {
            Debug.Log(Time.timeScale);
            t = GetComponent<Timer>();
        }

        void Update()
        {
            if(start)
            {
                t.StartTimer(10f);
                start = false;
            }

            if(stop)
            {
                t.StopTimer();
                stop = false;
            }

            if(reset)
            {
                t.ResetTimer(10f);
                reset = false;
            }
        }
    }
}
