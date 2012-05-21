using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace RazorSpy.Xaml
{
    public class EnumToDictionaryValueConverter : DependencyObject, IValueConverter
    {
        private static readonly DependencyPropertyKey MappingsProperty = DependencyProperty.RegisterReadOnly(
            "Mappings", typeof(IDictionary<string, object>), typeof(EnumToDictionaryValueConverter), new PropertyMetadata());

        public IDictionary<string, object> Mappings
        {
            get { return (IDictionary<string, object>)GetValue(MappingsProperty.DependencyProperty); }
            set { SetValue(MappingsProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valStr = value as string;
            object val;
            if (String.IsNullOrEmpty(valStr) || Mappings == null || !Mappings.TryGetValue(valStr, out val))
            {
                return null;
            }
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("EnumToResourceValueConverter only supports one-way bindings");
        }
    }
}
