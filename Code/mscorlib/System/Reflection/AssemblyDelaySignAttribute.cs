using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies that the assembly is not fully signed when created.</summary>
	// Token: 0x02000275 RID: 629
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDelaySignAttribute" /> class.</summary>
		/// <param name="delaySign">true if the feature this attribute represents is activated; otherwise, false. </param>
		// Token: 0x060020C2 RID: 8386 RVA: 0x00077CE4 File Offset: 0x00075EE4
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.delay = delaySign;
		}

		/// <summary>Gets a value indicating the state of the attribute.</summary>
		/// <returns>true if this assembly has been built as delay-signed; otherwise, false.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x00077CF4 File Offset: 0x00075EF4
		public bool DelaySign
		{
			get
			{
				return this.delay;
			}
		}

		// Token: 0x04000C03 RID: 3075
		private bool delay;
	}
}
