using System;
using UnityEngine;

// Token: 0x02000859 RID: 2137
[ExecuteInEditMode]
[AddComponentMenu("Uber Water/Random Actor")]
public class UberWaterRandomActor : MonoBehaviour
{
	// Token: 0x0600306C RID: 12396 RVA: 0x000CD507 File Offset: 0x000CB707
	private void OnEnable()
	{
		this.m_lastTime = Time.realtimeSinceStartup;
	}

	// Token: 0x0600306D RID: 12397 RVA: 0x000CD514 File Offset: 0x000CB714
	private void Update()
	{
		this.m_deltaTime = Mathf.Clamp((Time.realtimeSinceStartup - this.m_lastTime) * Time.timeScale, 0f, 0.05f);
		this.m_lastTime = Time.realtimeSinceStartup;
		Vector3 a = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z) - base.transform.rotation * new Vector3(this.Size.width / 2f, 0f, this.Size.height / 2f);
		this.m_num += (float)this.AverageImpactsPerSecond * this.m_deltaTime * UnityEngine.Random.Range(0.8f, 1.2f) * 0.5f;
		int num = Mathf.FloorToInt(this.m_num);
		this.m_num -= (float)num;
		if (this.MinStrength <= 0.001f && this.MaxStrength <= 0.001f)
		{
			return;
		}
		for (int i = 0; i < num; i++)
		{
			float power = UnityEngine.Random.Range(this.MinStrength, this.MaxStrength) * 2f;
			Vector3 pos = a + base.transform.rotation * new Vector3(UnityEngine.Random.Range(this.Size.xMin, this.Size.xMax), 0f, UnityEngine.Random.Range(this.Size.yMin, this.Size.yMax));
			for (int j = 0; j < UberWaterControl.All.Count; j++)
			{
				UberWaterControl uberWaterControl = UberWaterControl.All[j];
				if (uberWaterControl.IsOverWater(pos))
				{
					uberWaterControl.Impact(pos, power, UnityEngine.Random.Range(this.MinRadius, this.MaxRadius), true, 1);
				}
			}
		}
	}

	// Token: 0x04002BBD RID: 11197
	[Range(0f, 80f)]
	public int AverageImpactsPerSecond = 30;

	// Token: 0x04002BBE RID: 11198
	public float MinStrength = 1f;

	// Token: 0x04002BBF RID: 11199
	public float MaxStrength = 5f;

	// Token: 0x04002BC0 RID: 11200
	public Rect Size;

	// Token: 0x04002BC1 RID: 11201
	public float MinRadius = 0.05f;

	// Token: 0x04002BC2 RID: 11202
	public float MaxRadius = 0.2f;

	// Token: 0x04002BC3 RID: 11203
	private float m_deltaTime;

	// Token: 0x04002BC4 RID: 11204
	private float m_lastTime;

	// Token: 0x04002BC5 RID: 11205
	private float m_num;
}
