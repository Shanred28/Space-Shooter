using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics
    {
        public int numkills;
        public int score;
        public int time;
        public bool IsBonusScore = false;

        public void Reset()
        {
            numkills = 0;
            score = 0;
            time = 0;
            IsBonusScore = false;
        }

    }
}
