using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Inheritance.Filters;

//script to add context to life for predator and prey inheritance

namespace Assets.Scripts.Inheritance.Behaviours
{
    public abstract class LifeBehaviour : ScriptableObject
    {
        [SerializeField]
        public ContextFilterForLife Filter;

        //method to override the calcuation of the movement speed and position of agent
        public abstract Vector2 CalculateMoveSpeed(Life life, List<Transform> context);
    }
}