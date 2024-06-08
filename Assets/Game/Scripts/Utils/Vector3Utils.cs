using UnityEngine;

namespace Game.Scripts.Utils
{
    public static class Vector3Utils
    {
        public static Vector3 GetRandomVector3(float minValue, float maxValue)
        {
            return new Vector3(
                Random.Range(-minValue, maxValue),
                Random.Range(minValue, maxValue),
                Random.Range(minValue, maxValue)
            );
        }
    }
}