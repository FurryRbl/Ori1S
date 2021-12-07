using System;

namespace System.Xml
{
	// Token: 0x020000FB RID: 251
	internal class XmlNameEntry
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x00035448 File Offset: 0x00033648
		public XmlNameEntry(string prefix, string local, string ns)
		{
			this.Update(prefix, local, ns);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0003545C File Offset: 0x0003365C
		public void Update(string prefix, string local, string ns)
		{
			this.Prefix = prefix;
			this.LocalName = local;
			this.NS = ns;
			this.Hash = local.GetHashCode() + ((prefix.Length <= 0) ? 0 : prefix.GetHashCode());
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000354A4 File Offset: 0x000336A4
		public override bool Equals(object other)
		{
			XmlNameEntry xmlNameEntry = other as XmlNameEntry;
			return xmlNameEntry != null && xmlNameEntry.Hash == this.Hash && object.ReferenceEquals(xmlNameEntry.LocalName, this.LocalName) && object.ReferenceEquals(xmlNameEntry.NS, this.NS) && object.ReferenceEquals(xmlNameEntry.Prefix, this.Prefix);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00035510 File Offset: 0x00033710
		public override int GetHashCode()
		{
			return this.Hash;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00035518 File Offset: 0x00033718
		public string GetPrefixedName(XmlNameEntryCache owner)
		{
			if (this.prefixed_name_cache == null)
			{
				this.prefixed_name_cache = owner.GetAtomizedPrefixedName(this.Prefix, this.LocalName);
			}
			return this.prefixed_name_cache;
		}

		// Token: 0x04000504 RID: 1284
		public string Prefix;

		// Token: 0x04000505 RID: 1285
		public string LocalName;

		// Token: 0x04000506 RID: 1286
		public string NS;

		// Token: 0x04000507 RID: 1287
		public int Hash;

		// Token: 0x04000508 RID: 1288
		private string prefixed_name_cache;
	}
}
