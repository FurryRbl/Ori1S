using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client end as a type that can be activated on the server.</summary>
	// Token: 0x02000416 RID: 1046
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given <see cref="T:System.Type" /> and application URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client activated type. </param>
		/// <param name="appUrl">The URL of the application to activate the type in. </param>
		// Token: 0x06002CAC RID: 11436 RVA: 0x00093D2C File Offset: 0x00091F2C
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.applicationUrl = appUrl;
			this.obj_type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given type name, assembly name, and application URL.</summary>
		/// <param name="typeName">The type name of the client activated type. </param>
		/// <param name="assemblyName">The assembly name of the client activated type. </param>
		/// <param name="appUrl">The URL of the application to activate the type in. </param>
		// Token: 0x06002CAD RID: 11437 RVA: 0x00093D6C File Offset: 0x00091F6C
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			this.applicationUrl = appUrl;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x00093DCC File Offset: 0x00091FCC
		public string ApplicationUrl
		{
			get
			{
				return this.applicationUrl;
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated type.</summary>
		/// <returns>The context attributes for the client activated type.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x00093DD4 File Offset: 0x00091FD4
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x00093DD8 File Offset: 0x00091FD8
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the client-activated type.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x00093DDC File Offset: 0x00091FDC
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06002CB2 RID: 11442 RVA: 0x00093DE4 File Offset: 0x00091FE4
		public override string ToString()
		{
			return base.TypeName + base.AssemblyName + this.ApplicationUrl;
		}

		// Token: 0x04001354 RID: 4948
		private string applicationUrl;

		// Token: 0x04001355 RID: 4949
		private Type obj_type;
	}
}
