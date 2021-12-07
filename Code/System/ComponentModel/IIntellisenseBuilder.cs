using System;

namespace System.ComponentModel
{
	/// <summary>Provides an interface to facilitate the retrieval of the builder's name and to display the builder.</summary>
	// Token: 0x0200015A RID: 346
	public interface IIntellisenseBuilder
	{
		/// <summary>Gets a localized name.</summary>
		/// <returns>A localized name.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C8E RID: 3214
		string Name { get; }

		/// <summary>Shows the builder.</summary>
		/// <returns>true if the value should be replaced with <paramref name="newValue" />; otherwise, false (if the user cancels, for example).</returns>
		/// <param name="language">The language service that is calling the builder.</param>
		/// <param name="value">The expression being edited.</param>
		/// <param name="newValue">The new value.</param>
		// Token: 0x06000C8F RID: 3215
		bool Show(string language, string value, ref string newValue);
	}
}
