using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inheritance.Filters;
using UnityEngine;

//script to add hiding behavior behind obstacles if agent from another flock within distance

namespace Assets.Scripts.Inheritance.Behaviours
{
    [CreateAssetMenu(menuName = "Life/Behavior/Hide")]
    public class HideBehaviour : LifeBehaviour
    {
        [Header("Obstacle Calibration")]
        [SerializeField]
        private ContextFilterForLife obstaclesFilter;
        [SerializeField]
        private float hideBehindObstacleDistance = 2f;

        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            // hide from list
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(context, life);

            // hide behind list
            List<Transform> obstacleContext = Filter == null ? context : obstaclesFilter.Filter(context, life);

            if (!filteredContext.Any() || !obstacleContext.Any())
            {
                return Vector2.zero;
            }

            // find nearest obstacle
            float nearestDistance = float.MaxValue;
            Transform nearestObstacle = null;

            foreach (Transform item in obstacleContext)
            {
                float distance = Vector2.Distance(item.position, life.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObstacle = item;
                }
            }

            // find best hiding spot
            Vector2 hidePosition = Vector2.zero;

            foreach (Transform item in filteredContext)
            {
                Vector2 obstacleDirection = nearestObstacle.position - item.position;
                obstacleDirection = obstacleDirection.normalized * hideBehindObstacleDistance;
                hidePosition += (Vector2)nearestObstacle.position + obstacleDirection;
            }

            hidePosition /= filteredContext.Count;

            // debug tool to see where AI is hiding
            Debug.DrawRay(hidePosition, Vector2.up * 1f);

            return hidePosition - (Vector2)life.transform.position;
        }
    }
}