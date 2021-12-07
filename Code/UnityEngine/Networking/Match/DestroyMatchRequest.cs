using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000238 RID: 568
	public class DestroyMatchRequest : Request
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x0002B794 File Offset: 0x00029994
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x0002B79C File Offset: 0x0002999C
		public NetworkID networkId { get; set; }

		// Token: 0x060022A1 RID: 8865 RVA: 0x0002B7A8 File Offset: 0x000299A8
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X")
			});
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0002B7E8 File Offset: 0x000299E8
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid;
		}
	}
}
