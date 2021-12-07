using System;

namespace System.ComponentModel
{
	/// <summary>Identifies a type as an object suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object. This class cannot be inherited.</summary>
	// Token: 0x020000E7 RID: 231
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class. </summary>
		// Token: 0x060009AB RID: 2475 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		public DataObjectAttribute() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class and indicates whether an object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
		/// <param name="isDataObject">true if the object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, false.</param>
		// Token: 0x060009AC RID: 2476 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		public DataObjectAttribute(bool isDataObject)
		{
			this._isDataObject = isDataObject;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001C0F0 File Offset: 0x0001A2F0
		static DataObjectAttribute()
		{
			DataObjectAttribute.Default = DataObjectAttribute.NonDataObject;
		}

		/// <summary>Gets a value indicating whether an object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time.</summary>
		/// <returns>true if the object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, false.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0001C120 File Offset: 0x0001A320
		public bool IsDataObject
		{
			get
			{
				return this._isDataObject;
			}
		}

		/// <summary>Determines whether this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" /> fits the pattern of another object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" />. </param>
		// Token: 0x060009AF RID: 2479 RVA: 0x0001C128 File Offset: 0x0001A328
		public override bool Equals(object obj)
		{
			return obj is DataObjectAttribute && ((DataObjectAttribute)obj).IsDataObject == this.IsDataObject;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009B0 RID: 2480 RVA: 0x0001C158 File Offset: 0x0001A358
		public override int GetHashCode()
		{
			return this.IsDataObject.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x060009B1 RID: 2481 RVA: 0x0001C174 File Offset: 0x0001A374
		public override bool IsDefaultAttribute()
		{
			return DataObjectAttribute.Default.Equals(this);
		}

		/// <summary>Indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x0400028B RID: 651
		public static readonly DataObjectAttribute DataObject = new DataObjectAttribute(true);

		/// <summary>Represents the default value of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class, which indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x0400028C RID: 652
		public static readonly DataObjectAttribute Default;

		/// <summary>Indicates that the class is not suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x0400028D RID: 653
		public static readonly DataObjectAttribute NonDataObject = new DataObjectAttribute(false);

		// Token: 0x0400028E RID: 654
		private readonly bool _isDataObject;
	}
}
