using ElementDuel;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "QiQi", menuName = "ElementDuel/CharacterCard/QiQi")]
public class QiQi : BaseCharacterCard
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
			CreateSkillAsset<QiQiNormalAttack, QiQi>(skillPath, skillDataPath);
			CreateSkillAsset<QiQiSkill, QiQi>(skillPath, skillDataPath);
			CreateSkillAsset<QiQiBurst, QiQi>(skillPath, skillDataPath);

			Refresh();
			m_isInitialized = true;

		};
	}
#endif
}
