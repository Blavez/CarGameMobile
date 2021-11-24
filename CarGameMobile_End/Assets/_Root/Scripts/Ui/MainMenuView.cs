using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
<<<<<<< Updated upstream
=======
        [SerializeField] private Button _buttonRewarded;
        [SerializeField] private Button _buttonBuying;
        [SerializeField] private Button _buttonInventory;
<<<<<<< Updated upstream
>>>>>>> Stashed changes


        public void Init(UnityAction startGame, UnityAction settings)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
=======

        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewarded, UnityAction buying, UnityAction inventory)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewarded.onClick.AddListener(rewarded);
            _buttonBuying.onClick.AddListener(buying);
            _buttonInventory.onClick.AddListener(inventory);
>>>>>>> Stashed changes
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
<<<<<<< Updated upstream

=======
            _buttonRewarded.onClick.RemoveAllListeners();
            _buttonBuying.onClick.RemoveAllListeners();
            _buttonInventory.onClick.RemoveAllListeners();
>>>>>>> Stashed changes
        }
    }
}
