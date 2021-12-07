using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
	// Token: 0x020004C1 RID: 1217
	public class UploadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BC3 RID: 11203 RVA: 0x00098BF4 File Offset: 0x00096DF4
		internal UploadFileCompletedEventArgs(byte[] result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets the server reply to a data upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadFileAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x00098C08 File Offset: 0x00096E08
		public byte[] Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B93 RID: 7059
		private byte[] result;
	}
}
