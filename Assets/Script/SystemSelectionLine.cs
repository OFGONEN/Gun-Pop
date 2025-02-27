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
	[ SerializeField ] RunTimeSetGun set_gun;

    SelectionLine selection_line;
	[ ShowInInspector, ReadOnly ] List< SelectionLine > selection_line_list;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	public void Init()
	{
		selection_line_list = new List< SelectionLine >( 16 );
	}

    public void OnEnvironmentSelected( RaycastHitData data )
    {
		selection_line.UpdateLine( data.hit_position );
	}

    public void OnSelectionGunInitial( Gun gun )
    {
		SpawnLineOnGun( gun );
	}

	public void OnSelectionGunNew( Gun gun )
	{
		selection_line.Despawn();

		SpawnLineBetweenGuns( set_gun.itemList.PeekPenultimateItem(), gun );
		SpawnLineOnGun( gun );
	}

	public void OnSelectionGunDeselect()
	{
		selection_line_list.ReturnLastItem().Despawn();
		selection_line.ChangePosition( set_gun.itemList.PeekLastItem().transform.position, selection_line.EndPosition );
	}

	public void OnSelectionLineDespawn()
	{
		selection_line_list[ 0 ].Despawn();
		selection_line_list.RemoveAt( 0 );
	}

	public void OnSelectionComplete()
	{
		selection_line.Despawn();
	}

	public void OnSelectionEnable()
	{
        foreach( var line in selection_line_list )
			line.Despawn();

		selection_line_list.Clear();
	}
#endregion

#region Implementation
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
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}