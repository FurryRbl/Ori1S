using System;

// Token: 0x02000448 RID: 1096
public interface IPushable
{
	// Token: 0x1700052B RID: 1323
	// (get) Token: 0x06001E83 RID: 7811
	bool IsGrabbed { get; }

	// Token: 0x06001E84 RID: 7812
	void OnPushOrPull(PlatformMovement platformMovement);

	// Token: 0x06001E85 RID: 7813
	void OnGrabbed(PlatformMovement platformMovement);

	// Token: 0x06001E86 RID: 7814
	void OnReleased(PlatformMovement platformMovement);

	// Token: 0x06001E87 RID: 7815
	void OnHighlight();

	// Token: 0x06001E88 RID: 7816
	void OnDehighlight();

	// Token: 0x06001E89 RID: 7817
	bool CanBeBashed();

	// Token: 0x06001E8A RID: 7818
	float PushableSpeedRatio();
}
