using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that a method's unmanaged signature expects a locale identifier (LCID) parameter.</summary>
	// Token: 0x020003A9 RID: 937
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the LCIDConversionAttribute class with the position of the LCID in the unmanaged signature.</summary>
		/// <param name="lcid">Indicates the position of the LCID argument in the unmanaged signature, where 0 is the first argument. </param>
		// Token: 0x06002A9E RID: 10910 RVA: 0x00092A3C File Offset: 0x00090C3C
		public LCIDConversionAttribute(int lcid)
		{
			this.id = lcid;
		}

		/// <summary>Gets the position of the LCID argument in the unmanaged signature.</summary>
		/// <returns>The position of the LCID argument in the unmanaged signature, where 0 is the first argument.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00092A4C File Offset: 0x00090C4C
		public int Value
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04001154 RID: 4436
		private int id;
	}
}
