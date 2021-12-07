using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.</summary>
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComVisibleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the ComVisibleAttribute class.</summary>
		/// <param name="visibility">true to indicate that the type is visible to COM; otherwise, false. The default is true. </param>
		// Token: 0x0600008A RID: 138 RVA: 0x00003554 File Offset: 0x00001754
		public ComVisibleAttribute(bool visibility)
		{
			this.Visible = visibility;
		}

		/// <summary>Gets a value that indicates whether the COM type is visible.</summary>
		/// <returns>true if the type is visible; otherwise, false. The default value is true.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003564 File Offset: 0x00001764
		public bool Value
		{
			get
			{
				return this.Visible;
			}
		}

		// Token: 0x04000007 RID: 7
		private bool Visible;
	}
}
