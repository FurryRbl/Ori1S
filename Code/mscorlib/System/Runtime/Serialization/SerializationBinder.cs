using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Allows users to control class loading and mandate what class to load.</summary>
	// Token: 0x02000506 RID: 1286
	[ComVisible(true)]
	[Serializable]
	public abstract class SerializationBinder
	{
		/// <summary>When overridden in a derived class, controls the binding of a serialized object to a type.</summary>
		/// <returns>The type of the object the formatter creates a new instance of.</returns>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object. </param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object. </param>
		// Token: 0x06003335 RID: 13109
		public abstract Type BindToType(string assemblyName, string typeName);
	}
}
