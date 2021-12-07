using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002CE RID: 718
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x060025D6 RID: 9686 RVA: 0x000347DC File Offset: 0x000329DC
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000347E4 File Offset: 0x000329E4
		public NotifyPropertyChangedInvocatorAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000347F4 File Offset: 0x000329F4
		// (set) Token: 0x060025D9 RID: 9689 RVA: 0x000347FC File Offset: 0x000329FC
		public string ParameterName { get; private set; }
	}
}
