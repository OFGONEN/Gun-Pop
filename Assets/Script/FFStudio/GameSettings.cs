/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class GameSettings : ScriptableObject
    {
#region Fields (Settings)
    // Info: You can use Title() attribute ONCE for every game-specific group of settings.
    [ Title( "Selection System" ) ]
		[ LabelText( "Selection Layer" ), Layer() ] public int selection_layer;
		[ LabelText( "Selection Delta" ) ] public float selection_delta;

    [ Title( "Gun" ) ]
		[ LabelText( "Gun Fire Position Move Duration" ) ] public float gun_fire_move_duration;
		[ LabelText( "Gun Fire Position Move Ease" ) ] public Ease gun_fire_move_ease;
		[ LabelText( "Gun Fire Position Shake Duration" ) ] public float gun_fire_shake_duration;
		[ LabelText( "Gun Fire Position Shake Ease" ) ] public Ease gun_fire_shake_ease;
		[ LabelText( "Gun Fire Position Shake Loop Count Range" ) ] public Vector2Int gun_fire_shake_count_range;
		[ LabelText( "Gun Fire Sequence End Delay" ) ] public float gun_fire_sequence_end_delay;
		[ LabelText( "Gun Spawn Punch Scale" ) ] public PunchScaleTween gun_spawn_punchScale;

    [ Title( "Merge" ) ]
		[ LabelText( "Merge Min Count" ) ] public int merge_count;
		[ LabelText( "Merge Size Final" ) ] public float merge_size_final;
		[ LabelText( "Merge Size Step" ) ] public float merge_size_step;
		[ LabelText( "Merge Jump Power" ) ] public float  merge_jump_power;
		[ LabelText( "Merge Jump Duration" ) ] public float  merge_jump_duration;
		[ LabelText( "Merge Jump Delay" ) ] public float  merge_jump_delay;
		[ LabelText( "Merge Jump Ease" ) ] public Ease merge_jump_ease;
    
    [ Title( "Camera" ) ]
        [ LabelText( "Follow Speed (Z)" ), SuffixLabel( "units/seconds" ), Min( 0 ) ] public float camera_follow_speed_depth = 2.8f;
    
    [ Title( "Project Setup", "These settings should not be edited by Level Designer(s).", TitleAlignments.Centered ) ]
        public int game_level_count_max;
        public int game_level_count_min;
		public LevelData[] game_level_data_array;

		// Info: 3 groups below (coming from template project) are foldout by design: They should remain hidden.
		[ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_GameSettings;
        [ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_Components;

        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for ui element"          ) ] public float ui_Entity_Move_TweenDuration;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the fading for ui element"            ) ] public float ui_Entity_Fade_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the scaling for ui element"           ) ] public float ui_Entity_Scale_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for floating ui element" ) ] public float ui_Entity_FloatingMove_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Joy Stick"                                        ) ] public float ui_Entity_JoyStick_Gap;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text relative float height"                ) ] public float ui_PopUp_height;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text float duration"                       ) ] public float ui_PopUp_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Random Spawn Area in Screen" ), SuffixLabel( "percentage" ) ] public float ui_particle_spawn_width; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Duration" ), SuffixLabel( "seconds" ) ] public float ui_particle_spawn_duration; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Ease" ) ] public Ease ui_particle_spawn_ease;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Wait Time Before Target" ) ] public float ui_particle_target_waitTime;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Time" ) ] public float ui_particle_target_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Ease" ) ] public Ease ui_particle_target_ease;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Percentage of the screen to register a swipe"     ) ] public int swipeThreshold;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Safe Area Base Top Offset" ) ] public int ui_safeArea_offset_top = 88;

    [ Title( "UI Particle" ) ]
		[ LabelText( "Random Spawn Area in Screen Witdh Percentage" ) ] public float uiParticle_spawn_width_percentage = 10;
		[ LabelText( "Spawn Movement Duration" ) ] public float uiParticle_spawn_duration = 0.1f;
		[ LabelText( "Spawn Movement Ease" ) ] public DG.Tweening.Ease uiParticle_spawn_ease = DG.Tweening.Ease.Linear;
		[ LabelText( "Target Travel Wait Time" ) ] public float uiParticle_target_waitDuration = 0.16f;
		[ LabelText( "Target Travel Duration" ) ] public float uiParticle_target_duration = 0.4f;
		[ LabelText( "Target Travel Duration (REWARD)" ) ] public float uiParticle_target_duration_reward = 0.85f;
		[ LabelText( "Target Travel Ease" ) ] public DG.Tweening.Ease uiParticle_target_ease = DG.Tweening.Ease.Linear;


        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_height;
        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_duration;
#endregion

#region Fields (Singleton Related)
        static GameSettings instance;

        delegate GameSettings ReturnGameSettings();
        static ReturnGameSettings returnInstance = LoadInstance;

		public static GameSettings Instance => returnInstance();
#endregion

#region Implementation
        static GameSettings LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load< GameSettings >( "game_settings" );

			returnInstance = ReturnInstance;

			return instance;
		}

		static GameSettings ReturnInstance()
        {
            return instance;
        }
#endregion
    }
}
