using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the XSLT transform for a digital signature as defined by the W3C.</summary>
	// Token: 0x02000060 RID: 96
	public class XmlDsigXsltTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> class.</summary>
		// Token: 0x06000319 RID: 793 RVA: 0x0000E47C File Offset: 0x0000C67C
		public XmlDsigXsltTransform() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">true to include comments; otherwise, false. </param>
		// Token: 0x0600031A RID: 794 RVA: 0x0000E488 File Offset: 0x0000C688
		public XmlDsigXsltTransform(bool includeComments)
		{
			this.comments = includeComments;
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xslt-19991116";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
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

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000E504 File Offset: 0x0000C704
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

		/// <summary>Returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x0600031D RID: 797 RVA: 0x0000E538 File Offset: 0x0000C738
		protected override XmlNodeList GetInnerXml()
		{
			return this.xnl;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600031E RID: 798 RVA: 0x0000E540 File Offset: 0x0000C740
		public override object GetOutput()
		{
			if (this.xnl == null)
			{
				throw new ArgumentNullException("LoadInnerXml before transformation.");
			}
			XmlResolver resolver = base.GetResolver();
			XslTransform xslTransform = new XslTransform();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = resolver;
			foreach (object obj in this.xnl)
			{
				XmlNode node = (XmlNode)obj;
				xmlDocument.AppendChild(xmlDocument.ImportNode(node, true));
			}
			xslTransform.Load(xmlDocument, resolver);
			if (this.inputDoc == null)
			{
				throw new ArgumentNullException("LoadInput before transformation.");
			}
			MemoryStream memoryStream = new MemoryStream();
			xslTransform.XmlResolver = resolver;
			xslTransform.Transform(this.inputDoc, null, memoryStream);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600031F RID: 799 RVA: 0x0000E638 File Offset: 0x0000C838
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream))
			{
				throw new ArgumentException("type");
			}
			return this.GetOutput();
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that encapsulates an XSLT style sheet to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object. This style sheet is applied to the document loaded by the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.LoadInput(System.Object)" /> method. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is null.-or- The <paramref name="nodeList" /> parameter does not contain an <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object. </exception>
		// Token: 0x06000320 RID: 800 RVA: 0x0000E65C File Offset: 0x0000C85C
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("nodeList");
			}
			this.xnl = nodeList;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000321 RID: 801 RVA: 0x0000E678 File Offset: 0x0000C878
		public override void LoadInput(object obj)
		{
			Stream stream = obj as Stream;
			if (stream != null)
			{
				this.inputDoc = new XmlDocument();
				this.inputDoc.XmlResolver = base.GetResolver();
				this.inputDoc.Load(new XmlSignatureStreamReader(new StreamReader(stream)));
				return;
			}
			XmlDocument xmlDocument = obj as XmlDocument;
			if (xmlDocument != null)
			{
				this.inputDoc = xmlDocument;
				return;
			}
			XmlNodeList xmlNodeList = obj as XmlNodeList;
			if (xmlNodeList != null)
			{
				this.inputDoc = new XmlDocument();
				this.inputDoc.XmlResolver = base.GetResolver();
				for (int i = 0; i < xmlNodeList.Count; i++)
				{
					this.inputDoc.AppendChild(this.inputDoc.ImportNode(xmlNodeList[i], true));
				}
			}
		}

		// Token: 0x04000151 RID: 337
		private Type[] input;

		// Token: 0x04000152 RID: 338
		private Type[] output;

		// Token: 0x04000153 RID: 339
		private bool comments;

		// Token: 0x04000154 RID: 340
		private XmlNodeList xnl;

		// Token: 0x04000155 RID: 341
		private XmlDocument inputDoc;
	}
}
