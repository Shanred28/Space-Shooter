using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_Prefab;

        [SerializeField] private TMP_Text m_ShipName;
        [SerializeField] private TMP_Text m_Hitpoint;
        [SerializeField] private TMP_Text m_Speed;
        [SerializeField] private TMP_Text m_Agility;
        
        [SerializeField] private Image m_Preview;

        private void Start()
        {
            if (m_Prefab != null)
            {
                m_ShipName.text = m_Prefab.Nicname;
                m_Hitpoint.text = "HP: " + m_Prefab?.HitPoints.ToString();
                m_Speed.text = "Speed: " + m_Prefab?.MaxLinearVelocity.ToString();
                m_Agility.text = "Agility: " + m_Prefab?.MaxAngularVelocity.ToString();

                m_Preview.sprite = m_Prefab.PreviewImage;
            }
        }

        public void OnSelectShip()
        { 
            LevelSequenceController.PlayerShip = m_Prefab;
            MainMenuController.Instance.gameObject.SetActive(true);
        }

    }
}

