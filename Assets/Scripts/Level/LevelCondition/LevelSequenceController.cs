using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";
        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public UnityEvent IsLevelFinish;
        

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        { 
            LastLevelResult = success;
            IsLevelFinish.Invoke();
            CalculateLevelStatistic();
            ResultPanelController.Instance.ShowResults(LevelStatistics, success);

        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

           ++ CurrentLevel;
            if (CurrentEpisode.Levels.Length <= CurrentLevel)
                SceneManager.LoadScene(MainMenuSceneNickname);
            else
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        private void CalculateLevelStatistic()
        {
            float timeBonus = LevelController.Instance.TimeBonusScore;
            if (LevelController.Instance.LevelTime < timeBonus)
            {
                LevelStatistics.score = Player.Instance.Score * 2;
                LevelStatistics.IsBonusScore = true;
            }
            else
            {
                LevelStatistics.score = Player.Instance.Score;
                LevelStatistics.IsBonusScore = false;
            }
                
            LevelStatistics.numkills = Player.Instance.NumKills;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
        }
    }
}
