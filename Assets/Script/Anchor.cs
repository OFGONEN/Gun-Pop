/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using UnityEditor;
using Sirenix.OdinInspector;

public class Anchor : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
	[ SerializeField ] RunTimeSetAnchor set_anchor;
	[ SerializeField ] SystemSpawn system_spawn;

  [ Title( "Setup" ) ]
    [ SerializeField, ReadOnly ] Anchor anchor_next;
    [ SerializeField, ReadOnly ] Anchor anchor_next_left;
    [ SerializeField, ReadOnly ] Anchor anchor_next_right;
    [ SerializeField, ReadOnly ] Vector2Int anchor_coordinate;
    [ SerializeField, ReadOnly ] bool anchor_topMost;

	Gun gun_current = null;
#endregion

#region Properties
    public Anchor AnchorNext           => anchor_next;
    public Anchor AnchorNextLeft       => anchor_next_left;
    public Anchor AnchorNextRight      => anchor_next_right;
    public Vector2Int AnchorCoordinate => anchor_coordinate;
    public Gun ResidingGun             => gun_current;
#endregion

#region Unity API
	void OnEnable()
	{
		set_anchor.AddDictionary( anchor_coordinate.GetUniqueHashCode_PositiveIntegers(), this );
	}

	void OnDisable()
	{
		set_anchor.RemoveDictionary( anchor_coordinate.GetUniqueHashCode_PositiveIntegers() );
	}

	void Start()
	{
		SpawnGun();
	}
#endregion

#region API
	public void OnLevelStart()
	{
		// SpawnGun();
	}

	public void SpawnGun()
	{
		if( gun_current != null ) return;

		var spawnData = system_spawn.GetGunSpawnData();
		spawnData.Item1.Spawn( transform.position, this, spawnData.Item2, spawnData.Item3 );

		gun_current = spawnData.Item1;
	}

	public void OnGunMerged()
	{
		gun_current = null;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    public void SetAnchorNext( Anchor anchor )
    {
		UnityEditor.EditorUtility.SetDirty( this );
		anchor_next = anchor;
	}

	public void SetAnchorNextLeft( Anchor anchor )
	{
		UnityEditor.EditorUtility.SetDirty( this );
		anchor_next_left = anchor;
	}

	public void SetAnchorNextRight( Anchor anchor )
	{
		UnityEditor.EditorUtility.SetDirty( this );
		anchor_next_right = anchor;
	}

    public void SetTopMostAnchor()
    {
		UnityEditor.EditorUtility.SetDirty( this );
		anchor_topMost = true;
	}

	public void SetAnchorCoordinate( Vector2Int coordinate )
	{
		UnityEditor.EditorUtility.SetDirty( this );
		anchor_coordinate = coordinate;
	}

    void OnDrawGizmos()
    {
        if( anchor_next != null )
			Handles.DrawLine( transform.position, anchor_next.transform.position );

		if( anchor_next_left != null )
			Handles.DrawLine( transform.position, anchor_next_left.transform.position );

		if( anchor_next_right != null )
			Handles.DrawLine( transform.position, anchor_next_right.transform.position );
	}
#endif
#endregion
}