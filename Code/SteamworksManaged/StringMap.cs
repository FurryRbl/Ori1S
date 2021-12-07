using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ManagedSteam
{
	// Token: 0x0200016B RID: 363
	internal static class StringMap
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x000104F7 File Offset: 0x0000E6F7
		private static CultureInfo Culture
		{
			get
			{
				return CultureInfo.InvariantCulture;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000104FE File Offset: 0x0000E6FE
		private static string SafeGetString(StringID id)
		{
			if (StringMap.strings.ContainsKey(id))
			{
				return StringMap.strings[id];
			}
			return id.ToString();
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00010524 File Offset: 0x0000E724
		public static string GetString(StringID id, params object[] variables)
		{
			return StringMap.GetString(StringMap.SafeGetString(id), variables);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00010532 File Offset: 0x0000E732
		private static string SafeGetString(ErrorCodes id)
		{
			if (StringMap.errorStrings.ContainsKey(id))
			{
				return StringMap.errorStrings[id];
			}
			return id.ToString();
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00010558 File Offset: 0x0000E758
		public static string GetString(ErrorCodes id, params object[] variables)
		{
			return StringMap.GetString(StringMap.SafeGetString(id), variables);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00010568 File Offset: 0x0000E768
		private static string GetString(string message, params object[] variables)
		{
			if (variables == null)
			{
				return message;
			}
			string[] array = message.Split(new char[]
			{
				'%'
			});
			StringBuilder stringBuilder = new StringBuilder(message.Length + (array.Length - 1) * 4);
			int num = 0;
			foreach (string value in array)
			{
				stringBuilder.Append(value);
				if (num++ < variables.Length)
				{
					stringBuilder.AppendFormat(StringMap.Culture, "{0}", new object[]
					{
						variables[num - 1]
					});
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400064D RID: 1613
		private const char VariableToken = '%';

		// Token: 0x0400064E RID: 1614
		private static Dictionary<StringID, string> strings = new Dictionary<StringID, string>
		{
			{
				StringID.OnlyOneInstance,
				"Can only have one instance of the % class."
			}
		};

		// Token: 0x0400064F RID: 1615
		private static Dictionary<ErrorCodes, string> errorStrings = new Dictionary<ErrorCodes, string>
		{
			{
				ErrorCodes.Ok,
				"Unexpected error."
			},
			{
				ErrorCodes.UsageAfterAPIShutdown,
				"Can't use Steam features after the API has been shutdown."
			},
			{
				ErrorCodes.InvalidInterfaceVersion,
				"Invalid interface version. This is caused by a mismatch between dll versions. Make sure that all dll files is from the same package. Restart Unity and reimport the package from the asset store. Native %, Managed %."
			},
			{
				ErrorCodes.CallbackStructSizeMissmatch,
				"Mismatch of structure size for struct %. Please report this as it is not an usage error."
			},
			{
				ErrorCodes.NotAvailableInLite,
				"This method/operation is not available in Lite mode."
			},
			{
				ErrorCodes.NoCallbackEvent,
				"No callback handler for event %! Please report this as it is not an usage error."
			},
			{
				ErrorCodes.NoResultEvent,
				"No result handler for event %! Please report this as it is not an usage error."
			},
			{
				ErrorCodes.CantChangeEncoding,
				"Can't change the encoding of a native string."
			},
			{
				ErrorCodes.SteamInstanceIsNull,
				"No Steam object exist. Use Steam.Initialize() to create one."
			},
			{
				ErrorCodes.MatchmakingServersIsNull,
				"No MatchmakingServers object exist. Use Steam.Initialize() to initialize the plugin and one will be created."
			},
			{
				ErrorCodes.StringIsToBig,
				"The string is too big. Maximum allowed size in bytes, including null terminator, is %"
			}
		};
	}
}
