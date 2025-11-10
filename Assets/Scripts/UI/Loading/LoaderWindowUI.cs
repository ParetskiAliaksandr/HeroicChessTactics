using UnityEngine;
using UnityEngine.UI;

namespace HCT.Scripts.UI.Loading
{
    public class LoaderWindowUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _fillSpeed = 4f;

        private float _target = 0f;

        private void Update()
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _target, Time.deltaTime * _fillSpeed);
        }

        public void SetProgress(float value)
        {
            _target = Mathf.Clamp01(value);

            if (_target <= 0f && _slider != null)
            {
                _slider.value = 0f;
            }
        }

        public void ResetLoadReadings()
        {
            _target = 0f;

            if (_slider != null)
            {
                _slider.value = 0f;
            }
        }
    }
}
