using System;

namespace System.Configuration
{
	/// <summary>Specifies the type of a <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object.</summary>
	// Token: 0x02000025 RID: 37
	public enum ConfigurationElementCollectionType
	{
		/// <summary>Collections of this type contain elements that apply to the level at which they are specified, and to all child levels. A child level cannot modify the properties specified by a parent element of this type.</summary>
		// Token: 0x04000075 RID: 117
		BasicMap,
		/// <summary>The default type of <see cref="T:System.Configuration.ConfigurationElementCollection" />. Collections of this type contain elements that can be merged across a hierarchy of configuration files. At any particular level within such a hierarchy, add, remove, and clear directives are used to modify any inherited properties and specify new ones.</summary>
		// Token: 0x04000076 RID: 118
		AddRemoveClearMap,
		/// <summary>Same as <see cref="F:System.Configuration.ConfigurationElementCollectionType.BasicMap" />, except that this type causes the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object to sort its contents such that inherited elements are listed last.</summary>
		// Token: 0x04000077 RID: 119
		BasicMapAlternate,
		/// <summary>Same as <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" />, except that this type causes the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object to sort its contents such that inherited elements are listed last.</summary>
		// Token: 0x04000078 RID: 120
		AddRemoveClearMapAlternate
	}
}
