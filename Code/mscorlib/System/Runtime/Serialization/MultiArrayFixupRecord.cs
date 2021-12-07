using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004FC RID: 1276
	internal class MultiArrayFixupRecord : BaseFixupRecord
	{
		// Token: 0x0600331A RID: 13082 RVA: 0x000A5740 File Offset: 0x000A3940
		public MultiArrayFixupRecord(ObjectRecord objectToBeFixed, int[] indices, ObjectRecord objectRequired) : base(objectToBeFixed, objectRequired)
		{
			this._indices = indices;
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000A5754 File Offset: 0x000A3954
		protected override void FixupImpl(ObjectManager manager)
		{
			this.ObjectToBeFixed.SetArrayValue(manager, this.ObjectRequired.ObjectInstance, this._indices);
		}

		// Token: 0x0400153E RID: 5438
		private int[] _indices;
	}
}
