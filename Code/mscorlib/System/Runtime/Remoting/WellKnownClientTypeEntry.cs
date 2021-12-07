using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client as a server-activated type (single call or singleton).</summary>
	// Token: 0x02000437 RID: 1079
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class with the given type and URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the server-activated type. </param>
		/// <param name="objectUrl">The URL of the server-activated type. </param>
		// Token: 0x06002DD0 RID: 11728 RVA: 0x00098B3C File Offset: 0x00096D3C
		public WellKnownClientTypeEntry(Type type, string objectUrl)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
			this.obj_url = objectUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class with the given type, assembly name, and URL.</summary>
		/// <param name="typeName">The type name of the server-activated type. </param>
		/// <param name="assemblyName">The assembly name of the server-activated type. </param>
		/// <param name="objectUrl">The URL of the server-activated type. </param>
		// Token: 0x06002DD1 RID: 11729 RVA: 0x00098B7C File Offset: 0x00096D7C
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			this.obj_url = objectUrl;
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x00098BDC File Offset: 0x00096DDC
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x00098BE4 File Offset: 0x00096DE4
		public string ApplicationUrl
		{
			get
			{
				return this.app_url;
			}
			set
			{
				this.app_url = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated client type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the server-activated client type.</returns>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x00098BF0 File Offset: 0x00096DF0
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Gets the URL of the server-activated client object.</summary>
		/// <returns>The URL of the server-activated client object.</returns>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x00098BF8 File Offset: 0x00096DF8
		public string ObjectUrl
		{
			get
			{
				return this.obj_url;
			}
		}

		/// <summary>Returns the full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</summary>
		/// <returns>The full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06002DD6 RID: 11734 RVA: 0x00098C00 File Offset: 0x00096E00
		public override string ToString()
		{
			if (this.ApplicationUrl != null)
			{
				return base.TypeName + base.AssemblyName + this.ObjectUrl + this.ApplicationUrl;
			}
			return base.TypeName + base.AssemblyName + this.ObjectUrl;
		}

		// Token: 0x040013B3 RID: 5043
		private Type obj_type;

		// Token: 0x040013B4 RID: 5044
		private string obj_url;

		// Token: 0x040013B5 RID: 5045
		private string app_url;
	}
}
