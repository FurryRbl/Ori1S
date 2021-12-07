using System;

// Token: 0x02000473 RID: 1139
public class SoulFlameCastTrigger : Trigger
{
	// Token: 0x06001F60 RID: 8032 RVA: 0x0008A544 File Offset: 0x00088744
	public new void Awake()
	{
		SeinSoulFlame.OnSoulFlameCast += this.OnSoulFlameCast;
		base.Awake();
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x0008A55D File Offset: 0x0008875D
	public void OnSoulFlameCast()
	{
		base.DoTrigger(true);
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x0008A566 File Offset: 0x00088766
	public new void OnDestroy()
	{
		SeinSoulFlame.OnSoulFlameCast -= this.OnSoulFlameCast;
		base.OnDestroy();
	}
}
