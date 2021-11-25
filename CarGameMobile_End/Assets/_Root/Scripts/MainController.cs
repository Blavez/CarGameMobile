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
    private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
    private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

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
                _shedController = new ShedController(_profilePlayer, LoadInventoryView(_placeForUi), CreateItemsRepository(),
                    CreateInventoryController(_placeForUi), CreateRepository(), LoadView(_placeForUi));
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }


    private UpgradeHandlersRepository CreateRepository()
    {
        UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
        var repository = new UpgradeHandlersRepository(upgradeConfigs);
        AddRepository(repository);

        return repository;
    }

    private InventoryController CreateInventoryController(Transform placeForUi)
    {
        var inventoryView = LoadInventoryView(placeForUi);
        var itemsRepository = CreateItemsRepository();
        var inventoryModel = _profilePlayer.Inventory;
        var inventoryController = new InventoryController(inventoryView, inventoryModel, itemsRepository);
        AddController(inventoryController);

        return inventoryController;
    }

    private IItemsRepository CreateItemsRepository()
    {
        ResourcePath path = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
        ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(path);
        var repository = new ItemsRepository(itemConfigs);
        AddRepository(repository);

        return repository;
    }
    private IInventoryView LoadInventoryView(Transform placeForUi)
    {
        ResourcePath path = new ResourcePath("Prefabs/Inventory/InventoryView");
        GameObject prefab = ResourcesLoader.LoadPrefab(path);
        GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi);
        AddGameObject(objectView);

        return objectView.GetComponent<InventoryView>();
    }
    private ShedView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
        GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<ShedView>();
    }
    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();
        _shedController?.Dispose();
    }
}
