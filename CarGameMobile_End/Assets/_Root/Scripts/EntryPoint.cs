using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;


internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;
    private const TransportType InitialTransport = TransportType.Boat;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;
    [SerializeField] private AnalyticsManager _analytics;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private IAPService _iapService;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState, InitialTransport);
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
