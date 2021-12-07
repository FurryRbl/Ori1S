using System;

namespace UnityEngine
{
	// Token: 0x0200030C RID: 780
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class UnityAPICompatibilityVersionAttribute : Attribute
	{
		// Token: 0x06002718 RID: 10008 RVA: 0x00036FC4 File Offset: 0x000351C4
		public UnityAPICompatibilityVersionAttribute(string version)
		{
			this._version = version;
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x00036FD4 File Offset: 0x000351D4
		public string version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x04000C0D RID: 3085
		private string _version;
	}
}
