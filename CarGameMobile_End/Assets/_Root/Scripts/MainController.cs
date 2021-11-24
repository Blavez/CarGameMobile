using Ui;
using Game;
using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;
using Features.Inventory;
using Features.Shed;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _settingsController;
    private ShedController _shedController;
    private InventoryController _inventoryController;
    private AnalyticsManager _analytics;
    private UnityAdsService _adsService;
    private IAPService _iapService;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analytics, UnityAdsService adsService, IAPService iapService)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _adsService = adsService;
        _iapService = iapService;
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();
        _inventoryController?.Dispose();
        _shedController?.Dispose();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsService, _iapService);
                _gameController?.Dispose();
                _settingsController?.Dispose();
                _inventoryController?.Dispose();
                _shedController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi,_profilePlayer);
                Debug.Log("GameController started");
                _analytics.SendEvent("GameController started");
                _mainMenuController?.Dispose();
                _inventoryController?.Dispose();
                _shedController?.Dispose();
                break;
            case GameState.Inventory:
                _inventoryController = new InventoryController(_placeForUi, _profilePlayer.Inventory);
                _mainMenuController?.Dispose();
                _settingsController?.Dispose();
                _gameController?.Dispose();
                break;
            case GameState.Settings:
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                _inventoryController?.Dispose();
                _shedController?.Dispose();

                break;
            case GameState.Shed:
                _shedController = new ShedController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                _settingsController?.Dispose();
                _gameController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _inventoryController?.Dispose();
                _settingsController?.Dispose();
                _shedController?.Dispose();
                break;
        }
    }
}
