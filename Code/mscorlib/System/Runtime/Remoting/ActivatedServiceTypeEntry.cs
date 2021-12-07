using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as one that can be activated on request from a client.</summary>
	// Token: 0x02000417 RID: 1047
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> class with the given <see cref="T:System.Type" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client-activated service type. </param>
		// Token: 0x06002CB3 RID: 11443 RVA: 0x00093E08 File Offset: 0x00092008
		public ActivatedServiceTypeEntry(Type type)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> class with the given type name and assembly name.</summary>
		/// <param name="typeName">The type name of the client-activated service type. </param>
		/// <param name="assemblyName">The assembly name of the client-activated service type. </param>
		// Token: 0x06002CB4 RID: 11444 RVA: 0x00093E40 File Offset: 0x00092040
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated service type.</summary>
		/// <returns>The context attributes for the client-activated service type.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x00093E98 File Offset: 0x00092098
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x00093E9C File Offset: 0x0009209C
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

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the client-activated service type.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x00093EA0 File Offset: 0x000920A0
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06002CB8 RID: 11448 RVA: 0x00093EA8 File Offset: 0x000920A8
		public override string ToString()
		{
			return base.AssemblyName + base.TypeName;
		}

		// Token: 0x04001356 RID: 4950
		private Type obj_type;
	}
}
