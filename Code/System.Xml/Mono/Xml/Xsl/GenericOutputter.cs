using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200007E RID: 126
	internal class GenericOutputter : Outputter
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0001B668 File Offset: 0x00019868
		private GenericOutputter(Hashtable outputs, Encoding encoding)
		{
			this._encoding = encoding;
			this._outputs = outputs;
			this._currentOutput = (XslOutput)outputs[string.Empty];
			this._state = WriteState.Prolog;
			this._nt = new NameTable();
			this._nsManager = new XmlNamespaceManager(this._nt);
			this._currentNamespaceDecls = new ListDictionary();
			this._omitXmlDeclaration = false;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001B6EC File Offset: 0x000198EC
		public GenericOutputter(XmlWriter writer, Hashtable outputs, Encoding encoding) : this(writer, outputs, encoding, false)
		{
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001B6F8 File Offset: 0x000198F8
		internal GenericOutputter(XmlWriter writer, Hashtable outputs, Encoding encoding, bool isVariable) : this(outputs, encoding)
		{
			this._emitter = new XmlWriterEmitter(writer);
			this._state = writer.WriteState;
			this._omitXmlDeclaration = true;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001B724 File Offset: 0x00019924
		public GenericOutputter(TextWriter writer, Hashtable outputs, Encoding encoding) : this(outputs, encoding)
		{
			this.pendingTextWriter = writer;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001B738 File Offset: 0x00019938
		internal GenericOutputter(TextWriter writer, Hashtable outputs) : this(writer, outputs, null)
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001B744 File Offset: 0x00019944
		internal GenericOutputter(XmlWriter writer, Hashtable outputs) : this(writer, outputs, null)
		{
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0001B750 File Offset: 0x00019950
		private Emitter Emitter
		{
			get
			{
				if (this._emitter == null)
				{
					this.DetermineOutputMethod(null, null);
				}
				return this._emitter;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001B76C File Offset: 0x0001996C
		private void DetermineOutputMethod(string localName, string ns)
		{
			XslOutput xslOutput = (XslOutput)this._outputs[string.Empty];
			switch (xslOutput.Method)
			{
			case OutputMethod.XML:
				goto IL_89;
			case OutputMethod.HTML:
				break;
			case OutputMethod.Text:
				this._emitter = new TextEmitter(this.pendingTextWriter);
				goto IL_11B;
			default:
				if (localName == null || string.Compare(localName, "html", true, CultureInfo.InvariantCulture) != 0 || !(ns == string.Empty))
				{
					goto IL_89;
				}
				break;
			}
			this._emitter = new HtmlEmitter(this.pendingTextWriter, xslOutput);
			goto IL_11B;
			IL_89:
			XmlTextWriter xmlTextWriter = new XmlTextWriter(this.pendingTextWriter);
			if (xslOutput.Indent == "yes")
			{
				xmlTextWriter.Formatting = Formatting.Indented;
			}
			this._emitter = new XmlWriterEmitter(xmlTextWriter);
			if (!this._omitXmlDeclaration && !xslOutput.OmitXmlDeclaration)
			{
				this._emitter.WriteStartDocument((this._encoding == null) ? xslOutput.Encoding : this._encoding, xslOutput.Standalone);
			}
			IL_11B:
			this.pendingTextWriter = null;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001B89C File Offset: 0x00019A9C
		private void CheckState()
		{
			if (this._state == WriteState.Element)
			{
				this._nsManager.PushScope();
				foreach (object obj in this._currentNamespaceDecls.Keys)
				{
					string text = (string)obj;
					string text2 = this._currentNamespaceDecls[text] as string;
					if (!(this._nsManager.LookupNamespace(text, false) == text2))
					{
						this.newNamespaces.Add(text);
						this._nsManager.AddNamespace(text, text2);
					}
				}
				for (int i = 0; i < this.pendingAttributesPos; i++)
				{
					Attribute attribute = this.pendingAttributes[i];
					string text3 = attribute.Prefix;
					if (text3 == "xml" && attribute.Namespace != "http://www.w3.org/XML/1998/namespace")
					{
						text3 = string.Empty;
					}
					string text4 = this._nsManager.LookupPrefix(attribute.Namespace, false);
					if (text3.Length == 0 && attribute.Namespace.Length > 0)
					{
						text3 = text4;
					}
					if (attribute.Namespace.Length > 0 && (text3 == null || text3 == string.Empty))
					{
						text3 = "xp_" + this._xpCount++;
						while (this._nsManager.LookupNamespace(text3) != null)
						{
							text3 = "xp_" + this._xpCount++;
						}
						this.newNamespaces.Add(text3);
						this._currentNamespaceDecls.Add(text3, attribute.Namespace);
						this._nsManager.AddNamespace(text3, attribute.Namespace);
					}
					this.Emitter.WriteAttributeString(text3, attribute.LocalName, attribute.Namespace, attribute.Value);
				}
				for (int j = 0; j < this.newNamespaces.Count; j++)
				{
					string text5 = (string)this.newNamespaces[j];
					string value = this._currentNamespaceDecls[text5] as string;
					if (text5 != string.Empty)
					{
						this.Emitter.WriteAttributeString("xmlns", text5, "http://www.w3.org/2000/xmlns/", value);
					}
					else
					{
						this.Emitter.WriteAttributeString(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", value);
					}
				}
				this._currentNamespaceDecls.Clear();
				this._state = WriteState.Content;
				this.newNamespaces.Clear();
			}
			this._canProcessAttributes = false;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001BB9C File Offset: 0x00019D9C
		public override void WriteStartElement(string prefix, string localName, string nsURI)
		{
			if (this._emitter == null)
			{
				this.DetermineOutputMethod(localName, nsURI);
				if (this.pendingFirstSpaces != null)
				{
					this.WriteWhitespace(this.pendingFirstSpaces.ToString());
					this.pendingFirstSpaces = null;
				}
			}
			if (this._state == WriteState.Prolog && (this._currentOutput.DoctypePublic != null || this._currentOutput.DoctypeSystem != null))
			{
				this.Emitter.WriteDocType(prefix + ((prefix != null) ? string.Empty : ":") + localName, this._currentOutput.DoctypePublic, this._currentOutput.DoctypeSystem);
			}
			this.CheckState();
			if (nsURI == string.Empty)
			{
				prefix = string.Empty;
			}
			this.Emitter.WriteStartElement(prefix, localName, nsURI);
			this._state = WriteState.Element;
			if (this._nsManager.LookupNamespace(prefix, false) != nsURI)
			{
				this._currentNamespaceDecls[prefix] = nsURI;
			}
			this.pendingAttributesPos = 0;
			this._canProcessAttributes = true;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001BCB0 File Offset: 0x00019EB0
		public override void WriteEndElement()
		{
			this.WriteEndElementInternal(false);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001BCBC File Offset: 0x00019EBC
		public override void WriteFullEndElement()
		{
			this.WriteEndElementInternal(true);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		private void WriteEndElementInternal(bool fullEndElement)
		{
			this.CheckState();
			if (fullEndElement)
			{
				this.Emitter.WriteFullEndElement();
			}
			else
			{
				this.Emitter.WriteEndElement();
			}
			this._state = WriteState.Content;
			this._nsManager.PopScope();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001BD10 File Offset: 0x00019F10
		public override void WriteAttributeString(string prefix, string localName, string nsURI, string value)
		{
			for (int i = 0; i < this.pendingAttributesPos; i++)
			{
				Attribute attribute = this.pendingAttributes[i];
				if (attribute.LocalName == localName && attribute.Namespace == nsURI)
				{
					this.pendingAttributes[i].Value = value;
					this.pendingAttributes[i].Prefix = prefix;
					return;
				}
			}
			if (this.pendingAttributesPos == this.pendingAttributes.Length)
			{
				Attribute[] sourceArray = this.pendingAttributes;
				this.pendingAttributes = new Attribute[this.pendingAttributesPos * 2 + 1];
				if (this.pendingAttributesPos > 0)
				{
					Array.Copy(sourceArray, 0, this.pendingAttributes, 0, this.pendingAttributesPos);
				}
			}
			this.pendingAttributes[this.pendingAttributesPos].Prefix = prefix;
			this.pendingAttributes[this.pendingAttributesPos].Namespace = nsURI;
			this.pendingAttributes[this.pendingAttributesPos].LocalName = localName;
			this.pendingAttributes[this.pendingAttributesPos].Value = value;
			this.pendingAttributesPos++;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001BE4C File Offset: 0x0001A04C
		public override void WriteNamespaceDecl(string prefix, string nsUri)
		{
			if (this._nsManager.LookupNamespace(prefix, false) == nsUri)
			{
				return;
			}
			for (int i = 0; i < this.pendingAttributesPos; i++)
			{
				Attribute attribute = this.pendingAttributes[i];
				if (attribute.Prefix == prefix || attribute.Namespace == nsUri)
				{
					return;
				}
			}
			if (this._currentNamespaceDecls[prefix] as string != nsUri)
			{
				this._currentNamespaceDecls[prefix] = nsUri;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		public override void WriteComment(string text)
		{
			this.CheckState();
			this.Emitter.WriteComment(text);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001BF00 File Offset: 0x0001A100
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.CheckState();
			this.Emitter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001BF18 File Offset: 0x0001A118
		public override void WriteString(string text)
		{
			this.CheckState();
			if (this._insideCData)
			{
				this.Emitter.WriteCDataSection(text);
			}
			else if (this._state != WriteState.Content && text.Length > 0 && XmlChar.IsWhitespace(text))
			{
				this.Emitter.WriteWhitespace(text);
			}
			else
			{
				this.Emitter.WriteString(text);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001BF88 File Offset: 0x0001A188
		public override void WriteRaw(string data)
		{
			this.CheckState();
			this.Emitter.WriteRaw(data);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001BF9C File Offset: 0x0001A19C
		public override void WriteWhitespace(string text)
		{
			if (this._emitter == null)
			{
				if (this.pendingFirstSpaces == null)
				{
					this.pendingFirstSpaces = new StringBuilder();
				}
				this.pendingFirstSpaces.Append(text);
				if (this._state == WriteState.Start)
				{
					this._state = WriteState.Prolog;
				}
			}
			else
			{
				this.CheckState();
				this.Emitter.WriteWhitespace(text);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001C000 File Offset: 0x0001A200
		public override void Done()
		{
			this.Emitter.Done();
			this._state = WriteState.Closed;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001C014 File Offset: 0x0001A214
		public override bool CanProcessAttributes
		{
			get
			{
				return this._canProcessAttributes;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001C01C File Offset: 0x0001A21C
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0001C024 File Offset: 0x0001A224
		public override bool InsideCDataSection
		{
			get
			{
				return this._insideCData;
			}
			set
			{
				this._insideCData = value;
			}
		}

		// Token: 0x040002DA RID: 730
		private Hashtable _outputs;

		// Token: 0x040002DB RID: 731
		private XslOutput _currentOutput;

		// Token: 0x040002DC RID: 732
		private Emitter _emitter;

		// Token: 0x040002DD RID: 733
		private TextWriter pendingTextWriter;

		// Token: 0x040002DE RID: 734
		private StringBuilder pendingFirstSpaces;

		// Token: 0x040002DF RID: 735
		private WriteState _state;

		// Token: 0x040002E0 RID: 736
		private Attribute[] pendingAttributes = new Attribute[10];

		// Token: 0x040002E1 RID: 737
		private int pendingAttributesPos;

		// Token: 0x040002E2 RID: 738
		private XmlNamespaceManager _nsManager;

		// Token: 0x040002E3 RID: 739
		private ListDictionary _currentNamespaceDecls;

		// Token: 0x040002E4 RID: 740
		private ArrayList newNamespaces = new ArrayList();

		// Token: 0x040002E5 RID: 741
		private NameTable _nt;

		// Token: 0x040002E6 RID: 742
		private Encoding _encoding;

		// Token: 0x040002E7 RID: 743
		private bool _canProcessAttributes;

		// Token: 0x040002E8 RID: 744
		private bool _insideCData;

		// Token: 0x040002E9 RID: 745
		private bool _omitXmlDeclaration;

		// Token: 0x040002EA RID: 746
		private int _xpCount;
	}
}
