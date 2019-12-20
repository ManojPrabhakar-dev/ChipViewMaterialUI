using System.Windows;
using System.Windows.Controls;

namespace ChipViewApp
{
    /// <summary>
    /// Interaction logic for PairConfigSelection_UserControl.xaml
    /// </summary>
    public partial class PairConfigSelection_UserControl : UserControl
    {
        public PairConfigSelection_UserControl()
        {
            InitializeComponent();
        }

        #region PAIRMODE
        public string PairMode
        {
            get { return (string)GetValue(PairModeProperty); }
            set { SetValue(PairModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Left_PairConfigName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PairModeProperty =
        DependencyProperty.Register("PairMode", typeof(string), typeof(PairConfigSelection_UserControl), new PropertyMetadata("Single"));


        #endregion

        #region PAIRCONFIG_LEFT
        public string Left_PairConfigName
        {
            get { return (string)GetValue(Left_PairConfigNameProperty); }
            set { SetValue(Left_PairConfigNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Left_PairConfigName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Left_PairConfigNameProperty =
        DependencyProperty.Register("Left_PairConfigName", typeof(string), typeof(PairConfigSelection_UserControl), new PropertyMetadata(OnLeftPairConfig_TextChanged));

        private static void OnLeftPairConfig_TextChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            PairConfigSelection_UserControl UserControl1Control = d as PairConfigSelection_UserControl;
            UserControl1Control.OnLeftPairConfig_TextChanged(e);
        }

        public void OnLeftPairConfig_TextChanged(DependencyPropertyChangedEventArgs e)
        {
            cb_topLeft.Content = e.NewValue.ToString();
            cb_bottomLeft.Content = e.NewValue.ToString();
        }
        #endregion

        #region PAIRCONFIG_RIGHT
        public string Right_PairConfigName
        {
            get { return (string)GetValue(Right_PairConfigNameProperty); }
            set { SetValue(Right_PairConfigNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Left_PairConfigName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Right_PairConfigNameProperty =
        DependencyProperty.Register("Right_PairConfigName", typeof(string), typeof(PairConfigSelection_UserControl), new PropertyMetadata(OnRightPairConfig_TextChanged));

        private static void OnRightPairConfig_TextChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            PairConfigSelection_UserControl UserControl1Control = d as PairConfigSelection_UserControl;
            UserControl1Control.OnRightPairConfig_TextChanged(e);
        }

        public void OnRightPairConfig_TextChanged(DependencyPropertyChangedEventArgs e)
        {
            cb_topRight.Content = e.NewValue.ToString();
            cb_bottomRight.Content = e.NewValue.ToString();
        }
        #endregion

        #region TOPLEFT
        public bool IsTopLeftChecked
        {
            get { return (bool)GetValue(IsTopLeftCheckedProperty); }
            set { SetValue(IsTopLeftCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTopLeftChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTopLeftCheckedProperty =
    DependencyProperty.Register("IsTopLeftChecked", typeof(bool), typeof(PairConfigSelection_UserControl), new PropertyMetadata(OnTopLeftCheckedChanged));
        private static void OnTopLeftCheckedChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            PairConfigSelection_UserControl UserControl1Control = d as PairConfigSelection_UserControl;
            UserControl1Control.OnTopLeftCheckedChanged(e);
        }

        public void OnTopLeftCheckedChanged(DependencyPropertyChangedEventArgs e)
        {
            //cb_topLeft.Content = "Xyz";
            cb_topLeft.IsChecked = (bool)e.NewValue;
            //IsTopLeftChecked = (bool)e.NewValue;
        }
        #endregion

        #region TOPRIGHT
        public bool IsTopRightChecked
        {
            get { return (bool)GetValue(IsTopRightCheckedProperty); }
            set { SetValue(IsTopRightCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTopLeftChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTopRightCheckedProperty =
    DependencyProperty.Register("IsTopRightChecked", typeof(bool), typeof(PairConfigSelection_UserControl), new PropertyMetadata(IsTopRightCheckedChanged));
        private static void IsTopRightCheckedChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            PairConfigSelection_UserControl UserControl1Control = d as PairConfigSelection_UserControl;
            UserControl1Control.IsTopRightCheckedChanged(e);
        }

        public void IsTopRightCheckedChanged(DependencyPropertyChangedEventArgs e)
        {
            cb_topRight.IsChecked = (bool)e.NewValue;
        }
        #endregion

        #region BOTTOMLEFT
        public bool IsBottomLeftChecked
        {
            get { return (bool)GetValue(IsBottomLeftCheckedProperty); }
            set { SetValue(IsBottomLeftCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTopLeftChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBottomLeftCheckedProperty =
    DependencyProperty.Register("IsBottomLeftChecked", typeof(bool), typeof(PairConfigSelection_UserControl), new PropertyMetadata(IsBottomLeftCheckedChanged));
        private static void IsBottomLeftCheckedChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            PairConfigSelection_UserControl UserControl1Control = d as PairConfigSelection_UserControl;
            UserControl1Control.IsBottomLeftCheckedChanged(e);
        }

        public void IsBottomLeftCheckedChanged(DependencyPropertyChangedEventArgs e)
        {
            cb_bottomLeft.IsChecked = (bool)e.NewValue;
        }
        #endregion

        #region BOTTOMRIGHT
        public bool IsBottomRightChecked
        {
            get { return (bool)GetValue(IsBottomRightCheckedProperty); }
            set { SetValue(IsBottomRightCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTopLeftChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBottomRightCheckedProperty =
    DependencyProperty.Register("IsBottomRightChecked", typeof(bool), typeof(PairConfigSelection_UserControl), new PropertyMetadata(IsBottomRightChanged));
        private static void IsBottomRightChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            PairConfigSelection_UserControl UserControl1Control = d as PairConfigSelection_UserControl;
            UserControl1Control.IsBottomRightChanged(e);
        }

        public void IsBottomRightChanged(DependencyPropertyChangedEventArgs e)
        {
            cb_bottomRight.IsChecked = (bool)e.NewValue;
        }
        #endregion

        private void cb_topLeft_CheckedChanged(object sender, RoutedEventArgs e)
        {

            CheckBox cb_input = (CheckBox)sender;


            if (PairMode == "Single")
            {
                #region SINGLE
                //MessageBox.Show("Single PairMode = " + PairMode);
                if (cb_input.Name == "cb_topLeft")
                {
                    if (cb_input.IsChecked == true)
                    {
                        //cb_topRight.IsChecked = false;
                        //cb_bottomLeft.IsChecked = false;

                        IsTopLeftChecked = true;
                        //IsTopRightChecked = false;
                        IsBottomLeftChecked = false;
                        // IsBottomRightChecked = false;
                    }
                    else
                    {
                        IsTopLeftChecked = false;
                    }
                }
                else if (cb_input.Name == "cb_topRight")
                {
                    if (cb_input.IsChecked == true)
                    {
                        //cb_topLeft.IsChecked = false;
                        //cb_bottomRight.IsChecked = false;

                        IsTopRightChecked = true;
                        //IsTopLeftChecked = false;
                        // IsBottomLeftChecked = false;
                        IsBottomRightChecked = false;
                    }
                    else
                    {
                        IsTopRightChecked = false;
                    }
                }
                else if (cb_input.Name == "cb_bottomLeft")
                {
                    if (cb_input.IsChecked == true)
                    {
                        //cb_topLeft.IsChecked = false;
                        //cb_bottomRight.IsChecked = false;

                        IsBottomLeftChecked = true;
                        IsTopLeftChecked = false;
                        // IsTopRightChecked = false;
                        //IsBottomRightChecked = false;
                    }
                    else
                    {
                        IsBottomLeftChecked = false;
                    }

                }
                else if (cb_input.Name == "cb_bottomRight")
                {
                    if (cb_input.IsChecked == true)
                    {
                        //cb_topRight.IsChecked = false;
                        //cb_bottomLeft.IsChecked = false;

                        IsBottomRightChecked = true;
                        //IsTopLeftChecked = false;
                        IsTopRightChecked = false;
                        //IsBottomLeftChecked = false;
                    }
                    else
                    {
                        IsBottomRightChecked = false;
                    }
                }
                #endregion
            }
            else
            {
                #region DIFFERENTIAL
                //  MessageBox.Show("Differential PairMode = " + PairMode);
                if ((cb_input.Name == "cb_topLeft") || (cb_input.Name == "cb_topRight"))
                {
                    if (cb_input.IsChecked == true)
                    {
                        //cb_topLeft.IsChecked = true;
                        //cb_topRight.IsChecked = true;
                        //cb_bottomLeft.IsChecked = false;
                        //cb_bottomRight.IsChecked = false;

                        IsTopLeftChecked = true;
                        IsTopRightChecked = true;
                        IsBottomLeftChecked = false;
                        IsBottomRightChecked = false;
                    }
                    else
                    {
                        IsTopLeftChecked = false;
                        IsTopRightChecked = false;
                    }
                }
                else if ((cb_input.Name == "cb_bottomLeft") || (cb_input.Name == "cb_bottomRight"))
                {
                    if (cb_input.IsChecked == true)
                    {
                        //cb_topLeft.IsChecked = false;
                        //cb_topRight.IsChecked = false;
                        //cb_bottomLeft.IsChecked = true;
                        //cb_bottomRight.IsChecked = true;

                        IsTopLeftChecked = false;
                        IsTopRightChecked = false;
                        IsBottomLeftChecked = true;
                        IsBottomRightChecked = true;
                    }
                    else
                    {
                        IsBottomLeftChecked = false;
                        IsBottomRightChecked = false;
                    }
                }
                #endregion
            }

            //MessageBox.Show("cb_IsChecked = " + cb_input.IsChecked + " Content = " + cb_input.Content);
        }

    }
}
