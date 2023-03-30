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
      
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnSelectionComplete()
    {
        if( set_gun.itemList.Count >= GameSettings.Instance.merge_count )
			DoMerge();
        else
			event_selection_enable.Raise();
	}
#endregion

#region Implementation
    void DoMerge()
    {

    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}