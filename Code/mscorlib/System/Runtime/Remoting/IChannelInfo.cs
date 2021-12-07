using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Provides custom channel information that is carried along with the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
	// Token: 0x0200041B RID: 1051
	[ComVisible(true)]
	public interface IChannelInfo
	{
		/// <summary>Gets and sets the channel data for each channel.</summary>
		/// <returns>The channel data for each channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002CBD RID: 11453
		// (set) Token: 0x06002CBE RID: 11454
		object[] ChannelData { get; set; }
	}
}
