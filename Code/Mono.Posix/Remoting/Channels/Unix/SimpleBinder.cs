using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007A RID: 122
	internal class SimpleBinder : SerializationBinder
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x0000DE40 File Offset: 0x0000C040
		public override Type BindToType(string assemblyName, string typeName)
		{
			Assembly assembly;
			if (assemblyName.IndexOf(',') != -1)
			{
				try
				{
					assembly = Assembly.Load(assemblyName);
					if (assembly == null)
					{
						return null;
					}
					Type type = assembly.GetType(typeName);
					if (type != null)
					{
						return type;
					}
				}
				catch
				{
				}
			}
			assembly = Assembly.LoadWithPartialName(assemblyName);
			if (assembly == null)
			{
				return null;
			}
			return assembly.GetType(typeName, true);
		}

		// Token: 0x04000427 RID: 1063
		public static SimpleBinder Instance = new SimpleBinder();
	}
}
