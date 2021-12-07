using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Indicates a serialization surrogate selector class.</summary>
	// Token: 0x020004F6 RID: 1270
	[ComVisible(true)]
	public interface ISurrogateSelector
	{
		/// <summary>Specifies the next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> for surrogates to examine if the current instance does not have a surrogate for the specified type and assembly in the specified context.</summary>
		/// <param name="selector">The next surrogate selector to examine. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060032F9 RID: 13049
		void ChainSelector(ISurrogateSelector selector);

		/// <summary>Returns the next surrogate selector in the chain.</summary>
		/// <returns>The next surrogate selector in the chain or null.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060032FA RID: 13050
		ISurrogateSelector GetNextSelector();

		/// <summary>Finds the surrogate that represents the specified object's type, starting with the specified surrogate selector for the specified serialization context.</summary>
		/// <returns>The appropriate surrogate for the given type in the given context.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of object (class) that needs a surrogate. </param>
		/// <param name="context">The source or destination context for the current serialization. </param>
		/// <param name="selector">When this method returns, contains a <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that holds a reference to the surrogate selector where the appropriate surrogate was found. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060032FB RID: 13051
		ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);
	}
}
