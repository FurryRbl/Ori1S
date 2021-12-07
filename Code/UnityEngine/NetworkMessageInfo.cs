using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200007C RID: 124
	[RequiredByNativeCode]
	public struct NetworkMessageInfo
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0000AB34 File Offset: 0x00008D34
		public double timestamp
		{
			get
			{
				return this.m_TimeStamp;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0000AB3C File Offset: 0x00008D3C
		public NetworkPlayer sender
		{
			get
			{
				return this.m_Sender;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0000AB44 File Offset: 0x00008D44
		public NetworkView networkView
		{
			get
			{
				if (this.m_ViewID == NetworkViewID.unassigned)
				{
					Debug.LogError("No NetworkView is assigned to this NetworkMessageInfo object. Note that this is expected in OnNetworkInstantiate().");
					return this.NullNetworkView();
				}
				return NetworkView.Find(this.m_ViewID);
			}
		}

		// Token: 0x06000795 RID: 1941
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern NetworkView NullNetworkView();

		// Token: 0x0400015B RID: 347
		private double m_TimeStamp;

		// Token: 0x0400015C RID: 348
		private NetworkPlayer m_Sender;

		// Token: 0x0400015D RID: 349
		private NetworkViewID m_ViewID;
	}
}
