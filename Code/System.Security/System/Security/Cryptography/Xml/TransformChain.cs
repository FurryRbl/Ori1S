using System;
using System.Collections;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Defines an ordered list of <see cref="T:System.Security.Cryptography.Xml.Transform" /> objects that is applied to unsigned content prior to digest calculation.</summary>
	// Token: 0x02000052 RID: 82
	public class TransformChain
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> class.</summary>
		// Token: 0x060002A7 RID: 679 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public TransformChain()
		{
			this.chain = new ArrayList();
		}

		/// <summary>Gets the number of transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <returns>The number of transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public int Count
		{
			get
			{
				return this.chain.Count;
			}
		}

		/// <summary>Gets the transform at the specified index in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <returns>The transform at the specified index in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		/// <param name="index">The index into the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that specifies which transform to return. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> parameter is greater than the number of transforms.</exception>
		// Token: 0x170000B6 RID: 182
		public Transform this[int index]
		{
			get
			{
				return (Transform)this.chain[index];
			}
		}

		/// <summary>Adds a transform to the list of transforms to be applied to the unsigned content prior to digest calculation.</summary>
		/// <param name="transform">The transform to add to the list of transforms. </param>
		// Token: 0x060002AA RID: 682 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public void Add(Transform transform)
		{
			this.chain.Add(transform);
		}

		/// <summary>Returns an enumerator of the transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <returns>An enumerator of the transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		// Token: 0x060002AB RID: 683 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public IEnumerator GetEnumerator()
		{
			return this.chain.GetEnumerator();
		}

		// Token: 0x0400012C RID: 300
		private ArrayList chain;
	}
}
