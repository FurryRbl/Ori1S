using System;
using Game;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class Objective : ScriptableObject
{
	// Token: 0x06000DA5 RID: 3493 RVA: 0x0003FA81 File Offset: 0x0003DC81
	[ContextMenu("Add objective")]
	public void AddObjective()
	{
		Objectives.AddObjective(this);
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0003FA89 File Offset: 0x0003DC89
	[ContextMenu("Complete objective")]
	public void CompleteObjective()
	{
		Objectives.CompleteObjective(this);
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0003FA91 File Offset: 0x0003DC91
	// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0003FA99 File Offset: 0x0003DC99
	public Transform AreaMapTransform { get; private set; }

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0003FAA2 File Offset: 0x0003DCA2
	// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0003FAAA File Offset: 0x0003DCAA
	public Transform WorldMapTransform { get; private set; }

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0003FAB3 File Offset: 0x0003DCB3
	public RuntimeGameWorldArea Area
	{
		get
		{
			return World.CurrentArea;
		}
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x0003FABC File Offset: 0x0003DCBC
	public void Show()
	{
		if (this.AreaMapTransform)
		{
			InstantiateUtility.Destroy(this.AreaMapTransform.gameObject);
		}
		if (this.WorldMapTransform)
		{
			InstantiateUtility.Destroy(this.WorldMapTransform.gameObject);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(AreaMapUI.Instance.ObjectivePrefab);
		this.AreaMapTransform = gameObject.transform;
		this.AreaMapTransform.parent = AreaMapUI.Instance.FadeOutGroup;
		TransparencyAnimator.Register(this.AreaMapTransform);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(AreaMapUI.Instance.ObjectivePrefab);
		this.WorldMapTransform = gameObject2.transform;
		this.WorldMapTransform.parent = WorldMapUI.Instance.FadeOutGroup;
		TransparencyAnimator.Register(this.WorldMapTransform);
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0003FB84 File Offset: 0x0003DD84
	public void Update()
	{
		if (this.AreaMapTransform)
		{
			this.AreaMapTransform.position = AreaMapUI.Instance.Navigation.WorldToMapPosition(this.Position);
		}
		if (this.WorldMapTransform)
		{
			this.WorldMapTransform.position = WorldMapUI.Instance.WorldToUIPosition(this.Position);
		}
		if (this.m_appearEffect)
		{
			this.m_appearEffect.transform.position = this.WorldMapTransform.position;
		}
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0003FC1C File Offset: 0x0003DE1C
	public void Hide()
	{
		if (this.AreaMapTransform)
		{
			InstantiateUtility.Destroy(this.AreaMapTransform.gameObject);
		}
		if (this.WorldMapTransform)
		{
			InstantiateUtility.Destroy(this.WorldMapTransform.gameObject);
		}
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0003FC69 File Offset: 0x0003DE69
	public void Complete()
	{
		this.Hide();
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0003FC74 File Offset: 0x0003DE74
	public void SpawnAppearEffect()
	{
		foreach (BaseAnimator baseAnimator in this.AreaMapTransform.GetComponents<BaseAnimator>())
		{
			baseAnimator.Initialize();
			baseAnimator.AnimatorDriver.Restart();
		}
		foreach (BaseAnimator baseAnimator2 in this.WorldMapTransform.GetComponents<BaseAnimator>())
		{
			baseAnimator2.Initialize();
			baseAnimator2.AnimatorDriver.Restart();
		}
		this.m_appearEffect = (GameObject)InstantiateUtility.Instantiate(GameMapUI.Instance.ShowObjective.ObjectiveAppearEffect);
	}

	// Token: 0x04000B1A RID: 2842
	public Texture2D Icon;

	// Token: 0x04000B1B RID: 2843
	public Vector2 Position;

	// Token: 0x04000B1C RID: 2844
	private GameObject m_appearEffect;
}
