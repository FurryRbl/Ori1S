using System;
using System.IO;
using System.Net;
using System.Security.Permissions;

namespace System.Xml
{
	/// <summary>Resolves external XML resources named by a Uniform Resource Identifier (URI). </summary>
	// Token: 0x0200011A RID: 282
	public abstract class XmlResolver
	{
		/// <summary>When overridden in a derived class, sets the credentials used to authenticate Web requests.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> object. If this property is not set, the value defaults to null; that is, the XmlResolver has no user credentials.</returns>
		// Token: 0x17000370 RID: 880
		// (set) Token: 0x06000C01 RID: 3073
		public abstract ICredentials Credentials { set; }

		/// <summary>When overridden in a derived class, maps a URI to an object containing the actual resource.</summary>
		/// <returns>A System.IO.Stream object or null if a type other than stream is specified.</returns>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />. </param>
		/// <param name="role">The current version does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink:role and used as an implementation specific argument in other scenarios. </param>
		/// <param name="ofObjectToReturn">The type of object to return. The current version only returns System.IO.Stream objects. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="ofObjectToReturn" /> is not a Stream type. </exception>
		/// <exception cref="T:System.UriFormatException">The specified URI is not an absolute URI. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="absoluteUri" /> is null. </exception>
		/// <exception cref="T:System.Exception">There is a runtime error (for example, an interrupted server connection). </exception>
		// Token: 0x06000C02 RID: 3074
		public abstract object GetEntity(Uri absoluteUri, string role, Type type);

		/// <summary>When overridden in a derived class, resolves the absolute URI from the base and relative URIs.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the absolute URI or null if the relative URI cannot be resolved.</returns>
		/// <param name="baseUri">The base URI used to resolve the relative URI </param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="relativeUri" /> is null</exception>
		// Token: 0x06000C03 RID: 3075 RVA: 0x0003CBBC File Offset: 0x0003ADBC
		[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public virtual Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			if (baseUri == null)
			{
				if (relativeUri == null)
				{
					throw new ArgumentNullException("Either baseUri or relativeUri are required.");
				}
				if (relativeUri.StartsWith("http:") || relativeUri.StartsWith("https:") || relativeUri.StartsWith("ftp:") || relativeUri.StartsWith("file:"))
				{
					return new Uri(relativeUri);
				}
				return new Uri(Path.GetFullPath(relativeUri));
			}
			else
			{
				if (relativeUri == null)
				{
					return baseUri;
				}
				return new Uri(baseUri, this.EscapeRelativeUriBody(relativeUri));
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0003CC50 File Offset: 0x0003AE50
		private string EscapeRelativeUriBody(string src)
		{
			return src.Replace("<", "%3C").Replace(">", "%3E").Replace("#", "%23").Replace("%", "%25").Replace("\"", "%22");
		}
	}
}
