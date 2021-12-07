using System;
using System.IO;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a response from a Uniform Resource Identifier (URI). This is an abstract class.</summary>
	// Token: 0x02000424 RID: 1060
	[Serializable]
	public abstract class WebResponse : MarshalByRefObject, IDisposable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class.</summary>
		// Token: 0x0600265C RID: 9820 RVA: 0x000773D0 File Offset: 0x000755D0
		protected WebResponse()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">An instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class that contains the information required to serialize the new <see cref="T:System.Net.WebRequest" /> instance. </param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that indicates the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebRequest" /> instance. </param>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the constructor, when the constructor is not overridden in a descendant class. </exception>
		// Token: 0x0600265D RID: 9821 RVA: 0x000773D8 File Offset: 0x000755D8
		protected WebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>When overridden in a derived class, releases all resources used by the <see cref="T:System.Net.WebResponse" />. </summary>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		// Token: 0x0600265E RID: 9822 RVA: 0x000773E8 File Offset: 0x000755E8
		void IDisposable.Dispose()
		{
			this.Close();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize <see cref="T:System.Net.WebResponse" />.  </summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that will hold the serialized data for the <see cref="T:System.Net.WebResponse" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.WebResponse" />. </param>
		// Token: 0x0600265F RID: 9823 RVA: 0x000773F0 File Offset: 0x000755F0
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>When overridden in a descendant class, gets or sets the content length of data being received.</summary>
		/// <returns>The number of bytes returned from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000773F8 File Offset: 0x000755F8
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x00077400 File Offset: 0x00075600
		public virtual long ContentLength
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the content type of the data being received.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x00077408 File Offset: 0x00075608
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x00077410 File Offset: 0x00075610
		public virtual string ContentType
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of header name-value pairs associated with this request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.WebHeaderCollection" /> class that contains header values associated with this response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x00077418 File Offset: 0x00075618
		public virtual WebHeaderCollection Headers
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x00077420 File Offset: 0x00075620
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this response was obtained from the cache.</summary>
		/// <returns>true if the response was taken from the cache; otherwise, false.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x00077428 File Offset: 0x00075628
		[MonoTODO]
		public virtual bool IsFromCache
		{
			get
			{
				throw WebResponse.GetMustImplement();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>true if both client and server were authenticated; otherwise, false.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x00077430 File Offset: 0x00075630
		[MonoTODO]
		public virtual bool IsMutuallyAuthenticated
		{
			get
			{
				throw WebResponse.GetMustImplement();
			}
		}

		/// <summary>When overridden in a derived class, gets the URI of the Internet resource that actually responded to the request.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet resource that actually responded to the request.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x00077438 File Offset: 0x00075638
		public virtual System.Uri ResponseUri
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>When overridden by a descendant class, closes the response stream.</summary>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002669 RID: 9833 RVA: 0x00077440 File Offset: 0x00075640
		public virtual void Close()
		{
			throw new NotSupportedException();
		}

		/// <summary>When overridden in a descendant class, returns the data stream from the Internet resource.</summary>
		/// <returns>An instance of the <see cref="T:System.IO.Stream" /> class for reading data from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600266A RID: 9834 RVA: 0x00077448 File Offset: 0x00075648
		public virtual Stream GetResponseStream()
		{
			throw new NotSupportedException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization. </param>
		// Token: 0x0600266B RID: 9835 RVA: 0x00077450 File Offset: 0x00075650
		[MonoTODO]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw WebResponse.GetMustImplement();
		}
	}
}
