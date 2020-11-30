using UnityEngine;

//script to define prefab & inital counts of prey/predtor objects to be spawned

namespace Assets.Scripts.Inheritance
{
    public class LifeFabric : MonoBehaviour
    {
        [Header("Life Calibration")]
        [SerializeField]
        private GameObject predatorPrefab = null;
        [SerializeField]
        private GameObject preyPrefab = null;
        [SerializeField]
        [Range(1, 100)]
        private int predatorCount = 2;
        [SerializeField]
        [Range(1, 100)]
        private int preyCount = 5;

        // Loop through initial predator/prey count and instantiate within random radius
        // add name for id
        private void Start()
        {
            if (predatorPrefab != null)
            {
                for (int i = 0; i < predatorCount; i++)
                {
                    GameObject predator = Instantiate(predatorPrefab, Random.insideUnitCircle * predatorCount * 0.2f, Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)), transform);
                    predator.name = "Predator " + i;
                }
            }

            if (preyPrefab != null)
            {
                for (int i = 0; i < preyCount; i++)
                {
                    GameObject predator = Instantiate(preyPrefab, Random.insideUnitCircle * preyCount * 0.2f, Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)), transform);
                    predator.name = "Prey " + i;
                }
            }
        }
    }
}