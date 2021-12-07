using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the usage of another attribute class. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200000C RID: 12
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AttributeUsageAttribute" /> class with the specified list of <see cref="T:System.AttributeTargets" />, the <see cref="P:System.AttributeUsageAttribute.AllowMultiple" /> value, and the <see cref="P:System.AttributeUsageAttribute.Inherited" /> value.</summary>
		/// <param name="validOn">The set of values combined using a bitwise OR operation to indicate which program elements are valid. </param>
		// Token: 0x06000084 RID: 132 RVA: 0x0000350C File Offset: 0x0000170C
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this.valid_on = validOn;
		}

		/// <summary>Gets or sets a Boolean value indicating whether more than one instance of the indicated attribute can be specified for a single program element.</summary>
		/// <returns>true if more than one instance is allowed to be specified; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003524 File Offset: 0x00001724
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000352C File Offset: 0x0000172C
		public bool AllowMultiple
		{
			get
			{
				return this.allow_multiple;
			}
			set
			{
				this.allow_multiple = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the indicated attribute can be inherited by derived classes and overriding members.</summary>
		/// <returns>true if the attribute can be inherited by derived classes and overriding members; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003538 File Offset: 0x00001738
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003540 File Offset: 0x00001740
		public bool Inherited
		{
			get
			{
				return this.inherited;
			}
			set
			{
				this.inherited = value;
			}
		}

		/// <summary>Gets a set of values identifying which program elements that the indicated attribute can be applied to.</summary>
		/// <returns>One or several <see cref="T:System.AttributeTargets" /> values. The default is All.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000354C File Offset: 0x0000174C
		public AttributeTargets ValidOn
		{
			get
			{
				return this.valid_on;
			}
		}

		// Token: 0x04000004 RID: 4
		private AttributeTargets valid_on;

		// Token: 0x04000005 RID: 5
		private bool allow_multiple;

		// Token: 0x04000006 RID: 6
		private bool inherited = true;
	}
}
