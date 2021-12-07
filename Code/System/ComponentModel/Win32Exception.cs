using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Throws an exception for a Win32 error code.</summary>
	// Token: 0x020001C5 RID: 453
	[SuppressUnmanagedCodeSecurity]
	[Serializable]
	public class Win32Exception : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the last Win32 error that occurred.</summary>
		// Token: 0x06000FDC RID: 4060 RVA: 0x000299D8 File Offset: 0x00027BD8
		public Win32Exception() : base(Win32Exception.W32ErrorMessage(Marshal.GetLastWin32Error()))
		{
			this.native_error_code = Marshal.GetLastWin32Error();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified error.</summary>
		/// <param name="error">The Win32 error code associated with this exception. </param>
		// Token: 0x06000FDD RID: 4061 RVA: 0x000299F8 File Offset: 0x00027BF8
		public Win32Exception(int error) : base(Win32Exception.W32ErrorMessage(error))
		{
			this.native_error_code = error;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified error and the specified detailed description.</summary>
		/// <param name="error">The Win32 error code associated with this exception. </param>
		/// <param name="message">A detailed description of the error. </param>
		// Token: 0x06000FDE RID: 4062 RVA: 0x00029A10 File Offset: 0x00027C10
		public Win32Exception(int error, string message) : base(message)
		{
			this.native_error_code = error;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified detailed description. </summary>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x06000FDF RID: 4063 RVA: 0x00029A20 File Offset: 0x00027C20
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public Win32Exception(string message) : base(message)
		{
			this.native_error_code = Marshal.GetLastWin32Error();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06000FE0 RID: 4064 RVA: 0x00029A34 File Offset: 0x00027C34
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public Win32Exception(string message, Exception innerException) : base(message, innerException)
		{
			this.native_error_code = Marshal.GetLastWin32Error();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified context and the serialization information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> associated with this exception. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the context of this exception. </param>
		// Token: 0x06000FE1 RID: 4065 RVA: 0x00029A4C File Offset: 0x00027C4C
		protected Win32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.native_error_code = info.GetInt32("NativeErrorCode");
		}

		/// <summary>Gets the Win32 error code associated with this exception.</summary>
		/// <returns>The Win32 error code associated with this exception.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00029A68 File Offset: 0x00027C68
		public int NativeErrorCode
		{
			get
			{
				return this.native_error_code;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this <see cref="T:System.ComponentModel.Win32Exception" /> occurred.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000FE3 RID: 4067 RVA: 0x00029A70 File Offset: 0x00027C70
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("NativeErrorCode", this.native_error_code);
			base.GetObjectData(info, context);
		}

		// Token: 0x06000FE4 RID: 4068
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string W32ErrorMessage(int error_code);

		// Token: 0x0400046C RID: 1132
		private int native_error_code;
	}
}
