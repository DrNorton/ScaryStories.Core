using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Test.Controls
{
    public partial class FontPicker : UserControl
    {
        public event EventHandler<FontFamilyChangedEventArgs> FontFamilyChanged;

        public void OnFontFamilyChanged(FontFamilyChangedEventArgs e)
        {
            EventHandler<FontFamilyChangedEventArgs> handler = FontFamilyChanged;
            if (handler != null) handler(this, e);
        }

        public ObservableCollection<FontFamily> FontFamilyCollection
        {
            get { return (ObservableCollection<FontFamily>)GetValue(FontFamilyCollectionProperty); }
            set { SetValue(FontFamilyCollectionProperty, value); }
        }

        public FontFamily SelectedFont
        {
            get { return (FontFamily)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        public static readonly DependencyProperty FontFamilyCollectionProperty =
            DependencyProperty.Register("FontFamilyCollection", typeof(ObservableCollection<FontFamily>), typeof(FontPicker), new PropertyMetadata(null));

        public static readonly DependencyProperty FontProperty =
            DependencyProperty.Register(
                "SelectedFont",
                typeof(FontFamily),
                typeof(FontPicker),
                new PropertyMetadata(OnFontPropertyChanged));

        private static void OnFontPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var fontPicker = (FontPicker)d;
            fontPicker.SelectedFont = (FontFamily)e.NewValue;
            var index= fontPicker.FontFamilyCollection.IndexOf(fontPicker.SelectedFont);
            if (index != -1) {
                fontPicker.lstPicker.SelectedItem = fontPicker.FontFamilyCollection.ElementAt(index); 
            }
           
            //fontPicker.lstPicker.SelectedIndex = ;
        }

    

        private static ObservableCollection<FontFamily> GetFontFamilies()
        {
            var fontFamilies = new ObservableCollection<FontFamily>();
            fontFamilies.Add(new FontFamily("Arial"));
            fontFamilies.Add(new FontFamily("Arial Black"));
            fontFamilies.Add(new FontFamily("Arial Bold"));
            fontFamilies.Add(new FontFamily("Arial Italic"));
            fontFamilies.Add(new FontFamily("Calibri"));
            fontFamilies.Add(new FontFamily("Calibri Bold"));
            fontFamilies.Add(new FontFamily("Calibri Italic"));
            fontFamilies.Add(new FontFamily("Comic Sans MS"));
            fontFamilies.Add(new FontFamily("Comic Sans MS Bold"));
            fontFamilies.Add(new FontFamily("Courier New"));
            fontFamilies.Add(new FontFamily("Courier New Bold"));
            fontFamilies.Add(new FontFamily("Courier New Italic"));
            fontFamilies.Add(new FontFamily("Georgia"));
            fontFamilies.Add(new FontFamily("Georgia Bold"));
            fontFamilies.Add(new FontFamily("Georgia Italic"));
            fontFamilies.Add(new FontFamily("Lucida Sans Unicode"));
            fontFamilies.Add(new FontFamily("Malgun Gothic"));
            fontFamilies.Add(new FontFamily("Meiryo UI"));
            fontFamilies.Add(new FontFamily("Microsoft YaHei"));
            fontFamilies.Add(new FontFamily("Segoe UI"));
            fontFamilies.Add(new FontFamily("Segoe UI Bold"));
            fontFamilies.Add(new FontFamily("Segoe WP"));
            fontFamilies.Add(new FontFamily("Segoe WP Black"));
            fontFamilies.Add(new FontFamily("Segoe WP Bold"));
            fontFamilies.Add(new FontFamily("Segoe WP Light"));
            fontFamilies.Add(new FontFamily("Segoe WP Semibold"));
            fontFamilies.Add(new FontFamily("Segoe WP SemiLight"));
            fontFamilies.Add(new FontFamily("Tahoma"));
            fontFamilies.Add(new FontFamily("Tahoma Bold"));
            fontFamilies.Add(new FontFamily("Times New Roman"));
            fontFamilies.Add(new FontFamily("Times New Roman Bold"));
            fontFamilies.Add(new FontFamily("Times New Roman Italic"));
            fontFamilies.Add(new FontFamily("Trebuchet MS"));
            fontFamilies.Add(new FontFamily("Trebuchet MS Bold"));
            fontFamilies.Add(new FontFamily("Trebuchet MS Italic"));
            fontFamilies.Add(new FontFamily("Verdana"));
            fontFamilies.Add(new FontFamily("Verdana Bold"));
            fontFamilies.Add(new FontFamily("Verdana Italic"));
            fontFamilies.Add(new FontFamily("Webdings"));
            fontFamilies.Add(new FontFamily("Wingdings"));
            return fontFamilies;
        }

        public FontPicker()
        {
            InitializeComponent();
            FontFamilyCollection = GetFontFamilies();
            lstPicker.ItemsSource = FontFamilyCollection;
        }

        private void LstPickerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0) {
                SelectedFont = (FontFamily)e.AddedItems[0];
                OnFontFamilyChanged(new FontFamilyChangedEventArgs((FontFamily)e.AddedItems[0]));
            }
        }
    }

    public class FontFamilyChangedEventArgs : EventArgs
    {
        public FontFamily FontFamily { get; set; }
        public FontFamilyChangedEventArgs(FontFamily fontFamily)
        {
            FontFamily = fontFamily;
        }
    }
}
