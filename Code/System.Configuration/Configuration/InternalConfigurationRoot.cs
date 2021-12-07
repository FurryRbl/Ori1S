using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000054 RID: 84
	internal class InternalConfigurationRoot : IInternalConfigRoot
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002FD RID: 765 RVA: 0x00008E10 File Offset: 0x00007010
		// (remove) Token: 0x060002FE RID: 766 RVA: 0x00008E2C File Offset: 0x0000702C
		public event InternalConfigEventHandler ConfigChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002FF RID: 767 RVA: 0x00008E48 File Offset: 0x00007048
		// (remove) Token: 0x06000300 RID: 768 RVA: 0x00008E64 File Offset: 0x00007064
		public event InternalConfigEventHandler ConfigRemoved;

		// Token: 0x06000301 RID: 769 RVA: 0x00008E80 File Offset: 0x00007080
		[MonoTODO]
		public IInternalConfigRecord GetConfigRecord(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008E88 File Offset: 0x00007088
		public object GetSection(string section, string configPath)
		{
			IInternalConfigRecord configRecord = this.GetConfigRecord(configPath);
			return configRecord.GetSection(section);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008EA4 File Offset: 0x000070A4
		[MonoTODO]
		public string GetUniqueConfigPath(string configPath)
		{
			return configPath;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00008EA8 File Offset: 0x000070A8
		[MonoTODO]
		public IInternalConfigRecord GetUniqueConfigRecord(string configPath)
		{
			return this.GetConfigRecord(this.GetUniqueConfigPath(configPath));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00008EB8 File Offset: 0x000070B8
		public void Init(IInternalConfigHost host, bool isDesignTime)
		{
			this.host = host;
			this.isDesignTime = isDesignTime;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008EC8 File Offset: 0x000070C8
		[MonoTODO]
		public void RemoveConfig(string configPath)
		{
			this.host.DeleteStream(configPath);
			if (this.ConfigRemoved != null)
			{
				this.ConfigRemoved(this, new InternalConfigEventArgs(configPath));
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00008EF4 File Offset: 0x000070F4
		public bool IsDesignTime
		{
			get
			{
				return this.isDesignTime;
			}
		}

		// Token: 0x040000EB RID: 235
		private IInternalConfigHost host;

		// Token: 0x040000EC RID: 236
		private bool isDesignTime;
	}
}
