using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the parent property is notified when the value of the property that this attribute is applied to is modified. This class cannot be inherited.</summary>
	// Token: 0x02000190 RID: 400
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NotifyParentPropertyAttribute" /> class, using the specified value to determine whether the parent property is notified of changes to the value of the property.</summary>
		/// <param name="notifyParent">true if the parent should be notified of changes; otherwise, false. </param>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x000240D8 File Offset: 0x000222D8
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		/// <summary>Gets or sets a value indicating whether the parent property should be notified of changes to the value of the property.</summary>
		/// <returns>true if the parent property should be notified of changes; otherwise, false.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0002410C File Offset: 0x0002230C
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is the same as the current object.</summary>
		/// <returns>true if the object is the same as this object; otherwise, false.</returns>
		/// <param name="obj">The object to test for equality. </param>
		// Token: 0x06000DFA RID: 3578 RVA: 0x00024114 File Offset: 0x00022314
		public override bool Equals(object obj)
		{
			return obj is NotifyParentPropertyAttribute && (obj == this || ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06000DFB RID: 3579 RVA: 0x00024140 File Offset: 0x00022340
		public override int GetHashCode()
		{
			return this.notifyParent.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x06000DFC RID: 3580 RVA: 0x00024150 File Offset: 0x00022350
		public override bool IsDefaultAttribute()
		{
			return this.notifyParent == NotifyParentPropertyAttribute.Default.NotifyParent;
		}

		// Token: 0x040003F3 RID: 1011
		private bool notifyParent;

		/// <summary>Indicates the default attribute state, that the property should not notify the parent property of changes to its value. This field is read-only.</summary>
		// Token: 0x040003F4 RID: 1012
		public static readonly NotifyParentPropertyAttribute Default = new NotifyParentPropertyAttribute(false);

		/// <summary>Indicates that the parent property is not be notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x040003F5 RID: 1013
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		/// <summary>Indicates that the parent property is notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x040003F6 RID: 1014
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);
	}
}
