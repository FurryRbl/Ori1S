using System;
using System.Configuration.Internal;
using System.IO;
using System.Xml;

// Token: 0x0200003D RID: 61
internal class ConfigXmlTextReader : XmlTextReader, IConfigErrorInfo
{
	// Token: 0x06000254 RID: 596 RVA: 0x00007C78 File Offset: 0x00005E78
	public ConfigXmlTextReader(Stream s, string fileName) : base(s)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00007C9C File Offset: 0x00005E9C
	public ConfigXmlTextReader(TextReader input, string fileName) : base(input)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000256 RID: 598 RVA: 0x00007CC0 File Offset: 0x00005EC0
	public string Filename
	{
		get
		{
			return this.fileName;
		}
	}

	// Token: 0x040000C5 RID: 197
	private readonly string fileName;
}
