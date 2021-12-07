using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
	// Token: 0x020004C7 RID: 1223
	public class OpenWriteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BD0 RID: 11216 RVA: 0x00098CC0 File Offset: 0x00096EC0
		internal OpenWriteCompletedEventArgs(Stream result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets a writable stream that is used to send data to a server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> where you can write data to be uploaded.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x00098CD4 File Offset: 0x00096ED4
		public Stream Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B9A RID: 7066
		private Stream result;
	}
}
