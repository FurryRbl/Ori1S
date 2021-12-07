using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000316 RID: 790
	internal class Win32EncodedResource : Win32Resource
	{
		// Token: 0x0600285E RID: 10334 RVA: 0x00090CB8 File Offset: 0x0008EEB8
		internal Win32EncodedResource(NameOrId type, NameOrId name, int language, byte[] data) : base(type, name, language)
		{
			this.data = data;
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x00090CCC File Offset: 0x0008EECC
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x00090CD4 File Offset: 0x0008EED4
		public override void WriteTo(Stream s)
		{
			s.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x04001063 RID: 4195
		private byte[] data;
	}
}
