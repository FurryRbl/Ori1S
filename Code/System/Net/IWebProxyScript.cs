using System;

namespace System.Net
{
	/// <summary>Provides the base interface to load and execute scripts for automatic proxy detection.</summary>
	// Token: 0x0200032F RID: 815
	public interface IWebProxyScript
	{
		/// <summary>Closes a script.</summary>
		// Token: 0x06001CF4 RID: 7412
		void Close();

		/// <summary>Loads a script.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> indicating whether the script was successfully loaded.</returns>
		/// <param name="scriptLocation">Internal only.</param>
		/// <param name="script">Internal only.</param>
		/// <param name="helperType">Internal only.</param>
		// Token: 0x06001CF5 RID: 7413
		bool Load(System.Uri scriptLocation, string Script, Type helperType);

		/// <summary>Runs a script.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <param name="url">Internal only.</param>
		/// <param name="host">Internal only.</param>
		// Token: 0x06001CF6 RID: 7414
		string Run(string url, string host);
	}
}
