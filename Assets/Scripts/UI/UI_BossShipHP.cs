using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UI_BossShipHP : MonoBehaviour
    {

        [SerializeField] private Image m_ImageBarHp;
        [SerializeField] private Text m_TextHp;

        [SerializeField] private SpaceShip m_BossShip;

        private void Update()
        {
            m_ImageBarHp.fillAmount = (float) m_BossShip.CurrentHitPoints / (float) m_BossShip.HitPoints;
            m_TextHp.text = m_BossShip.CurrentHitPoints.ToString() + " / " + m_BossShip.HitPoints.ToString();
        }

    }
}


