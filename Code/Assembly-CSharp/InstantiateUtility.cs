using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class InstantiateUtility
{
	// Token: 0x06000243 RID: 579 RVA: 0x00009C9C File Offset: 0x00007E9C
	public static UnityEngine.Object Instantiate(UnityEngine.Object original)
	{
		if (UberPoolManager.Instance && original is GameObject)
		{
			GameObject o = (GameObject)original;
			GameObject gameObject = UberPoolManager.Instance.Spawn(o);
			if (gameObject != null)
			{
				return gameObject;
			}
			InstantiateUtility.UpdateInstanceCount(original);
		}
		return UnityEngine.Object.Instantiate(original);
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00009CF4 File Offset: 0x00007EF4
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	private static void Process(GameObject go)
	{
		if (InstantiateUtility.DisableParticles)
		{
			ParticleSystem[] componentsInChildren = go.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in componentsInChildren)
			{
				particleSystem.gameObject.SetActive(false);
			}
			ParticleEmitter[] componentsInChildren2 = go.GetComponentsInChildren<ParticleEmitter>();
			foreach (ParticleEmitter particleEmitter in componentsInChildren2)
			{
				particleEmitter.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x00009D74 File Offset: 0x00007F74
	public static void UpdateInstanceCount(UnityEngine.Object original)
	{
		if (InstantiateUtility.ProfileInstantiate)
		{
			InstantiateUtility.s_info.Add(new InstantiateUtility.InstanceInfo
			{
				Name = original.name,
				Time = Time.time
			});
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00009DB8 File Offset: 0x00007FB8
	public static UnityEngine.Object Instantiate(UnityEngine.Object original, Vector3 position, Quaternion rotation)
	{
		if (UberPoolManager.Instance)
		{
			GameObject gameObject = UberPoolManager.Instance.Spawn((GameObject)original, position, rotation);
			if (gameObject != null)
			{
				return gameObject;
			}
			InstantiateUtility.UpdateInstanceCount(original);
		}
		return UnityEngine.Object.Instantiate(original, position, rotation);
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00009E08 File Offset: 0x00008008
	public static void DumpInstanceCount()
	{
		if (InstantiateUtility.ProfileInstantiate)
		{
			string path = Path.Combine(OutputFolder.BuildOutputPath, "instantiation.csv");
			using (StreamWriter streamWriter = new StreamWriter(new FileStream(path, FileMode.Create)))
			{
				streamWriter.WriteLine("name;info");
				foreach (InstantiateUtility.InstanceInfo instanceInfo in InstantiateUtility.s_info)
				{
					streamWriter.WriteLine(instanceInfo.Name + ";" + instanceInfo.Time);
				}
			}
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00009ED0 File Offset: 0x000080D0
	public static bool IsDestroyed(Component comp)
	{
		return comp == null || UberPoolManager.Instance.IsDestroyed(comp);
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00009EEB File Offset: 0x000080EB
	public static bool IsDestroyed(GameObject gameObject)
	{
		return gameObject == null || UberPoolManager.Instance.IsDestroyed(gameObject);
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00009F06 File Offset: 0x00008106
	public static void Destroy(GameObject gameObject)
	{
		if (gameObject == null)
		{
			return;
		}
		if (!UberPoolManager.Instance.Destroy(gameObject))
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00009F2C File Offset: 0x0000812C
	public static void Destroy(GameObject gameObject, float time)
	{
		if (gameObject == null)
		{
			return;
		}
		if (UberPoolManager.Instance)
		{
			UberPoolManager.Instance.RunDestroyDelayed(time, delegate
			{
				InstantiateUtility.Destroy(gameObject);
			});
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00009F7E File Offset: 0x0000817E
	public static void AddOnDestroy(GameObject get, Action set)
	{
		if (UberPoolManager.Instance)
		{
			UberPoolManager.Instance.AddOnDestroyed(get, set);
		}
	}

	// Token: 0x040001D5 RID: 469
	public static bool ProfileInstantiate;

	// Token: 0x040001D6 RID: 470
	private static List<InstantiateUtility.InstanceInfo> s_info = new List<InstantiateUtility.InstanceInfo>();

	// Token: 0x040001D7 RID: 471
	public static bool DisableParticles;

	// Token: 0x020006E5 RID: 1765
	private struct InstanceInfo
	{
		// Token: 0x0400259B RID: 9627
		public string Name;

		// Token: 0x0400259C RID: 9628
		public float Time;
	}
}
