/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_spawn", menuName = "FF/Game/System/Spawn" ) ]
public class SystemSpawn : ScriptableObject
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public ( GunData, GunVisualData ) GetSpawnData()
    {
		GunData      gun_data       = null;
		GunVisualData gun_color_data = null;

		var randomColorData = Random.Range( 0, 100 );
		var randomData      = Random.Range( 0, 100 );

		int percentage = 0;
		var gunColorDataArray = CurrentLevelData.Instance.levelData.data_gunColor_percentage_array;
		var gunDataArray      = CurrentLevelData.Instance.levelData.data_gun_percentage_array;


		for( var i = 0; i < gunColorDataArray.Length; i++ )
        {
			var dataPercentage  = gunColorDataArray[ i ].data_percentage;
			    percentage     += dataPercentage;

			if( randomColorData < percentage )
            {
				gun_color_data = gunColorDataArray[ i ].data;
				break;
			}
		}

		percentage = 0;

		for( var i = 0; i < gunDataArray.Length; i++ )
		{
			var dataPercentage  = gunDataArray[ i ].data_percentage;
			    percentage     += dataPercentage;

			if( randomData < percentage )
			{
				gun_data = gunDataArray[ i ].data;
				break;
			}
		}

		return ( gun_data, gun_color_data );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}