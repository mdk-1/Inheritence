using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to steer the cohesion between agents together

namespace Assets.Scripts.Inheritance.Behaviours
{
    [CreateAssetMenu(menuName = "Life/Behavior/Steered Cohesion")]
    public class SteeredCohesionBehavior : LifeBehaviour
    {
        [Header("Cohesion Calibration")]
        public float AgentSmoothTime = 0.5f;
        private Vector2 currentVelocity;

        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            // if no neighbours, return no adjustment
            if (!context.Any())
            {
                return Vector2.zero;
            }

            // all add points together and average
            Vector2 cohesionMove = Vector2.zero;

            // if (filter == null) { filteredContext = context} else {filter.Filter(agent,context)}
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(context, life);
            int count = 0;

            // instead of context
            foreach (Transform item in filteredContext)
            {
                if (Vector2.Distance(item.position, life.transform.position) <= life.Characteristics.SmallRadius)
                {
                    cohesionMove += (Vector2)item.position;
                    count++;
                }
            }

            if (count != 0)
            {
                cohesionMove /= count;
            }

            // create offset from agent position
            cohesionMove -= (Vector2)life.transform.position;
            cohesionMove = Vector2.SmoothDamp(life.transform.up, cohesionMove, ref currentVelocity, AgentSmoothTime);
            return cohesionMove;
        }
    }
}