using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000024 RID: 36
	internal class XsdKeyTable
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00008504 File Offset: 0x00006704
		public XsdKeyTable(XmlSchemaIdentityConstraint source)
		{
			this.Reset(source);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000853C File Offset: 0x0000673C
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00008544 File Offset: 0x00006744
		public XmlQualifiedName RefKeyName
		{
			get
			{
				return this.refKeyName;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000854C File Offset: 0x0000674C
		public XmlSchemaIdentityConstraint SourceSchemaIdentity
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00008554 File Offset: 0x00006754
		public XsdIdentitySelector Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000855C File Offset: 0x0000675C
		public void Reset(XmlSchemaIdentityConstraint source)
		{
			this.source = source;
			this.selector = source.CompiledSelector;
			this.qname = source.QualifiedName;
			XmlSchemaKeyref xmlSchemaKeyref = source as XmlSchemaKeyref;
			if (xmlSchemaKeyref != null)
			{
				this.refKeyName = xmlSchemaKeyref.Refer;
			}
			this.StartDepth = 0;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000085A8 File Offset: 0x000067A8
		public XsdIdentityPath SelectorMatches(ArrayList qnameStack, int depth)
		{
			for (int i = 0; i < this.Selector.Paths.Length; i++)
			{
				XsdIdentityPath xsdIdentityPath = this.Selector.Paths[i];
				if (depth == this.StartDepth)
				{
					if (xsdIdentityPath.OrderedSteps.Length == 0)
					{
						return xsdIdentityPath;
					}
				}
				else if (depth - this.StartDepth >= xsdIdentityPath.OrderedSteps.Length - 1)
				{
					int num = xsdIdentityPath.OrderedSteps.Length;
					if (xsdIdentityPath.OrderedSteps[num - 1].IsAttribute)
					{
						num--;
					}
					if (!xsdIdentityPath.Descendants || depth >= this.StartDepth + num)
					{
						if (xsdIdentityPath.Descendants || depth == this.StartDepth + num)
						{
							num--;
							int num2 = 0;
							while (0 <= num)
							{
								XsdIdentityStep xsdIdentityStep = xsdIdentityPath.OrderedSteps[num];
								if (!xsdIdentityStep.IsAnyName)
								{
									XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)qnameStack[qnameStack.Count - num2 - 1];
									if (xsdIdentityStep.NsName == null || !(xmlQualifiedName.Namespace == xsdIdentityStep.NsName))
									{
										if (!(xsdIdentityStep.Name == xmlQualifiedName.Name) || !(xsdIdentityStep.Namespace == xmlQualifiedName.Namespace))
										{
											if (this.alwaysTrue)
											{
												break;
											}
										}
									}
								}
								num2++;
								num--;
							}
							if (num < 0)
							{
								return xsdIdentityPath;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0400010C RID: 268
		public readonly bool alwaysTrue = true;

		// Token: 0x0400010D RID: 269
		private XsdIdentitySelector selector;

		// Token: 0x0400010E RID: 270
		private XmlSchemaIdentityConstraint source;

		// Token: 0x0400010F RID: 271
		private XmlQualifiedName qname;

		// Token: 0x04000110 RID: 272
		private XmlQualifiedName refKeyName;

		// Token: 0x04000111 RID: 273
		public XsdKeyEntryCollection Entries = new XsdKeyEntryCollection();

		// Token: 0x04000112 RID: 274
		public XsdKeyEntryCollection FinishedEntries = new XsdKeyEntryCollection();

		// Token: 0x04000113 RID: 275
		public int StartDepth;

		// Token: 0x04000114 RID: 276
		public XsdKeyTable ReferencedKey;
	}
}
