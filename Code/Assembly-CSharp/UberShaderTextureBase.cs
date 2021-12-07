using System;
using UnityEngine;

// Token: 0x02000795 RID: 1941
[Serializable]
public abstract class UberShaderTextureBase : UberShaderProperty
{
	// Token: 0x1700073A RID: 1850
	// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000C1294 File Offset: 0x000BF494
	// (set) Token: 0x06002D15 RID: 11541 RVA: 0x000C129C File Offset: 0x000BF49C
	public Vector2 TextureScale
	{
		get
		{
			return this.ProTextureScale;
		}
		set
		{
			this.ProTextureScale = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700073B RID: 1851
	// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000C12AB File Offset: 0x000BF4AB
	// (set) Token: 0x06002D17 RID: 11543 RVA: 0x000C12B3 File Offset: 0x000BF4B3
	public Vector2 TextureOffset
	{
		get
		{
			return this.ProTextureOffset;
		}
		set
		{
			this.ProTextureOffset = value;
			this.BindProperties();
		}
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x000C12C4 File Offset: 0x000BF4C4
	public Vector4 GetTextureShaderSettings()
	{
		float num = this.ProTextureRotation;
		if (this.UvMode == TextureUvMode.WorldRotation)
		{
			num += this.AttachedBlock.transform.rotation.eulerAngles.z;
		}
		return new Vector4(this.ProTextureScroll.x, this.ProTextureScroll.y, num * 0.017453292f, this.ProTextureRotationSpeed * 0.017453292f);
	}

	// Token: 0x1700073C RID: 1852
	// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000C1335 File Offset: 0x000BF535
	// (set) Token: 0x06002D1A RID: 11546 RVA: 0x000C133D File Offset: 0x000BF53D
	public Vector2 TextureScroll
	{
		get
		{
			return this.ProTextureScroll;
		}
		set
		{
			this.ProTextureScroll = value;
			this.BindTexSettings();
		}
	}

	// Token: 0x1700073D RID: 1853
	// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000C134C File Offset: 0x000BF54C
	// (set) Token: 0x06002D1C RID: 11548 RVA: 0x000C1354 File Offset: 0x000BF554
	public float TextureRotation
	{
		get
		{
			return this.ProTextureRotation;
		}
		set
		{
			this.ProTextureRotation = value;
			this.BindTexSettings();
		}
	}

	// Token: 0x1700073E RID: 1854
	// (get) Token: 0x06002D1D RID: 11549 RVA: 0x000C1363 File Offset: 0x000BF563
	// (set) Token: 0x06002D1E RID: 11550 RVA: 0x000C136B File Offset: 0x000BF56B
	public float TextureRotationSpeed
	{
		get
		{
			return this.ProTextureRotationSpeed;
		}
		set
		{
			this.ProTextureRotationSpeed = value;
			this.BindTexSettings();
		}
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x000C137A File Offset: 0x000BF57A
	public void BindTexSettings()
	{
		if (this.AttachedBlock != null)
		{
			base.BindVector(this.BindName + "_US_ST", this.GetTextureShaderSettings());
		}
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x000C13AC File Offset: 0x000BF5AC
	protected void BindOptions()
	{
		if (this.DoParralax)
		{
			base.BindVector(this.BindName + "_Parralax", new Vector4(this.ParralaxAmount, 0f, this.AttachedBlock.transform.position.x, this.AttachedBlock.transform.position.y));
		}
		if (this.IsPolarUvs)
		{
			base.BindVector(this.BindName + "_Polar", this.PolarUvSettings);
		}
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x000C1444 File Offset: 0x000BF644
	protected void BindBase()
	{
		if (!base.BindMaterial.HasProperty(this.BindName))
		{
			return;
		}
		if (base.BindMaterial.GetTextureOffset(this.BindName) != this.TextureOffset)
		{
			base.BindMaterial.SetTextureOffset(this.BindName, this.TextureOffset);
		}
		if (base.BindMaterial.GetTextureScale(this.BindName) != this.TextureScale)
		{
			base.BindMaterial.SetTextureScale(this.BindName, this.TextureScale);
		}
		this.BindTexSettings();
		this.BindOptions();
	}

	// Token: 0x040028BE RID: 10430
	public TextureUvMode UvMode;

	// Token: 0x040028BF RID: 10431
	public bool IsPolarUvs;

	// Token: 0x040028C0 RID: 10432
	public bool DoParralax;

	// Token: 0x040028C1 RID: 10433
	public float ParralaxAmount;

	// Token: 0x040028C2 RID: 10434
	[UberShaderVectorDisplay("U scale", "V scale", "Polar offset", "Polar scroll")]
	public Vector4 PolarUvSettings;

	// Token: 0x040028C3 RID: 10435
	[SerializeField]
	protected Vector2 ProTextureScale = Vector2.one;

	// Token: 0x040028C4 RID: 10436
	[SerializeField]
	protected Vector2 ProTextureScroll;

	// Token: 0x040028C5 RID: 10437
	[SerializeField]
	protected Vector2 ProTextureOffset;

	// Token: 0x040028C6 RID: 10438
	[SerializeField]
	protected float ProTextureRotation;

	// Token: 0x040028C7 RID: 10439
	[SerializeField]
	protected float ProTextureRotationSpeed;
}
