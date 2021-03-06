using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime
{
	/// <summary>Check for sufficient memory resources prior to execution. This class cannot be inherited.</summary>
	// Token: 0x0200031F RID: 799
	public sealed class MemoryFailPoint : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.MemoryFailPoint" /> class, specifying the amount of memory required for successful execution. </summary>
		/// <param name="sizeInMegabytes">The required memory size in megabytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified memory size is negative.</exception>
		/// <exception cref="T:System.InsufficientMemoryException">There is insufficient memory to begin execution of the code protected by the gate.</exception>
		// Token: 0x06002890 RID: 10384 RVA: 0x00091CA0 File Offset: 0x0008FEA0
		[MonoTODO]
		public MemoryFailPoint(int sizeInMegabytes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x00091CB0 File Offset: 0x0008FEB0
		~MemoryFailPoint()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.MemoryFailPoint" />. </summary>
		// Token: 0x06002892 RID: 10386 RVA: 0x00091CE8 File Offset: 0x0008FEE8
		[MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
