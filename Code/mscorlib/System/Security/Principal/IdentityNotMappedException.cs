using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	/// <summary>Represents an exception for a principal whose identity could not be mapped to a known identity.</summary>
	// Token: 0x0200065E RID: 1630
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class.</summary>
		// Token: 0x06003E0F RID: 15887 RVA: 0x000D59B8 File Offset: 0x000D3BB8
		public IdentityNotMappedException() : base(Locale.GetText("Couldn't translate some identities."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06003E10 RID: 15888 RVA: 0x000D59CC File Offset: 0x000D3BCC
		public IdentityNotMappedException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message and inner exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If <paramref name="inner" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06003E11 RID: 15889 RVA: 0x000D59D8 File Offset: 0x000D3BD8
		public IdentityNotMappedException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Represents the collection of unmapped identities for an <see cref="T:System.Security.Principal.IdentityNotMappedException" /> exception.</summary>
		/// <returns>The collection of unmapped identities.</returns>
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000D59E4 File Offset: 0x000D3BE4
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this._coll == null)
				{
					this._coll = new IdentityReferenceCollection();
				}
				return this._coll;
			}
		}

		/// <summary>Gets serialization information with the data needed to create an instance of this <see cref="T:System.Security.Principal.IdentityNotMappedException" /> object. </summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization." /><see cref="SerializationInfo object" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.SerializationInfo." /><see cref="StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06003E13 RID: 15891 RVA: 0x000D5A04 File Offset: 0x000D3C04
		[MonoTODO("not implemented")]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x04001AB3 RID: 6835
		private IdentityReferenceCollection _coll;
	}
}
