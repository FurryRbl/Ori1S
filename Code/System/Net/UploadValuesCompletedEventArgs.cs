using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadValuesCompleted" /> event.</summary>
	// Token: 0x020004C9 RID: 1225
	public class UploadValuesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BD7 RID: 11223 RVA: 0x00098D28 File Offset: 0x00096F28
		internal UploadValuesCompletedEventArgs(byte[] result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets the server reply to a data upload operation started by calling an <see cref="Overload:System.Net.WebClient.UploadValuesAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x00098D3C File Offset: 0x00096F3C
		public byte[] Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B9F RID: 7071
		private byte[] result;
	}
}
