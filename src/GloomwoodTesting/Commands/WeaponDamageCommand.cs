using GloomwoodTesting.Commands;
using UnityEngine;

public class WeaponDamageCommand : ICommand
{
    public string Name => "weapondamage";
    public string Description => "Set the current weapons damage to a new number";
    public string Usage => "weapondamage [amount]";

    public string Execute(params string[] args)
    {
        if (!int.TryParse(args[0], out var amount))
        {
            return $"Input '{args[0]}' is not a recognizable value.";
        }
        
        var player = Object.FindObjectsByType<Gloomwood.Players.PlayerEntity>(FindObjectsSortMode.None)[0];
        if (player is null) return "Player not found";

        var currentWeapon = player.equipment.currentWeapon;
        if (currentWeapon is null) return "Player is not holding a weapon";

        var ammoTypeGroupConfig = currentWeapon.AmmoTypes;
        if (ammoTypeGroupConfig is null) return "Weapon does not have any ammo type group config";
        
        var ammoTypes = ammoTypeGroupConfig.ammoTypes;
        if (ammoTypes is null) return "Weapon does not have any ammo types";

        foreach (var ammoType in ammoTypes)
        {
            if (ammoType is null) continue;
            ammoType.ProjectileType.Damage.amountMin = amount;
            Plugin.Log.LogDebug($"Set minimum damage for {ammoType.projectileType.EntityName}");
        }
        
        return "Set equipped weapon damage";
    }
}