using System;
using UnityEngine;

// Token: 0x020008A4 RID: 2212
public class ActivationBasedOnCondition : MonoBehaviour, IDynamicGraphicHierarchy
{
	// Token: 0x0600317A RID: 12666 RVA: 0x000D3139 File Offset: 0x000D1339
	public void Awake()
	{
		this.UpdateActivation();
	}

	// Token: 0x0600317B RID: 12667 RVA: 0x000D3144 File Offset: 0x000D1344
	public void UpdateActivation()
	{
		if (this.Condition)
		{
			bool flag = this.Condition.Validate(null);
			if (this.TargetTrue)
			{
				this.TargetTrue.SetActive(flag);
			}
			if (this.TargetFalse)
			{
				this.TargetFalse.SetActive(!flag);
			}
		}
	}

	// Token: 0x0600317C RID: 12668 RVA: 0x000D31A9 File Offset: 0x000D13A9
	private void FixedUpdate()
	{
		this.UpdateActivation();
	}

	// Token: 0x04002CBC RID: 11452
	public Condition Condition;

	// Token: 0x04002CBD RID: 11453
	public GameObject TargetTrue;

	// Token: 0x04002CBE RID: 11454
	public GameObject TargetFalse;
}
