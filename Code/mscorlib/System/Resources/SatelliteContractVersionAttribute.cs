using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Instructs the <see cref="T:System.Resources.ResourceManager" /> to ask for a particular version of a satellite assembly to simplify updates of the main assembly of an application.</summary>
	// Token: 0x02000311 RID: 785
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> class.</summary>
		/// <param name="version">A <see cref="T:System.String" /> with the version of the satellite assemblies to load. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="version" /> parameter is null. </exception>
		// Token: 0x0600284E RID: 10318 RVA: 0x00090B4C File Offset: 0x0008ED4C
		public SatelliteContractVersionAttribute(string version)
		{
			this.ver = new Version(version);
		}

		/// <summary>Gets the version of the satellite assemblies with the required resources.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the version of the satellite assemblies with the required resources.</returns>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x00090B60 File Offset: 0x0008ED60
		public string Version
		{
			get
			{
				return this.ver.ToString();
			}
		}

		// Token: 0x04001045 RID: 4165
		private Version ver;
	}
}
