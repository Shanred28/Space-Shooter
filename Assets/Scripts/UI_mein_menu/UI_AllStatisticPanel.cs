using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UI_AllStatisticPanel : SingletonBase<UI_AllStatisticPanel>
    {
        [SerializeField] private Text m_NumKills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private GameObject m_PanelMenu;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowAllStatisticGame()
        {

            m_NumKills.text = "All Kills: " + SaveLoadStatistic.Instance.AllNummKills.ToString();
            m_Score.text = "All Score: " + SaveLoadStatistic.Instance.AllScore.ToString();
            m_Time.text = "All Time: " + SaveLoadStatistic.Instance.AllTime.ToString();

        }

        public void OnButtonBack()
        {
            gameObject.SetActive(false);
            m_PanelMenu.SetActive(true);
        }
    }
}

