using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction BackToMain) =>
            _buttonBack.onClick.AddListener(BackToMain);


        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }

    }
}
