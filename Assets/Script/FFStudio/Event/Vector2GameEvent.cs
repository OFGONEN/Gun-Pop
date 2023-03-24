/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Lean.Touch;

namespace FFStudio
{
    [ CreateAssetMenu( fileName = "event_", menuName = "FF/Event/Vector2GameEvent" ) ]
    public class Vector2GameEvent : GameEvent
    {
        public Vector2 eventValue;

        public void Raise( Vector2 value )
        {
			eventValue = value;
			Raise();
		}

        public void Raise( LeanFinger finger )
        {
			eventValue = finger.ScreenPosition;
			Raise();
		}
    }
}
