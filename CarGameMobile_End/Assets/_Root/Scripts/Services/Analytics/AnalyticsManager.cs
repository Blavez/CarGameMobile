using UnityEngine;
using Services.Analytics.UnityAnalytics;

namespace Services.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _services;

        private void Awake()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }
        
        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
    }
}
