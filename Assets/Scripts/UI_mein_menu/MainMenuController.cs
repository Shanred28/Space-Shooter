using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaceShip m_DefaultSpaceShip;
        [SerializeField] private GameObject m_EpisodeSelection;
        [SerializeField] private GameObject m_SelectionShip;
        [SerializeField] private GameObject m_StatisticGame;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip;
        }

        public void OnButtonStartNew()
        { 
            m_EpisodeSelection.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        { 
            m_SelectionShip.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtomFullGameStatistic()
        {
            m_StatisticGame.gameObject.SetActive(true);
            UI_AllStatisticPanel.Instance.ShowAllStatisticGame();
        }

        public void OnButtonExit()
        { 
            Application.Quit();
        }
    }
}
