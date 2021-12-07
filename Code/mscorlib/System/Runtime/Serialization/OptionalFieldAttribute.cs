using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that a field can be missing from a serialization stream so that the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> and the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> does not throw an exception. </summary>
	// Token: 0x02000505 RID: 1285
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		/// <summary>This property is unused and is reserved.</summary>
		/// <returns>This property is reserved.</returns>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000A5D78 File Offset: 0x000A3F78
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x000A5D80 File Offset: 0x000A3F80
		public int VersionAdded
		{
			get
			{
				return this.version_added;
			}
			set
			{
				this.version_added = value;
			}
		}

		// Token: 0x04001553 RID: 5459
		private int version_added;
	}
}
