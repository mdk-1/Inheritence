using System.Collections.Generic;
using UnityEngine;

//script for agents to avoid objects

namespace Assets.Scripts.Inheritance.Behaviours
{
    [CreateAssetMenu(menuName = "Life/Behavior/Avoidance")]
    public class AvoidanceBehavior : LifeBehaviour
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            // getting average 
            Vector2 avoidanceMove = Vector2.zero;
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(context, life);
            int numAvoid = 0;
            // instead of context, using filter
            foreach (var item in filteredContext)
            {
                if (Vector2.Distance(item.position, life.transform.position) <= life.Characteristics.AvoidanceRadius)
                {
                    numAvoid++;
                    avoidanceMove += (Vector2)(life.transform.position - item.position);
                }
            }

            if (numAvoid > 0)
            {
                avoidanceMove /= numAvoid;
            }

            return avoidanceMove;
        }
    }
}