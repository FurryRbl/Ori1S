using System;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	/// <summary>Describes a schema importer.</summary>
	// Token: 0x0200025C RID: 604
	public abstract class SchemaImporter
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x0007BC4C File Offset: 0x00079E4C
		internal SchemaImporter()
		{
		}

		/// <summary>Gets a collection of schema importer extensions.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection" /> containing a collection of schema importer extensions.</returns>
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0007BC54 File Offset: 0x00079E54
		public SchemaImporterExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new SchemaImporterExtensionCollection();
				}
				return this.extensions;
			}
		}

		// Token: 0x04000A1C RID: 2588
		private SchemaImporterExtensionCollection extensions;
	}
}
