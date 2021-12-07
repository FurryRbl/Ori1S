using System;
using Game;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class CutsceneTrigger : MonoBehaviour
{
	// Token: 0x060008C7 RID: 2247 RVA: 0x00025BC0 File Offset: 0x00023DC0
	public void OnTriggerEnter(Collider collider)
	{
		SeinController component = collider.GetComponent<SeinController>();
		if (component == null)
		{
			return;
		}
		if (!this.m_wasTriggered)
		{
			Vector3 position = Characters.Sein.Position;
			BoxCollider boxCollider = (BoxCollider)base.GetComponent<Collider>();
			Vector3 a = base.transform.position;
			a += boxCollider.center;
			if (a.x > position.x)
			{
				position.x = a.x - boxCollider.size.x * 0.5f - Characters.Sein.PlatformBehaviour.CapsuleController.OriginalRadius;
			}
			if (a.x < position.x)
			{
				position.x = a.x + boxCollider.size.x * 0.5f + Characters.Sein.PlatformBehaviour.CapsuleController.OriginalRadius;
			}
			Characters.Sein.Position = position;
			this.Cutscene.ChangeState(this.State);
			this.m_wasTriggered = true;
		}
	}

	// Token: 0x0400071E RID: 1822
	public CutsceneController Cutscene;

	// Token: 0x0400071F RID: 1823
	public CutsceneState State;

	// Token: 0x04000720 RID: 1824
	private bool m_wasTriggered;
}
