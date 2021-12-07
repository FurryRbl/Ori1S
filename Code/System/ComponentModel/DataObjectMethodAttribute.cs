using System;

namespace System.ComponentModel
{
	/// <summary>Identifies a data operation method exposed by a type, what type of operation the method performs, and whether the method is the default data method. This class cannot be inherited.</summary>
	// Token: 0x020000E9 RID: 233
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DataObjectMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class and identifies the type of data operation the method performs.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		// Token: 0x060009BC RID: 2492 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		public DataObjectMethodAttribute(DataObjectMethodType methodType) : this(methodType, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class, identifies the type of data operation the method performs, and identifies whether the method is the default data method that the data object exposes.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		/// <param name="isDefault">true to indicate the method that the attribute is applied to is the default method of the data object for the specified <paramref name="methodType" />; otherwise, false.</param>
		// Token: 0x060009BD RID: 2493 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public DataObjectMethodAttribute(DataObjectMethodType methodType, bool isDefault)
		{
			this._methodType = methodType;
			this._isDefault = isDefault;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.DataObjectMethodType" /> value indicating the type of data operation the method performs.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that identifies the type of data operation performed by the method to which the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001C314 File Offset: 0x0001A514
		public DataObjectMethodType MethodType
		{
			get
			{
				return this._methodType;
			}
		}

		/// <summary>Gets a value indicating whether the method that the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied to is the default data method exposed by the data object for a specific method type.</summary>
		/// <returns>true if the method is the default method exposed by the object for a method type; otherwise, false.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0001C31C File Offset: 0x0001A51C
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		/// <summary>Gets a value indicating whether this instance shares a common pattern with a specified attribute.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		// Token: 0x060009C0 RID: 2496 RVA: 0x0001C324 File Offset: 0x0001A524
		public override bool Match(object obj)
		{
			return obj is DataObjectMethodAttribute && ((DataObjectMethodAttribute)obj).MethodType == this.MethodType;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		// Token: 0x060009C1 RID: 2497 RVA: 0x0001C354 File Offset: 0x0001A554
		public override bool Equals(object obj)
		{
			return this.Match(obj) && ((DataObjectMethodAttribute)obj).IsDefault == this.IsDefault;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009C2 RID: 2498 RVA: 0x0001C388 File Offset: 0x0001A588
		public override int GetHashCode()
		{
			return this.MethodType.GetHashCode() ^ this.IsDefault.GetHashCode();
		}

		// Token: 0x04000293 RID: 659
		private readonly DataObjectMethodType _methodType;

		// Token: 0x04000294 RID: 660
		private readonly bool _isDefault;
	}
}
