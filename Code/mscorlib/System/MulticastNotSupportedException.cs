﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to combine two delegates based on the <see cref="T:System.Delegate" /> type instead of the <see cref="T:System.MulticastDelegate" /> type. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015B RID: 347
	[ComVisible(true)]
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class.</summary>
		// Token: 0x0600128D RID: 4749 RVA: 0x000494A0 File Offset: 0x000476A0
		public MulticastNotSupportedException() : base(Locale.GetText("This operation cannot be performed with the specified delagates."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600128E RID: 4750 RVA: 0x000494B4 File Offset: 0x000476B4
		public MulticastNotSupportedException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600128F RID: 4751 RVA: 0x000494C0 File Offset: 0x000476C0
		public MulticastNotSupportedException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000494CC File Offset: 0x000476CC
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
