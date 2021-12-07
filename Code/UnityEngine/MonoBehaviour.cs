using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B7 RID: 183
	[RequiredByNativeCode]
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x06000B13 RID: 2835
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MonoBehaviour();

		// Token: 0x06000B14 RID: 2836
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CancelInvokeAll();

		// Token: 0x06000B15 RID: 2837
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsInvokingAll();

		// Token: 0x06000B16 RID: 2838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Invoke(string methodName, float time);

		// Token: 0x06000B17 RID: 2839
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InvokeRepeating(string methodName, float time, float repeatRate);

		// Token: 0x06000B18 RID: 2840 RVA: 0x0000F050 File Offset: 0x0000D250
		public void CancelInvoke()
		{
			this.Internal_CancelInvokeAll();
		}

		// Token: 0x06000B19 RID: 2841
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CancelInvoke(string methodName);

		// Token: 0x06000B1A RID: 2842
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInvoking(string methodName);

		// Token: 0x06000B1B RID: 2843 RVA: 0x0000F058 File Offset: 0x0000D258
		public bool IsInvoking()
		{
			return this.Internal_IsInvokingAll();
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0000F060 File Offset: 0x0000D260
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return this.StartCoroutine_Auto(routine);
		}

		// Token: 0x06000B1D RID: 2845
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Coroutine StartCoroutine_Auto(IEnumerator routine);

		// Token: 0x06000B1E RID: 2846
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value);

		// Token: 0x06000B1F RID: 2847 RVA: 0x0000F06C File Offset: 0x0000D26C
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object value = null;
			return this.StartCoroutine(methodName, value);
		}

		// Token: 0x06000B20 RID: 2848
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopCoroutine(string methodName);

		// Token: 0x06000B21 RID: 2849 RVA: 0x0000F084 File Offset: 0x0000D284
		public void StopCoroutine(IEnumerator routine)
		{
			this.StopCoroutineViaEnumerator_Auto(routine);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0000F090 File Offset: 0x0000D290
		public void StopCoroutine(Coroutine routine)
		{
			this.StopCoroutine_Auto(routine);
		}

		// Token: 0x06000B23 RID: 2851
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void StopCoroutineViaEnumerator_Auto(IEnumerator routine);

		// Token: 0x06000B24 RID: 2852
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void StopCoroutine_Auto(Coroutine routine);

		// Token: 0x06000B25 RID: 2853
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopAllCoroutines();

		// Token: 0x06000B26 RID: 2854 RVA: 0x0000F09C File Offset: 0x0000D29C
		public static void print(object message)
		{
			Debug.Log(message);
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B27 RID: 2855
		// (set) Token: 0x06000B28 RID: 2856
		public extern bool useGUILayout { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
