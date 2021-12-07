using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
	// Token: 0x020004C2 RID: 1218
	public class OpenReadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BC5 RID: 11205 RVA: 0x00098C10 File Offset: 0x00096E10
		internal OpenReadCompletedEventArgs(Stream result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets a readable stream that contains data downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x00098C24 File Offset: 0x00096E24
		public Stream Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B94 RID: 7060
		private Stream result;
	}
}
