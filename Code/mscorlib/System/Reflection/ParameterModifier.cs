using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Attaches a modifier to parameters so that binding can work with parameter signatures in which the types have been modified.</summary>
	// Token: 0x020002B1 RID: 689
	[ComVisible(true)]
	[Serializable]
	public struct ParameterModifier
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ParameterModifier" /> structure representing the specified number of parameters.</summary>
		/// <param name="parameterCount">The number of parameters. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parameterCount" /> is negative. </exception>
		// Token: 0x06002314 RID: 8980 RVA: 0x0007DF60 File Offset: 0x0007C160
		public ParameterModifier(int parameterCount)
		{
			if (parameterCount <= 0)
			{
				throw new ArgumentException("Must specify one or more parameters.");
			}
			this._byref = new bool[parameterCount];
		}

		/// <summary>Gets or sets a value that specifies whether the parameter at the specified index position is to be modified by the current <see cref="T:System.Reflection.ParameterModifier" />.</summary>
		/// <returns>true if the parameter at this index position is to be modified by this <see cref="T:System.Reflection.ParameterModifier" />; otherwise, false.</returns>
		/// <param name="index">The index position of the parameter whose modification status is being examined or set. </param>
		// Token: 0x17000620 RID: 1568
		public bool this[int index]
		{
			get
			{
				return this._byref[index];
			}
			set
			{
				this._byref[index] = value;
			}
		}

		// Token: 0x04000D1D RID: 3357
		private bool[] _byref;
	}
}
