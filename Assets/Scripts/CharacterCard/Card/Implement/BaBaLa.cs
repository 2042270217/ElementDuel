using ElementDuel;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BaBaLa", menuName = "ElementDuel/CharacterCard/BaBaLa")]
public class BaBaLa : BaseCharacterCard
{


#if UNITY_EDITOR

	private void OnValidate()
	{
		if (m_isInitialized) return;

		// 延迟执行以避开 Asset 导入限制
		EditorApplication.delayCall += () =>
		{
			string basePath;
			string skillPath;
			string skillDataPath;

			InitializeFolder(out basePath, out skillPath, out skillDataPath);

			CreatCharacterDataAsset(basePath);
			CreateSkillAsset<BaBaLaNormalAttack, BaBaLa>(skillPath, skillDataPath);
			CreateSkillAsset<BaBaLaSkill, BaBaLa>(skillPath,skillDataPath);
			CreateSkillAsset<BaBaLaBurst, BaBaLa>(skillPath, skillDataPath);

			Refresh();
			m_isInitialized = true;

		};
	}
#endif
}
