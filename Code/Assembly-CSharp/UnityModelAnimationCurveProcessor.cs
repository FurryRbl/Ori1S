using System;
using UnityEngine;

// Token: 0x02000397 RID: 919
public abstract class UnityModelAnimationCurveProcessor
{
	// Token: 0x060019DC RID: 6620 RVA: 0x0006EF78 File Offset: 0x0006D178
	public void ProcessCurves(GameObject model, float step, float start, float end)
	{
		this.GameObject = UnityEngine.Object.Instantiate<GameObject>(model);
		Animation component = this.GameObject.GetComponent<Animation>();
		this.OnPreProcessModel();
		component.Play();
		int num = Mathf.RoundToInt(start / step);
		int num2 = Mathf.RoundToInt(end / step);
		for (int i = num; i <= num2; i++)
		{
			foreach (object obj in component)
			{
				AnimationState animationState = (AnimationState)obj;
				animationState.time = (float)i * step;
			}
			component.Sample();
			this.OnSampleFrame(i);
		}
		this.OnPostProcessModel();
		UnityEngine.Object.DestroyImmediate(this.GameObject);
	}

	// Token: 0x060019DD RID: 6621
	public abstract void OnPreProcessModel();

	// Token: 0x060019DE RID: 6622
	public abstract void OnPostProcessModel();

	// Token: 0x060019DF RID: 6623
	public abstract void OnSampleFrame(int frame);

	// Token: 0x04001636 RID: 5686
	protected GameObject GameObject;
}
