using System;

namespace System.ComponentModel
{
	/// <summary>Provides metadata for a property representing a data field. This class cannot be inherited.</summary>
	// Token: 0x020000E8 RID: 232
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DataObjectFieldAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		// Token: 0x060009B2 RID: 2482 RVA: 0x0001C184 File Offset: 0x0001A384
		public DataObjectFieldAttribute(bool primaryKey)
		{
			this.primary_key = primaryKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, and whether the field is a database identity field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		// Token: 0x060009B3 RID: 2483 RVA: 0x0001C19C File Offset: 0x0001A39C
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity)
		{
			this.primary_key = primaryKey;
			this.is_identity = isIdentity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether the field is a database identity field, and whether the field can be null.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		// Token: 0x060009B4 RID: 2484 RVA: 0x0001C1BC File Offset: 0x0001A3BC
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable)
		{
			this.primary_key = primaryKey;
			this.is_identity = isIdentity;
			this.is_nullable = isNullable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether it is a database identity field, and whether it can be null and sets the length of the field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		/// <param name="length">The length of the field in bytes.</param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable, int length)
		{
			this.primary_key = primaryKey;
			this.is_identity = isIdentity;
			this.is_nullable = isNullable;
			this.length = length;
		}

		/// <summary>Gets a value indicating whether a property represents an identity field in the underlying data.</summary>
		/// <returns>true if the property represents an identity field in the underlying data; otherwise, false. The default value is false.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001C224 File Offset: 0x0001A424
		public bool IsIdentity
		{
			get
			{
				return this.is_identity;
			}
		}

		/// <summary>Gets a value indicating whether a property represents a field that can be null in the underlying data store.</summary>
		/// <returns>true if the property represents a field that can be null in the underlying data store; otherwise, false.</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001C22C File Offset: 0x0001A42C
		public bool IsNullable
		{
			get
			{
				return this.is_nullable;
			}
		}

		/// <summary>Gets the length of the property in bytes.</summary>
		/// <returns>The length of the property in bytes, or -1 if not set.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001C234 File Offset: 0x0001A434
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>Gets a value indicating whether a property is in the primary key in the underlying data.</summary>
		/// <returns>true if the property is in the primary key of the data store; otherwise, false.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001C23C File Offset: 0x0001A43C
		public bool PrimaryKey
		{
			get
			{
				return this.primary_key;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectFieldAttribute" />.</param>
		// Token: 0x060009BA RID: 2490 RVA: 0x0001C244 File Offset: 0x0001A444
		public override bool Equals(object obj)
		{
			DataObjectFieldAttribute dataObjectFieldAttribute = obj as DataObjectFieldAttribute;
			return dataObjectFieldAttribute != null && (dataObjectFieldAttribute.primary_key == this.primary_key && dataObjectFieldAttribute.is_identity == this.is_identity && dataObjectFieldAttribute.is_nullable == this.is_nullable) && dataObjectFieldAttribute.length == this.length;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009BB RID: 2491 RVA: 0x0001C2A4 File Offset: 0x0001A4A4
		public override int GetHashCode()
		{
			return (((!this.primary_key) ? 0 : 1) | ((!this.is_identity) ? 0 : 2) | ((!this.is_nullable) ? 0 : 4)) ^ this.length;
		}

		// Token: 0x0400028F RID: 655
		private bool primary_key;

		// Token: 0x04000290 RID: 656
		private bool is_identity;

		// Token: 0x04000291 RID: 657
		private bool is_nullable;

		// Token: 0x04000292 RID: 658
		private int length = -1;
	}
}
