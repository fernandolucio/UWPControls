using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace UWPControls.Controls
{
    public sealed class ExpandedGridView : GridView
    {
        private Grid rootGrid;
        private const int Row_Width = 134;
        public const int Row_Height = 135;

        public string CountItemsHide
        {
            get { return (string)GetValue(CountItemsHideProperty); }
            set { SetValue(CountItemsHideProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CountItemsHideProperty =
            DependencyProperty.Register("CountItemsHide", typeof(string), typeof(ExpandedGridView), new PropertyMetadata(string.Empty));

        public ExpandedGridView()
        {
            this.DefaultStyleKey = typeof(ExpandedGridView);
            this.SizeChanged += ExpandedGridView_SizeChanged;
        }
        private void ExpandedGridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var totalItemsVisible = Math.Round(e.NewSize.Width / Row_Width);

            var totalItemsHide = Items.Count - totalItemsVisible;

            if (totalItemsHide > 0)
            {
                CountItemsHide = "+ " + totalItemsHide.ToString();
            }
            else
            {
                CountItemsHide = string.Empty;
            }
        }
        private void OpenExpandedToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            rootGrid.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
        }
        private void OpenExpandedToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            rootGrid.RowDefinitions[1].Height = new GridLength(Row_Height);
        }
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ToggleButton OpenExpandedToggleButton = GetTemplateChild<ToggleButton>("OpenExpandedToggleButton");
            rootGrid = GetTemplateChild<Grid>("RootGrid");

            OpenExpandedToggleButton.Checked += OpenExpandedToggleButton_Checked;
            OpenExpandedToggleButton.Unchecked += OpenExpandedToggleButton_Unchecked;
        }
        private T GetTemplateChild<T>(string name) where T : class
        {
            object obj = GetTemplateChild(name);

            return ((obj != null && obj is T) ? (T)obj : null);
        }
    }
}
