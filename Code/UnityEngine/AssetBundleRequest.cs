using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssetBundleRequest : AsyncOperation
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
		public Object asset
		{
			get
			{
				return this.m_AssetBundle.LoadAsset(this.m_Path, this.m_Type);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002118 File Offset: 0x00000318
		public Object[] allAssets
		{
			get
			{
				return this.m_AssetBundle.LoadAssetWithSubAssets_Internal(this.m_Path, this.m_Type);
			}
		}

		// Token: 0x04000001 RID: 1
		internal AssetBundle m_AssetBundle;

		// Token: 0x04000002 RID: 2
		internal string m_Path;

		// Token: 0x04000003 RID: 3
		internal Type m_Type;
	}
}
