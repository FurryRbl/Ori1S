using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores channel data for the remoting channels.</summary>
	// Token: 0x02000450 RID: 1104
	[ComVisible(true)]
	public interface IChannelDataStore
	{
		/// <summary>Gets an array of channel URIs to which the current channel maps.</summary>
		/// <returns>An array of channel URIs to which the current channel maps.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002E6D RID: 11885
		string[] ChannelUris { get; }

		/// <summary>Gets or sets the data object associated with the specified key for the implementing channel.</summary>
		/// <returns>The specified data object for the implementing channel.</returns>
		/// <param name="key">The key the data object is associated with. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x1700084C RID: 2124
		object this[object key]
		{
			get;
			set;
		}
	}
}
