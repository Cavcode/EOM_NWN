using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.CombatService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;
using EOM.Game.Server.Service;


namespace EOM.Game.Server.Feature.AbilityDefinition.Weapons.Gunblades
{
    public class SonicSlashAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            SonicSlash(builder);

            return builder.Build();
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0;
            var willBonus = GetAbilityScore(activator, AbilityType.Willpower);
            var perBonus = GetAbilityScore(activator, AbilityType.Perception);

            switch (level)
            {
                case 1:
                    dmg = willBonus;
                    break;
            }

            dmg += Combat.GetAbilityDamageBonus(activator, SkillType.Gunblades);

            var attackerStat = GetAbilityScore(activator, AbilityType.Willpower);
            var defense = Stat.GetDefense(target, CombatDamageType.Physical, AbilityType.Vitality);
            var defenderStat = GetAbilityScore(target, AbilityType.Vitality);
            var attack = Stat.GetAttack(activator, AbilityType.Willpower, SkillType.Gunblades);
            var damage = Combat.CalculateDamage(
                attack,
                dmg,
                attackerStat,
                defense,
                defenderStat,
                0);
            var delay = GetDistanceBetweenLocations(GetLocation(activator), targetLocation) / 25.0f;

            AssignCommand(activator, () => DoSonicSlash(target, level));

            var eDMG = EffectDamage(damage, DamageType.Force);
            var eVFX = EffectVisualEffect(VisualEffect.Vfx_Fnf_Electric_Explosion);

            Enmity.ModifyEnmity(activator, target, damage);
            CombatPoint.AddCombatPoint(activator, target, SkillType.Gunblades, 3);

            DelayCommand(delay, () =>
            {
                ApplyEffectToObject(DurationType.Instant, eVFX, target);
                ApplyEffectToObject(DurationType.Instant, eDMG, target);
            });
        }

        private static void SonicSlash(AbilityBuilder builder)
        {
            builder.Create(FeatType.SonicSlash, PerkType.SonicSlash)
                .Name("Sonic Slash")
                .Level(1)
                .HasActivationDelay(2f)
                .HasMaxRange(30.0f)
                .RequirementMagick(1)
                .IsCastedAbility()
                .IsHostileAbility()
                .DisplaysVisualEffectWhenActivating(VisualEffect.None)
                .UsesAnimation(Animation.CastOutAnimation)
                .HasImpactAction(ImpactAction);
        }
        private static void DoSonicSlash(uint target, int level)
        {
            VisualEffect rockType = VisualEffect.Vfx_Imp_Mirv_Electric;

            var missile = EffectVisualEffect(rockType);
            ApplyEffectToObject(DurationType.Instant, missile, target);
        }
    }
}