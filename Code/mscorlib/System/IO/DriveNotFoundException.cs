﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when trying to access a drive or share that is not available.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000237 RID: 567
	[ComVisible(true)]
	[Serializable]
	public class DriveNotFoundException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_DIRECTORYNOTFOUND. </summary>
		// Token: 0x06001D73 RID: 7539 RVA: 0x0006CDEC File Offset: 0x0006AFEC
		public DriveNotFoundException() : base("Attempted to access a drive that is not available.")
		{
			base.HResult = -2147024893;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified message string and the HRESULT set to COR_E_DIRECTORYNOTFOUND. </summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06001D74 RID: 7540 RVA: 0x0006CE04 File Offset: 0x0006B004
		public DriveNotFoundException(string message) : base(message)
		{
			base.HResult = -2147024893;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception. </summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001D75 RID: 7541 RVA: 0x0006CE18 File Offset: 0x0006B018
		public DriveNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024893;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified serialization and context information. </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the serialized object data about the exception being thrown. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the exception being thrown. </param>
		// Token: 0x06001D76 RID: 7542 RVA: 0x0006CE30 File Offset: 0x0006B030
		protected DriveNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000AF8 RID: 2808
		private const int ErrorCode = -2147024893;
	}
}
