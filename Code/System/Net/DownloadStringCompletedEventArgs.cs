using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
	// Token: 0x020004C3 RID: 1219
	public class DownloadStringCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BC7 RID: 11207 RVA: 0x00098C2C File Offset: 0x00096E2C
		internal DownloadStringCompletedEventArgs(string result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x00098C40 File Offset: 0x00096E40
		public string Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B95 RID: 7061
		private string result;
	}
}
