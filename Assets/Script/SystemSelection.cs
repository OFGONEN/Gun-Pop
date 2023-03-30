/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_selection", menuName = "FF/Game/System/Selection" ) ]
public class SystemSelection : ScriptableObject
{
#region Fields
  [ Title( "Events" ) ]
    [ SerializeField ] GunGameEvent event_selection_gun_inital;
    [ SerializeField ] GunGameEvent event_selection_gun_new;
	[ SerializeField ] GameEvent event_selection_gun_deselect;
	[ SerializeField ] GameEvent event_selection_end;
    [ SerializeField ] RaycastHitGameEvent event_selection_ground;

  [ Title( "Shared" ) ]
    [ SerializeField ] SharedReferenceNotifier notif_camera_reference;
	[ SerializeField ] RunTimeSetGun set_gun;

	Vector2Delegate onFingerUpdate = Extensions.EmptyMethod;
	UnityMessage    onFingerUp     = Extensions.EmptyMethod;
	Camera _camera;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnLevelStart()
    {
        _camera = ( notif_camera_reference.sharedValue as Transform ).GetComponent< Camera >();

		onFingerUpdate = FingerDown;
	}

	public void OnFingerUpdate( Vector2 screenPosition )
	{
		onFingerUpdate( screenPosition );
	}

	public void OnFingerUp()
	{
		onFingerUp();
	}
#endregion

#region Implementation
	void FingerUp()
	{
		onFingerUpdate = FingerDown;
		onFingerUp     = Extensions.EmptyMethod;

		set_gun.itemList.Clear();
		event_selection_end.Raise();
	}
	void FingerDown( Vector2 screenPosition )
	{
		var hit = TryToSelect( screenPosition );

		if( hit.collider.tag == "Gun" )
		{
			var gun = hit.collider.GetComponent< ComponentHost >().HostComponent as Gun;

			set_gun.itemList.Add( gun );
			event_selection_gun_inital.Raise( gun );

			onFingerUpdate = FingerUpdate;
			onFingerUp     = FingerUp;
		}
	}

	void FingerUpdate( Vector2 screenPosition )
	{
		var hit = TryToSelect( screenPosition );

		if( hit.collider.tag == "Gun" )
		{
			var gun = hit.collider.GetComponent< ComponentHost >().HostComponent as Gun;

			if( gun != set_gun.itemList.PeekLastItem() && gun == set_gun.itemList.PeekPenultimateItem() ) // Deselect last selected gun
			{
				set_gun.itemList.RemoveLastItem();
				event_selection_gun_deselect.Raise();
			}
			else
			{
				for( var i = 0; i < set_gun.itemList.Count; i++ )
				{
					if( set_gun.itemList[ i ] == gun )
						return; // Do nothing
				}

				var lastSelectedGun = set_gun.itemList.PeekLastItem();
				var distance        = lastSelectedGun.AnchorCoordiante - gun.AnchorCoordiante;

				if( gun.GunVisualData == lastSelectedGun.GunVisualData && Mathf.Abs( distance.x ) <= 1 && Mathf.Abs( distance.y ) <= 1 ) // New and Same Colored Gun, select it.
				{
					set_gun.itemList.Add( gun );
					event_selection_gun_new.Raise( gun );
				}
			}
		}
		else
			event_selection_ground.Raise( hit.point, hit.collider );
	}

    RaycastHit TryToSelect( Vector2 screenPosition ) // %100 hit rate
    {
		var worldPositionNear = _camera.ScreenToWorldPoint( screenPosition.ConvertToVector3( _camera.nearClipPlane ) );
		var worldPositionFar  = _camera.ScreenToWorldPoint( screenPosition.ConvertToVector3( _camera.farClipPlane ) );

		var direction = ( worldPositionFar - worldPositionNear ).normalized;
		var layerMask = 1 << GameSettings.Instance.selection_layer;

		RaycastHit hitInfo;

		Physics.Raycast(
			worldPositionNear,
			direction,
            out hitInfo,
			GameSettings.Instance.selection_delta,
            layerMask
        );

		return hitInfo;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}