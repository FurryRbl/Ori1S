using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Holds the name/value pair of the property name and the object representing the property of a context.</summary>
	// Token: 0x02000473 RID: 1139
	[ComVisible(true)]
	public class ContextProperty
	{
		// Token: 0x06002F06 RID: 12038 RVA: 0x0009BF74 File Offset: 0x0009A174
		private ContextProperty(string name, object prop)
		{
			this.name = name;
			this.prop = prop;
		}

		/// <summary>Gets the name of the T:System.Runtime.Remoting.Contexts.ContextProperty class.</summary>
		/// <returns>The name of the <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> class.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x0009BF8C File Offset: 0x0009A18C
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the object representing the property of a context.</summary>
		/// <returns>The object representing the property of a context.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x0009BF94 File Offset: 0x0009A194
		public virtual object Property
		{
			get
			{
				return this.prop;
			}
		}

		// Token: 0x04001404 RID: 5124
		private string name;

		// Token: 0x04001405 RID: 5125
		private object prop;
	}
}
