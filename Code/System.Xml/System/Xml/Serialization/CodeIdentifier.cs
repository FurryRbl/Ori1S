using System;
using System.Globalization;

namespace System.Xml.Serialization
{
	/// <summary>Provides static methods to convert input text into names for code entities.</summary>
	// Token: 0x02000255 RID: 597
	public class CodeIdentifier
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.CodeIdentifier" /> class. </summary>
		// Token: 0x0600182A RID: 6186 RVA: 0x0007A024 File Offset: 0x00078224
		[Obsolete("Design mistake. It only contains static methods.")]
		public CodeIdentifier()
		{
		}

		/// <summary>Produces a camel-case string from an input string. </summary>
		/// <returns>A camel-case version of the parameter string.</returns>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		// Token: 0x0600182B RID: 6187 RVA: 0x0007A02C File Offset: 0x0007822C
		public static string MakeCamel(string identifier)
		{
			string text = CodeIdentifier.MakeValid(identifier);
			return char.ToLower(text[0], CultureInfo.InvariantCulture) + text.Substring(1);
		}

		/// <summary>Produces a Pascal-case string from an input string. </summary>
		/// <returns>A Pascal-case version of the parameter string.</returns>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		// Token: 0x0600182C RID: 6188 RVA: 0x0007A064 File Offset: 0x00078264
		public static string MakePascal(string identifier)
		{
			string text = CodeIdentifier.MakeValid(identifier);
			return char.ToUpper(text[0], CultureInfo.InvariantCulture) + text.Substring(1);
		}

		/// <summary>Produces a valid code entity name from an input string. </summary>
		/// <returns>A string that can be used as a code identifier, such as the name of a method parameter.</returns>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		// Token: 0x0600182D RID: 6189 RVA: 0x0007A09C File Offset: 0x0007829C
		public static string MakeValid(string identifier)
		{
			if (identifier == null)
			{
				throw new NullReferenceException();
			}
			if (identifier.Length == 0)
			{
				return "Item";
			}
			string text = string.Empty;
			if (!char.IsLetter(identifier[0]) && identifier[0] != '_')
			{
				text = "Item";
			}
			foreach (char c in identifier)
			{
				if (char.IsLetterOrDigit(c) || c == '_')
				{
					text += c;
				}
			}
			if (text.Length > 400)
			{
				text = text.Substring(0, 400);
			}
			return text;
		}
	}
}
