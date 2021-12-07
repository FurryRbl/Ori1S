using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000C7 RID: 199
	internal abstract class DTDNode : IXmlLineInfo
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0002646C File Offset: 0x0002466C
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00026474 File Offset: 0x00024674
		public virtual string BaseURI
		{
			get
			{
				return this.baseURI;
			}
			set
			{
				this.baseURI = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00026480 File Offset: 0x00024680
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00026488 File Offset: 0x00024688
		public bool IsInternalSubset
		{
			get
			{
				return this.isInternalSubset;
			}
			set
			{
				this.isInternalSubset = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00026494 File Offset: 0x00024694
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0002649C File Offset: 0x0002469C
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x000264A8 File Offset: 0x000246A8
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x000264B0 File Offset: 0x000246B0
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000264BC File Offset: 0x000246BC
		public bool HasLineInfo()
		{
			return this.lineNumber != 0;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000264CC File Offset: 0x000246CC
		internal void SetRoot(DTDObjectModel root)
		{
			this.root = root;
			if (this.baseURI == null)
			{
				this.BaseURI = root.BaseURI;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x000264EC File Offset: 0x000246EC
		protected DTDObjectModel Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000264F4 File Offset: 0x000246F4
		internal XmlException NotWFError(string message)
		{
			return new XmlException(this, this.BaseURI, message);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00026504 File Offset: 0x00024704
		public void SetLineInfo(XmlSchemaObject obj)
		{
			obj.SourceUri = this.BaseURI;
			obj.LineNumber = this.LineNumber;
			obj.LinePosition = this.LinePosition;
		}

		// Token: 0x040003FB RID: 1019
		private DTDObjectModel root;

		// Token: 0x040003FC RID: 1020
		private bool isInternalSubset;

		// Token: 0x040003FD RID: 1021
		private string baseURI;

		// Token: 0x040003FE RID: 1022
		private int lineNumber;

		// Token: 0x040003FF RID: 1023
		private int linePosition;
	}
}
