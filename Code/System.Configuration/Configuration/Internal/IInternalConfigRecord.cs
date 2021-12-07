using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces used by internal .NET structures to support creation of new configuration records.</summary>
	// Token: 0x0200000C RID: 12
	[ComVisible(false)]
	public interface IInternalConfigRecord
	{
		/// <summary>Returns an object representing a section of a configuration from the last-known-good (LKG) configuration.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance representing the section of the last-known-good configuration specified by <paramref name="configKey" />.</returns>
		/// <param name="configKey">A string representing a key to a configuration section.</param>
		// Token: 0x06000072 RID: 114
		object GetLkgSection(string configKey);

		/// <summary>Returns an <see cref="T:System.Object" /> instance representing a section of a configuration file.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance representing a section of a configuration file.</returns>
		/// <param name="configKey">A string representing a key to a configuration section.</param>
		// Token: 0x06000073 RID: 115
		object GetSection(string configKey);

		/// <summary>Causes a specified section of the configuration object to be reinitialized.</summary>
		/// <param name="configKey">A string representing a key to a configuration section that is to be refreshed.</param>
		// Token: 0x06000074 RID: 116
		void RefreshSection(string configKey);

		/// <summary>Removes a configuration record.</summary>
		// Token: 0x06000075 RID: 117
		void Remove();

		/// <summary>Grants the configuration object the permission to throw an exception if an error occurs during initialization.</summary>
		// Token: 0x06000076 RID: 118
		void ThrowIfInitErrors();

		/// <summary>Gets a string representing a configuration file path.</summary>
		/// <returns>A string representing a configuration file path.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000077 RID: 119
		string ConfigPath { get; }

		/// <summary>Returns a value indicating whether an error occurred during initialization of a configuration object.</summary>
		/// <returns>true if an error occurred during initialization of a configuration object; otherwise, false.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000078 RID: 120
		bool HasInitErrors { get; }

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</summary>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000079 RID: 121
		string StreamName { get; }
	}
}
