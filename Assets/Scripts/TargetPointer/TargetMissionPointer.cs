using UnityEngine;

namespace SpaceShooter
{
    public class TargetMissionPointer : MonoBehaviour
    {
        private void Start()
        {
            PointerManager.Instance.AddToListTargetMission(this);

        }

        private void Destroy()
        {
            PointerManager.Instance.RemoveToListTargetMission(this);
        }
    }
}

