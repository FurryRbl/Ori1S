using System;
using Game;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class CutsceneController : MonoBehaviour
{
	// Token: 0x060008B2 RID: 2226 RVA: 0x000255C9 File Offset: 0x000237C9
	public void Start()
	{
		this.ChangeState(this.CurrentState);
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x000255D8 File Offset: 0x000237D8
	public void FixedUpdate()
	{
		if (!this.m_foundSein && Characters.Sein)
		{
			this.m_foundSein = true;
			this.m_cutsceneSein = Characters.Sein;
		}
		if (Characters.Sein != this.m_cutsceneSein)
		{
			return;
		}
		if (Characters.Sein == null)
		{
			return;
		}
		if (this.CurrentState)
		{
			this.CurrentState.OnUpdate();
			this.CurrentStateTime += Time.deltaTime;
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00025668 File Offset: 0x00023868
	public void ChangeState(CutsceneState state)
	{
		if (state == null)
		{
			return;
		}
		if (this.CurrentState != null)
		{
			this.CurrentState.OnExit();
		}
		this.CurrentStateTime = 0f;
		this.CurrentState = state;
		if (this.CurrentState != null)
		{
			this.CurrentState.OnEnter();
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x000256CC File Offset: 0x000238CC
	public void Awake()
	{
		foreach (CutsceneState cutsceneState in base.gameObject.GetComponentsInChildren<CutsceneState>())
		{
			cutsceneState.Parent = this;
		}
		if (this.CutsceneMusicPlayer != null)
		{
			CutsceneMusicPlayer cutsceneMusicPlayer = UnityEngine.Object.Instantiate<CutsceneMusicPlayer>(this.CutsceneMusicPlayer);
			cutsceneMusicPlayer.Cutscene = this;
			cutsceneMusicPlayer.Play(false);
		}
	}

	// Token: 0x04000703 RID: 1795
	public CutsceneState CurrentState;

	// Token: 0x04000704 RID: 1796
	public CutsceneMusicPlayer CutsceneMusicPlayer;

	// Token: 0x04000705 RID: 1797
	public float CurrentStateTime;

	// Token: 0x04000706 RID: 1798
	private SeinCharacter m_cutsceneSein;

	// Token: 0x04000707 RID: 1799
	private bool m_foundSein;
}
