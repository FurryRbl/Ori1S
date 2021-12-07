using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Supplies an explicit <see cref="T:System.Guid" /> when an automatic GUID is undesirable.</summary>
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class GuidAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.GuidAttribute" /> class with the specified GUID.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> to be assigned. </param>
		// Token: 0x0600066A RID: 1642 RVA: 0x00014B30 File Offset: 0x00012D30
		public GuidAttribute(string guid)
		{
			this.guidValue = guid;
		}

		/// <summary>Gets the <see cref="T:System.Guid" /> of the class.</summary>
		/// <returns>The <see cref="T:System.Guid" /> of the class.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00014B40 File Offset: 0x00012D40
		public string Value
		{
			get
			{
				return this.guidValue;
			}
		}

		// Token: 0x0400009D RID: 157
		private string guidValue;
	}
}
