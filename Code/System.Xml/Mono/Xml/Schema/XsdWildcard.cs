using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000032 RID: 50
	internal class XsdWildcard
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public XsdWildcard(XmlSchemaObject wildcard)
		{
			this.xsobj = wildcard;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		private void Reset()
		{
			this.HasValueAny = false;
			this.HasValueLocal = false;
			this.HasValueOther = false;
			this.HasValueTargetNamespace = false;
			this.ResolvedNamespaces = new StringCollection();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000CF08 File Offset: 0x0000B108
		public void Compile(string nss, ValidationEventHandler h, XmlSchema schema)
		{
			if (this.SkipCompile)
			{
				return;
			}
			this.Reset();
			int num = 0;
			string list = (nss != null) ? nss : "##any";
			string[] array = XmlSchemaUtil.SplitList(list);
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string text2 = text;
				if (text2 == null)
				{
					goto IL_170;
				}
				if (XsdWildcard.<>f__switch$map6 == null)
				{
					XsdWildcard.<>f__switch$map6 = new Dictionary<string, int>(4)
					{
						{
							"##any",
							0
						},
						{
							"##other",
							1
						},
						{
							"##targetNamespace",
							2
						},
						{
							"##local",
							3
						}
					};
				}
				int num2;
				if (!XsdWildcard.<>f__switch$map6.TryGetValue(text2, out num2))
				{
					goto IL_170;
				}
				switch (num2)
				{
				case 0:
					if (this.HasValueAny)
					{
						this.xsobj.error(h, "Multiple specification of ##any was found.");
					}
					num |= 1;
					this.HasValueAny = true;
					break;
				case 1:
					if (this.HasValueOther)
					{
						this.xsobj.error(h, "Multiple specification of ##other was found.");
					}
					num |= 2;
					this.HasValueOther = true;
					break;
				case 2:
					if (this.HasValueTargetNamespace)
					{
						this.xsobj.error(h, "Multiple specification of ##targetNamespace was found.");
					}
					num |= 4;
					this.HasValueTargetNamespace = true;
					break;
				case 3:
					if (this.HasValueLocal)
					{
						this.xsobj.error(h, "Multiple specification of ##local was found.");
					}
					num |= 8;
					this.HasValueLocal = true;
					break;
				default:
					goto IL_170;
				}
				IL_1DE:
				i++;
				continue;
				IL_170:
				if (!XmlSchemaUtil.CheckAnyUri(text))
				{
					this.xsobj.error(h, "the namespace is not a valid anyURI");
				}
				else if (this.ResolvedNamespaces.Contains(text))
				{
					this.xsobj.error(h, "Multiple specification of '" + text + "' was found.");
				}
				else
				{
					num |= 16;
					this.ResolvedNamespaces.Add(text);
				}
				goto IL_1DE;
			}
			if ((num & 1) == 1 && num != 1)
			{
				this.xsobj.error(h, "##any if present must be the only namespace attribute");
			}
			if ((num & 2) == 2 && num != 2)
			{
				this.xsobj.error(h, "##other if present must be the only namespace attribute");
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000D144 File Offset: 0x0000B344
		public bool ExamineAttributeWildcardIntersection(XmlSchemaAny other, ValidationEventHandler h, XmlSchema schema)
		{
			if (this.HasValueAny == other.HasValueAny && this.HasValueLocal == other.HasValueLocal && this.HasValueOther == other.HasValueOther && this.HasValueTargetNamespace == other.HasValueTargetNamespace && this.ResolvedProcessing == other.ResolvedProcessContents)
			{
				bool flag = false;
				for (int i = 0; i < this.ResolvedNamespaces.Count; i++)
				{
					if (!other.ResolvedNamespaces.Contains(this.ResolvedNamespaces[i]))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (this.HasValueAny)
			{
				return !other.HasValueAny && !other.HasValueLocal && !other.HasValueOther && !other.HasValueTargetNamespace && other.ResolvedNamespaces.Count == 0;
			}
			if (other.HasValueAny)
			{
				return !this.HasValueAny && !this.HasValueLocal && !this.HasValueOther && !this.HasValueTargetNamespace && this.ResolvedNamespaces.Count == 0;
			}
			if (this.HasValueOther && other.HasValueOther && this.TargetNamespace != other.TargetNamespace)
			{
				return false;
			}
			if (this.HasValueOther)
			{
				return (!other.HasValueLocal || !(this.TargetNamespace != string.Empty)) && (!other.HasValueTargetNamespace || !(this.TargetNamespace != other.TargetNamespace)) && other.ValidateWildcardAllowsNamespaceName(this.TargetNamespace, h, schema, false);
			}
			if (other.HasValueOther)
			{
				return (!this.HasValueLocal || !(other.TargetNamespace != string.Empty)) && (!this.HasValueTargetNamespace || !(other.TargetNamespace != this.TargetNamespace)) && this.ValidateWildcardAllowsNamespaceName(other.TargetNamespace, h, schema, false);
			}
			if (this.ResolvedNamespaces.Count > 0)
			{
				for (int j = 0; j < this.ResolvedNamespaces.Count; j++)
				{
					if (other.ResolvedNamespaces.Contains(this.ResolvedNamespaces[j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public bool ValidateWildcardAllowsNamespaceName(string ns, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			if (this.HasValueAny)
			{
				return true;
			}
			if (this.HasValueOther && ns != this.TargetNamespace)
			{
				return true;
			}
			if (this.HasValueTargetNamespace && ns == this.TargetNamespace)
			{
				return true;
			}
			if (this.HasValueLocal && ns == string.Empty)
			{
				return true;
			}
			for (int i = 0; i < this.ResolvedNamespaces.Count; i++)
			{
				if (ns == this.ResolvedNamespaces[i])
				{
					return true;
				}
			}
			if (raiseError)
			{
				this.xsobj.error(h, "This wildcard does not allow the namespace: " + ns);
			}
			return false;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000D480 File Offset: 0x0000B680
		internal void ValidateWildcardSubset(XsdWildcard other, ValidationEventHandler h, XmlSchema schema)
		{
			this.ValidateWildcardSubset(other, h, schema, true);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000D490 File Offset: 0x0000B690
		internal bool ValidateWildcardSubset(XsdWildcard other, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			if (other.HasValueAny)
			{
				return true;
			}
			if (this.HasValueOther && other.HasValueOther && (this.TargetNamespace == other.TargetNamespace || other.TargetNamespace == null || other.TargetNamespace == string.Empty))
			{
				return true;
			}
			if (this.HasValueAny)
			{
				if (raiseError)
				{
					this.xsobj.error(h, "Invalid wildcard subset was found.");
				}
				return false;
			}
			if (other.HasValueOther)
			{
				if ((this.HasValueTargetNamespace && other.TargetNamespace == this.TargetNamespace) || (this.HasValueLocal && (other.TargetNamespace == null || other.TargetNamespace.Length == 0)))
				{
					if (raiseError)
					{
						this.xsobj.error(h, "Invalid wildcard subset was found.");
					}
					return false;
				}
				for (int i = 0; i < this.ResolvedNamespaces.Count; i++)
				{
					if (this.ResolvedNamespaces[i] == other.TargetNamespace)
					{
						if (raiseError)
						{
							this.xsobj.error(h, "Invalid wildcard subset was found.");
						}
						return false;
					}
				}
			}
			else
			{
				if ((this.HasValueLocal && !other.HasValueLocal) || (this.HasValueTargetNamespace && !other.HasValueTargetNamespace))
				{
					if (raiseError)
					{
						this.xsobj.error(h, "Invalid wildcard subset was found.");
					}
					return false;
				}
				if (this.HasValueOther)
				{
					if (raiseError)
					{
						this.xsobj.error(h, "Invalid wildcard subset was found.");
					}
					return false;
				}
				for (int j = 0; j < this.ResolvedNamespaces.Count; j++)
				{
					if (!other.ResolvedNamespaces.Contains(this.ResolvedNamespaces[j]))
					{
						if (raiseError)
						{
							this.xsobj.error(h, "Invalid wildcard subset was found.");
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0400014F RID: 335
		private XmlSchemaObject xsobj;

		// Token: 0x04000150 RID: 336
		public XmlSchemaContentProcessing ResolvedProcessing;

		// Token: 0x04000151 RID: 337
		public string TargetNamespace;

		// Token: 0x04000152 RID: 338
		public bool SkipCompile;

		// Token: 0x04000153 RID: 339
		public bool HasValueAny;

		// Token: 0x04000154 RID: 340
		public bool HasValueLocal;

		// Token: 0x04000155 RID: 341
		public bool HasValueOther;

		// Token: 0x04000156 RID: 342
		public bool HasValueTargetNamespace;

		// Token: 0x04000157 RID: 343
		public StringCollection ResolvedNamespaces;
	}
}
