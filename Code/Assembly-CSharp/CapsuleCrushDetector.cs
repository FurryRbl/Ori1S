using System;
using UnityEngine;

// Token: 0x0200046C RID: 1132
public class CapsuleCrushDetector : CharacterState, ISeinReceiver
{
	// Token: 0x17000572 RID: 1394
	// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0008A0A3 File Offset: 0x000882A3
	public PlatformBehaviour PlatformBehaviour
	{
		get
		{
			return this.Sein.PlatformBehaviour;
		}
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x0008A0B0 File Offset: 0x000882B0
	public void OnTriggerEnter(Collider collider)
	{
		this.OnTrigger(collider);
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x0008A0B9 File Offset: 0x000882B9
	public void OnTriggerStay(Collider collider)
	{
		this.OnTrigger(collider);
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x0008A0C4 File Offset: 0x000882C4
	private void OnTrigger(Collider collider)
	{
		if (collider.GetComponent<CrushPlayer>())
		{
			Damage damage = new Damage(10000f, Vector2.zero, this.Sein.Position, DamageType.Crush, base.gameObject);
			damage.DealToComponents(this.Sein.gameObject);
		}
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x0008A115 File Offset: 0x00088315
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Mortality.CrushDetector = this;
	}

	// Token: 0x04001AF4 RID: 6900
	public SeinCharacter Sein;
}
