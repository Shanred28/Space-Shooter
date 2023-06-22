using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UI_StatsPlayerHUD : MonoBehaviour
    {
        [SerializeField] private Text m_TextLifeShip;
        [SerializeField] private Text m_TextAmmo;
        [SerializeField] private Image m_ImageShipActive;
        [SerializeField] private Image m_ImageFilledHp;
        [SerializeField] private Image m_ImageFilledEnergy;
        [SerializeField] private Text m_TextHP;


        private void Start()
        {
            m_ImageShipActive.sprite = Player.Instance.ActiveShip.PreviewImage;
        }

        private void Update()
        {
            m_ImageFilledHp.fillAmount = (float)Player.Instance.ActiveShip.CurrentHitPoints / (float)Player.Instance.ActiveShip.HitPoints;
            m_TextHP.text = Player.Instance.ActiveShip.CurrentHitPoints.ToString()  + " / " + Player.Instance.ActiveShip.HitPoints.ToString();

            m_ImageFilledEnergy.fillAmount = (float)Player.Instance.ActiveShip.CurrentEnergy / (float)Player.Instance.ActiveShip.MaxEnergy;

            m_TextAmmo.text = "Ammo: " + Player.Instance.ActiveShip.CurrentAmmo.ToString();

            m_TextLifeShip.text = Player.Instance.LivesPlayer.ToString(); 

        }


    }
}

