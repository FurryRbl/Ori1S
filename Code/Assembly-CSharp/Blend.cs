using System;
using UnityEngine;

// Token: 0x020003CB RID: 971
public class Blend<T>
{
	// Token: 0x06001AD1 RID: 6865 RVA: 0x000734D8 File Offset: 0x000716D8
	public Blend(Func<float, float> ease, Func<T, T, float, T> lerp)
	{
		this.m_ease = ease;
		this.m_lerp = lerp;
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x000734F0 File Offset: 0x000716F0
	public T Current
	{
		get
		{
			float arg = this.m_ease(Mathf.Clamp01(this.Time));
			return this.m_lerp(this.Start, this.End, arg);
		}
	}

	// Token: 0x04001749 RID: 5961
	public T End;

	// Token: 0x0400174A RID: 5962
	public T Start;

	// Token: 0x0400174B RID: 5963
	public float Time;

	// Token: 0x0400174C RID: 5964
	private Func<float, float> m_ease;

	// Token: 0x0400174D RID: 5965
	private Func<T, T, float, T> m_lerp;
}
