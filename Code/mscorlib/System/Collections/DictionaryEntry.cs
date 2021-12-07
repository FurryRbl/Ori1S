using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Defines a dictionary key/value pair that can be set or retrieved.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001B9 RID: 441
	[ComVisible(true)]
	[DebuggerDisplay("{_value}", Name = "[{_key}]")]
	[Serializable]
	public struct DictionaryEntry
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Collections.DictionaryEntry" /> type with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair. </param>
		/// <param name="value">The definition associated with <paramref name="key" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null and the .NET Framework version is 1.0 or 1.1. </exception>
		// Token: 0x060016FC RID: 5884 RVA: 0x00059088 File Offset: 0x00057288
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		/// <summary>Gets or sets the key in the key/value pair.</summary>
		/// <returns>The key in the key/value pair.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00059098 File Offset: 0x00057298
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x000590A0 File Offset: 0x000572A0
		public object Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the value in the key/value pair.</summary>
		/// <returns>The value in the key/value pair.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x000590AC File Offset: 0x000572AC
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x000590B4 File Offset: 0x000572B4
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04000879 RID: 2169
		private object _key;

		// Token: 0x0400087A RID: 2170
		private object _value;
	}
}
