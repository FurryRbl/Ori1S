using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces used by internal .NET structures to support a configuration root object.</summary>
	// Token: 0x0200000D RID: 13
	[ComVisible(false)]
	public interface IInternalConfigRoot
	{
		/// <summary>Represents the method that handles the <see cref="E:System.Configuration.Internal.IInternalConfigRoot.ConfigChanged" /> event of an <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007A RID: 122
		// (remove) Token: 0x0600007B RID: 123
		event InternalConfigEventHandler ConfigChanged;

		/// <summary>Represents the method that handles the <see cref="E:System.Configuration.Internal.IInternalConfigRoot.ConfigRemoved" /> event of a <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600007C RID: 124
		// (remove) Token: 0x0600007D RID: 125
		event InternalConfigEventHandler ConfigRemoved;

		/// <summary>Returns an <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a configuration specified by a configuration path.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a configuration specified by <paramref name="configPath" />.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x0600007E RID: 126
		IInternalConfigRecord GetConfigRecord(string configPath);

		/// <summary>Returns an <see cref="T:System.Object" /> representing the data in a section of a configuration file.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data in a section of a configuration file.</returns>
		/// <param name="section">A string representing a section of a configuration file.</param>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x0600007F RID: 127
		object GetSection(string section, string configPath);

		/// <summary>Returns a value representing the file path of the nearest configuration ancestor that has configuration data.</summary>
		/// <returns>Returns a string representing the file path of the nearest configuration ancestor that has configuration data.</returns>
		/// <param name="configPath">The path of configuration file.</param>
		// Token: 0x06000080 RID: 128
		string GetUniqueConfigPath(string configPath);

		/// <summary>Returns an <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a unique configuration record for given configuration path.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a unique configuration record for a given configuration path.</returns>
		/// <param name="configPath">The path of the configuration file.</param>
		// Token: 0x06000081 RID: 129
		IInternalConfigRecord GetUniqueConfigRecord(string configPath);

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="host">An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</param>
		/// <param name="isDesignTime">true if design time; false if run time.</param>
		// Token: 0x06000082 RID: 130
		void Init(IInternalConfigHost host, bool isDesignTime);

		/// <summary>Returns a value indicating whether the configuration is a design-time configuration.</summary>
		/// <returns>true if the configuration is a design-time configuration; false if the configuration is not a design-time configuration.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000083 RID: 131
		bool IsDesignTime { get; }

		/// <summary>Finds and removes a configuration record and all its children for a given configuration path.</summary>
		/// <param name="configPath">The path of the configuration file.</param>
		// Token: 0x06000084 RID: 132
		void RemoveConfig(string configPath);
	}
}
