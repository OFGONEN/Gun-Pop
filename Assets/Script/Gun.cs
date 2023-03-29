/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
#region Fields
    GunData gun_data;
    GunVisualData gun_color_data;
#endregion

#region Properties
    public GunVisualData GunVisualData => gun_color_data;
    public GunData GunData             => gun_data;
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}