using Features.Inventory;
using Game.Boat;
using Game.Car;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly TransportType CurrentTransport;
        public readonly CarModel CurrentCar;
        public readonly BoatModel CurrentBoat;
        public readonly InventoryModel inventoryModel;


        public ProfilePlayer(float speedCar, GameState initialState, TransportType initialTransport) : this(speedCar)
        {
            CurrentState.Value = initialState;
            CurrentTransport = initialTransport;
        }

        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            inventoryModel = new InventoryModel();
            CurrentCar = new CarModel(speedCar);
            CurrentBoat = new BoatModel(speedCar);
        }
    }
}
