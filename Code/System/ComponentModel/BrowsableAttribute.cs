using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property or event should be displayed in a Properties window.</summary>
	// Token: 0x020000D3 RID: 211
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BrowsableAttribute" /> class.</summary>
		/// <param name="browsable">true if a property or event can be modified at design time; otherwise, false. The default is true. </param>
		// Token: 0x06000928 RID: 2344 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		public BrowsableAttribute(bool browsable)
		{
			this.browsable = browsable;
		}

		/// <summary>Gets a value indicating whether an object is browsable.</summary>
		/// <returns>true if the object is browsable; otherwise, false.</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public bool Browsable
		{
			get
			{
				return this.browsable;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x0600092B RID: 2347 RVA: 0x0001AA00 File Offset: 0x00018C00
		public override bool Equals(object obj)
		{
			return obj is BrowsableAttribute && (obj == this || ((BrowsableAttribute)obj).Browsable == this.browsable);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600092C RID: 2348 RVA: 0x0001AA2C File Offset: 0x00018C2C
		public override int GetHashCode()
		{
			return this.browsable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x0600092D RID: 2349 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public override bool IsDefaultAttribute()
		{
			return this.browsable == BrowsableAttribute.Default.Browsable;
		}

		// Token: 0x04000263 RID: 611
		private bool browsable;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BrowsableAttribute" />, which is <see cref="F:System.ComponentModel.BrowsableAttribute.Yes" />. This static field is read-only.</summary>
		// Token: 0x04000264 RID: 612
		public static readonly BrowsableAttribute Default = new BrowsableAttribute(true);

		/// <summary>Specifies that a property or event cannot be modified at design time. This static field is read-only.</summary>
		// Token: 0x04000265 RID: 613
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		/// <summary>Specifies that a property or event can be modified at design time. This static field is read-only.</summary>
		// Token: 0x04000266 RID: 614
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);
	}
}
