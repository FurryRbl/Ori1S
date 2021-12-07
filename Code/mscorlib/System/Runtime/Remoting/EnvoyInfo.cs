using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000419 RID: 1049
	[Serializable]
	internal class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x06002CB9 RID: 11449 RVA: 0x00093EBC File Offset: 0x000920BC
		public EnvoyInfo(IMessageSink sinks)
		{
			this.envoySinks = sinks;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x00093ECC File Offset: 0x000920CC
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x00093ED4 File Offset: 0x000920D4
		public IMessageSink EnvoySinks
		{
			get
			{
				return this.envoySinks;
			}
			set
			{
				this.envoySinks = value;
			}
		}

		// Token: 0x0400135B RID: 4955
		private IMessageSink envoySinks;
	}
}
