using System;
using UnityEngine;

// Token: 0x02000398 RID: 920
[ExecuteInEditMode]
public class AnimationMetaDataDrivenTransform : MonoBehaviour, ISuspendable
{
	// Token: 0x060019E1 RID: 6625 RVA: 0x0006F07B File Offset: 0x0006D27B
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_transform = base.transform;
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x0006F08F File Offset: 0x0006D28F
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x0006F097 File Offset: 0x0006D297
	public void Start()
	{
		this.Initialize();
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x0006F0A0 File Offset: 0x0006D2A0
	public void Initialize()
	{
		if (this.m_isInitialized)
		{
			return;
		}
		this.m_isInitialized = true;
		this.m_originalLocalPosition = base.transform.localPosition;
		this.UpdateCurrentTextureAnimation();
		this.UpdateDataFromAnimator();
		if (this.m_targetTransform)
		{
			this.m_originalScale = this.m_targetTransform.localScale;
		}
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x0006F100 File Offset: 0x0006D300
	public void UpdateCurrentTextureAnimation()
	{
		if (this.Animator)
		{
			this.m_textureAnimation = this.Animator.CurrentAnimation;
			this.m_textureAnimator = this.Animator.TextureAnimator;
			this.m_targetTransform = this.Animator.transform;
		}
		if (this.SpriteAnimatorWithTransitions)
		{
			this.m_textureAnimation = this.SpriteAnimatorWithTransitions.CurrentAnimation;
			this.m_textureAnimator = this.SpriteAnimatorWithTransitions.TextureAnimator;
			this.m_targetTransform = this.SpriteAnimatorWithTransitions.transform;
		}
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x0006F194 File Offset: 0x0006D394
	private void UpdateDataFromAnimator()
	{
		if (this.m_textureAnimation.AnimationMetaData == null)
		{
			return;
		}
		if (this.ShouldFollowCameraPlane)
		{
			this.m_data = this.m_textureAnimation.AnimationMetaData.CameraData;
		}
		else
		{
			for (int i = 0; i < this.m_textureAnimation.AnimationMetaData.Data.Count; i++)
			{
				if (this.m_textureAnimation.AnimationMetaData.Data[i].Name == this.NodeName)
				{
					this.m_data = this.m_textureAnimation.AnimationMetaData.Data[i];
					break;
				}
			}
		}
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x0006F250 File Offset: 0x0006D450
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.UpdateCurrentTextureAnimation();
		this.Sample();
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x0006F26C File Offset: 0x0006D46C
	public void Sample()
	{
		if (this.m_lastAnimation != this.m_textureAnimation)
		{
			this.UpdateDataFromAnimator();
			this.m_lastAnimation = this.m_textureAnimation;
		}
		int num = (int)this.m_textureAnimator.Frame;
		if (num < 0)
		{
			num = 0;
		}
		if (this.m_data == null || num >= this.m_data.PositionX.Values.Count)
		{
			return;
		}
		if (this.ShouldFollowCameraPlane)
		{
			if (this.UseDelta)
			{
				Vector3 vector = (!this.DontInterpolate) ? this.m_data.GetDeltaPositionAtTime(this.m_textureAnimator.Time) : this.m_data.GetRawDeltaPositionAtTime(this.m_textureAnimator.Time);
				this.m_transform.localPosition += new Vector3(vector.x * this.m_originalScale.x, vector.y * this.m_originalScale.y, 0f);
			}
			else
			{
				Vector2 v = (!this.DontInterpolate) ? (this.m_data.GetPositionAtTime(this.m_textureAnimator.Time) - this.m_data.GetPositionAtTime(0f)) : (this.m_data.GetRawPositionAtTime(this.m_textureAnimator.Time) - this.m_data.GetRawPositionAtTime(0f));
				v.x *= this.m_originalScale.x;
				v.y *= this.m_originalScale.y;
				this.m_transform.localPosition = this.m_originalLocalPosition + v;
			}
			if (this.ShouldUpdateScale)
			{
				this.m_transform.localScale = this.m_originalScale * ((!this.DontInterpolate) ? this.m_data.GetPositionAtTime(this.m_textureAnimator.Time) : this.m_data.GetRawPositionAtTime(this.m_textureAnimator.Time)).z;
			}
		}
		else
		{
			this.m_transform.position = this.m_targetTransform.localToWorldMatrix.MultiplyPoint(this.m_data.GetRawPositionAtFrame(num));
		}
		if (this.ShouldUpdateRotation && num < this.m_data.RotationZ.Values.Count && num >= 0)
		{
			this.m_transform.rotation = this.m_targetTransform.rotation * Quaternion.Euler(0f, 0f, this.m_data.RotationZ.Values[num]);
		}
	}

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0006F542 File Offset: 0x0006D742
	// (set) Token: 0x060019EA RID: 6634 RVA: 0x0006F54A File Offset: 0x0006D74A
	public bool IsSuspended { get; set; }

	// Token: 0x04001637 RID: 5687
	public SpriteAnimator Animator;

	// Token: 0x04001638 RID: 5688
	public SpriteAnimatorWithTransitions SpriteAnimatorWithTransitions;

	// Token: 0x04001639 RID: 5689
	public bool UseDelta = true;

	// Token: 0x0400163A RID: 5690
	public bool ShouldFollowCameraPlane = true;

	// Token: 0x0400163B RID: 5691
	public string NodeName;

	// Token: 0x0400163C RID: 5692
	public bool DontInterpolate;

	// Token: 0x0400163D RID: 5693
	public bool ShouldUpdateScale = true;

	// Token: 0x0400163E RID: 5694
	public bool ShouldUpdateRotation = true;

	// Token: 0x0400163F RID: 5695
	private AnimationMetaData.AnimationData m_data;

	// Token: 0x04001640 RID: 5696
	private TextureAnimation m_textureAnimation;

	// Token: 0x04001641 RID: 5697
	private TextureAnimator m_textureAnimator;

	// Token: 0x04001642 RID: 5698
	private Transform m_targetTransform;

	// Token: 0x04001643 RID: 5699
	private Vector3 m_originalScale;

	// Token: 0x04001644 RID: 5700
	private Vector3 m_originalLocalPosition;

	// Token: 0x04001645 RID: 5701
	private Transform m_transform;

	// Token: 0x04001646 RID: 5702
	private bool m_isInitialized;

	// Token: 0x04001647 RID: 5703
	private TextureAnimation m_lastAnimation;
}
