using System;
using Hazzik.Objects.Templates;
using Hazzik.Skills;

namespace Hazzik.Objects {
	public class CharacterCreateHandler {
		private readonly Player _player;

		public CharacterCreateHandler(Player player) {
			_player = player;
		}

		public void Init() {
			//return;
			//{X:-2133,334 Y:135,4584 Z:-9070,833}
			_player.MapId = 0;
			_player.PosX = -9070.833F;
			_player.PosY = -2133.334F;
			_player.PosZ = 135.4584f;
			_player.Facing = 2.083644F;

			_player.Speed0 = 2.5F;
			_player.Speed1 = 7F;
			_player.Speed2 = 4.5F;
			_player.Speed3 = 4.722222F;
			_player.Speed4 = 2.5F;
			_player.Speed5 = 7F;
			_player.Speed6 = 4.5F;
			_player.TurnRate = 3.141593F;

			_player.Race = Races.Orc;
			_player.Classe = Classes.Warrior;
			_player.Gender = GenderType.Male;
			_player.PowerType = PowerType.Rage;
			_player.Health = 50;
			_player.Power = 100;
			_player.Level = 80;
			_player.Stat0 = 20;
			_player.DisplayId = (uint)(51 + _player.Gender);
			_player.NativeDisplayId = (uint)(51 + _player.Gender);
			_player.FactionTemplate = 0x0000065D;
			_player.WatchedFactionIndex = -1;
			var abjurerSBoots9936 = new Abjurer_sBoots9936();
			_player.Inventory[(int)abjurerSBoots9936.CanBeEquipedIn[0]] = ItemFactory.Create(abjurerSBoots9936);
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new AncestralBoots3289());
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new FelIronShells23772());
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new FelIronShells23772());
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			_player.Inventory[_player.Inventory.FindFreeSlot()] = ItemFactory.Create(new Abjurer_sRobe9943());

			_player.TrainSpell(new[] {
				78, // Heroic Strike Rank 1
				81, // Dodge Passive
				107, // Block Passive
				196, // One-Handed Axes 
				198, // One-Handed Maces 
				201, // One-Handed Swords 
				203, // Unarmed 
				204, // Defense 
				522, // SPELLDEFENSE (DND) 
				668, // Language Common 
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
				32215, // Victorious State
			});
			_player.AddSkill(new Skill { Id = (ushort)SkillType.Protection, Value = 1, Cap = 1 });
			_player.AddSkill(new Skill { Id = (ushort)SkillType.Arms, Value = 1, Cap = 1 });
			_player.AddSkill(new Skill { Id = (ushort)SkillType.Fury, Value = 1, Cap = 1 });
			//armor skills
			_player.AddSkill(new Skill { Id = (ushort)SkillType.Cloth, Value = 1, Cap = 1 });
			_player.AddSkill(new Skill { Id = (ushort)SkillType.Leather, Value = 1, Cap = 1 });
			_player.AddSkill(new Skill { Id = (ushort)SkillType.Mail, Value = 1, Cap = 1 });
			_player.AddSkill(new Skill { Id = (ushort)SkillType.Shield, Value = 1, Cap = 1 });
		}
	}
}