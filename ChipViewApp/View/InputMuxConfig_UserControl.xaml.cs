using ChipViewApp.Utils;
using System.ComponentModel;
using System.Windows.Controls;

namespace ChipViewApp
{
    /// <summary>
    /// Interaction logic for InputMuxConfig_UserControl.xaml
    /// </summary>
    public partial class InputMuxConfig_UserControl : UserControl
    {
        public InputConfig m_inputConfig_inst { get; set; }
        public InputMuxConfig_UserControl()
        {
            InitializeComponent();

            m_inputConfig_inst = new InputConfig();

            //m_inputConfig_inst.Pair12_config = "Single";
            //m_inputConfig_inst.Pair34_config = "Single";
            //m_inputConfig_inst.Pair56_config = "Differential";
            //m_inputConfig_inst.Pair78_config = "Differential";

            DataContext = m_inputConfig_inst;
        }
    }

    public class InputConfig : INotifyPropertyChanged
    {
        private string Pair12_config;
        public string PAIR12_CONFIG
        {
            get
            {
                return Pair12_config;
            }
            set
            {
                Pair12_config = value;
                OnPropertyChanged(nameof(PAIR12_CONFIG));
            }
        }

        private string Pair34_config;
        public string PAIR34_CONFIG
        {
            get
            {
                return Pair34_config;
            }
            set
            {
                Pair34_config = value;
                OnPropertyChanged(nameof(PAIR34_CONFIG));
            }
        }

        private string Pair56_config;
        public string PAIR56_CONFIG
        {
            get
            {
                return Pair56_config;
            }
            set
            {
                Pair56_config = value;
                OnPropertyChanged(nameof(PAIR56_CONFIG));
            }
        }

        private string Pair78_config;
        public string PAIR78_CONFIG
        {
            get
            {
                return Pair78_config;
            }
            set
            {
                Pair78_config = value;
                OnPropertyChanged(nameof(PAIR78_CONFIG));
            }
        }

        private bool isCh2Enabled;
        public bool IS_CH2_ENABLED
        {
            get
            {
                return isCh2Enabled;
            }
            set
            {
                isCh2Enabled = value;
                OnPropertyChanged(nameof(IS_CH2_ENABLED));
            }
        }

        public PairXX m_pair12 { get; set; }
        public PairXX m_pair34 { get; set; }
        public PairXX m_pair56 { get; set; }
        public PairXX m_pair78 { get; set; }

        public INPUT_PAIR e_inputpair12 { get; set; }
        public INPUT_PAIR e_inputpair34 { get; set; }
        public INPUT_PAIR e_inputpair56 { get; set; }
        public INPUT_PAIR e_inputpair78 { get; set; }

        public InputConfig()
        {
            m_pair12 = new PairXX();
            m_pair34 = new PairXX();
            m_pair56 = new PairXX();
            m_pair78 = new PairXX();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class PairXX : INotifyPropertyChanged
    {
        private bool Top_Left;
        public bool TOP_LEFT
        {
            get
            {
                return Top_Left;
            }
            set
            {
                Top_Left = value;
                OnPropertyChanged(nameof(TOP_LEFT));
            }
        }

        private bool Top_Right;
        public bool TOP_RIGHT
        {
            get
            {
                return Top_Right;
            }
            set
            {
                Top_Right = value;
                OnPropertyChanged(nameof(TOP_RIGHT));
            }
        }

        private bool Bottom_Left;
        public bool BOTTOM_LEFT
        {
            get
            {
                return Bottom_Left;
            }
            set
            {
                Bottom_Left = value;
                OnPropertyChanged(nameof(BOTTOM_LEFT));
            }
        }

        private bool Bottom_Right;
        public bool BOTTOM_RIGHT
        {
            get
            {
                return Bottom_Right;
            }
            set
            {
                Bottom_Right = value;
                OnPropertyChanged(nameof(BOTTOM_RIGHT));
            }
        }

        public PairXX()
        {
            Top_Left = false;
            Top_Right = false;
            Bottom_Left = false;
            Bottom_Right = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

}
