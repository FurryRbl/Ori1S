using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000239 RID: 569
	public class DropConnectionRequest : Request
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0002B810 File Offset: 0x00029A10
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x0002B818 File Offset: 0x00029A18
		public NetworkID networkId { get; set; }

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0002B824 File Offset: 0x00029A24
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0002B82C File Offset: 0x00029A2C
		public NodeID nodeId { get; set; }

		// Token: 0x060022A8 RID: 8872 RVA: 0x0002B838 File Offset: 0x00029A38
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},nodeId:0x{2}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.nodeId.ToString("X")
			});
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0002B890 File Offset: 0x00029A90
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid && this.nodeId != NodeID.Invalid;
		}
	}
}
