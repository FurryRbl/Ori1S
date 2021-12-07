using System;
using Game;
using UnityEngine;

// Token: 0x0200099A RID: 2458
[ExecuteInEditMode]
public class SeinPositionAndDirectionTracker : MonoBehaviour
{
	// Token: 0x17000865 RID: 2149
	// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000E0F3D File Offset: 0x000DF13D
	public Vector3 Speed
	{
		get
		{
			return Characters.Sein.PlatformBehaviour.PlatformMovement.WorldSpeed;
		}
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x000E0F58 File Offset: 0x000DF158
	private void FixedUpdate()
	{
		Vector3 a = Vector3.ClampMagnitude(this.Speed * 0.03f, this.SpeedLimit);
		this.m_smoothSpeed = Vector3.Lerp(a, this.m_smoothSpeed, Mathf.Pow(0.5f, Time.deltaTime / this.SpeedSmoothingFactor));
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x000E0FAC File Offset: 0x000DF1AC
	private void Update()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		float z = Utility.Normalize(this.m_smoothSpeed.x);
		Shader.SetGlobalVector("_SeinPositionDirectionAndSpeed", new Vector4(Characters.Sein.Position.x, Characters.Sein.Position.y, z, this.m_smoothSpeed.magnitude));
		Shader.SetGlobalVector("_SeinAffectSettings", new Vector4(this.SeinColorHighlightFactor, 0f, 0f, 0f));
	}

	// Token: 0x0400302E RID: 12334
	public float SpeedSmoothingFactor = 1f;

	// Token: 0x0400302F RID: 12335
	public float SpeedLimit = 4f;

	// Token: 0x04003030 RID: 12336
	public float SeinColorHighlightFactor = 3f;

	// Token: 0x04003031 RID: 12337
	private Vector3 m_smoothSpeed;

	// Token: 0x04003032 RID: 12338
	private float m_currentSeinSpeed;

	// Token: 0x04003033 RID: 12339
	private float m_currentDirection;
}
