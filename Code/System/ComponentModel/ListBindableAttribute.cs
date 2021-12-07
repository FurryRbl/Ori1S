using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a list can be used as a data source. A visual designer should use this attribute to determine whether to display a particular list in a data-binding picker. This class cannot be inherited.</summary>
	// Token: 0x0200017C RID: 380
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class ListBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using a value to indicate whether the list is bindable.</summary>
		/// <param name="listBindable">true if the list is bindable; otherwise, false. </param>
		// Token: 0x06000D0E RID: 3342 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		public ListBindableAttribute(bool listBindable)
		{
			this.bindable = listBindable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using <see cref="T:System.ComponentModel.BindableSupport" /> to indicate whether the list is bindable.</summary>
		/// <param name="flags">A <see cref="T:System.ComponentModel.BindableSupport" /> that indicates whether the list is bindable. </param>
		// Token: 0x06000D0F RID: 3343 RVA: 0x00020BD0 File Offset: 0x0001EDD0
		public ListBindableAttribute(BindableSupport flags)
		{
			if (flags == BindableSupport.No)
			{
				this.bindable = false;
			}
			else
			{
				this.bindable = true;
			}
		}

		/// <summary>Returns whether the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		/// <returns>true if the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test equality with. </param>
		// Token: 0x06000D11 RID: 3345 RVA: 0x00020C18 File Offset: 0x0001EE18
		public override bool Equals(object obj)
		{
			return obj is ListBindableAttribute && ((ListBindableAttribute)obj).ListBindable.Equals(this.bindable);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ListBindableAttribute" />.</returns>
		// Token: 0x06000D12 RID: 3346 RVA: 0x00020C4C File Offset: 0x0001EE4C
		public override int GetHashCode()
		{
			return this.bindable.GetHashCode();
		}

		/// <summary>Returns whether <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value.</summary>
		/// <returns>true if <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value; otherwise, false.</returns>
		// Token: 0x06000D13 RID: 3347 RVA: 0x00020C5C File Offset: 0x0001EE5C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default);
		}

		/// <summary>Gets whether the list is bindable.</summary>
		/// <returns>true if the list is bindable; otherwise, false.</returns>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00020C6C File Offset: 0x0001EE6C
		public bool ListBindable
		{
			get
			{
				return this.bindable;
			}
		}

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		// Token: 0x0400038C RID: 908
		public static readonly ListBindableAttribute Default = new ListBindableAttribute(true);

		/// <summary>Specifies that the list is not bindable. This static field is read-only.</summary>
		// Token: 0x0400038D RID: 909
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		/// <summary>Specifies that the list is bindable. This static field is read-only.</summary>
		// Token: 0x0400038E RID: 910
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		// Token: 0x0400038F RID: 911
		private bool bindable;
	}
}
