using System;

namespace UnityEngineInternal
{
	// Token: 0x0200033B RID: 827
	[AttributeUsage(AttributeTargets.Method)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x0600284B RID: 10315 RVA: 0x0003A010 File Offset: 0x00038210
		public TypeInferenceRuleAttribute(TypeInferenceRules rule) : this(rule.ToString())
		{
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0003A024 File Offset: 0x00038224
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0003A034 File Offset: 0x00038234
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x04000C6C RID: 3180
		private readonly string _rule;
	}
}
