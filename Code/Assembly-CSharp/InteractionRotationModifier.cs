using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020007FC RID: 2044
[CustomShaderModifier("Interaction Rotation Modifier")]
[ExecuteInEditMode]
[UberShaderOrder(UberShaderOrder.InteractionRotation)]
[UberShaderCategory(UberShaderCategory.Interaction)]
public class InteractionRotationModifier : UberInteractionModifier
{
	// Token: 0x06002F0C RID: 12044 RVA: 0x000C740B File Offset: 0x000C560B
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.Strength *= strength;
	}

	// Token: 0x06002F0D RID: 12045 RVA: 0x000C741B File Offset: 0x000C561B
	public override void Randomize()
	{
		this.Settings.W += UnityEngine.Random.Range(-0.05f, 0.05f);
	}

	// Token: 0x17000793 RID: 1939
	// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000C743E File Offset: 0x000C563E
	public override string InteractionName
	{
		get
		{
			return InteractionRotationModifier.s_name;
		}
	}

	// Token: 0x17000794 RID: 1940
	// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000C7445 File Offset: 0x000C5645
	protected override UberInteractionManager.PropertyIDCache PropertyCache
	{
		get
		{
			return UberInteractionManager.GetCachedPropertyID(ref InteractionRotationModifier.s_cache, InteractionRotationModifier.s_name);
		}
	}

	// Token: 0x06002F10 RID: 12048 RVA: 0x000C7458 File Offset: 0x000C5658
	public override IEnumerable<string> GetKeywordsForShader()
	{
		foreach (string key in base.GetKeywordsForShader())
		{
			yield return key;
		}
		if (base.HasCageMesh)
		{
			yield return "_CustomMesh";
		}
		yield break;
	}

	// Token: 0x06002F11 RID: 12049 RVA: 0x000C747B File Offset: 0x000C567B
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002F12 RID: 12050 RVA: 0x000C7480 File Offset: 0x000C5680
	public static void WarmUpResource()
	{
		if (InteractionRotationModifier.s_largeInteractionProvider == null)
		{
			GameObject gameObject = Resources.Load<GameObject>("bushesSoundProvider");
			InteractionRotationModifier.s_largeInteractionProvider = gameObject.GetComponent<Varying2DSoundProvider>();
		}
	}

	// Token: 0x06002F13 RID: 12051 RVA: 0x000C74B4 File Offset: 0x000C56B4
	protected override void OnPlay(float strength, bool explosion)
	{
		InteractionRotationModifier.WarmUpResource();
		if (explosion)
		{
			return;
		}
		if (base.Bounds.extents.x <= 0.9f || base.Bounds.extents.y <= 1.7f)
		{
			return;
		}
		strength = Mathf.Abs(strength);
		if (strength > 0.12f && Time.realtimeSinceStartup - this.m_lastSoundPlay > 0.4f)
		{
			SoundPlayer soundPlayer = Sound.Play(InteractionRotationModifier.s_largeInteractionProvider.GetSound(null), base.transform.position, null);
			if (soundPlayer)
			{
				soundPlayer.Volume *= Mathf.Clamp(strength * 15f, 0f, 2f);
			}
			this.m_lastSoundPlay = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06002F14 RID: 12052 RVA: 0x000C7590 File Offset: 0x000C5790
	protected override int GetFlip(Vector3 pos, bool explode)
	{
		int num = 1;
		Vector3 eulerAngles = base.transform.eulerAngles;
		if (Mathf.Abs(Mathf.DeltaAngle(0f, eulerAngles.y)) > 90f)
		{
			num *= -1;
		}
		if (Mathf.Abs(Mathf.DeltaAngle(0f, eulerAngles.z)) > 90f)
		{
			num *= -1;
		}
		if (base.transform.InverseTransformPoint(pos).y < this.Pivot.Y)
		{
			num *= -1;
		}
		return num;
	}

	// Token: 0x06002F15 RID: 12053 RVA: 0x000C761C File Offset: 0x000C581C
	public static Vector2 Rotate(Vector2 v, float degrees)
	{
		float num = Mathf.Sin(degrees * 0.017453292f);
		float num2 = Mathf.Cos(degrees * 0.017453292f);
		float x = v.x;
		float y = v.y;
		v.x = num2 * x - num * y;
		v.y = num * x + num2 * y;
		return v;
	}

	// Token: 0x06002F16 RID: 12054 RVA: 0x000C7670 File Offset: 0x000C5870
	protected override float GetStrength(Vector3 velocity, Vector4 strengthVal)
	{
		float z = base.transform.rotation.eulerAngles.z;
		Vector2 vector = InteractionRotationModifier.Rotate(velocity, z);
		float num = Mathf.Pow(velocity.magnitude, UberInteractionManager.Instance.MagnitudePower);
		int num2 = base.RandomSign(-vector.x);
		return (float)num2 * num * this.Strength * strengthVal.x * 0.1f;
	}

	// Token: 0x06002F17 RID: 12055 RVA: 0x000C76E8 File Offset: 0x000C58E8
	public override void SetProperties()
	{
		this.DistortionMask.Set("_InteractionDistortMask", base.AttachedToShaderBlock);
		this.Pivot.Set("_InteractionRotationPivot", base.AttachedToShaderBlock);
		this.Pivot.Mode = UberShaderVector.ScalingMode.PivotOnXy;
		this.DistortionMask.IsVertexTexture = true;
		this.Settings.Set("_InteractionSettingsRotation", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A27 RID: 10791
	private const float c_timeBetweenPlays = 0.4f;

	// Token: 0x04002A28 RID: 10792
	public UberShaderTexture DistortionMask = new UberShaderTexture();

	// Token: 0x04002A29 RID: 10793
	public float Strength = 1f;

	// Token: 0x04002A2A RID: 10794
	[UberShaderVectorDisplay("Waviness x", "Waviness y", "Faloff Size", "Speed")]
	public UberShaderVector Settings = new UberShaderVector(0.1f, 0.1f, 3f, 1f);

	// Token: 0x04002A2B RID: 10795
	[UberShaderVectorDisplay("Pivot", "Pivot mask", ShowAsVector2 = true)]
	public UberShaderVector Pivot = new UberShaderVector(0f, 0f, 1f, 1f);

	// Token: 0x04002A2C RID: 10796
	private static Varying2DSoundProvider s_largeInteractionProvider;

	// Token: 0x04002A2D RID: 10797
	private static string s_name = "Rotation";

	// Token: 0x04002A2E RID: 10798
	private static UberInteractionManager.PropertyIDCache s_cache;

	// Token: 0x04002A2F RID: 10799
	private float m_lastSoundPlay;
}
