using System;
using DefaultNamespace.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LevelLoaderButtonController : MonoBehaviour
    {
        [SerializeField] private Button nextLevelBtn;
        [SerializeField] private Button reloadLevelBtn;

        private void Start()
        {
            nextLevelBtn.onClick.AddListener(NextLevel);
        }

        private void NextLevel()
        {
            EventsInvoker.TriggerEvent(EventsKeys.LOAD_NEXT_LEVEL, null);
        }
        
        private void ReloadLevel()
        {
            EventsInvoker.TriggerEvent(EventsKeys.RELOAD_LEVEL, null);
        }
    }
}