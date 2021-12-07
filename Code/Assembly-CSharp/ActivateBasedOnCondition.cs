using System;
using UnityEngine;

// Token: 0x020008A3 RID: 2211
public class ActivateBasedOnCondition : MonoBehaviour, IDynamicGraphicHierarchy
{
	// Token: 0x06003177 RID: 12663 RVA: 0x000D3094 File Offset: 0x000D1294
	public void Awake()
	{
		if (this.Condition)
		{
			this.Target.SetActive((!this.Condition.Validate(null)) ? (!this.Activate) : this.Activate);
		}
	}

	// Token: 0x06003178 RID: 12664 RVA: 0x000D30E4 File Offset: 0x000D12E4
	private void FixedUpdate()
	{
		if (this.Condition)
		{
			this.Target.SetActive((!this.Condition.Validate(null)) ? (!this.Activate) : this.Activate);
		}
	}

	// Token: 0x04002CB9 RID: 11449
	public Condition Condition;

	// Token: 0x04002CBA RID: 11450
	public GameObject Target;

	// Token: 0x04002CBB RID: 11451
	public bool Activate = true;
}
