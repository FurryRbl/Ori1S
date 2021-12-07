using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000076 RID: 118
	public sealed class NetworkView : Behaviour
	{
		// Token: 0x060006E8 RID: 1768
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RPC(NetworkView view, string name, RPCMode mode, object[] args);

		// Token: 0x060006E9 RID: 1769
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RPC_Target(NetworkView view, string name, NetworkPlayer target, object[] args);

		// Token: 0x060006EA RID: 1770 RVA: 0x0000A5B8 File Offset: 0x000087B8
		[Obsolete("NetworkView RPC functions are deprecated. Refer to the new Multiplayer Networking system.")]
		public void RPC(string name, RPCMode mode, params object[] args)
		{
			NetworkView.Internal_RPC(this, name, mode, args);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000A5C4 File Offset: 0x000087C4
		[Obsolete("NetworkView RPC functions are deprecated. Refer to the new Multiplayer Networking system.")]
		public void RPC(string name, NetworkPlayer target, params object[] args)
		{
			NetworkView.Internal_RPC_Target(this, name, target, args);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006EC RID: 1772
		// (set) Token: 0x060006ED RID: 1773
		public extern Component observed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006EE RID: 1774
		// (set) Token: 0x060006EF RID: 1775
		public extern NetworkStateSynchronization stateSynchronization { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060006F0 RID: 1776
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetViewID(out NetworkViewID viewID);

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000A5D0 File Offset: 0x000087D0
		private void Internal_SetViewID(NetworkViewID viewID)
		{
			NetworkView.INTERNAL_CALL_Internal_SetViewID(this, ref viewID);
		}

		// Token: 0x060006F2 RID: 1778
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetViewID(NetworkView self, ref NetworkViewID viewID);

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000A5DC File Offset: 0x000087DC
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0000A5F4 File Offset: 0x000087F4
		public NetworkViewID viewID
		{
			get
			{
				NetworkViewID result;
				this.Internal_GetViewID(out result);
				return result;
			}
			set
			{
				this.Internal_SetViewID(value);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006F5 RID: 1781
		// (set) Token: 0x060006F6 RID: 1782
		public extern int group { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0000A600 File Offset: 0x00008800
		public bool isMine
		{
			get
			{
				return this.viewID.isMine;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000A61C File Offset: 0x0000881C
		public NetworkPlayer owner
		{
			get
			{
				return this.viewID.owner;
			}
		}

		// Token: 0x060006F9 RID: 1785
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetScope(NetworkPlayer player, bool relevancy);

		// Token: 0x060006FA RID: 1786 RVA: 0x0000A638 File Offset: 0x00008838
		public static NetworkView Find(NetworkViewID viewID)
		{
			return NetworkView.INTERNAL_CALL_Find(ref viewID);
		}

		// Token: 0x060006FB RID: 1787
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkView INTERNAL_CALL_Find(ref NetworkViewID viewID);
	}
}
