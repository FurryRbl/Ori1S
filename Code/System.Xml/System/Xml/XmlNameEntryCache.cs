using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000FC RID: 252
	internal class XmlNameEntryCache
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x00035544 File Offset: 0x00033744
		public XmlNameEntryCache(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00035584 File Offset: 0x00033784
		public string GetAtomizedPrefixedName(string prefix, string local)
		{
			if (prefix == null || prefix.Length == 0)
			{
				return local;
			}
			if (this.cacheBuffer == null)
			{
				this.cacheBuffer = new char[20];
			}
			if (this.cacheBuffer.Length < prefix.Length + local.Length + 1)
			{
				this.cacheBuffer = new char[Math.Max(prefix.Length + local.Length + 1, this.cacheBuffer.Length << 1)];
			}
			prefix.CopyTo(0, this.cacheBuffer, 0, prefix.Length);
			this.cacheBuffer[prefix.Length] = ':';
			local.CopyTo(0, this.cacheBuffer, prefix.Length + 1, local.Length);
			return this.nameTable.Add(this.cacheBuffer, 0, prefix.Length + local.Length + 1);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00035660 File Offset: 0x00033860
		public XmlNameEntry Add(string prefix, string local, string ns, bool atomic)
		{
			if (!atomic)
			{
				prefix = this.nameTable.Add(prefix);
				local = this.nameTable.Add(local);
				ns = this.nameTable.Add(ns);
			}
			XmlNameEntry xmlNameEntry = this.GetInternal(prefix, local, ns, true);
			if (xmlNameEntry == null)
			{
				xmlNameEntry = new XmlNameEntry(prefix, local, ns);
				this.table[xmlNameEntry] = xmlNameEntry;
			}
			return xmlNameEntry;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000356C8 File Offset: 0x000338C8
		public XmlNameEntry Get(string prefix, string local, string ns, bool atomic)
		{
			return this.GetInternal(prefix, local, ns, atomic);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000356E4 File Offset: 0x000338E4
		private XmlNameEntry GetInternal(string prefix, string local, string ns, bool atomic)
		{
			if (!atomic && (this.nameTable.Get(prefix) == null || this.nameTable.Get(local) == null || this.nameTable.Get(ns) == null))
			{
				return null;
			}
			this.dummy.Update(prefix, local, ns);
			return this.table[this.dummy] as XmlNameEntry;
		}

		// Token: 0x04000509 RID: 1289
		private Hashtable table = new Hashtable();

		// Token: 0x0400050A RID: 1290
		private XmlNameTable nameTable;

		// Token: 0x0400050B RID: 1291
		private XmlNameEntry dummy = new XmlNameEntry(string.Empty, string.Empty, string.Empty);

		// Token: 0x0400050C RID: 1292
		private char[] cacheBuffer;
	}
}
