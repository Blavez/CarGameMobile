using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction backToMain) =>
            _buttonBack.onClick.AddListener(backToMain);


        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }

    }
}
