using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default event for a component.</summary>
	// Token: 0x020000EE RID: 238
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultEventAttribute" /> class.</summary>
		/// <param name="name">The name of the default event for the component this attribute is bound to. </param>
		// Token: 0x060009D4 RID: 2516 RVA: 0x0001C740 File Offset: 0x0001A940
		public DefaultEventAttribute(string name)
		{
			this.eventName = name;
		}

		/// <summary>Gets the name of the default event for the component this attribute is bound to.</summary>
		/// <returns>The name of the default event for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001C760 File Offset: 0x0001A960
		public string Name
		{
			get
			{
				return this.eventName;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultEventAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060009D7 RID: 2519 RVA: 0x0001C768 File Offset: 0x0001A968
		public override bool Equals(object o)
		{
			return o is DefaultEventAttribute && ((DefaultEventAttribute)o).eventName == this.eventName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009D8 RID: 2520 RVA: 0x0001C790 File Offset: 0x0001A990
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400029D RID: 669
		private string eventName;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultEventAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x0400029E RID: 670
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
