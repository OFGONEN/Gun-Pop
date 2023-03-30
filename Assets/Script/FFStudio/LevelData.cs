/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.IO;
using System.Collections;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "LevelData", menuName = "FF/Data/LevelData" ) ]
	public class LevelData : ScriptableObject
    {
	[ Title( "Setup" ) ]
		[ ValueDropdown( "SceneList" ), LabelText( "Scene Index" ) ] public int scene_index;
        [ LabelText( "Override As Active Scene" ) ] public bool scene_overrideAsActiveScene;

	[ Title( "Level" ) ]
		[ LabelText( "Level Size " ) ] public Vector2Int level_size;
		[ LabelText( "Level Move Count " ) ] public int level_moveCount;
		[ LabelText( "Gun Color Data Percentage " ) ] public DataPercentageGunColorData[] data_gunColor_percentage_array;
		[ LabelText( "Gun Data Percentage " ) ] public DataPercentageGunData[] data_gun_percentage_array;

#if UNITY_EDITOR
		static IEnumerable SceneList()
        {
			var list = new ValueDropdownList< int >();

			var scene_count = SceneManager.sceneCountInBuildSettings;

			for( var i = 0; i < scene_count; i++ )
				list.Add( Path.GetFileNameWithoutExtension( SceneUtility.GetScenePathByBuildIndex( i ) ) + $" ({i})", i );

			return list;
		}
#endif
    }
}

[ System.Serializable ]
public struct DataPercentageGunColorData
{
	public int data_percentage;
	public GunVisualData data;
}

[ System.Serializable ]
public struct DataPercentageGunData
{
	public int data_percentage;
	public GunData data;
}