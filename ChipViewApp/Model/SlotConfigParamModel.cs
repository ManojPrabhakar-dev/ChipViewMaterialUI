using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipViewApp.Model
{    
    class SlotConfigParams : INotifyPropertyChanged
    {
        private string lbl_OperatingModeName;
        public string OPERATING_MODE_NAME
        {
            get
            {
                return lbl_OperatingModeName;
            }
            set
            {
                lbl_OperatingModeName = value;
                OnPropertyChanged(nameof(OPERATING_MODE_NAME));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
