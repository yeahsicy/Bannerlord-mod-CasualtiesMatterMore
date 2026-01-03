using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace CasualtiesMatterMore
{
    public class CasualtiesMatterMoreMod : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (gameStarterObject is CampaignGameStarter campaignStarter)
            {
                var models = campaignStarter.Models as List<GameModel>;
                models.RemoveAll(m => m is DiplomacyModel);
                campaignStarter.AddModel(new CustomDiplomacyModel());
            }
        }
    }

    public class CustomDiplomacyModel : DefaultDiplomacyModel
    {
        public override ExplainedNumber GetWarProgressScore(IFaction factionDeclaresWar, IFaction factionDeclaredWar, bool includeDescriptions = false)
        {
            ExplainedNumber result = new ExplainedNumber(0f, includeDescriptions);
            StanceLink stanceWith = factionDeclaresWar.GetStanceWith(factionDeclaredWar);

            if (!stanceWith.IsAtWar)
                return result;

            int num = 0;
            int num2 = 0;
            int num3 = 0;

            foreach (Town fief in factionDeclaredWar.Fiefs)
            {
                if (fief.IsTown)
                {
                    num++;
                }
                else if (fief.IsCastle)
                {
                    num2++;
                }
                num3 += fief.Villages.Count;
            }

            int num4 = factionDeclaredWar.WarPartyComponents.Sum((WarPartyComponent x) => x.Party.NumberOfAllMembers);
            int num5 = factionDeclaredWar.Fiefs.Sum((Town x) => x.GarrisonParty?.Party.NumberOfAllMembers ?? 0) + num4;
            int casualties = stanceWith.GetCasualties(factionDeclaredWar);
            int successfulTownSieges = stanceWith.GetSuccessfulTownSieges(factionDeclaresWar);
            int num6 = stanceWith.GetSuccessfulSieges(factionDeclaresWar) - successfulTownSieges;
            int successfulRaids = stanceWith.GetSuccessfulRaids(factionDeclaresWar);
            int casualties2 = stanceWith.GetCasualties(factionDeclaresWar);
            int successfulTownSieges2 = stanceWith.GetSuccessfulTownSieges(factionDeclaredWar);
            int num7 = stanceWith.GetSuccessfulSieges(factionDeclaredWar) - successfulTownSieges2;
            int successfulRaids2 = stanceWith.GetSuccessfulRaids(factionDeclaredWar);

            // default score towards "Kills": 
            // float value = Math.Max(0f, (float)(casualties - casualties2) / (float)Math.Max(1, num5 * 4) * 500f);
            float value2 = Math.Max(0f, (float)(successfulTownSieges - successfulTownSieges2) / (float)Math.Max(1, num + successfulTownSieges - successfulTownSieges2) * 1000f);
            float value3 = Math.Max(0f, (float)(num6 - num7) / (float)Math.Max(1, num2 + num6 - num7) * 500f);
            float value4 = Math.Max(0f, (float)(successfulRaids - successfulRaids2) / (float)Math.Max(1, num3) * 250f);

            // add more weight on it: 
            var value = Math.Max(0f, (float)(casualties - casualties2) / (float)Math.Max(1, num5 * 4) * 4000f);
            result.Add(value, new TextObject("{=FKe05WtJ}Kills"));

            result.Add(value2, new TextObject("{=bVa5jNbd}Town Sieges"));
            result.Add(value3, new TextObject("{=Sdu2FmgY}Castle Sieges"));
            result.Add(value4, new TextObject("{=w6E2lb09}Raids"));
            result.LimitMin(0f);
            result.LimitMax(750f);
            return result;
        }
    }
}
