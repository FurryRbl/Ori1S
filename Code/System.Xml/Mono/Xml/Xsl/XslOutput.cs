using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000095 RID: 149
	internal class XslOutput
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0001F464 File Offset: 0x0001D664
		public XslOutput(string uri, string stylesheetVersion)
		{
			this.uri = uri;
			this.stylesheetVersion = stylesheetVersion;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001F498 File Offset: 0x0001D698
		public OutputMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		public XmlQualifiedName CustomMethod
		{
			get
			{
				return this.customMethod;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		public bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDeclaration;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		public StandaloneType Standalone
		{
			get
			{
				return this.standalone;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		public string DoctypePublic
		{
			get
			{
				return this.doctypePublic;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		public string DoctypeSystem
		{
			get
			{
				return this.doctypeSystem;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		public XmlQualifiedName[] CDataSectionElements
		{
			get
			{
				if (this.cdataSectionElements == null)
				{
					this.cdataSectionElements = (this.cdSectsList.ToArray(typeof(XmlQualifiedName)) as XmlQualifiedName[]);
				}
				return this.cdataSectionElements;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001F514 File Offset: 0x0001D714
		public string Indent
		{
			get
			{
				return this.indent;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001F51C File Offset: 0x0001D71C
		public string MediaType
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001F524 File Offset: 0x0001D724
		public void Fill(XPathNavigator nav)
		{
			if (nav.MoveToFirstAttribute())
			{
				this.ProcessAttribute(nav);
				while (nav.MoveToNextAttribute())
				{
					this.ProcessAttribute(nav);
				}
				nav.MoveToParent();
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001F564 File Offset: 0x0001D764
		private void ProcessAttribute(XPathNavigator nav)
		{
			if (nav.NamespaceURI != string.Empty)
			{
				return;
			}
			string value = nav.Value;
			string localName = nav.LocalName;
			switch (localName)
			{
			case "cdata-section-elements":
				if (value.Length > 0)
				{
					this.cdSectsList.AddRange(XslNameUtil.FromListString(value, nav));
				}
				return;
			case "method":
			{
				if (value.Length == 0)
				{
					return;
				}
				string text = value;
				switch (text)
				{
				case "xml":
					this.method = OutputMethod.XML;
					goto IL_269;
				case "html":
					this.omitXmlDeclaration = true;
					this.method = OutputMethod.HTML;
					goto IL_269;
				case "text":
					this.omitXmlDeclaration = true;
					this.method = OutputMethod.Text;
					goto IL_269;
				}
				this.method = OutputMethod.Custom;
				this.customMethod = XslNameUtil.FromString(value, nav);
				if (this.customMethod.Namespace == string.Empty)
				{
					IXmlLineInfo xmlLineInfo = nav as IXmlLineInfo;
					throw new XsltCompileException(new ArgumentException("Invalid output method value: '" + value + "'. It must be either 'xml' or 'html' or 'text' or QName."), nav.BaseURI, (xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber, (xmlLineInfo == null) ? 0 : xmlLineInfo.LinePosition);
				}
				IL_269:
				return;
			}
			case "version":
				if (value.Length > 0)
				{
					this.version = value;
				}
				return;
			case "encoding":
				if (value.Length > 0)
				{
					try
					{
						this.encoding = Encoding.GetEncoding(value);
					}
					catch (ArgumentException)
					{
					}
					catch (NotSupportedException)
					{
					}
				}
				return;
			case "standalone":
			{
				string text = value;
				if (text != null)
				{
					if (XslOutput.<>f__switch$map1D == null)
					{
						XslOutput.<>f__switch$map1D = new Dictionary<string, int>(2)
						{
							{
								"yes",
								0
							},
							{
								"no",
								1
							}
						};
					}
					int num2;
					if (XslOutput.<>f__switch$map1D.TryGetValue(text, out num2))
					{
						if (num2 == 0)
						{
							this.standalone = StandaloneType.YES;
							goto IL_397;
						}
						if (num2 == 1)
						{
							this.standalone = StandaloneType.NO;
							goto IL_397;
						}
					}
				}
				if (!(this.stylesheetVersion != "1.0"))
				{
					IXmlLineInfo xmlLineInfo2 = nav as IXmlLineInfo;
					throw new XsltCompileException(new XsltException("'" + value + "' is an invalid value for 'standalone' attribute.", null), nav.BaseURI, (xmlLineInfo2 == null) ? 0 : xmlLineInfo2.LineNumber, (xmlLineInfo2 == null) ? 0 : xmlLineInfo2.LinePosition);
				}
				IL_397:
				return;
			}
			case "doctype-public":
				this.doctypePublic = value;
				return;
			case "doctype-system":
				this.doctypeSystem = value;
				return;
			case "media-type":
				if (value.Length > 0)
				{
					this.mediaType = value;
				}
				return;
			case "omit-xml-declaration":
			{
				string text = value;
				if (text != null)
				{
					if (XslOutput.<>f__switch$map1E == null)
					{
						XslOutput.<>f__switch$map1E = new Dictionary<string, int>(2)
						{
							{
								"yes",
								0
							},
							{
								"no",
								1
							}
						};
					}
					int num2;
					if (XslOutput.<>f__switch$map1E.TryGetValue(text, out num2))
					{
						if (num2 == 0)
						{
							this.omitXmlDeclaration = true;
							goto IL_4AF;
						}
						if (num2 == 1)
						{
							this.omitXmlDeclaration = false;
							goto IL_4AF;
						}
					}
				}
				if (!(this.stylesheetVersion != "1.0"))
				{
					IXmlLineInfo xmlLineInfo3 = nav as IXmlLineInfo;
					throw new XsltCompileException(new XsltException("'" + value + "' is an invalid value for 'omit-xml-declaration' attribute.", null), nav.BaseURI, (xmlLineInfo3 == null) ? 0 : xmlLineInfo3.LineNumber, (xmlLineInfo3 == null) ? 0 : xmlLineInfo3.LinePosition);
				}
				IL_4AF:
				return;
			}
			case "indent":
			{
				this.indent = value;
				if (this.stylesheetVersion != "1.0")
				{
					return;
				}
				string text = value;
				if (text != null)
				{
					if (XslOutput.<>f__switch$map1F == null)
					{
						XslOutput.<>f__switch$map1F = new Dictionary<string, int>(2)
						{
							{
								"yes",
								0
							},
							{
								"no",
								0
							}
						};
					}
					int num2;
					if (XslOutput.<>f__switch$map1F.TryGetValue(text, out num2))
					{
						if (num2 == 0)
						{
							goto IL_568;
						}
					}
				}
				OutputMethod outputMethod = this.method;
				if (outputMethod != OutputMethod.Custom)
				{
					throw new XsltCompileException(string.Format("Unexpected 'indent' attribute value in 'output' element: '{0}'", value), null, nav);
				}
				IL_568:
				return;
			}
			}
			if (!(this.stylesheetVersion != "1.0"))
			{
				IXmlLineInfo xmlLineInfo4 = nav as IXmlLineInfo;
				throw new XsltCompileException(new XsltException("'" + nav.LocalName + "' is an invalid attribute for 'output' element.", null), nav.BaseURI, (xmlLineInfo4 == null) ? 0 : xmlLineInfo4.LineNumber, (xmlLineInfo4 == null) ? 0 : xmlLineInfo4.LinePosition);
			}
		}

		// Token: 0x04000330 RID: 816
		private string uri;

		// Token: 0x04000331 RID: 817
		private XmlQualifiedName customMethod;

		// Token: 0x04000332 RID: 818
		private OutputMethod method = OutputMethod.Unknown;

		// Token: 0x04000333 RID: 819
		private string version;

		// Token: 0x04000334 RID: 820
		private Encoding encoding = Encoding.UTF8;

		// Token: 0x04000335 RID: 821
		private bool omitXmlDeclaration;

		// Token: 0x04000336 RID: 822
		private StandaloneType standalone;

		// Token: 0x04000337 RID: 823
		private string doctypePublic;

		// Token: 0x04000338 RID: 824
		private string doctypeSystem;

		// Token: 0x04000339 RID: 825
		private XmlQualifiedName[] cdataSectionElements;

		// Token: 0x0400033A RID: 826
		private string indent;

		// Token: 0x0400033B RID: 827
		private string mediaType;

		// Token: 0x0400033C RID: 828
		private string stylesheetVersion;

		// Token: 0x0400033D RID: 829
		private ArrayList cdSectsList = new ArrayList();
	}
}
