using System;

// Token: 0x020003C3 RID: 963
public interface IBashAttackable
{
	// Token: 0x06001AB8 RID: 6840
	void OnEnterBash();

	// Token: 0x06001AB9 RID: 6841
	void OnBashHighlight();

	// Token: 0x06001ABA RID: 6842
	void OnBashDehighlight();

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06001ABB RID: 6843
	int BashPriority { get; }
}
