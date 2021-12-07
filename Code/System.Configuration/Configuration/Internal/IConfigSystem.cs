using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support the initialization of configuration properties.</summary>
	// Token: 0x02000006 RID: 6
	public interface IConfigSystem
	{
		/// <summary>Gets the configuration host.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object that is used by the .NET Framework to initialize application configuration properties.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50
		IInternalConfigHost Host { get; }

		/// <summary>Gets the root of the configuration hierarchy.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51
		IInternalConfigRoot Root { get; }

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="typeConfigHost">The type of configuration host.</param>
		/// <param name="hostInitParams">An array of configuration host parameters.</param>
		// Token: 0x06000034 RID: 52
		void Init(Type typeConfigHost, params object[] hostInitParams);
	}
}
