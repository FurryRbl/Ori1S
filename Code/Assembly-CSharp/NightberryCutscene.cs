using System;
using Game;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class NightberryCutscene : CutsceneController
{
	// Token: 0x060008F2 RID: 2290 RVA: 0x000268EC File Offset: 0x00024AEC
	public new void Start()
	{
		Characters.Sein.Prefabs.Fall.IsInstantiated = false;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Characters.Sein.Prefabs.SeinPrefabSet.CutsceneBlock);
		Characters.Sein.CutsceneBlocked = gameObject.GetComponent<SeinCutsceneBlocked>();
		Characters.Sein.CutsceneBlocked.Sein = Characters.Sein;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(Characters.Sein.Prefabs.SeinPrefabSet.CutsceneMovement);
		Characters.Sein.CutsceneMovement = gameObject2.GetComponent<SeinCutsceneMovement>();
		Characters.Sein.CutsceneMovement.Sein = Characters.Sein;
		Characters.Sein.Controller.EnterPlayingAnimation();
		base.ChangeState(this.CurrentState);
		Characters.Sein.CutsceneBlocked.CurrentState = SeinCutsceneBlocked.State.Normal;
		foreach (Collider collider in Physics.OverlapSphere(Characters.Sein.transform.position, 500f))
		{
			ICarryable carryable = collider.gameObject.FindComponent<ICarryable>();
			if (carryable != null && carryable.CanBeCarried())
			{
				return;
			}
		}
	}
}
