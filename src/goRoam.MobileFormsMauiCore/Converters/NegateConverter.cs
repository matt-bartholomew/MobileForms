﻿using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace goRoamPOD.Converters
{
	public class NegateConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
	}
}
