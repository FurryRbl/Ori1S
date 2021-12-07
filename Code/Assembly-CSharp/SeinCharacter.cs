using System;
using Game;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class SeinCharacter : MonoBehaviour, ICharacter
{
	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x0600039D RID: 925 RVA: 0x0000E9BC File Offset: 0x0000CBBC
	public Vector2 PhysicsSpeed
	{
		get
		{
			PlatformMovement platformMovement = this.PlatformBehaviour.PlatformMovement;
			return (!platformMovement.IsOnGround) ? platformMovement.WorldSpeed : (platformMovement.GroundNormal * platformMovement.LocalSpeedY + platformMovement.GroundBinormal * platformMovement.LocalSpeedX);
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600039E RID: 926 RVA: 0x0000EA1C File Offset: 0x0000CC1C
	public CharacterAnimationSystem Animation
	{
		get
		{
			return this.PlatformBehaviour.Visuals.Animation;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x0600039F RID: 927 RVA: 0x0000EA2E File Offset: 0x0000CC2E
	public bool IsSuspended
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement.IsSuspended;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000EA40 File Offset: 0x0000CC40
	// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000EA52 File Offset: 0x0000CC52
	public Vector3 Position
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement.Position;
		}
		set
		{
			this.PlatformBehaviour.PlatformMovement.Position = value;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000EA73 File Offset: 0x0000CC73
	// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000EA65 File Offset: 0x0000CC65
	public bool Active
	{
		get
		{
			return base.gameObject.activeSelf;
		}
		set
		{
			base.gameObject.SetActive(value);
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0000EA80 File Offset: 0x0000CC80
	public void Awake()
	{
		Characters.Sein = this;
		Characters.Current = this;
		this.Input = new SeinInput(this);
		this.MakeBelongToSein(base.gameObject);
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
	public void OnDestroy()
	{
		if (Characters.Sein == this)
		{
			Characters.Sein = null;
		}
		if (object.ReferenceEquals(Characters.Current, this))
		{
			Characters.Current = null;
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0000EAED File Offset: 0x0000CCED
	public void MakeBelongToSein(GameObject go)
	{
		go.BroadcastMessage("SetReferenceToSein", this, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x0000EAFC File Offset: 0x0000CCFC
	public void FixedUpdate()
	{
		this.Input.Update();
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x0000EB09 File Offset: 0x0000CD09
	public void Activate(bool active)
	{
		base.gameObject.SetActive(active);
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000EB17 File Offset: 0x0000CD17
	public GameObject GameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060003AA RID: 938 RVA: 0x0000EB1F File Offset: 0x0000CD1F
	// (set) Token: 0x060003AB RID: 939 RVA: 0x0000EB31 File Offset: 0x0000CD31
	public bool FaceLeft
	{
		get
		{
			return this.Animation.SpriteMirror.FaceLeft;
		}
		set
		{
			this.Animation.SpriteMirror.FaceLeft = value;
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060003AC RID: 940 RVA: 0x0000EB44 File Offset: 0x0000CD44
	// (set) Token: 0x060003AD RID: 941 RVA: 0x0000EB5B File Offset: 0x0000CD5B
	public Vector3 Speed
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement.LocalSpeed;
		}
		set
		{
			this.PlatformBehaviour.PlatformMovement.LocalSpeed = value;
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060003AE RID: 942 RVA: 0x0000EB73 File Offset: 0x0000CD73
	public Transform Transform
	{
		get
		{
			return base.transform;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060003AF RID: 943 RVA: 0x0000EB7B File Offset: 0x0000CD7B
	public bool IsOnGround
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement.IsOnGround;
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0000EB8D File Offset: 0x0000CD8D
	public void PlaceOnGround()
	{
		this.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0000EBAC File Offset: 0x0000CDAC
	public void ResetAirLimits()
	{
		if (this.Abilities.DoubleJump)
		{
			this.Abilities.DoubleJump.ResetDoubleJump();
		}
		if (this.Abilities.Dash)
		{
			this.Abilities.Dash.ResetDashLimit();
		}
	}

	// Token: 0x040002B1 RID: 689
	public SeinAbilities Abilities;

	// Token: 0x040002B2 RID: 690
	public CloneOfSeinForPortals CloneOfSeinForPortals;

	// Token: 0x040002B3 RID: 691
	public SeinController Controller;

	// Token: 0x040002B4 RID: 692
	public SeinCutsceneBlocked CutsceneBlocked;

	// Token: 0x040002B5 RID: 693
	public SeinCutsceneMovement CutsceneMovement;

	// Token: 0x040002B6 RID: 694
	public SeinDoorHandler DoorHandler;

	// Token: 0x040002B7 RID: 695
	public SeinSoulFlame SoulFlame;

	// Token: 0x040002B8 RID: 696
	public SeinInventory Inventory;

	// Token: 0x040002B9 RID: 697
	public SeinEnvironmentForceController ForceController;

	// Token: 0x040002BA RID: 698
	public SeinInput Input;

	// Token: 0x040002BB RID: 699
	public SeinLevel Level;

	// Token: 0x040002BC RID: 700
	public SeinEnergy Energy;

	// Token: 0x040002BD RID: 701
	public SeinMortality Mortality;

	// Token: 0x040002BE RID: 702
	public SeinPickupProcessor PickupHandler;

	// Token: 0x040002BF RID: 703
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x040002C0 RID: 704
	public PlayerAbilities PlayerAbilities;

	// Token: 0x040002C1 RID: 705
	public SeinPrefabFactory Prefabs;
}
