using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002CC RID: 716
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x060025D2 RID: 9682 RVA: 0x000347B0 File Offset: 0x000329B0
		public StringFormatMethodAttribute(string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x000347C0 File Offset: 0x000329C0
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x000347C8 File Offset: 0x000329C8
		public string FormatParameterName { get; private set; }
	}
}
