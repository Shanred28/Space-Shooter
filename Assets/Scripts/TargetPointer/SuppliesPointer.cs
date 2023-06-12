using UnityEngine;

namespace SpaceShooter
{
    public class SuppliesPointer : MonoBehaviour
    {
        [SerializeField] private Powerup powerup;
        private void Start()
        {
            PointerManager.Instance.AddToListSupplies(this);
            powerup.PickUp.AddListener(Destroy);
        }

        private void Destroy()
        {
            powerup.PickUp.RemoveListener(Destroy);
            PointerManager.Instance.RemoveToListSupplies(this);
        }
    }
}
