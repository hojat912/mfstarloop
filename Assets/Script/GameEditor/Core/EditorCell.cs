using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor.Core
{
    public class EditorCell : MonoBehaviour
    {

        [SerializeField]
        private Toggle _enableToggle;
        [SerializeField]
        private Slider _heightSlider;
        [SerializeField]
        private TextMeshProUGUI _heightText;

        public bool IsCellEnable => _enableToggle.isOn;

        public int HeightLevel => (int)_heightSlider.value;


        private void Awake()
        {
            _enableToggle.onValueChanged.AddListener(OnEnableToggleValueChanged);
            _heightSlider.onValueChanged.AddListener(OnHeightSliderValueChanged);
        }

        private void OnHeightSliderValueChanged(float value)
        {
            _heightText.text = value.ToString();
        }

        private void OnEnableToggleValueChanged(bool value)
        {
            _heightText.gameObject.SetActive(value);
            _heightSlider.gameObject.SetActive(value);
        }

        public void Clear()
        {
            _enableToggle.isOn = false;
            OnEnableToggleValueChanged(false);
        }
    }
}