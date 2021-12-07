using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Xml;

namespace Mono.Xml
{
	// Token: 0x020000D6 RID: 214
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	internal class EntityResolvingXmlReader : XmlReader, IHasXmlParserContext, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x0002C448 File Offset: 0x0002A648
		public EntityResolvingXmlReader(XmlReader source)
		{
			this.source = source;
			IHasXmlParserContext hasXmlParserContext = source as IHasXmlParserContext;
			if (hasXmlParserContext != null)
			{
				this.context = hasXmlParserContext.ParserContext;
			}
			else
			{
				this.context = new XmlParserContext(source.NameTable, new XmlNamespaceManager(source.NameTable), null, XmlSpace.None);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002C4A0 File Offset: 0x0002A6A0
		private EntityResolvingXmlReader(XmlReader entityContainer, bool inside_attr)
		{
			this.source = entityContainer;
			this.entity_inside_attr = inside_attr;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0002C4B8 File Offset: 0x0002A6B8
		XmlParserContext IHasXmlParserContext.ParserContext
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0002C4C0 File Offset: 0x0002A6C0
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002C4CC File Offset: 0x0002A6CC
		string IXmlNamespaceResolver.LookupPrefix(string ns)
		{
			return ((IXmlNamespaceResolver)this.Current).LookupPrefix(ns);
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
		private XmlReader Current
		{
			get
			{
				return (this.entity == null || this.entity.ReadState == ReadState.Initial) ? this.source : this.entity;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0002C51C File Offset: 0x0002A71C
		public override int AttributeCount
		{
			get
			{
				return this.Current.AttributeCount;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0002C52C File Offset: 0x0002A72C
		public override string BaseURI
		{
			get
			{
				return this.Current.BaseURI;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0002C53C File Offset: 0x0002A73C
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002C540 File Offset: 0x0002A740
		public override int Depth
		{
			get
			{
				if (this.entity != null && this.entity.ReadState == ReadState.Interactive)
				{
					return this.source.Depth + this.entity.Depth + 1;
				}
				return this.source.Depth;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0002C590 File Offset: 0x0002A790
		public override bool EOF
		{
			get
			{
				return this.source.EOF;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		public override bool HasValue
		{
			get
			{
				return this.Current.HasValue;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
		public override bool IsDefault
		{
			get
			{
				return this.Current.IsDefault;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0002C5C0 File Offset: 0x0002A7C0
		public override bool IsEmptyElement
		{
			get
			{
				return this.Current.IsEmptyElement;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
		public override string LocalName
		{
			get
			{
				return this.Current.LocalName;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		public override string Name
		{
			get
			{
				return this.Current.Name;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0002C5F0 File Offset: 0x0002A7F0
		public override string NamespaceURI
		{
			get
			{
				return this.Current.NamespaceURI;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0002C600 File Offset: 0x0002A800
		public override XmlNameTable NameTable
		{
			get
			{
				return this.Current.NameTable;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0002C610 File Offset: 0x0002A810
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.entity == null)
				{
					return this.source.NodeType;
				}
				if (this.entity.ReadState == ReadState.Initial)
				{
					return this.source.NodeType;
				}
				return (!this.entity.EOF) ? this.entity.NodeType : XmlNodeType.EndEntity;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0002C674 File Offset: 0x0002A874
		internal XmlParserContext ParserContext
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0002C67C File Offset: 0x0002A87C
		public override string Prefix
		{
			get
			{
				return this.Current.Prefix;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0002C68C File Offset: 0x0002A88C
		public override char QuoteChar
		{
			get
			{
				return this.Current.QuoteChar;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0002C69C File Offset: 0x0002A89C
		public override ReadState ReadState
		{
			get
			{
				return (this.entity == null) ? this.source.ReadState : ReadState.Interactive;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0002C6BC File Offset: 0x0002A8BC
		public override string Value
		{
			get
			{
				return this.Current.Value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0002C6CC File Offset: 0x0002A8CC
		public override string XmlLang
		{
			get
			{
				return this.Current.XmlLang;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0002C6DC File Offset: 0x0002A8DC
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.Current.XmlSpace;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002C6EC File Offset: 0x0002A8EC
		private void CopyProperties(EntityResolvingXmlReader other)
		{
			this.context = other.context;
			this.resolver = other.resolver;
			this.entity_handling = other.entity_handling;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002C720 File Offset: 0x0002A920
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0002C728 File Offset: 0x0002A928
		public EntityHandling EntityHandling
		{
			get
			{
				return this.entity_handling;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.EntityHandling = value;
				}
				this.entity_handling = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0002C748 File Offset: 0x0002A948
		public int LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.Current as IXmlLineInfo;
				return (xmlLineInfo != null) ? xmlLineInfo.LineNumber : 0;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0002C774 File Offset: 0x0002A974
		public int LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.Current as IXmlLineInfo;
				return (xmlLineInfo != null) ? xmlLineInfo.LinePosition : 0;
			}
		}

		// Token: 0x1700022C RID: 556
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0002C7A0 File Offset: 0x0002A9A0
		public XmlResolver XmlResolver
		{
			set
			{
				if (this.entity != null)
				{
					this.entity.XmlResolver = value;
				}
				this.resolver = value;
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002C7C0 File Offset: 0x0002A9C0
		public override void Close()
		{
			if (this.entity != null)
			{
				this.entity.Close();
			}
			this.source.Close();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002C7E4 File Offset: 0x0002A9E4
		public override string GetAttribute(int i)
		{
			return this.Current.GetAttribute(i);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002C7F4 File Offset: 0x0002A9F4
		public override string GetAttribute(string name)
		{
			return this.Current.GetAttribute(name);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002C804 File Offset: 0x0002AA04
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.Current.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002C814 File Offset: 0x0002AA14
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return ((IXmlNamespaceResolver)this.Current).GetNamespacesInScope(scope);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002C828 File Offset: 0x0002AA28
		public override string LookupNamespace(string prefix)
		{
			return this.Current.LookupNamespace(prefix);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002C838 File Offset: 0x0002AA38
		public override void MoveToAttribute(int i)
		{
			if (this.entity != null && this.entity_inside_attr)
			{
				this.entity.Close();
				this.entity = null;
			}
			this.Current.MoveToAttribute(i);
			this.inside_attr = true;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002C878 File Offset: 0x0002AA78
		public override bool MoveToAttribute(string name)
		{
			if (this.entity != null && !this.entity_inside_attr)
			{
				return this.entity.MoveToAttribute(name);
			}
			if (!this.source.MoveToAttribute(name))
			{
				return false;
			}
			if (this.entity != null && this.entity_inside_attr)
			{
				this.entity.Close();
				this.entity = null;
			}
			this.inside_attr = true;
			return true;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002C8EC File Offset: 0x0002AAEC
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (this.entity != null && !this.entity_inside_attr)
			{
				return this.entity.MoveToAttribute(localName, namespaceName);
			}
			if (!this.source.MoveToAttribute(localName, namespaceName))
			{
				return false;
			}
			if (this.entity != null && this.entity_inside_attr)
			{
				this.entity.Close();
				this.entity = null;
			}
			this.inside_attr = true;
			return true;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002C964 File Offset: 0x0002AB64
		public override bool MoveToElement()
		{
			if (this.entity != null && this.entity_inside_attr)
			{
				this.entity.Close();
				this.entity = null;
			}
			if (!this.Current.MoveToElement())
			{
				return false;
			}
			this.inside_attr = false;
			return true;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		public override bool MoveToFirstAttribute()
		{
			if (this.entity != null && !this.entity_inside_attr)
			{
				return this.entity.MoveToFirstAttribute();
			}
			if (!this.source.MoveToFirstAttribute())
			{
				return false;
			}
			if (this.entity != null && this.entity_inside_attr)
			{
				this.entity.Close();
				this.entity = null;
			}
			this.inside_attr = true;
			return true;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0002CA28 File Offset: 0x0002AC28
		public override bool MoveToNextAttribute()
		{
			if (this.entity != null && !this.entity_inside_attr)
			{
				return this.entity.MoveToNextAttribute();
			}
			if (!this.source.MoveToNextAttribute())
			{
				return false;
			}
			if (this.entity != null && this.entity_inside_attr)
			{
				this.entity.Close();
				this.entity = null;
			}
			this.inside_attr = true;
			return true;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0002CA9C File Offset: 0x0002AC9C
		public override bool Read()
		{
			if (this.do_resolve)
			{
				this.DoResolveEntity();
				this.do_resolve = false;
			}
			this.inside_attr = false;
			if (this.entity != null && (this.entity_inside_attr || this.entity.EOF))
			{
				this.entity.Close();
				this.entity = null;
			}
			if (this.entity != null)
			{
				if (this.entity.Read())
				{
					return true;
				}
				if (this.EntityHandling == EntityHandling.ExpandEntities)
				{
					this.entity.Close();
					this.entity = null;
					return this.Read();
				}
				return true;
			}
			else
			{
				if (!this.source.Read())
				{
					return false;
				}
				if (this.EntityHandling == EntityHandling.ExpandEntities && this.source.NodeType == XmlNodeType.EntityReference)
				{
					this.ResolveEntity();
					return this.Read();
				}
				return true;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002CB84 File Offset: 0x0002AD84
		public override bool ReadAttributeValue()
		{
			if (this.entity != null && this.entity_inside_attr)
			{
				if (!this.entity.EOF)
				{
					this.entity.Read();
					return true;
				}
				this.entity.Close();
				this.entity = null;
			}
			return this.Current.ReadAttributeValue();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002CBE8 File Offset: 0x0002ADE8
		public override string ReadString()
		{
			return base.ReadString();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002CBF0 File Offset: 0x0002ADF0
		public override void ResolveEntity()
		{
			this.DoResolveEntity();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		private void DoResolveEntity()
		{
			if (this.entity != null)
			{
				this.entity.ResolveEntity();
			}
			else
			{
				if (this.source.NodeType != XmlNodeType.EntityReference)
				{
					throw new InvalidOperationException("The current node is not an Entity Reference");
				}
				if (this.ParserContext.Dtd == null)
				{
					throw new XmlException(this, this.BaseURI, string.Format("Cannot resolve entity without DTD: '{0}'", this.source.Name));
				}
				XmlReader xmlReader = this.ParserContext.Dtd.GenerateEntityContentReader(this.source.Name, this.ParserContext);
				if (xmlReader == null)
				{
					throw new XmlException(this, this.BaseURI, string.Format("Reference to undeclared entity '{0}'.", this.source.Name));
				}
				this.entity = new EntityResolvingXmlReader(xmlReader, this.inside_attr);
				this.entity.CopyProperties(this);
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		public override void Skip()
		{
			base.Skip();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		public bool HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.Current as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x04000468 RID: 1128
		private EntityResolvingXmlReader entity;

		// Token: 0x04000469 RID: 1129
		private XmlReader source;

		// Token: 0x0400046A RID: 1130
		private XmlParserContext context;

		// Token: 0x0400046B RID: 1131
		private XmlResolver resolver;

		// Token: 0x0400046C RID: 1132
		private EntityHandling entity_handling;

		// Token: 0x0400046D RID: 1133
		private bool entity_inside_attr;

		// Token: 0x0400046E RID: 1134
		private bool inside_attr;

		// Token: 0x0400046F RID: 1135
		private bool do_resolve;
	}
}
