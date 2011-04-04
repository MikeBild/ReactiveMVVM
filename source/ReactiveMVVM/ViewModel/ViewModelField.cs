using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReactiveMVVM.ViewModel
{
    public class ViewModelField<T> : INotifyPropertyChanged
    {
        public ViewModelField()
        {
            _isVisible = true;
            _isVisible = true;
            _isEnabled = true;
            _validationMessage = String.Empty;
        }

        public ViewModelField(T defaultValue)
        {
            _value = defaultValue;
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsValid"));
            }
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value) == false)
                {
                    _value = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        private string _validationMessage;
        public string ValidationMessage { get { return _validationMessage; } set { _validationMessage = value; PropertyChanged(this, new PropertyChangedEventArgs("ValidationMessage")); } }
        private bool _isVisible;
        public bool IsVisible { get { return _isVisible; } set { _isVisible = value; PropertyChanged(this, new PropertyChangedEventArgs("IsVisible")); } }
        private bool _isEnabled;
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; PropertyChanged(this, new PropertyChangedEventArgs("IsEnabled")); } }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public override string ToString()
        {
            return Value == null ? String.Empty : Value.ToString();
        }
    }
}