namespace Services.Ads
{
    internal interface IAdsService
    {
        IAdsPlayer InterstitialPlayer { get; }
        IAdsPlayer RewardedPlayer { get; }
        IAdsPlayer BannerPlayer { get; }
    }
}
