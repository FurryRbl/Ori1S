using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class CharacterCapsuleController : SaveSerialize
{
	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00025CDF File Offset: 0x00023EDF
	public bool CanBecomeOriginalCapsule
	{
		get
		{
			return this.CanBecomeCapsule(this.OriginalRadius, this.OriginalHeight, this.OriginalCenter);
		}
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00025CFC File Offset: 0x00023EFC
	public void EnableCollider(bool enable)
	{
		this.CapsuleCollider.enabled = enable;
		if (this.CrushCollider)
		{
			this.CrushCollider.enabled = enable;
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00025D31 File Offset: 0x00023F31
	public void Start()
	{
		this.OriginalCenter = this.CapsuleCollider.center;
		this.OriginalRadius = this.CapsuleCollider.radius;
		this.OriginalHeight = this.CapsuleCollider.height;
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00025D66 File Offset: 0x00023F66
	public void Restore()
	{
		this.CapsuleCollider.center = this.OriginalCenter;
		this.CapsuleCollider.radius = this.OriginalRadius;
		this.CapsuleCollider.height = this.OriginalHeight;
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00025D9C File Offset: 0x00023F9C
	public void ChangeToSphere()
	{
		this.CapsuleCollider.height = this.CapsuleCollider.radius * 2f;
		Vector3 originalCenter = this.OriginalCenter;
		originalCenter.y -= this.OriginalHeight / 2f - this.OriginalRadius;
		this.CapsuleCollider.center = originalCenter;
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00025DFC File Offset: 0x00023FFC
	public void ChangeHeightTop(float height)
	{
		if (height == this.CapsuleCollider.height)
		{
			return;
		}
		float height2 = this.CapsuleCollider.height;
		this.CapsuleCollider.height = height;
		float d = height - height2;
		this.CapsuleCollider.center += Vector3.down * d * 0.5f;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00025E64 File Offset: 0x00024064
	public void ChangeHeightBottom(float height)
	{
		if (height == this.CapsuleCollider.height)
		{
			return;
		}
		float height2 = this.CapsuleCollider.height;
		this.CapsuleCollider.height = height;
		float d = height - height2;
		this.CapsuleCollider.center += Vector3.down * d * 0.5f;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00025ECC File Offset: 0x000240CC
	public bool CanBecomeCapsule(float radius, float height, Vector3 center)
	{
		height -= radius * 2f;
		Vector3 a = this.CapsuleCollider.transform.position + center;
		bool enabled = !this.CrushCollider || this.CrushCollider.enabled;
		bool enabled2 = this.CapsuleCollider.enabled;
		this.CapsuleCollider.enabled = false;
		if (this.CrushCollider)
		{
			this.CrushCollider.enabled = false;
		}
		Vector3 vector = a + Vector3.down * height * 0.5f;
		Vector3 a2 = a + Vector3.up * height * 0.5f;
		Ray ray = new Ray(vector, (a2 - vector).normalized);
		bool flag = Physics.SphereCast(ray, radius - 0.08f, (a2 - vector).magnitude);
		this.CapsuleCollider.enabled = enabled2;
		if (this.CrushCollider)
		{
			this.CrushCollider.enabled = enabled;
		}
		return !flag;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00025FF1 File Offset: 0x000241F1
	public override void Serialize(Archive ar)
	{
		ar.Serialize(this.OriginalHeight);
		ar.Serialize(this.OriginalCenter);
		ar.Serialize(this.OriginalRadius);
	}

	// Token: 0x04000721 RID: 1825
	public CapsuleCollider CapsuleCollider;

	// Token: 0x04000722 RID: 1826
	public CapsuleCollider CrushCollider;

	// Token: 0x04000723 RID: 1827
	public Vector3 OriginalCenter;

	// Token: 0x04000724 RID: 1828
	public float OriginalHeight;

	// Token: 0x04000725 RID: 1829
	public float OriginalRadius;

	// Token: 0x04000726 RID: 1830
	public PlatformBehaviour PlatformBehaviour;
}
