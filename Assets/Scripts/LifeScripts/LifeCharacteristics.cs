using UnityEngine;

//Script to provide characterisitcs as scriptable object
//Characteristics to define life types, predator and prey

namespace Assets.Scripts.Inheritance
{
    [CreateAssetMenu(menuName = "Life/LifeCharacteristics")]
    public class LifeCharacteristics : ScriptableObject
    {
        [Header ("Characteristics Calibration")]
        [Range(1f, 100f)]
        public float DriveFactor = 10f;
        [Range(1f, 100f)]
        public float AgentMaxSpeed = 5f;
        [Range(0f, 100f)]
        public float NeighborRadius = 1.5f;
        [Range(0f, 1f)]
        public float AvoidanceRadiusMultiplier = 0.5f;
        [Range(0f, 1f)]
        public float SmallRadiusMultiplier = 0.2f;
        private float avoidanceRadius;
        private float smallRadius;

        public float AvoidanceRadius => avoidanceRadius;

        public float SmallRadius => smallRadius;

        public void Init()
        {
            avoidanceRadius = NeighborRadius * AvoidanceRadiusMultiplier;
            smallRadius = NeighborRadius * SmallRadiusMultiplier;
        }
    }
}