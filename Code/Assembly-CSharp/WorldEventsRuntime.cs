using System;

// Token: 0x020004C9 RID: 1225
public class WorldEventsRuntime : ISerializable
{
	// Token: 0x06002134 RID: 8500 RVA: 0x00091CF2 File Offset: 0x0008FEF2
	public WorldEventsRuntime(int defaultValue)
	{
		this.Value = defaultValue;
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x00091D01 File Offset: 0x0008FF01
	public void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Value);
	}

	// Token: 0x04001C1D RID: 7197
	public int Value;
}
