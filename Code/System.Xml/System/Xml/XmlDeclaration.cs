using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Xml
{
	/// <summary>Represents the XML declaration node &lt;?xml version='1.0'...?&gt;.</summary>
	// Token: 0x020000EF RID: 239
	public class XmlDeclaration : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDeclaration" /> class.</summary>
		/// <param name="version">The XML version; see the <see cref="P:System.Xml.XmlDeclaration.Version" /> property.</param>
		/// <param name="encoding">The encoding scheme; see the <see cref="P:System.Xml.XmlDeclaration.Encoding" /> property.</param>
		/// <param name="standalone">Indicates whether the XML document depends on an external DTD; see the <see cref="P:System.Xml.XmlDeclaration.Standalone" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x060008A3 RID: 2211 RVA: 0x0002F8B0 File Offset: 0x0002DAB0
		protected internal XmlDeclaration(string version, string encoding, string standalone, XmlDocument doc) : base(doc)
		{
			if (encoding == null)
			{
				encoding = string.Empty;
			}
			if (standalone == null)
			{
				standalone = string.Empty;
			}
			this.version = version;
			this.encoding = encoding;
			this.standalone = standalone;
		}

		/// <summary>Gets or sets the encoding level of the XML document.</summary>
		/// <returns>The valid character encoding name. The most commonly supported character encoding names for XML are the following: Category Encoding Names Unicode UTF-8, UTF-16 ISO 10646 ISO-10646-UCS-2, ISO-10646-UCS-4 ISO 8859 ISO-8859-n (where "n" is a digit from 1 to 9) JIS X-0208-1997 ISO-2022-JP, Shift_JIS, EUC-JP This value is optional. If a value is not set, this property returns String.Empty.If an encoding attribute is not included, UTF-8 encoding is assumed when the document is written or saved out.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0002F900 File Offset: 0x0002DB00
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0002F908 File Offset: 0x0002DB08
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets the concatenated values of the XmlDeclaration.</summary>
		/// <returns>The concatenated values of the XmlDeclaration (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0002F924 File Offset: 0x0002DB24
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0002F92C File Offset: 0x0002DB2C
		public override string InnerText
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.ParseInput(value);
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlDeclaration nodes, the local name is xml.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0002F938 File Offset: 0x0002DB38
		public override string LocalName
		{
			get
			{
				return "xml";
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlDeclaration nodes, the name is xml.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0002F940 File Offset: 0x0002DB40
		public override string Name
		{
			get
			{
				return "xml";
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlDeclaration nodes, this value is XmlNodeType.XmlDeclaration.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0002F948 File Offset: 0x0002DB48
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		/// <summary>Gets or sets the value of the standalone attribute.</summary>
		/// <returns>Valid values are yes if all entity declarations required by the XML document are contained within the document or no if an external document type definition (DTD) is required. If a standalone attribute is not present in the XML declaration, this property returns String.Empty.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0002F94C File Offset: 0x0002DB4C
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0002F954 File Offset: 0x0002DB54
		public string Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				if (value != null)
				{
					if (string.Compare(value, "YES", true, CultureInfo.InvariantCulture) == 0)
					{
						this.standalone = "yes";
					}
					if (string.Compare(value, "NO", true, CultureInfo.InvariantCulture) == 0)
					{
						this.standalone = "no";
					}
				}
				else
				{
					this.standalone = string.Empty;
				}
			}
		}

		/// <summary>Gets or sets the value of the XmlDeclaration.</summary>
		/// <returns>The contents of the XmlDeclaration (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0002F9BC File Offset: 0x0002DBBC
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0002FA34 File Offset: 0x0002DC34
		public override string Value
		{
			get
			{
				string arg = string.Empty;
				string arg2 = string.Empty;
				if (this.encoding != string.Empty)
				{
					arg = string.Format(" encoding=\"{0}\"", this.encoding);
				}
				if (this.standalone != string.Empty)
				{
					arg2 = string.Format(" standalone=\"{0}\"", this.standalone);
				}
				return string.Format("version=\"{0}\"{1}{2}", this.Version, arg, arg2);
			}
			set
			{
				this.ParseInput(value);
			}
		}

		/// <summary>Gets the XML version of the document.</summary>
		/// <returns>The value is always 1.0.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0002FA40 File Offset: 0x0002DC40
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because XmlDeclaration nodes do not have children, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0002FA48 File Offset: 0x0002DC48
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlDeclaration(this.Version, this.Encoding, this.standalone, this.OwnerDocument);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because XmlDeclaration nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060008B1 RID: 2225 RVA: 0x0002FA74 File Offset: 0x0002DC74
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060008B2 RID: 2226 RVA: 0x0002FA78 File Offset: 0x0002DC78
		public override void WriteTo(XmlWriter w)
		{
			w.WriteRaw(string.Format("<?xml {0}?>", this.Value));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002FA90 File Offset: 0x0002DC90
		private int SkipWhitespace(string input, int index)
		{
			while (index < input.Length)
			{
				if (!XmlChar.IsWhitespace((int)input[index]))
				{
					break;
				}
				index++;
			}
			return index;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002FAD0 File Offset: 0x0002DCD0
		private void ParseInput(string input)
		{
			int num = this.SkipWhitespace(input, 0);
			if (num + 7 > input.Length || input.IndexOf("version", num, 7) != num)
			{
				throw new XmlException("Missing 'version' specification.");
			}
			num = this.SkipWhitespace(input, num + 7);
			char c = input[num];
			if (c != '=')
			{
				throw new XmlException("Invalid 'version' specification.");
			}
			num++;
			num = this.SkipWhitespace(input, num);
			c = input[num];
			if (c != '"' && c != '\'')
			{
				throw new XmlException("Invalid 'version' specification.");
			}
			num++;
			int num2 = input.IndexOf(c, num);
			if (num2 < 0 || input.IndexOf("1.0", num, 3) != num)
			{
				throw new XmlException("Invalid 'version' specification.");
			}
			num += 4;
			if (num == input.Length)
			{
				return;
			}
			if (!XmlChar.IsWhitespace((int)input[num]))
			{
				throw new XmlException("Invalid XML declaration.");
			}
			num = this.SkipWhitespace(input, num + 1);
			if (num == input.Length)
			{
				return;
			}
			if (input.Length > num + 8 && input.IndexOf("encoding", num, 8) > 0)
			{
				num = this.SkipWhitespace(input, num + 8);
				c = input[num];
				if (c != '=')
				{
					throw new XmlException("Invalid 'version' specification.");
				}
				num++;
				num = this.SkipWhitespace(input, num);
				c = input[num];
				if (c != '"' && c != '\'')
				{
					throw new XmlException("Invalid 'encoding' specification.");
				}
				num2 = input.IndexOf(c, num + 1);
				if (num2 < 0)
				{
					throw new XmlException("Invalid 'encoding' specification.");
				}
				this.Encoding = input.Substring(num + 1, num2 - num - 1);
				num = num2 + 1;
				if (num == input.Length)
				{
					return;
				}
				if (!XmlChar.IsWhitespace((int)input[num]))
				{
					throw new XmlException("Invalid XML declaration.");
				}
				num = this.SkipWhitespace(input, num + 1);
			}
			if (input.Length > num + 10 && input.IndexOf("standalone", num, 10) > 0)
			{
				num = this.SkipWhitespace(input, num + 10);
				c = input[num];
				if (c != '=')
				{
					throw new XmlException("Invalid 'version' specification.");
				}
				num++;
				num = this.SkipWhitespace(input, num);
				c = input[num];
				if (c != '"' && c != '\'')
				{
					throw new XmlException("Invalid 'standalone' specification.");
				}
				num2 = input.IndexOf(c, num + 1);
				if (num2 < 0)
				{
					throw new XmlException("Invalid 'standalone' specification.");
				}
				string text = input.Substring(num + 1, num2 - num - 1);
				string text2 = text;
				if (text2 != null)
				{
					if (XmlDeclaration.<>f__switch$map30 == null)
					{
						XmlDeclaration.<>f__switch$map30 = new Dictionary<string, int>(2)
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
					int num3;
					if (XmlDeclaration.<>f__switch$map30.TryGetValue(text2, out num3))
					{
						if (num3 == 0)
						{
							this.Standalone = text;
							num = num2 + 1;
							num = this.SkipWhitespace(input, num);
							goto IL_308;
						}
					}
				}
				throw new XmlException("Invalid standalone specification.");
			}
			IL_308:
			if (num != input.Length)
			{
				throw new XmlException("Invalid XML declaration.");
			}
		}

		// Token: 0x040004C0 RID: 1216
		private string encoding = "UTF-8";

		// Token: 0x040004C1 RID: 1217
		private string standalone;

		// Token: 0x040004C2 RID: 1218
		private string version;
	}
}
