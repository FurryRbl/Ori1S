using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies that the type has a visualizer. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001E4 RID: 484
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerVisualizerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		// Token: 0x06001876 RID: 6262 RVA: 0x0005D6F0 File Offset: 0x0005B8F0
		public DebuggerVisualizerAttribute(string visualizerTypeName)
		{
			this.visualizerName = visualizerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		// Token: 0x06001877 RID: 6263 RVA: 0x0005D700 File Offset: 0x0005B900
		public DebuggerVisualizerAttribute(Type visualizer)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		// Token: 0x06001878 RID: 6264 RVA: 0x0005D728 File Offset: 0x0005B928
		public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
		{
			this.visualizerName = visualizerTypeName;
			this.visualizerSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is null.</exception>
		// Token: 0x06001879 RID: 6265 RVA: 0x0005D740 File Offset: 0x0005B940
		public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
		{
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizerTypeName;
			this.visualizerSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		// Token: 0x0600187A RID: 6266 RVA: 0x0005D778 File Offset: 0x0005B978
		public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is null.</exception>
		// Token: 0x0600187B RID: 6267 RVA: 0x0005D7B0 File Offset: 0x0005B9B0
		public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Gets or sets the description of the visualizer.</summary>
		/// <returns>The description of the visualizer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0005D800 File Offset: 0x0005BA00
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x0005D808 File Offset: 0x0005BA08
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the target type when the attribute is applied at the assembly level.</summary>
		/// <returns>The type that is the target of the visualizer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value cannot be set because it is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0005D814 File Offset: 0x0005BA14
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x0005D81C File Offset: 0x0005BA1C
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
				this.targetTypeName = this.target.AssemblyQualifiedName;
			}
		}

		/// <summary>Gets or sets the fully qualified type name when the attribute is applied at the assembly level.</summary>
		/// <returns>The fully qualified type name of the target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0005D838 File Offset: 0x0005BA38
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x0005D840 File Offset: 0x0005BA40
		public string TargetTypeName
		{
			get
			{
				return this.targetTypeName;
			}
			set
			{
				this.targetTypeName = value;
			}
		}

		/// <summary>Gets the fully qualified type name of the visualizer object source.</summary>
		/// <returns>The fully qualified type name of the visualizer object source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005D84C File Offset: 0x0005BA4C
		public string VisualizerObjectSourceTypeName
		{
			get
			{
				return this.visualizerSourceName;
			}
		}

		/// <summary>Gets the fully qualified type name of the visualizer.</summary>
		/// <returns>The fully qualified visualizer type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0005D854 File Offset: 0x0005BA54
		public string VisualizerTypeName
		{
			get
			{
				return this.visualizerName;
			}
		}

		// Token: 0x040008EC RID: 2284
		private string description;

		// Token: 0x040008ED RID: 2285
		private string visualizerSourceName;

		// Token: 0x040008EE RID: 2286
		private string visualizerName;

		// Token: 0x040008EF RID: 2287
		private string targetTypeName;

		// Token: 0x040008F0 RID: 2288
		private Type target;
	}
}
