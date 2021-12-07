using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009B0 RID: 2480
public class RocksGenerator : MonoBehaviour, ISuspendable
{
	// Token: 0x06003615 RID: 13845 RVA: 0x000E2CBA File Offset: 0x000E0EBA
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_nextGenerateTime = this.InitialTimeOffset;
	}

	// Token: 0x06003616 RID: 13846 RVA: 0x000E2CCE File Offset: 0x000E0ECE
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003617 RID: 13847 RVA: 0x000E2CD8 File Offset: 0x000E0ED8
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_time += Time.fixedDeltaTime;
		if (this.m_time >= this.m_nextGenerateTime)
		{
			this.m_distanceToPlayer = Vector3.Distance(base.transform.position, Characters.Current.Position);
			if (this.m_distanceToPlayer < this.MinDistanceToPlayer)
			{
				this.m_nextGenerateTime = this.m_time + 2f;
				return;
			}
			GameObject original = this.ObjectToGenerate;
			if (this.ObjectsToGenerate.Count != 0)
			{
				original = FixedRandom.GetRandomListItem<GameObject>(this.ObjectsToGenerate, FixedRandom.IndexFromPosition(base.transform.position));
			}
			GameObject gameObject = InstantiateUtility.Instantiate(original, base.transform.position + Vector3.Lerp(this.LocalPosition.Min, this.LocalPosition.Max, FixedRandom.Values[4]), base.transform.rotation) as GameObject;
			if (this.OnSpawnSound)
			{
				Sound.Play(this.OnSpawnSound.GetSound(null), base.transform.position, null);
			}
			gameObject.transform.localScale *= Mathf.Lerp(0.7f, 1.5f, FixedRandom.Values[0]);
			if (gameObject.GetComponent<Rigidbody>())
			{
				gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(this.Velocity.Min, this.Velocity.Max, FixedRandom.Values[3]);
				gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.Lerp(this.InitialRotation.Min, this.InitialRotation.Max, FixedRandom.Values[2]);
			}
			this.UpdateMaterial(gameObject);
			this.m_nextGenerateTime = this.m_time + 1f / Mathf.Lerp(this.GenerateFrequence.Min, this.GenerateFrequence.Max, FixedRandom.Values[1]);
		}
	}

	// Token: 0x06003618 RID: 13848 RVA: 0x000E2ED4 File Offset: 0x000E10D4
	private void UpdateMaterial(GameObject generateObject)
	{
		Renderer component = generateObject.GetComponent<Renderer>();
		if (component)
		{
			component.sharedMaterial = component.material;
		}
		foreach (object obj in generateObject.transform)
		{
			Transform transform = (Transform)obj;
			this.UpdateMaterial(transform.gameObject);
		}
	}

	// Token: 0x06003619 RID: 13849 RVA: 0x000E2F5C File Offset: 0x000E115C
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawTextFilled(base.transform, "Rock Generator", false);
	}

	// Token: 0x17000871 RID: 2161
	// (get) Token: 0x0600361A RID: 13850 RVA: 0x000E2F6F File Offset: 0x000E116F
	// (set) Token: 0x0600361B RID: 13851 RVA: 0x000E2F77 File Offset: 0x000E1177
	public bool IsSuspended { get; set; }

	// Token: 0x0400309D RID: 12445
	public GameObject ObjectToGenerate;

	// Token: 0x0400309E RID: 12446
	public List<GameObject> ObjectsToGenerate = new List<GameObject>();

	// Token: 0x0400309F RID: 12447
	public RocksGenerator.FloatMinMax GenerateFrequence;

	// Token: 0x040030A0 RID: 12448
	public RocksGenerator.Vector3MinMax Scale;

	// Token: 0x040030A1 RID: 12449
	public RocksGenerator.Vector3MinMax Velocity;

	// Token: 0x040030A2 RID: 12450
	public RocksGenerator.Vector3MinMax InitialRotation;

	// Token: 0x040030A3 RID: 12451
	public RocksGenerator.Vector3MinMax LocalPosition;

	// Token: 0x040030A4 RID: 12452
	public float InitialTimeOffset;

	// Token: 0x040030A5 RID: 12453
	public float MinDistanceToPlayer = 5f;

	// Token: 0x040030A6 RID: 12454
	public SoundProvider OnSpawnSound;

	// Token: 0x040030A7 RID: 12455
	private float m_nextGenerateTime;

	// Token: 0x040030A8 RID: 12456
	private float m_distanceToPlayer;

	// Token: 0x040030A9 RID: 12457
	private float m_time;

	// Token: 0x020009B1 RID: 2481
	[Serializable]
	public class FloatMinMax
	{
		// Token: 0x040030AB RID: 12459
		public float Min;

		// Token: 0x040030AC RID: 12460
		public float Max;
	}

	// Token: 0x020009B2 RID: 2482
	[Serializable]
	public class Vector3MinMax
	{
		// Token: 0x040030AD RID: 12461
		public Vector3 Min;

		// Token: 0x040030AE RID: 12462
		public Vector3 Max;
	}
}
