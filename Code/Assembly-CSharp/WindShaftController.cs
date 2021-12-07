using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009CD RID: 2509
public class WindShaftController : MonoBehaviour
{
	// Token: 0x060036B8 RID: 14008 RVA: 0x000E5D62 File Offset: 0x000E3F62
	public void Awake()
	{
		WindShaftController.Instance = this;
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x000E5D6A File Offset: 0x000E3F6A
	public void OnDestroy()
	{
		WindShaftController.Instance = null;
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x000E5D74 File Offset: 0x000E3F74
	public WindSegment FindPrevious(WindSegment windSegment)
	{
		int num = this.Segments.IndexOf(windSegment);
		num--;
		if (num >= 0)
		{
			return this.Segments[num];
		}
		return null;
	}

	// Token: 0x040031A1 RID: 12705
	public static WindShaftController Instance;

	// Token: 0x040031A2 RID: 12706
	public List<WindSegment> Segments;
}
