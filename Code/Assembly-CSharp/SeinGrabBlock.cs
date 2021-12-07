using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public class SeinGrabBlock : CharacterState, ISeinReceiver
{
	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0007704F File Offset: 0x0007524F
	// (set) Token: 0x06001BAA RID: 7082 RVA: 0x00077057 File Offset: 0x00075257
	public bool InRange { get; private set; }

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00077060 File Offset: 0x00075260
	// (set) Token: 0x06001BAC RID: 7084 RVA: 0x00077068 File Offset: 0x00075268
	public bool IsGrabbing { get; private set; }

	// Token: 0x17000490 RID: 1168
	// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00077071 File Offset: 0x00075271
	public CharacterSpriteMirror Mirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x17000491 RID: 1169
	// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00077088 File Offset: 0x00075288
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x17000492 RID: 1170
	// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0007709A File Offset: 0x0007529A
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000493 RID: 1171
	// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000770AC File Offset: 0x000752AC
	public float HorizontalInput
	{
		get
		{
			return this.CharacterLeftRightMovement.HorizontalInput;
		}
	}

	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x000770B9 File Offset: 0x000752B9
	public bool FaceLeft
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft;
		}
	}

	// Token: 0x17000495 RID: 1173
	// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x000770D5 File Offset: 0x000752D5
	public bool IsPushing
	{
		get
		{
			return this.CurrentState == SeinGrabBlock.State.Push;
		}
	}

	// Token: 0x17000496 RID: 1174
	// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x000770E0 File Offset: 0x000752E0
	public bool IsPulling
	{
		get
		{
			return this.CurrentState == SeinGrabBlock.State.Pull;
		}
	}

	// Token: 0x17000497 RID: 1175
	// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x000770EB File Offset: 0x000752EB
	public bool CanPushCurrentBlock
	{
		get
		{
			return this.m_pushable as Component != null;
		}
	}

	// Token: 0x17000498 RID: 1176
	// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00077106 File Offset: 0x00075306
	public float GrabDistance
	{
		get
		{
			return 0.3f;
		}
	}

	// Token: 0x17000499 RID: 1177
	// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0007710D File Offset: 0x0007530D
	public float ReachDistance
	{
		get
		{
			return 0.9f;
		}
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x00077114 File Offset: 0x00075314
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.GrabBlock = this;
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x0007712E File Offset: 0x0007532E
	public void OnRestoreCheckpoint()
	{
		if (this.IsGrabbing)
		{
			this.ReleaseBlock();
		}
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00077141 File Offset: 0x00075341
	public new void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x0007715F File Offset: 0x0007535F
	public void Start()
	{
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x00077178 File Offset: 0x00075378
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x000771B8 File Offset: 0x000753B8
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.IsGrabbing)
		{
			switch (this.CurrentState)
			{
			case SeinGrabBlock.State.Push:
				if (!this.CanPushCurrentBlock)
				{
					settings.Ground.MaxSpeed = 0f;
					settings.Air.MaxSpeed = 0f;
				}
				settings.Ground.ApplySpeedMultiplier(this.Push);
				if (this.m_pushable != null)
				{
					settings.Ground.MaxSpeed *= this.m_pushable.PushableSpeedRatio();
				}
				break;
			case SeinGrabBlock.State.Pull:
				if (!this.CanPushCurrentBlock)
				{
					settings.Ground.MaxSpeed = 0f;
					settings.Air.MaxSpeed = 0f;
				}
				settings.Ground.ApplySpeedMultiplier(this.Pull);
				if (this.m_pushable != null)
				{
					settings.Ground.MaxSpeed *= this.m_pushable.PushableSpeedRatio();
				}
				break;
			case SeinGrabBlock.State.Idle:
				settings.Ground.MaxSpeed = 0f;
				settings.Air.MaxSpeed = 0f;
				break;
			}
		}
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x000772E8 File Offset: 0x000754E8
	public void HandleMovingBlock()
	{
		float x = this.PlatformMovement.transform.localScale.x;
		this.m_pushable.OnPushOrPull(this.PlatformMovement);
		float num = (float)((!this.FaceLeft) ? 1 : -1) * (this.m_distanceToBlock - this.GrabDistance * x) / Time.deltaTime;
		this.PlatformMovement.LocalSpeedX += num;
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x0007735C File Offset: 0x0007555C
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		if (this.InRange)
		{
			if (this.IsGrabbing)
			{
				if (!this.StillCloseToPushable() || !this.PlatformMovement.IsOnGround)
				{
					this.ReleaseBlock();
				}
				else
				{
					this.HandleMovingBlock();
					switch (this.CurrentState)
					{
					case SeinGrabBlock.State.Push:
						this.UpdateGrabPushState();
						break;
					case SeinGrabBlock.State.Pull:
						this.UpdateGrabPullState();
						break;
					case SeinGrabBlock.State.Idle:
						this.UpdateGrabIdleState();
						break;
					}
				}
			}
			else
			{
				if (!this.StillCloseToPushable())
				{
					this.ReleaseBlock();
					this.ExitRange();
				}
				if (this.PlatformMovement.IsOnGround && this.m_pushable as Component != null && Core.Input.Glide.OnPressed && !Core.Input.Glide.Used)
				{
					Core.Input.Glide.Used = true;
					this.GrabBlock();
				}
			}
		}
		else
		{
			if (this.IsGrabbing)
			{
				this.ReleaseBlock();
			}
			IPushable pushable = this.FindPushableNearby();
			if (pushable != null)
			{
				this.m_pushable = pushable;
				this.EnterRange();
			}
		}
		this.m_currentTime += Time.deltaTime;
		this.UpdateSounds();
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x000774B4 File Offset: 0x000756B4
	private void ExitRange()
	{
		this.m_pushable.OnDehighlight();
		this.CurrentState = SeinGrabBlock.State.Idle;
		this.InRange = false;
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x000774D0 File Offset: 0x000756D0
	private void EnterRange()
	{
		this.m_pushable.OnHighlight();
		this.InRange = true;
		if (this.Hints)
		{
			this.Hints.OnEnterRange(this.m_pushable.CanBeBashed() && Characters.Sein.PlayerAbilities.Bash.HasAbility);
		}
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x00077534 File Offset: 0x00075734
	private void UpdateGrabIdleState()
	{
		if (this.WantsToPull())
		{
			this.EnterGrabPullState();
		}
		else if (this.WantsToPush())
		{
			this.EnterGrabPushState();
		}
		else if (this.ShouldLetGo())
		{
			this.ReleaseBlock();
		}
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x00077580 File Offset: 0x00075780
	private void UpdateGrabPushState()
	{
		if (this.WantsToPull())
		{
			this.EnterGrabPullState();
		}
		else if (this.ShouldLetGo())
		{
			this.ReleaseBlock();
		}
		else if (!this.WantsToPush())
		{
			this.EnterGrabIdleState();
		}
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x000775CC File Offset: 0x000757CC
	private void UpdateGrabPullState()
	{
		if (this.WantsToPush())
		{
			this.EnterGrabPushState();
		}
		else if (this.ShouldLetGo())
		{
			this.ReleaseBlock();
		}
		else if (!this.WantsToPull())
		{
			this.EnterGrabIdleState();
		}
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x00077618 File Offset: 0x00075818
	private void ReleaseBlock()
	{
		if (this.IsGrabbing)
		{
			if (this.m_pushable as Component != null)
			{
				this.m_pushable.OnReleased(this.PlatformMovement);
			}
			this.CurrentState = SeinGrabBlock.State.Idle;
			this.IsGrabbing = false;
			this.SpriteMirrorLock = false;
		}
	}

	// Token: 0x1700049A RID: 1178
	// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0007766C File Offset: 0x0007586C
	// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x00077674 File Offset: 0x00075874
	public bool SpriteMirrorLock
	{
		get
		{
			return this.m_spriteMirrorLock;
		}
		set
		{
			if (this.m_spriteMirrorLock != value)
			{
				this.m_spriteMirrorLock = value;
				if (value)
				{
					this.Sein.PlatformBehaviour.Visuals.SpriteMirror.Lock++;
				}
				else
				{
					this.Sein.PlatformBehaviour.Visuals.SpriteMirror.Lock--;
				}
			}
		}
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x000776E4 File Offset: 0x000758E4
	private void GrabBlock()
	{
		if (!this.IsGrabbing)
		{
			this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedX = 0f;
			this.m_pushable.OnGrabbed(this.PlatformMovement);
			this.SpriteMirrorLock = true;
			this.IsGrabbing = true;
			this.EnterGrabIdleState();
		}
		if (this.Hints)
		{
			this.Hints.OnGrabBlock();
		}
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x00077756 File Offset: 0x00075956
	private IPushable FindPushableNearby()
	{
		return this.DetectPushPullBlock();
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x0007775E File Offset: 0x0007595E
	private bool ShouldGrabBlockIdleAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.CurrentState == SeinGrabBlock.State.Idle;
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x00077777 File Offset: 0x00075977
	private bool ShouldGrabBlockCantPullAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.CurrentState == SeinGrabBlock.State.Pull && !this.CanPushCurrentBlock;
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x0007779C File Offset: 0x0007599C
	private bool ShouldGrabBlockCantPushAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.CurrentState == SeinGrabBlock.State.Push && !this.CanPushCurrentBlock;
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x000777CC File Offset: 0x000759CC
	private bool ShouldGrabBlockPullAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.CurrentState == SeinGrabBlock.State.Pull && this.CanPushCurrentBlock;
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x000777F9 File Offset: 0x000759F9
	private bool ShouldGrabBlockPushAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.CurrentState == SeinGrabBlock.State.Push && this.CanPushCurrentBlock;
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x0007781A File Offset: 0x00075A1A
	private void EnterGrabIdleState()
	{
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.IdleAnimation, 120, new Func<bool>(this.ShouldGrabBlockIdleAnimationKeepPlaying), false);
		this.CurrentState = SeinGrabBlock.State.Idle;
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x00077854 File Offset: 0x00075A54
	private void EnterGrabPushState()
	{
		if (this.CanPushCurrentBlock)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.PushAnimation, 120, new Func<bool>(this.ShouldGrabBlockPushAnimationKeepPlaying), false);
		}
		else
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.CantPushAnimation, 120, new Func<bool>(this.ShouldGrabBlockCantPushAnimationKeepPlaying), false);
		}
		this.CurrentState = SeinGrabBlock.State.Push;
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000778D8 File Offset: 0x00075AD8
	private void EnterGrabPullState()
	{
		if (this.CanPushCurrentBlock)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.PullAnimation, 120, new Func<bool>(this.ShouldGrabBlockPullAnimationKeepPlaying), false);
		}
		else
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.CantPullAnimation, 120, new Func<bool>(this.ShouldGrabBlockCantPullAnimationKeepPlaying), false);
		}
		this.CurrentState = SeinGrabBlock.State.Pull;
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x0007795C File Offset: 0x00075B5C
	private bool StillCloseToPushable()
	{
		return this.DetectPushPullBlock() == this.m_pushable;
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x0007796C File Offset: 0x00075B6C
	private IPushable DetectPushPullBlock()
	{
		this.m_distanceToBlock = 0f;
		float x = this.PlatformMovement.transform.localScale.x;
		Ray ray = new Ray(this.PlatformMovement.HeadPosition, this.PlatformMovement.LocalToWorld((!this.FaceLeft) ? Vector2.right : (-Vector2.right)));
		IPushable result = null;
		bool flag = false;
		for (int i = 0; i < PushPullBlock.All.Count; i++)
		{
			PushPullBlock pushPullBlock = PushPullBlock.All[i];
			if (Vector3.Distance(pushPullBlock.Position, ray.origin) < 10f)
			{
				flag = true;
			}
		}
		RaycastHit raycastHit;
		if (flag && Physics.Raycast(ray, out raycastHit, x * this.ReachDistance) && raycastHit.collider.attachedRigidbody && Vector3.Dot((!this.FaceLeft) ? Vector3.left : Vector3.right, raycastHit.normal) > Mathf.Cos(0.7853982f))
		{
			PushPullBlock component = raycastHit.collider.attachedRigidbody.GetComponent<PushPullBlock>();
			if (component)
			{
				result = component;
				this.m_distanceToBlock = raycastHit.distance;
			}
		}
		return result;
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x00077AC8 File Offset: 0x00075CC8
	private void UpdateSounds()
	{
		if (this.CurrentState == SeinGrabBlock.State.Push)
		{
			if (this.m_nextPushTime < this.m_currentTime && this.CanPushCurrentBlock)
			{
				if (this.PushBlockSound)
				{
					this.m_lastPushSoundPlayer = Sound.Play(this.PushBlockSound.GetSoundForMaterial(SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider), null), this.PlatformMovement.Position, delegate()
					{
						this.m_lastPushSoundPlayer = null;
					});
				}
				if (this.m_lastPushSoundPlayer)
				{
					this.m_nextPushTime = this.m_currentTime + this.m_lastPushSoundPlayer.Length - 0.02f;
				}
			}
		}
		else if (this.m_lastPushSoundPlayer)
		{
			this.FinishPushSound();
		}
		if (this.CurrentState == SeinGrabBlock.State.Pull)
		{
			if (this.m_nextPullTime < this.m_currentTime && this.CanPushCurrentBlock)
			{
				if (this.PullBlockSound)
				{
					this.m_lastPullSoundPlayer = Sound.Play(this.PullBlockSound.GetSoundForMaterial(SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider), null), this.PlatformMovement.Position, delegate()
					{
						this.m_lastPullSoundPlayer = null;
					});
				}
				if (this.m_lastPullSoundPlayer)
				{
					this.m_nextPullTime = this.m_currentTime + this.m_lastPullSoundPlayer.Length - 0.02f;
				}
			}
		}
		else if (this.m_lastPullSoundPlayer)
		{
			this.FinishPullSound();
		}
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x00077C68 File Offset: 0x00075E68
	private void FinishPullSound()
	{
		if (!this.m_lastPullSoundPlayer)
		{
			return;
		}
		this.m_nextPullTime = 0f;
		this.m_lastPullSoundPlayer.FadeOut(0.2f, true);
		UberPoolManager.Instance.RemoveOnDestroyed(this.m_lastPullSoundPlayer.gameObject);
		this.m_lastPullSoundPlayer = null;
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x00077CC0 File Offset: 0x00075EC0
	private void FinishPushSound()
	{
		if (!this.m_lastPushSoundPlayer)
		{
			return;
		}
		this.m_nextPushTime = 0f;
		this.m_lastPushSoundPlayer.FadeOut(0.2f, true);
		UberPoolManager.Instance.RemoveOnDestroyed(this.m_lastPushSoundPlayer.gameObject);
		this.m_lastPushSoundPlayer = null;
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x00077D18 File Offset: 0x00075F18
	private bool WantsToPush()
	{
		return (this.FaceLeft && this.HorizontalInput < 0f) || (!this.FaceLeft && this.HorizontalInput > 0f);
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x00077D60 File Offset: 0x00075F60
	private bool WantsToPull()
	{
		if (this.FaceLeft)
		{
			if (this.HorizontalInput > 0f)
			{
				RaycastHit raycastHit;
				return !this.Sein.Controller.RayTest(this.Sein.Position, Vector3.right * this.PullRayDistance, out raycastHit) || Vector3.Dot(Vector3.left, raycastHit.normal) <= Mathf.Cos(0.5235988f);
			}
		}
		else if (this.HorizontalInput < 0f)
		{
			RaycastHit raycastHit2;
			return !this.Sein.Controller.RayTest(this.Sein.Position, this.Sein.PlatformBehaviour.PlatformMovement.GroundBinormal * -this.PullRayDistance, out raycastHit2) || Vector3.Dot(Vector3.right, raycastHit2.normal) <= Mathf.Cos(0.5235988f);
		}
		return false;
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x00077E5B File Offset: 0x0007605B
	private bool WantsToStopPushOrPull()
	{
		return this.HorizontalInput == 0f;
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x00077E6A File Offset: 0x0007606A
	private bool ShouldLetGo()
	{
		return this.m_pushable as Component == null || Core.Input.Glide.Released;
	}

	// Token: 0x0400180D RID: 6157
	public SeinGrabBlock.State CurrentState = SeinGrabBlock.State.Idle;

	// Token: 0x0400180E RID: 6158
	public TextureAnimationWithTransitions CantPullAnimation;

	// Token: 0x0400180F RID: 6159
	public TextureAnimationWithTransitions CantPushAnimation;

	// Token: 0x04001810 RID: 6160
	public TextureAnimationWithTransitions IdleAnimation;

	// Token: 0x04001811 RID: 6161
	public TextureAnimationWithTransitions PullAnimation;

	// Token: 0x04001812 RID: 6162
	public TextureAnimationWithTransitions PushAnimation;

	// Token: 0x04001813 RID: 6163
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet Pull;

	// Token: 0x04001814 RID: 6164
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet Push;

	// Token: 0x04001815 RID: 6165
	public SurfaceToSoundProviderMap PullBlockSound;

	// Token: 0x04001816 RID: 6166
	public SurfaceToSoundProviderMap PushBlockSound;

	// Token: 0x04001817 RID: 6167
	public float FootstepsSoundsPerSecond;

	// Token: 0x04001818 RID: 6168
	public SeinCharacter Sein;

	// Token: 0x04001819 RID: 6169
	private float m_currentTime;

	// Token: 0x0400181A RID: 6170
	private float m_distanceToBlock;

	// Token: 0x0400181B RID: 6171
	private SoundPlayer m_lastPullSoundPlayer;

	// Token: 0x0400181C RID: 6172
	private SoundPlayer m_lastPushSoundPlayer;

	// Token: 0x0400181D RID: 6173
	private float m_nextPullTime;

	// Token: 0x0400181E RID: 6174
	private float m_nextPushTime;

	// Token: 0x0400181F RID: 6175
	private IPushable m_pushable;

	// Token: 0x04001820 RID: 6176
	public PlayerGrabPushPullHintSystem Hints;

	// Token: 0x04001821 RID: 6177
	private bool m_spriteMirrorLock;

	// Token: 0x04001822 RID: 6178
	public float PullRayDistance = 1f;

	// Token: 0x02000447 RID: 1095
	public enum State
	{
		// Token: 0x04001A42 RID: 6722
		Push,
		// Token: 0x04001A43 RID: 6723
		Pull,
		// Token: 0x04001A44 RID: 6724
		Idle
	}
}
