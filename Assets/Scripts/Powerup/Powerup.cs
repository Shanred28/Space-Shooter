using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {
        public UnityEvent PickUp;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            if (ship != null && Player.Instance.ActiveShip)
            {
                OnPickedUp(ship);
                PickUp.Invoke();

                Destroy(gameObject);
            }
        }

        protected abstract void OnPickedUp(SpaceShip ship);
        

    }
}

