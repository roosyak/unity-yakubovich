using UnityEngine;
namespace PixselCrew.Model
{
    public abstract class PersistenProperty<TPropertyType>
    {
        // отображаемое значение 
        [SerializeField] private TPropertyType _value;
        // записанное на диске значение 
        private TPropertyType _stored;
        private TPropertyType _defaultValue;

        // конструктор
        public PersistenProperty(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;
        public TPropertyType Value
        {
            get => _stored;
            set
            {
                var isEquals = _stored.Equals(value);
                if (isEquals) return;

                var oldValue = _value;
                Write(value);
                _stored = _value = value;

                OnChanged?.Invoke(value, oldValue);
            }
        }

        // инициализация 
        protected void Init()
        {
            _stored = _value = Read(_defaultValue);
        }
        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaultValue);

        public void Validate()
        {
            if (!_stored.Equals(_value)) {
                Value = _value;
            }
        }
    }
}
