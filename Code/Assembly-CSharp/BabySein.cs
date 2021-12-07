using System;
using Game;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class BabySein : MonoBehaviour, ICharacter
{
	// Token: 0x06000054 RID: 84 RVA: 0x00003531 File Offset: 0x00001731
	public void Awake()
	{
		Characters.BabySein = this;
		Characters.Current = this;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003540 File Offset: 0x00001740
	public void OnDestroy()
	{
		if (Characters.BabySein == this)
		{
			Characters.BabySein = null;
		}
		if (object.ReferenceEquals(Characters.Current, this))
		{
			Characters.Current = null;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000056 RID: 86 RVA: 0x00003579 File Offset: 0x00001779
	// (set) Token: 0x06000057 RID: 87 RVA: 0x00003586 File Offset: 0x00001786
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

	// Token: 0x06000058 RID: 88 RVA: 0x00003594 File Offset: 0x00001794
	public void Activate(bool active)
	{
		base.gameObject.SetActive(active);
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000059 RID: 89 RVA: 0x000035A2 File Offset: 0x000017A2
	public GameObject GameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600005A RID: 90 RVA: 0x000035AA File Offset: 0x000017AA
	// (set) Token: 0x0600005B RID: 91 RVA: 0x000035BC File Offset: 0x000017BC
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

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x0600005C RID: 92 RVA: 0x000035CF File Offset: 0x000017CF
	// (set) Token: 0x0600005D RID: 93 RVA: 0x000035E6 File Offset: 0x000017E6
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

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600005E RID: 94 RVA: 0x000035FE File Offset: 0x000017FE
	public Transform Transform
	{
		get
		{
			return base.transform;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600005F RID: 95 RVA: 0x00003606 File Offset: 0x00001806
	public bool IsOnGround
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement.IsOnGround;
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003618 File Offset: 0x00001818
	public void PlaceOnGround()
	{
		this.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
	}

	// Token: 0x04000079 RID: 121
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x0400007A RID: 122
	public BabySeinController Controller;

	// Token: 0x0400007B RID: 123
	public CharacterAnimationSystem Animation;

	// Token: 0x0400007C RID: 124
	public BabySeinSounds Sounds;
}
