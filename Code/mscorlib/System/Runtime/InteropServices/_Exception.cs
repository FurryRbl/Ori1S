using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Exception" /> class to unmanaged code.</summary>
	// Token: 0x0200003A RID: 58
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
	[CLSCompliant(false)]
	public interface _Exception
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.HelpLink" /> property.</summary>
		/// <returns>The Uniform Resource Name (URN) or Uniform Resource Locator (URL) to a help file.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600062B RID: 1579
		// (set) Token: 0x0600062C RID: 1580
		string HelpLink { get; set; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.InnerException" /> property.</summary>
		/// <returns>An instance of <see cref="T:System.Exception" /> that describes the error that caused the current exception. The <see cref="P:System.Exception.InnerException" /> property returns the same value that was passed to the constructor, or a null reference (Nothing in Visual Basic) if the inner exception value was not supplied to the constructor. This property is read-only.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600062D RID: 1581
		Exception InnerException { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.Message" /> property.</summary>
		/// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600062E RID: 1582
		string Message { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.Source" /> property.</summary>
		/// <returns>The name of the application or the object that causes the error.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600062F RID: 1583
		// (set) Token: 0x06000630 RID: 1584
		string Source { get; set; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.StackTrace" /> property.</summary>
		/// <returns>A string that describes the contents of the call stack, with the most recent method call appearing first.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000631 RID: 1585
		string StackTrace { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.TargetSite" /> property.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> object that threw the current exception.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000632 RID: 1586
		MethodBase TargetSite { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x06000633 RID: 1587
		bool Equals(object obj);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetBaseException" /> method.</summary>
		/// <returns>The first exception thrown in a chain of exceptions. If the <see cref="P:System.Exception.InnerException" /> property of the current exception is a null reference (Nothing in Visual Basic), this property returns the current exception.</returns>
		// Token: 0x06000634 RID: 1588
		Exception GetBaseException();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x06000635 RID: 1589
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains contextual information about the source or destination. </param>
		// Token: 0x06000636 RID: 1590
		void GetObjectData(SerializationInfo info, StreamingContext context);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the exact runtime type of the current instance.</returns>
		// Token: 0x06000637 RID: 1591
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Exception" /> object.</returns>
		// Token: 0x06000638 RID: 1592
		string ToString();
	}
}
