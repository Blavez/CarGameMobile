using Profile;
using Services.Ads.UnityAds;
using Services.IAP;
using Tool;
using UnityEngine;
using UnityEngine.Analytics;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private UnityAdsService _adsService;
        private IAPService _iapService;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService adsService, IAPService iapService)
        {
            _profilePlayer = profilePlayer;
            _adsService = adsService;
            _iapService = iapService;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, ShowRewarded, Buying);
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

        private void Buying()
        {
            _iapService.Initialized.AddListener(OnIapInitialized);
            Debug.Log("Buying");
            Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);
        }
           

        private void ShowRewarded()
        {
            Debug.Log("Rewarded");
            _adsService.Initialized.AddListener(_adsService.RewardedPlayer.Play);
        }
        private void OnAdsInitialized() => _adsService.RewardedPlayer.Play();
        private void OnIapInitialized() => _iapService.Buy("product_1");
    }
}
