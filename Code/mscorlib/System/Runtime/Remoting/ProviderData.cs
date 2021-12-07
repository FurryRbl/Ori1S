using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000426 RID: 1062
	internal class ProviderData
	{
		// Token: 0x06002D3C RID: 11580 RVA: 0x0009650C File Offset: 0x0009470C
		public void CopyFrom(ProviderData other)
		{
			if (this.Ref == null)
			{
				this.Ref = other.Ref;
			}
			if (this.Id == null)
			{
				this.Id = other.Id;
			}
			if (this.Type == null)
			{
				this.Type = other.Type;
			}
			foreach (object obj in other.CustomProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
				{
					this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			if (other.CustomData != null)
			{
				if (this.CustomData == null)
				{
					this.CustomData = new ArrayList();
				}
				foreach (object obj2 in other.CustomData)
				{
					SinkProviderData value = (SinkProviderData)obj2;
					this.CustomData.Add(value);
				}
			}
		}

		// Token: 0x0400138D RID: 5005
		internal string Ref;

		// Token: 0x0400138E RID: 5006
		internal string Type;

		// Token: 0x0400138F RID: 5007
		internal string Id;

		// Token: 0x04001390 RID: 5008
		internal Hashtable CustomProperties = new Hashtable();

		// Token: 0x04001391 RID: 5009
		internal IList CustomData;
	}
}
