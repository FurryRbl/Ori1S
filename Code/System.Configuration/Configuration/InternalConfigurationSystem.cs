using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000050 RID: 80
	internal class InternalConfigurationSystem : IConfigSystem
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00008824 File Offset: 0x00006A24
		public void Init(Type typeConfigHost, params object[] hostInitParams)
		{
			this.hostInitParams = hostInitParams;
			this.host = (IInternalConfigHost)Activator.CreateInstance(typeConfigHost);
			this.root = new InternalConfigurationRoot();
			this.root.Init(this.host, false);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000885C File Offset: 0x00006A5C
		public void InitForConfiguration(ref string locationConfigPath, out string parentConfigPath, out string parentLocationConfigPath)
		{
			this.host.InitForConfiguration(ref locationConfigPath, out parentConfigPath, out parentLocationConfigPath, this.root, this.hostInitParams);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00008878 File Offset: 0x00006A78
		public IInternalConfigHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00008880 File Offset: 0x00006A80
		public IInternalConfigRoot Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x040000E4 RID: 228
		private IInternalConfigHost host;

		// Token: 0x040000E5 RID: 229
		private IInternalConfigRoot root;

		// Token: 0x040000E6 RID: 230
		private object[] hostInitParams;
	}
}
