using System.Collections;
using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class BackToMainController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/backToMain");
        private readonly ProfilePlayer _profilePlayer;
        private readonly BackToMainView _view;


        public BackToMainController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            Debug.Log("ûàûà");
            _view.Init(BackToMain);
        }

        private BackToMainView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<BackToMainView>();
        }

        private void BackToMain() =>
            _profilePlayer.CurrentState.Value = GameState.Start;

    }
}

