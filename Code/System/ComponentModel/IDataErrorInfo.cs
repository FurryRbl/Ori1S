using System;

namespace System.ComponentModel
{
	/// <summary>Provides the functionality to offer custom error information that a user interface can bind to.</summary>
	// Token: 0x02000157 RID: 343
	public interface IDataErrorInfo
	{
		/// <summary>Gets an error message indicating what is wrong with this object.</summary>
		/// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C88 RID: 3208
		string Error { get; }

		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		/// <param name="columnName">The name of the property whose error message to get. </param>
		// Token: 0x170002D5 RID: 725
		string this[string columnName]
		{
			get;
		}
	}
}
