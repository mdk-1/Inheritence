using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//life class will provide characteristics for classes that inherit from it
//preadator and prey classes inherit based on life model

namespace Assets.Scripts.Inheritance
{
    //enum to store life types, predator and prey
    public enum LifeType
    {
        Predator,

        Prey,
    }

    // GameObject must have Collider2D attached
    [RequireComponent(typeof(Collider2D))]
    // GameObject must have characteristic behaviours attached
    [RequireComponent(typeof(LifeCharacteristics))]
    public class Life : MonoBehaviour
    {
        [SerializeField]
        public LifeType Type = LifeType.Prey;
        [SerializeField]
        public LifeCharacteristics Characteristics;

        private new Collider2D collider;
        private Animator animator;

        //reference collider/animation
        //initialize characteristics
        protected void Start()
        {
            collider = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();

            if (Characteristics != null)
            {
                Characteristics.Init();
            }
        }
        // handle transitions within animator if distance conditions are met
        protected void Update()
        {
            if (animator != null)
            {
                animator.SetFloat("Minimal distance to same type life", GetMinimalDistanceToLifeSameType());
                animator.SetFloat("Minimal distance to other type life", GetMinimalDistanceToLifeOtherType());
            }
        }
        //method to update movement position over time
        public void UpdatePosition(Vector2 velocity)
        {
            if (velocity.sqrMagnitude > Characteristics.AgentMaxSpeed)
            {
                velocity = velocity.normalized * Characteristics.AgentMaxSpeed;
            }

            transform.up = (Vector3)velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
        //method to create list of all nearby object positions excluding agents 
        //return object collider to list in radius
        public List<Transform> GetNearbyObjectsByRadius(float radius)
        {
            return Physics2D.OverlapCircleAll(transform.position, radius).Where(x => x != collider).Select(x => x.transform).ToList();
        }
        //method to calculate distance to agents assgined to same flock
        private float GetMinimalDistanceToLifeSameType()
        {
            //get all life objects by raidus, add to list
            List<Transform> nearbyObjects = GetNearbyObjectsByRadius(Characteristics.NeighborRadius).Where(x => x.GetComponent<Life>() != null && x.GetComponent<Life>().Type == Type).ToList();
            //if list contains life objects return position
            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }
            //return radius
            return Characteristics.NeighborRadius;
        }
        //method to calculate distance to agents assgined to different flock
        private float GetMinimalDistanceToLifeOtherType()
        {
            //create list of objects not equal to same type
            List<Transform> nearbyObjects = GetNearbyObjectsByRadius(Characteristics.NeighborRadius).Where(x => x.GetComponent<Life>() != null && x.GetComponent<Life>().Type != Type).ToList();

            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }

            return Characteristics.NeighborRadius;
        }
    }
}