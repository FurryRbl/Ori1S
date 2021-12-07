using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Holds the value, <see cref="T:System.Type" />, and name of a serialized object. </summary>
	// Token: 0x02000508 RID: 1288
	[ComVisible(true)]
	public struct SerializationEntry
	{
		// Token: 0x06003343 RID: 13123 RVA: 0x000A6074 File Offset: 0x000A4274
		internal SerializationEntry(string name, Type type, object value)
		{
			this.name = name;
			this.objectType = type;
			this.value = value;
		}

		/// <summary>Gets the name of the object.</summary>
		/// <returns>The name of the object.</returns>
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000A608C File Offset: 0x000A428C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object.</returns>
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000A6094 File Offset: 0x000A4294
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		/// <summary>Gets the value contained in the object.</summary>
		/// <returns>The value contained in the object.</returns>
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000A609C File Offset: 0x000A429C
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400155B RID: 5467
		private string name;

		// Token: 0x0400155C RID: 5468
		private Type objectType;

		// Token: 0x0400155D RID: 5469
		private object value;
	}
}
