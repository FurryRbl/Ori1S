using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000031 RID: 49
	internal class XsdIDManager
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000CD50 File Offset: 0x0000AF50
		private ArrayList MissingIDReferences
		{
			get
			{
				if (this.missingIDReferences == null)
				{
					this.missingIDReferences = new ArrayList();
				}
				return this.missingIDReferences;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000CD70 File Offset: 0x0000AF70
		public void OnStartElement()
		{
			this.thisElementId = null;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		public string AssessEachAttributeIdentityConstraint(XmlSchemaDatatype dt, object parsedValue, string elementName)
		{
			string text = parsedValue as string;
			switch (dt.TokenizedType)
			{
			case XmlTokenizedType.ID:
				if (this.thisElementId != null)
				{
					return "ID type attribute was already assigned in the containing element.";
				}
				this.thisElementId = text;
				if (this.idList.ContainsKey(text))
				{
					return "Duplicate ID value was found.";
				}
				this.idList.Add(text, elementName);
				if (this.MissingIDReferences.Contains(text))
				{
					this.MissingIDReferences.Remove(text);
				}
				break;
			case XmlTokenizedType.IDREF:
				if (!this.idList.Contains(text))
				{
					this.MissingIDReferences.Add(text);
				}
				break;
			case XmlTokenizedType.IDREFS:
				foreach (string text2 in (string[])parsedValue)
				{
					if (!this.idList.Contains(text2))
					{
						this.MissingIDReferences.Add(text2);
					}
				}
				break;
			}
			return null;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000CE78 File Offset: 0x0000B078
		public bool HasMissingIDReferences()
		{
			return this.missingIDReferences != null && this.missingIDReferences.Count > 0;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000CE98 File Offset: 0x0000B098
		public string GetMissingIDString()
		{
			return string.Join(" ", this.MissingIDReferences.ToArray(typeof(string)) as string[]);
		}

		// Token: 0x0400014C RID: 332
		private Hashtable idList = new Hashtable();

		// Token: 0x0400014D RID: 333
		private ArrayList missingIDReferences;

		// Token: 0x0400014E RID: 334
		private string thisElementId;
	}
}
