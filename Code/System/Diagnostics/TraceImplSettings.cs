using System;

namespace System.Diagnostics
{
	// Token: 0x0200025C RID: 604
	internal class TraceImplSettings
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x00037B10 File Offset: 0x00035D10
		public TraceImplSettings()
		{
			this.Listeners.Add(new DefaultTraceListener(), this);
		}

		// Token: 0x0400068F RID: 1679
		public const string Key = ".__TraceInfoSettingsKey__.";

		// Token: 0x04000690 RID: 1680
		public bool AutoFlush;

		// Token: 0x04000691 RID: 1681
		public int IndentLevel;

		// Token: 0x04000692 RID: 1682
		public int IndentSize = 4;

		// Token: 0x04000693 RID: 1683
		public TraceListenerCollection Listeners = new TraceListenerCollection(false);
	}
}
