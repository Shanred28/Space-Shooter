using UnityEngine;

#if     UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public Vector2 GetRandominsideZone()
        { 
            return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * m_Radius;
        }

        private static Color GismaColor = new Color(0, 1, 0, 0.3f);

#if     UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = GismaColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}

