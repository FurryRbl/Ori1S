﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design
{
	/// <summary>The exception that is thrown when an attempt to check out a file that is checked into a source code management program is canceled or fails.</summary>
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class CheckoutException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with no associated message or error code.</summary>
		// Token: 0x06000A06 RID: 2566 RVA: 0x0001CB38 File Offset: 0x0001AD38
		public CheckoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message.</summary>
		/// <param name="message">A message describing the exception. </param>
		// Token: 0x06000A07 RID: 2567 RVA: 0x0001CB40 File Offset: 0x0001AD40
		public CheckoutException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message and error code.</summary>
		/// <param name="message">A message describing the exception. </param>
		/// <param name="errorCode">The error code to pass. </param>
		// Token: 0x06000A08 RID: 2568 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		public CheckoutException(string message, int errorCode) : base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06000A09 RID: 2569 RVA: 0x0001CB58 File Offset: 0x0001AD58
		public CheckoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class using the specified serialization data and context. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06000A0A RID: 2570 RVA: 0x0001CB64 File Offset: 0x0001AD64
		protected CheckoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class that specifies that the check out was canceled. This field is read-only.</summary>
		// Token: 0x040002AA RID: 682
		public static readonly CheckoutException Canceled = new CheckoutException("The user canceled the checkout.", -2147467260);
	}
}
