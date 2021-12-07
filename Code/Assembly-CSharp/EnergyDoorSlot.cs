using System;
using UnityEngine;

// Token: 0x020008E4 RID: 2276
public class EnergyDoorSlot : MonoBehaviour
{
	// Token: 0x060032C9 RID: 13001 RVA: 0x000D6B64 File Offset: 0x000D4D64
	public void Awake()
	{
		this.Door.RegisterSlot(this);
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x000D6B74 File Offset: 0x000D4D74
	public void Refresh()
	{
		if (!this.Activated && this.Door.AmountOfEnergyUsed > this.Index)
		{
			this.Activated = true;
			this.Slot.SetActive(true);
			if (this.ActivateSpawnEffect)
			{
				UnityEngine.Object.Instantiate(this.ActivateSpawnEffect, base.transform.position, Quaternion.identity);
			}
		}
		if (this.Activated && this.Door.AmountOfEnergyUsed <= this.Index)
		{
			this.Activated = false;
			this.Slot.SetActive(false);
		}
	}

	// Token: 0x060032CB RID: 13003 RVA: 0x000D6C15 File Offset: 0x000D4E15
	public void FixedUpdate()
	{
		this.Refresh();
	}

	// Token: 0x04002DC6 RID: 11718
	public int Index;

	// Token: 0x04002DC7 RID: 11719
	public EnergyDoor Door;

	// Token: 0x04002DC8 RID: 11720
	public bool Activated;

	// Token: 0x04002DC9 RID: 11721
	public GameObject Slot;

	// Token: 0x04002DCA RID: 11722
	public GameObject ActivateSpawnEffect;
}
