/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_", menuName = "FF/Event/Raycast Hit Game Event" ) ]
public class RaycastHitGameEvent : GameEvent
{
#region Fields
    public RaycastHitData event_data;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Raise( Vector3 position, Collider collider )
    {
		event_data.hit_position = position;
		event_data.hit_collider = collider;

		Raise();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

[ System.Serializable ]
public struct RaycastHitData
{
	public Vector3 hit_position;
	public Collider hit_collider;
}