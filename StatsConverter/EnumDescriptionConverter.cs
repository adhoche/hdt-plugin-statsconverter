﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HDT.Plugins.StatsConverter
{
	public class EnumDescriptionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return DependencyProperty.UnsetValue;
			try
			{
				return GetDescription((Enum)value);
			}
			catch (Exception)
			{
				return DependencyProperty.UnsetValue;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Enum.ToObject(targetType, value);

		public static string GetDescription(Enum en)
		{
			var type = en.GetType();
			var memInfo = type.GetMember(en.ToString());
			if (memInfo.Length > 0)
			{
				var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attrs.Length > 0)
					return ((DescriptionAttribute)attrs[0]).Description;
			}
			return en.ToString();
		}
	}
}