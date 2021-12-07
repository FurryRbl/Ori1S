using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Indicates to compilers that a method call or attribute should be ignored unless a specified conditional compilation symbol is defined.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ConditionalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConditionalAttribute" /> class.</summary>
		/// <param name="conditionString">A string that specifies the case-sensitive conditional compilation symbol that is associated with the attribute. </param>
		// Token: 0x06000660 RID: 1632 RVA: 0x00014AD0 File Offset: 0x00012CD0
		public ConditionalAttribute(string conditionString)
		{
			this.myCondition = conditionString;
		}

		/// <summary>Gets the conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</summary>
		/// <returns>A string that specifies the case-sensitive conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00014AE0 File Offset: 0x00012CE0
		public string ConditionString
		{
			get
			{
				return this.myCondition;
			}
		}

		// Token: 0x0400009A RID: 154
		private string myCondition;
	}
}
