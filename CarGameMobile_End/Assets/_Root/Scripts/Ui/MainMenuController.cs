using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
<<<<<<< Updated upstream
            _view.Init(StartGame, Settings);
=======
            _view.Init(StartGame, Settings, ShowRewarded, Buying, Inventory);
>>>>>>> Stashed changes
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;
<<<<<<< Updated upstream
=======

        private void Buying()
        {
            _iapService.Initialized.AddListener(OnIapInitialized);
            Debug.Log("Buying");
            Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);
        }
           
        private void Inventory()
        {
            _profilePlayer.CurrentState.Value = GameState.Shed;
        }
        private void ShowRewarded()
        {
            Debug.Log("Rewarded");
            _adsService.Initialized.AddListener(_adsService.RewardedPlayer.Play);
        }
        private void OnAdsInitialized() => _adsService.RewardedPlayer.Play();
        private void OnIapInitialized() => _iapService.Buy("product_1");
>>>>>>> Stashed changes
    }
}
