/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Gun : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PoolGun pool_gun;
    [ SerializeField ] SharedVector3Notifier notif_gun_fire_position;
	[ SerializeField ] IntGameEvent event_gun_fired;

  [ Title( "Components" ) ]
    [ SerializeField ] Collider gun_collider;
    [ SerializeField ] MeshFilter gun_mesh_filter;
    [ SerializeField ] MeshRenderer gun_mesh_renderer;

    GunData gun_data;
    GunVisualData gun_visual_data;
    Anchor gun_anchor;

	RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
    public GunVisualData GunVisualData => gun_visual_data;
    public GunData GunData             => gun_data;
	public Vector2Int AnchorCoordiante => gun_anchor.AnchorCoordinate;
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
		gun_anchor      = anchor;

		gun_collider.enabled = true;
		UpdateVisual();

		transform.position = position;
		gameObject.SetActive( true );
	}
	
	public void StartMerge()
	{
		gun_anchor.OnGunMerged();
	}

	public void DoMerge( Gun target, UnityMessage onMergeDone )
	{
		recycledTween.Recycle( transform.DOJump(
			target.transform.position,
			GameSettings.Instance.merge_jump_power,
			1,
			GameSettings.Instance.merge_jump_duration )
			.SetEase( GameSettings.Instance.merge_jump_ease ),
			onMergeDone
		);
	}

	public void OnMerged()
	{
		gun_anchor.OnGunMerged();
		pool_gun.ReturnEntity( this );
	}

	public void DoUpgrade()
	{
		gun_data = gun_data.gun_nextData;
		UpdateVisual();
	}

	public void DoFire()
	{
		event_gun_fired.eventValue = gun_data.gun_damage;

		recycledTween.Recycle( transform.DOMove( 
			notif_gun_fire_position.sharedValue, 
			GameSettings.Instance.gun_fire_move_duration )
			.SetEase( GameSettings.Instance.gun_fire_move_ease ),
			OnGunFireSequenceComplete
		);
	}
#endregion

#region Implementation
	void OnGunFireSequenceComplete()
	{
		event_gun_fired.Raise();
		pool_gun.ReturnEntity( this );
	}

    void UpdateVisual()
    {
		var visualData = gun_visual_data.gun_model_data_array[ Mathf.Clamp( gun_data.gun_level - 1, 0, gun_visual_data.gun_model_data_array.Length - 1 ) ];

		gun_mesh_filter.mesh             = visualData.gun_model_mesh;
		gun_mesh_renderer.sharedMaterial = visualData.gun_model_material;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}