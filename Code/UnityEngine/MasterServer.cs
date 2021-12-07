using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200007B RID: 123
	public sealed class MasterServer
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000784 RID: 1924
		// (set) Token: 0x06000785 RID: 1925
		public static extern string ipAddress { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000786 RID: 1926
		// (set) Token: 0x06000787 RID: 1927
		public static extern int port { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000788 RID: 1928
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RequestHostList(string gameTypeName);

		// Token: 0x06000789 RID: 1929
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern HostData[] PollHostList();

		// Token: 0x0600078A RID: 1930
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterHost(string gameTypeName, string gameName, [DefaultValue("\"\"")] string comment);

		// Token: 0x0600078B RID: 1931 RVA: 0x0000AB18 File Offset: 0x00008D18
		[ExcludeFromDocs]
		public static void RegisterHost(string gameTypeName, string gameName)
		{
			string empty = string.Empty;
			MasterServer.RegisterHost(gameTypeName, gameName, empty);
		}

		// Token: 0x0600078C RID: 1932
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterHost();

		// Token: 0x0600078D RID: 1933
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearHostList();

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600078E RID: 1934
		// (set) Token: 0x0600078F RID: 1935
		public static extern int updateRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000790 RID: 1936
		// (set) Token: 0x06000791 RID: 1937
		public static extern bool dedicatedServer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
