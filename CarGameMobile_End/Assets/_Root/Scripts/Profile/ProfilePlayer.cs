<<<<<<< Updated upstream
using Features.Inventory;
using Game.Boat;
using Game.Car;
=======
>>>>>>> Stashed changes
using Tool;
using Game;
using Game.Transport;
using Features.Inventory;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
<<<<<<< Updated upstream
        public readonly TransportType CurrentTransport;
        public readonly CarModel CurrentCar;
        public readonly BoatModel CurrentBoat;
        public readonly InventoryModel inventoryModel;
=======
        public readonly TransportModel CurrentTransport;
        public readonly InventoryModel Inventory;
>>>>>>> Stashed changes


        public ProfilePlayer(float transportSpeed, float JumpHeight, Game.TransportType transportType, GameState initialState)
        {
<<<<<<< Updated upstream
            CurrentState.Value = initialState;
            CurrentTransport = initialTransport;
        }

        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            inventoryModel = new InventoryModel();
            CurrentCar = new CarModel(speedCar);
            CurrentBoat = new BoatModel(speedCar);
=======
            CurrentState = new SubscriptionProperty<GameState>(initialState);
            CurrentTransport = new TransportModel(transportSpeed, JumpHeight, transportType);
            Inventory = new InventoryModel();
>>>>>>> Stashed changes
        }
    }
}
