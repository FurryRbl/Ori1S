using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000318 RID: 792
	internal class Win32GroupIconResource : Win32Resource
	{
		// Token: 0x06002864 RID: 10340 RVA: 0x00090D2C File Offset: 0x0008EF2C
		public Win32GroupIconResource(int id, int language, Win32IconResource[] icons) : base(Win32ResourceType.RT_GROUP_ICON, id, language)
		{
			this.icons = icons;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x00090D40 File Offset: 0x0008EF40
		public override void WriteTo(Stream s)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(s))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write((short)this.icons.Length);
				for (int i = 0; i < this.icons.Length; i++)
				{
					Win32IconResource win32IconResource = this.icons[i];
					ICONDIRENTRY icon = win32IconResource.Icon;
					binaryWriter.Write(icon.bWidth);
					binaryWriter.Write(icon.bHeight);
					binaryWriter.Write(icon.bColorCount);
					binaryWriter.Write(0);
					binaryWriter.Write(icon.wPlanes);
					binaryWriter.Write(icon.wBitCount);
					binaryWriter.Write(icon.image.Length);
					binaryWriter.Write((short)win32IconResource.Name.Id);
				}
			}
		}

		// Token: 0x04001065 RID: 4197
		private Win32IconResource[] icons;
	}
}
