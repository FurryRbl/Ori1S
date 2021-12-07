using System;
using System.Collections.Generic;

// Token: 0x020007C4 RID: 1988
public abstract class CageMetaData<T> : SaveSerialize, ICageMetaData
{
	// Token: 0x06002DC5 RID: 11717 RVA: 0x000C34E1 File Offset: 0x000C16E1
	public void OnEnable()
	{
		if (this.CageStructureTool)
		{
			this.CageStructureTool.Register(this);
		}
	}

	// Token: 0x06002DC6 RID: 11718 RVA: 0x000C34FF File Offset: 0x000C16FF
	public void OnDisable()
	{
		if (this.CageStructureTool)
		{
			this.CageStructureTool.Unregister(this);
		}
	}

	// Token: 0x06002DC7 RID: 11719 RVA: 0x000C3520 File Offset: 0x000C1720
	public override void Serialize(Archive ar)
	{
		if (this.ShouldSerialize)
		{
			if (ar.Reading)
			{
				this.Data.Clear();
				int num = ar.Serialize(0);
				for (int i = 0; i < num; i++)
				{
					T item = default(T);
					int item2 = ar.Serialize(0);
					this.Serialize(ref item, ar);
					this.IDs.Add(item2);
					this.Data.Add(item);
				}
			}
			else
			{
				ar.Serialize(this.Data.Count);
				for (int j = 0; j < this.Data.Count; j++)
				{
					ar.Serialize(this.IDs[j]);
					T t = this.Data[j];
					this.Serialize(ref t, ar);
				}
			}
		}
	}

	// Token: 0x06002DC8 RID: 11720 RVA: 0x000C3600 File Offset: 0x000C1800
	public void Remove(int id)
	{
		int index = this.IDs.FindIndex((int a) => a == id);
		this.IDs.RemoveAt(index);
		this.Data.RemoveAt(index);
	}

	// Token: 0x06002DC9 RID: 11721
	public abstract void Serialize(ref T worldMapAreaState, Archive ar);

	// Token: 0x0400291D RID: 10525
	public List<int> IDs = new List<int>();

	// Token: 0x0400291E RID: 10526
	public List<T> Data = new List<T>();

	// Token: 0x0400291F RID: 10527
	public T DefaultValue;

	// Token: 0x04002920 RID: 10528
	public CageStructureTool CageStructureTool;

	// Token: 0x04002921 RID: 10529
	public bool ShouldSerialize;
}
