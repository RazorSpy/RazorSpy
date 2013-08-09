using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace VibrantStyles.Xaml
{
    [DefaultProperty("Mappings")]
    public class EnumToDictionaryValueConverter : DependencyObject, IValueConverter
    {
        private static readonly DependencyProperty MappingsProperty = DependencyProperty.Register(
            "Mappings", typeof(ResourceDictionary), typeof(EnumToDictionaryValueConverter), new PropertyMetadata());

        public ResourceDictionary Mappings
        {
            get { return (ResourceDictionary)GetValue(MappingsProperty); }
            set { SetValue(MappingsProperty, value); }
        }

        public EnumToDictionaryValueConverter()
        {
            Mappings = new ResourceDictionary();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valStr = value as string;
            object val;
            if (String.IsNullOrEmpty(valStr) || Mappings == null || TryGetValue(Mappings, valStr, out val))
            {
                return null;
            }
            return val;
        }

        private static bool TryGetValue(ResourceDictionary dict, string key, out object val)
        {
            val = null;
            if (!dict.Contains(key))
            {
                return false;
            }
            val = dict[key];
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("EnumToResourceValueConverter only supports one-way bindings");
        }
    }
}