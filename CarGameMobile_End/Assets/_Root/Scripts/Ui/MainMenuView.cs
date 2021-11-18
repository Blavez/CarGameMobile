using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewarded;
        [SerializeField] private Button _buttonBuying;

        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewarded, UnityAction buying)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewarded.onClick.AddListener(rewarded);
            _buttonBuying.onClick.AddListener(buying);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewarded.onClick.RemoveAllListeners();
            _buttonBuying.onClick.RemoveAllListeners();
        }
    }
}
