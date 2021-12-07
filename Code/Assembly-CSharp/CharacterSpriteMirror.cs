using System;
using Game;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class CharacterSpriteMirror : CharacterState
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000099 RID: 153 RVA: 0x000046BD File Offset: 0x000028BD
	// (set) Token: 0x0600009A RID: 154 RVA: 0x000046C5 File Offset: 0x000028C5
	public int Lock
	{
		get
		{
			return this.m_lock;
		}
		set
		{
			this.m_lock = value;
			if (this.m_lock < 0)
			{
				this.m_lock = 0;
			}
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600009C RID: 156 RVA: 0x00004717 File Offset: 0x00002917
	// (set) Token: 0x0600009B RID: 155 RVA: 0x000046E4 File Offset: 0x000028E4
	public bool FaceLeft
	{
		get
		{
			return this.m_faceLeft;
		}
		set
		{
			if (this.Lock > 0)
			{
				return;
			}
			if (this.m_faceLeft != value)
			{
				this.m_faceLeft = value;
				this.UpdateMaterial();
			}
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00004720 File Offset: 0x00002920
	public override void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		base.Active = true;
		if (this.StartFacingLeft)
		{
			this.FaceLeft = true;
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00004762 File Offset: 0x00002962
	public override void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600009F RID: 159 RVA: 0x0000477C File Offset: 0x0000297C
	public void UpdateMaterial()
	{
		Vector3 localEulerAngles = base.transform.localEulerAngles;
		localEulerAngles.y = (float)((!this.FaceLeft) ? 0 : 180);
		base.transform.localEulerAngles = localEulerAngles;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000047C0 File Offset: 0x000029C0
	public override void Serialize(Archive ar)
	{
		ar.Serialize(this.Lock);
		if (ar.Reading)
		{
			this.Lock = 0;
		}
		ar.Serialize(this.FaceLeft);
		base.Serialize(ar);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00004800 File Offset: 0x00002A00
	public void OnRestoreCheckpoint()
	{
		this.Lock = 0;
	}

	// Token: 0x040000A7 RID: 167
	private int m_lock;

	// Token: 0x040000A8 RID: 168
	public bool StartFacingLeft;

	// Token: 0x040000A9 RID: 169
	private bool m_faceLeft;
}
