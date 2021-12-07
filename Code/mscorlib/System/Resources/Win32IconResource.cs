using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000317 RID: 791
	internal class Win32IconResource : Win32Resource
	{
		// Token: 0x06002861 RID: 10337 RVA: 0x00090CEC File Offset: 0x0008EEEC
		public Win32IconResource(int id, int language, ICONDIRENTRY icon) : base(Win32ResourceType.RT_ICON, id, language)
		{
			this.icon = icon;
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x00090D00 File Offset: 0x0008EF00
		public ICONDIRENTRY Icon
		{
			get
			{
				return this.icon;
			}
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x00090D08 File Offset: 0x0008EF08
		public override void WriteTo(Stream s)
		{
			s.Write(this.icon.image, 0, this.icon.image.Length);
		}

		// Token: 0x04001064 RID: 4196
		private ICONDIRENTRY icon;
	}
}
