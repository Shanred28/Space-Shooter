using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class PointerIcon : MonoBehaviour
    {
        [SerializeField] Image m_Image;

        private bool IsShow = true;

        private void Awake()
        {
            m_Image.enabled = false;
            IsShow = false;
        }

        public void SetIconPosition(Vector3 position, Quaternion rotation)
        { 
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Show()
        {
            if (IsShow) return;
            IsShow=true;
            StopAllCoroutines();
            StartCoroutine(ShowProcess());
        }

        public void Hide()
        { 
            if(!IsShow) return;
            IsShow = false;

            StopAllCoroutines();
            StartCoroutine(HideProcess());
        }

        IEnumerator ShowProcess()
        { 
            m_Image.enabled=true;
            transform.localScale = Vector3.zero;

            for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
            { 
                transform.localScale = Vector3.one * t;
                yield return null;
            }
            transform.localScale = Vector3.one;
        }

        IEnumerator HideProcess() 
        {
            for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
            { 
                transform.localScale = Vector3.one * (1f - t);
                yield return null;
            }
            m_Image.enabled = false;
        }
    }
}

