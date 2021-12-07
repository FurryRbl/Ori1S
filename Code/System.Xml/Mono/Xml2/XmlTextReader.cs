using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Mono.Xml;

namespace Mono.Xml2
{
	// Token: 0x0200011F RID: 287
	internal class XmlTextReader : XmlReader, IHasXmlParserContext, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x0003CF94 File Offset: 0x0003B194
		protected XmlTextReader()
		{
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0003CFE8 File Offset: 0x0003B1E8
		public XmlTextReader(Stream input) : this(new XmlStreamReader(input))
		{
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003CFF8 File Offset: 0x0003B1F8
		public XmlTextReader(string url) : this(url, new NameTable())
		{
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003D008 File Offset: 0x0003B208
		public XmlTextReader(TextReader input) : this(input, new NameTable())
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0003D018 File Offset: 0x0003B218
		protected XmlTextReader(XmlNameTable nt) : this(string.Empty, null, XmlNodeType.None, null)
		{
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0003D028 File Offset: 0x0003B228
		public XmlTextReader(Stream input, XmlNameTable nt) : this(new XmlStreamReader(input), nt)
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003D038 File Offset: 0x0003B238
		public XmlTextReader(string url, Stream input) : this(url, new XmlStreamReader(input))
		{
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0003D048 File Offset: 0x0003B248
		public XmlTextReader(string url, TextReader input) : this(url, input, new NameTable())
		{
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003D058 File Offset: 0x0003B258
		public XmlTextReader(string url, XmlNameTable nt)
		{
			string url2;
			Stream streamFromUrl = this.GetStreamFromUrl(url, out url2);
			XmlParserContext context = new XmlParserContext(nt, new XmlNamespaceManager(nt), string.Empty, XmlSpace.None);
			this.InitializeContext(url2, context, new XmlStreamReader(streamFromUrl), XmlNodeType.Document);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003D0D8 File Offset: 0x0003B2D8
		public XmlTextReader(TextReader input, XmlNameTable nt) : this(string.Empty, input, nt)
		{
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0003D0E8 File Offset: 0x0003B2E8
		internal XmlTextReader(bool dummy, XmlResolver resolver, string url, XmlNodeType fragType, XmlParserContext context)
		{
			if (resolver == null)
			{
				resolver = new XmlUrlResolver();
			}
			this.XmlResolver = resolver;
			string url2;
			Stream streamFromUrl = this.GetStreamFromUrl(url, out url2);
			this.InitializeContext(url2, context, new XmlStreamReader(streamFromUrl), fragType);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0003D168 File Offset: 0x0003B368
		public XmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context) : this((context == null) ? string.Empty : context.BaseURI, new XmlStreamReader(xmlFragment), fragType, context)
		{
			this.disallowReset = true;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003D1A0 File Offset: 0x0003B3A0
		internal XmlTextReader(string baseURI, TextReader xmlFragment, XmlNodeType fragType) : this(baseURI, xmlFragment, fragType, null)
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0003D1AC File Offset: 0x0003B3AC
		public XmlTextReader(string url, Stream input, XmlNameTable nt) : this(url, new XmlStreamReader(input), nt)
		{
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0003D1BC File Offset: 0x0003B3BC
		public XmlTextReader(string url, TextReader input, XmlNameTable nt) : this(url, input, XmlNodeType.Document, null)
		{
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0003D1CC File Offset: 0x0003B3CC
		public XmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context) : this((context == null) ? string.Empty : context.BaseURI, new StringReader(xmlFragment), fragType, context)
		{
			this.disallowReset = true;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0003D204 File Offset: 0x0003B404
		internal XmlTextReader(string url, TextReader fragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.InitializeContext(url, context, fragment, fragType);
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0003D260 File Offset: 0x0003B460
		XmlParserContext IHasXmlParserContext.ParserContext
		{
			get
			{
				return this.parserContext;
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0003D268 File Offset: 0x0003B468
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0003D274 File Offset: 0x0003B474
		string IXmlNamespaceResolver.LookupPrefix(string ns)
		{
			return this.LookupPrefix(ns, false);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0003D280 File Offset: 0x0003B480
		private Stream GetStreamFromUrl(string url, out string absoluteUriString)
		{
			Uri uri = this.resolver.ResolveUri(null, url);
			absoluteUriString = ((!(uri != null)) ? string.Empty : uri.ToString());
			return this.resolver.GetEntity(uri, null, typeof(Stream)) as Stream;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0003D2D8 File Offset: 0x0003B4D8
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0003D2E0 File Offset: 0x0003B4E0
		public override string BaseURI
		{
			get
			{
				return this.parserContext.BaseURI;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0003D2F8 File Offset: 0x0003B4F8
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x0003D300 File Offset: 0x0003B500
		internal bool CharacterChecking
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.checkCharacters = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0003D30C File Offset: 0x0003B50C
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x0003D314 File Offset: 0x0003B514
		internal bool CloseInput
		{
			get
			{
				return this.closeInput;
			}
			set
			{
				this.closeInput = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0003D320 File Offset: 0x0003B520
		public override int Depth
		{
			get
			{
				int num = (this.currentToken.NodeType != XmlNodeType.Element) ? -1 : 0;
				if (this.currentAttributeValue >= 0)
				{
					return num + this.elementDepth + 2;
				}
				if (this.currentAttribute >= 0)
				{
					return num + this.elementDepth + 1;
				}
				return this.elementDepth;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0003D37C File Offset: 0x0003B57C
		public Encoding Encoding
		{
			get
			{
				return this.parserContext.Encoding;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0003D38C File Offset: 0x0003B58C
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0003D394 File Offset: 0x0003B594
		public EntityHandling EntityHandling
		{
			get
			{
				return this.entityHandling;
			}
			set
			{
				this.entityHandling = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0003D3A0 File Offset: 0x0003B5A0
		public override bool EOF
		{
			get
			{
				return this.readState == ReadState.EndOfFile;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0003D3AC File Offset: 0x0003B5AC
		public override bool HasValue
		{
			get
			{
				return this.cursorToken.Value != null;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0003D3C4 File Offset: 0x0003B5C4
		public override bool IsEmptyElement
		{
			get
			{
				return this.cursorToken.IsEmptyElement;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0003D3D4 File Offset: 0x0003B5D4
		public int LineNumber
		{
			get
			{
				if (this.useProceedingLineInfo)
				{
					return this.line;
				}
				return this.cursorToken.LineNumber;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
		public int LinePosition
		{
			get
			{
				if (this.useProceedingLineInfo)
				{
					return this.column;
				}
				return this.cursorToken.LinePosition;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0003D414 File Offset: 0x0003B614
		public override string LocalName
		{
			get
			{
				return this.cursorToken.LocalName;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0003D424 File Offset: 0x0003B624
		public override string Name
		{
			get
			{
				return this.cursorToken.Name;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0003D434 File Offset: 0x0003B634
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0003D43C File Offset: 0x0003B63C
		public bool Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				if (this.readState != ReadState.Initial)
				{
					throw new InvalidOperationException("Namespaces have to be set before reading.");
				}
				this.namespaces = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0003D45C File Offset: 0x0003B65C
		public override string NamespaceURI
		{
			get
			{
				return this.cursorToken.NamespaceURI;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0003D46C File Offset: 0x0003B66C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0003D474 File Offset: 0x0003B674
		public override XmlNodeType NodeType
		{
			get
			{
				return this.cursorToken.NodeType;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0003D484 File Offset: 0x0003B684
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0003D48C File Offset: 0x0003B68C
		public bool Normalization
		{
			get
			{
				return this.normalization;
			}
			set
			{
				this.normalization = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0003D498 File Offset: 0x0003B698
		public override string Prefix
		{
			get
			{
				return this.cursorToken.Prefix;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0003D4B0 File Offset: 0x0003B6B0
		public bool ProhibitDtd
		{
			get
			{
				return this.prohibitDtd;
			}
			set
			{
				this.prohibitDtd = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0003D4BC File Offset: 0x0003B6BC
		public override char QuoteChar
		{
			get
			{
				return this.cursorToken.QuoteChar;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0003D4CC File Offset: 0x0003B6CC
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0003D4D4 File Offset: 0x0003B6D4
		public override XmlReaderSettings Settings
		{
			get
			{
				return base.Settings;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		public override string Value
		{
			get
			{
				return (this.cursorToken.Value == null) ? string.Empty : this.cursorToken.Value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0003D504 File Offset: 0x0003B704
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x0003D50C File Offset: 0x0003B70C
		public WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.whitespaceHandling;
			}
			set
			{
				this.whitespaceHandling = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0003D518 File Offset: 0x0003B718
		public override string XmlLang
		{
			get
			{
				return this.parserContext.XmlLang;
			}
		}

		// Token: 0x1700039D RID: 925
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x0003D528 File Offset: 0x0003B728
		public XmlResolver XmlResolver
		{
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0003D534 File Offset: 0x0003B734
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.parserContext.XmlSpace;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0003D544 File Offset: 0x0003B744
		public override void Close()
		{
			this.readState = ReadState.Closed;
			this.cursorToken.Clear();
			this.currentToken.Clear();
			this.attributeCount = 0;
			if (this.closeInput && this.reader != null)
			{
				this.reader.Close();
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0003D598 File Offset: 0x0003B798
		public override string GetAttribute(int i)
		{
			if (i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i is smaller than AttributeCount");
			}
			return this.attributeTokens[i].Value;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0003D5CC File Offset: 0x0003B7CC
		public override string GetAttribute(string name)
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (this.attributeTokens[i].Name == name)
				{
					return this.attributeTokens[i].Value;
				}
			}
			return null;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0003D618 File Offset: 0x0003B818
		private int GetIndexOfQualifiedAttribute(string localName, string namespaceURI)
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				XmlTextReader.XmlAttributeTokenInfo xmlAttributeTokenInfo = this.attributeTokens[i];
				if (xmlAttributeTokenInfo.LocalName == localName && xmlAttributeTokenInfo.NamespaceURI == namespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0003D66C File Offset: 0x0003B86C
		public override string GetAttribute(string localName, string namespaceURI)
		{
			int indexOfQualifiedAttribute = this.GetIndexOfQualifiedAttribute(localName, namespaceURI);
			if (indexOfQualifiedAttribute < 0)
			{
				return null;
			}
			return this.attributeTokens[indexOfQualifiedAttribute].Value;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0003D698 File Offset: 0x0003B898
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.nsmgr.GetNamespacesInScope(scope);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
		public TextReader GetRemainder()
		{
			if (this.peekCharsLength < 0)
			{
				return this.reader;
			}
			return new StringReader(new string(this.peekChars, this.peekCharsIndex, this.peekCharsLength - this.peekCharsIndex) + this.reader.ReadToEnd());
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0003D700 File Offset: 0x0003B900
		public override string LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix, false);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0003D70C File Offset: 0x0003B90C
		private string LookupNamespace(string prefix, bool atomizedNames)
		{
			string text = this.nsmgr.LookupNamespace(prefix, atomizedNames);
			return (!(text == string.Empty)) ? text : null;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003D740 File Offset: 0x0003B940
		public string LookupPrefix(string ns, bool atomizedName)
		{
			return this.nsmgr.LookupPrefix(ns, atomizedName);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0003D750 File Offset: 0x0003B950
		public override void MoveToAttribute(int i)
		{
			if (i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("attribute index out of range.");
			}
			this.currentAttribute = i;
			this.currentAttributeValue = -1;
			this.cursorToken = this.attributeTokens[i];
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003D788 File Offset: 0x0003B988
		public override bool MoveToAttribute(string name)
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				XmlTextReader.XmlAttributeTokenInfo xmlAttributeTokenInfo = this.attributeTokens[i];
				if (xmlAttributeTokenInfo.Name == name)
				{
					this.MoveToAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0003D7D0 File Offset: 0x0003B9D0
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			int indexOfQualifiedAttribute = this.GetIndexOfQualifiedAttribute(localName, namespaceName);
			if (indexOfQualifiedAttribute < 0)
			{
				return false;
			}
			this.MoveToAttribute(indexOfQualifiedAttribute);
			return true;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003D7F8 File Offset: 0x0003B9F8
		public override bool MoveToElement()
		{
			if (this.currentToken == null)
			{
				return false;
			}
			if (this.cursorToken == this.currentToken)
			{
				return false;
			}
			if (this.currentAttribute >= 0)
			{
				this.currentAttribute = -1;
				this.currentAttributeValue = -1;
				this.cursorToken = this.currentToken;
				return true;
			}
			return false;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0003D850 File Offset: 0x0003BA50
		public override bool MoveToFirstAttribute()
		{
			if (this.attributeCount == 0)
			{
				return false;
			}
			this.MoveToElement();
			return this.MoveToNextAttribute();
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0003D86C File Offset: 0x0003BA6C
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttribute == 0 && this.attributeCount == 0)
			{
				return false;
			}
			if (this.currentAttribute + 1 < this.attributeCount)
			{
				this.currentAttribute++;
				this.currentAttributeValue = -1;
				this.cursorToken = this.attributeTokens[this.currentAttribute];
				return true;
			}
			return false;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		public override bool Read()
		{
			if (this.readState == ReadState.Closed)
			{
				return false;
			}
			this.curNodePeekIndex = this.peekCharsIndex;
			this.preserveCurrentTag = true;
			this.nestLevel = 0;
			this.ClearValueBuffer();
			if (this.startNodeType == XmlNodeType.Attribute)
			{
				if (this.currentAttribute == 0)
				{
					return false;
				}
				this.SkipTextDeclaration();
				this.ClearAttributes();
				this.IncrementAttributeToken();
				this.ReadAttributeValueTokens(34);
				this.cursorToken = this.attributeTokens[0];
				this.currentAttributeValue = -1;
				this.readState = ReadState.Interactive;
				return true;
			}
			else
			{
				if (this.readState == ReadState.Initial && this.currentState == XmlNodeType.Element)
				{
					this.SkipTextDeclaration();
				}
				if (base.Binary != null)
				{
					base.Binary.Reset();
				}
				this.readState = ReadState.Interactive;
				this.currentLinkedNodeLineNumber = this.line;
				this.currentLinkedNodeLinePosition = this.column;
				this.useProceedingLineInfo = true;
				this.cursorToken = this.currentToken;
				this.attributeCount = 0;
				this.currentAttribute = (this.currentAttributeValue = -1);
				this.currentToken.Clear();
				if (this.depthUp)
				{
					this.depth++;
					this.depthUp = false;
				}
				if (this.readCharsInProgress)
				{
					this.readCharsInProgress = false;
					return this.ReadUntilEndTag();
				}
				bool flag = this.ReadContent();
				if (!flag && this.startNodeType == XmlNodeType.Document && this.currentState != XmlNodeType.EndElement)
				{
					throw this.NotWFError("Document element did not appear.");
				}
				this.useProceedingLineInfo = false;
				return flag;
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0003DA5C File Offset: 0x0003BC5C
		public override bool ReadAttributeValue()
		{
			if (this.readState == ReadState.Initial && this.startNodeType == XmlNodeType.Attribute)
			{
				this.Read();
			}
			if (this.currentAttribute < 0)
			{
				return false;
			}
			XmlTextReader.XmlAttributeTokenInfo xmlAttributeTokenInfo = this.attributeTokens[this.currentAttribute];
			if (this.currentAttributeValue < 0)
			{
				this.currentAttributeValue = xmlAttributeTokenInfo.ValueTokenStartIndex - 1;
			}
			if (this.currentAttributeValue < xmlAttributeTokenInfo.ValueTokenEndIndex)
			{
				this.currentAttributeValue++;
				this.cursorToken = this.attributeValueTokens[this.currentAttributeValue];
				return true;
			}
			return false;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003DAF4 File Offset: 0x0003BCF4
		public int ReadBase64(byte[] buffer, int offset, int length)
		{
			base.BinaryCharGetter = this.binaryCharGetter;
			int result;
			try
			{
				result = base.Binary.ReadBase64(buffer, offset, length);
			}
			finally
			{
				base.BinaryCharGetter = null;
			}
			return result;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0003DB4C File Offset: 0x0003BD4C
		public int ReadBinHex(byte[] buffer, int offset, int length)
		{
			base.BinaryCharGetter = this.binaryCharGetter;
			int result;
			try
			{
				result = base.Binary.ReadBinHex(buffer, offset, length);
			}
			finally
			{
				base.BinaryCharGetter = null;
			}
			return result;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0003DBA4 File Offset: 0x0003BDA4
		public int ReadChars(char[] buffer, int offset, int length)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset must be non-negative integer.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Length must be non-negative integer.");
			}
			if (buffer.Length < offset + length)
			{
				throw new ArgumentOutOfRangeException("buffer length is smaller than the sum of offset and length.");
			}
			if (this.IsEmptyElement)
			{
				this.Read();
				return 0;
			}
			if (!this.readCharsInProgress && this.NodeType != XmlNodeType.Element)
			{
				return 0;
			}
			this.preserveCurrentTag = false;
			this.readCharsInProgress = true;
			this.useProceedingLineInfo = true;
			return this.ReadCharsInternal(buffer, offset, length);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0003DC4C File Offset: 0x0003BE4C
		public void ResetState()
		{
			if (this.disallowReset)
			{
				throw new InvalidOperationException("Cannot call ResetState when parsing an XML fragment.");
			}
			this.Clear();
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0003DC6C File Offset: 0x0003BE6C
		public override void ResolveEntity()
		{
			throw new InvalidOperationException("XmlTextReader cannot resolve external entities.");
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0003DC78 File Offset: 0x0003BE78
		[MonoTODO]
		public override void Skip()
		{
			base.Skip();
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0003DC80 File Offset: 0x0003BE80
		internal DTDObjectModel DTD
		{
			get
			{
				return this.parserContext.Dtd;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0003DC90 File Offset: 0x0003BE90
		internal XmlResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0003DC98 File Offset: 0x0003BE98
		private XmlException NotWFError(string message)
		{
			return new XmlException(this, this.BaseURI, message);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0003DCA8 File Offset: 0x0003BEA8
		private void Init()
		{
			this.allowMultipleRoot = false;
			this.elementNames = new XmlTextReader.TagName[10];
			this.valueBuffer = new StringBuilder();
			this.binaryCharGetter = new XmlReaderBinarySupport.CharGetter(this.ReadChars);
			this.checkCharacters = true;
			if (this.Settings != null)
			{
				this.checkCharacters = this.Settings.CheckCharacters;
			}
			this.prohibitDtd = false;
			this.closeInput = true;
			this.entityHandling = EntityHandling.ExpandCharEntities;
			this.peekCharsIndex = 0;
			if (this.peekChars == null)
			{
				this.peekChars = new char[1024];
			}
			this.peekCharsLength = -1;
			this.curNodePeekIndex = -1;
			this.line = 1;
			this.column = 1;
			this.currentLinkedNodeLineNumber = (this.currentLinkedNodeLinePosition = 0);
			this.Clear();
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0003DD74 File Offset: 0x0003BF74
		private void Clear()
		{
			this.currentToken = new XmlTextReader.XmlTokenInfo(this);
			this.cursorToken = this.currentToken;
			this.currentAttribute = -1;
			this.currentAttributeValue = -1;
			this.attributeCount = 0;
			this.readState = ReadState.Initial;
			this.depth = 0;
			this.elementDepth = 0;
			this.depthUp = false;
			this.popScope = (this.allowMultipleRoot = false);
			this.elementNameStackPos = 0;
			this.isStandalone = false;
			this.returnEntityReference = false;
			this.entityReferenceName = string.Empty;
			this.useProceedingLineInfo = false;
			this.currentState = XmlNodeType.None;
			this.readCharsInProgress = false;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0003DE10 File Offset: 0x0003C010
		private void InitializeContext(string url, XmlParserContext context, TextReader fragment, XmlNodeType fragType)
		{
			this.startNodeType = fragType;
			this.parserContext = context;
			if (context == null)
			{
				XmlNameTable nt = new NameTable();
				this.parserContext = new XmlParserContext(nt, new XmlNamespaceManager(nt), string.Empty, XmlSpace.None);
			}
			this.nameTable = this.parserContext.NameTable;
			this.nameTable = ((this.nameTable == null) ? new NameTable() : this.nameTable);
			this.nsmgr = this.parserContext.NamespaceManager;
			this.nsmgr = ((this.nsmgr == null) ? new XmlNamespaceManager(this.nameTable) : this.nsmgr);
			if (url != null && url.Length > 0)
			{
				Uri uri = null;
				try
				{
					uri = new Uri(url, UriKind.RelativeOrAbsolute);
				}
				catch (Exception)
				{
					string fullPath = Path.GetFullPath("./a");
					uri = new Uri(new Uri(fullPath), url);
				}
				this.parserContext.BaseURI = uri.ToString();
			}
			this.Init();
			this.reader = fragment;
			if (fragType != XmlNodeType.Element)
			{
				if (fragType != XmlNodeType.Attribute)
				{
					if (fragType != XmlNodeType.Document)
					{
						throw new XmlException(string.Format("NodeType {0} is not allowed to create XmlTextReader.", fragType));
					}
				}
				else
				{
					this.reader = new StringReader(fragment.ReadToEnd().Replace("\"", "&quot;"));
				}
			}
			else
			{
				this.currentState = XmlNodeType.Element;
				this.allowMultipleRoot = true;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0003DFA8 File Offset: 0x0003C1A8
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0003DFBC File Offset: 0x0003C1BC
		internal ConformanceLevel Conformance
		{
			get
			{
				return (!this.allowMultipleRoot) ? ConformanceLevel.Document : ConformanceLevel.Fragment;
			}
			set
			{
				if (value == ConformanceLevel.Fragment)
				{
					this.currentState = XmlNodeType.Element;
					this.allowMultipleRoot = true;
				}
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0003DFD4 File Offset: 0x0003C1D4
		internal void AdjustLineInfoOffset(int lineNumberOffset, int linePositionOffset)
		{
			this.line += lineNumberOffset;
			this.column += linePositionOffset;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0003DFF4 File Offset: 0x0003C1F4
		internal void SetNameTable(XmlNameTable nameTable)
		{
			this.parserContext.NameTable = nameTable;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0003E004 File Offset: 0x0003C204
		private void SetProperties(XmlNodeType nodeType, string name, string prefix, string localName, bool isEmptyElement, string value, bool clearAttributes)
		{
			this.SetTokenProperties(this.currentToken, nodeType, name, prefix, localName, isEmptyElement, value, clearAttributes);
			this.currentToken.LineNumber = this.currentLinkedNodeLineNumber;
			this.currentToken.LinePosition = this.currentLinkedNodeLinePosition;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0003E04C File Offset: 0x0003C24C
		private void SetTokenProperties(XmlTextReader.XmlTokenInfo token, XmlNodeType nodeType, string name, string prefix, string localName, bool isEmptyElement, string value, bool clearAttributes)
		{
			token.NodeType = nodeType;
			token.Name = name;
			token.Prefix = prefix;
			token.LocalName = localName;
			token.IsEmptyElement = isEmptyElement;
			token.Value = value;
			this.elementDepth = this.depth;
			if (clearAttributes)
			{
				this.ClearAttributes();
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
		private void ClearAttributes()
		{
			this.attributeCount = 0;
			this.currentAttribute = -1;
			this.currentAttributeValue = -1;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
		private int PeekSurrogate(int c)
		{
			if (this.peekCharsLength <= this.peekCharsIndex + 1 && !this.ReadTextReader(c))
			{
				return c;
			}
			int num = (int)this.peekChars[this.peekCharsIndex];
			int num2 = (int)this.peekChars[this.peekCharsIndex + 1];
			if ((num & 64512) != 55296 || (num2 & 64512) != 56320)
			{
				return num;
			}
			return 65536 + (num - 55296) * 1024 + (num2 - 56320);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003E144 File Offset: 0x0003C344
		private int PeekChar()
		{
			if (this.peekCharsIndex < this.peekCharsLength)
			{
				int num = (int)this.peekChars[this.peekCharsIndex];
				if (num == 0)
				{
					return -1;
				}
				if (num < 55296 || num >= 57343)
				{
					return num;
				}
				return this.PeekSurrogate(num);
			}
			else
			{
				if (!this.ReadTextReader(-1))
				{
					return -1;
				}
				return this.PeekChar();
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0003E1AC File Offset: 0x0003C3AC
		private int ReadChar()
		{
			int num = this.PeekChar();
			this.peekCharsIndex++;
			if (num >= 65536)
			{
				this.peekCharsIndex++;
			}
			if (num == 10)
			{
				this.line++;
				this.column = 1;
			}
			else if (num != -1)
			{
				this.column++;
			}
			return num;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0003E220 File Offset: 0x0003C420
		private void Advance(int ch)
		{
			this.peekCharsIndex++;
			if (ch >= 65536)
			{
				this.peekCharsIndex++;
			}
			if (ch == 10)
			{
				this.line++;
				this.column = 1;
			}
			else if (ch != -1)
			{
				this.column++;
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003E28C File Offset: 0x0003C48C
		private bool ReadTextReader(int remained)
		{
			if (this.peekCharsLength < 0)
			{
				this.peekCharsLength = this.reader.Read(this.peekChars, 0, this.peekChars.Length);
				return this.peekCharsLength > 0;
			}
			int num = (remained < 0) ? 0 : 1;
			int length = this.peekCharsLength - this.curNodePeekIndex;
			if (!this.preserveCurrentTag)
			{
				this.curNodePeekIndex = 0;
				this.peekCharsIndex = 0;
			}
			else if (this.peekCharsLength >= this.peekChars.Length)
			{
				if (this.curNodePeekIndex <= this.peekCharsLength >> 1)
				{
					char[] destinationArray = new char[this.peekChars.Length * 2];
					Array.Copy(this.peekChars, this.curNodePeekIndex, destinationArray, 0, length);
					this.peekChars = destinationArray;
					this.curNodePeekIndex = 0;
					this.peekCharsIndex = length;
				}
				else
				{
					Array.Copy(this.peekChars, this.curNodePeekIndex, this.peekChars, 0, length);
					this.curNodePeekIndex = 0;
					this.peekCharsIndex = length;
				}
			}
			if (remained >= 0)
			{
				this.peekChars[this.peekCharsIndex] = (char)remained;
			}
			int num2 = this.peekChars.Length - this.peekCharsIndex - num;
			if (num2 > 1024)
			{
				num2 = 1024;
			}
			int num3 = this.reader.Read(this.peekChars, this.peekCharsIndex + num, num2);
			int num4 = num + num3;
			this.peekCharsLength = this.peekCharsIndex + num4;
			return num4 != 0;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0003E40C File Offset: 0x0003C60C
		private bool ReadContent()
		{
			if (this.popScope)
			{
				this.nsmgr.PopScope();
				this.parserContext.PopScope();
				this.popScope = false;
			}
			if (this.returnEntityReference)
			{
				this.SetEntityReferenceProperties();
			}
			else
			{
				int num = this.PeekChar();
				if (num == -1)
				{
					this.readState = ReadState.EndOfFile;
					this.ClearValueBuffer();
					this.SetProperties(XmlNodeType.None, string.Empty, string.Empty, string.Empty, false, null, true);
					if (this.depth > 0)
					{
						throw this.NotWFError("unexpected end of file. Current depth is " + this.depth);
					}
					return false;
				}
				else
				{
					int num2 = num;
					switch (num2)
					{
					case 9:
					case 10:
					case 13:
						break;
					default:
						if (num2 != 32)
						{
							if (num2 != 60)
							{
								this.ReadText(true);
								goto IL_168;
							}
							this.Advance(num);
							int num3 = this.PeekChar();
							if (num3 != 33)
							{
								if (num3 != 47)
								{
									if (num3 != 63)
									{
										this.ReadStartTag();
									}
									else
									{
										this.Advance(63);
										this.ReadProcessingInstruction();
									}
								}
								else
								{
									this.Advance(47);
									this.ReadEndTag();
								}
							}
							else
							{
								this.Advance(33);
								this.ReadDeclaration();
							}
							goto IL_168;
						}
						break;
					}
					if (!this.ReadWhitespace())
					{
						return this.ReadContent();
					}
				}
			}
			IL_168:
			return this.ReadState != ReadState.EndOfFile;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003E590 File Offset: 0x0003C790
		private void SetEntityReferenceProperties()
		{
			DTDEntityDeclaration dtdentityDeclaration = (this.DTD == null) ? null : this.DTD.EntityDecls[this.entityReferenceName];
			if (this.isStandalone && (this.DTD == null || dtdentityDeclaration == null || !dtdentityDeclaration.IsInternalSubset))
			{
				throw this.NotWFError("Standalone document must not contain any references to an non-internally declared entity.");
			}
			if (dtdentityDeclaration != null && dtdentityDeclaration.NotationName != null)
			{
				throw this.NotWFError("Reference to any unparsed entities is not allowed here.");
			}
			this.ClearValueBuffer();
			this.SetProperties(XmlNodeType.EntityReference, this.entityReferenceName, string.Empty, this.entityReferenceName, false, null, true);
			this.returnEntityReference = false;
			this.entityReferenceName = string.Empty;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0003E648 File Offset: 0x0003C848
		private void ReadStartTag()
		{
			if (this.currentState == XmlNodeType.EndElement)
			{
				throw this.NotWFError("Multiple document element was detected.");
			}
			this.currentState = XmlNodeType.Element;
			this.nsmgr.PushScope();
			this.currentLinkedNodeLineNumber = this.line;
			this.currentLinkedNodeLinePosition = this.column;
			string text;
			string text2;
			string name = this.ReadName(out text, out text2);
			if (this.currentState == XmlNodeType.EndElement)
			{
				throw this.NotWFError("document has terminated, cannot open new element");
			}
			bool isEmptyElement = false;
			this.ClearAttributes();
			this.SkipWhitespace();
			if (XmlChar.IsFirstNameChar(this.PeekChar()))
			{
				this.ReadAttributes(false);
			}
			this.cursorToken = this.currentToken;
			for (int i = 0; i < this.attributeCount; i++)
			{
				this.attributeTokens[i].FillXmlns();
			}
			for (int j = 0; j < this.attributeCount; j++)
			{
				this.attributeTokens[j].FillNamespace();
			}
			if (this.namespaces)
			{
				for (int k = 0; k < this.attributeCount; k++)
				{
					if (this.attributeTokens[k].Prefix == "xmlns" && this.attributeTokens[k].Value == string.Empty)
					{
						throw this.NotWFError("Empty namespace URI cannot be mapped to non-empty prefix.");
					}
				}
			}
			for (int l = 0; l < this.attributeCount; l++)
			{
				for (int m = l + 1; m < this.attributeCount; m++)
				{
					if (object.ReferenceEquals(this.attributeTokens[l].Name, this.attributeTokens[m].Name) || (object.ReferenceEquals(this.attributeTokens[l].LocalName, this.attributeTokens[m].LocalName) && object.ReferenceEquals(this.attributeTokens[l].NamespaceURI, this.attributeTokens[m].NamespaceURI)))
					{
						throw this.NotWFError("Attribute name and qualified name must be identical.");
					}
				}
			}
			if (this.PeekChar() == 47)
			{
				this.Advance(47);
				isEmptyElement = true;
				this.popScope = true;
			}
			else
			{
				this.depthUp = true;
				this.PushElementName(name, text2, text);
			}
			this.parserContext.PushScope();
			this.Expect(62);
			this.SetProperties(XmlNodeType.Element, name, text, text2, isEmptyElement, null, false);
			if (text.Length > 0)
			{
				this.currentToken.NamespaceURI = this.LookupNamespace(text, true);
			}
			else if (this.namespaces)
			{
				this.currentToken.NamespaceURI = this.nsmgr.DefaultNamespace;
			}
			if (this.namespaces)
			{
				if (this.NamespaceURI == null)
				{
					throw this.NotWFError(string.Format("'{0}' is undeclared namespace.", this.Prefix));
				}
				try
				{
					for (int n = 0; n < this.attributeCount; n++)
					{
						this.MoveToAttribute(n);
						if (this.NamespaceURI == null)
						{
							throw this.NotWFError(string.Format("'{0}' is undeclared namespace.", this.Prefix));
						}
					}
				}
				finally
				{
					this.MoveToElement();
				}
			}
			for (int num = 0; num < this.attributeCount; num++)
			{
				if (object.ReferenceEquals(this.attributeTokens[num].Prefix, "xml"))
				{
					string localName = this.attributeTokens[num].LocalName;
					string value = this.attributeTokens[num].Value;
					string text3 = localName;
					switch (text3)
					{
					case "base":
						if (this.resolver != null)
						{
							Uri baseUri = (!(this.BaseURI != string.Empty)) ? null : new Uri(this.BaseURI);
							Uri uri = this.resolver.ResolveUri(baseUri, value);
							this.parserContext.BaseURI = ((!(uri != null)) ? string.Empty : uri.ToString());
						}
						else
						{
							this.parserContext.BaseURI = value;
						}
						break;
					case "lang":
						this.parserContext.XmlLang = value;
						break;
					case "space":
					{
						string text4 = value;
						if (text4 != null)
						{
							if (XmlTextReader.<>f__switch$map38 == null)
							{
								XmlTextReader.<>f__switch$map38 = new Dictionary<string, int>(2)
								{
									{
										"preserve",
										0
									},
									{
										"default",
										1
									}
								};
							}
							int num3;
							if (XmlTextReader.<>f__switch$map38.TryGetValue(text4, out num3))
							{
								if (num3 != 0)
								{
									if (num3 != 1)
									{
										goto IL_502;
									}
									this.parserContext.XmlSpace = XmlSpace.Default;
								}
								else
								{
									this.parserContext.XmlSpace = XmlSpace.Preserve;
								}
								break;
							}
						}
						IL_502:
						throw this.NotWFError(string.Format("Invalid xml:space value: {0}", value));
					}
					}
				}
			}
			if (this.IsEmptyElement)
			{
				this.CheckCurrentStateUpdate();
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
		private void PushElementName(string name, string local, string prefix)
		{
			if (this.elementNames.Length == this.elementNameStackPos)
			{
				XmlTextReader.TagName[] destinationArray = new XmlTextReader.TagName[this.elementNames.Length * 2];
				Array.Copy(this.elementNames, 0, destinationArray, 0, this.elementNameStackPos);
				this.elementNames = destinationArray;
			}
			this.elementNames[this.elementNameStackPos++] = new XmlTextReader.TagName(name, local, prefix);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003EC24 File Offset: 0x0003CE24
		private void ReadEndTag()
		{
			if (this.currentState != XmlNodeType.Element)
			{
				throw this.NotWFError("End tag cannot appear in this state.");
			}
			this.currentLinkedNodeLineNumber = this.line;
			this.currentLinkedNodeLinePosition = this.column;
			if (this.elementNameStackPos == 0)
			{
				throw this.NotWFError("closing element without matching opening element");
			}
			XmlTextReader.TagName tagName = this.elementNames[--this.elementNameStackPos];
			this.Expect(tagName.Name);
			this.ExpectAfterWhitespace('>');
			this.depth--;
			this.SetProperties(XmlNodeType.EndElement, tagName.Name, tagName.Prefix, tagName.LocalName, false, null, true);
			if (tagName.Prefix.Length > 0)
			{
				this.currentToken.NamespaceURI = this.LookupNamespace(tagName.Prefix, true);
			}
			else if (this.namespaces)
			{
				this.currentToken.NamespaceURI = this.nsmgr.DefaultNamespace;
			}
			this.popScope = true;
			this.CheckCurrentStateUpdate();
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		private void CheckCurrentStateUpdate()
		{
			if (this.depth == 0 && !this.allowMultipleRoot && (this.IsEmptyElement || this.NodeType == XmlNodeType.EndElement))
			{
				this.currentState = XmlNodeType.EndElement;
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003ED80 File Offset: 0x0003CF80
		private void AppendValueChar(int ch)
		{
			if (ch < 65535)
			{
				this.valueBuffer.Append((char)ch);
			}
			else
			{
				this.AppendSurrogatePairValueChar(ch);
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003EDA8 File Offset: 0x0003CFA8
		private void AppendSurrogatePairValueChar(int ch)
		{
			this.valueBuffer.Append((char)((ch - 65536) / 1024 + 55296));
			this.valueBuffer.Append((char)((ch - 65536) % 1024 + 56320));
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003EDF8 File Offset: 0x0003CFF8
		private string CreateValueString()
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Whitespace || nodeType == XmlNodeType.SignificantWhitespace)
			{
				int length = this.valueBuffer.Length;
				if (this.whitespaceCache == null)
				{
					this.whitespaceCache = new char[32];
				}
				if (length < this.whitespaceCache.Length)
				{
					if (this.whitespacePool == null)
					{
						this.whitespacePool = new NameTable();
					}
					this.valueBuffer.CopyTo(0, this.whitespaceCache, 0, length);
					return this.whitespacePool.Add(this.whitespaceCache, 0, this.valueBuffer.Length);
				}
			}
			return (this.valueBuffer.Capacity >= 100) ? this.valueBuffer.ToString() : this.valueBuffer.ToString(0, this.valueBuffer.Length);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003EEDC File Offset: 0x0003D0DC
		private void ClearValueBuffer()
		{
			this.valueBuffer.Length = 0;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003EEEC File Offset: 0x0003D0EC
		private void ReadText(bool notWhitespace)
		{
			if (this.currentState != XmlNodeType.Element)
			{
				throw this.NotWFError("Text node cannot appear in this state.");
			}
			this.preserveCurrentTag = false;
			if (notWhitespace)
			{
				this.ClearValueBuffer();
			}
			int num = this.PeekChar();
			bool flag = false;
			while (num != 60 && num != -1)
			{
				if (num == 38)
				{
					this.ReadChar();
					num = this.ReadReference(false);
					if (this.returnEntityReference)
					{
						break;
					}
				}
				else
				{
					if (this.normalization && num == 13)
					{
						this.ReadChar();
						num = this.PeekChar();
						if (num != 10)
						{
							this.AppendValueChar(10);
						}
						continue;
					}
					if (this.CharacterChecking && XmlChar.IsInvalid(num))
					{
						throw this.NotWFError("Not allowed character was found.");
					}
					num = this.ReadChar();
				}
				if (num < 65535)
				{
					this.valueBuffer.Append((char)num);
				}
				else
				{
					this.AppendSurrogatePairValueChar(num);
				}
				if (num == 93)
				{
					if (flag && this.PeekChar() == 62)
					{
						throw this.NotWFError("Inside text content, character sequence ']]>' is not allowed.");
					}
					flag = true;
				}
				else if (flag)
				{
					flag = false;
				}
				num = this.PeekChar();
				notWhitespace = true;
			}
			if (this.returnEntityReference && this.valueBuffer.Length == 0)
			{
				this.SetEntityReferenceProperties();
			}
			else
			{
				XmlNodeType nodeType = (!notWhitespace) ? ((this.XmlSpace != XmlSpace.Preserve) ? XmlNodeType.Whitespace : XmlNodeType.SignificantWhitespace) : XmlNodeType.Text;
				this.SetProperties(nodeType, string.Empty, string.Empty, string.Empty, false, null, true);
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003F094 File Offset: 0x0003D294
		private int ReadReference(bool ignoreEntityReferences)
		{
			if (this.PeekChar() == 35)
			{
				this.Advance(35);
				return this.ReadCharacterReference();
			}
			return this.ReadEntityReference(ignoreEntityReferences);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003F0C4 File Offset: 0x0003D2C4
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.PeekChar() == 120)
			{
				this.Advance(120);
				int num2;
				while ((num2 = this.PeekChar()) != 59 && num2 != -1)
				{
					this.Advance(num2);
					if (num2 >= 48 && num2 <= 57)
					{
						num = (num << 4) + num2 - 48;
					}
					else if (num2 >= 65 && num2 <= 70)
					{
						num = (num << 4) + num2 - 65 + 10;
					}
					else
					{
						if (num2 < 97 || num2 > 102)
						{
							throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "invalid hexadecimal digit: {0} (#x{1:X})", new object[]
							{
								(char)num2,
								num2
							}));
						}
						num = (num << 4) + num2 - 97 + 10;
					}
				}
			}
			else
			{
				int num2;
				while ((num2 = this.PeekChar()) != 59 && num2 != -1)
				{
					this.Advance(num2);
					if (num2 < 48 || num2 > 57)
					{
						throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "invalid decimal digit: {0} (#x{1:X})", new object[]
						{
							(char)num2,
							num2
						}));
					}
					num = num * 10 + num2 - 48;
				}
			}
			this.ReadChar();
			if (this.CharacterChecking && this.Normalization && XmlChar.IsInvalid(num))
			{
				throw this.NotWFError(string.Concat(new object[]
				{
					"Referenced character was not allowed in XML. Normalization is ",
					this.normalization,
					", checkCharacters = ",
					this.checkCharacters
				}));
			}
			return num;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0003F278 File Offset: 0x0003D478
		private int ReadEntityReference(bool ignoreEntityReferences)
		{
			string text = this.ReadName();
			this.Expect(59);
			int predefinedEntity = XmlChar.GetPredefinedEntity(text);
			if (predefinedEntity >= 0)
			{
				return predefinedEntity;
			}
			if (ignoreEntityReferences)
			{
				this.AppendValueChar(38);
				for (int i = 0; i < text.Length; i++)
				{
					this.AppendValueChar((int)text[i]);
				}
				this.AppendValueChar(59);
			}
			else
			{
				this.returnEntityReference = true;
				this.entityReferenceName = text;
			}
			return -1;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0003F2F4 File Offset: 0x0003D4F4
		private void ReadAttributes(bool isXmlDecl)
		{
			bool flag = false;
			this.currentAttribute = -1;
			this.currentAttributeValue = -1;
			while (this.SkipWhitespace() || !flag)
			{
				this.IncrementAttributeToken();
				this.currentAttributeToken.LineNumber = this.line;
				this.currentAttributeToken.LinePosition = this.column;
				string prefix;
				string localName;
				this.currentAttributeToken.Name = this.ReadName(out prefix, out localName);
				this.currentAttributeToken.Prefix = prefix;
				this.currentAttributeToken.LocalName = localName;
				this.ExpectAfterWhitespace('=');
				this.SkipWhitespace();
				this.ReadAttributeValueTokens(-1);
				if (isXmlDecl)
				{
					string value = this.currentAttributeToken.Value;
				}
				this.attributeCount++;
				if (!this.SkipWhitespace())
				{
					flag = true;
				}
				int num = this.PeekChar();
				if (isXmlDecl)
				{
					if (num == 63)
					{
						goto IL_103;
					}
				}
				else if (num == 47 || num == 62)
				{
					goto IL_103;
				}
				if (num != -1)
				{
					continue;
				}
				IL_103:
				this.currentAttribute = -1;
				this.currentAttributeValue = -1;
				return;
			}
			throw this.NotWFError("Unexpected token. Name is required here.");
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003F414 File Offset: 0x0003D614
		private void AddAttributeWithValue(string name, string value)
		{
			this.IncrementAttributeToken();
			XmlTextReader.XmlAttributeTokenInfo xmlAttributeTokenInfo = this.attributeTokens[this.currentAttribute];
			xmlAttributeTokenInfo.Name = this.NameTable.Add(name);
			xmlAttributeTokenInfo.Prefix = string.Empty;
			xmlAttributeTokenInfo.NamespaceURI = string.Empty;
			this.IncrementAttributeValueToken();
			XmlTextReader.XmlTokenInfo token = this.attributeValueTokens[this.currentAttributeValue];
			this.SetTokenProperties(token, XmlNodeType.Text, string.Empty, string.Empty, string.Empty, false, value, false);
			xmlAttributeTokenInfo.Value = value;
			this.attributeCount++;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003F4A0 File Offset: 0x0003D6A0
		private void IncrementAttributeToken()
		{
			this.currentAttribute++;
			if (this.attributeTokens.Length == this.currentAttribute)
			{
				XmlTextReader.XmlAttributeTokenInfo[] array = new XmlTextReader.XmlAttributeTokenInfo[this.attributeTokens.Length * 2];
				this.attributeTokens.CopyTo(array, 0);
				this.attributeTokens = array;
			}
			if (this.attributeTokens[this.currentAttribute] == null)
			{
				this.attributeTokens[this.currentAttribute] = new XmlTextReader.XmlAttributeTokenInfo(this);
			}
			this.currentAttributeToken = this.attributeTokens[this.currentAttribute];
			this.currentAttributeToken.Clear();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003F538 File Offset: 0x0003D738
		private void IncrementAttributeValueToken()
		{
			this.currentAttributeValue++;
			if (this.attributeValueTokens.Length == this.currentAttributeValue)
			{
				XmlTextReader.XmlTokenInfo[] array = new XmlTextReader.XmlTokenInfo[this.attributeValueTokens.Length * 2];
				this.attributeValueTokens.CopyTo(array, 0);
				this.attributeValueTokens = array;
			}
			if (this.attributeValueTokens[this.currentAttributeValue] == null)
			{
				this.attributeValueTokens[this.currentAttributeValue] = new XmlTextReader.XmlTokenInfo(this);
			}
			this.currentAttributeValueToken = this.attributeValueTokens[this.currentAttributeValue];
			this.currentAttributeValueToken.Clear();
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
		private void ReadAttributeValueTokens(int dummyQuoteChar)
		{
			int num = (dummyQuoteChar >= 0) ? dummyQuoteChar : this.ReadChar();
			if (num != 39 && num != 34)
			{
				throw this.NotWFError("an attribute value was not quoted");
			}
			this.currentAttributeToken.QuoteChar = (char)num;
			this.IncrementAttributeValueToken();
			this.currentAttributeToken.ValueTokenStartIndex = this.currentAttributeValue;
			this.currentAttributeValueToken.LineNumber = this.line;
			this.currentAttributeValueToken.LinePosition = this.column;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = true;
			this.currentAttributeValueToken.ValueBufferStart = this.valueBuffer.Length;
			while (flag3)
			{
				int num2 = this.ReadChar();
				if (num2 == num)
				{
					break;
				}
				if (flag)
				{
					this.IncrementAttributeValueToken();
					this.currentAttributeValueToken.ValueBufferStart = this.valueBuffer.Length;
					this.currentAttributeValueToken.LineNumber = this.line;
					this.currentAttributeValueToken.LinePosition = this.column;
					flag = false;
					flag2 = true;
				}
				int num3 = num2;
				switch (num3)
				{
				case 9:
				case 10:
					if (!this.normalization)
					{
						goto IL_2CD;
					}
					num2 = 32;
					goto IL_2CD;
				default:
					if (num3 != -1)
					{
						if (num3 != 38)
						{
							if (num3 != 60)
							{
								goto IL_2CD;
							}
							throw this.NotWFError("attribute values cannot contain '<'");
						}
						else if (this.PeekChar() == 35)
						{
							this.Advance(35);
							num2 = this.ReadCharacterReference();
							this.AppendValueChar(num2);
						}
						else
						{
							string text = this.ReadName();
							this.Expect(59);
							int predefinedEntity = XmlChar.GetPredefinedEntity(text);
							if (predefinedEntity < 0)
							{
								this.CheckAttributeEntityReferenceWFC(text);
								if (this.entityHandling == EntityHandling.ExpandEntities)
								{
									string text2 = this.DTD.GenerateEntityAttributeText(text);
									foreach (char ch in ((IEnumerable<char>)text2))
									{
										this.AppendValueChar((int)ch);
									}
								}
								else
								{
									this.currentAttributeValueToken.ValueBufferEnd = this.valueBuffer.Length;
									this.currentAttributeValueToken.NodeType = XmlNodeType.Text;
									if (!flag2)
									{
										this.IncrementAttributeValueToken();
									}
									this.currentAttributeValueToken.Name = text;
									this.currentAttributeValueToken.Value = string.Empty;
									this.currentAttributeValueToken.NodeType = XmlNodeType.EntityReference;
									flag = true;
								}
							}
							else
							{
								this.AppendValueChar(predefinedEntity);
							}
						}
					}
					else
					{
						if (dummyQuoteChar < 0)
						{
							throw this.NotWFError("unexpected end of file in an attribute value");
						}
						flag3 = false;
					}
					break;
				case 13:
					if (!this.normalization)
					{
						goto IL_2CD;
					}
					if (this.PeekChar() == 10)
					{
						continue;
					}
					if (!this.normalization)
					{
						goto IL_2CD;
					}
					num2 = 32;
					goto IL_2CD;
				}
				IL_31D:
				flag2 = false;
				continue;
				IL_2CD:
				if (this.CharacterChecking && XmlChar.IsInvalid(num2))
				{
					throw this.NotWFError("Invalid character was found.");
				}
				if (num2 < 65535)
				{
					this.valueBuffer.Append((char)num2);
				}
				else
				{
					this.AppendSurrogatePairValueChar(num2);
				}
				goto IL_31D;
			}
			if (!flag)
			{
				this.currentAttributeValueToken.ValueBufferEnd = this.valueBuffer.Length;
				this.currentAttributeValueToken.NodeType = XmlNodeType.Text;
			}
			this.currentAttributeToken.ValueTokenEndIndex = this.currentAttributeValue;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003F958 File Offset: 0x0003DB58
		private void CheckAttributeEntityReferenceWFC(string entName)
		{
			DTDEntityDeclaration dtdentityDeclaration = (this.DTD != null) ? this.DTD.EntityDecls[entName] : null;
			if (dtdentityDeclaration == null)
			{
				if (this.entityHandling == EntityHandling.ExpandEntities || (this.DTD != null && this.resolver != null && dtdentityDeclaration == null))
				{
					throw this.NotWFError(string.Format("Referenced entity '{0}' does not exist.", entName));
				}
				return;
			}
			else
			{
				if (dtdentityDeclaration.HasExternalReference)
				{
					throw this.NotWFError("Reference to external entities is not allowed in the value of an attribute.");
				}
				if (this.isStandalone && !dtdentityDeclaration.IsInternalSubset)
				{
					throw this.NotWFError("Reference to external entities is not allowed in the internal subset.");
				}
				if (dtdentityDeclaration.EntityValue.IndexOf('<') >= 0)
				{
					throw this.NotWFError("Attribute must not contain character '<' either directly or indirectly by way of entity references.");
				}
				return;
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003FA24 File Offset: 0x0003DC24
		private void ReadProcessingInstruction()
		{
			string text = this.ReadName();
			if (text != "xml" && text.ToLower(CultureInfo.InvariantCulture) == "xml")
			{
				throw this.NotWFError("Not allowed processing instruction name which starts with 'X', 'M', 'L' was found.");
			}
			if (!this.SkipWhitespace() && this.PeekChar() != 63)
			{
				throw this.NotWFError("Invalid processing instruction name was found.");
			}
			this.ClearValueBuffer();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				this.Advance(num);
				if (num == 63 && this.PeekChar() == 62)
				{
					this.Advance(62);
					break;
				}
				if (this.CharacterChecking && XmlChar.IsInvalid(num))
				{
					throw this.NotWFError("Invalid character was found.");
				}
				this.AppendValueChar(num);
			}
			if (object.ReferenceEquals(text, "xml"))
			{
				this.VerifyXmlDeclaration();
			}
			else
			{
				if (this.currentState == XmlNodeType.None)
				{
					this.currentState = XmlNodeType.XmlDeclaration;
				}
				this.SetProperties(XmlNodeType.ProcessingInstruction, text, string.Empty, text, false, null, true);
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0003FB3C File Offset: 0x0003DD3C
		private void VerifyXmlDeclaration()
		{
			if (!this.allowMultipleRoot && this.currentState != XmlNodeType.None)
			{
				throw this.NotWFError("XML declaration cannot appear in this state.");
			}
			this.currentState = XmlNodeType.XmlDeclaration;
			string text = this.CreateValueString();
			this.ClearAttributes();
			int num = 0;
			string text2 = null;
			string text3 = null;
			string empty;
			string text4;
			this.ParseAttributeFromString(text, ref num, out empty, out text4);
			if (empty != "version" || text4 != "1.0")
			{
				throw this.NotWFError("'version' is expected.");
			}
			empty = string.Empty;
			if (this.SkipWhitespaceInString(text, ref num) && num < text.Length)
			{
				this.ParseAttributeFromString(text, ref num, out empty, out text4);
			}
			if (empty == "encoding")
			{
				if (!XmlChar.IsValidIANAEncoding(text4))
				{
					throw this.NotWFError("'encoding' must be a valid IANA encoding name.");
				}
				if (this.reader is XmlStreamReader)
				{
					this.parserContext.Encoding = ((XmlStreamReader)this.reader).Encoding;
				}
				else
				{
					this.parserContext.Encoding = Encoding.Unicode;
				}
				text2 = text4;
				empty = string.Empty;
				if (this.SkipWhitespaceInString(text, ref num) && num < text.Length)
				{
					this.ParseAttributeFromString(text, ref num, out empty, out text4);
				}
			}
			if (empty == "standalone")
			{
				this.isStandalone = (text4 == "yes");
				if (text4 != "yes" && text4 != "no")
				{
					throw this.NotWFError("Only 'yes' or 'no' is allow for 'standalone'");
				}
				text3 = text4;
				this.SkipWhitespaceInString(text, ref num);
			}
			else if (empty.Length != 0)
			{
				throw this.NotWFError(string.Format("Unexpected token: '{0}'", empty));
			}
			if (num < text.Length)
			{
				throw this.NotWFError("'?' is expected.");
			}
			this.AddAttributeWithValue("version", "1.0");
			if (text2 != null)
			{
				this.AddAttributeWithValue("encoding", text2);
			}
			if (text3 != null)
			{
				this.AddAttributeWithValue("standalone", text3);
			}
			this.currentAttribute = (this.currentAttributeValue = -1);
			this.SetProperties(XmlNodeType.XmlDeclaration, "xml", string.Empty, "xml", false, text, false);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003FD84 File Offset: 0x0003DF84
		private bool SkipWhitespaceInString(string text, ref int idx)
		{
			int num = idx;
			while (idx < text.Length && XmlChar.IsWhitespace((int)text[idx]))
			{
				idx++;
			}
			return idx - num > 0;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003FDC8 File Offset: 0x0003DFC8
		private void ParseAttributeFromString(string src, ref int idx, out string name, out string value)
		{
			while (idx < src.Length && XmlChar.IsWhitespace((int)src[idx]))
			{
				idx++;
			}
			int num = idx;
			while (idx < src.Length && XmlChar.IsNameChar((int)src[idx]))
			{
				idx++;
			}
			name = src.Substring(num, idx - num);
			while (idx < src.Length && XmlChar.IsWhitespace((int)src[idx]))
			{
				idx++;
			}
			if (idx == src.Length || src[idx] != '=')
			{
				throw this.NotWFError(string.Format("'=' is expected after {0}", name));
			}
			idx++;
			while (idx < src.Length && XmlChar.IsWhitespace((int)src[idx]))
			{
				idx++;
			}
			if (idx == src.Length || (src[idx] != '"' && src[idx] != '\''))
			{
				throw this.NotWFError("'\"' or ''' is expected.");
			}
			char c = src[idx];
			idx++;
			num = idx;
			while (idx < src.Length && src[idx] != c)
			{
				idx++;
			}
			idx++;
			value = src.Substring(num, idx - num - 1);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003FF48 File Offset: 0x0003E148
		internal void SkipTextDeclaration()
		{
			if (this.PeekChar() != 60)
			{
				return;
			}
			this.ReadChar();
			if (this.PeekChar() != 63)
			{
				this.peekCharsIndex = 0;
				return;
			}
			this.ReadChar();
			while (this.peekCharsIndex < 6)
			{
				if (this.PeekChar() < 0)
				{
					break;
				}
				this.ReadChar();
			}
			if (!(new string(this.peekChars, 2, 4) != "xml "))
			{
				this.SkipWhitespace();
				if (this.PeekChar() == 118)
				{
					this.Expect("version");
					this.ExpectAfterWhitespace('=');
					this.SkipWhitespace();
					int num = this.ReadChar();
					char[] array = new char[3];
					int num2 = 0;
					int num3 = num;
					if (num3 != 34 && num3 != 39)
					{
						throw this.NotWFError("Invalid version declaration inside text declaration.");
					}
					while (this.PeekChar() != num)
					{
						if (this.PeekChar() == -1)
						{
							throw this.NotWFError("Invalid version declaration inside text declaration.");
						}
						if (num2 == 3)
						{
							throw this.NotWFError("Invalid version number inside text declaration.");
						}
						array[num2] = (char)this.ReadChar();
						num2++;
						if (num2 == 3 && new string(array) != "1.0")
						{
							throw this.NotWFError("Invalid version number inside text declaration.");
						}
					}
					this.ReadChar();
					this.SkipWhitespace();
				}
				if (this.PeekChar() == 101)
				{
					this.Expect("encoding");
					this.ExpectAfterWhitespace('=');
					this.SkipWhitespace();
					int num4 = this.ReadChar();
					int num3 = num4;
					if (num3 != 34 && num3 != 39)
					{
						throw this.NotWFError("Invalid encoding declaration inside text declaration.");
					}
					while (this.PeekChar() != num4)
					{
						if (this.ReadChar() == -1)
						{
							throw this.NotWFError("Invalid encoding declaration inside text declaration.");
						}
					}
					this.ReadChar();
					this.SkipWhitespace();
				}
				else if (this.Conformance == ConformanceLevel.Auto)
				{
					throw this.NotWFError("Encoding declaration is mandatory in text declaration.");
				}
				this.Expect("?>");
				this.curNodePeekIndex = this.peekCharsIndex;
				return;
			}
			if (new string(this.peekChars, 2, 4).ToLower(CultureInfo.InvariantCulture) == "xml ")
			{
				throw this.NotWFError("Processing instruction name must not be character sequence 'X' 'M' 'L' with case insensitivity.");
			}
			this.peekCharsIndex = 0;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000401B4 File Offset: 0x0003E3B4
		private void ReadDeclaration()
		{
			int num = this.PeekChar();
			int num2 = num;
			if (num2 != 45)
			{
				if (num2 != 68)
				{
					if (num2 != 91)
					{
						throw this.NotWFError("Unexpected declaration markup was found.");
					}
					this.ReadChar();
					this.Expect("CDATA[");
					this.ReadCDATA();
				}
				else
				{
					this.Expect("DOCTYPE");
					this.ReadDoctypeDecl();
				}
			}
			else
			{
				this.Expect("--");
				this.ReadComment();
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0004023C File Offset: 0x0003E43C
		private void ReadComment()
		{
			if (this.currentState == XmlNodeType.None)
			{
				this.currentState = XmlNodeType.XmlDeclaration;
			}
			this.preserveCurrentTag = false;
			this.ClearValueBuffer();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				this.Advance(num);
				if (num == 45 && this.PeekChar() == 45)
				{
					this.Advance(45);
					if (this.PeekChar() != 62)
					{
						throw this.NotWFError("comments cannot contain '--'");
					}
					this.Advance(62);
					break;
				}
				else
				{
					if (XmlChar.IsInvalid(num))
					{
						throw this.NotWFError("Not allowed character was found.");
					}
					this.AppendValueChar(num);
				}
			}
			this.SetProperties(XmlNodeType.Comment, string.Empty, string.Empty, string.Empty, false, null, true);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00040300 File Offset: 0x0003E500
		private void ReadCDATA()
		{
			if (this.currentState != XmlNodeType.Element)
			{
				throw this.NotWFError("CDATA section cannot appear in this state.");
			}
			this.preserveCurrentTag = false;
			this.ClearValueBuffer();
			bool flag = false;
			int num = 0;
			while (this.PeekChar() != -1)
			{
				if (!flag)
				{
					num = this.ReadChar();
				}
				flag = false;
				if (num == 93 && this.PeekChar() == 93)
				{
					num = this.ReadChar();
					if (this.PeekChar() == 62)
					{
						this.ReadChar();
						break;
					}
					flag = true;
				}
				if (this.normalization && num == 13)
				{
					num = this.PeekChar();
					if (num != 10)
					{
						this.AppendValueChar(10);
					}
				}
				else
				{
					if (this.CharacterChecking && XmlChar.IsInvalid(num))
					{
						throw this.NotWFError("Invalid character was found.");
					}
					if (num < 65535)
					{
						this.valueBuffer.Append((char)num);
					}
					else
					{
						this.AppendSurrogatePairValueChar(num);
					}
				}
			}
			this.SetProperties(XmlNodeType.CDATA, string.Empty, string.Empty, string.Empty, false, null, true);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0004041C File Offset: 0x0003E61C
		private void ReadDoctypeDecl()
		{
			if (this.prohibitDtd)
			{
				throw this.NotWFError("Document Type Declaration (DTD) is prohibited in this XML.");
			}
			XmlNodeType xmlNodeType = this.currentState;
			if (xmlNodeType != XmlNodeType.Element && xmlNodeType != XmlNodeType.DocumentType && xmlNodeType != XmlNodeType.EndElement)
			{
				this.currentState = XmlNodeType.DocumentType;
				string text = null;
				string text2 = null;
				int intSubsetStartLine = 0;
				int intSubsetStartColumn = 0;
				this.SkipWhitespace();
				string text3 = this.ReadName();
				this.SkipWhitespace();
				switch (this.PeekChar())
				{
				case 80:
					text = this.ReadPubidLiteral();
					if (!this.SkipWhitespace())
					{
						throw this.NotWFError("Whitespace is required between PUBLIC id and SYSTEM id.");
					}
					text2 = this.ReadSystemLiteral(false);
					break;
				case 83:
					text2 = this.ReadSystemLiteral(true);
					break;
				}
				this.SkipWhitespace();
				if (this.PeekChar() == 91)
				{
					this.ReadChar();
					intSubsetStartLine = this.LineNumber;
					intSubsetStartColumn = this.LinePosition;
					this.ClearValueBuffer();
					this.ReadInternalSubset();
					this.parserContext.InternalSubset = this.CreateValueString();
				}
				this.ExpectAfterWhitespace('>');
				this.GenerateDTDObjectModel(text3, text, text2, this.parserContext.InternalSubset, intSubsetStartLine, intSubsetStartColumn);
				this.SetProperties(XmlNodeType.DocumentType, text3, string.Empty, text3, false, this.parserContext.InternalSubset, true);
				if (text != null)
				{
					this.AddAttributeWithValue("PUBLIC", text);
				}
				if (text2 != null)
				{
					this.AddAttributeWithValue("SYSTEM", text2);
				}
				this.currentAttribute = (this.currentAttributeValue = -1);
				return;
			}
			throw this.NotWFError("Document type cannot appear in this state.");
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000405B4 File Offset: 0x0003E7B4
		internal DTDObjectModel GenerateDTDObjectModel(string name, string publicId, string systemId, string internalSubset)
		{
			return this.GenerateDTDObjectModel(name, publicId, systemId, internalSubset, 0, 0);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000405C4 File Offset: 0x0003E7C4
		internal DTDObjectModel GenerateDTDObjectModel(string name, string publicId, string systemId, string internalSubset, int intSubsetStartLine, int intSubsetStartColumn)
		{
			this.parserContext.Dtd = new DTDObjectModel(this.NameTable);
			this.DTD.BaseURI = this.BaseURI;
			this.DTD.Name = name;
			this.DTD.PublicId = publicId;
			this.DTD.SystemId = systemId;
			this.DTD.InternalSubset = internalSubset;
			this.DTD.XmlResolver = this.resolver;
			this.DTD.IsStandalone = this.isStandalone;
			this.DTD.LineNumber = this.line;
			this.DTD.LinePosition = this.column;
			return new DTDReader(this.DTD, intSubsetStartLine, intSubsetStartColumn)
			{
				Normalization = this.normalization
			}.GenerateDTDObjectModel();
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00040690 File Offset: 0x0003E890
		private XmlTextReader.DtdInputState State
		{
			get
			{
				return this.stateStack.Peek();
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000406A0 File Offset: 0x0003E8A0
		private int ReadValueChar()
		{
			int num = this.ReadChar();
			this.AppendValueChar(num);
			return num;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000406BC File Offset: 0x0003E8BC
		private void ExpectAndAppend(string s)
		{
			this.Expect(s);
			this.valueBuffer.Append(s);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000406D4 File Offset: 0x0003E8D4
		private void ReadInternalSubset()
		{
			bool flag = true;
			while (flag)
			{
				int num = this.ReadValueChar();
				switch (num)
				{
				case 34:
					if (this.State == XmlTextReader.DtdInputState.InsideDoubleQuoted)
					{
						this.stateStack.Pop();
					}
					else if (this.State != XmlTextReader.DtdInputState.InsideSingleQuoted && this.State != XmlTextReader.DtdInputState.Comment)
					{
						this.stateStack.Push(XmlTextReader.DtdInputState.InsideDoubleQuoted);
					}
					break;
				default:
					switch (num)
					{
					case 60:
						switch (this.State)
						{
						case XmlTextReader.DtdInputState.Comment:
						case XmlTextReader.DtdInputState.InsideSingleQuoted:
						case XmlTextReader.DtdInputState.InsideDoubleQuoted:
							break;
						default:
						{
							int num2 = this.ReadValueChar();
							int num3 = num2;
							if (num3 != 33)
							{
								if (num3 != 63)
								{
									throw this.NotWFError(string.Format("unexpected '<{0}'.", (char)num2));
								}
								this.stateStack.Push(XmlTextReader.DtdInputState.PI);
							}
							else
							{
								int num4 = this.ReadValueChar();
								if (num4 != 45)
								{
									if (num4 != 65)
									{
										if (num4 == 69)
										{
											switch (this.ReadValueChar())
											{
											case 76:
												this.ExpectAndAppend("EMENT");
												this.stateStack.Push(XmlTextReader.DtdInputState.ElementDecl);
												break;
											case 77:
												goto IL_1B1;
											case 78:
												this.ExpectAndAppend("TITY");
												this.stateStack.Push(XmlTextReader.DtdInputState.EntityDecl);
												break;
											default:
												goto IL_1B1;
											}
											break;
											IL_1B1:
											throw this.NotWFError("unexpected token '<!E'.");
										}
										if (num4 == 78)
										{
											this.ExpectAndAppend("OTATION");
											this.stateStack.Push(XmlTextReader.DtdInputState.NotationDecl);
										}
									}
									else
									{
										this.ExpectAndAppend("TTLIST");
										this.stateStack.Push(XmlTextReader.DtdInputState.AttlistDecl);
									}
								}
								else
								{
									this.ExpectAndAppend("-");
									this.stateStack.Push(XmlTextReader.DtdInputState.Comment);
								}
							}
							break;
						}
						}
						break;
					default:
						if (num == -1)
						{
							throw this.NotWFError("unexpected end of file at DTD.");
						}
						if (num != 45)
						{
							if (num == 93)
							{
								XmlTextReader.DtdInputState state = this.State;
								switch (state)
								{
								case XmlTextReader.DtdInputState.Comment:
								case XmlTextReader.DtdInputState.InsideSingleQuoted:
								case XmlTextReader.DtdInputState.InsideDoubleQuoted:
									break;
								default:
									if (state != XmlTextReader.DtdInputState.Free)
									{
										throw this.NotWFError("unexpected end of file at DTD.");
									}
									this.valueBuffer.Remove(this.valueBuffer.Length - 1, 1);
									flag = false;
									break;
								}
							}
						}
						else if (this.State == XmlTextReader.DtdInputState.Comment && this.PeekChar() == 45)
						{
							this.ReadValueChar();
							this.ExpectAndAppend(">");
							this.stateStack.Pop();
						}
						break;
					case 62:
						switch (this.State)
						{
						case XmlTextReader.DtdInputState.ElementDecl:
							break;
						case XmlTextReader.DtdInputState.AttlistDecl:
							break;
						case XmlTextReader.DtdInputState.EntityDecl:
							break;
						case XmlTextReader.DtdInputState.NotationDecl:
							break;
						case XmlTextReader.DtdInputState.PI:
							goto IL_320;
						case XmlTextReader.DtdInputState.Comment:
						case XmlTextReader.DtdInputState.InsideSingleQuoted:
						case XmlTextReader.DtdInputState.InsideDoubleQuoted:
							continue;
						default:
							goto IL_320;
						}
						this.stateStack.Pop();
						break;
						IL_320:
						throw this.NotWFError("unexpected token '>'");
					case 63:
						if (this.State == XmlTextReader.DtdInputState.PI && this.ReadValueChar() == 62)
						{
							this.stateStack.Pop();
						}
						break;
					}
					break;
				case 37:
					if (this.State != XmlTextReader.DtdInputState.Free && this.State != XmlTextReader.DtdInputState.EntityDecl && this.State != XmlTextReader.DtdInputState.Comment && this.State != XmlTextReader.DtdInputState.InsideDoubleQuoted && this.State != XmlTextReader.DtdInputState.InsideSingleQuoted)
					{
						throw this.NotWFError("Parameter Entity Reference cannot appear as a part of markupdecl (see XML spec 2.8).");
					}
					break;
				case 39:
					if (this.State == XmlTextReader.DtdInputState.InsideSingleQuoted)
					{
						this.stateStack.Pop();
					}
					else if (this.State != XmlTextReader.DtdInputState.InsideDoubleQuoted && this.State != XmlTextReader.DtdInputState.Comment)
					{
						this.stateStack.Push(XmlTextReader.DtdInputState.InsideSingleQuoted);
					}
					break;
				}
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00040ACC File Offset: 0x0003ECCC
		private string ReadSystemLiteral(bool expectSYSTEM)
		{
			if (expectSYSTEM)
			{
				this.Expect("SYSTEM");
				if (!this.SkipWhitespace())
				{
					throw this.NotWFError("Whitespace is required after 'SYSTEM'.");
				}
			}
			else
			{
				this.SkipWhitespace();
			}
			int num = this.ReadChar();
			int num2 = 0;
			this.ClearValueBuffer();
			while (num2 != num)
			{
				num2 = this.ReadChar();
				if (num2 < 0)
				{
					throw this.NotWFError("Unexpected end of stream in ExternalID.");
				}
				if (num2 != num)
				{
					this.AppendValueChar(num2);
				}
			}
			return this.CreateValueString();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00040B58 File Offset: 0x0003ED58
		private string ReadPubidLiteral()
		{
			this.Expect("PUBLIC");
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required after 'PUBLIC'.");
			}
			int num = this.ReadChar();
			int num2 = 0;
			this.ClearValueBuffer();
			while (num2 != num)
			{
				num2 = this.ReadChar();
				if (num2 < 0)
				{
					throw this.NotWFError("Unexpected end of stream in ExternalID.");
				}
				if (num2 != num && !XmlChar.IsPubidChar(num2))
				{
					throw this.NotWFError(string.Format("character '{0}' not allowed for PUBLIC ID", (char)num2));
				}
				if (num2 != num)
				{
					this.AppendValueChar(num2);
				}
			}
			return this.CreateValueString();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00040BFC File Offset: 0x0003EDFC
		private string ReadName()
		{
			string text;
			string text2;
			return this.ReadName(out text, out text2);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00040C14 File Offset: 0x0003EE14
		private string ReadName(out string prefix, out string localName)
		{
			bool flag = this.preserveCurrentTag;
			this.preserveCurrentTag = true;
			int num = this.peekCharsIndex - this.curNodePeekIndex;
			int num2 = this.PeekChar();
			if (!XmlChar.IsFirstNameChar(num2))
			{
				throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "a name did not start with a legal character {0} ({1})", new object[]
				{
					num2,
					(char)num2
				}));
			}
			this.Advance(num2);
			int num3 = 1;
			int num4 = -1;
			while (XmlChar.IsNameChar(num2 = this.PeekChar()))
			{
				this.Advance(num2);
				if (num2 == 58 && this.namespaces && num4 < 0)
				{
					num4 = num3;
				}
				num3++;
			}
			int num5 = this.curNodePeekIndex + num;
			string text = this.NameTable.Add(this.peekChars, num5, num3);
			if (num4 > 0)
			{
				prefix = this.NameTable.Add(this.peekChars, num5, num4);
				localName = this.NameTable.Add(this.peekChars, num5 + num4 + 1, num3 - num4 - 1);
			}
			else
			{
				prefix = string.Empty;
				localName = text;
			}
			this.preserveCurrentTag = flag;
			return text;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00040D44 File Offset: 0x0003EF44
		private void Expect(int expected)
		{
			int num = this.ReadChar();
			if (num != expected)
			{
				throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "expected '{0}' ({1:X}) but found '{2}' ({3:X})", new object[]
				{
					(char)expected,
					expected,
					(num >= 0) ? ((char)num) : "EOF",
					num
				}));
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00040DB4 File Offset: 0x0003EFB4
		private void Expect(string expected)
		{
			for (int i = 0; i < expected.Length; i++)
			{
				if (this.ReadChar() != (int)expected[i])
				{
					throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "'{0}' is expected", new object[]
					{
						expected
					}));
				}
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00040E0C File Offset: 0x0003F00C
		private void ExpectAfterWhitespace(char c)
		{
			int num;
			do
			{
				num = this.ReadChar();
			}
			while (num < 33 && XmlChar.IsWhitespace(num));
			if ((int)c != num)
			{
				throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "Expected {0}, but found {1} [{2}]", new object[]
				{
					c,
					(num >= 0) ? ((char)num) : "EOF",
					num
				}));
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00040E94 File Offset: 0x0003F094
		private bool SkipWhitespace()
		{
			int num = this.PeekChar();
			bool flag = num == 32 || num == 9 || num == 10 || num == 13;
			if (!flag)
			{
				return false;
			}
			this.Advance(num);
			while ((num = this.PeekChar()) == 32 || num == 9 || num == 10 || num == 13)
			{
				this.Advance(num);
			}
			return flag;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00040F0C File Offset: 0x0003F10C
		private bool ReadWhitespace()
		{
			if (this.currentState == XmlNodeType.None)
			{
				this.currentState = XmlNodeType.XmlDeclaration;
			}
			bool flag = this.preserveCurrentTag;
			this.preserveCurrentTag = true;
			int num = this.peekCharsIndex - this.curNodePeekIndex;
			int num2 = this.PeekChar();
			do
			{
				this.Advance(num2);
				num2 = this.PeekChar();
			}
			while (num2 == 32 || num2 == 9 || num2 == 10 || num2 == 13);
			bool flag2 = this.currentState == XmlNodeType.Element && num2 != -1 && num2 != 60;
			if (!flag2 && (this.whitespaceHandling == WhitespaceHandling.None || (this.whitespaceHandling == WhitespaceHandling.Significant && this.XmlSpace != XmlSpace.Preserve)))
			{
				return false;
			}
			this.ClearValueBuffer();
			this.valueBuffer.Append(this.peekChars, this.curNodePeekIndex, this.peekCharsIndex - this.curNodePeekIndex - num);
			this.preserveCurrentTag = flag;
			if (flag2)
			{
				this.ReadText(false);
			}
			else
			{
				XmlNodeType nodeType = (this.XmlSpace != XmlSpace.Preserve) ? XmlNodeType.Whitespace : XmlNodeType.SignificantWhitespace;
				this.SetProperties(nodeType, string.Empty, string.Empty, string.Empty, false, null, true);
			}
			return true;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00041040 File Offset: 0x0003F240
		private int ReadCharsInternal(char[] buffer, int offset, int length)
		{
			int num = offset;
			for (int i = 0; i < length; i++)
			{
				int num2 = this.PeekChar();
				int num3 = num2;
				if (num3 == -1)
				{
					throw this.NotWFError("Unexpected end of xml.");
				}
				if (num3 != 60)
				{
					this.Advance(num2);
					if (num2 < 65535)
					{
						buffer[num++] = (char)num2;
					}
					else
					{
						buffer[num++] = (char)((num2 - 65536) / 1024 + 55296);
						buffer[num++] = (char)((num2 - 65536) % 1024 + 56320);
					}
				}
				else
				{
					if (i + 1 == length)
					{
						return i;
					}
					this.Advance(num2);
					if (this.PeekChar() != 47)
					{
						this.nestLevel++;
						buffer[num++] = '<';
					}
					else
					{
						if (this.nestLevel-- <= 0)
						{
							this.Expect(47);
							if (this.depthUp)
							{
								this.depth++;
								this.depthUp = false;
							}
							this.ReadEndTag();
							this.readCharsInProgress = false;
							this.Read();
							return i;
						}
						buffer[num++] = '<';
					}
				}
			}
			return length;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00041188 File Offset: 0x0003F388
		private bool ReadUntilEndTag()
		{
			if (this.Depth == 0)
			{
				this.currentState = XmlNodeType.EndElement;
			}
			for (;;)
			{
				int num = this.ReadChar();
				int num2 = num;
				if (num2 == -1)
				{
					break;
				}
				if (num2 == 60)
				{
					if (this.PeekChar() != 47)
					{
						this.nestLevel++;
					}
					else if (--this.nestLevel <= 0)
					{
						this.ReadChar();
						string a = this.ReadName();
						if (!(a != this.elementNames[this.elementNameStackPos - 1].Name))
						{
							goto IL_AE;
						}
					}
				}
			}
			throw this.NotWFError("Unexpected end of xml.");
			IL_AE:
			this.Expect(62);
			this.depth--;
			return this.Read();
		}

		// Token: 0x040005AD RID: 1453
		private const int peekCharCapacity = 1024;

		// Token: 0x040005AE RID: 1454
		private XmlTextReader.XmlTokenInfo cursorToken;

		// Token: 0x040005AF RID: 1455
		private XmlTextReader.XmlTokenInfo currentToken;

		// Token: 0x040005B0 RID: 1456
		private XmlTextReader.XmlAttributeTokenInfo currentAttributeToken;

		// Token: 0x040005B1 RID: 1457
		private XmlTextReader.XmlTokenInfo currentAttributeValueToken;

		// Token: 0x040005B2 RID: 1458
		private XmlTextReader.XmlAttributeTokenInfo[] attributeTokens = new XmlTextReader.XmlAttributeTokenInfo[10];

		// Token: 0x040005B3 RID: 1459
		private XmlTextReader.XmlTokenInfo[] attributeValueTokens = new XmlTextReader.XmlTokenInfo[10];

		// Token: 0x040005B4 RID: 1460
		private int currentAttribute;

		// Token: 0x040005B5 RID: 1461
		private int currentAttributeValue;

		// Token: 0x040005B6 RID: 1462
		private int attributeCount;

		// Token: 0x040005B7 RID: 1463
		private XmlParserContext parserContext;

		// Token: 0x040005B8 RID: 1464
		private XmlNameTable nameTable;

		// Token: 0x040005B9 RID: 1465
		private XmlNamespaceManager nsmgr;

		// Token: 0x040005BA RID: 1466
		private ReadState readState;

		// Token: 0x040005BB RID: 1467
		private bool disallowReset;

		// Token: 0x040005BC RID: 1468
		private int depth;

		// Token: 0x040005BD RID: 1469
		private int elementDepth;

		// Token: 0x040005BE RID: 1470
		private bool depthUp;

		// Token: 0x040005BF RID: 1471
		private bool popScope;

		// Token: 0x040005C0 RID: 1472
		private XmlTextReader.TagName[] elementNames;

		// Token: 0x040005C1 RID: 1473
		private int elementNameStackPos;

		// Token: 0x040005C2 RID: 1474
		private bool allowMultipleRoot;

		// Token: 0x040005C3 RID: 1475
		private bool isStandalone;

		// Token: 0x040005C4 RID: 1476
		private bool returnEntityReference;

		// Token: 0x040005C5 RID: 1477
		private string entityReferenceName;

		// Token: 0x040005C6 RID: 1478
		private StringBuilder valueBuffer;

		// Token: 0x040005C7 RID: 1479
		private TextReader reader;

		// Token: 0x040005C8 RID: 1480
		private char[] peekChars;

		// Token: 0x040005C9 RID: 1481
		private int peekCharsIndex;

		// Token: 0x040005CA RID: 1482
		private int peekCharsLength;

		// Token: 0x040005CB RID: 1483
		private int curNodePeekIndex;

		// Token: 0x040005CC RID: 1484
		private bool preserveCurrentTag;

		// Token: 0x040005CD RID: 1485
		private int line;

		// Token: 0x040005CE RID: 1486
		private int column;

		// Token: 0x040005CF RID: 1487
		private int currentLinkedNodeLineNumber;

		// Token: 0x040005D0 RID: 1488
		private int currentLinkedNodeLinePosition;

		// Token: 0x040005D1 RID: 1489
		private bool useProceedingLineInfo;

		// Token: 0x040005D2 RID: 1490
		private XmlNodeType startNodeType;

		// Token: 0x040005D3 RID: 1491
		private XmlNodeType currentState;

		// Token: 0x040005D4 RID: 1492
		private int nestLevel;

		// Token: 0x040005D5 RID: 1493
		private bool readCharsInProgress;

		// Token: 0x040005D6 RID: 1494
		private XmlReaderBinarySupport.CharGetter binaryCharGetter;

		// Token: 0x040005D7 RID: 1495
		private bool namespaces = true;

		// Token: 0x040005D8 RID: 1496
		private WhitespaceHandling whitespaceHandling;

		// Token: 0x040005D9 RID: 1497
		private XmlResolver resolver = new XmlUrlResolver();

		// Token: 0x040005DA RID: 1498
		private bool normalization;

		// Token: 0x040005DB RID: 1499
		private bool checkCharacters;

		// Token: 0x040005DC RID: 1500
		private bool prohibitDtd;

		// Token: 0x040005DD RID: 1501
		private bool closeInput = true;

		// Token: 0x040005DE RID: 1502
		private EntityHandling entityHandling;

		// Token: 0x040005DF RID: 1503
		private NameTable whitespacePool;

		// Token: 0x040005E0 RID: 1504
		private char[] whitespaceCache;

		// Token: 0x040005E1 RID: 1505
		private XmlTextReader.DtdInputStateStack stateStack = new XmlTextReader.DtdInputStateStack();

		// Token: 0x02000120 RID: 288
		internal class XmlTokenInfo
		{
			// Token: 0x06000CB8 RID: 3256 RVA: 0x00041264 File Offset: 0x0003F464
			public XmlTokenInfo(XmlTextReader xtr)
			{
				this.Reader = xtr;
				this.Clear();
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0004127C File Offset: 0x0003F47C
			// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00041334 File Offset: 0x0003F534
			public virtual string Value
			{
				get
				{
					if (this.valueCache != null)
					{
						return this.valueCache;
					}
					if (this.ValueBufferStart >= 0)
					{
						this.valueCache = this.Reader.valueBuffer.ToString(this.ValueBufferStart, this.ValueBufferEnd - this.ValueBufferStart);
						return this.valueCache;
					}
					switch (this.NodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						this.valueCache = this.Reader.CreateValueString();
						return this.valueCache;
					}
					return null;
				}
				set
				{
					this.valueCache = value;
				}
			}

			// Token: 0x06000CBB RID: 3259 RVA: 0x00041340 File Offset: 0x0003F540
			public virtual void Clear()
			{
				this.ValueBufferStart = -1;
				this.valueCache = null;
				this.NodeType = XmlNodeType.None;
				this.Name = (this.LocalName = (this.Prefix = (this.NamespaceURI = string.Empty)));
				this.IsEmptyElement = false;
				this.QuoteChar = '"';
				this.LineNumber = (this.LinePosition = 0);
			}

			// Token: 0x040005E4 RID: 1508
			private string valueCache;

			// Token: 0x040005E5 RID: 1509
			protected XmlTextReader Reader;

			// Token: 0x040005E6 RID: 1510
			public string Name;

			// Token: 0x040005E7 RID: 1511
			public string LocalName;

			// Token: 0x040005E8 RID: 1512
			public string Prefix;

			// Token: 0x040005E9 RID: 1513
			public string NamespaceURI;

			// Token: 0x040005EA RID: 1514
			public bool IsEmptyElement;

			// Token: 0x040005EB RID: 1515
			public char QuoteChar;

			// Token: 0x040005EC RID: 1516
			public int LineNumber;

			// Token: 0x040005ED RID: 1517
			public int LinePosition;

			// Token: 0x040005EE RID: 1518
			public int ValueBufferStart;

			// Token: 0x040005EF RID: 1519
			public int ValueBufferEnd;

			// Token: 0x040005F0 RID: 1520
			public XmlNodeType NodeType;
		}

		// Token: 0x02000121 RID: 289
		internal class XmlAttributeTokenInfo : XmlTextReader.XmlTokenInfo
		{
			// Token: 0x06000CBC RID: 3260 RVA: 0x000413A8 File Offset: 0x0003F5A8
			public XmlAttributeTokenInfo(XmlTextReader reader) : base(reader)
			{
				this.NodeType = XmlNodeType.Attribute;
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x06000CBD RID: 3261 RVA: 0x000413C4 File Offset: 0x0003F5C4
			// (set) Token: 0x06000CBE RID: 3262 RVA: 0x000414F0 File Offset: 0x0003F6F0
			public override string Value
			{
				get
				{
					if (this.valueCache != null)
					{
						return this.valueCache;
					}
					if (this.ValueTokenStartIndex == this.ValueTokenEndIndex)
					{
						XmlTextReader.XmlTokenInfo xmlTokenInfo = this.Reader.attributeValueTokens[this.ValueTokenStartIndex];
						if (xmlTokenInfo.NodeType == XmlNodeType.EntityReference)
						{
							this.valueCache = "&" + xmlTokenInfo.Name + ";";
						}
						else
						{
							this.valueCache = xmlTokenInfo.Value;
						}
						return this.valueCache;
					}
					this.tmpBuilder.Length = 0;
					for (int i = this.ValueTokenStartIndex; i <= this.ValueTokenEndIndex; i++)
					{
						XmlTextReader.XmlTokenInfo xmlTokenInfo2 = this.Reader.attributeValueTokens[i];
						if (xmlTokenInfo2.NodeType == XmlNodeType.Text)
						{
							this.tmpBuilder.Append(xmlTokenInfo2.Value);
						}
						else
						{
							this.tmpBuilder.Append('&');
							this.tmpBuilder.Append(xmlTokenInfo2.Name);
							this.tmpBuilder.Append(';');
						}
					}
					this.valueCache = this.tmpBuilder.ToString(0, this.tmpBuilder.Length);
					return this.valueCache;
				}
				set
				{
					this.valueCache = value;
				}
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x000414FC File Offset: 0x0003F6FC
			public override void Clear()
			{
				base.Clear();
				this.valueCache = null;
				this.NodeType = XmlNodeType.Attribute;
				this.ValueTokenStartIndex = (this.ValueTokenEndIndex = 0);
			}

			// Token: 0x06000CC0 RID: 3264 RVA: 0x00041530 File Offset: 0x0003F730
			internal void FillXmlns()
			{
				if (object.ReferenceEquals(this.Prefix, "xmlns"))
				{
					this.Reader.nsmgr.AddNamespace(this.LocalName, this.Value);
				}
				else if (object.ReferenceEquals(this.Name, "xmlns"))
				{
					this.Reader.nsmgr.AddNamespace(string.Empty, this.Value);
				}
			}

			// Token: 0x06000CC1 RID: 3265 RVA: 0x000415A4 File Offset: 0x0003F7A4
			internal void FillNamespace()
			{
				if (object.ReferenceEquals(this.Prefix, "xmlns") || object.ReferenceEquals(this.Name, "xmlns"))
				{
					this.NamespaceURI = "http://www.w3.org/2000/xmlns/";
				}
				else if (this.Prefix.Length == 0)
				{
					this.NamespaceURI = string.Empty;
				}
				else
				{
					this.NamespaceURI = this.Reader.LookupNamespace(this.Prefix, true);
				}
			}

			// Token: 0x040005F1 RID: 1521
			public int ValueTokenStartIndex;

			// Token: 0x040005F2 RID: 1522
			public int ValueTokenEndIndex;

			// Token: 0x040005F3 RID: 1523
			private string valueCache;

			// Token: 0x040005F4 RID: 1524
			private StringBuilder tmpBuilder = new StringBuilder();
		}

		// Token: 0x02000122 RID: 290
		private struct TagName
		{
			// Token: 0x06000CC2 RID: 3266 RVA: 0x00041624 File Offset: 0x0003F824
			public TagName(string n, string l, string p)
			{
				this.Name = n;
				this.LocalName = l;
				this.Prefix = p;
			}

			// Token: 0x040005F5 RID: 1525
			public readonly string Name;

			// Token: 0x040005F6 RID: 1526
			public readonly string LocalName;

			// Token: 0x040005F7 RID: 1527
			public readonly string Prefix;
		}

		// Token: 0x02000123 RID: 291
		private enum DtdInputState
		{
			// Token: 0x040005F9 RID: 1529
			Free = 1,
			// Token: 0x040005FA RID: 1530
			ElementDecl,
			// Token: 0x040005FB RID: 1531
			AttlistDecl,
			// Token: 0x040005FC RID: 1532
			EntityDecl,
			// Token: 0x040005FD RID: 1533
			NotationDecl,
			// Token: 0x040005FE RID: 1534
			PI,
			// Token: 0x040005FF RID: 1535
			Comment,
			// Token: 0x04000600 RID: 1536
			InsideSingleQuoted,
			// Token: 0x04000601 RID: 1537
			InsideDoubleQuoted
		}

		// Token: 0x02000124 RID: 292
		private class DtdInputStateStack
		{
			// Token: 0x06000CC3 RID: 3267 RVA: 0x0004163C File Offset: 0x0003F83C
			public DtdInputStateStack()
			{
				this.Push(XmlTextReader.DtdInputState.Free);
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x00041658 File Offset: 0x0003F858
			public XmlTextReader.DtdInputState Peek()
			{
				return (XmlTextReader.DtdInputState)((int)this.intern.Peek());
			}

			// Token: 0x06000CC5 RID: 3269 RVA: 0x0004166C File Offset: 0x0003F86C
			public XmlTextReader.DtdInputState Pop()
			{
				return (XmlTextReader.DtdInputState)((int)this.intern.Pop());
			}

			// Token: 0x06000CC6 RID: 3270 RVA: 0x00041680 File Offset: 0x0003F880
			public void Push(XmlTextReader.DtdInputState val)
			{
				this.intern.Push(val);
			}

			// Token: 0x04000602 RID: 1538
			private Stack intern = new Stack();
		}
	}
}
