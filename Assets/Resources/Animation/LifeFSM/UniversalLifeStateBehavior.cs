using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inheritance;
using Assets.Scripts.Inheritance.Behaviours;
using UnityEngine;

//script to calibrate the use of flocking compisite behaviors on FSM in animator
//using unity state machine behaviour

namespace Assets.Resources.Animation.LifeFSM
{
    [CreateAssetMenu(menuName = "Life/StateBehaviors/UniversalStateBehavior")]
    public class UniversalLifeStateBehavior : StateMachineBehaviour
    {
        //class for behaviour groups
        [System.Serializable]
        public class BehaviorGroup
        {
            public LifeBehaviour Behavior;
            public float Weight;
        }
        //list of behaviour groups
        [SerializeField]
        public List<BehaviorGroup> Behaviors;
        //reference to life class
        private Life life;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            life = animator.gameObject.GetComponent<Life>();

            foreach (BehaviorGroup behaviorGroup in Behaviors)
            {
                if (behaviorGroup.Behavior != null)
                {
                    //create new instance of behavior for each state behavior
                    behaviorGroup.Behavior = Instantiate(behaviorGroup.Behavior);
                }
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //determine neighbours, weight and position and update local
            List<Transform> nearbyObjects = life.GetNearbyObjectsByRadius(life.Characteristics.NeighborRadius);
            float totalWeight = Behaviors.Sum(behaviorGroup => behaviorGroup.Weight);
            Vector2 newVelocity = Vector2.zero;

            //calcuate movement for each life agent based on characteristics
            foreach (BehaviorGroup behaviorGroup in Behaviors)
            {
                Vector2 partialVelocity = behaviorGroup.Behavior.CalculateMoveSpeed(life, nearbyObjects).normalized;
                partialVelocity *= life.Characteristics.DriveFactor;
                newVelocity += partialVelocity * (behaviorGroup.Weight / totalWeight);
            }
            //update position
            life.UpdatePosition(newVelocity);
        }
    }
}