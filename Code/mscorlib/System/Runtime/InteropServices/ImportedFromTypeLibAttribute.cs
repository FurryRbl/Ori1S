using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the types defined within an assembly were originally defined in a type library.</summary>
	// Token: 0x020003A4 RID: 932
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ImportedFromTypeLibAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ImportedFromTypeLibAttribute" /> class with the name of the original type library file.</summary>
		/// <param name="tlbFile">The location of the original type library file. </param>
		// Token: 0x06002A91 RID: 10897 RVA: 0x0009294C File Offset: 0x00090B4C
		public ImportedFromTypeLibAttribute(string tlbFile)
		{
			this.TlbFile = tlbFile;
		}

		/// <summary>Gets the name of the original type library file.</summary>
		/// <returns>The name of the original type library file.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x0009295C File Offset: 0x00090B5C
		public string Value
		{
			get
			{
				return this.TlbFile;
			}
		}

		// Token: 0x0400114C RID: 4428
		private string TlbFile;
	}
}
