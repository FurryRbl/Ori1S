using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001DD RID: 477
	internal class XsdBase64Binary : XsdString
	{
		// Token: 0x060012DC RID: 4828 RVA: 0x000527AC File Offset: 0x000509AC
		internal XsdBase64Binary()
		{
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000527B4 File Offset: 0x000509B4
		static XsdBase64Binary()
		{
			int length = XsdBase64Binary.ALPHABET.Length;
			XsdBase64Binary.decodeTable = new byte[123];
			for (int i = 0; i < XsdBase64Binary.decodeTable.Length; i++)
			{
				XsdBase64Binary.decodeTable[i] = byte.MaxValue;
			}
			for (int j = 0; j < length; j++)
			{
				char c = XsdBase64Binary.ALPHABET[j];
				XsdBase64Binary.decodeTable[(int)c] = (byte)j;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00052830 File Offset: 0x00050A30
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Base64Binary;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00052834 File Offset: 0x00050A34
		public override Type ValueType
		{
			get
			{
				return typeof(byte[]);
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00052840 File Offset: 0x00050A40
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			byte[] bytes = new ASCIIEncoding().GetBytes(s);
			FromBase64Transform fromBase64Transform = new FromBase64Transform();
			return fromBase64Transform.TransformFinalBlock(bytes, 0, bytes.Length);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0005286C File Offset: 0x00050A6C
		internal override int Length(string s)
		{
			int num = 0;
			int num2 = 0;
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if (!char.IsWhiteSpace(c))
				{
					if (XsdBase64Binary.isData(c))
					{
						num++;
					}
					else
					{
						if (!XsdBase64Binary.isPad(c))
						{
							return -1;
						}
						num2++;
					}
				}
			}
			if (num2 > 2)
			{
				return -1;
			}
			if (num2 > 0)
			{
				num2 = 3 - num2;
			}
			return num / 4 * 3 + num2;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000528F4 File Offset: 0x00050AF4
		protected static bool isPad(char octect)
		{
			return octect == '=';
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000528FC File Offset: 0x00050AFC
		protected static bool isData(char octect)
		{
			return octect <= 'z' && XsdBase64Binary.decodeTable[(int)octect] != byte.MaxValue;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0005291C File Offset: 0x00050B1C
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringValueType(this.ParseValue(s, nameTable, nsmgr) as string);
		}

		// Token: 0x04000776 RID: 1910
		private static string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

		// Token: 0x04000777 RID: 1911
		private static byte[] decodeTable;
	}
}
