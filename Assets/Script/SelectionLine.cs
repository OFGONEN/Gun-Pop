/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using Sirenix.OdinInspector;

public class SelectionLine : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PoolSelectionLine pool_selection_line;

  [ Title( "Components" ) ]
    [ SerializeField ] Line _lineRenderer;
#endregion

#region Properties
	public Vector3 StartPosition => _lineRenderer.Start;
	public Vector3 EndPosition   => _lineRenderer.End;
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( Vector3 position, Color color )
    {
		gameObject.SetActive( true );

		_lineRenderer.Start = position;
		_lineRenderer.End   = position;
		_lineRenderer.Color = color;
	}

    public void UpdateLine( Vector3 position )
    {
		_lineRenderer.End = position;
	}

	public void ChangePosition( Vector3 start, Vector3 end )
	{
		_lineRenderer.Start = start;
		_lineRenderer.End   = end;
	}

    public void Despawn()
    {
		pool_selection_line.ReturnEntity( this );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}