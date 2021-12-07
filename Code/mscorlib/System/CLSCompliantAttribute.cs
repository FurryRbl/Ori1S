using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates whether a program element is compliant with the Common Language Specification (CLS). This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000011 RID: 17
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.All)]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.CLSCompliantAttribute" /> class with a Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <param name="isCompliant">true if CLS-compliant; otherwise, false. </param>
		// Token: 0x060000CF RID: 207 RVA: 0x000049FC File Offset: 0x00002BFC
		public CLSCompliantAttribute(bool isCompliant)
		{
			this.is_compliant = isCompliant;
		}

		/// <summary>Gets the Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <returns>true if the program element is CLS-compliant; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004A0C File Offset: 0x00002C0C
		public bool IsCompliant
		{
			get
			{
				return this.is_compliant;
			}
		}

		// Token: 0x0400000E RID: 14
		private bool is_compliant;
	}
}
