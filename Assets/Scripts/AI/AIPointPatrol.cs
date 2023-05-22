using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        [SerializeField] private float m_RadiusPointPatrol;
        public float RadiusPointPatrol => m_RadiusPointPatrol;

        [SerializeField] private List<Transform> m_PointPatrolRoute;
        public List<Transform> PointPatrolRoute => m_PointPatrolRoute;

        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
#endif
    }
}

