using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines if and how a member is displayed in the debugger variable windows. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DE RID: 478
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerBrowsableAttribute" /> class. </summary>
		/// <param name="state">One of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values that specifies how to display the member.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="state" /> is not one of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values.</exception>
		// Token: 0x06001860 RID: 6240 RVA: 0x0005D5A4 File Offset: 0x0005B7A4
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			this.state = state;
		}

		/// <summary>Gets the display state for the attribute.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
		public DebuggerBrowsableState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x040008E3 RID: 2275
		private DebuggerBrowsableState state;
	}
}
