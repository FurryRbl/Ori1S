using System;
using UnityEngine;

// Token: 0x02000838 RID: 2104
public class ShaderAnimationTimeDriver : MonoBehaviour, ISuspendable
{
	// Token: 0x170007B0 RID: 1968
	// (get) Token: 0x06003003 RID: 12291 RVA: 0x000CB75D File Offset: 0x000C995D
	public float GameTime
	{
		get
		{
			return this.m_time;
		}
	}

	// Token: 0x06003004 RID: 12292 RVA: 0x000CB765 File Offset: 0x000C9965
	private void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06003005 RID: 12293 RVA: 0x000CB76D File Offset: 0x000C996D
	private void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003006 RID: 12294 RVA: 0x000CB775 File Offset: 0x000C9975
	private void Update()
	{
		if (this.IsSuspended)
		{
		}
		this.m_time += Time.deltaTime * this.GameTimeMultiplier;
		Shader.SetGlobalFloat("_GameTime", this.m_time);
	}

	// Token: 0x170007B1 RID: 1969
	// (get) Token: 0x06003007 RID: 12295 RVA: 0x000CB7AB File Offset: 0x000C99AB
	// (set) Token: 0x06003008 RID: 12296 RVA: 0x000CB7B3 File Offset: 0x000C99B3
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
		}
	}

	// Token: 0x04002B3E RID: 11070
	public float GameTimeMultiplier = 1f;

	// Token: 0x04002B3F RID: 11071
	private float m_time;

	// Token: 0x04002B40 RID: 11072
	private bool m_isSuspended;
}
