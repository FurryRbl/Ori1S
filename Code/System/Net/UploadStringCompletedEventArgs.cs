using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
	// Token: 0x020004C6 RID: 1222
	public class UploadStringCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x06002BCE RID: 11214 RVA: 0x00098CA4 File Offset: 0x00096EA4
		internal UploadStringCompletedEventArgs(string result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		/// <summary>Gets the server reply to a string upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x00098CB8 File Offset: 0x00096EB8
		public string Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04001B99 RID: 7065
		private string result;
	}
}
