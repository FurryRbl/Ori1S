using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Tracks outstanding handles and forces a garbage collection when the specified threshold is reached.</summary>
	// Token: 0x020004CA RID: 1226
	public sealed class HandleCollector
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name and a threshold at which to begin handle collection. </summary>
		/// <param name="name">A name for the collector.  This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.</exception>
		// Token: 0x06002BD9 RID: 11225 RVA: 0x00098D44 File Offset: 0x00096F44
		public HandleCollector(string name, int initialThreshold) : this(name, initialThreshold, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name, a threshold at which to begin handle collection, and a threshold at which handle collection must occur. </summary>
		/// <param name="name">A name for the collector.  This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <param name="maximumThreshold">A value that specifies the point at which collections must occur. This should be set to the maximum number of available handles.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.-or-The <paramref name="maximumThreshold" /> parameter is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="maximumThreshold" /> parameter is less than the <paramref name="initialThreshold" /> parameter.</exception>
		// Token: 0x06002BDA RID: 11226 RVA: 0x00098D54 File Offset: 0x00096F54
		public HandleCollector(string name, int initialThreshold, int maximumThreshold)
		{
			if (initialThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("initialThreshold", "initialThreshold must not be less than zero");
			}
			if (maximumThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("maximumThreshold", "maximumThreshold must not be less than zero");
			}
			if (maximumThreshold < initialThreshold)
			{
				throw new ArgumentException("maximumThreshold must not be less than initialThreshold");
			}
			this.name = name;
			this.init = initialThreshold;
			this.max = maximumThreshold;
		}

		/// <summary>Gets the number of handles collected.</summary>
		/// <returns>The number of handles collected.</returns>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x00098DC8 File Offset: 0x00096FC8
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections should begin.</summary>
		/// <returns>A value that specifies the point at which collections should begin.</returns>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x00098DD0 File Offset: 0x00096FD0
		public int InitialThreshold
		{
			get
			{
				return this.init;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections must occur.</summary>
		/// <returns>A value that specifies the point at which collections must occur.</returns>
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x00098DD8 File Offset: 0x00096FD8
		public int MaximumThreshold
		{
			get
			{
				return this.max;
			}
		}

		/// <summary>Gets the name of a <see cref="T:System.Runtime.InteropServices.HandleCollector" /> object.</summary>
		/// <returns>This <see cref="P:System.Runtime.InteropServices.HandleCollector.Name" /> property allows you to name collectors that track handle types separately.</returns>
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x00098DE0 File Offset: 0x00096FE0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Increments the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x06002BDF RID: 11231 RVA: 0x00098DE8 File Offset: 0x00096FE8
		public void Add()
		{
			if (++this.count >= this.max)
			{
				GC.Collect(GC.MaxGeneration);
			}
			else if (this.count >= this.init && DateTime.Now - this.previous_collection > TimeSpan.FromSeconds(5.0))
			{
				GC.Collect(GC.MaxGeneration);
				this.previous_collection = DateTime.Now;
			}
		}

		/// <summary>Decrements the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x06002BE0 RID: 11232 RVA: 0x00098E70 File Offset: 0x00097070
		public void Remove()
		{
			if (this.count == 0)
			{
				throw new InvalidOperationException("Cannot call Remove method when Count is 0");
			}
			this.count--;
		}

		// Token: 0x04001BA0 RID: 7072
		private int count;

		// Token: 0x04001BA1 RID: 7073
		private readonly int init;

		// Token: 0x04001BA2 RID: 7074
		private readonly int max;

		// Token: 0x04001BA3 RID: 7075
		private readonly string name;

		// Token: 0x04001BA4 RID: 7076
		private DateTime previous_collection = DateTime.MinValue;
	}
}
