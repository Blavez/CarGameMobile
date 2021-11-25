using Tool;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using JetBrains.Annotations;
using Features.Inventory.Items;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly IShedView _viewShed;
        private readonly ProfilePlayer _profilePlayer;
        private readonly IInventoryController _inventoryController;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;


        public ShedController(
            //[NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IInventoryController inventoryController,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository,
            [NotNull] IShedView viewShed)

        {
            //if (placeForUi == null)
               // throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            //_upgradeHandlersRepository = CreateRepository();
            //_inventoryController = CreateInventoryController(placeForUi);
            // _view = LoadView(placeForUi);
            _upgradeHandlersRepository
                 = upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));
            _inventoryController
                 = inventoryController ?? throw new ArgumentNullException(nameof(inventoryController));
            _viewShed
                = viewShed ?? throw new ArgumentNullException(nameof(viewShed));
            _viewShed.Init(Apply, Back);
        }


        private void Apply()
        {
            UpgradeWithEquippedItems(
                _profilePlayer.CurrentTransport,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
            Log($"Back. Current JumpHeight: {_profilePlayer.CurrentTransport.JumpHeight}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }


        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out var handler))
                    handler.Upgrade(upgradable);
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
