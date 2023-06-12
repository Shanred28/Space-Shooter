using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private Collider2D m_TargetPosition;

        private bool m_Reached = false;

        private bool IsTargetPosition = false;

        bool ILevelCondition.IsCompleted
        { 
            get 
            {
                if (IsTargetPosition)
                {
                    m_Reached = true;
                    Debug.Log(m_Reached);
                    return m_Reached;
                }
                else
                {
                    m_Reached = false;
                    return m_Reached;
                }                  
            }                     
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.GetComponent<SpaceShip>() == Player.Instance.ActiveShip )
                IsTargetPosition = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.transform.root.GetComponent<SpaceShip>() == Player.Instance.ActiveShip)
                IsTargetPosition = false;
        }
    }
}

