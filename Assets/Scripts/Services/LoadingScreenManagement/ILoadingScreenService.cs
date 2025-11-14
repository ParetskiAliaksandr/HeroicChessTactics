using HCT.Scripts.UI.Loading;

namespace HCT.Scripts.Services.LoadingScreenManagement
{
    public interface ILoadingScreenService
    {
        void Register(LoadingScreenController controller);
        void Unregister(LoadingScreenController controller);

        void Show();
        void Hide();

        void SetProgress(float value);
        void ResetProgress();
    }
}
