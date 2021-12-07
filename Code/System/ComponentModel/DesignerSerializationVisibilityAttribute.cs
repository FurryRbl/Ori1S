using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the type of persistence to use when serializing a property on a component at design time.</summary>
	// Token: 0x02000107 RID: 263
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" /> class using the specified <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> value.</summary>
		/// <param name="visibility">One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. </param>
		// Token: 0x06000A95 RID: 2709 RVA: 0x0001D858 File Offset: 0x0001BA58
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility vis)
		{
			this.visibility = vis;
		}

		/// <summary>Gets a value indicating the basic serialization mode a serializer should use when determining whether and how to persist the value of a property.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. The default is <see cref="F:System.ComponentModel.DesignerSerializationVisibility.Visible" />.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		public DesignerSerializationVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06000A98 RID: 2712 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		public override bool Equals(object obj)
		{
			return obj is DesignerSerializationVisibilityAttribute && (obj == this || ((DesignerSerializationVisibilityAttribute)obj).Visibility == this.visibility);
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A99 RID: 2713 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		public override int GetHashCode()
		{
			return this.visibility.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the attribute is set to the default value; otherwise, false.</returns>
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		public override bool IsDefaultAttribute()
		{
			return this.visibility == DesignerSerializationVisibilityAttribute.Default.Visibility;
		}

		// Token: 0x040002D0 RID: 720
		private DesignerSerializationVisibility visibility;

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.DesignerSerializationVisibilityAttribute.Visible" />, that is, a visual designer uses default rules to generate the value of a property. This static field is read-only.</summary>
		// Token: 0x040002D1 RID: 721
		public static readonly DesignerSerializationVisibilityAttribute Default = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		/// <summary>Specifies that a serializer should serialize the contents of the property, rather than the property itself. This field is read-only.</summary>
		// Token: 0x040002D2 RID: 722
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		/// <summary>Specifies that a serializer should not serialize the value of the property. This static field is read-only.</summary>
		// Token: 0x040002D3 RID: 723
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		/// <summary>Specifies that a serializer should be allowed to serialize the value of the property. This static field is read-only.</summary>
		// Token: 0x040002D4 RID: 724
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);
	}
}
