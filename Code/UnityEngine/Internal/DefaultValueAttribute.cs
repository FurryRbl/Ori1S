using System;

namespace UnityEngine.Internal
{
	// Token: 0x02000328 RID: 808
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x060027F8 RID: 10232 RVA: 0x00039270 File Offset: 0x00037470
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x00039280 File Offset: 0x00037480
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00039288 File Offset: 0x00037488
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.DefaultValue == null)
			{
				return defaultValueAttribute.Value == null;
			}
			return this.DefaultValue.Equals(defaultValueAttribute.Value);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000392CC File Offset: 0x000374CC
		public override int GetHashCode()
		{
			if (this.DefaultValue == null)
			{
				return base.GetHashCode();
			}
			return this.DefaultValue.GetHashCode();
		}

		// Token: 0x04000C51 RID: 3153
		private object DefaultValue;
	}
}
