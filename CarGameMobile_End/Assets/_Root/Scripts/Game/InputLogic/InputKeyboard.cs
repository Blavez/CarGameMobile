using JoostenProductions;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class InputKeyboard : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10000000;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                float moveValue = _inputMultiplier * Time.deltaTime;
                float abs = Mathf.Abs(moveValue);
                OnRightMove(abs);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                float moveValue = _inputMultiplier * Time.deltaTime;
                float abs = Mathf.Abs(moveValue);
                OnLeftMove(abs);
            }
        }
    }
}
