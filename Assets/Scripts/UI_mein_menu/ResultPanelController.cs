using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_NumKills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private Text m_Result;

        [SerializeField] private Text m_ButtomNextText;

        [SerializeField] private GameObject m_BonusScoreText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool seccess)
        { 
            gameObject.SetActive(true);
            m_Success = seccess;
            m_Result.text = seccess ? "Win" : "Lose";
            m_ButtomNextText.text = seccess ? "Next" : "Restart";

            m_NumKills.text = "Kills: " + levelResults.numkills.ToString();
            m_Score.text = "Score: " + levelResults.score.ToString();
            m_Time.text = "Time: " + levelResults.time.ToString();

            if(levelResults.IsBonusScore && seccess == true)
                m_BonusScoreText.SetActive(true);
            else
                m_BonusScoreText.SetActive(false);

            SaveLoadStatistic.Instance.SaveStatistic(levelResults.numkills, levelResults.score, levelResults.time);

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if (m_Success)
                LevelSequenceController.Instance.AdvanceLevel();
            else
                LevelSequenceController.Instance.RestartLevel();
        }
    }
}

