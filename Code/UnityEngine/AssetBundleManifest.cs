using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	public sealed class AssetBundleManifest : Object
	{
		// Token: 0x06000034 RID: 52
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetBundles();

		// Token: 0x06000035 RID: 53
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetBundlesWithVariant();

		// Token: 0x06000036 RID: 54 RVA: 0x0000243C File Offset: 0x0000063C
		public Hash128 GetAssetBundleHash(string assetBundleName)
		{
			Hash128 result;
			AssetBundleManifest.INTERNAL_CALL_GetAssetBundleHash(this, assetBundleName, out result);
			return result;
		}

		// Token: 0x06000037 RID: 55
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetAssetBundleHash(AssetBundleManifest self, string assetBundleName, out Hash128 value);

		// Token: 0x06000038 RID: 56
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetDirectDependencies(string assetBundleName);

		// Token: 0x06000039 RID: 57
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllDependencies(string assetBundleName);
	}
}
