using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the enveloped signature transform for an XML digital signature as defined by the W3C.</summary>
	// Token: 0x02000059 RID: 89
	public class XmlDsigEnvelopedSignatureTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> class.</summary>
		// Token: 0x060002E2 RID: 738 RVA: 0x0000D778 File Offset: 0x0000B978
		public XmlDsigEnvelopedSignatureTransform() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">true to include comments; otherwise, false. </param>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000D784 File Offset: 0x0000B984
		public XmlDsigEnvelopedSignatureTransform(bool includeComments)
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
			this.comments = includeComments;
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
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

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000D800 File Offset: 0x0000BA00
		public override Type[] OutputTypes
		{
			get
			{
				if (this.output == null)
				{
					this.output = new Type[2];
					this.output[0] = typeof(XmlDocument);
					this.output[1] = typeof(XmlNodeList);
				}
				return this.output;
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060002E6 RID: 742 RVA: 0x0000D850 File Offset: 0x0000BA50
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The containing XML document is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002E7 RID: 743 RVA: 0x0000D854 File Offset: 0x0000BA54
		public override object GetOutput()
		{
			if (this.inputObj is Stream)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.XmlResolver = base.GetResolver();
				xmlDocument.Load(new XmlSignatureStreamReader(new StreamReader(this.inputObj as Stream)));
				return this.GetOutputFromNode(xmlDocument, this.GetNamespaceManager(xmlDocument), true);
			}
			if (this.inputObj is XmlDocument)
			{
				XmlDocument xmlDocument = this.inputObj as XmlDocument;
				return this.GetOutputFromNode(xmlDocument, this.GetNamespaceManager(xmlDocument), true);
			}
			if (this.inputObj is XmlNodeList)
			{
				ArrayList arrayList = new ArrayList();
				XmlNodeList xmlNodeList = (XmlNodeList)this.inputObj;
				if (xmlNodeList.Count > 0)
				{
					XmlNamespaceManager namespaceManager = this.GetNamespaceManager(xmlNodeList.Item(0));
					ArrayList arrayList2 = new ArrayList();
					foreach (object obj in xmlNodeList)
					{
						XmlNode value = (XmlNode)obj;
						arrayList2.Add(value);
					}
					foreach (object obj2 in arrayList2)
					{
						XmlNode xmlNode = (XmlNode)obj2;
						if (xmlNode.SelectNodes("ancestor-or-self::dsig:Signature", namespaceManager).Count == 0)
						{
							arrayList.Add(this.GetOutputFromNode(xmlNode, namespaceManager, false));
						}
					}
				}
				return new XmlDsigNodeList(arrayList);
			}
			if (this.inputObj is XmlElement)
			{
				XmlElement xmlElement = this.inputObj as XmlElement;
				XmlNamespaceManager namespaceManager2 = this.GetNamespaceManager(xmlElement);
				if (xmlElement.SelectNodes("ancestor-or-self::dsig:Signature", namespaceManager2).Count == 0)
				{
					return this.GetOutputFromNode(xmlElement, namespaceManager2, true);
				}
			}
			throw new NullReferenceException();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000DA68 File Offset: 0x0000BC68
		private XmlNamespaceManager GetNamespaceManager(XmlNode n)
		{
			XmlDocument xmlDocument = (!(n is XmlDocument)) ? n.OwnerDocument : (n as XmlDocument);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			return xmlNamespaceManager;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		private XmlNode GetOutputFromNode(XmlNode input, XmlNamespaceManager nsmgr, bool remove)
		{
			if (remove)
			{
				XmlNodeList xmlNodeList = input.SelectNodes("descendant-or-self::dsig:Signature", nsmgr);
				ArrayList arrayList = new ArrayList();
				foreach (object obj in xmlNodeList)
				{
					XmlNode value = (XmlNode)obj;
					arrayList.Add(value);
				}
				foreach (object obj2 in arrayList)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					xmlNode.ParentNode.RemoveChild(xmlNode);
				}
			}
			return input;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		// Token: 0x060002EA RID: 746 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		public override object GetOutput(Type type)
		{
			if (type == typeof(Stream))
			{
				return this.GetOutput();
			}
			throw new ArgumentException("type");
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object. </param>
		// Token: 0x060002EB RID: 747 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The containing XML document is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002EC RID: 748 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public override void LoadInput(object obj)
		{
			this.inputObj = obj;
		}

		// Token: 0x0400013F RID: 319
		private Type[] input;

		// Token: 0x04000140 RID: 320
		private Type[] output;

		// Token: 0x04000141 RID: 321
		private bool comments;

		// Token: 0x04000142 RID: 322
		private object inputObj;
	}
}
