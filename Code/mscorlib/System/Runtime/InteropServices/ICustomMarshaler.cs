using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Designed to provide custom wrappers for handling method calls.</summary>
	// Token: 0x02000398 RID: 920
	[ComVisible(true)]
	public interface ICustomMarshaler
	{
		/// <summary>Performs necessary cleanup of the managed data when it is no longer needed.</summary>
		/// <param name="ManagedObj">The managed object to be destroyed. </param>
		// Token: 0x06002A78 RID: 10872
		void CleanUpManagedData(object ManagedObj);

		/// <summary>Performs necessary cleanup of the unmanaged data when it is no longer needed.</summary>
		/// <param name="pNativeData">A pointer to the unmanaged data to be destroyed. </param>
		// Token: 0x06002A79 RID: 10873
		void CleanUpNativeData(IntPtr pNativeData);

		/// <summary>Returns the size of the native data to be marshaled.</summary>
		/// <returns>The size in bytes of the native data.</returns>
		// Token: 0x06002A7A RID: 10874
		int GetNativeDataSize();

		/// <summary>Converts the managed data to unmanaged data.</summary>
		/// <returns>Returns the COM view of the managed object.</returns>
		/// <param name="ManagedObj">The managed object to be converted. </param>
		// Token: 0x06002A7B RID: 10875
		IntPtr MarshalManagedToNative(object ManagedObj);

		/// <summary>Converts the unmanaged data to managed data.</summary>
		/// <returns>Returns the managed view of the COM data.</returns>
		/// <param name="pNativeData">A pointer to the unmanaged data to be wrapped. </param>
		// Token: 0x06002A7C RID: 10876
		object MarshalNativeToManaged(IntPtr pNativeData);
	}
}
