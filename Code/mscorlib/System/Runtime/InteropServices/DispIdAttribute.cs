using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the COM dispatch identifier (DISPID) of a method, field, or property.</summary>
	// Token: 0x02000387 RID: 903
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	public sealed class DispIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the DispIdAttribute class with the specified DISPID.</summary>
		/// <param name="dispId">The DISPID for the member. </param>
		// Token: 0x06002A4A RID: 10826 RVA: 0x00092570 File Offset: 0x00090770
		public DispIdAttribute(int dispId)
		{
			this.id = dispId;
		}

		/// <summary>Gets the DISPID for the member.</summary>
		/// <returns>The DISPID for the member.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x00092580 File Offset: 0x00090780
		public int Value
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x040010F8 RID: 4344
		private int id;
	}
}
