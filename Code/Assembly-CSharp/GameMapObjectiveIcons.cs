using System;
using Game;
using UnityEngine;

// Token: 0x0200087E RID: 2174
public class GameMapObjectiveIcons : MonoBehaviour
{
	// Token: 0x0600310D RID: 12557 RVA: 0x000D1024 File Offset: 0x000CF224
	public void ShowIcons()
	{
		for (int i = 0; i < Objectives.All.Count; i++)
		{
			Objective objective = Objectives.All[i];
			objective.Show();
		}
	}

	// Token: 0x0600310E RID: 12558 RVA: 0x000D1060 File Offset: 0x000CF260
	public void HideIcons()
	{
		for (int i = 0; i < Objectives.All.Count; i++)
		{
			Objective objective = Objectives.All[i];
			objective.Hide();
		}
	}

	// Token: 0x0600310F RID: 12559 RVA: 0x000D109C File Offset: 0x000CF29C
	public void Advance()
	{
		for (int i = 0; i < Objectives.All.Count; i++)
		{
			Objective objective = Objectives.All[i];
			objective.Update();
		}
	}
}
