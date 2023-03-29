/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_", menuName = "FF/Event/Gun Game Event" ) ]
public class GunGameEvent : GameEvent
{
#region Fields
    public Gun event_value;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Raise( Gun gun )
    {
		event_value = gun;
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