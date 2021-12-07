using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to commit or rollback changes to an object that is used as a data source.</summary>
	// Token: 0x02000158 RID: 344
	public interface IEditableObject
	{
		/// <summary>Begins an edit on an object.</summary>
		// Token: 0x06000C8A RID: 3210
		void BeginEdit();

		/// <summary>Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.</summary>
		// Token: 0x06000C8B RID: 3211
		void CancelEdit();

		/// <summary>Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into the underlying object.</summary>
		// Token: 0x06000C8C RID: 3212
		void EndEdit();
	}
}
