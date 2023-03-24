/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "gun_model_data_", menuName = "FF/Game/Gun Model Data" ) ]
public class GunColorData : ScriptableObject
{
#region Fields
    public Color gun_model_color;
    public GunModelData[] gun_model_data_array;
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

[ System.Serializable ]
public struct GunModelData
{
	public Mesh gun_model_mesh;
	public Material gun_model_material;
}