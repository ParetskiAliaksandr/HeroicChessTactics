using HCT.Scripts.Services.LoadingScreenManagement;
using UnityEngine;
using Zenject;

namespace HCT.Scripts.UI.Loading
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private ProgressBarView _loaderWindowUI;

        private ILoadingScreenService _loadingScreenService;

        [Inject]
        public void Construct(ILoadingScreenService loadingService)
        {
            _loadingScreenService = loadingService;
        }

        private void Start()
        {
            _loadingScreenService.Register(this);
        }

        private void OnDisable()
        {
            _loadingScreenService.Unregister(this);
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
    }
}
