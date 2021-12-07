using System;
using UnityEngine;

// Token: 0x02000951 RID: 2385
public class BelongToScene : SaveSerialize
{
	// Token: 0x06003480 RID: 13440 RVA: 0x000DC6CF File Offset: 0x000DA8CF
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.SceneBelongTo);
		if (ar.Reading)
		{
			this.Refresh();
		}
	}

	// Token: 0x06003481 RID: 13441 RVA: 0x000DC6F0 File Offset: 0x000DA8F0
	private void Start()
	{
		if (this.SceneBelongTo == string.Empty)
		{
			this.SceneBelongTo = Application.loadedLevelName;
		}
		this.Refresh();
	}

	// Token: 0x06003482 RID: 13442 RVA: 0x000DC723 File Offset: 0x000DA923
	private void Refresh()
	{
		base.gameObject.SetActive(this.SceneBelongTo == Application.loadedLevelName);
	}

	// Token: 0x04002F5D RID: 12125
	public string SceneBelongTo;
}
