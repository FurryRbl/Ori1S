using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates the name by which an indexer is known in programming languages that do not support indexers directly.</summary>
	// Token: 0x02000044 RID: 68
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	[Serializable]
	public sealed class IndexerNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IndexerNameAttribute" /> class.</summary>
		/// <param name="indexerName">The name of the indexer, as shown to other languages. </param>
		// Token: 0x0600065F RID: 1631 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
