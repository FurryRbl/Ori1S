using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002D0 RID: 720
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x060025E0 RID: 9696 RVA: 0x00034854 File Offset: 0x00032A54
		public LocalizationRequiredAttribute() : this(true)
		{
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00034860 File Offset: 0x00032A60
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x00034870 File Offset: 0x00032A70
		// (set) Token: 0x060025E3 RID: 9699 RVA: 0x00034878 File Offset: 0x00032A78
		public bool Required { get; private set; }
	}
}
