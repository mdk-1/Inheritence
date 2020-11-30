using System.Collections.Generic;
using UnityEngine;

//abstract class as scriptable object for life filter - predator or prey

namespace Assets.Scripts.Inheritance.Filters
{
    public abstract class ContextFilterForLife : ScriptableObject
    {
        public abstract List<Transform> Filter(List<Transform> original, Life life = null);
    }
}