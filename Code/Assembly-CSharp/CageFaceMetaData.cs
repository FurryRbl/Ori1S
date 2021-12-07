using System;

// Token: 0x020007C3 RID: 1987
public abstract class CageFaceMetaData<T> : CageMetaData<T>
{
	// Token: 0x06002DC1 RID: 11713 RVA: 0x000C3457 File Offset: 0x000C1657
	public new void OnEnable()
	{
		base.OnEnable();
		if (this.CageStructureTool)
		{
			this.CageStructureTool.OnRemoveFace += this.OnRemoveFace;
		}
	}

	// Token: 0x06002DC2 RID: 11714 RVA: 0x000C3486 File Offset: 0x000C1686
	public new void OnDisable()
	{
		base.OnDisable();
		if (this.CageStructureTool)
		{
			this.CageStructureTool.OnRemoveFace -= this.OnRemoveFace;
		}
	}

	// Token: 0x06002DC3 RID: 11715 RVA: 0x000C34B5 File Offset: 0x000C16B5
	public void OnRemoveFace(CageStructureTool.Face face)
	{
		base.Remove(face.ID);
	}
}
