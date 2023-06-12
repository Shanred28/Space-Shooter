using UnityEngine;

namespace SpaceShooter
{
    public class EnemyPointer : MonoBehaviour
    {
        [SerializeField] private SpaceShip spaceShip;

        private void Start () 
        {
            PointerManager.Instance.AddToListEnemy(this);
            spaceShip.EventOnDeath.AddListener(Destroy);

        }

        private void Destroy()
        {
            spaceShip.EventOnDeath.RemoveListener(Destroy);
            PointerManager.Instance.RemoveToListEnemy(this);
        }
    }
}

