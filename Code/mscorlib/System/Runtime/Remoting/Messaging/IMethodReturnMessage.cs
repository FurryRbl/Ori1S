using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method call return message interface.</summary>
	// Token: 0x0200049F RID: 1183
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMessage, IMethodMessage
	{
		/// <summary>Gets the exception thrown during the method call.</summary>
		/// <returns>The exception object for the method call, or null if the method did not throw an exception.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002FF9 RID: 12281
		Exception Exception { get; }

		/// <summary>Gets the number of arguments in the method call marked as ref or out parameters.</summary>
		/// <returns>The number of arguments in the method call marked as ref or out parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002FFA RID: 12282
		int OutArgCount { get; }

		/// <summary>Returns the specified argument marked as a ref or an out parameter.</summary>
		/// <returns>The specified argument marked as a ref or an out parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002FFB RID: 12283
		object[] OutArgs { get; }

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>The return value of the method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002FFC RID: 12284
		object ReturnValue { get; }

		/// <summary>Returns the specified argument marked as a ref or an out parameter.</summary>
		/// <returns>The specified argument marked as a ref or an out parameter.</returns>
		/// <param name="argNum">The number of the requested argument. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06002FFD RID: 12285
		object GetOutArg(int argNum);

		/// <summary>Returns the name of the specified argument marked as a ref or an out parameter.</summary>
		/// <returns>The argument name, or null if the current method is not implemented.</returns>
		/// <param name="index">The number of the requested argument name. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06002FFE RID: 12286
		string GetOutArgName(int index);
	}
}
