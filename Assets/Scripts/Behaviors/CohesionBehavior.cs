using System.Collections.Generic;
using UnityEngine;

//script to align flocks movements in cohesion

namespace Assets.Scripts.Inheritance.Behaviours
{
    [CreateAssetMenu(menuName = "Life/Behavior/Cohesion")]
    public class CohesionBehavior : LifeBehaviour
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            // add all points together and get average
            Vector2 cohesionMove = Vector2.zero;
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(context, life);
            int count = 0;

            // instead of context, using filter
            foreach (var item in filteredContext)
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

                // create offset from agent position
                // direction from a to b = b - a
                cohesionMove -= (Vector2)life.transform.position;
            }

            return cohesionMove;
        }
    }
}