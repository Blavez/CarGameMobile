using Ui;
using Game;
using Profile;
using UnityEngine;
<<<<<<< Updated upstream
=======
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;
using Features.Inventory;
>>>>>>> Stashed changes

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _settingsController;
<<<<<<< Updated upstream
=======
    private InventoryController _inventoryController;
    private AnalyticsManager _analytics;
    private UnityAdsService _adsService;
    private IAPService _iapService;
>>>>>>> Stashed changes


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
<<<<<<< Updated upstream
=======
        _settingsController?.Dispose();
        _inventoryController?.Dispose();
>>>>>>> Stashed changes

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                _settingsController?.Dispose();
                _inventoryController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                _inventoryController?.Dispose();
                break;
            case GameState.Settings:
                Debug.Log("jdjd");
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                _inventoryController?.Dispose();
                break;
            case GameState.Inventory:
                _inventoryController = new InventoryController(_placeForUi, _profilePlayer.inventoryModel);
                _settingsController?.Dispose();
                _gameController?.Dispose();
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
