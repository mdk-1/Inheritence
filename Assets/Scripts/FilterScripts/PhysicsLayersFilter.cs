using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to add objects on physcis layer to filter  

namespace Assets.Scripts.Inheritance.Filters
{
    [CreateAssetMenu(menuName = "Life/Filter/PhysicsLayers")]
    public class PhysicsLayersFilter : ContextFilterForLife
    {
        [SerializeField]
        private LayerMask mask;

        //method to use bit shift to determine layer and add to list
        public override List<Transform> Filter(List<Transform> original, Life life = null)
        {
            return original.Where(x => mask == (mask | (1 << x.gameObject.layer))).ToList();
        }
    }
}
