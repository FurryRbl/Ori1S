using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Informs the <see cref="T:System.Resources.ResourceManager" /> of the neutral culture of an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000306 RID: 774
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(true)]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class.</summary>
		/// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cultureName" /> parameter is null. </exception>
		// Token: 0x060027E6 RID: 10214 RVA: 0x0008DF34 File Offset: 0x0008C134
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("culture is null");
			}
			this.culture = cultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class with the specified ultimate resource fallback location.</summary>
		/// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in.</param>
		/// <param name="location">An <see cref="T:System.Resources.UltimateResourceFallbackLocation" /> enumeration value indicating the location from which to retrieve neutral fallback resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cultureName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="location" /> is not a member of <see cref="T:System.Resources.UltimateResourceFallbackLocation" />.</exception>
		// Token: 0x060027E7 RID: 10215 RVA: 0x0008DF54 File Offset: 0x0008C154
		public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("culture is null");
			}
			this.culture = cultureName;
			this.loc = location;
		}

		/// <summary>Gets the culture name.</summary>
		/// <returns>A <see cref="T:System.String" /> with the name of the default culture for the main assembly.</returns>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x0008DF7C File Offset: 0x0008C17C
		public string CultureName
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets the location for the <see cref="T:System.Resources.ResourceManager" /> class to use to retrieve neutral resources by using the resource fallback process.</summary>
		/// <returns>The value of the <see cref="T:System.Resources.UltimateResourceFallbackLocation" /> enumeration that indicates the location (main assembly or satellite) from which to retrieve neutral resources.</returns>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x0008DF84 File Offset: 0x0008C184
		public UltimateResourceFallbackLocation Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x04001004 RID: 4100
		private string culture;

		// Token: 0x04001005 RID: 4101
		private UltimateResourceFallbackLocation loc;
	}
}
