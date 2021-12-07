using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines the basic operations of cryptographic transformations.</summary>
	// Token: 0x020005B8 RID: 1464
	[ComVisible(true)]
	public interface ICryptoTransform : IDisposable
	{
		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>true if the current transform can be reused; otherwise, false.</returns>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003839 RID: 14393
		bool CanReuseTransform { get; }

		/// <summary>Gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x0600383A RID: 14394
		bool CanTransformMultipleBlocks { get; }

		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600383B RID: 14395
		int InputBlockSize { get; }

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600383C RID: 14396
		int OutputBlockSize { get; }

		/// <summary>Transforms the specified region of the input byte array and copies the resulting transform to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input for which to compute the transform. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the transform. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		// Token: 0x0600383D RID: 14397
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		/// <summary>Transforms the specified region of the specified byte array.</summary>
		/// <returns>The computed transform.</returns>
		/// <param name="inputBuffer">The input for which to compute the transform. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		// Token: 0x0600383E RID: 14398
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}
