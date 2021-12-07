using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the default value for a property.</summary>
	// Token: 0x020000F0 RID: 240
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> that is the default value. </param>
		// Token: 0x060009DE RID: 2526 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
		public DefaultValueAttribute(bool value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using an 8-bit unsigned integer.</summary>
		/// <param name="value">An 8-bit unsigned integer that is the default value. </param>
		// Token: 0x060009DF RID: 2527 RVA: 0x0001C804 File Offset: 0x0001AA04
		public DefaultValueAttribute(byte value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a Unicode character.</summary>
		/// <param name="value">A Unicode character that is the default value. </param>
		// Token: 0x060009E0 RID: 2528 RVA: 0x0001C818 File Offset: 0x0001AA18
		public DefaultValueAttribute(char value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a double-precision floating point number.</summary>
		/// <param name="value">A double-precision floating point number that is the default value. </param>
		// Token: 0x060009E1 RID: 2529 RVA: 0x0001C82C File Offset: 0x0001AA2C
		public DefaultValueAttribute(double value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 16-bit signed integer.</summary>
		/// <param name="value">A 16-bit signed integer that is the default value. </param>
		// Token: 0x060009E2 RID: 2530 RVA: 0x0001C840 File Offset: 0x0001AA40
		public DefaultValueAttribute(short value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that is the default value. </param>
		// Token: 0x060009E3 RID: 2531 RVA: 0x0001C854 File Offset: 0x0001AA54
		public DefaultValueAttribute(int value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 64-bit signed integer.</summary>
		/// <param name="value">A 64-bit signed integer that is the default value. </param>
		// Token: 0x060009E4 RID: 2532 RVA: 0x0001C868 File Offset: 0x0001AA68
		public DefaultValueAttribute(long value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that represents the default value. </param>
		// Token: 0x060009E5 RID: 2533 RVA: 0x0001C87C File Offset: 0x0001AA7C
		public DefaultValueAttribute(object value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a single-precision floating point number.</summary>
		/// <param name="value">A single-precision floating point number that is the default value. </param>
		// Token: 0x060009E6 RID: 2534 RVA: 0x0001C88C File Offset: 0x0001AA8C
		public DefaultValueAttribute(float value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.String" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that is the default value. </param>
		// Token: 0x060009E7 RID: 2535 RVA: 0x0001C8A0 File Offset: 0x0001AAA0
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class, converting the specified value to the specified type, and using an invariant culture as the translation context.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to convert the value to. </param>
		/// <param name="value">A <see cref="T:System.String" /> that can be converted to the type using the <see cref="T:System.ComponentModel.TypeConverter" /> for the type and the U.S. English culture. </param>
		// Token: 0x060009E8 RID: 2536 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				this.DefaultValue = converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
			}
			catch
			{
			}
		}

		/// <summary>Gets the default value of the property this attribute is bound to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of the property this attribute is bound to.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0001C904 File Offset: 0x0001AB04
		public virtual object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		/// <summary>Sets the default value for the property to which this attribute is bound.</summary>
		/// <param name="value">The default value.</param>
		// Token: 0x060009EA RID: 2538 RVA: 0x0001C90C File Offset: 0x0001AB0C
		protected void SetValue(object value)
		{
			this.DefaultValue = value;
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultValueAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060009EB RID: 2539 RVA: 0x0001C918 File Offset: 0x0001AB18
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.DefaultValue == null)
			{
				return defaultValueAttribute.Value == null;
			}
			return this.DefaultValue.Equals(defaultValueAttribute.Value);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001C95C File Offset: 0x0001AB5C
		public override int GetHashCode()
		{
			if (this.DefaultValue == null)
			{
				return base.GetHashCode();
			}
			return this.DefaultValue.GetHashCode();
		}

		// Token: 0x040002A1 RID: 673
		private object DefaultValue;
	}
}
