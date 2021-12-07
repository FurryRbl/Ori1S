using System;

namespace System.Xml.Schema
{
	/// <summary>Provides schema compilation options for the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class This class cannot be inherited.</summary>
	// Token: 0x02000203 RID: 515
	public sealed class XmlSchemaCompilationSettings
	{
		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations; otherwise, false. The default is true.</returns>
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00059EA4 File Offset: 0x000580A4
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x00059EAC File Offset: 0x000580AC
		public bool EnableUpaCheck
		{
			get
			{
				return this.enable_upa_check;
			}
			set
			{
				this.enable_upa_check = value;
			}
		}

		// Token: 0x040007DE RID: 2014
		private bool enable_upa_check = true;
	}
}
