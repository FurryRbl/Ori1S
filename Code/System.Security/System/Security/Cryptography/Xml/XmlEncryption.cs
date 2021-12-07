using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000061 RID: 97
	internal class XmlEncryption
	{
		// Token: 0x04000156 RID: 342
		public const string Prefix = "xenc";

		// Token: 0x02000062 RID: 98
		public class ElementNames
		{
			// Token: 0x04000157 RID: 343
			public const string CarriedKeyName = "CarriedKeyName";

			// Token: 0x04000158 RID: 344
			public const string CipherData = "CipherData";

			// Token: 0x04000159 RID: 345
			public const string CipherReference = "CipherReference";

			// Token: 0x0400015A RID: 346
			public const string CipherValue = "CipherValue";

			// Token: 0x0400015B RID: 347
			public const string DataReference = "DataReference";

			// Token: 0x0400015C RID: 348
			public const string EncryptedData = "EncryptedData";

			// Token: 0x0400015D RID: 349
			public const string EncryptedKey = "EncryptedKey";

			// Token: 0x0400015E RID: 350
			public const string EncryptionMethod = "EncryptionMethod";

			// Token: 0x0400015F RID: 351
			public const string EncryptionProperties = "EncryptionProperties";

			// Token: 0x04000160 RID: 352
			public const string EncryptionProperty = "EncryptionProperty";

			// Token: 0x04000161 RID: 353
			public const string KeyReference = "KeyReference";

			// Token: 0x04000162 RID: 354
			public const string KeySize = "KeySize";

			// Token: 0x04000163 RID: 355
			public const string ReferenceList = "ReferenceList";

			// Token: 0x04000164 RID: 356
			public const string Transforms = "Transforms";
		}

		// Token: 0x02000063 RID: 99
		public class AttributeNames
		{
			// Token: 0x04000165 RID: 357
			public const string Algorithm = "Algorithm";

			// Token: 0x04000166 RID: 358
			public const string Encoding = "Encoding";

			// Token: 0x04000167 RID: 359
			public const string Id = "Id";

			// Token: 0x04000168 RID: 360
			public const string MimeType = "MimeType";

			// Token: 0x04000169 RID: 361
			public const string Recipient = "Recipient";

			// Token: 0x0400016A RID: 362
			public const string Target = "Target";

			// Token: 0x0400016B RID: 363
			public const string Type = "Type";

			// Token: 0x0400016C RID: 364
			public const string URI = "URI";
		}
	}
}
