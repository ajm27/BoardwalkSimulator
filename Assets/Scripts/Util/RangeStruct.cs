using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RangeStruct
{
    public struct RangeI
    {
        public int min;
        public int max;

        public RangeI(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }

    public struct RangeF
    {
        public float min;
        public float max;

        public RangeF(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
