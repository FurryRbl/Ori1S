using System;

namespace System.Net
{
	/// <summary>Provides the base interface for creating <see cref="T:System.Net.WebRequest" /> instances.</summary>
	// Token: 0x02000330 RID: 816
	public interface IWebRequestCreate
	{
		/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> instance.</returns>
		/// <param name="uri">The uniform resource identifier (URI) of the Web resource. </param>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="uri" /> is not supported by this <see cref="T:System.Net.IWebRequestCreate" /> instance. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="uri" /> is not a valid URI. </exception>
		// Token: 0x06001CF7 RID: 7415
		WebRequest Create(System.Uri uri);
	}
}
