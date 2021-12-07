using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
	// Token: 0x020004C4 RID: 1220
	public class DownloadDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x00098C48 File Offset: 0x00096E48
		internal DownloadDataCompletedEventArgs(byte[] result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x00098C5C File Offset: 0x00096E5C
		public byte[] Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B96 RID: 7062
		private byte[] result;
	}
}
