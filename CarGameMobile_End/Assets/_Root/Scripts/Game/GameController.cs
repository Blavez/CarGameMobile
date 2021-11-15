using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;

namespace Game
{
    internal class GameController : BaseController
    {
        public GameController(ProfilePlayer profilePlayer)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            if (profilePlayer.CurrentTransport == TransportType.Boat)
            {
                var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentBoat);
                AddController(inputGameController);
            }
            if (profilePlayer.CurrentTransport == TransportType.Car)
            {
                var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
                AddController(inputGameController);
            }

            var carController = new CarController();
            AddController(carController);
        }
    }
}
