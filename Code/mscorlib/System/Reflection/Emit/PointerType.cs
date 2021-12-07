using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002CE RID: 718
	internal class PointerType : DerivedType
	{
		// Token: 0x06002458 RID: 9304 RVA: 0x0008202C File Offset: 0x0008022C
		internal PointerType(Type elementType) : base(elementType)
		{
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00082038 File Offset: 0x00080238
		protected override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0008203C File Offset: 0x0008023C
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00082048 File Offset: 0x00080248
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "*";
		}
	}
}
