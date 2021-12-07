using System;

namespace System.Configuration
{
	// Token: 0x020001D2 RID: 466
	internal class SectionData
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x0002AFC8 File Offset: 0x000291C8
		public SectionData(string sectionName, string typeName, bool allowLocation, AllowDefinition allowDefinition, bool requirePermission)
		{
			this.SectionName = sectionName;
			this.TypeName = typeName;
			this.AllowLocation = allowLocation;
			this.AllowDefinition = allowDefinition;
			this.RequirePermission = requirePermission;
		}

		// Token: 0x04000488 RID: 1160
		public readonly string SectionName;

		// Token: 0x04000489 RID: 1161
		public readonly string TypeName;

		// Token: 0x0400048A RID: 1162
		public readonly bool AllowLocation;

		// Token: 0x0400048B RID: 1163
		public readonly AllowDefinition AllowDefinition;

		// Token: 0x0400048C RID: 1164
		public string FileName;

		// Token: 0x0400048D RID: 1165
		public readonly bool RequirePermission;
	}
}
