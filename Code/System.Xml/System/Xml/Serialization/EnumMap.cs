using System;
using System.Globalization;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x020002CA RID: 714
	internal class EnumMap : ObjectMap
	{
		// Token: 0x06001E0D RID: 7693 RVA: 0x0009D550 File Offset: 0x0009B750
		public EnumMap(EnumMap.EnumMapMember[] members, bool isFlags)
		{
			this._members = members;
			this._isFlags = isFlags;
			this._enumNames = new string[this._members.Length];
			this._xmlNames = new string[this._members.Length];
			this._values = new long[this._members.Length];
			for (int i = 0; i < this._members.Length; i++)
			{
				EnumMap.EnumMapMember enumMapMember = this._members[i];
				this._enumNames[i] = enumMapMember.EnumName;
				this._xmlNames[i] = enumMapMember.XmlName;
				this._values[i] = enumMapMember.Value;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0009D5F8 File Offset: 0x0009B7F8
		public bool IsFlags
		{
			get
			{
				return this._isFlags;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0009D600 File Offset: 0x0009B800
		public EnumMap.EnumMapMember[] Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0009D608 File Offset: 0x0009B808
		public string[] EnumNames
		{
			get
			{
				return this._enumNames;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0009D610 File Offset: 0x0009B810
		public string[] XmlNames
		{
			get
			{
				return this._xmlNames;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0009D618 File Offset: 0x0009B818
		public long[] Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0009D620 File Offset: 0x0009B820
		public string GetXmlName(string typeName, object enumValue)
		{
			if (enumValue is string)
			{
				throw new InvalidCastException();
			}
			long num = 0L;
			try
			{
				num = ((IConvertible)enumValue).ToInt64(CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				throw new InvalidCastException();
			}
			for (int i = 0; i < this.Values.Length; i++)
			{
				if (this.Values[i] == num)
				{
					return this.XmlNames[i];
				}
			}
			if (this.IsFlags && num == 0L)
			{
				return string.Empty;
			}
			string text = string.Empty;
			if (this.IsFlags)
			{
				text = XmlCustomFormatter.FromEnum(num, this.XmlNames, this.Values, typeName);
			}
			if (text.Length == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid value for {1}.", new object[]
				{
					num,
					typeName
				}));
			}
			return text;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x0009D720 File Offset: 0x0009B920
		public string GetEnumName(string typeName, string xmlName)
		{
			if (!this._isFlags)
			{
				foreach (EnumMap.EnumMapMember enumMapMember in this._members)
				{
					if (enumMapMember.XmlName == xmlName)
					{
						return enumMapMember.EnumName;
					}
				}
				return null;
			}
			xmlName = xmlName.Trim();
			if (xmlName.Length == 0)
			{
				return "0";
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = xmlName.Split(null);
			foreach (string text in array)
			{
				if (!(text == string.Empty))
				{
					string text2 = null;
					for (int k = 0; k < this.XmlNames.Length; k++)
					{
						if (this.XmlNames[k] == text)
						{
							text2 = this.EnumNames[k];
							break;
						}
					}
					if (text2 == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid value for {1}.", new object[]
						{
							text,
							typeName
						}));
					}
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(text2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BE9 RID: 3049
		private readonly EnumMap.EnumMapMember[] _members;

		// Token: 0x04000BEA RID: 3050
		private readonly bool _isFlags;

		// Token: 0x04000BEB RID: 3051
		private readonly string[] _enumNames;

		// Token: 0x04000BEC RID: 3052
		private readonly string[] _xmlNames;

		// Token: 0x04000BED RID: 3053
		private readonly long[] _values;

		// Token: 0x020002CB RID: 715
		public class EnumMapMember
		{
			// Token: 0x06001E15 RID: 7701 RVA: 0x0009D870 File Offset: 0x0009BA70
			public EnumMapMember(string xmlName, string enumName) : this(xmlName, enumName, 0L)
			{
			}

			// Token: 0x06001E16 RID: 7702 RVA: 0x0009D87C File Offset: 0x0009BA7C
			public EnumMapMember(string xmlName, string enumName, long value)
			{
				this._xmlName = xmlName;
				this._enumName = enumName;
				this._value = value;
			}

			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0009D89C File Offset: 0x0009BA9C
			public string XmlName
			{
				get
				{
					return this._xmlName;
				}
			}

			// Token: 0x17000863 RID: 2147
			// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0009D8A4 File Offset: 0x0009BAA4
			public string EnumName
			{
				get
				{
					return this._enumName;
				}
			}

			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0009D8AC File Offset: 0x0009BAAC
			public long Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000865 RID: 2149
			// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0009D8B4 File Offset: 0x0009BAB4
			// (set) Token: 0x06001E1B RID: 7707 RVA: 0x0009D8BC File Offset: 0x0009BABC
			public string Documentation
			{
				get
				{
					return this._documentation;
				}
				set
				{
					this._documentation = value;
				}
			}

			// Token: 0x04000BEE RID: 3054
			private readonly string _xmlName;

			// Token: 0x04000BEF RID: 3055
			private readonly string _enumName;

			// Token: 0x04000BF0 RID: 3056
			private readonly long _value;

			// Token: 0x04000BF1 RID: 3057
			private string _documentation;
		}
	}
}
