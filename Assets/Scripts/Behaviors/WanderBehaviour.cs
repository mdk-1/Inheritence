using System.Collections.Generic;
using UnityEngine;

//script to generate a random path for agent to wander

namespace Assets.Scripts.Inheritance.Behaviours
{
    [CreateAssetMenu(menuName = "Life/Behavior/Wander")]
    public class WanderBehaviour : LifeBehaviour
    {
        private Path path = null;
        private int currentWaypoint = 0;
        private Vector2 waypointDirection = Vector2.zero;

        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (path == null)
            {
                FindPath(life, context);
            }

            return FollowPath(life);
        }

        //method to check alignment on path
        public bool InRadius(Life agent)
        {
            waypointDirection = (Vector2)path.waypoints[currentWaypoint].position - (Vector2)agent.transform.position;

            return waypointDirection.magnitude < path.radius;
        }

        //method for agent to follow found path
        public Vector2 FollowPath(Life life)
        {
            if (path == null)
            {
                return Vector2.zero;
            }

            if (InRadius(life))
            {
                currentWaypoint++; // go to next waypoint

                if (currentWaypoint >= path.waypoints.Count)
                {
                    // if at last waypoint
                    currentWaypoint = 0; // reset waypoint
                }

                return Vector2.zero; // dont have to move if at waypoint already
            }

            return waypointDirection; // return class variable
        }

        //method for agent to find path
        public void FindPath(Life life, List<Transform> context)
        {
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(context, life);

            if (filteredContext.Count == 0)
            {
                return;
            }

            int randomPath = Random.Range(0, filteredContext.Count);
            path = filteredContext[randomPath].GetComponentInParent<Path>();
        }
    }
}