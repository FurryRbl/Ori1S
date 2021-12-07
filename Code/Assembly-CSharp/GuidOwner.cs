using System;
using UnityEngine;

// Token: 0x0200023B RID: 571
public class GuidOwner : MonoBehaviour
{
	// Token: 0x060012EF RID: 4847 RVA: 0x00057D18 File Offset: 0x00055F18
	public MoonGuid GetGuid()
	{
		if (!this.IsGuidInitialized())
		{
			if (Application.isPlaying)
			{
				throw new Exception("GUID generated in runtime " + base.name);
			}
			this.MoonGuid = GuidOwner.GenerateGUID();
		}
		return this.MoonGuid;
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x00057D61 File Offset: 0x00055F61
	public void RegenerateGuid()
	{
		if (Application.isPlaying)
		{
			throw new Exception("GUID generated in runtime " + base.name);
		}
		this.MoonGuid = GuidOwner.GenerateGUID();
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x00057D8E File Offset: 0x00055F8E
	private static MoonGuid GenerateGUID()
	{
		return new MoonGuid(Guid.NewGuid());
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x00057D9A File Offset: 0x00055F9A
	public bool IsGuidInitialized()
	{
		return this.MoonGuid != GuidOwner.UNINITIALIZED_GUID;
	}

	// Token: 0x040010A9 RID: 4265
	public static MoonGuid UNINITIALIZED_GUID = MoonGuid.Empty;

	// Token: 0x040010AA RID: 4266
	[HideInInspector]
	public MoonGuid MoonGuid = new MoonGuid(GuidOwner.UNINITIALIZED_GUID);
}
