using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200012D RID: 301
	public class Joint : Component
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600133C RID: 4924
		// (set) Token: 0x0600133D RID: 4925
		public extern Rigidbody connectedBody { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00015CF4 File Offset: 0x00013EF4
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x00015D0C File Offset: 0x00013F0C
		public Vector3 axis
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_axis(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_axis(ref value);
			}
		}

		// Token: 0x06001340 RID: 4928
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_axis(out Vector3 value);

		// Token: 0x06001341 RID: 4929
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_axis(ref Vector3 value);

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00015D18 File Offset: 0x00013F18
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x00015D30 File Offset: 0x00013F30
		public Vector3 anchor
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_anchor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchor(ref value);
			}
		}

		// Token: 0x06001344 RID: 4932
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchor(out Vector3 value);

		// Token: 0x06001345 RID: 4933
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchor(ref Vector3 value);

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00015D3C File Offset: 0x00013F3C
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x00015D54 File Offset: 0x00013F54
		public Vector3 connectedAnchor
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_connectedAnchor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_connectedAnchor(ref value);
			}
		}

		// Token: 0x06001348 RID: 4936
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_connectedAnchor(out Vector3 value);

		// Token: 0x06001349 RID: 4937
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_connectedAnchor(ref Vector3 value);

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600134A RID: 4938
		// (set) Token: 0x0600134B RID: 4939
		public extern bool autoConfigureConnectedAnchor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600134C RID: 4940
		// (set) Token: 0x0600134D RID: 4941
		public extern float breakForce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600134E RID: 4942
		// (set) Token: 0x0600134F RID: 4943
		public extern float breakTorque { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001350 RID: 4944
		// (set) Token: 0x06001351 RID: 4945
		public extern bool enableCollision { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001352 RID: 4946
		// (set) Token: 0x06001353 RID: 4947
		public extern bool enablePreprocessing { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
