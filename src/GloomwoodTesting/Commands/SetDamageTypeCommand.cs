using System;
using Gloomwood;
using GloomwoodTesting.Commands;
using UnityEngine;
using Object = UnityEngine.Object;

public class SetDamageTypeCommand : ICommand
{
    public string Name => "set-damage-type";

    public string Description => "Sets the damage type for all ammo types in the currently equipped weapon";

    public string Usage => "set-damage-type [damage type]";
    
    public string Execute(params string[] args)
    {
        var player = Object.FindObjectsByType<Gloomwood.Players.PlayerEntity>(FindObjectsSortMode.None)[0];
        if (player is null) return "Player not found";

        var currentWeapon = player.equipment.currentWeapon;
        if (currentWeapon is null) return "Player is not holding a weapon";

        var ammoTypeGroupConfig = currentWeapon.AmmoTypes;
        if (ammoTypeGroupConfig is null) return "Weapon does not have any ammo type group config";
        
        var ammoTypes = ammoTypeGroupConfig.ammoTypes;
        if (ammoTypes is null) return "Weapon does not have any ammo types";


        if (!Enum.TryParse<DamageTypeFlags>(args[0], out var damageType))
        {
            return "Invalid damage type";
        } 
        
        foreach (var ammoType in ammoTypes)
        {
            if (ammoType is null) continue;
            ammoType.projectileType.Damage.damageType = damageType;
            
            Plugin.Log.LogDebug($"Set damage type to {damageType} for {ammoType.projectileType.EntityName}");
        }
        
        return $"Let there be {damageType}";
    }
}