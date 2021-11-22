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
>>>>>>> Stashed changes


        public void Init(UnityAction startGame, UnityAction settings)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();

        }
    }
}
