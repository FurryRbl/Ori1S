using System;
using System.Text;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000221 RID: 545
	public class MultipartFormFileSection : IMultipartFormSection
	{
		// Token: 0x060021D0 RID: 8656 RVA: 0x0002A7C0 File Offset: 0x000289C0
		public MultipartFormFileSection(string name, byte[] data, string fileName, string contentType)
		{
			if (data == null || data.Length < 1)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				fileName = "file.dat";
			}
			if (string.IsNullOrEmpty(contentType))
			{
				contentType = "application/octet-stream";
			}
			this.Init(name, data, fileName, contentType);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0002A820 File Offset: 0x00028A20
		public MultipartFormFileSection(byte[] data) : this(null, data, null, null)
		{
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0002A82C File Offset: 0x00028A2C
		public MultipartFormFileSection(string fileName, byte[] data) : this(null, data, fileName, null)
		{
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0002A838 File Offset: 0x00028A38
		public MultipartFormFileSection(string name, string data, Encoding dataEncoding, string fileName)
		{
			if (data == null || data.Length < 1)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			if (dataEncoding == null)
			{
				dataEncoding = Encoding.UTF8;
			}
			byte[] bytes = dataEncoding.GetBytes(data);
			if (string.IsNullOrEmpty(fileName))
			{
				fileName = "file.txt";
			}
			if (string.IsNullOrEmpty(this.content))
			{
				this.content = "text/plain; charset=" + dataEncoding.WebName;
			}
			this.Init(name, bytes, fileName, this.content);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		public MultipartFormFileSection(string data, Encoding dataEncoding, string fileName) : this(null, data, dataEncoding, fileName)
		{
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0002A8D4 File Offset: 0x00028AD4
		public MultipartFormFileSection(string data, string fileName) : this(data, null, fileName)
		{
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0002A8E0 File Offset: 0x00028AE0
		private void Init(string name, byte[] data, string fileName, string contentType)
		{
			this.name = name;
			this.data = data;
			this.file = fileName;
			this.content = contentType;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0002A900 File Offset: 0x00028B00
		public string sectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x0002A908 File Offset: 0x00028B08
		public byte[] sectionData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0002A910 File Offset: 0x00028B10
		public string fileName
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x0002A918 File Offset: 0x00028B18
		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x040008DB RID: 2267
		private string name;

		// Token: 0x040008DC RID: 2268
		private byte[] data;

		// Token: 0x040008DD RID: 2269
		private string file;

		// Token: 0x040008DE RID: 2270
		private string content;
	}
}
