using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002D2 RID: 722
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[BaseTypeRequired(typeof(Attribute))]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x060025E5 RID: 9701 RVA: 0x0003488C File Offset: 0x00032A8C
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x0003489C File Offset: 0x00032A9C
		// (set) Token: 0x060025E7 RID: 9703 RVA: 0x000348A4 File Offset: 0x00032AA4
		[NotNull]
		public Type BaseType { get; private set; }
	}
}
