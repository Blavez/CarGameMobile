using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Tween.Scripts
{
    public class PopupView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _buttonClosePopup;

        [Header("Settings")]
        [SerializeField] private Vector3 _showSize = Vector3.one;
        [SerializeField] private Vector3 _hideSize = Vector3.zero;
        [SerializeField] private float _duration = 0.3f;


        private void Start() =>
            _buttonClosePopup.onClick.AddListener(HidePopup);

        private void OnDestroy() =>
            _buttonClosePopup.onClick.RemoveAllListeners();


        public void ShowPopup() =>
            PlayAnimation(_showSize, _duration,
                () => gameObject.SetActive(true));

        public void HidePopup() =>
            PlayAnimation(_hideSize, _duration,
            null, () => gameObject.SetActive(false));


        private void PlayAnimation(Vector3 targetScale, float duration, Action onStart = null, Action onFinish = null)
        {
            onStart?.Invoke();

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(targetScale, duration));
            sequence.OnComplete(
                () => onFinish?.Invoke());
        }
    }
}
