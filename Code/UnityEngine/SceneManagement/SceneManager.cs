using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020000DE RID: 222
	public class SceneManager
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E4D RID: 3661
		public static extern int sceneCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E4E RID: 3662
		public static extern int sceneCountInBuildSettings { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000E4F RID: 3663 RVA: 0x00012184 File Offset: 0x00010384
		public static Scene GetActiveScene()
		{
			Scene result;
			SceneManager.INTERNAL_CALL_GetActiveScene(out result);
			return result;
		}

		// Token: 0x06000E50 RID: 3664
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetActiveScene(out Scene value);

		// Token: 0x06000E51 RID: 3665 RVA: 0x0001219C File Offset: 0x0001039C
		public static bool SetActiveScene(Scene scene)
		{
			return SceneManager.INTERNAL_CALL_SetActiveScene(ref scene);
		}

		// Token: 0x06000E52 RID: 3666
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SetActiveScene(ref Scene scene);

		// Token: 0x06000E53 RID: 3667 RVA: 0x000121A8 File Offset: 0x000103A8
		public static Scene GetSceneByPath(string scenePath)
		{
			Scene result;
			SceneManager.INTERNAL_CALL_GetSceneByPath(scenePath, out result);
			return result;
		}

		// Token: 0x06000E54 RID: 3668
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetSceneByPath(string scenePath, out Scene value);

		// Token: 0x06000E55 RID: 3669 RVA: 0x000121C0 File Offset: 0x000103C0
		public static Scene GetSceneByName(string name)
		{
			Scene result;
			SceneManager.INTERNAL_CALL_GetSceneByName(name, out result);
			return result;
		}

		// Token: 0x06000E56 RID: 3670
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetSceneByName(string name, out Scene value);

		// Token: 0x06000E57 RID: 3671 RVA: 0x000121D8 File Offset: 0x000103D8
		public static Scene GetSceneAt(int index)
		{
			Scene result;
			SceneManager.INTERNAL_CALL_GetSceneAt(index, out result);
			return result;
		}

		// Token: 0x06000E58 RID: 3672
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetSceneAt(int index, out Scene value);

		// Token: 0x06000E59 RID: 3673 RVA: 0x000121F0 File Offset: 0x000103F0
		[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
		public static Scene[] GetAllScenes()
		{
			Scene[] array = new Scene[SceneManager.sceneCount];
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				array[i] = SceneManager.GetSceneAt(i);
			}
			return array;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00012234 File Offset: 0x00010434
		[ExcludeFromDocs]
		public static void LoadScene(string sceneName)
		{
			LoadSceneMode mode = LoadSceneMode.Single;
			SceneManager.LoadScene(sceneName, mode);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0001224C File Offset: 0x0001044C
		public static void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, mode == LoadSceneMode.Additive, true);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00012268 File Offset: 0x00010468
		[ExcludeFromDocs]
		public static void LoadScene(int sceneBuildIndex)
		{
			LoadSceneMode mode = LoadSceneMode.Single;
			SceneManager.LoadScene(sceneBuildIndex, mode);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00012280 File Offset: 0x00010480
		public static void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, mode == LoadSceneMode.Additive, true);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0001229C File Offset: 0x0001049C
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			LoadSceneMode mode = LoadSceneMode.Single;
			return SceneManager.LoadSceneAsync(sceneName, mode);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000122B4 File Offset: 0x000104B4
		public static AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, mode == LoadSceneMode.Additive, false);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000122CC File Offset: 0x000104CC
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			LoadSceneMode mode = LoadSceneMode.Single;
			return SceneManager.LoadSceneAsync(sceneBuildIndex, mode);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x000122E4 File Offset: 0x000104E4
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, mode == LoadSceneMode.Additive, false);
		}

		// Token: 0x06000E62 RID: 3682
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, bool isAdditive, bool mustCompleteNextFrame);

		// Token: 0x06000E63 RID: 3683 RVA: 0x000122FC File Offset: 0x000104FC
		public static Scene CreateScene(string sceneName)
		{
			Scene result;
			SceneManager.INTERNAL_CALL_CreateScene(sceneName, out result);
			return result;
		}

		// Token: 0x06000E64 RID: 3684
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_CreateScene(string sceneName, out Scene value);

		// Token: 0x06000E65 RID: 3685 RVA: 0x00012314 File Offset: 0x00010514
		public static bool UnloadScene(int sceneBuildIndex)
		{
			return SceneManager.UnloadSceneNameIndexInternal(string.Empty, sceneBuildIndex);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00012324 File Offset: 0x00010524
		public static bool UnloadScene(string sceneName)
		{
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1);
		}

		// Token: 0x06000E67 RID: 3687
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex);

		// Token: 0x06000E68 RID: 3688 RVA: 0x00012330 File Offset: 0x00010530
		public static void MergeScenes(Scene sourceScene, Scene destinationScene)
		{
			SceneManager.INTERNAL_CALL_MergeScenes(ref sourceScene, ref destinationScene);
		}

		// Token: 0x06000E69 RID: 3689
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MergeScenes(ref Scene sourceScene, ref Scene destinationScene);

		// Token: 0x06000E6A RID: 3690 RVA: 0x0001233C File Offset: 0x0001053C
		public static void MoveGameObjectToScene(GameObject go, Scene scene)
		{
			SceneManager.INTERNAL_CALL_MoveGameObjectToScene(go, ref scene);
		}

		// Token: 0x06000E6B RID: 3691
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MoveGameObjectToScene(GameObject go, ref Scene scene);
	}
}
