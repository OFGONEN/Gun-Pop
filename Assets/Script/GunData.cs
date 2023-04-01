/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "gun_data_", menuName = "FF/Game/Gun Data" ) ]
public class GunData : ScriptableObject
{
#region Fields
    public int gun_level;
    public int gun_damage;
	public Vector3 gun_fire_pfx_offset;
	public GunData gun_nextData;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}