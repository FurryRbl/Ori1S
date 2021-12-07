using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for design-time license context serialization.</summary>
	// Token: 0x02000104 RID: 260
	public class DesigntimeLicenseContextSerializer
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x0001D564 File Offset: 0x0001B764
		private DesigntimeLicenseContextSerializer()
		{
		}

		/// <summary>Serializes the licenses within the specified design-time license context using the specified key and output stream.</summary>
		/// <param name="o">The stream to output to. </param>
		/// <param name="cryptoKey">The key to use for encryption. </param>
		/// <param name="context">A <see cref="T:System.ComponentModel.Design.DesigntimeLicenseContext" /> indicating the license context. </param>
		// Token: 0x06000A82 RID: 2690 RVA: 0x0001D56C File Offset: 0x0001B76C
		public static void Serialize(Stream o, string cryptoKey, DesigntimeLicenseContext context)
		{
			object[] array = new object[2];
			array[0] = cryptoKey;
			Hashtable hashtable = new Hashtable();
			foreach (object obj in context.keys)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				hashtable.Add(((Type)dictionaryEntry.Key).AssemblyQualifiedName, dictionaryEntry.Value);
			}
			array[1] = hashtable;
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(o, array);
		}
	}
}
