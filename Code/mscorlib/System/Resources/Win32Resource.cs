using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000315 RID: 789
	internal abstract class Win32Resource
	{
		// Token: 0x06002856 RID: 10326 RVA: 0x00090BF0 File Offset: 0x0008EDF0
		internal Win32Resource(NameOrId type, NameOrId name, int language)
		{
			this.type = type;
			this.name = name;
			this.language = language;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00090C10 File Offset: 0x0008EE10
		internal Win32Resource(Win32ResourceType type, int name, int language)
		{
			this.type = new NameOrId((int)type);
			this.name = new NameOrId(name);
			this.language = language;
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x00090C38 File Offset: 0x0008EE38
		public Win32ResourceType ResourceType
		{
			get
			{
				if (this.type.IsName)
				{
					return (Win32ResourceType)(-1);
				}
				return (Win32ResourceType)this.type.Id;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x00090C58 File Offset: 0x0008EE58
		public NameOrId Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x00090C60 File Offset: 0x0008EE60
		public NameOrId Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x00090C68 File Offset: 0x0008EE68
		public int Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x0600285C RID: 10332
		public abstract void WriteTo(Stream s);

		// Token: 0x0600285D RID: 10333 RVA: 0x00090C70 File Offset: 0x0008EE70
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Win32Resource (Kind=",
				this.ResourceType,
				", Name=",
				this.name,
				")"
			});
		}

		// Token: 0x04001060 RID: 4192
		private NameOrId type;

		// Token: 0x04001061 RID: 4193
		private NameOrId name;

		// Token: 0x04001062 RID: 4194
		private int language;
	}
}
