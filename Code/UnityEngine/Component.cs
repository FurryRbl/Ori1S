using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000C6 RID: 198
	[RequiredByNativeCode]
	public class Component : Object
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000BD3 RID: 3027
		public extern Transform transform { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000BD4 RID: 3028
		public extern GameObject gameObject { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0000F664 File Offset: 0x0000D864
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			return this.gameObject.GetComponent(type);
		}

		// Token: 0x06000BD6 RID: 3030
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0000F674 File Offset: 0x0000D874
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x06000BD8 RID: 3032
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponent(string type);

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[ExcludeFromDocs]
		public Component GetComponentInChildren(Type t)
		{
			bool includeInactive = false;
			return this.GetComponentInChildren(t, includeInactive);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		public Component GetComponentInChildren(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentInChildren(t, includeInactive);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool includeInactive = false;
			return this.GetComponentInChildren<T>(includeInactive);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0000F704 File Offset: 0x0000D904
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type t)
		{
			bool includeInactive = false;
			return this.GetComponentsInChildren(t, includeInactive);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0000F71C File Offset: 0x0000D91C
		public Component[] GetComponentsInChildren(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren(t, includeInactive);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0000F72C File Offset: 0x0000D92C
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0000F73C File Offset: 0x0000D93C
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0000F74C File Offset: 0x0000D94C
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0000F758 File Offset: 0x0000D958
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0000F764 File Offset: 0x0000D964
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t)
		{
			return this.gameObject.GetComponentInParent(t);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0000F774 File Offset: 0x0000D974
		public T GetComponentInParent<T>()
		{
			return (T)((object)this.GetComponentInParent(typeof(T)));
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0000F78C File Offset: 0x0000D98C
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type t)
		{
			bool includeInactive = false;
			return this.GetComponentsInParent(t, includeInactive);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		public Component[] GetComponentsInParent(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent(t, includeInactive);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent<T>(includeInactive);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.gameObject.GetComponentsInParent<T>(includeInactive, results);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public Component[] GetComponents(Type type)
		{
			return this.gameObject.GetComponents(type);
		}

		// Token: 0x06000BEB RID: 3051
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetComponentsForListInternal(Type searchType, object resultList);

		// Token: 0x06000BEC RID: 3052 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, results);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsForListInternal(typeof(T), results);
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0000F810 File Offset: 0x0000DA10
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0000F820 File Offset: 0x0000DA20
		public string tag
		{
			get
			{
				return this.gameObject.tag;
			}
			set
			{
				this.gameObject.tag = value;
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0000F830 File Offset: 0x0000DA30
		public T[] GetComponents<T>()
		{
			return this.gameObject.GetComponents<T>();
		}

		// Token: 0x06000BF1 RID: 3057
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CompareTag(string tag);

		// Token: 0x06000BF2 RID: 3058
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0000F840 File Offset: 0x0000DA40
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0000F858 File Offset: 0x0000DA58
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0000F874 File Offset: 0x0000DA74
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x06000BF6 RID: 3062
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0000F880 File Offset: 0x0000DA80
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0000F898 File Offset: 0x0000DA98
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x06000BFA RID: 3066
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000BFB RID: 3067 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object parameter = null;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}
	}
}
