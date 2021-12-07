using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public abstract class SoundProvider : MonoBehaviour
{
	// Token: 0x0600033E RID: 830
	public abstract SoundDescriptor GetSound(IContext context);

	// Token: 0x04000266 RID: 614
	public bool ShowMessageOnPlay;

	// Token: 0x04000267 RID: 615
	protected SoundDescriptor Descriptor = new SoundDescriptor();
}
