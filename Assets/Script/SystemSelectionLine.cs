/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_selection_line", menuName = "FF/Game/System/Selection Line" ) ]
public class SystemSelectionLine : ScriptableObject
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PoolSelectionLine pool_selection_line;

    SelectionLine selection_line;
	UnityMessage onFingerUp = Extensions.EmptyMethod;
	RaycastHitDataMessage onGunSelected;

	List< Gun > selection_gun_list = new List< Gun >( 16 );
	List< SelectionLine > selection_line_list = new List< SelectionLine >( 16 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Init()
    {
		onGunSelected = FirstGunSelected;
	}
    
    public void OnGunSelected( RaycastHitData data )
    {
		onGunSelected( data );
	}

    public void OnEnvironmentSelected( RaycastHitData data )
    {
		selection_line.UpdateLine( data.hit_position );
	}

    public void OnFingerUp()
    {
		onFingerUp();
	}
#endregion

#region Implementation
    void FirstGunSelected( RaycastHitData data )
    {
        var gun  = data.hit_collider.GetComponent< ComponentHost >().HostComponent as Gun;

		selection_gun_list.Add( gun );
		SpawnLineOnGun( gun );

		onFingerUp    = DespawnLines;
		onGunSelected = NewGunSelected;
	}

    void NewGunSelected( RaycastHitData data )
    {
        var newGun = data.hit_collider.GetComponent< ComponentHost >().HostComponent as Gun;

        if( newGun != selection_gun_list.PeekLastItem() && newGun != selection_gun_list.PeekPenultimateItem() )
        {
			selection_line.Despawn();

			SpawnLineBetweenGuns( selection_gun_list.PeekLastItem(), newGun );
			SpawnLineOnGun( newGun );

			selection_gun_list.Add( newGun );
		}
        else if( newGun != selection_gun_list.PeekLastItem() ) // Equals to Penultimate, Deselect last gun
        {
			selection_line_list.ReturnLastItem().Despawn();
			selection_gun_list.RemoveLastItem();

			var gun = selection_gun_list.PeekLastItem();
			selection_line.ChangePosition( gun.transform.position, selection_line.EndPosition );
		}
    }

    void SpawnLineOnGun( Gun gun )
    {
		var selectionLine = pool_selection_line.GetEntity();
		selectionLine.Spawn( gun.transform.position, gun.GunVisualData.gun_model_color );

		selection_line = selectionLine;
	}

    void SpawnLineBetweenGuns( Gun start, Gun end )
    {
		var selectionLine = pool_selection_line.GetEntity();

		selectionLine.Spawn( start.transform.position, start.GunVisualData.gun_model_color );
		selectionLine.UpdateLine( end.transform.position );

		selection_line_list.Add( selectionLine );
	}

    void DespawnLines()
    {
		onFingerUp    = Extensions.EmptyMethod;
		onGunSelected = FirstGunSelected;

		selection_line.Despawn();

        foreach( var line in selection_line_list )
			line.Despawn();

		selection_line_list.Clear();
		selection_gun_list.Clear();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}