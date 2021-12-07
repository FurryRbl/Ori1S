using System;
using UnityEngine;

// Token: 0x020005F8 RID: 1528
public class WormHole : SaveSerialize, IDynamicGraphicHierarchy
{
	// Token: 0x06002643 RID: 9795 RVA: 0x000A7D87 File Offset: 0x000A5F87
	public void Start()
	{
		if (this.HiddenAtStart)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x000A7DA0 File Offset: 0x000A5FA0
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			base.gameObject.SetActive(ar.Serialize(true));
		}
		else
		{
			ar.Serialize(base.gameObject.activeSelf);
		}
	}

	// Token: 0x17000619 RID: 1561
	// (get) Token: 0x06002645 RID: 9797 RVA: 0x000A7DE1 File Offset: 0x000A5FE1
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x1700061A RID: 1562
	// (get) Token: 0x06002646 RID: 9798 RVA: 0x000A7DEE File Offset: 0x000A5FEE
	public Quaternion Rotation
	{
		get
		{
			return base.transform.rotation;
		}
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x000A7DFC File Offset: 0x000A5FFC
	public void OnEmerge()
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x040020E0 RID: 8416
	public bool HiddenAtStart;
}
