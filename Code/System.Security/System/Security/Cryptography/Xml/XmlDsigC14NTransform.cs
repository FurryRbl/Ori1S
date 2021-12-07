using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Mono.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), without comments.</summary>
	// Token: 0x02000057 RID: 87
	public class XmlDsigC14NTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class.</summary>
		// Token: 0x060002D7 RID: 727 RVA: 0x0000D594 File Offset: 0x0000B794
		public XmlDsigC14NTransform() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">true to include comments; otherwise, false. </param>
		// Token: 0x060002D8 RID: 728 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		public XmlDsigC14NTransform(bool includeComments)
		{
			if (includeComments)
			{
				base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";
			}
			else
			{
				base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
			}
			this.canonicalizer = new XmlCanonicalizer(includeComments, false, base.PropagatedNamespaces);
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		public override Type[] InputTypes
		{
			get
			{
				if (this.input == null)
				{
					this.input = new Type[3];
					this.input[0] = typeof(Stream);
					this.input[1] = typeof(XmlDocument);
					this.input[2] = typeof(XmlNodeList);
				}
				return this.input;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object return only objects of one of these types.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000D648 File Offset: 0x0000B848
		public override Type[] OutputTypes
		{
			get
			{
				if (this.output == null)
				{
					this.output = new Type[1];
					this.output[0] = typeof(Stream);
				}
				return this.output;
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060002DB RID: 731 RVA: 0x0000D67C File Offset: 0x0000B87C
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object. </summary>
		/// <returns>The digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		// Token: 0x060002DC RID: 732 RVA: 0x0000D680 File Offset: 0x0000B880
		[ComVisible(false)]
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return hash.ComputeHash((Stream)this.GetOutput());
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		// Token: 0x060002DD RID: 733 RVA: 0x0000D694 File Offset: 0x0000B894
		public override object GetOutput()
		{
			return this.s;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.</exception>
		// Token: 0x060002DE RID: 734 RVA: 0x0000D69C File Offset: 0x0000B89C
		public override object GetOutput(Type type)
		{
			if (type == typeof(Stream))
			{
				return this.GetOutput();
			}
			throw new ArgumentException("type");
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element; this method is not supported because this element has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object. </param>
		// Token: 0x060002DF RID: 735 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a <see cref="T:System.IO.Stream" /> object and it is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E0 RID: 736 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		public override void LoadInput(object obj)
		{
			Stream stream = obj as Stream;
			if (stream != null)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.XmlResolver = base.GetResolver();
				xmlDocument.Load(new XmlSignatureStreamReader(new StreamReader(stream)));
				this.s = this.canonicalizer.Canonicalize(xmlDocument);
				return;
			}
			XmlDocument xmlDocument2 = obj as XmlDocument;
			if (xmlDocument2 != null)
			{
				this.s = this.canonicalizer.Canonicalize(xmlDocument2);
				return;
			}
			XmlNodeList xmlNodeList = obj as XmlNodeList;
			if (xmlNodeList != null)
			{
				this.s = this.canonicalizer.Canonicalize(xmlNodeList);
				return;
			}
			throw new ArgumentException("obj");
		}

		// Token: 0x0400013B RID: 315
		private Type[] input;

		// Token: 0x0400013C RID: 316
		private Type[] output;

		// Token: 0x0400013D RID: 317
		private XmlCanonicalizer canonicalizer;

		// Token: 0x0400013E RID: 318
		private Stream s;
	}
}
