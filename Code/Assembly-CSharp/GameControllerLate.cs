using System;
using UnityEngine;

// Token: 0x02000974 RID: 2420
public class GameControllerLate : MonoBehaviour
{
	// Token: 0x06003514 RID: 13588 RVA: 0x000DEA99 File Offset: 0x000DCC99
	public void Start()
	{
		GameController.Instance.GameScheduler.OnGameStartLate.Call();
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x000DEAB0 File Offset: 0x000DCCB0
	public void FixedUpdate()
	{
		GameController.Instance.GameScheduler.OnGameFixedUpdateLate.Call();
		if (LateStartHook.Actions.Count > 0)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.LateStartHookPrefab);
		}
	}

	// Token: 0x04002FC0 RID: 12224
	public GameObject LateStartHookPrefab;
}
