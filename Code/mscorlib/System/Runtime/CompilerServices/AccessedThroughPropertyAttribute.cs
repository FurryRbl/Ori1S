using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the name of the property that accesses the attributed field.</summary>
	// Token: 0x02000320 RID: 800
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AccessedThroughPropertyAttribute class with the name of the property used to access the attributed field.</summary>
		/// <param name="propertyName">The name of the property used to access the attributed field. </param>
		// Token: 0x06002893 RID: 10387 RVA: 0x00091CF0 File Offset: 0x0008FEF0
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.name = propertyName;
		}

		/// <summary>Gets the name of the property used to access the attributed field.</summary>
		/// <returns>The name of the property used to access the attributed field.</returns>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x00091D00 File Offset: 0x0008FF00
		public string PropertyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04001083 RID: 4227
		private string name;
	}
}
