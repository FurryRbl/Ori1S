using System;
using System.Collections;
using System.Xml;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000077 RID: 119
	internal class CompiledStylesheet
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x00019B9C File Offset: 0x00017D9C
		public CompiledStylesheet(XslStylesheet style, Hashtable globalVariables, Hashtable attrSets, XmlNamespaceManager nsMgr, Hashtable keys, Hashtable outputs, Hashtable decimalFormats, MSXslScriptManager msScripts)
		{
			this.style = style;
			this.globalVariables = globalVariables;
			this.attrSets = attrSets;
			this.nsMgr = nsMgr;
			this.keys = keys;
			this.outputs = outputs;
			this.decimalFormats = decimalFormats;
			this.msScripts = msScripts;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00019BEC File Offset: 0x00017DEC
		public Hashtable Variables
		{
			get
			{
				return this.globalVariables;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00019BF4 File Offset: 0x00017DF4
		public XslStylesheet Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00019BFC File Offset: 0x00017DFC
		public XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.nsMgr;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00019C04 File Offset: 0x00017E04
		public Hashtable Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00019C0C File Offset: 0x00017E0C
		public Hashtable Outputs
		{
			get
			{
				return this.outputs;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00019C14 File Offset: 0x00017E14
		public MSXslScriptManager ScriptManager
		{
			get
			{
				return this.msScripts;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00019C1C File Offset: 0x00017E1C
		public XslDecimalFormat LookupDecimalFormat(XmlQualifiedName name)
		{
			XslDecimalFormat xslDecimalFormat = this.decimalFormats[name] as XslDecimalFormat;
			if (xslDecimalFormat == null && name == XmlQualifiedName.Empty)
			{
				return XslDecimalFormat.Default;
			}
			return xslDecimalFormat;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00019C58 File Offset: 0x00017E58
		public XslGeneralVariable ResolveVariable(XmlQualifiedName name)
		{
			return (XslGeneralVariable)this.globalVariables[name];
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00019C6C File Offset: 0x00017E6C
		public ArrayList ResolveKey(XmlQualifiedName name)
		{
			return (ArrayList)this.keys[name];
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00019C80 File Offset: 0x00017E80
		public XslAttributeSet ResolveAttributeSet(XmlQualifiedName name)
		{
			return (XslAttributeSet)this.attrSets[name];
		}

		// Token: 0x040002AA RID: 682
		private XslStylesheet style;

		// Token: 0x040002AB RID: 683
		private Hashtable globalVariables;

		// Token: 0x040002AC RID: 684
		private Hashtable attrSets;

		// Token: 0x040002AD RID: 685
		private XmlNamespaceManager nsMgr;

		// Token: 0x040002AE RID: 686
		private Hashtable keys;

		// Token: 0x040002AF RID: 687
		private Hashtable outputs;

		// Token: 0x040002B0 RID: 688
		private Hashtable decimalFormats;

		// Token: 0x040002B1 RID: 689
		private MSXslScriptManager msScripts;
	}
}
