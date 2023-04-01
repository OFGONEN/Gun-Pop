/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Enemy : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] SharedFloatNotifier notif_level_progress;
    [ SerializeField ] SharedVector3Notifier notif_enemy_fire_position;
    [ SerializeField ] GameEvent event_enemy_damaged;
    [ SerializeField ] GameEvent event_enemy_died;

  [ Title( "Setup" ) ]
    [ SerializeField ] Material enemy_material_idle;
    [ SerializeField ] Material enemy_material_damaged;
    [ SerializeField ] Material enemy_material_dead;

  [ Title( "Components" ) ]
	[ SerializeField ] Transform enemy_gfx_transform;
	[ SerializeField ] MeshRenderer enemy_body_renderer;
	[ SerializeField ] MeshRenderer enemy_crown_renderer;

    int health_current;
	Vector3 enemy_position_start;

	RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
    void Start()
    {
		notif_level_progress.SharedValue = 1;
		health_current                   = CurrentLevelData.Instance.levelData.enemy_health;

		enemy_position_start = transform.position;
	}
#endregion

#region API
	public void OnGunFireStart()
	{
		recycledTween.Recycle( transform.DOMove(
			notif_enemy_fire_position.sharedValue,
			GameSettings.Instance.enemy_move_duration )
			.SetEase( GameSettings.Instance.enemy_move_ease )
		);
	}

    public void OnGunFired( int damage )
    {
		health_current                   = Mathf.Max( health_current - damage,  0 );
		notif_level_progress.SharedValue = Mathf.InverseLerp( 0, CurrentLevelData.Instance.levelData.enemy_health, health_current );

        if( health_current > 0 )
			EnemyDamagedSequence();
		else
			EnemyDiedSequence();
	}
#endregion

#region Implementation
	void EnemyDamagedSequence()
	{
		enemy_body_renderer.sharedMaterial = enemy_material_damaged;
		recycledTween.Recycle( GameSettings.Instance.enemy_damage_punchScale.CreateTween( enemy_gfx_transform ), OnEnemyDamagedSequenceComplete );
	}

	void EnemyDiedSequence()
	{
		enemy_body_renderer.sharedMaterial  = enemy_material_dead;
		enemy_crown_renderer.sharedMaterial = enemy_material_dead;

		recycledTween.Recycle( GameSettings.Instance.enemy_damage_punchScale.CreateTween( enemy_gfx_transform ) );

		event_enemy_died.Raise();
	}

	void OnEnemyDamagedSequenceComplete()
	{
		enemy_body_renderer.sharedMaterial = enemy_material_idle;
		event_enemy_damaged.Raise();

		recycledTween.Recycle( transform.DOMove(
			enemy_position_start,
			GameSettings.Instance.enemy_move_duration )
			.SetEase( GameSettings.Instance.enemy_move_ease )
		);
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}