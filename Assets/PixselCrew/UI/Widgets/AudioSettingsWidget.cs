using UnityEngine;
using UnityEngine.UI;
using PixselCrew.Model;
using System;

namespace PixselCrew.UI
{
    /*
    связь модели данных с интерфейсом  
     */
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _value;

        private FloatPersistenProperty _model;

        private void Start()
        {
            // подписаться на изменение слайдера на форме
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }

        public void SetModel(FloatPersistenProperty model)
        {
            _model = model;
            // подписаться на изменения модели 
            model.OnChanged += OnValueGhanged;
            OnValueGhanged(model.Value, model.Value);
        }

        private void OnValueGhanged(float newValue, float oldValue)
        {
            // изменить текст на форма 
            var textValue = Mathf.Round(newValue * 100);
            _value.text = textValue.ToString();
            _slider.normalizedValue = newValue;
        }

        private void OnDestroy()
        {
            // отписаться от всего 
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            _model.OnChanged -= OnValueGhanged;

        }
    }
}
