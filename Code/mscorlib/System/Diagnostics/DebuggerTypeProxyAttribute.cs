using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the display proxy for a type.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001E3 RID: 483
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type name of the proxy. </summary>
		/// <param name="typeName">The type name of the proxy type.</param>
		// Token: 0x0600186F RID: 6255 RVA: 0x0005D68C File Offset: 0x0005B88C
		public DebuggerTypeProxyAttribute(string typeName)
		{
			this.proxy_type_name = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type of the proxy. </summary>
		/// <param name="type">A <see cref="T:System.Type" /> object that represents the proxy type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06001870 RID: 6256 RVA: 0x0005D69C File Offset: 0x0005B89C
		public DebuggerTypeProxyAttribute(Type type)
		{
			this.proxy_type_name = type.Name;
		}

		/// <summary>Gets the type name of the proxy type. </summary>
		/// <returns>The type name of the proxy type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0005D6B0 File Offset: 0x0005B8B0
		public string ProxyTypeName
		{
			get
			{
				return this.proxy_type_name;
			}
		}

		/// <summary>Gets or sets the target type for the attribute.</summary>
		/// <returns>A <see cref="T:System.Type" /> object identifying the target type for the attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerTypeProxyAttribute.Target" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0005D6B8 File Offset: 0x0005B8B8
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0005D6C0 File Offset: 0x0005B8C0
		public Type Target
		{
			get
			{
				return this.target_type;
			}
			set
			{
				this.target_type = value;
				this.target_type_name = this.target_type.Name;
			}
		}

		/// <summary>Gets or sets the name of the target type.</summary>
		/// <returns>The name of the target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0005D6DC File Offset: 0x0005B8DC
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0005D6E4 File Offset: 0x0005B8E4
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

		// Token: 0x040008E9 RID: 2281
		private string proxy_type_name;

		// Token: 0x040008EA RID: 2282
		private string target_type_name;

		// Token: 0x040008EB RID: 2283
		private Type target_type;
	}
}
