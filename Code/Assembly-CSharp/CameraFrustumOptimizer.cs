using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020003F5 RID: 1013
public class CameraFrustumOptimizer : MonoBehaviour
{
	// Token: 0x06001B82 RID: 7042 RVA: 0x000766D8 File Offset: 0x000748D8
	public static void Register(IFrustumOptimizable frustumOptimizable)
	{
		CameraFrustumOptimizer.s_frustumOptimizables.Add(frustumOptimizable);
		if (Optimization.CameraFrustumOptimizer)
		{
			Optimization.CameraFrustumOptimizer.ProcessFrustumOptimizable(frustumOptimizable, true);
		}
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x0007670B File Offset: 0x0007490B
	public static void Unregister(IFrustumOptimizable frustumOptimizable)
	{
		CameraFrustumOptimizer.s_frustumOptimizables.Remove(frustumOptimizable);
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x00076718 File Offset: 0x00074918
	public static void RegisterUninitialized(IFrustumOptimizable frustumOptimizable)
	{
		CameraFrustumOptimizer.s_unitializedFrustumOptimizables.Add(frustumOptimizable);
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x00076728 File Offset: 0x00074928
	public void Awake()
	{
		Optimization.CameraFrustumOptimizer = this;
		this.m_lastCalculationCameraPosition = UI.Cameras.Current.Camera.transform.position;
		AspectRatioManager.OnAspectChanged.Add(new Action(this.OnAspectChanged));
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x0007676B File Offset: 0x0007496B
	public void OnDestroy()
	{
		AspectRatioManager.OnAspectChanged.Remove(new Action(this.OnAspectChanged));
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x00076783 File Offset: 0x00074983
	public void OnAspectChanged()
	{
		this.UpdateFrustrumOptimizables();
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x0007678B File Offset: 0x0007498B
	public void Start()
	{
		this.UpdateFrustrumOptimizables();
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x00076794 File Offset: 0x00074994
	public void FixedUpdate()
	{
		Vector3 position = UI.Cameras.Current.Camera.transform.position;
		if (Vector3.Distance(position, this.m_lastCalculationCameraPosition) > this.UpdateDelta || this.m_forceCount > 0)
		{
			if (this.m_forceCount > 0)
			{
				this.m_forceCount--;
			}
			this.UpdateFrustrumOptimizables();
			this.m_lastCalculationCameraPosition = position;
		}
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00076800 File Offset: 0x00074A00
	public float ExpansionAmount
	{
		get
		{
			return (this.UpdateDelta + this.Padding) * 2f;
		}
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x00076815 File Offset: 0x00074A15
	public static void ForceUpdate()
	{
		if (Optimization.CameraFrustumOptimizer)
		{
			Optimization.CameraFrustumOptimizer.m_forceCount = 3;
		}
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x00076834 File Offset: 0x00074A34
	public void UpdateFrustrumOptimizables()
	{
		for (int i = 0; i < CameraFrustumOptimizer.s_frustumOptimizables.Count; i++)
		{
			IFrustumOptimizable o = CameraFrustumOptimizer.s_frustumOptimizables[i];
			this.ProcessFrustumOptimizable(o, false);
		}
		CameraFrustumOptimizer.s_unitializedFrustumOptimizables.RemoveAll(new Predicate<IFrustumOptimizable>(this.IsNull));
		for (int j = 0; j < CameraFrustumOptimizer.s_unitializedFrustumOptimizables.Count; j++)
		{
			IFrustumOptimizable frustumOptimizable = CameraFrustumOptimizer.s_unitializedFrustumOptimizables[j];
			this.ProcessFrustumOptimizable(frustumOptimizable, false);
			if (frustumOptimizable.InsideFrustum)
			{
				CameraFrustumOptimizer.s_unitializedFrustumOptimizables.RemoveAt(j);
				j--;
			}
		}
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x000768D0 File Offset: 0x00074AD0
	private bool IsNull(IFrustumOptimizable o)
	{
		return o as Component == null;
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x000768E0 File Offset: 0x00074AE0
	public void ProcessFrustumOptimizable(IFrustumOptimizable o, bool isFirstTime)
	{
		Bounds bounds = o.Bounds;
		bounds.extents = new Vector3(Mathf.Abs(bounds.extents.x) + this.ExpansionAmount, Mathf.Abs(bounds.extents.y) + this.ExpansionAmount, Mathf.Abs(bounds.extents.z) + this.ExpansionAmount);
		bool flag = false;
		for (int i = 0; i < UI.Cameras.Manager.Cameras.Count; i++)
		{
			CameraController cameraController = UI.Cameras.Manager.Cameras[i];
			flag = cameraController.InsideFrustum(bounds);
		}
		if (flag)
		{
			if (!o.InsideFrustum)
			{
				o.OnFrustumEnter();
			}
		}
		else if (o.InsideFrustum || isFirstTime)
		{
			o.OnFrustumExit();
		}
	}

	// Token: 0x040017F1 RID: 6129
	public float UpdateDelta = 5f;

	// Token: 0x040017F2 RID: 6130
	public float Padding;

	// Token: 0x040017F3 RID: 6131
	private Vector3 m_lastCalculationCameraPosition;

	// Token: 0x040017F4 RID: 6132
	private static readonly AllContainer<IFrustumOptimizable> s_frustumOptimizables = new AllContainer<IFrustumOptimizable>();

	// Token: 0x040017F5 RID: 6133
	private static readonly List<IFrustumOptimizable> s_unitializedFrustumOptimizables = new List<IFrustumOptimizable>();

	// Token: 0x040017F6 RID: 6134
	private int m_forceCount;
}
