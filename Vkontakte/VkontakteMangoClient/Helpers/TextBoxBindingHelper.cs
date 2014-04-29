using System.Windows;
using System.Windows.Controls;

namespace VkontakteMangoClient.Helpers
{
    public class TextBoxBindingHelper
    {
        public static bool GetUpdateSourceOnChange(DependencyObject obj)
        {
            return (bool) obj.GetValue(UpdateSourceOnChangeProperty);
        }

        public static void SetUpdateSourceOnChange(DependencyObject obj, bool value)
        {
            obj.SetValue(UpdateSourceOnChangeProperty, value);
        }

        private static readonly DependencyProperty UpdateSourceOnChangeProperty =
            DependencyProperty.RegisterAttached("UpdateSourceOnChange", typeof (bool), typeof (TextBoxBindingHelper),
                                                new PropertyMetadata(false, OnPropertyChange));

        private static void OnPropertyChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var txt = obj as TextBox;
            if (txt == null)
            {
                return;
            }
            if ((bool)e.NewValue)
            {
                txt.TextChanged += OnTextChanged;
            }
            else
            {
                txt.TextChanged -= OnTextChanged;
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            var be = txt.GetBindingExpression(TextBox.TextProperty);
            if (be != null)
            {
                be.UpdateSource();
            }
        }
    }
}
