using UnityEngine;

//script to create Life derived Predator class
//override start and update methods with base keyword

namespace Assets.Scripts.Inheritance
{
    public class Predator : Life
    {
        public new void Start()
        {
            base.Start();
        }

        public new void Update()
        {
            base.Update();
        }
    }
}