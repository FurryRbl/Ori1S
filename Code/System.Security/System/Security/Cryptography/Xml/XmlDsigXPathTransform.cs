using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the XPath transform for a digital signature as defined by the W3C.</summary>
	// Token: 0x0200005D RID: 93
	public class XmlDsigXPathTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> class.</summary>
		// Token: 0x06000301 RID: 769 RVA: 0x0000DE6C File Offset: 0x0000C06C
		public XmlDsigXPathTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xpath-19991116";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000DE80 File Offset: 0x0000C080
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

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object; the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object return only objects of one of these types.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public override Type[] OutputTypes
		{
			get
			{
				if (this.output == null)
				{
					this.output = new Type[1];
					this.output[0] = typeof(XmlNodeList);
				}
				return this.output;
			}
		}

		/// <summary>Returns an XML representation of the parameters of a <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x06000304 RID: 772 RVA: 0x0000DF14 File Offset: 0x0000C114
		protected override XmlNodeList GetInnerXml()
		{
			if (this.xpath == null)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml("<XPath xmlns=\"http://www.w3.org/2000/09/xmldsig#\"></XPath>");
				this.xpath = xmlDocument.ChildNodes;
			}
			return this.xpath;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000305 RID: 773 RVA: 0x0000DF50 File Offset: 0x0000C150
		[MonoTODO("Evaluation of extension function here() results in different from MS.NET (is MS.NET really correct??).")]
		public override object GetOutput()
		{
			if (this.xpath == null || this.doc == null)
			{
				return new XmlDsigNodeList(new ArrayList());
			}
			string str = null;
			for (int i = 0; i < this.xpath.Count; i++)
			{
				switch (this.xpath[i].NodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					str += this.xpath[i].InnerText;
					break;
				}
			}
			this.ctx = new XmlDsigXPathTransform.XmlDsigXPathContext(this.doc);
			foreach (object obj in this.xpath)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XPathNavigator xpathNavigator = xmlNode.CreateNavigator();
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select("namespace::*");
				while (xpathNodeIterator.MoveNext())
				{
					if (xpathNodeIterator.Current.LocalName != "xml")
					{
						this.ctx.AddNamespace(xpathNodeIterator.Current.LocalName, xpathNodeIterator.Current.Value);
					}
				}
			}
			return this.EvaluateMatch(this.doc, str);
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		// Token: 0x06000306 RID: 774 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlNodeList))
			{
				throw new ArgumentException("type");
			}
			return this.GetOutput();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		private XmlDsigNodeList EvaluateMatch(XmlNode n, string xpath)
		{
			ArrayList arrayList = new ArrayList();
			XPathNavigator xpathNavigator = n.CreateNavigator();
			XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
			xpathExpression.SetContext(this.ctx);
			this.EvaluateMatch(n, xpathExpression, arrayList);
			return new XmlDsigNodeList(arrayList);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E130 File Offset: 0x0000C330
		private void EvaluateMatch(XmlNode n, XPathExpression exp, ArrayList al)
		{
			if (this.NodeMatches(n, exp))
			{
				al.Add(n);
			}
			if (n.Attributes != null)
			{
				for (int i = 0; i < n.Attributes.Count; i++)
				{
					if (this.NodeMatches(n.Attributes[i], exp))
					{
						al.Add(n.Attributes[i]);
					}
				}
			}
			for (int j = 0; j < n.ChildNodes.Count; j++)
			{
				this.EvaluateMatch(n.ChildNodes[j], exp, al);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		private bool NodeMatches(XmlNode n, XPathExpression exp)
		{
			object obj = n.CreateNavigator().Evaluate(exp);
			if (obj is bool)
			{
				return (bool)obj;
			}
			if (obj is double)
			{
				double num = (double)obj;
				return num != 0.0 && !double.IsNaN(num);
			}
			if (obj is string)
			{
				return ((string)obj).Length > 0;
			}
			if (obj is XPathNodeIterator)
			{
				XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)obj;
				return xpathNodeIterator.Count > 0;
			}
			return false;
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is null.-or- The <paramref name="nodeList" /> parameter does not contain an <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> element. </exception>
		// Token: 0x0600030A RID: 778 RVA: 0x0000E268 File Offset: 0x0000C468
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("nodeList");
			}
			this.xpath = nodeList;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600030B RID: 779 RVA: 0x0000E284 File Offset: 0x0000C484
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.doc = new XmlDocument();
				this.doc.PreserveWhitespace = true;
				this.doc.XmlResolver = base.GetResolver();
				this.doc.Load(new XmlSignatureStreamReader(new StreamReader((Stream)obj)));
			}
			else if (obj is XmlDocument)
			{
				this.doc = (obj as XmlDocument);
			}
			else if (obj is XmlNodeList)
			{
				this.doc = new XmlDocument();
				this.doc.XmlResolver = base.GetResolver();
				foreach (object obj2 in (obj as XmlNodeList))
				{
					XmlNode node = (XmlNode)obj2;
					XmlNode newChild = this.doc.ImportNode(node, true);
					this.doc.AppendChild(newChild);
				}
			}
		}

		// Token: 0x04000149 RID: 329
		private Type[] input;

		// Token: 0x0400014A RID: 330
		private Type[] output;

		// Token: 0x0400014B RID: 331
		private XmlNodeList xpath;

		// Token: 0x0400014C RID: 332
		private XmlDocument doc;

		// Token: 0x0400014D RID: 333
		private XsltContext ctx;

		// Token: 0x0200005E RID: 94
		internal class XmlDsigXPathContext : XsltContext
		{
			// Token: 0x0600030C RID: 780 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
			public XmlDsigXPathContext(XmlNode node)
			{
				this.here = new XmlDsigXPathTransform.XmlDsigXPathFunctionHere(node);
			}

			// Token: 0x0600030D RID: 781 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
			public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] argType)
			{
				if (name == "here" && prefix == string.Empty && argType.Length == 0)
				{
					return this.here;
				}
				return null;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600030E RID: 782 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
			public override bool Whitespace
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600030F RID: 783 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
			public override bool PreserveWhitespace(XPathNavigator node)
			{
				return true;
			}

			// Token: 0x06000310 RID: 784 RVA: 0x0000E3FC File Offset: 0x0000C5FC
			public override int CompareDocument(string s1, string s2)
			{
				return string.Compare(s1, s2);
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000E408 File Offset: 0x0000C608
			public override IXsltContextVariable ResolveVariable(string prefix, string name)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0400014E RID: 334
			private XmlDsigXPathTransform.XmlDsigXPathFunctionHere here;
		}

		// Token: 0x0200005F RID: 95
		internal class XmlDsigXPathFunctionHere : IXsltContextFunction
		{
			// Token: 0x06000312 RID: 786 RVA: 0x0000E410 File Offset: 0x0000C610
			public XmlDsigXPathFunctionHere(XmlNode node)
			{
				this.xpathNode = node.CreateNavigator().Select(".");
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000314 RID: 788 RVA: 0x0000E440 File Offset: 0x0000C640
			public XPathResultType[] ArgTypes
			{
				get
				{
					return XmlDsigXPathTransform.XmlDsigXPathFunctionHere.types;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000315 RID: 789 RVA: 0x0000E448 File Offset: 0x0000C648
			public int Maxargs
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000316 RID: 790 RVA: 0x0000E44C File Offset: 0x0000C64C
			public int Minargs
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000317 RID: 791 RVA: 0x0000E450 File Offset: 0x0000C650
			public XPathResultType ReturnType
			{
				get
				{
					return XPathResultType.NodeSet;
				}
			}

			// Token: 0x06000318 RID: 792 RVA: 0x0000E454 File Offset: 0x0000C654
			public object Invoke(XsltContext ctx, object[] args, XPathNavigator docContext)
			{
				if (args.Length != 0)
				{
					throw new ArgumentException("Not allowed arguments for function here().", "args");
				}
				return this.xpathNode.Clone();
			}

			// Token: 0x0400014F RID: 335
			private static XPathResultType[] types = new XPathResultType[0];

			// Token: 0x04000150 RID: 336
			private XPathNodeIterator xpathNode;
		}
	}
}
