using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x0200020C RID: 524
	internal sealed class AlphabeticalEnumConverter : System.ComponentModel.EnumConverter
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x0002EB94 File Offset: 0x0002CD94
		public AlphabeticalEnumConverter(Type type) : base(type)
		{
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
		[MonoTODO("Create sorted standart values")]
		public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
		{
			return base.Values;
		}
	}
}
