using Profile;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(EntryPointConfig), menuName = "Configs/" + nameof(EntryPointConfig))]

internal class EntryPointConfig : ScriptableObject
{
        [field: SerializeField] public float SpeedCar = 15f;
        [field: SerializeField] public float JumpHeight = 15f;
        [field: SerializeField] public GameState InitialState = GameState.Start;
        [field: SerializeField] public Game.TransportType TransportType = Game.TransportType.Car;
}
