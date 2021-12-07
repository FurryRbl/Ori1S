using System;

// Token: 0x02000025 RID: 37
public class IsOnCollisionState : ISerializable
{
	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007F68 File Offset: 0x00006168
	public bool IsOnOrFutureOn
	{
		get
		{
			return this.IsOn || this.FutureOn;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007F7E File Offset: 0x0000617E
	public bool WasOnButNotIsOn
	{
		get
		{
			return this.WasOn && !this.IsOn;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007F97 File Offset: 0x00006197
	public bool OnThisFrame
	{
		get
		{
			return !this.WasOn && this.IsOn;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060001D4 RID: 468 RVA: 0x00007FAD File Offset: 0x000061AD
	public bool OffThisFrame
	{
		get
		{
			return this.WasOn && !this.IsOn;
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00007FC6 File Offset: 0x000061C6
	public void Update()
	{
		this.WasOn = this.IsOn;
		this.IsOn = this.FutureOn;
		this.FutureOn = false;
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00007FE8 File Offset: 0x000061E8
	public void Serialize(Archive ar)
	{
		ar.Serialize(ref this.WasOn);
		ar.Serialize(ref this.IsOn);
		ar.Serialize(ref this.FutureOn);
	}

	// Token: 0x04000182 RID: 386
	public bool WasOn;

	// Token: 0x04000183 RID: 387
	public bool IsOn;

	// Token: 0x04000184 RID: 388
	public bool FutureOn;
}
