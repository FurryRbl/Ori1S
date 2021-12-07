using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a member is typically used for binding. This class cannot be inherited.</summary>
	// Token: 0x020000CE RID: 206
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with one of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		// Token: 0x060008E4 RID: 2276 RVA: 0x0001A374 File Offset: 0x00018574
		public BindableAttribute(BindableSupport flags)
		{
			if (flags == BindableSupport.No)
			{
				this.bindable = false;
			}
			if (flags == BindableSupport.Yes || flags == BindableSupport.Default)
			{
				this.bindable = true;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with a Boolean value.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		// Token: 0x060008E5 RID: 2277 RVA: 0x0001A3AC File Offset: 0x000185AC
		public BindableAttribute(bool bindable)
		{
			this.bindable = bindable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x060008E6 RID: 2278 RVA: 0x0001A3BC File Offset: 0x000185BC
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.bindable = bindable;
			this.direction = direction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x060008E7 RID: 2279 RVA: 0x0001A3D4 File Offset: 0x000185D4
		public BindableAttribute(BindableSupport flags, BindingDirection direction) : this(flags)
		{
			this.direction = direction;
		}

		/// <summary>Gets a value indicating the direction or directions of this property's data binding.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.BindingDirection" />.</returns>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001A408 File Offset: 0x00018608
		public BindingDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Gets a value indicating that a property is typically used for binding.</summary>
		/// <returns>true if the property is typically used for binding; otherwise, false.</returns>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0001A410 File Offset: 0x00018610
		public bool Bindable
		{
			get
			{
				return this.bindable;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.BindableAttribute" /> objects are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.BindableAttribute" /> is equal to the current <see cref="T:System.ComponentModel.BindableAttribute" />; false if it is not equal.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x060008EB RID: 2283 RVA: 0x0001A418 File Offset: 0x00018618
		public override bool Equals(object obj)
		{
			return obj is BindableAttribute && (obj == this || ((BindableAttribute)obj).Bindable == this.bindable);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.BindableAttribute" />.</returns>
		// Token: 0x060008EC RID: 2284 RVA: 0x0001A444 File Offset: 0x00018644
		public override int GetHashCode()
		{
			return this.bindable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x060008ED RID: 2285 RVA: 0x0001A454 File Offset: 0x00018654
		public override bool IsDefaultAttribute()
		{
			return this.bindable == BindableAttribute.Default.Bindable;
		}

		// Token: 0x0400024C RID: 588
		private bool bindable;

		// Token: 0x0400024D RID: 589
		private BindingDirection direction;

		/// <summary>Specifies that a property is not typically used for binding. This field is read-only.</summary>
		// Token: 0x0400024E RID: 590
		public static readonly BindableAttribute No = new BindableAttribute(BindableSupport.No);

		/// <summary>Specifies that a property is typically used for binding. This field is read-only.</summary>
		// Token: 0x0400024F RID: 591
		public static readonly BindableAttribute Yes = new BindableAttribute(BindableSupport.Yes);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BindableAttribute" />, which is <see cref="F:System.ComponentModel.BindableAttribute.No" />. This field is read-only.</summary>
		// Token: 0x04000250 RID: 592
		public static readonly BindableAttribute Default = new BindableAttribute(BindableSupport.Default);
	}
}
