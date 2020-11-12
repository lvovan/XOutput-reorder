﻿using XOutput.Devices;

namespace XOutput.UI.Component
{
    public class ButtonModel : ModelBase
    {
        private InputSource type;
        public InputSource Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }
        private bool value;
        public bool Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
    }
}
