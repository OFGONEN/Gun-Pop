/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_merge", menuName = "FF/Game/System/Merge" ) ]
public class SystemMerge : ScriptableObject
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] RunTimeSetGun set_gun;
    [ SerializeField ] GameEvent event_selection_enable;

    int merge_index;
    Gun merge_gun;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnSelectionComplete()
    {
        if( set_gun.itemList.Count >= GameSettings.Instance.merge_count )
			StartMerge();
        else
			event_selection_enable.Raise();
	}
#endregion

#region Implementation
    void StartMerge()
    {
		merge_index = 1;
		merge_gun   = set_gun.itemList[ 0 ];

		merge_gun.DoMerge( set_gun.itemList[ merge_index ], OnMergeDone );
	}

    void ContiuneMerge()
    {
		merge_index++;
		merge_gun.DoMerge( set_gun.itemList[ merge_index ], OnMergeDone );
	}

    void OnMergeDone()
    {
		set_gun.itemList[ merge_index ].OnMerged();
		merge_gun.DoUpgrade();
        // Remove selection line

		if( merge_index == set_gun.itemList.Count - 1 )
			OnMergeSequenceComplete();
        else
			ContiuneMerge();
	}

    void OnMergeSequenceComplete()
    {
		// lift the gun up and fire
		merge_gun.DoFire();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}