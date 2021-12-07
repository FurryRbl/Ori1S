using System;
using System.Text;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000220 RID: 544
	public class MultipartFormDataSection : IMultipartFormSection
	{
		// Token: 0x060021C5 RID: 8645 RVA: 0x0002A6A8 File Offset: 0x000288A8
		public MultipartFormDataSection(string name, byte[] data, string contentType)
		{
			if (data == null || data.Length < 1)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			this.name = name;
			this.data = data;
			this.content = contentType;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0002A6E0 File Offset: 0x000288E0
		public MultipartFormDataSection(string name, byte[] data) : this(name, data, null)
		{
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0002A6EC File Offset: 0x000288EC
		public MultipartFormDataSection(byte[] data) : this(null, data)
		{
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0002A6F8 File Offset: 0x000288F8
		public MultipartFormDataSection(string name, string data, Encoding encoding, string contentType)
		{
			if (data == null || data.Length < 1)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			byte[] bytes = encoding.GetBytes(data);
			this.name = name;
			this.data = bytes;
			if (contentType != null && !contentType.Contains("encoding="))
			{
				contentType = contentType.Trim() + "; encoding=" + encoding.WebName;
			}
			this.content = contentType;
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0002A778 File Offset: 0x00028978
		public MultipartFormDataSection(string name, string data, string contentType) : this(name, data, Encoding.UTF8, contentType)
		{
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0002A788 File Offset: 0x00028988
		public MultipartFormDataSection(string name, string data) : this(name, data, "text/plain")
		{
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0002A798 File Offset: 0x00028998
		public MultipartFormDataSection(string data) : this(null, data)
		{
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0002A7A4 File Offset: 0x000289A4
		public string sectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x0002A7AC File Offset: 0x000289AC
		public byte[] sectionData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x0002A7B4 File Offset: 0x000289B4
		public string fileName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x0002A7B8 File Offset: 0x000289B8
		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x040008D8 RID: 2264
		private string name;

		// Token: 0x040008D9 RID: 2265
		private byte[] data;

		// Token: 0x040008DA RID: 2266
		private string content;
	}
}
