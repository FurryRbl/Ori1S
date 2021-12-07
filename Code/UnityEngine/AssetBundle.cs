using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	public sealed class AssetBundle : Object
	{
		// Token: 0x06000008 RID: 8
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AssetBundleCreateRequest LoadFromFileAsync(string path, [DefaultValue("0")] uint crc);

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		[ExcludeFromDocs]
		public static AssetBundleCreateRequest LoadFromFileAsync(string path)
		{
			uint crc = 0U;
			return AssetBundle.LoadFromFileAsync(path, crc);
		}

		// Token: 0x0600000A RID: 10
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AssetBundle LoadFromFile(string path, [DefaultValue("0")] uint crc);

		// Token: 0x0600000B RID: 11 RVA: 0x00002154 File Offset: 0x00000354
		[ExcludeFromDocs]
		public static AssetBundle LoadFromFile(string path)
		{
			uint crc = 0U;
			return AssetBundle.LoadFromFile(path, crc);
		}

		// Token: 0x0600000C RID: 12
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary, [DefaultValue("0")] uint crc);

		// Token: 0x0600000D RID: 13 RVA: 0x0000216C File Offset: 0x0000036C
		[ExcludeFromDocs]
		public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary)
		{
			uint crc = 0U;
			return AssetBundle.LoadFromMemoryAsync(binary, crc);
		}

		// Token: 0x0600000E RID: 14
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AssetBundle LoadFromMemory(byte[] binary, [DefaultValue("0")] uint crc);

		// Token: 0x0600000F RID: 15 RVA: 0x00002184 File Offset: 0x00000384
		[ExcludeFromDocs]
		public static AssetBundle LoadFromMemory(byte[] binary)
		{
			uint crc = 0U;
			return AssetBundle.LoadFromMemory(binary, crc);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16
		public extern Object mainAsset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000011 RID: 17
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Contains(string name);

		// Token: 0x06000012 RID: 18 RVA: 0x0000219C File Offset: 0x0000039C
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		public Object Load(string name)
		{
			return null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021A0 File Offset: 0x000003A0
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		public T Load<T>(string name) where T : Object
		{
			return (T)((object)null);
		}

		// Token: 0x06000014 RID: 20
		[WrapperlessIcall]
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Object Load(string name, Type type);

		// Token: 0x06000015 RID: 21
		[WrapperlessIcall]
		[Obsolete("Method LoadAsync has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAssetAsync instead and check the documentation for details.", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AssetBundleRequest LoadAsync(string name, Type type);

		// Token: 0x06000016 RID: 22
		[WrapperlessIcall]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Object[] LoadAll(Type type);

		// Token: 0x06000017 RID: 23 RVA: 0x000021A8 File Offset: 0x000003A8
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		public Object[] LoadAll()
		{
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021AC File Offset: 0x000003AC
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		public T[] LoadAll<T>() where T : Object
		{
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021B0 File Offset: 0x000003B0
		public Object LoadAsset(string name)
		{
			return this.LoadAsset(name, typeof(Object));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021C4 File Offset: 0x000003C4
		public T LoadAsset<T>(string name) where T : Object
		{
			return (T)((object)this.LoadAsset(name, typeof(T)));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021DC File Offset: 0x000003DC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		public Object LoadAsset(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAsset_Internal(name, type);
		}

		// Token: 0x0600001C RID: 28
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object LoadAsset_Internal(string name, Type type);

		// Token: 0x0600001D RID: 29 RVA: 0x0000222C File Offset: 0x0000042C
		public AssetBundleRequest LoadAssetAsync(string name)
		{
			return this.LoadAssetAsync(name, typeof(Object));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002240 File Offset: 0x00000440
		public AssetBundleRequest LoadAssetAsync<T>(string name)
		{
			return this.LoadAssetAsync(name, typeof(T));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002254 File Offset: 0x00000454
		public AssetBundleRequest LoadAssetAsync(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetAsync_Internal(name, type);
		}

		// Token: 0x06000020 RID: 32
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AssetBundleRequest LoadAssetAsync_Internal(string name, Type type);

		// Token: 0x06000021 RID: 33 RVA: 0x000022A4 File Offset: 0x000004A4
		public Object[] LoadAssetWithSubAssets(string name)
		{
			return this.LoadAssetWithSubAssets(name, typeof(Object));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022B8 File Offset: 0x000004B8
		public T[] LoadAssetWithSubAssets<T>(string name) where T : Object
		{
			return Resources.ConvertObjects<T>(this.LoadAssetWithSubAssets(name, typeof(T)));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022D0 File Offset: 0x000004D0
		public Object[] LoadAssetWithSubAssets(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssets_Internal(name, type);
		}

		// Token: 0x06000024 RID: 36
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Object[] LoadAssetWithSubAssets_Internal(string name, Type type);

		// Token: 0x06000025 RID: 37 RVA: 0x00002320 File Offset: 0x00000520
		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name)
		{
			return this.LoadAssetWithSubAssetsAsync(name, typeof(Object));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002334 File Offset: 0x00000534
		public AssetBundleRequest LoadAssetWithSubAssetsAsync<T>(string name)
		{
			return this.LoadAssetWithSubAssetsAsync(name, typeof(T));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002348 File Offset: 0x00000548
		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal(name, type);
		}

		// Token: 0x06000028 RID: 40
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AssetBundleRequest LoadAssetWithSubAssetsAsync_Internal(string name, Type type);

		// Token: 0x06000029 RID: 41 RVA: 0x00002398 File Offset: 0x00000598
		public Object[] LoadAllAssets()
		{
			return this.LoadAllAssets(typeof(Object));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023AC File Offset: 0x000005AC
		public T[] LoadAllAssets<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(this.LoadAllAssets(typeof(T)));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023C4 File Offset: 0x000005C4
		public Object[] LoadAllAssets(Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssets_Internal(string.Empty, type);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023E4 File Offset: 0x000005E4
		public AssetBundleRequest LoadAllAssetsAsync()
		{
			return this.LoadAllAssetsAsync(typeof(Object));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023F8 File Offset: 0x000005F8
		public AssetBundleRequest LoadAllAssetsAsync<T>()
		{
			return this.LoadAllAssetsAsync(typeof(T));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000240C File Offset: 0x0000060C
		public AssetBundleRequest LoadAllAssetsAsync(Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal(string.Empty, type);
		}

		// Token: 0x0600002F RID: 47
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Unload(bool unloadAllLoadedObjects);

		// Token: 0x06000030 RID: 48 RVA: 0x0000242C File Offset: 0x0000062C
		[Obsolete("This method is deprecated. Use GetAllAssetNames() instead.")]
		public string[] AllAssetNames()
		{
			return this.GetAllAssetNames();
		}

		// Token: 0x06000031 RID: 49
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetNames();

		// Token: 0x06000032 RID: 50
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllScenePaths();
	}
}
