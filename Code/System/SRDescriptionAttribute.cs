using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000461 RID: 1121
	[AttributeUsage(AttributeTargets.All)]
	internal class SRDescriptionAttribute : System.ComponentModel.DescriptionAttribute
	{
		// Token: 0x06002843 RID: 10307 RVA: 0x0007FC5C File Offset: 0x0007DE5C
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x0007FC68 File Offset: 0x0007DE68
		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		// Token: 0x040018D0 RID: 6352
		private bool isReplaced;
	}
}
