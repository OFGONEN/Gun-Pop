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
	[ SerializeField ] GameEvent event_gun_fire_start;
	[ SerializeField ] IntGameEvent event_gun_fired;
	[ SerializeField ] ParticleSpawnEvent event_particle_spawn;

  [ Title( "Components" ) ]
    [ SerializeField ] Collider gun_collider;
    [ SerializeField ] MeshFilter gun_mesh_filter;
    [ SerializeField ] MeshRenderer gun_mesh_renderer;

    GunData gun_data;
    GunVisualData gun_visual_data;
    Anchor gun_anchor;

	RecycledTween    recycledTween    = new RecycledTween();
	RecycledSequence recycledSequence = new RecycledSequence();
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

		transform.position   = position;
		transform.localScale = Vector3.one;

		gameObject.SetActive( true );
		recycledTween.Recycle( GameSettings.Instance.gun_spawn_punchScale.CreateTween( transform ) );
	}
	
	public void StartMerge()
	{
		gun_anchor.OnGunMerged();
	}

	public void DoMerge( Gun target, UnityMessage onMergeDone )
	{
		var targetPosition = target.transform.position;
		var targetScale    = transform.localScale + Vector3.one * GameSettings.Instance.merge_size_step;

		var sequence = recycledSequence.Recycle( onMergeDone );
		sequence.Append( transform.DOMoveX(
			targetPosition.x,
			GameSettings.Instance.merge_jump_duration )
			.SetEase( GameSettings.Instance.merge_jump_ease )
		);

		sequence.Join( transform.DOMoveY(
			targetPosition.y,
			GameSettings.Instance.merge_jump_duration )
			.SetEase( GameSettings.Instance.merge_jump_ease )
		);

		sequence.Join( transform.DOMoveZ(
			-GameSettings.Instance.merge_jump_power,
			GameSettings.Instance.merge_jump_duration )
			.SetEase( GameSettings.Instance.merge_jump_ease )
		);

		sequence.Join( transform.DOScale(
			targetScale,
			GameSettings.Instance.merge_jump_duration )
			.SetEase( GameSettings.Instance.merge_jump_ease ) 
		);

		sequence.AppendInterval( GameSettings.Instance.merge_jump_delay );
	}

	public void OnMerged()
	{
		gun_anchor.OnGunMerged();
		pool_gun.ReturnEntity( this );
	}

	public void DoUpgrade()
	{
		gun_data = gun_data.gun_nextData;
		event_particle_spawn.Raise( "gun_upgrade", transform.position );
		UpdateVisual();
	}

	public void DoFire()
	{
		event_gun_fire_start.Raise();
		event_gun_fired.eventValue = gun_data.gun_damage;

		var sequence = recycledSequence.Recycle( OnGunFireSequenceComplete );

		sequence.Append( transform.DOMove(
			notif_gun_fire_position.sharedValue,
			GameSettings.Instance.gun_fire_move_duration )
			.SetEase( GameSettings.Instance.gun_fire_move_ease )
		);

		sequence.Join( transform.DOScale(
			GameSettings.Instance.merge_size_final,
			GameSettings.Instance.gun_fire_move_duration )
			.SetEase( GameSettings.Instance.gun_fire_move_ease )
		);

		sequence.Append( transform.DOShakePosition(
			GameSettings.Instance.gun_fire_shake_duration )
			.SetEase( GameSettings.Instance.gun_fire_shake_ease )
			.SetLoops( GameSettings.Instance.gun_fire_shake_count_range.ReturnRandom(), LoopType.Yoyo )
			.OnStepComplete( OnGunFireStep )
		);

		sequence.AppendInterval( GameSettings.Instance.gun_fire_sequence_end_delay );
	}
#endregion

#region Implementation
	void OnGunFireSequenceComplete()
	{
		pool_gun.ReturnEntity( this );
		event_gun_fired.Raise();
	}

	void OnGunFireStep()
	{
		event_particle_spawn.Raise( "gun_fire", transform.position + gun_data.gun_fire_pfx_offset, transform );
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