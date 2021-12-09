using Ui;
using Game;
using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;
using Features.Inventory;
using Features.Shed;
using Features.Shed.Upgrade;
using Features.Inventory.Items;
using Tool;
using System.Collections.Generic;
using System;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _settingsController;
    private BackToMainController _backToMainController;
    private ShedController _shedController;
    private AnalyticsManager _analytics;
    private UnityAdsService _adsService;
    private IAPService _iapService;
    private readonly List<GameObject> _newObjects = new List<GameObject>();
    private readonly List<IDisposable> _newDisposables = new List<IDisposable>();

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
        DisposeAllNew();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                DisposeAllControllers();
                DisposeAllNew();
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsService, _iapService);
                break;
            case GameState.Game:
                DisposeAllControllers();
                DisposeAllNew();
                _gameController = new GameController(_placeForUi,_profilePlayer);
                _backToMainController = new BackToMainController(_placeForUi,_profilePlayer);
                Debug.Log("GameController started");
                _analytics.SendEvent("GameController started");
                break;
            case GameState.Settings:
                DisposeAllControllers();
                DisposeAllNew();
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                DisposeAllControllers();
                DisposeAllNew();
                _shedController = CreateShedController(_placeForUi);
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }
    private void DisposeAllNew()
    {
        DisposeNewDisposables();
        DisposeNewObjects();
    }

    private void DisposeNewDisposables()
    {
        foreach (IDisposable disposable in _newDisposables)
            disposable.Dispose();

        _newDisposables.Clear();
    }

    private void DisposeNewObjects()
    {
        foreach (GameObject gameObject in _newObjects)
            UnityEngine.Object.Destroy(gameObject);

        _newObjects.Clear();
    }
    private InventoryController CreateInventoryController(Transform placeForUi)
    {
        var inventoryView = LoadInventoryView(placeForUi);
        var itemsRepository = CreateItemsRepository();
        var inventoryModel = _profilePlayer.Inventory;
        var inventoryController = new InventoryController(inventoryView, inventoryModel, itemsRepository);
        _newDisposables.Add(inventoryController);
        return inventoryController;
    }

    private IItemsRepository CreateItemsRepository()
    {
        ResourcePath path = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
        ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(path);
        var repository = new ItemsRepository(itemConfigs);
        _newDisposables.Add(repository);
        return repository;
    }
    private IInventoryView LoadInventoryView(Transform placeForUi)
    {
        ResourcePath path = new ResourcePath("Prefabs/Inventory/InventoryView");
        GameObject prefab = ResourcesLoader.LoadPrefab(path);
        GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi);
        _newObjects.Add(objectView);
        return objectView.GetComponent<InventoryView>();
    }

    private UpgradeHandlersRepository CreateShedRepository()
    {
        var path = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(path);
        var repository = new UpgradeHandlersRepository(upgradeConfigs);
        return repository;
    }
    private ShedController CreateShedController(Transform placeForUi)
    {
        InventoryController inventoryController = CreateInventoryController(placeForUi);
        UpgradeHandlersRepository shedRepository = CreateShedRepository();
        ShedView shedView = LoadShedView(placeForUi);

        return new ShedController
        (
            shedView,
            _profilePlayer,
            inventoryController,
            shedRepository
        );
    }
    private ShedView LoadShedView(Transform placeForUi)
    {
        var path = new ResourcePath("Prefabs/Shed/ShedView");

        GameObject prefab = ResourcesLoader.LoadPrefab(path);
        GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
        _newObjects.Add(objectView);
        return objectView.GetComponent<ShedView>();
    }
    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();
        _shedController?.Dispose();
        _backToMainController?.Dispose();
    }
}
