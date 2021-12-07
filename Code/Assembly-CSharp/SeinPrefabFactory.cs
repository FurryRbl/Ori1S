using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class SeinPrefabFactory : SaveSerialize, ISeinReceiver
{
	// Token: 0x0600078D RID: 1933 RVA: 0x0001F864 File Offset: 0x0001DA64
	public new void Awake()
	{
		base.Awake();
		this.Bash = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Bash);
		this.Carry = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Carry);
		this.ChargeJump = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.ChargeJump);
		this.Crouch = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Crouch);
		this.DoubleJump = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.DoubleJump);
		this.Fall = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Fall);
		this.Glide = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Glide);
		this.GrabPushPull = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.GrabPushPull);
		this.GrabWall = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.GrabWall);
		this.Jump = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Jump);
		this.PushAgainstWall = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.PushAgainstWall);
		this.Run = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Run);
		this.Idle = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Idle);
		this.SpiritFlame = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.SpiritFlame);
		this.StandingOnEdge = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.StandingOnEdge);
		this.Stomp = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Stomp);
		this.WallJump = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.WallJump);
		this.WallSlide = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.WallSlide);
		this.Swimming = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Swimming);
		this.SoulFlame = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.SoulFlame);
		this.PickupProcessor = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.PickupProcessor);
		this.Dash = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Dash);
		this.Grenade = new SeinNestedPrefab(this.Sein, this.SeinPrefabSet.Grenade);
		this.Carry.IsInstantiated = true;
		this.Crouch.IsInstantiated = true;
		this.Fall.IsInstantiated = true;
		this.Jump.IsInstantiated = true;
		this.PushAgainstWall.IsInstantiated = true;
		this.Run.IsInstantiated = true;
		this.Idle.IsInstantiated = true;
		this.StandingOnEdge.IsInstantiated = true;
		this.Swimming.IsInstantiated = true;
		this.SoulFlame.IsInstantiated = true;
		this.GrabPushPull.IsInstantiated = true;
		this.SpiritFlame.IsInstantiated = true;
		this.PickupProcessor.IsInstantiated = true;
		this.m_prefabs = new SeinNestedPrefab[]
		{
			this.Bash,
			this.Carry,
			this.ChargeJump,
			this.Crouch,
			this.DoubleJump,
			this.Fall,
			this.Glide,
			this.GrabPushPull,
			this.GrabWall,
			this.Jump,
			this.PushAgainstWall,
			this.Run,
			this.SpiritFlame,
			this.StandingOnEdge,
			this.Stomp,
			this.WallJump,
			this.WallSlide,
			this.Swimming,
			this.SoulFlame,
			this.PickupProcessor,
			this.Dash,
			this.Grenade
		};
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0001FC77 File Offset: 0x0001DE77
	public void Start()
	{
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0001FC7C File Offset: 0x0001DE7C
	public void EnsureRightPrefabsAreThereForAbilities()
	{
		this.WallJump.IsInstantiated = this.Sein.PlayerAbilities.WallJump.HasAbility;
		this.WallSlide.IsInstantiated = true;
		this.Stomp.IsInstantiated = this.Sein.PlayerAbilities.Stomp.HasAbility;
		this.DoubleJump.IsInstantiated = this.Sein.PlayerAbilities.DoubleJump.HasAbility;
		this.ChargeJump.IsInstantiated = this.Sein.PlayerAbilities.ChargeJump.HasAbility;
		this.GrabWall.IsInstantiated = this.Sein.PlayerAbilities.Climb.HasAbility;
		this.Bash.IsInstantiated = this.Sein.PlayerAbilities.Bash.HasAbility;
		this.Glide.IsInstantiated = this.Sein.PlayerAbilities.Glide.HasAbility;
		this.Dash.IsInstantiated = this.Sein.PlayerAbilities.Dash.HasAbility;
		this.Grenade.IsInstantiated = this.Sein.PlayerAbilities.Grenade.HasAbility;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0001FDB5 File Offset: 0x0001DFB5
	public void PushState()
	{
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0001FDB7 File Offset: 0x0001DFB7
	public void PopState()
	{
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0001FDBC File Offset: 0x0001DFBC
	public override void Serialize(Archive ar)
	{
		try
		{
			foreach (SeinNestedPrefab seinNestedPrefab in this.m_prefabs)
			{
				seinNestedPrefab.IsInstantiated = ar.Serialize(seinNestedPrefab.IsInstantiated);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0001FE1C File Offset: 0x0001E01C
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x040005C5 RID: 1477
	public SeinCharacter Sein;

	// Token: 0x040005C6 RID: 1478
	public SeinPrefabSet SeinPrefabSet;

	// Token: 0x040005C7 RID: 1479
	private SeinNestedPrefab[] m_prefabs = new SeinNestedPrefab[0];

	// Token: 0x040005C8 RID: 1480
	public SeinNestedPrefab Bash;

	// Token: 0x040005C9 RID: 1481
	public SeinNestedPrefab Carry;

	// Token: 0x040005CA RID: 1482
	public SeinNestedPrefab ChargeJump;

	// Token: 0x040005CB RID: 1483
	public SeinNestedPrefab Crouch;

	// Token: 0x040005CC RID: 1484
	public SeinNestedPrefab DoubleJump;

	// Token: 0x040005CD RID: 1485
	public SeinNestedPrefab Fall;

	// Token: 0x040005CE RID: 1486
	public SeinNestedPrefab Glide;

	// Token: 0x040005CF RID: 1487
	public SeinNestedPrefab GrabPushPull;

	// Token: 0x040005D0 RID: 1488
	public SeinNestedPrefab GrabWall;

	// Token: 0x040005D1 RID: 1489
	public SeinNestedPrefab Jump;

	// Token: 0x040005D2 RID: 1490
	public SeinNestedPrefab PushAgainstWall;

	// Token: 0x040005D3 RID: 1491
	public SeinNestedPrefab Run;

	// Token: 0x040005D4 RID: 1492
	public SeinNestedPrefab Idle;

	// Token: 0x040005D5 RID: 1493
	public SeinNestedPrefab SpiritFlame;

	// Token: 0x040005D6 RID: 1494
	public SeinNestedPrefab StandingOnEdge;

	// Token: 0x040005D7 RID: 1495
	public SeinNestedPrefab Stomp;

	// Token: 0x040005D8 RID: 1496
	public SeinNestedPrefab WallJump;

	// Token: 0x040005D9 RID: 1497
	public SeinNestedPrefab WallSlide;

	// Token: 0x040005DA RID: 1498
	public SeinNestedPrefab Swimming;

	// Token: 0x040005DB RID: 1499
	public SeinNestedPrefab SoulFlame;

	// Token: 0x040005DC RID: 1500
	public SeinNestedPrefab Dash;

	// Token: 0x040005DD RID: 1501
	public SeinNestedPrefab Grenade;

	// Token: 0x040005DE RID: 1502
	public SeinNestedPrefab PickupProcessor;
}
