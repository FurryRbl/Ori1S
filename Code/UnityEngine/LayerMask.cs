using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200005A RID: 90
	[UsedByNativeCode]
	public struct LayerMask
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00004E50 File Offset: 0x00003050
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00004E58 File Offset: 0x00003058
		public int value
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x060004D3 RID: 1235
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string LayerToName(int layer);

		// Token: 0x060004D4 RID: 1236
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NameToLayer(string layerName);

		// Token: 0x060004D5 RID: 1237 RVA: 0x00004E64 File Offset: 0x00003064
		public static int GetMask(params string[] layerNames)
		{
			if (layerNames == null)
			{
				throw new ArgumentNullException("layerNames");
			}
			int num = 0;
			foreach (string layerName in layerNames)
			{
				int num2 = LayerMask.NameToLayer(layerName);
				if (num2 != -1)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00004EBC File Offset: 0x000030BC
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00004EC8 File Offset: 0x000030C8
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask result;
			result.m_Mask = intVal;
			return result;
		}

		// Token: 0x040000D3 RID: 211
		private int m_Mask;
	}
}
