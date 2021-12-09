using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class BackToMainView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBackToMain;


        public void Init(UnityAction backToMain)
        {
            _buttonBackToMain.onClick.AddListener(backToMain);
            Debug.Log("sss");
        }

        public void OnDestroy()
        {
            _buttonBackToMain.onClick.RemoveAllListeners();
        }

    }
}

