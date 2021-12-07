using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property should be registered at runtime through the <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> method.</summary>
	// Token: 0x0200047F RID: 1151
	[ComVisible(true)]
	public interface IDynamicProperty
	{
		/// <summary>Gets the name of the dynamic property.</summary>
		/// <returns>The name of the dynamic property.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002F22 RID: 12066
		string Name { get; }
	}
}
