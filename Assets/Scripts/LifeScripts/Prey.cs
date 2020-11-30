using UnityEngine;

//script to create Life derived Prey class
//override start and update methods with base keyword

namespace Assets.Scripts.Inheritance
{
    public class Prey : Life
    {
        private new void Start()
        {
            base.Start();
        }

        private new void Update()
        {
            base.Update();
        }
    }
}