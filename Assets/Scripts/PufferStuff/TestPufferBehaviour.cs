using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestPufferBehaviour : MonoBehaviour
    {
        // Start is called before the first frame update

        PufferBehaviour pb;

        public bool inflate = false;
        public bool splash = false;
        public bool stopSplash = false;
        public bool deflate = false;

        public bool changeColor = false;
        public bool resetColor = false;

        void Start()
        {
            pb = GetComponent<PufferBehaviour>();
        }

        // Update is called once per frame
        void Update()
        {
            if (inflate)
            {
                inflate = false;
                pb.PufferInflate(3f);

            }

            if (splash)
            {
                splash = false;
                pb.PufferSplash();
            }

            if (deflate)
            {
                deflate = false;
                pb.PufferDeflate(2);
            }
            if(stopSplash)
            {
                stopSplash = false;
                pb.PufferStopSplash();
            }
            if(changeColor)
            {
                changeColor = false;
                pb.PufferChangeColor(Color.yellow);
            }

            if(resetColor)
            {
                resetColor = false;
                pb.PufferResetColor();
            }
        }

    }
}