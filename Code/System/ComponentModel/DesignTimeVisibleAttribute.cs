using System;

namespace System.ComponentModel
{
	/// <summary>
	///   <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> marks a component's visibility. If <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Yes" /> is present, a visual designer can show this component on a designer.</summary>
	// Token: 0x0200013D RID: 317
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class DesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> set to the default value of false.</summary>
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		public DesignTimeVisibleAttribute() : this(true)
		{
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> with the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property set to the given value in <paramref name="visible" />.</summary>
		/// <param name="visible">The value that the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property will be set against. </param>
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001EEC8 File Offset: 0x0001D0C8
		public DesignTimeVisibleAttribute(bool visible)
		{
			this.visible = visible;
		}

		/// <summary>Gets or sets whether the component should be shown at design time.</summary>
		/// <returns>true if this component should be shown at design time, or false if it shouldn't.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0001EEFC File Offset: 0x0001D0FC
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000BCA RID: 3018 RVA: 0x0001EF04 File Offset: 0x0001D104
		public override bool Equals(object obj)
		{
			return obj is DesignTimeVisibleAttribute && (obj == this || ((DesignTimeVisibleAttribute)obj).Visible == this.visible);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0001EF30 File Offset: 0x0001D130
		public override int GetHashCode()
		{
			return this.visible.GetHashCode();
		}

		/// <summary>Gets a value indicating if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value.</summary>
		/// <returns>true, if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value; otherwise, false.</returns>
		// Token: 0x06000BCC RID: 3020 RVA: 0x0001EF40 File Offset: 0x0001D140
		public override bool IsDefaultAttribute()
		{
			return this.visible == DesignTimeVisibleAttribute.Default.Visible;
		}

		// Token: 0x04000352 RID: 850
		private bool visible;

		/// <summary>The default visibility which is Yes.</summary>
		// Token: 0x04000353 RID: 851
		public static readonly DesignTimeVisibleAttribute Default = new DesignTimeVisibleAttribute(true);

		/// <summary>Marks a component as not visible in a visual designer.</summary>
		// Token: 0x04000354 RID: 852
		public static readonly DesignTimeVisibleAttribute No = new DesignTimeVisibleAttribute(false);

		/// <summary>Marks a component as visible in a visual designer.</summary>
		// Token: 0x04000355 RID: 853
		public static readonly DesignTimeVisibleAttribute Yes = new DesignTimeVisibleAttribute(true);
	}
}
