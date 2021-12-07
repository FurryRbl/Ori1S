using System;

// Token: 0x020002C3 RID: 707
[Category("Camera")]
public class CameraShakeAction : ActionMethod
{
	// Token: 0x06001602 RID: 5634 RVA: 0x00061808 File Offset: 0x0005FA08
	public override void Perform(IContext context)
	{
		if (this.ShakeCamera)
		{
			this.ShakeCamera.PerformTheShake();
		}
	}

	// Token: 0x040012F5 RID: 4853
	public CameraShake ShakeCamera;
}
