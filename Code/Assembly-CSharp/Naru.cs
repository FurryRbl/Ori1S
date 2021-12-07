using System;
using Game;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class Naru : MonoBehaviour, ICharacter
{
	// Token: 0x0600028A RID: 650 RVA: 0x0000ACCB File Offset: 0x00008ECB
	public void Awake()
	{
		Characters.Naru = this;
		Characters.Current = this;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000ACDC File Offset: 0x00008EDC
	public void OnDestroy()
	{
		if (Characters.Naru == this)
		{
			Characters.Naru = null;
		}
		if (object.ReferenceEquals(Characters.Current, this))
		{
			Characters.Current = null;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AD15 File Offset: 0x00008F15
	// (set) Token: 0x0600028D RID: 653 RVA: 0x0000AD22 File Offset: 0x00008F22
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000AD30 File Offset: 0x00008F30
	public void Activate(bool active)
	{
		base.gameObject.SetActive(active);
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x0600028F RID: 655 RVA: 0x0000AD3E File Offset: 0x00008F3E
	public GameObject GameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000290 RID: 656 RVA: 0x0000AD46 File Offset: 0x00008F46
	// (set) Token: 0x06000291 RID: 657 RVA: 0x0000AD58 File Offset: 0x00008F58
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

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000292 RID: 658 RVA: 0x0000AD6B File Offset: 0x00008F6B
	// (set) Token: 0x06000293 RID: 659 RVA: 0x0000AD82 File Offset: 0x00008F82
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

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000294 RID: 660 RVA: 0x0000AD9A File Offset: 0x00008F9A
	public Transform Transform
	{
		get
		{
			return base.transform;
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000295 RID: 661 RVA: 0x0000ADA2 File Offset: 0x00008FA2
	public bool IsOnGround
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement.IsOnGround;
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0000ADB4 File Offset: 0x00008FB4
	public void PlaceOnGround()
	{
		this.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
	}

	// Token: 0x040001DB RID: 475
	public CharacterAnimationSystem Animation;

	// Token: 0x040001DC RID: 476
	public NaruController Controller;

	// Token: 0x040001DD RID: 477
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x040001DE RID: 478
	public bool SeinNaruComboEnabled;

	// Token: 0x040001DF RID: 479
	public NaruSounds Sounds;
}
