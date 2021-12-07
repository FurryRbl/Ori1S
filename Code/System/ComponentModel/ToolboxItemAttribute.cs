using System;

namespace System.ComponentModel
{
	/// <summary>Represents an attribute of a toolbox item.</summary>
	// Token: 0x020001AB RID: 427
	[AttributeUsage(AttributeTargets.All)]
	public class ToolboxItemAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and specifies whether to use default initialization values.</summary>
		/// <param name="defaultType">true to create a toolbox item attribute for a default type; false to associate no default toolbox item support for this attribute. </param>
		// Token: 0x06000EE0 RID: 3808 RVA: 0x000268D0 File Offset: 0x00024AD0
		public ToolboxItemAttribute(bool defaultType)
		{
			if (defaultType)
			{
				this.itemTypeName = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified name of the type.</summary>
		/// <param name="toolboxItemTypeName">The names of the type of the toolbox item and of the assembly that contains the type. </param>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x000268EC File Offset: 0x00024AEC
		public ToolboxItemAttribute(string toolboxItemName)
		{
			this.itemTypeName = toolboxItemName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified type of the toolbox item.</summary>
		/// <param name="toolboxItemType">The type of the toolbox item. </param>
		// Token: 0x06000EE2 RID: 3810 RVA: 0x000268FC File Offset: 0x00024AFC
		public ToolboxItemAttribute(Type toolboxItemType)
		{
			this.itemType = toolboxItemType;
		}

		/// <summary>Gets or sets the type of the toolbox item.</summary>
		/// <returns>The type of the toolbox item.</returns>
		/// <exception cref="T:System.ArgumentException">The type cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00026928 File Offset: 0x00024B28
		public Type ToolboxItemType
		{
			get
			{
				if (this.itemType == null && this.itemTypeName != null)
				{
					try
					{
						this.itemType = Type.GetType(this.itemTypeName, true);
					}
					catch (Exception innerException)
					{
						throw new ArgumentException("Failed to create ToolboxItem of type: " + this.itemTypeName, innerException);
					}
				}
				return this.itemType;
			}
		}

		/// <summary>Gets or sets the name of the type of the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>The fully qualified type name of the current toolbox item.</returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000269A4 File Offset: 0x00024BA4
		public string ToolboxItemTypeName
		{
			get
			{
				if (this.itemTypeName == null)
				{
					if (this.itemType == null)
					{
						return string.Empty;
					}
					this.itemTypeName = this.itemType.AssemblyQualifiedName;
				}
				return this.itemTypeName;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000EE6 RID: 3814 RVA: 0x000269DC File Offset: 0x00024BDC
		public override bool Equals(object o)
		{
			ToolboxItemAttribute toolboxItemAttribute = o as ToolboxItemAttribute;
			return toolboxItemAttribute != null && toolboxItemAttribute.ToolboxItemTypeName == this.ToolboxItemTypeName;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00026A0C File Offset: 0x00024C0C
		public override int GetHashCode()
		{
			if (this.itemTypeName != null)
			{
				return this.itemTypeName.GetHashCode();
			}
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00026A2C File Offset: 0x00024C2C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxItemAttribute.Default);
		}

		// Token: 0x04000436 RID: 1078
		private const string defaultItemType = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to the default, <see cref="T:System.Drawing.Design.ToolboxItem" />. This field is read-only.</summary>
		// Token: 0x04000437 RID: 1079
		public static readonly ToolboxItemAttribute Default = new ToolboxItemAttribute("System.Drawing.Design.ToolboxItem, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to null. This field is read-only.</summary>
		// Token: 0x04000438 RID: 1080
		public static readonly ToolboxItemAttribute None = new ToolboxItemAttribute(false);

		// Token: 0x04000439 RID: 1081
		private Type itemType;

		// Token: 0x0400043A RID: 1082
		private string itemTypeName;
	}
}
