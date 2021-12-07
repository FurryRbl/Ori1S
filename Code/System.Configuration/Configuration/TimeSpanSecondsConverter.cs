﻿using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts a time span expressed in seconds. </summary>
	// Token: 0x0200007C RID: 124
	public class TimeSpanSecondsConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in seconds.</returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> cannot be parsed as an integer value.</exception>
		// Token: 0x06000420 RID: 1056 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if (!(data is string))
			{
				throw new ArgumentException("data");
			}
			long num;
			if (!long.TryParse((string)data, out num))
			{
				throw new ArgumentException("data");
			}
			return TimeSpan.FromSeconds((double)num);
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>The <see cref="T:System.String" /> that represents the <paramref name="value" /> parameter in minutes.</returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		// Token: 0x06000421 RID: 1057 RVA: 0x0000BB14 File Offset: 0x00009D14
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			return ((long)((TimeSpan)value).TotalSeconds).ToString();
		}
	}
}
