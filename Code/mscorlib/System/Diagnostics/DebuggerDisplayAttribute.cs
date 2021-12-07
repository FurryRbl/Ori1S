using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines how a class or field is displayed in the debugger variable windows.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DF RID: 479
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerDisplayAttribute" /> class. </summary>
		/// <param name="value">The string to be displayed in the value column for instances of the type; an empty string ("") causes the value column to be hidden.</param>
		// Token: 0x06001862 RID: 6242 RVA: 0x0005D5BC File Offset: 0x0005B7BC
		public DebuggerDisplayAttribute(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			this.value = value;
			this.type = string.Empty;
			this.name = string.Empty;
		}

		/// <summary>Gets the string to display in the value column of the debugger variable windows.</summary>
		/// <returns>The string to display in the value column of the debugger variable.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0005D5FC File Offset: 0x0005B7FC
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets or sets the type of the attribute's target.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that identifies the attribute's target type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerDisplayAttribute.Target" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0005D604 File Offset: 0x0005B804
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x0005D60C File Offset: 0x0005B80C
		public Type Target
		{
			get
			{
				return this.target_type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.target_type = value;
				this.target_type_name = this.target_type.AssemblyQualifiedName;
			}
		}

		/// <summary>Gets or sets the type name of the attribute's target.</summary>
		/// <returns>The name of the attribute's target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0005D638 File Offset: 0x0005B838
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0005D640 File Offset: 0x0005B840
		public string TargetTypeName
		{
			get
			{
				return this.target_type_name;
			}
			set
			{
				this.target_type_name = value;
			}
		}

		/// <summary>Gets or sets the string to display in the type column of the debugger variable windows.</summary>
		/// <returns>The string to display in the type column of the debugger variable windows.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0005D64C File Offset: 0x0005B84C
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0005D654 File Offset: 0x0005B854
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the name to display in the debugger variable windows.</summary>
		/// <returns>The name to display in the debugger variable windows.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005D660 File Offset: 0x0005B860
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x0005D668 File Offset: 0x0005B868
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040008E4 RID: 2276
		private string value;

		// Token: 0x040008E5 RID: 2277
		private string type;

		// Token: 0x040008E6 RID: 2278
		private string name;

		// Token: 0x040008E7 RID: 2279
		private string target_type_name;

		// Token: 0x040008E8 RID: 2280
		private Type target_type;
	}
}
