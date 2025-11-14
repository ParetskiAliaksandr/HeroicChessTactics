using HCT.Scripts.UI.Loading;

namespace HCT.Scripts.Services.LoadingScreenManagement
{
    public class LoadingScreenService : ILoadingScreenService
    {
        private LoadingScreenController _lsController;

        public void Register(LoadingScreenController controller)
        {
            _lsController = controller;
        }

        public void SetProgress(float value)
        {
            _lsController?.SetProgress(value);
        }

        public void Hide()
        {
            _lsController?.HideLoaderWindow();
        }

        public void Show()
        {
            _lsController?.ShowLoaderWindow();
        }

        public void Unregister(LoadingScreenController controller)
        {
            if (_lsController == controller)
            {
                _lsController = null;
            }
        }

        public void ResetProgress()
        {
            _lsController.SetProgress(0.0f);
        }
    }
}
