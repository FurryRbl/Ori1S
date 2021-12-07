using System;

namespace Mono.Xml
{
	// Token: 0x020000CD RID: 205
	internal class DTDNotationDeclaration : DTDNode
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x00027484 File Offset: 0x00025684
		internal DTDNotationDeclaration(DTDObjectModel root)
		{
			base.SetRoot(root);
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00027494 File Offset: 0x00025694
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0002749C File Offset: 0x0002569C
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x000274A8 File Offset: 0x000256A8
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x000274B0 File Offset: 0x000256B0
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

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x000274BC File Offset: 0x000256BC
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x000274C4 File Offset: 0x000256C4
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x000274D0 File Offset: 0x000256D0
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x000274D8 File Offset: 0x000256D8
		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x000274E4 File Offset: 0x000256E4
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x000274EC File Offset: 0x000256EC
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x04000421 RID: 1057
		private string name;

		// Token: 0x04000422 RID: 1058
		private string localName;

		// Token: 0x04000423 RID: 1059
		private string prefix;

		// Token: 0x04000424 RID: 1060
		private string publicId;

		// Token: 0x04000425 RID: 1061
		private string systemId;
	}
}
