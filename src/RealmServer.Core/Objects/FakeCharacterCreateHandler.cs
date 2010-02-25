using System;
using Hazzik.Data;
using Hazzik.Guilds;

namespace Hazzik.Objects {
	public class FakeCharacterCreateHandler {
		private readonly Player player;

		public FakeCharacterCreateHandler(Player player) {
			this.player = player;
		}

		public void Init() {
			//return;
			//{X:-2133,334 Y:135,4584 Z:-9070,833}
			player.PosX = -997.8f;
			player.PosY = -3823.5f;
			player.PosZ = 7f;
			player.MapId = 1;
			player.Facing = 2.083644F;

			player.Speed0 = 2.5F;
			player.Speed1 = 7F;
			player.Speed2 = 4.5F;
			player.Speed3 = 4.722222F;
			player.Speed4 = 2.5F;
			player.Speed5 = 7F;
			player.Speed6 = 4.5F;
			player.TurnRate = 3.141593F;

			player.PowerType = PowerType.Rage;
			player.Health = 50;
			player.MaxHealth = 50;
			player.Power = 100;
			player.Level = 80;
			player.Stat0 = 20;

            player.NativeDisplayId = player.DisplayId = GetDysplaylIdForRace();
			player.FactionTemplate = 0x0000065D;
			player.WatchedFactionIndex = -1;
			var itemTemplateRepository = IoC.Resolve<IItemTemplateRepository>();
			player.Equipment.AutoAdd(Item.Create(itemTemplateRepository.FindById(9936)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(3289)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(23772)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(23772)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(857)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(857)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(857)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(857)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(9943)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(30633)));
			player.BackPack.AutoAdd(Item.Create(itemTemplateRepository.FindById(2901)));

		    player.TrainSpell(78, // Heroic Strike Rank 1
		                      81, // Dodge Passive
		                      107, // Block Passive
		                      196, // One-Handed Axes 
		                      198, // One-Handed Maces 
		                      201, // One-Handed Swords 
		                      203, // Unarmed 
		                      204, // Defense 
		                      522, // SPELLDEFENSE (DND) 
		                      668, // Language Common
                              669, // Language orcish
		                      2382, // Generic 
		                      2457, // Battle Stance 
		                      2479, // Honorless Target 
		                      3050, // Detect 
		                      3365, // Opening 
		                      5301, // Defensive State (DND) 
		                      6233, // Closing 
		                      6246, // Closing 
		                      6247, // Opening 
		                      6477, // Opening 
		                      6478, // Opening 
		                      6603, // Attack 
		                      7266, // Duel 
		                      7267, // Grovel 
		                      7355, // Stuck 
		                      8386, // Attacking 
		                      8737, // Mail 
		                      9077, // Leather 
		                      9078, // Cloth 
		                      9116, // Shield 
		                      9125, // Generic 
		                      20597, // Sword Specialization Racial Passive
		                      20598, // The Human Spirit Racial Passive
		                      20599, // Diplomacy Racial Passive
		                      20600, // Perception Racial
		                      20864, // Mace Specialization Racial Passive
		                      21651, // Opening 
		                      21652, // Closing 
		                      22027, // Remove Insignia 
		                      22810, // Opening - No Text 
		                      32215 // Victorious State
		        );
			player.Coinage = 100 * 100 * 1000;
		    player.GuildMember = new GuildMember(player, new Guild {Id = 1});
		}

	    private uint GetDysplaylIdForRace()
	    {
	        switch(player.Race) {
	            case Races.Human:
	                return 49 + (uint)player.Gender;
	            case Races.Orc:
	                return 51 + (uint)player.Gender;
	            case Races.Dwarf:
	                return 53 + (uint)player.Gender;
	            case Races.NightElf:
	                return 55 + (uint)player.Gender;
	            case Races.Undead:
	                return 57 + (uint)player.Gender;
	            case Races.Tauren:
	                return 59 + (uint)player.Gender;
	            case Races.Gnome:
	                return 1563 + (uint)player.Gender;
	            case Races.Troll:
	                return 1478 + (uint)player.Gender;
	            case Races.BloodElf:
	                return 15476 - (uint)player.Gender;
	            case Races.Draenei:
	                return 16125 + (uint)player.Gender;
	            default:
	                throw new ArgumentOutOfRangeException();
	        }
	    }
	}
}