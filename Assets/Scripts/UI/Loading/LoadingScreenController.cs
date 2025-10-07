using UnityEngine;

namespace HCT.Scripts.UI.Loading
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private LoaderWindowUI _loaderWindowUI;

        public void ShowLoaderWindow()
        {
            _loaderWindowUI.gameObject.SetActive(true);   
        }

        public void HideLoaderWindow()
        {
            _loaderWindowUI.gameObject.SetActive(false);
        }
    }
}
