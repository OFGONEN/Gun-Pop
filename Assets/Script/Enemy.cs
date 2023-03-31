/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Enemy : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] SharedFloatNotifier notif_level_progress;
    [ SerializeField ] GameEvent event_enemy_damaged;
    [ SerializeField ] GameEvent event_enemy_died;

    int health_current;
#endregion

#region Properties
#endregion

#region Unity API
    void Start()
    {
		notif_level_progress.SharedValue = 1;
		health_current = CurrentLevelData.Instance.levelData.enemy_health;
	}


#endregion

#region API
    public void OnGunFire( int damage )
    {
		health_current                   = Mathf.Max( health_current - damage,  0 );
		notif_level_progress.SharedValue = Mathf.InverseLerp( CurrentLevelData.Instance.levelData.enemy_health, 0, health_current );

        if( health_current > 0 )
			event_enemy_damaged.Raise();
        else
			event_enemy_died.Raise();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}