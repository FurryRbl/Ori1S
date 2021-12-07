using System;
using System.IO;
using System.Xml;
using Mono.Xml2;

namespace Mono.Xml
{
	// Token: 0x020000CB RID: 203
	internal class DTDEntityBase : DTDNode
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x00026DAC File Offset: 0x00024FAC
		protected DTDEntityBase(DTDObjectModel root)
		{
			base.SetRoot(root);
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00026DBC File Offset: 0x00024FBC
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00026DC4 File Offset: 0x00024FC4
		internal bool IsInvalid
		{
			get
			{
				return this.isInvalid;
			}
			set
			{
				this.isInvalid = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00026DD0 File Offset: 0x00024FD0
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00026DD8 File Offset: 0x00024FD8
		public bool LoadFailed
		{
			get
			{
				return this.loadFailed;
			}
			set
			{
				this.loadFailed = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00026DE4 File Offset: 0x00024FE4
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00026DEC File Offset: 0x00024FEC
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00026DF8 File Offset: 0x00024FF8
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00026E00 File Offset: 0x00025000
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
			set
			{
				this.publicId = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00026E0C File Offset: 0x0002500C
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00026E14 File Offset: 0x00025014
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
			set
			{
				this.systemId = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00026E20 File Offset: 0x00025020
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00026E28 File Offset: 0x00025028
		public string LiteralEntityValue
		{
			get
			{
				return this.literalValue;
			}
			set
			{
				this.literalValue = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00026E34 File Offset: 0x00025034
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00026E3C File Offset: 0x0002503C
		public string ReplacementText
		{
			get
			{
				return this.replacementText;
			}
			set
			{
				this.replacementText = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00026E48 File Offset: 0x00025048
		public XmlResolver XmlResolver
		{
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00026E54 File Offset: 0x00025054
		public string ActualUri
		{
			get
			{
				if (this.uriString == null)
				{
					if (this.resolver == null || this.SystemId == null || this.SystemId.Length == 0)
					{
						this.uriString = this.BaseURI;
					}
					else
					{
						Uri baseUri = null;
						try
						{
							if (this.BaseURI != null && this.BaseURI.Length > 0)
							{
								baseUri = new Uri(this.BaseURI);
							}
						}
						catch (UriFormatException)
						{
						}
						this.absUri = this.resolver.ResolveUri(baseUri, this.SystemId);
						this.uriString = ((!(this.absUri != null)) ? string.Empty : this.absUri.ToString());
					}
				}
				return this.uriString;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00026F40 File Offset: 0x00025140
		public void Resolve()
		{
			if (this.ActualUri == string.Empty)
			{
				this.LoadFailed = true;
				this.LiteralEntityValue = string.Empty;
				return;
			}
			if (base.Root.ExternalResources.ContainsKey(this.ActualUri))
			{
				this.LiteralEntityValue = (string)base.Root.ExternalResources[this.ActualUri];
			}
			Stream stream = null;
			try
			{
				stream = (this.resolver.GetEntity(this.absUri, null, typeof(Stream)) as Stream);
				Mono.Xml2.XmlTextReader xmlTextReader = new Mono.Xml2.XmlTextReader(this.ActualUri, stream, base.Root.NameTable);
				this.LiteralEntityValue = xmlTextReader.GetRemainder().ReadToEnd();
				base.Root.ExternalResources.Add(this.ActualUri, this.LiteralEntityValue);
				if (base.Root.ExternalResources.Count > 256)
				{
					throw new InvalidOperationException("The total amount of external entities exceeded the allowed number.");
				}
			}
			catch (Exception)
			{
				this.LiteralEntityValue = string.Empty;
				this.LoadFailed = true;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x04000411 RID: 1041
		private string name;

		// Token: 0x04000412 RID: 1042
		private string publicId;

		// Token: 0x04000413 RID: 1043
		private string systemId;

		// Token: 0x04000414 RID: 1044
		private string literalValue;

		// Token: 0x04000415 RID: 1045
		private string replacementText;

		// Token: 0x04000416 RID: 1046
		private string uriString;

		// Token: 0x04000417 RID: 1047
		private Uri absUri;

		// Token: 0x04000418 RID: 1048
		private bool isInvalid;

		// Token: 0x04000419 RID: 1049
		private bool loadFailed;

		// Token: 0x0400041A RID: 1050
		private XmlResolver resolver;
	}
}
