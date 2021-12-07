using System;
using UnityEngine;

// Token: 0x0200084A RID: 2122
public class UberMotionBlurInterestZone : MonoBehaviour
{
	// Token: 0x06003047 RID: 12359 RVA: 0x000CC65C File Offset: 0x000CA85C
	public void DoBind(Material mat, string name)
	{
		Vector3 position = base.transform.position;
		Vector4 vector = Camera.current.WorldToViewportPoint(position);
		vector.z = 0f;
		vector.w = 1f / this.Radius;
		mat.SetVector(name, vector);
	}

	// Token: 0x06003048 RID: 12360 RVA: 0x000CC6AD File Offset: 0x000CA8AD
	private void Update()
	{
		UberPostProcess.Instance.PushInterestZone(this);
	}

	// Token: 0x04002B6F RID: 11119
	public float Radius;

	// Token: 0x04002B70 RID: 11120
	public UberMotionBlurInterestZone.ZoneType Type;

	// Token: 0x0200084B RID: 2123
	public enum ZoneType
	{
		// Token: 0x04002B72 RID: 11122
		MainInterest,
		// Token: 0x04002B73 RID: 11123
		SecondaryInterest,
		// Token: 0x04002B74 RID: 11124
		TertiaryInterest
	}
}
