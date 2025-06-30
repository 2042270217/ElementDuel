using ElementDuel;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FuNingNa", menuName = "ElementDuel/CharacterCard/FuNingNa")]
public class FuNingNa : BaseCharacterCard
{
	bool isHuang = true;


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
			CreateSkillAsset<FuNingNaNormalAttack, FuNingNa>(skillPath, skillDataPath);
			CreateSkillAsset<FuNingNaSkill, FuNingNa>(skillPath,skillDataPath);
			CreateSkillAsset<FuNingNaBurst, FuNingNa>(skillPath, skillDataPath);

			Refresh();
			m_isInitialized = true;

		};
	}
#endif
}
