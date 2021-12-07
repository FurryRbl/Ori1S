using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000090 RID: 144
	public struct SortingLayer
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		public int id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public string name
		{
			get
			{
				return SortingLayer.IDToName(this.m_Id);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		public int value
		{
			get
			{
				return SortingLayer.GetLayerValueFromID(this.m_Id);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		public static SortingLayer[] layers
		{
			get
			{
				int[] sortingLayerIDsInternal = SortingLayer.GetSortingLayerIDsInternal();
				SortingLayer[] array = new SortingLayer[sortingLayerIDsInternal.Length];
				for (int i = 0; i < sortingLayerIDsInternal.Length; i++)
				{
					array[i].m_Id = sortingLayerIDsInternal[i];
				}
				return array;
			}
		}

		// Token: 0x060008AE RID: 2222
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int[] GetSortingLayerIDsInternal();

		// Token: 0x060008AF RID: 2223
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLayerValueFromID(int id);

		// Token: 0x060008B0 RID: 2224
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLayerValueFromName(string name);

		// Token: 0x060008B1 RID: 2225
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NameToID(string name);

		// Token: 0x060008B2 RID: 2226
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string IDToName(int id);

		// Token: 0x060008B3 RID: 2227
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsValid(int id);

		// Token: 0x0400018B RID: 395
		private int m_Id;
	}
}
