using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;


internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private EntryPointConfig _initialEntryPoint;
    private float SpeedCar;
    private float JumpHeight;
    private GameState InitialState = GameState.Start;
    private Game.TransportType TransportType = Game.TransportType.Car;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analytics;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private IAPService _iapService;

    private MainController _mainController;

    private void Awake()
    {
        SpeedCar = _initialEntryPoint.SpeedCar;
        JumpHeight = _initialEntryPoint.JumpHeight;
        var profilePlayer = new ProfilePlayer(SpeedCar, JumpHeight, TransportType, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analytics, _adsService, _iapService);

        _iapService.Initialized.AddListener(OnIapInitialized);
        _adsService.Initialized.AddListener(_adsService.InterstitialPlayer.Play);
        _analytics.SendEvent("Start Game");
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
    }
    private void OnIapInitialized() => _iapService.Buy("product_1");
    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
}
