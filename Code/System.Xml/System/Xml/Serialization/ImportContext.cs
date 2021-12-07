using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Xml.Serialization
{
	/// <summary>Describes the context in which a set of schema is bound to .NET Framework code entities.</summary>
	// Token: 0x02000258 RID: 600
	public class ImportContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.ImportContext" /> class for the given code identifiers, with the given type-sharing option.</summary>
		/// <param name="identifiers">The code entities to which the context applies.</param>
		/// <param name="shareTypes">A <see cref="T:System.Boolean" /> value that determines whether custom types are shared among schema.</param>
		// Token: 0x0600183F RID: 6207 RVA: 0x0007A2EC File Offset: 0x000784EC
		public ImportContext(CodeIdentifiers identifiers, bool shareTypes)
		{
			this._typeIdentifiers = identifiers;
			this._shareTypes = shareTypes;
			if (shareTypes)
			{
				this.MappedTypes = new Hashtable();
				this.DataMappedTypes = new Hashtable();
				this.SharedAnonymousTypes = new Hashtable();
			}
		}

		/// <summary>Gets a value that determines whether custom types are shared.</summary>
		/// <returns>true, if custom types are shared among schema; otherwise, false.</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0007A340 File Offset: 0x00078540
		public bool ShareTypes
		{
			get
			{
				return this._shareTypes;
			}
		}

		/// <summary>Gets a set of code entities to which the context applies.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> that specifies the code entities to which the context applies.</returns>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0007A348 File Offset: 0x00078548
		public CodeIdentifiers TypeIdentifiers
		{
			get
			{
				return this._typeIdentifiers;
			}
		}

		/// <summary>Gets a collection of warnings that are generated when importing the code entity descriptions.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains warnings that were generated when importing the code entity descriptions.</returns>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x0007A350 File Offset: 0x00078550
		public StringCollection Warnings
		{
			get
			{
				return this._warnings;
			}
		}

		// Token: 0x04000A09 RID: 2569
		private bool _shareTypes;

		// Token: 0x04000A0A RID: 2570
		private CodeIdentifiers _typeIdentifiers;

		// Token: 0x04000A0B RID: 2571
		private StringCollection _warnings = new StringCollection();

		// Token: 0x04000A0C RID: 2572
		internal Hashtable MappedTypes;

		// Token: 0x04000A0D RID: 2573
		internal Hashtable DataMappedTypes;

		// Token: 0x04000A0E RID: 2574
		internal Hashtable SharedAnonymousTypes;
	}
}
