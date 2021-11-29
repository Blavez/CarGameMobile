using Profile;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(EntryPointConfig), menuName = "Configs/" + nameof(EntryPointConfig))]

internal class EntryPointConfig : ScriptableObject
{
        public float SpeedCar = 15f;
        public float JumpHeight = 15f;
        public GameState InitialState = GameState.Start;
        public Game.TransportType TransportType = Game.TransportType.Car;
}
