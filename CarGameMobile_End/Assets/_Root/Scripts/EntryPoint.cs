using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const float JumpHeight = 15f;
    private const GameState InitialState = GameState.Start;
    private const Game.TransportType TransportType = Game.TransportType.Car;

    [SerializeField] private Transform _placeForUi;
<<<<<<< Updated upstream

    private MainController _mainController;
=======
    [SerializeField] private AnalyticsManager _analytics;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private IAPService _iapService;
>>>>>>> Stashed changes

    private MainController _mainController;

    private void Awake()
    {
<<<<<<< Updated upstream
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState, InitialTransport);
        _mainController = new MainController(_placeForUi, profilePlayer);
=======
        var profilePlayer = new ProfilePlayer(SpeedCar, JumpHeight, TransportType, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analytics, _adsService, _iapService);

        _iapService.Initialized.AddListener(OnIapInitialized);
        _adsService.Initialized.AddListener(_adsService.InterstitialPlayer.Play);
        _analytics.SendEvent("Start Game");
>>>>>>> Stashed changes
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
