using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002D4 RID: 724
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x060025F0 RID: 9712 RVA: 0x00034914 File Offset: 0x00032B14
		public MeansImplicitUseAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00034920 File Offset: 0x00032B20
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0003492C File Offset: 0x00032B2C
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00034938 File Offset: 0x00032B38
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x00034950 File Offset: 0x00032B50
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x00034958 File Offset: 0x00032B58
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x00034964 File Offset: 0x00032B64
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x0003496C File Offset: 0x00032B6C
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
