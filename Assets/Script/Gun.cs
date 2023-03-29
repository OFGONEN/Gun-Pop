/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Gun : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PoolGun pool_gun;

  [ Title( "Components" ) ]
    [ SerializeField ] Collider gun_collider;
    [ SerializeField ] MeshFilter gun_mesh_filter;
    [ SerializeField ] MeshRenderer gun_mesh_renderer;

    GunData gun_data;
    GunVisualData gun_visual_data;
    Anchor gun_anchor;
#endregion

#region Properties
    public GunVisualData GunVisualData => gun_visual_data;
    public GunData GunData             => gun_data;
#endregion

#region Unity API
    void Awake()
    {
		gun_collider.enabled = false;
	}
#endregion

#region API
    public void Spawn( Vector3 position, Anchor anchor, GunData gunData, GunVisualData gunVisualData )
    {
		gun_data        = gunData;
		gun_visual_data = gunVisualData;

		gun_collider.enabled = false;
		UpdateVisual();

		transform.position = position;
		gun_anchor         = anchor;
	}
#endregion

#region Implementation
    void UpdateVisual()
    {
		var visualData = gun_visual_data.gun_model_data_array[ gun_data.gun_level ];

		gun_mesh_filter.mesh             = visualData.gun_model_mesh;
		gun_mesh_renderer.sharedMaterial = visualData.gun_model_material;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}