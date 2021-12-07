using System;
using UnityEngine;

// Token: 0x02000450 RID: 1104
public class GoThroughPlatform : MonoBehaviour
{
	// Token: 0x17000534 RID: 1332
	// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x00087045 File Offset: 0x00085245
	public int Length
	{
		get
		{
			return this.Colliders.Length;
		}
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x00087050 File Offset: 0x00085250
	public void OnValidate()
	{
		this.Colliders = base.GetComponentsInChildren<Collider>();
		this.Transforms = new Transform[this.Colliders.Length];
		for (int i = 0; i < this.Colliders.Length; i++)
		{
			this.Transforms[i] = this.Colliders[i].transform;
		}
		this.LightPlatform = base.GetComponent<LightPlatform>();
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x000870B8 File Offset: 0x000852B8
	public void Awake()
	{
		base.gameObject.layer = GoThroughPlatform.Layer;
		foreach (Collider collider in this.Colliders)
		{
			collider.gameObject.layer = GoThroughPlatform.Layer;
		}
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x00087104 File Offset: 0x00085304
	public void OnEnable()
	{
		GoThroughPlatformManager.Register(this);
	}

	// Token: 0x06001EA8 RID: 7848 RVA: 0x0008710C File Offset: 0x0008530C
	public void OnDisable()
	{
		GoThroughPlatformManager.Unregister(this);
	}

	// Token: 0x04001A73 RID: 6771
	public static int Layer = 19;

	// Token: 0x04001A74 RID: 6772
	[HideInInspector]
	public Collider[] Colliders;

	// Token: 0x04001A75 RID: 6773
	[HideInInspector]
	public Transform[] Transforms;

	// Token: 0x04001A76 RID: 6774
	[HideInInspector]
	public LightPlatform LightPlatform;
}
