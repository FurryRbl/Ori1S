using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002D3 RID: 723
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x060025E8 RID: 9704 RVA: 0x000348B0 File Offset: 0x00032AB0
		public UsedImplicitlyAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000348BC File Offset: 0x00032ABC
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000348C8 File Offset: 0x00032AC8
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000348D4 File Offset: 0x00032AD4
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000348EC File Offset: 0x00032AEC
		// (set) Token: 0x060025ED RID: 9709 RVA: 0x000348F4 File Offset: 0x00032AF4
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x00034900 File Offset: 0x00032B00
		// (set) Token: 0x060025EF RID: 9711 RVA: 0x00034908 File Offset: 0x00032B08
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
