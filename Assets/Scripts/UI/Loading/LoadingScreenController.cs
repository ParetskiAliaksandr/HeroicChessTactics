using HCT.Scripts.Services.LoadingScreenManagement;
using UnityEngine;
using Zenject;

namespace HCT.Scripts.UI.Loading
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private LoaderWindowUI _loaderWindowUI;

        private ILoadingScreenService _loadingService;

        [Inject]
        public void Construct(ILoadingScreenService loadingService)
        {
            _loadingService = loadingService;
        }

        private void Start()
        {
            _loadingService.Register(this);

            //_loaderWindowUI.ResetLoadReadings();
            //_loaderWindowUI.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _loadingService.Unregister(this);
        }

        public void ShowLoaderWindow()
        {
            if (_loaderWindowUI != null)
            {
                _loaderWindowUI.gameObject.SetActive(true);
            }
        }

        public void HideLoaderWindow()
        {
            if (_loaderWindowUI != null)
            {
                _loaderWindowUI.gameObject.SetActive(false);
            }
        }

        public void SetProgress(float value)
        {
            _loaderWindowUI.SetProgress(value);
        }

        public void ResetLoadReadings()
        {
            _loaderWindowUI.ResetLoadReadings();
        }
    }
}
