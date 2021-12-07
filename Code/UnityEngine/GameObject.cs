using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000C8 RID: 200
	public sealed class GameObject : Object
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0000F94C File Offset: 0x0000DB4C
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0000F96C File Offset: 0x0000DB6C
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type componentType in components)
			{
				this.AddComponent(componentType);
			}
		}

		// Token: 0x06000C39 RID: 3129
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		// Token: 0x06000C3A RID: 3130
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponent(Type type);

		// Token: 0x06000C3B RID: 3131
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06000C3C RID: 3132 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x06000C3D RID: 3133
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Component GetComponentByName(string type);

		// Token: 0x06000C3E RID: 3134 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x06000C3F RID: 3135
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponentInChildren(Type type, [DefaultValue("false")] bool includeInactive);

		// Token: 0x06000C40 RID: 3136 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		[ExcludeFromDocs]
		public Component GetComponentInChildren(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentInChildren(type, includeInactive);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0000FA04 File Offset: 0x0000DC04
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool includeInactive = false;
			return this.GetComponentInChildren<T>(includeInactive);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x06000C43 RID: 3139
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponentInParent(Type type);

		// Token: 0x06000C44 RID: 3140 RVA: 0x0000FA34 File Offset: 0x0000DC34
		public T GetComponentInParent<T>()
		{
			return (T)((object)this.GetComponentInParent(typeof(T)));
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		public Component[] GetComponents(Type type)
		{
			return (Component[])this.GetComponentsInternal(type, false, false, true, false, null);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0000FA60 File Offset: 0x0000DC60
		public T[] GetComponents<T>()
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false, null);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsInternal(type, false, false, true, false, results);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0000FA98 File Offset: 0x0000DC98
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsInternal(typeof(T), false, false, true, false, results);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0000FABC File Offset: 0x0000DCBC
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInChildren(type, includeInactive);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, false, null);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, null);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0000FB10 File Offset: 0x0000DD10
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, results);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0000FB40 File Offset: 0x0000DD40
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInParent(type, includeInactive);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0000FB64 File Offset: 0x0000DD64
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, true, null);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0000FB78 File Offset: 0x0000DD78
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, results);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, null);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06000C54 RID: 3156
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Array GetComponentsInternal(Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList);

		// Token: 0x06000C55 RID: 3157
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Component AddComponentInternal(string className);

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C56 RID: 3158
		public extern Transform transform { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C57 RID: 3159
		// (set) Token: 0x06000C58 RID: 3160
		public extern int layer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C59 RID: 3161
		// (set) Token: 0x06000C5A RID: 3162
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000C5B RID: 3163
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetActive(bool value);

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C5C RID: 3164
		public extern bool activeSelf { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C5D RID: 3165
		public extern bool activeInHierarchy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000C5E RID: 3166
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetActiveRecursively(bool state);

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C5F RID: 3167
		// (set) Token: 0x06000C60 RID: 3168
		public extern bool isStatic { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C61 RID: 3169
		internal extern bool isStaticBatchable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C62 RID: 3170
		// (set) Token: 0x06000C63 RID: 3171
		public extern string tag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000C64 RID: 3172
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CompareTag(string tag);

		// Token: 0x06000C65 RID: 3173
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		// Token: 0x06000C66 RID: 3174 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x06000C67 RID: 3175
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		// Token: 0x06000C68 RID: 3176
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000C69 RID: 3177 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x06000C6C RID: 3180
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000C6D RID: 3181 RVA: 0x0000FC18 File Offset: 0x0000DE18
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0000FC30 File Offset: 0x0000DE30
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x06000C70 RID: 3184
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000C71 RID: 3185 RVA: 0x0000FC58 File Offset: 0x0000DE58
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0000FC70 File Offset: 0x0000DE70
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object parameter = null;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x06000C74 RID: 3188
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		// Token: 0x06000C75 RID: 3189 RVA: 0x0000FC98 File Offset: 0x0000DE98
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x06000C77 RID: 3191
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateGameObject([Writable] GameObject mono, string name);

		// Token: 0x06000C78 RID: 3192
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject Find(string name);

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public Scene scene
		{
			get
			{
				Scene result;
				this.INTERNAL_get_scene(out result);
				return result;
			}
		}

		// Token: 0x06000C7A RID: 3194
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_scene(out Scene value);

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}
	}
}
