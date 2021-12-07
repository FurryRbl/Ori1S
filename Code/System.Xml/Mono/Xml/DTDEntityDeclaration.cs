using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000CC RID: 204
	internal class DTDEntityDeclaration : DTDEntityBase
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x000270A0 File Offset: 0x000252A0
		internal DTDEntityDeclaration(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x000270B4 File Offset: 0x000252B4
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x000270BC File Offset: 0x000252BC
		public string NotationName
		{
			get
			{
				return this.notationName;
			}
			set
			{
				this.notationName = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000270C8 File Offset: 0x000252C8
		public bool HasExternalReference
		{
			get
			{
				if (!this.scanned)
				{
					this.ScanEntityValue(new ArrayList());
				}
				return this.hasExternalReference;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000270E8 File Offset: 0x000252E8
		public string EntityValue
		{
			get
			{
				if (base.IsInvalid)
				{
					return string.Empty;
				}
				if (base.PublicId == null && base.SystemId == null && base.LiteralEntityValue == null)
				{
					return string.Empty;
				}
				if (this.entityValue == null)
				{
					if (this.NotationName != null)
					{
						this.entityValue = string.Empty;
					}
					else if (base.SystemId == null || base.SystemId == string.Empty)
					{
						this.entityValue = base.ReplacementText;
						if (this.entityValue == null)
						{
							this.entityValue = string.Empty;
						}
					}
					else
					{
						this.entityValue = base.ReplacementText;
					}
					this.ScanEntityValue(new ArrayList());
				}
				return this.entityValue;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000271B8 File Offset: 0x000253B8
		public void ScanEntityValue(ArrayList refs)
		{
			string text = this.EntityValue;
			if (base.SystemId != null)
			{
				this.hasExternalReference = true;
			}
			if (this.recursed)
			{
				throw base.NotWFError("Entity recursion was found.");
			}
			this.recursed = true;
			if (this.scanned)
			{
				foreach (object obj in refs)
				{
					string text2 = (string)obj;
					if (this.ReferencingEntities.Contains(text2))
					{
						throw base.NotWFError(string.Format("Nested entity was found between {0} and {1}", text2, base.Name));
					}
				}
				this.recursed = false;
				return;
			}
			int length = text.Length;
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c != '&')
				{
					if (c == ';')
					{
						if (num != 0)
						{
							string text3 = text.Substring(num, i - num);
							if (text3.Length == 0)
							{
								throw base.NotWFError("Entity reference name is missing.");
							}
							if (text3[0] != '#')
							{
								if (XmlChar.GetPredefinedEntity(text3) < 0)
								{
									this.ReferencingEntities.Add(text3);
									DTDEntityDeclaration dtdentityDeclaration = base.Root.EntityDecls[text3];
									if (dtdentityDeclaration != null)
									{
										if (dtdentityDeclaration.SystemId != null)
										{
											this.hasExternalReference = true;
										}
										refs.Add(base.Name);
										dtdentityDeclaration.ScanEntityValue(refs);
										foreach (object obj2 in dtdentityDeclaration.ReferencingEntities)
										{
											string value = (string)obj2;
											this.ReferencingEntities.Add(value);
										}
										refs.Remove(base.Name);
										text = text.Remove(num - 1, text3.Length + 2);
										text = text.Insert(num - 1, dtdentityDeclaration.EntityValue);
										i -= text3.Length + 1;
										length = text.Length;
									}
									num = 0;
								}
							}
						}
					}
				}
				else
				{
					num = i + 1;
				}
			}
			if (num != 0)
			{
				base.Root.AddError(new XmlSchemaException("Invalid reference character '&' is specified.", this.LineNumber, this.LinePosition, null, this.BaseURI, null));
			}
			this.scanned = true;
			this.recursed = false;
		}

		// Token: 0x0400041B RID: 1051
		private string entityValue;

		// Token: 0x0400041C RID: 1052
		private string notationName;

		// Token: 0x0400041D RID: 1053
		private ArrayList ReferencingEntities = new ArrayList();

		// Token: 0x0400041E RID: 1054
		private bool scanned;

		// Token: 0x0400041F RID: 1055
		private bool recursed;

		// Token: 0x04000420 RID: 1056
		private bool hasExternalReference;
	}
}
