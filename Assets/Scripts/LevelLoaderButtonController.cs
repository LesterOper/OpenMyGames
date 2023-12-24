using DefaultNamespace.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LevelLoaderButtonController : MonoBehaviour
    {
        [SerializeField] private Button nextLevelBtn;

        private void Start()
        {
            nextLevelBtn.onClick.AddListener(NextLevel);
        }

        private void NextLevel()
        {
            EventsInvoker.TriggerEvent(EventsKeys.LOAD_NEXT_LEVEL, null);
        }
    }
}