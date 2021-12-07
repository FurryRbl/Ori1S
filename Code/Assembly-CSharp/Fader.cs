using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class Fader : Suspendable
{
	// Token: 0x06000FA3 RID: 4003 RVA: 0x00047C28 File Offset: 0x00045E28
	public void ChangeState(Fader.State state)
	{
		switch (this.CurrentState)
		{
		}
		this.m_stateCurrentTime = 0f;
		this.CurrentState = state;
		switch (this.CurrentState)
		{
		}
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x00047CB4 File Offset: 0x00045EB4
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case Fader.State.FadeIn:
			this.m_stateCurrentTime += Time.deltaTime;
			this.SetOpacity(this.m_stateCurrentTime / this.FadeInTime);
			if (this.m_stateCurrentTime > this.FadeInTime)
			{
				this.OnFadeInFinished();
				this.SetOpacity(1f);
				this.ChangeState((this.FadeStayTime != 0f) ? Fader.State.FadeStay : Fader.State.FadeOut);
			}
			break;
		case Fader.State.FadeStay:
			this.m_stateCurrentTime += Time.deltaTime;
			if (this.m_stateCurrentTime >= this.FadeStayTime)
			{
				this.ChangeState(Fader.State.FadeOut);
			}
			break;
		case Fader.State.FadeOut:
			this.m_stateCurrentTime += Time.deltaTime;
			this.SetOpacity(1f - this.m_stateCurrentTime / this.FadeOutTime);
			if (this.m_stateCurrentTime > this.FadeOutTime)
			{
				this.OnFadeOutFinished();
				this.SetOpacity(0f);
				this.ChangeState(Fader.State.Invisible);
				InstantiateUtility.Destroy(base.gameObject);
			}
			break;
		}
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x00047DDC File Offset: 0x00045FDC
	protected void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.m_guitexture = base.GetComponent<GUITexture>();
		switch (this.CurrentState)
		{
		case Fader.State.FadeIn:
			this.SetOpacity(0f);
			break;
		case Fader.State.FadeStay:
			this.SetOpacity(1f);
			break;
		case Fader.State.FadeOut:
			this.SetOpacity(1f);
			break;
		case Fader.State.Invisible:
			this.SetOpacity(0f);
			break;
		}
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x00047E62 File Offset: 0x00046062
	public void ForceToOpaque()
	{
		this.SetOpacity(1f);
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x00047E6F File Offset: 0x0004606F
	public virtual void OnFadeInFinished()
	{
		this.OnFadeInEvent();
		UberGCManager.CollectResourcesIfNeeded();
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x00047E81 File Offset: 0x00046081
	public virtual void OnFadeOutFinished()
	{
		this.OnFadeOutEvent();
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00047E8E File Offset: 0x0004608E
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.UpdateState();
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x00047EA4 File Offset: 0x000460A4
	private void SetOpacity(float opacity)
	{
		Color color = this.m_guitexture.color;
		color.a = opacity / 2f;
		this.m_guitexture.color = color;
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00047ED7 File Offset: 0x000460D7
	// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00047EDF File Offset: 0x000460DF
	public override bool IsSuspended { get; set; }

	// Token: 0x04000C55 RID: 3157
	public float FadeInTime = 1f;

	// Token: 0x04000C56 RID: 3158
	public float FadeStayTime;

	// Token: 0x04000C57 RID: 3159
	public float FadeOutTime = 1f;

	// Token: 0x04000C58 RID: 3160
	private float m_stateCurrentTime;

	// Token: 0x04000C59 RID: 3161
	public Fader.State CurrentState;

	// Token: 0x04000C5A RID: 3162
	private GUITexture m_guitexture;

	// Token: 0x04000C5B RID: 3163
	public Action OnFadeInEvent = delegate()
	{
	};

	// Token: 0x04000C5C RID: 3164
	public Action OnFadeOutEvent = delegate()
	{
	};

	// Token: 0x02000862 RID: 2146
	public enum State
	{
		// Token: 0x04002BE3 RID: 11235
		FadeIn,
		// Token: 0x04002BE4 RID: 11236
		FadeStay,
		// Token: 0x04002BE5 RID: 11237
		FadeOut,
		// Token: 0x04002BE6 RID: 11238
		Invisible
	}
}
