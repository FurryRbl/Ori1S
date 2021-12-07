using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x02000074 RID: 116
	internal sealed class PermissionHelper
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000F058 File Offset: 0x0000D258
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None)
			{
				if (state != PermissionState.Unrestricted)
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), state);
					throw new ArgumentException(message, "state");
				}
				if (!allowUnrestricted)
				{
					string message = Locale.GetText("Unrestricted isn't not allowed for identity permissions.");
					throw new ArgumentException(message, "state");
				}
			}
			return state;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Attribute("class") == null)
			{
				string text = Locale.GetText("Missing 'class' attribute.");
				throw new ArgumentException(text, parameterName);
			}
			int num = minimumVersion;
			string text2 = se.Attribute("version");
			if (text2 != null)
			{
				try
				{
					num = int.Parse(text2);
				}
				catch (Exception innerException)
				{
					string text3 = Locale.GetText("Couldn't parse version from '{0}'.");
					text3 = string.Format(text3, text2);
					throw new ArgumentException(text3, parameterName, innerException);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				string text4 = Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}'].");
				text4 = string.Format(text4, num, minimumVersion, maximumVersion);
				throw new ArgumentException(text4, parameterName);
			}
			return num;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			string text = Locale.GetText("Invalid permission type '{0}', expected type '{1}'.");
			text = string.Format(text, target.GetType(), expected);
			throw new ArgumentException(text, "target");
		}
	}
}
