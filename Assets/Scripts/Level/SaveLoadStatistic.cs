using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SpaceShooter
{
    public class SaveLoadStatistic : SingletonBase<SaveLoadStatistic>
    {
        private int m_AllNummKills;
        public int AllNummKills => m_AllNummKills;
        private int m_AllScore;
        public int AllScore => m_AllScore;
        private int m_AllTime;

        public int AllTime => m_AllTime;

        private void Start()
        {
            LoadStatistic();           
        }
        public void SaveStatistic(int NumKills, int Score, int time)
        {
            
            m_AllNummKills +=  NumKills;
            m_AllScore +=  Score;
            m_AllTime += time;
            Debug.Log(Score);
            PlayerPrefs.SetInt("NumKills:AllNumkills", m_AllNummKills);
            PlayerPrefs.SetInt("Score:AllScore", m_AllScore);
            PlayerPrefs.SetInt("Time:AllTime", m_AllTime);

        }

        public void LoadStatistic() 
        {
            m_AllNummKills = PlayerPrefs.GetInt("NumKills:AllNumkills", 0);
            m_AllScore = PlayerPrefs.GetInt("Score:AllScore", 0);
            m_AllTime = PlayerPrefs.GetInt("Time:AllTime", 0);
        }
    }
}

