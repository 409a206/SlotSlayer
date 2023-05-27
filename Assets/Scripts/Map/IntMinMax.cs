using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class FloatMinMax
    {
        public float min;
        public float max;
        public float GetValue() => Random.Range(min, max);
    }
    
    [System.Serializable]
    public class IntMinMax {
        public int min;
        public int max;
        public int GetValue() => Random.Range(min, max + 1);
    }
}