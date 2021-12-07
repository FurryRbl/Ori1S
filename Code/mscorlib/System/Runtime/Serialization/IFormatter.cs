using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides functionality for formatting serialized objects.</summary>
	// Token: 0x020004F2 RID: 1266
	[ComVisible(true)]
	public interface IFormatter
	{
		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</returns>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060032DD RID: 13021
		// (set) Token: 0x060032DE RID: 13022
		SerializationBinder Binder { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</returns>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060032DF RID: 13023
		// (set) Token: 0x060032E0 RID: 13024
		StreamingContext Context { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by this formatter.</returns>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060032E1 RID: 13025
		// (set) Token: 0x060032E2 RID: 13026
		ISurrogateSelector SurrogateSelector { get; set; }

		/// <summary>Deserializes the data on the provided stream and reconstitutes the graph of objects.</summary>
		/// <returns>The top object of the deserialized graph.</returns>
		/// <param name="serializationStream">The stream that contains the data to deserialize. </param>
		// Token: 0x060032E3 RID: 13027
		object Deserialize(Stream serializationStream);

		/// <summary>Serializes an object, or graph of objects with the given root to the provided stream.</summary>
		/// <param name="serializationStream">The stream where the formatter puts the serialized data. This stream can reference a variety of backing stores (such as files, network, memory, and so on). </param>
		/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized. </param>
		// Token: 0x060032E4 RID: 13028
		void Serialize(Stream serializationStream, object graph);
	}
}
