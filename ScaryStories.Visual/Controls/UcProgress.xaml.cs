using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ScaryStories.Visual.Controls
{
    public partial class UcProgress : UserControl
    {
        public double Minimum
        {
            get { return this.pbLoading.Minimum; }
            set
            {
                this.pbLoading.Minimum = value;
            }
        }

        public double Maximum
        {
            get { return this.pbLoading.Maximum; }
            set
            {
                this.pbLoading.Maximum = value;
            }
        }

        public double Value
        {
            get { return this.pbLoading.Value; }
            set
            {

                this.pbLoading.Value = value;
            }
        }

        public string Text
        {
            get { return textBlockStatus.Text; }
            set { textBlockStatus.Text = value; }
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); } 
        }

        public static readonly DependencyProperty IsOpenProperty =
           DependencyProperty.Register("IsOpen", typeof(bool), typeof(UcProgress), new PropertyMetadata(default(bool),OnOpen));

        private static void OnOpen(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newVal = (bool) e.NewValue;
            var control = d as UcProgress;
            if (newVal)
            {
                control.Show();
            }
            else
            {
                control.Hide();
            }
        }



        public UcProgress()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }
        internal Popup ChildWindowPopup
        {
            get;
            private set;
        }
        public void Show()
        {
            this.Visibility = Visibility.Visible;
            if (this.ChildWindowPopup == null)
            {
          
                this.ChildWindowPopup = new Popup();

                try
                {
                    this.ChildWindowPopup.Child = this;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException("The control is already shown.");
                }
            }


            if (this.ChildWindowPopup != null && Application.Current.RootVisual != null)
            {
                // Show popup
                this.ChildWindowPopup.IsOpen = true;
            }
        }
        public void Hide()
        {
            this.Visibility = Visibility.Collapsed;
            this.ChildWindowPopup.IsOpen = false;

        }
    }
}
