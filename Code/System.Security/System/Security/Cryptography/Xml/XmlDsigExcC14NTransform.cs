using System;
using System.IO;
using System.Xml;
using Mono.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the exclusive C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), without comments.</summary>
	// Token: 0x0200005A RID: 90
	public class XmlDsigExcC14NTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class. </summary>
		// Token: 0x060002ED RID: 749 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public XmlDsigExcC14NTransform() : this(false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying a value that determines whether to include comments. </summary>
		/// <param name="includeComments">true to include comments; otherwise, false.</param>
		// Token: 0x060002EE RID: 750 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		public XmlDsigExcC14NTransform(bool includeComments) : this(includeComments, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying a list of namespace prefixes to canonicalize using the standard canonicalization algorithm. </summary>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		// Token: 0x060002EF RID: 751 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		public XmlDsigExcC14NTransform(string inclusiveNamespacesPrefixList) : this(false, inclusiveNamespacesPrefixList)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying whether to include comments, and specifying a list of namespace prefixes. </summary>
		/// <param name="includeComments">true to include comments; otherwise, false.</param>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		// Token: 0x060002F0 RID: 752 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public XmlDsigExcC14NTransform(bool includeComments, string inclusiveNamespacesPrefixList)
		{
			if (includeComments)
			{
				base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
			}
			else
			{
				base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#";
			}
			this.inclusiveNamespacesPrefixList = inclusiveNamespacesPrefixList;
			this.canonicalizer = new XmlCanonicalizer(includeComments, true, base.PropagatedNamespaces);
		}

		/// <summary>Gets or sets a string that contains namespace prefixes to canonicalize using the standard canonicalization algorithm. </summary>
		/// <returns>A string that contains namespace prefixes to canonicalize using the standard canonicalization algorithm.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public string InclusiveNamespacesPrefixList
		{
			get
			{
				return this.inclusiveNamespacesPrefixList;
			}
			set
			{
				this.inclusiveNamespacesPrefixList = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000DC60 File Offset: 0x0000BE60
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

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object; the <see cref="Overload:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object return only objects of one of these types.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
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

		/// <summary>Returns an XML representation of the parameters of a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060002F5 RID: 757 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the digest associated with a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>The digest associated with a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		// Token: 0x060002F6 RID: 758 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return hash.ComputeHash((Stream)this.GetOutput());
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		// Token: 0x060002F7 RID: 759 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		public override object GetOutput()
		{
			return this.s;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object as an object of the specified type.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object as an object of the specified type.</returns>
		/// <param name="type">The type of the output to return. This must be one of the types in the <see cref="P:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.OutputTypes" /> property.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.-or-The <paramref name="type" /> parameter does not derive from a <see cref="T:System.IO.Stream" /> object.</exception>
		// Token: 0x060002F8 RID: 760 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public override object GetOutput(Type type)
		{
			if (type == typeof(Stream))
			{
				return this.GetOutput();
			}
			throw new ArgumentException("type");
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</param>
		// Token: 0x060002F9 RID: 761 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is not a <see cref="T:System.IO.Stream" /> object.-or-The <paramref name="obj" /> parameter is not an <see cref="T:System.Xml.XmlDocument" /> object.-or-The <paramref name="obj" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002FA RID: 762 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		public override void LoadInput(object obj)
		{
			this.canonicalizer.InclusiveNamespacesPrefixList = this.InclusiveNamespacesPrefixList;
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

		// Token: 0x04000143 RID: 323
		private Type[] input;

		// Token: 0x04000144 RID: 324
		private Type[] output;

		// Token: 0x04000145 RID: 325
		private XmlCanonicalizer canonicalizer;

		// Token: 0x04000146 RID: 326
		private Stream s;

		// Token: 0x04000147 RID: 327
		private string inclusiveNamespacesPrefixList;
	}
}
