using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LoadLevelViewController : MonoBehaviour
    {
        [SerializeField] private Image loadLevelBg;

        public void LoadLevelViewActivate(float delay, Action loadLevel)
        {
            loadLevelBg.gameObject.SetActive(true);
            loadLevelBg.DOFade(1, 0.5f).SetDelay(delay)
                .OnComplete(loadLevel.Invoke);
        }

        public void LoadLevelViewDeactivate(float delay)
        {
            loadLevelBg.DOFade(0, 0.5f).SetDelay(delay)
                .OnComplete(() => loadLevelBg.gameObject.SetActive(false));
        }
    }
}