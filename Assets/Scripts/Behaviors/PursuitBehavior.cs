using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to chase agents in another flock within distance

namespace Assets.Scripts.Inheritance.Behaviours
{
    [CreateAssetMenu(menuName = "Life/Behavior/Pursuit")]
    public class PursuitBehavior : LifeBehaviour
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            //create list of filtered agents
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(context, life);

            if (!filteredContext.Any())
            {
                return Vector2.zero;
            }

            Vector2 move = Vector2.zero;

            //for each filtered agent in list caluclate disance and move direction
            foreach (Transform item in filteredContext)
            {
                float distance = Vector2.Distance(item.position, life.transform.position);
                float disancePercent = distance / life.Characteristics.NeighborRadius;
                float inverseDistancePercent = 1 - disancePercent;
                float weight = inverseDistancePercent / filteredContext.Count;

                Vector2 direction = (item.position - life.transform.position) * weight;

                move += direction;
            }
            //return movement as direction
            return move;
        }
    }
}