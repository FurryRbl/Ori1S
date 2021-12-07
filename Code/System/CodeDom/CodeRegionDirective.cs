using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Specifies the name and mode for a code region.</summary>
	// Token: 0x0200005A RID: 90
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class with default values. </summary>
		// Token: 0x06000300 RID: 768 RVA: 0x0000CA38 File Offset: 0x0000AC38
		public CodeRegionDirective()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class, specifying its mode and name. </summary>
		/// <param name="regionMode">One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values.</param>
		/// <param name="regionText">The name for the region.</param>
		// Token: 0x06000301 RID: 769 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public CodeRegionDirective(CodeRegionMode regionMode, string regionText)
		{
			this.regionMode = regionMode;
			this.regionText = regionText;
		}

		/// <summary>Gets or sets the mode for the region directive.</summary>
		/// <returns>One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values. The default is <see cref="F:System.CodeDom.CodeRegionMode.None" />.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000CA58 File Offset: 0x0000AC58
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public CodeRegionMode RegionMode
		{
			get
			{
				return this.regionMode;
			}
			set
			{
				this.regionMode = value;
			}
		}

		/// <summary>Gets or sets the name of the region.</summary>
		/// <returns>The name of the region.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public string RegionText
		{
			get
			{
				if (this.regionText == null)
				{
					return string.Empty;
				}
				return this.regionText;
			}
			set
			{
				this.regionText = value;
			}
		}

		// Token: 0x040000E6 RID: 230
		private CodeRegionMode regionMode;

		// Token: 0x040000E7 RID: 231
		private string regionText;
	}
}
