/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Gun : MonoBehaviour
{
#region Fields

  [ Title( "Components" ) ]
    [ SerializeField ] Collider gun_collider;
    [ SerializeField ] MeshFilter gun_mesh_filter;
    [ SerializeField ] MeshRenderer gun_mesh_renderer;

    GunData gun_data;
    GunVisualData gun_color_data;
#endregion

#region Properties
    public GunVisualData GunVisualData => gun_color_data;
    public GunData GunData             => gun_data;
#endregion

#region Unity API
    void Awake()
    {
		gun_collider.enabled = false;
	}
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