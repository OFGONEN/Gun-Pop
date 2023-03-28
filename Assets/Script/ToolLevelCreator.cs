/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using FFStudio;
using UnityEditor;

public class ToolLevelCreator : SerializedMonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
	[ SerializeField ] Anchor anchor_prefab;
	[ SerializeField ] Vector2 anchor_offset;
	[ SerializeField ] Vector2Int level_size;

  [ Title( "Output" ) ]
    [ TableMatrix( DrawElementMethod = "DrawCell" ) ]
	[ SerializeField ] bool[,] level_table;
	[ SerializeField, ReadOnly ] Anchor[,] level_anchor_array;

	[ SerializeField ] List< Anchor > anchor_list = new List< Anchor >( 32 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	[ Button() ]
	public void InitTable( bool value )
	{
		UnityEditor.EditorUtility.SetDirty( this );

		level_table        = new bool[ level_size.x, level_size.y ];
		level_anchor_array = new Anchor[ level_size.x, level_size.y ];

		for( var x = 0; x < level_size.x; x++ )
			for( var y = 0; y < level_size.y; y++ )
				level_table[ x, y ] = value;
	}

	[ Button() ]
	public void CreateLevel()
	{
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

		foreach( var anchor in anchor_list )
			DestroyImmediate( anchor.gameObject );

		anchor_list.Clear();

		Vector3 offset = new Vector3( ( level_size.x - 1 ) * anchor_offset.x / 2f * -1f, ( level_size.y - 1 ) * anchor_offset.y / 2f, 0 );
		var parent = GameObject.Find( "parent_spawn" ).transform;

		for( var x = 0; x < level_size.x; x++ )
		{
			for( var y = 0; y < level_size.y; y++ )
			{
				if( !level_table[ x, y ] ) continue;

				var anchor = PrefabUtility.InstantiatePrefab( anchor_prefab ) as Anchor;
				anchor.transform.position = offset + new Vector3( x * anchor_offset.x, y * anchor_offset.y * -1f, 0 );
				anchor.transform.parent   = parent;

				level_anchor_array[ x, y ] = anchor;
				anchor_list.Add( anchor );
			}
		}
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
	private static bool DrawCell( Rect rect, bool value )
	{
		if( Event.current.type == EventType.MouseDown && rect.Contains( Event.current.mousePosition ) )
        {
			value       = !value;
			GUI.changed = true;
			Event.current.Use();
		}

		UnityEditor.EditorGUI.DrawRect(
			rect.Padding( 1 ),
			value
				? new Color( 0.1f, 0.8f, 0.2f )
				: new Color( 0, 0, 0,    0.5f ) );

        return value;
	}
#endif
#endregion
}