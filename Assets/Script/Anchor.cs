/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class Anchor : MonoBehaviour
{
#region Fields

  [ Title( "Setup" ) ]
    [ SerializeField, ReadOnly ] Anchor anchor_next;
    [ SerializeField, ReadOnly ] Anchor anchor_next_left;
    [ SerializeField, ReadOnly ] Anchor anchor_next_right;
    [ SerializeField, ReadOnly ] Vector2Int anchor_coordinate;
    [ SerializeField, ReadOnly ] bool anchor_topMost;
#endregion

#region Properties
    public Anchor AnchorNext      => anchor_next;
    public Anchor AnchorNextLeft  => anchor_next_left;
    public Anchor AnchorNextRight => anchor_next_right;
#endregion

#region Unity API
#endregion

#region API
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