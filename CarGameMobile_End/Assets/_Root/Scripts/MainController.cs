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
        DisposeAllControllers();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                DisposeAllControllers();
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsService, _iapService);
                break;
            case GameState.Game:
                DisposeAllControllers();
                _gameController = new GameController(_placeForUi,_profilePlayer);
                Debug.Log("GameController started");
                _analytics.SendEvent("GameController started");
                break;
            case GameState.Settings:
                DisposeAllControllers();
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                DisposeAllControllers();
                _shedController = new ShedController(_placeForUi, _profilePlayer);
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }
    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();
        _shedController?.Dispose();
    }
}
