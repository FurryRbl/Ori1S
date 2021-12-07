using System;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class RandomObjectGenerator : MonoBehaviour
{
	// Token: 0x0600118A RID: 4490 RVA: 0x00050B14 File Offset: 0x0004ED14
	private void Start()
	{
		this.m_lastSpawnTime = Time.time;
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x00050B24 File Offset: 0x0004ED24
	private void FixedUpdate()
	{
		Vector2 vector = new Vector2(base.GetComponent<Renderer>().bounds.size.x, base.GetComponent<Renderer>().bounds.size.y);
		float magnitude = vector.magnitude;
		float num = this.ObjectGenerationFrequency * magnitude;
		int num2 = (int)((Time.time - this.m_lastSpawnTime) * num);
		for (int i = 0; i < num2; i++)
		{
			Vector3 position = base.GetComponent<Renderer>().bounds.min + Vector3.right * UnityEngine.Random.value * base.GetComponent<Renderer>().bounds.size.x + Vector3.up * UnityEngine.Random.value * base.GetComponent<Renderer>().bounds.size.y;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.ObjectPrefab, position, Quaternion.identity);
			gameObject.transform.localScale = Vector3.one * Mathf.Lerp(this.MinSize, this.MaxSize, UnityEngine.Random.value);
			if (gameObject.GetComponent<Rigidbody>())
			{
				gameObject.GetComponent<Rigidbody>().velocity = this.InitialVelocity;
			}
			gameObject.transform.SetParentMaintainingLocalTransform(this.GetGroupParent().transform);
		}
		if (num2 > 0)
		{
			this.m_lastSpawnTime = Time.time;
		}
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x00050CB8 File Offset: 0x0004EEB8
	private GameObject GetGroupParent()
	{
		if (this.m_groupParent == null)
		{
			foreach (UnityEngine.Object @object in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
			{
				if (@object.name == this.ObjectPrefab.name)
				{
					this.m_groupParent = (@object as GameObject);
				}
			}
			if (this.m_groupParent == null)
			{
				this.m_groupParent = new GameObject(this.ObjectPrefab.name);
			}
		}
		return this.m_groupParent;
	}

	// Token: 0x04000F22 RID: 3874
	public GameObject ObjectPrefab;

	// Token: 0x04000F23 RID: 3875
	public float ObjectGenerationFrequency = 20f;

	// Token: 0x04000F24 RID: 3876
	public float MaxSize = 2f;

	// Token: 0x04000F25 RID: 3877
	public float MinSize = 0.1f;

	// Token: 0x04000F26 RID: 3878
	public Vector3 InitialVelocity;

	// Token: 0x04000F27 RID: 3879
	private float m_lastSpawnTime;

	// Token: 0x04000F28 RID: 3880
	private GameObject m_groupParent;
}
