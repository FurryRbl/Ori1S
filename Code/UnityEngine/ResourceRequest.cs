using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000083 RID: 131
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ResourceRequest : AsyncOperation
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0000B614 File Offset: 0x00009814
		public Object asset
		{
			get
			{
				return Resources.Load(this.m_Path, this.m_Type);
			}
		}

		// Token: 0x04000182 RID: 386
		internal string m_Path;

		// Token: 0x04000183 RID: 387
		internal Type m_Type;
	}
}
