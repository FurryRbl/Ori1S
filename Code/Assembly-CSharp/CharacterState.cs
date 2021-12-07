using System;

// Token: 0x02000070 RID: 112
public abstract class CharacterState : SaveSerialize
{
	// Token: 0x14000011 RID: 17
	// (add) Token: 0x060004C0 RID: 1216 RVA: 0x000137F9 File Offset: 0x000119F9
	// (remove) Token: 0x060004C1 RID: 1217 RVA: 0x00013812 File Offset: 0x00011A12
	public event Action OnEnterEvent = delegate()
	{
	};

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x060004C2 RID: 1218 RVA: 0x0001382B File Offset: 0x00011A2B
	// (remove) Token: 0x060004C3 RID: 1219 RVA: 0x00013844 File Offset: 0x00011A44
	public event Action OnExitEvent = delegate()
	{
	};

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000138B3 File Offset: 0x00011AB3
	// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00013860 File Offset: 0x00011A60
	public bool Active
	{
		get
		{
			return this.m_active;
		}
		set
		{
			if (this.m_active == value)
			{
				return;
			}
			this.m_active = value;
			if (this.m_active)
			{
				this.OnEnter();
				this.OnEnterEvent();
			}
			else
			{
				this.OnExit();
				this.OnExitEvent();
			}
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x000138BB File Offset: 0x00011ABB
	public static void Activate(CharacterState state)
	{
		CharacterState.Activate(state, true);
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x000138C4 File Offset: 0x00011AC4
	public static bool IsActive(CharacterState state)
	{
		return state && state.Active;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x000138DA File Offset: 0x00011ADA
	public static void Deactivate(CharacterState state)
	{
		CharacterState.Activate(state, false);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x000138E3 File Offset: 0x00011AE3
	public static void Activate(CharacterState state, bool active)
	{
		if (state)
		{
			state.Active = active;
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x000138F7 File Offset: 0x00011AF7
	public static void UpdateCharacterState(CharacterState state)
	{
		if (state != null && state.Active)
		{
			state.UpdateCharacterState();
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00013916 File Offset: 0x00011B16
	public virtual void OnExit()
	{
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00013918 File Offset: 0x00011B18
	public virtual void OnEnter()
	{
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0001391A File Offset: 0x00011B1A
	public virtual void Enter()
	{
		this.Active = true;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00013923 File Offset: 0x00011B23
	public virtual void Exit()
	{
		this.Active = false;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0001392C File Offset: 0x00011B2C
	public virtual void UpdateCharacterState()
	{
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0001392E File Offset: 0x00011B2E
	public override void Serialize(Archive ar)
	{
		this.Active = ar.Serialize(this.Active);
	}

	// Token: 0x040003D9 RID: 985
	private bool m_active;
}
