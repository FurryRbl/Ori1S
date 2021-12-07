using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004FB RID: 1275
	internal class ArrayFixupRecord : BaseFixupRecord
	{
		// Token: 0x06003318 RID: 13080 RVA: 0x000A56F4 File Offset: 0x000A38F4
		public ArrayFixupRecord(ObjectRecord objectToBeFixed, int index, ObjectRecord objectRequired) : base(objectToBeFixed, objectRequired)
		{
			this._index = index;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000A5708 File Offset: 0x000A3908
		protected override void FixupImpl(ObjectManager manager)
		{
			Array array = (Array)this.ObjectToBeFixed.ObjectInstance;
			array.SetValue(this.ObjectRequired.ObjectInstance, this._index);
		}

		// Token: 0x0400153D RID: 5437
		private int _index;
	}
}
