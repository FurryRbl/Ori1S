using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown when an unrecognized HRESULT is returned from a COM method call.</summary>
	// Token: 0x0200037A RID: 890
	[ComVisible(true)]
	[Serializable]
	public class COMException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with default values.</summary>
		// Token: 0x06002A2A RID: 10794 RVA: 0x00092268 File Offset: 0x00090468
		public COMException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06002A2B RID: 10795 RVA: 0x00092270 File Offset: 0x00090470
		public COMException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002A2C RID: 10796 RVA: 0x0009227C File Offset: 0x0009047C
		public COMException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified message and error code.</summary>
		/// <param name="message">The message that indicates the reason the exception occurred. </param>
		/// <param name="errorCode">The error code (HRESULT) value associated with this exception. </param>
		// Token: 0x06002A2D RID: 10797 RVA: 0x00092288 File Offset: 0x00090488
		public COMException(string message, int errorCode) : base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class from serialization data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that supplies the contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002A2E RID: 10798 RVA: 0x00092294 File Offset: 0x00090494
		protected COMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Converts the contents of the exception to a string.</summary>
		/// <returns>A string containing the <see cref="P:System.Exception.HResult" />, <see cref="P:System.Exception.Message" />, <see cref="P:System.Exception.InnerException" />, and <see cref="P:System.Exception.StackTrace" /> properties of the exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002A2F RID: 10799 RVA: 0x000922A0 File Offset: 0x000904A0
		public override string ToString()
		{
			return string.Format("{0} (0x{1:x}): {2} {3}{4}{5}", new object[]
			{
				this.GetType(),
				base.HResult,
				this.Message,
				(this.InnerException != null) ? this.InnerException.ToString() : string.Empty,
				Environment.NewLine,
				(this.StackTrace == null) ? string.Empty : this.StackTrace
			});
		}
	}
}
