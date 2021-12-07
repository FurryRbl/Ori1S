using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class LoadFromMasterAtStart : MonoBehaviour
{
	// Token: 0x060006F0 RID: 1776 RVA: 0x0001C808 File Offset: 0x0001AA08
	public void Start()
	{
		base.gameObject.GetComponentsInChildren<SaveSerialize>(LoadFromMasterAtStart.s_saveSerialized);
		SaveScene master = Game.Checkpoint.SaveGameData.Master;
		SaveSceneManager.Master.Load(master, new HashSet<SaveSerialize>(LoadFromMasterAtStart.s_saveSerialized));
		this.AfterLoading();
		for (int i = 0; i < LoadFromMasterAtStart.s_saveSerialized.Count; i++)
		{
			SaveSceneManager.Master.Save(master, LoadFromMasterAtStart.s_saveSerialized[i]);
		}
		LoadFromMasterAtStart.s_saveSerialized.Clear();
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x04000527 RID: 1319
	private static readonly List<SaveSerialize> s_saveSerialized = new List<SaveSerialize>();

	// Token: 0x04000528 RID: 1320
	public Action AfterLoading = delegate()
	{
	};
}
