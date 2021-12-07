using System;

namespace System.Configuration
{
	// Token: 0x02000035 RID: 53
	internal class ConfigurationSaveEventArgs : EventArgs
	{
		// Token: 0x06000207 RID: 519 RVA: 0x000070A0 File Offset: 0x000052A0
		public ConfigurationSaveEventArgs(string streamPath, bool start, Exception ex, object context)
		{
			this.StreamPath = streamPath;
			this.Start = start;
			this.Failed = (ex != null);
			this.Exception = ex;
			this.Context = context;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000070E0 File Offset: 0x000052E0
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000070E8 File Offset: 0x000052E8
		public string StreamPath { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000070F4 File Offset: 0x000052F4
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000070FC File Offset: 0x000052FC
		public bool Start { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00007108 File Offset: 0x00005308
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00007110 File Offset: 0x00005310
		public object Context { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000711C File Offset: 0x0000531C
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00007124 File Offset: 0x00005324
		public bool Failed { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00007130 File Offset: 0x00005330
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00007138 File Offset: 0x00005338
		public Exception Exception { get; private set; }
	}
}
