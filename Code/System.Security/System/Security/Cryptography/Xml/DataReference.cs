using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;DataReference&gt; element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000037 RID: 55
	public sealed class DataReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class.</summary>
		// Token: 0x06000147 RID: 327 RVA: 0x00006B30 File Offset: 0x00004D30
		public DataReference()
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		// Token: 0x06000148 RID: 328 RVA: 0x00006B44 File Offset: 0x00004D44
		public DataReference(string uri) : base(uri)
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI) and a <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted data.</param>
		// Token: 0x06000149 RID: 329 RVA: 0x00006B58 File Offset: 0x00004D58
		public DataReference(string uri, TransformChain tc) : base(uri, tc)
		{
			base.ReferenceType = "DataReference";
		}
	}
}
