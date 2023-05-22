using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Level limiter. It works together with the Level Boundary script.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var lb = LevelBoundary.Instance;
            float r = lb.Radius;

            if (transform.position.magnitude > r)
            {
                if (lb.LimitMode == LevelBoundary.Mode.Limit)
                { 
                    transform.position = transform.position.normalized * r;
                }

                if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
    }
}

