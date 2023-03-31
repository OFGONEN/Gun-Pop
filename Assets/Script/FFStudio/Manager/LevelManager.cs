/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace FFStudio
{
    public class LevelManager : MonoBehaviour
    {
#region Fields
        [ Header( "Fired Events" ) ]
        public GameEvent levelFailedEvent;
        public GameEvent levelCompleted;
        public GameEvent event_selection_enable;
        public GameEvent event_gun_spawn;

        [ Header( "Level Releated" ) ]
        public SharedProgressNotifier notifier_progress;
        public SharedIntNotifier notif_move_count;
#endregion

#region UnityAPI
#endregion

#region API
        // Info: Called from Editor.
        public void LevelLoadedResponse()
        {
			var levelData = CurrentLevelData.Instance.levelData;
            // Set Active Scene.
			if( levelData.scene_overrideAsActiveScene )
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 1 ) );
            else
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 0 ) );

			notif_move_count.SharedValue = CurrentLevelData.Instance.levelData.level_moveCount;
		}

        // Info: Called from Editor.
        public void LevelRevealedResponse()
        {

        }

        // Info: Called from Editor.
        public void LevelStartedResponse()
        {

        }

        public void OnEnemyDied()
        {
			levelCompleted.Raise();
		}

        public void OnEnemyDamaged()
        {
			notif_move_count.SharedValue -= 1;

            if( notif_move_count.sharedValue <= 0 )
				levelFailedEvent.Raise();
            else
            {
				event_gun_spawn.Raise();
				event_selection_enable.Raise();
			}
		}
#endregion

#region Implementation
#endregion
    }
}