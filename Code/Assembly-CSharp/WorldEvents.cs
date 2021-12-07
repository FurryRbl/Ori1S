using System;
using System.Collections.Generic;

// Token: 0x020004A7 RID: 1191
public class WorldEvents : GuidOwner
{
	// Token: 0x06002080 RID: 8320 RVA: 0x0008D79C File Offset: 0x0008B99C
	public string GetNameFromID(int id)
	{
		WorldEvents.Item item = this.Items.Find((WorldEvents.Item a) => a.ID == id);
		return (item == null) ? string.Empty : item.Name;
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x0008D7E4 File Offset: 0x0008B9E4
	public int GetIDFromName(string eventName)
	{
		WorldEvents.Item item = this.Items.Find((WorldEvents.Item a) => a.Name == eventName);
		if (item == null)
		{
			throw new Exception("Name " + eventName + "not found.");
		}
		return item.ID;
	}

	// Token: 0x04001BA2 RID: 7074
	public int DefaultValue;

	// Token: 0x04001BA3 RID: 7075
	public int UniqueID;

	// Token: 0x04001BA4 RID: 7076
	public List<WorldEvents.Item> Items = new List<WorldEvents.Item>();

	// Token: 0x020004CB RID: 1227
	[Serializable]
	public class Item
	{
		// Token: 0x04001C1E RID: 7198
		public string Name;

		// Token: 0x04001C1F RID: 7199
		public int ID;
	}
}
