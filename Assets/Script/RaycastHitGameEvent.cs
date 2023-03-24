/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_", menuName = "FF/Event/Raycast Hit Game Event" ) ]
public class RaycastHitGameEvent : GameEvent
{
#region Fields
    public Vector3 hit_position;
    public Collider hit_collider;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Raise( Vector3 position, Collider collider )
    {
		hit_position = position;
		hit_collider = collider;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}