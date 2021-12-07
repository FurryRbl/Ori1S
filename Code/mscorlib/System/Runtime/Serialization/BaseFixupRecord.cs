using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004FA RID: 1274
	internal abstract class BaseFixupRecord
	{
		// Token: 0x06003315 RID: 13077 RVA: 0x000A5630 File Offset: 0x000A3830
		public BaseFixupRecord(ObjectRecord objectToBeFixed, ObjectRecord objectRequired)
		{
			this.ObjectToBeFixed = objectToBeFixed;
			this.ObjectRequired = objectRequired;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000A5648 File Offset: 0x000A3848
		public bool DoFixup(ObjectManager manager, bool strict)
		{
			if (this.ObjectToBeFixed.IsRegistered && this.ObjectRequired.IsInstanceReady)
			{
				this.FixupImpl(manager);
				return true;
			}
			if (!strict)
			{
				return false;
			}
			if (!this.ObjectToBeFixed.IsRegistered)
			{
				throw new SerializationException("An object with ID " + this.ObjectToBeFixed.ObjectID + " was included in a fixup, but it has not been registered");
			}
			if (!this.ObjectRequired.IsRegistered)
			{
				throw new SerializationException("An object with ID " + this.ObjectRequired.ObjectID + " was included in a fixup, but it has not been registered");
			}
			return false;
		}

		// Token: 0x06003317 RID: 13079
		protected abstract void FixupImpl(ObjectManager manager);

		// Token: 0x04001539 RID: 5433
		protected internal ObjectRecord ObjectToBeFixed;

		// Token: 0x0400153A RID: 5434
		protected internal ObjectRecord ObjectRequired;

		// Token: 0x0400153B RID: 5435
		public BaseFixupRecord NextSameContainer;

		// Token: 0x0400153C RID: 5436
		public BaseFixupRecord NextSameRequired;
	}
}
