using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
	// Token: 0x020004C0 RID: 1216
	public class UploadDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BC1 RID: 11201 RVA: 0x00098BD8 File Offset: 0x00096DD8
		internal UploadDataCompletedEventArgs(byte[] result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets the server reply to a data upload operation started by calling an <see cref="Overload:System.Net.WebClient.UploadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x00098BEC File Offset: 0x00096DEC
		public byte[] Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B92 RID: 7058
		private byte[] result;
	}
}
