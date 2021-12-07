using System;
using UnityEngine;

// Token: 0x0200045C RID: 1116
public class TextureAnimationGUIText : MonoBehaviour
{
	// Token: 0x06001ECB RID: 7883 RVA: 0x00087A80 File Offset: 0x00085C80
	public void Update()
	{
		base.GetComponent<GUIText>().text = this.TextureAnimator.CurrentAnimation.name;
	}

	// Token: 0x04001A9F RID: 6815
	public SpriteAnimatorWithTransitions TextureAnimator;
}
