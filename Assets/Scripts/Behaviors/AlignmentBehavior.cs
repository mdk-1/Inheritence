using System.Collections.Generic;
using UnityEngine;

//script to align agents in flock 

namespace Assets.Scripts.Inheritance.Behaviours
{
    [CreateAssetMenu(menuName = "Life/Behavior/Alignment")]
    public class AlignmentBehavior : LifeBehaviour
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (context.Count == 0)
            {
                // maintain same direction
                return life.transform.up;
            }

            // add all directions together and average
            Vector2 alignmentMove = Vector2.zero;
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(context, life);

            int count = 0;

            foreach (Transform item in filteredContext)
            {
                // instead of context, using filter
                if (Vector2.Distance(item.position, life.transform.position) <= life.Characteristics.SmallRadius)
                {
                    alignmentMove += (Vector2)item.transform.up;
                    count++;
                }
            }

            if (count != 0)
            {
                alignmentMove /= context.Count;
            }

            return alignmentMove;
        }
    }
}