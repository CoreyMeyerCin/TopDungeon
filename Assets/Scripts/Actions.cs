using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://www.youtube.com/watch?v=8fcI8W9NBEo
/// </summary>
public static class Actions
{
	public static Action<int> OnExperienceChanged;
	public static Action OnLevelUp;
	public static Action<Weapon> OnWeaponChanged;
}
