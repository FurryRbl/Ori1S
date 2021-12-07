using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000468 RID: 1128
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x06002EB7 RID: 11959 RVA: 0x0009AC34 File Offset: 0x00098E34
		internal CrossAppDomainData(int domainId)
		{
			this._ContextID = 0;
			this._DomainID = domainId;
			this._processGuid = RemotingConfiguration.ProcessId;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x0009AC68 File Offset: 0x00098E68
		internal int DomainID
		{
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x0009AC70 File Offset: 0x00098E70
		internal string ProcessID
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x040013E5 RID: 5093
		private object _ContextID;

		// Token: 0x040013E6 RID: 5094
		private int _DomainID;

		// Token: 0x040013E7 RID: 5095
		private string _processGuid;
	}
}
