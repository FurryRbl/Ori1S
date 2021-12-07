using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;KeyReference&gt; element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000049 RID: 73
	public sealed class KeyReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption.</summary>
		// Token: 0x0600020E RID: 526 RVA: 0x00009EB8 File Offset: 0x000080B8
		public KeyReference()
		{
			base.ReferenceType = "KeyReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption using the supplied Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted key.</param>
		// Token: 0x0600020F RID: 527 RVA: 0x00009ECC File Offset: 0x000080CC
		public KeyReference(string uri) : base(uri)
		{
			base.ReferenceType = "KeyReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption using the specified Uniform Resource Identifier (URI) and a <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted key.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted key.</param>
		// Token: 0x06000210 RID: 528 RVA: 0x00009EE0 File Offset: 0x000080E0
		public KeyReference(string uri, TransformChain tc) : base(uri, tc)
		{
			base.ReferenceType = "KeyReference";
		}
	}
}
