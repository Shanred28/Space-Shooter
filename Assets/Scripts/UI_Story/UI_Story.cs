using UnityEngine;

namespace SpaceShooter
{
    public class UI_Story : MonoBehaviour
    {
        [SerializeField] private GameObject m_StoryMode;
        [SerializeField] private GameObject m_StoryList1;
        [SerializeField] private GameObject m_StoryList2;

        private int clickOnButton;

        private void Start () 
        {
            m_StoryList1.SetActive(true);
            m_StoryList2.SetActive(false);
            Time.timeScale = 0;
        }

        public void OnButtonNext()
        {
            ++clickOnButton;
            m_StoryList1.SetActive(false);
            m_StoryList2.SetActive(true);
            if (clickOnButton == 2)
            {
                Time.timeScale = 1f;
                m_StoryMode.SetActive(false);
            }

        }

    }
}

