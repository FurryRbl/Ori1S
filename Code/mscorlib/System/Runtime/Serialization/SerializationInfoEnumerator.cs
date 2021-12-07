using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides a formatter-friendly mechanism for parsing the data in <see cref="T:System.Runtime.Serialization.SerializationInfo" />. This class cannot be inherited.</summary>
	// Token: 0x0200050B RID: 1291
	[ComVisible(true)]
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x06003376 RID: 13174 RVA: 0x000A68C0 File Offset: 0x000A4AC0
		internal SerializationInfoEnumerator(ArrayList list)
		{
			this.enumerator = list.GetEnumerator();
		}

		/// <summary>Gets the current item in the collection.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationEntry" /> that contains the current serialization data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration has not started or has already ended. </exception>
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000A68D4 File Offset: 0x000A4AD4
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Gets the item currently being examined.</summary>
		/// <returns>The item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000A68E4 File Offset: 0x000A4AE4
		public SerializationEntry Current
		{
			get
			{
				return (SerializationEntry)this.enumerator.Current;
			}
		}

		/// <summary>Gets the name for the item currently being examined.</summary>
		/// <returns>The item name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000A68F8 File Offset: 0x000A4AF8
		public string Name
		{
			get
			{
				SerializationEntry serializationEntry = this.Current;
				return serializationEntry.Name;
			}
		}

		/// <summary>Gets the type of the item currently being examined.</summary>
		/// <returns>The type of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000A6914 File Offset: 0x000A4B14
		public Type ObjectType
		{
			get
			{
				SerializationEntry serializationEntry = this.Current;
				return serializationEntry.ObjectType;
			}
		}

		/// <summary>Gets the value of the item currently being examined.</summary>
		/// <returns>The value of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000A6930 File Offset: 0x000A4B30
		public object Value
		{
			get
			{
				SerializationEntry serializationEntry = this.Current;
				return serializationEntry.Value;
			}
		}

		/// <summary>Updates the enumerator to the next item.</summary>
		/// <returns>true if a new element is found; otherwise, false.</returns>
		// Token: 0x0600337C RID: 13180 RVA: 0x000A694C File Offset: 0x000A4B4C
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Resets the enumerator to the first item.</summary>
		// Token: 0x0600337D RID: 13181 RVA: 0x000A695C File Offset: 0x000A4B5C
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x04001563 RID: 5475
		private IEnumerator enumerator;
	}
}
