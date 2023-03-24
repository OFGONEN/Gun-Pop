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
  [ Title( "Shared" ) ]
    [ SerializeField ] SharedReferenceNotifier notif_camera_reference;
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
    }
#endregion

#region Implementation
    void TryToSelect( Vector2 screenPosition ) // %100 hit rate
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
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}