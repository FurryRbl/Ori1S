﻿using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides access to the designer options located on the Tools menu under the Options command in the Visual Studio .NET development environment.</summary>
	// Token: 0x02000113 RID: 275
	public interface IDesignerOptionService
	{
		/// <summary>Gets the value of the specified Windows Forms Designer option.</summary>
		/// <returns>The value of the specified option.</returns>
		/// <param name="pageName">The name of the page that defines the option. </param>
		/// <param name="valueName">The name of the option property. </param>
		// Token: 0x06000AE6 RID: 2790
		object GetOptionValue(string pageName, string valueName);

		/// <summary>Sets the value of the specified Windows Forms Designer option.</summary>
		/// <param name="pageName">The name of the page that defines the option. </param>
		/// <param name="valueName">The name of the option property. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x06000AE7 RID: 2791
		void SetOptionValue(string pageName, string valueName, object value);
	}
}
