using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200014F RID: 335
	public sealed class PolygonCollider2D : Collider2D
	{
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001624 RID: 5668
		// (set) Token: 0x06001625 RID: 5669
		public extern Vector2[] points { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001626 RID: 5670
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector2[] GetPath(int index);

		// Token: 0x06001627 RID: 5671
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPath(int index, Vector2[] points);

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001628 RID: 5672
		// (set) Token: 0x06001629 RID: 5673
		public extern int pathCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600162A RID: 5674
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetTotalPointCount();

		// Token: 0x0600162B RID: 5675 RVA: 0x00017BBC File Offset: 0x00015DBC
		public void CreatePrimitive(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset)
		{
			PolygonCollider2D.INTERNAL_CALL_CreatePrimitive(this, sides, ref scale, ref offset);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00017BCC File Offset: 0x00015DCC
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides, Vector2 scale)
		{
			Vector2 zero = Vector2.zero;
			PolygonCollider2D.INTERNAL_CALL_CreatePrimitive(this, sides, ref scale, ref zero);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00017BEC File Offset: 0x00015DEC
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides)
		{
			Vector2 zero = Vector2.zero;
			Vector2 one = Vector2.one;
			PolygonCollider2D.INTERNAL_CALL_CreatePrimitive(this, sides, ref one, ref zero);
		}

		// Token: 0x0600162E RID: 5678
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_CreatePrimitive(PolygonCollider2D self, int sides, ref Vector2 scale, ref Vector2 offset);
	}
}
