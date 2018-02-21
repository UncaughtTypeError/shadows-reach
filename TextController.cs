using UnityEngine;
using UnityEngine.UI; // import namespace to use "Text" type
using System.Collections;

public class TextController : MonoBehaviour {

	#region Configuration and Construct
	// Access modifier (public), Type (Text), Variable name (varText)
	public Text varText;
	
	// States
	private enum States {
		nullState,
		game_start, 
		game_finish,
		game_over, 
		room, 
		toilet_0, 
		toilet_1, 
		toilet_2,  
		vent_0, 
		vent_1, 
		vent_2,
		mirror_0, 
		mirror_1_red, 
		mirror_1_blue, 
		mirror_2_red,  
		mirror_2_blue, 
		mirror_3,
		bed_0, 
		bed_1,
		door_0,
		door_1,
		exit_room,
		hallway_left,
		hallway_right,
		hallway_flight_0,
		hallway_fight_0,
		hallway_flight_1,
		hallway_fight_1,
		hallway_encounter,
		hallway_exit,
		inventory,
		achievements
	};
	private States currentState;
	
	// Transient States
	private enum transientStates {
		nullState,
		toilet_0_after_action,
		toilet_1_after_action,
		vent_0_after_action,
		mirror_1_red_after_action,
		mirror_1_blue_after_action,
		bed_0_after_action_0,
		bed_0_after_action_1,
		bed_0_after_action_2,
		door_0_after_action,
		door_1_after_action,
		hallway_abandon_fight_0,
		hallway_abandon_fight_1,
		hallway_fight_0_death,
		hallway_fight_1_death,
		hallway_fight_0_weapon_use,
		hallway_fight_1_weapon_use,
		red_focus_toilet,
		red_focus_bed,
		red_focus_vent,
		red_focus_door,
		leave_red_focus_confirmation,
		death,
		// Items
		inapplicable_item_use,
		shiv_item_use_cistern,
		shiv_item_use_vent,
		shiv_item_use_fight_0,
		shiv_item_use_fight_1,
		wire_hook_item_use,
		handheld_emp_item_use,
		inapplicable_handheld_emp_item_use,
		instructions_note_item_examine,
		instructions_note_item_read,
		wire_coathanger_item_modify,
		wire_coathanger_item_modified,
		// Achievements
		exit_room_achievement,
		took_the_red_pill_achievement,
		took_the_blue_pill_achievement,
		game_over_bad_ending_achievement,
		game_over_good_ending_achievement,
		game_completion_achievement,
		// Achievements Descriptions
		achievement_escape_artist_description,
		achievement_took_the_red_pill_description,
		achievement_took_the_blue_pill_description,
		achievement_game_over_bad_ending_description,
		achievement_game_over_good_ending_description,
		achievement_game_completed_description
	};
	private transientStates currentTransientState;
	
	private enum Items {
		shiv, 
		wire_hook, 
		handheld_emp, 
		instructions_note, 
		wire_coathanger
	};
	private string returnToState;
	
	// Conditional State Options
	private bool 	additionalOption_state = false;
	private string 	state_additional_option,
					gameAchievements;
	
	// Conditional State Cases
	private bool 	toilet_flushed = false,
					red_tap_opened = false,
					blue_tap_opened = false,
					stateToilet_1 = false,
					stateToilet_2 = false,
					stateVent_1 = false,
					stateVent_2 = false,
					stateBed_1 = false,
					stateMirror_3 = false,
					stateDoor_1 = false,
					redFocus_ItemReveal_bed = false;
	
	// Red Focus State commands
	private string 	redFocus_toilet,
					redFocus_bed,
					redFocus_vent;
	
	// Inventory Item booleans
	private bool 	inInventory_shiv = false,
					inInventory_wire_hook = false,
					inInventory_handheld_emp = false,
					inInventory_instructions_note = false,
					inInventory_wire_coathanger = false;
	
	// Inventory Item commands
	private string 	itemShiv,
					itemWireHook,
					itemHandheldEMP,
					itemInstructionsNote,
					itemWireCoathanger;
	
	// Achievements				
	private bool	achievement_escape_artist = false,
					achievement_took_the_red_pill = false,
					achievement_took_the_blue_pill = false,
					achievement_game_over_bad_ending = false,
					achievement_game_over_good_ending = false,
					achievement_game_completed = false;
	
	// Achievement Menu commands
	private string 	escapeArtist,
					tookTheRedPill,
					tookTheBluePill,
					gameOverBadEnding,
					gameOverGoodEnding,
					gameCompleted;
	#endregion
					
					
	#region Persistant Handler Methods
	// Use this for initialization
	void Start () {
		// set initialization state
		currentState = States.game_start;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Escape)) { // escape to Start menu at anytime
			currentState = States.game_start;
		}
		
		// States
		if(currentState == States.game_start) 				{ state_game_start(); } 
		else if (currentState == States.game_finish) 		{ state_game_finish(); } 
		else if (currentState == States.game_over) 			{ state_game_over(); } 
		else if (currentState == States.room) 				{ state_room(); }  
		else if (currentState == States.toilet_0) 			{ state_toilet_0(); }
		else if (currentState == States.toilet_1) 			{ state_toilet_1(); }
		else if (currentState == States.toilet_2) 			{ state_toilet_2(); }
		else if (currentState == States.vent_0) 			{ state_vent_0(); }
		else if (currentState == States.vent_1) 			{ state_vent_1(); }
		else if (currentState == States.vent_2) 			{ state_vent_2(); }
		else if (currentState == States.mirror_0) 			{ state_mirror_0(); }
		else if (currentState == States.mirror_1_red) 		{ state_mirror_1_red(); }
		else if (currentState == States.mirror_1_blue) 		{ state_mirror_1_blue(); }
		else if (currentState == States.mirror_2_red) 		{ state_mirror_2_red(); }
		else if (currentState == States.mirror_2_blue) 		{ state_mirror_2_blue(); }
		else if (currentState == States.mirror_3) 			{ state_mirror_3(); }
		else if (currentState == States.bed_0) 				{ state_bed_0(); }
		else if (currentState == States.bed_1) 				{ state_bed_1(); }
		else if (currentState == States.door_0) 			{ state_door_0(); }
		else if (currentState == States.door_1) 			{ state_door_1(); }
		else if (currentState == States.exit_room) 			{ state_exit_room(); }
		else if (currentState == States.hallway_left) 		{ state_hallway_left(); }
		else if (currentState == States.hallway_right) 		{ state_hallway_right(); }
		else if (currentState == States.hallway_flight_0) 	{ state_hallway_flight_0(); }
		else if (currentState == States.hallway_flight_1) 	{ state_hallway_flight_1(); }
		else if (currentState == States.hallway_fight_0) 	{ state_hallway_fight_0(); }
		else if (currentState == States.hallway_fight_1) 	{ state_hallway_fight_1(); }
		else if (currentState == States.hallway_encounter) 	{ state_hallway_encounter(); }
		else if (currentState == States.hallway_exit) 		{ state_hallway_exit(); }
		else if (currentState == States.inventory) 			{ state_inventory(); } 
		else if (currentState == States.achievements) 		{ state_achievements(); } 
		
		// Transient States
		if (currentTransientState == transientStates.nullState) {}
		else if(currentTransientState == transientStates.toilet_0_after_action) 		{ transientState_toilet_0_after_action(); } 
		else if(currentTransientState == transientStates.toilet_1_after_action) 		{ transientState_toilet_1_after_action(); }
		else if(currentTransientState == transientStates.vent_0_after_action) 			{ transientState_vent_0_after_action(); }
		else if(currentTransientState == transientStates.mirror_1_red_after_action) 	{ transientState_mirror_1_red_after_action(); }
		else if(currentTransientState == transientStates.mirror_1_blue_after_action) 	{ transientState_mirror_1_blue_after_action(); }
		else if(currentTransientState == transientStates.bed_0_after_action_0) 			{ transientState_bed_0_after_action_0(); }
		else if(currentTransientState == transientStates.bed_0_after_action_1) 			{ transientState_bed_0_after_action_1(); }
		else if(currentTransientState == transientStates.bed_0_after_action_2) 			{ transientState_bed_0_after_action_2(); }
		else if(currentTransientState == transientStates.door_0_after_action) 			{ transientState_door_0_after_action(); }
		else if(currentTransientState == transientStates.door_1_after_action) 			{ transientState_door_1_after_action(); }
		else if(currentTransientState == transientStates.hallway_abandon_fight_0) 		{ transientState_hallway_abandon_fight_0(); }
		else if(currentTransientState == transientStates.hallway_abandon_fight_1) 		{ transientState_hallway_abandon_fight_1(); }
		else if(currentTransientState == transientStates.hallway_fight_0_death) 		{ transientState_hallway_fight_0_death(); }
		else if(currentTransientState == transientStates.hallway_fight_1_death) 		{ transientState_hallway_fight_1_death(); }
		else if(currentTransientState == transientStates.hallway_fight_0_weapon_use) 	{ transientState_hallway_fight_0_weapon_use(); }
		else if(currentTransientState == transientStates.hallway_fight_1_weapon_use) 	{ transientState_hallway_fight_1_weapon_use(); }
		else if(currentTransientState == transientStates.red_focus_toilet) 				{ transientState_red_focus_toilet(); }
		else if(currentTransientState == transientStates.red_focus_bed) 				{ transientState_red_focus_bed(); }
		else if(currentTransientState == transientStates.red_focus_vent) 				{ transientState_red_focus_vent(); }
		else if(currentTransientState == transientStates.red_focus_door) 				{ transientState_red_focus_door(); }
		else if(currentTransientState == transientStates.leave_red_focus_confirmation) 	{ transientState_leave_red_focus_confirmation(); }
		else if(currentTransientState == transientStates.death) 						{ transientState_death(); }
		// Transient Item States
		else if(currentTransientState == transientStates.inapplicable_item_use) 				{ transientState_inapplicable_item_use(); } 
		else if(currentTransientState == transientStates.shiv_item_use_cistern) 				{ transientState_shiv_item_use_cistern(); } 
		else if(currentTransientState == transientStates.shiv_item_use_vent) 					{ transientState_shiv_item_use_vent(); }
		else if(currentTransientState == transientStates.shiv_item_use_fight_0) 				{ transientState_shiv_item_use_fight_0(); }
		else if(currentTransientState == transientStates.shiv_item_use_fight_1) 				{ transientState_shiv_item_use_fight_1(); }
		else if(currentTransientState == transientStates.wire_hook_item_use) 					{ transientState_wire_hook_item_use(); }
		else if(currentTransientState == transientStates.handheld_emp_item_use) 				{ transientState_handheld_emp_item_use(); }
		else if(currentTransientState == transientStates.inapplicable_handheld_emp_item_use) 	{ transientState_inapplicable_handheld_emp_item_use(); }
		else if(currentTransientState == transientStates.instructions_note_item_examine) 		{ transientState_instructions_note_item_examine(); }
		else if(currentTransientState == transientStates.instructions_note_item_read) 			{ transientState_instructions_note_item_read(); }
		else if(currentTransientState == transientStates.wire_coathanger_item_modify) 			{ transientState_wire_coathanger_item_modify(); }
		else if(currentTransientState == transientStates.wire_coathanger_item_modified) 		{ transientState_wire_coathanger_item_modified(); }
		// Transient Achievement States
		else if(currentTransientState == transientStates.exit_room_achievement) 			{ transientState_exit_room_achievement(); }
		else if(currentTransientState == transientStates.took_the_red_pill_achievement) 	{ transientState_took_the_red_pill_achievement(); }
		else if(currentTransientState == transientStates.took_the_blue_pill_achievement) 	{ transientState_took_the_blue_pill_achievement(); }
		else if(currentTransientState == transientStates.game_over_bad_ending_achievement) 	{ transientState_game_over_bad_ending_achievement(); }
		else if(currentTransientState == transientStates.game_over_good_ending_achievement) { transientState_game_over_good_ending_achievement(); }
		else if(currentTransientState == transientStates.game_completion_achievement) 		{ transientState_game_completion_achievement(); }
		// Transient Achievement Description States
		else if(currentTransientState == transientStates.achievement_escape_artist_description) 		{ transientState_achievement_escape_artist_description(); }
		else if(currentTransientState == transientStates.achievement_took_the_red_pill_description) 	{ transientState_achievement_took_the_red_pill_description(); }
		else if(currentTransientState == transientStates.achievement_took_the_blue_pill_description) 	{ transientState_achievement_took_the_blue_pill_description(); }
		else if(currentTransientState == transientStates.achievement_game_over_bad_ending_description) 	{ transientState_achievement_game_over_bad_ending_description(); }
		else if(currentTransientState == transientStates.achievement_game_over_good_ending_description) { transientState_achievement_game_over_good_ending_description(); }
		else if(currentTransientState == transientStates.achievement_game_completed_description) 		{ transientState_achievement_game_completed_description(); }
		
		// print(currentState);
	}
	#endregion
	
	#region State Handler Methods
	
	#region Flow State Handler Methods
	void resetBooleans () {
	
		// Conditional State Options
		additionalOption_state = false;
		
		// Conditional State Cases
		toilet_flushed = false;
		red_tap_opened = false;
		blue_tap_opened = false;
		stateToilet_1 = false;
		stateToilet_2 = false;
		stateVent_1 = false;
		stateVent_2 = false;
		stateBed_1 = false;
		stateMirror_3 = false;
		stateDoor_1 = false;
		redFocus_ItemReveal_bed = false;
		
		// Inventory Item booleans
		inInventory_shiv = false;
		inInventory_wire_hook = false;
		inInventory_handheld_emp = false;
		inInventory_instructions_note = false;
		inInventory_wire_coathanger = false;
		
	}
	
	void state_game_start () {
		
		resetBooleans();
	
		if(achievement_game_completed) {
			gameAchievements = "Press [A] to view [A]chievements\n\n";
		}
	
		varText.text = "Start a new game\n\n" +
			"Press [SPACE] to begin...\n\n" +
			gameAchievements;
	
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentState = States.room;
		}
		else if(Input.GetKeyDown(KeyCode.A)) {
			currentState = States.achievements;
		}
	
	}
	
	void state_game_finish () {
		
		varText.text = "Congratulations\n\n" +
			"You have successfully escaped.\n\n" +
			"Press [SPACE] to continue...\n\n\n" +
			"Story and code by: Nathan Shepherd";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentState = States.game_start;
		}
		
	}
	
	void state_game_over () {
		
		varText.text = "Game Over\n\n" +
			"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentState = States.game_start;
		}
		
	}
	#endregion
	
	#region Menu State Handler Methods
	void state_achievements () {
	
		if(achievement_escape_artist) {
			escapeArtist = "~ Escape Artist: Press [E] to view...\n";
		}
		if(achievement_took_the_red_pill) {
			tookTheRedPill = "~ Took The Red Pill: Press [R] to view...\n";
		}
		if(achievement_took_the_blue_pill) {
			tookTheBluePill = "~ Took The Blue Pill: Press [B] to view...\n";
		}
		if(achievement_game_over_bad_ending) {
			gameOverBadEnding = "~ The Story Ends: Press [S] to view...\n";
		}
		if(achievement_game_over_good_ending) {
			gameOverGoodEnding = "~ Deeper Down The Rabbit Hole: Press [D] to view...\n";
		}
		if(achievement_game_completed) {
			gameCompleted = "~ You stay in Wonderland: Press [W] to view...\n";
		}
		
		varText.text = "Achievements:\n\n" +
			escapeArtist +
			tookTheRedPill +
			tookTheBluePill +
			gameOverBadEnding +
			gameOverGoodEnding +
			gameCompleted +
			"\nPress [ENTER] to retrun to menu...";
		
		if(Input.GetKeyDown(KeyCode.Return)) {
			currentState = States.game_start;
		}
		else if(Input.GetKeyDown(KeyCode.E)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.achievement_escape_artist_description; 
		}
		else if(Input.GetKeyDown(KeyCode.R)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.achievement_took_the_red_pill_description; 
		}
		else if(Input.GetKeyDown(KeyCode.B)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.achievement_took_the_blue_pill_description; 
		}
		else if(Input.GetKeyDown(KeyCode.S)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.achievement_game_over_bad_ending_description; 
		}
		else if(Input.GetKeyDown(KeyCode.D)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.achievement_game_over_good_ending_description; 
		}
		else if(Input.GetKeyDown(KeyCode.W)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.achievement_game_completed_description; 
		}
		
	}
	
	void return_to_state() {
		switch (returnToState)
		{
		case "room":
			currentState = States.room;
			break;
		case "toilet_0":
			currentState = States.toilet_0;
			break;
		case "toilet_1":
			currentState = States.toilet_1;
			break;
		case "toilet_2":
			currentState = States.toilet_2;
			break;
		case "vent_0":
			currentState = States.vent_0;
			break;
		case "vent_1":
			currentState = States.vent_1;
			break;
		case "vent_2":
			currentState = States.vent_2;
			break;
		case "bed_0":
			currentState = States.bed_0;
			break;
		case "bed_1":
			currentState = States.bed_1;
			break;
		case "mirror_0":
			currentState = States.mirror_0;
			break;
		case "mirror_2_red":
			currentState = States.mirror_2_red;
			break;
		case "door_0":
			currentState = States.door_0;
			break;
		case "door_1":
			currentState = States.door_1;
			break;
		case "exit_room":
			currentState = States.exit_room;
			break;
		case "hallway_fight_0":
			currentState = States.hallway_fight_0;
			break;
		case "hallway_fight_1":
			currentState = States.hallway_fight_1;
			break;
		default:
			currentState = States.room;
			break;
		}
	}
	
	void state_inventory () {
	
		if(inInventory_shiv) { 
			itemShiv = "Shiv (press [S] to use)\n\n";
		} else { itemShiv = ""; }
		if(inInventory_wire_hook) { 
			itemWireHook = "Wire Hook (press [W] to use)\n\n";
		} else { itemWireHook = ""; }
		if(inInventory_handheld_emp) { 
			itemHandheldEMP = "Handheld EMP (press [H] to use)\n\n";
		} else { itemHandheldEMP = ""; }
		if(inInventory_instructions_note) { 
			itemInstructionsNote = "Instructions Note (press [E] to examine)\n\n";
		} else { itemInstructionsNote = ""; }
		if(inInventory_wire_coathanger) { 
			itemWireCoathanger = "Wire Coathanger (press [I] to investigate)\n\n";
		} else { itemWireCoathanger = ""; }
	
		varText.text = "Inventory\n\n" +
			itemShiv +
			itemWireHook +
			itemHandheldEMP + 
			itemInstructionsNote +
			itemWireCoathanger +
			"Press [R] to [R]eturn...";
			
			if(inInventory_shiv && Input.GetKeyDown(KeyCode.S)) { item_shiv(); }
			if(inInventory_wire_hook && Input.GetKeyDown(KeyCode.W)) { item_wire_hook(); }
			if(inInventory_handheld_emp && Input.GetKeyDown(KeyCode.H)) { item_handheld_emp(); }
			if(inInventory_instructions_note && Input.GetKeyDown(KeyCode.E)) { item_instructions_note(); }
			if(inInventory_wire_coathanger && Input.GetKeyDown(KeyCode.I)) { item_wire_coathanger(); }
			
			if(Input.GetKeyDown(KeyCode.R)) {
				return_to_state();
			}
		
	}
	#endregion
	
	#region Inventory Item State Handler Methods
	void item_shiv() { 
		
		currentState = States.nullState; 
		
		if(returnToState == "toilet_1") { currentTransientState = transientStates.shiv_item_use_cistern; }
		else if(returnToState == "vent_0" && returnToState != "vent_1") { currentTransientState = transientStates.shiv_item_use_vent; }
		else if(returnToState == "hallway_fight_0") { currentTransientState = transientStates.shiv_item_use_fight_0; }
		else if(returnToState == "hallway_fight_1") { currentTransientState = transientStates.shiv_item_use_fight_1; }
		else { currentTransientState = transientStates.inapplicable_item_use; }
		
	}
	void item_wire_hook() { 
		
		currentState = States.nullState; 
		
		if(returnToState == "vent_1") { currentTransientState = transientStates.wire_hook_item_use; }
		else { currentTransientState = transientStates.inapplicable_item_use; }
		
	}
	void item_handheld_emp() { 
		
		currentState = States.nullState; 
		
		if(returnToState == "door_0") { currentTransientState = transientStates.handheld_emp_item_use; }
		else { currentTransientState = transientStates.inapplicable_handheld_emp_item_use; }
		
	}
	void item_instructions_note() { 
		
		currentState = States.nullState; 
		
		currentTransientState = transientStates.instructions_note_item_examine;
		
	}
	void item_wire_coathanger() { 
		
		currentState = States.nullState; 
		
		currentTransientState = transientStates.wire_coathanger_item_modify;
		
	}
	#endregion
	
	#region State Handler Methods
	void state_room () {
		
		varText.text = "You find yourself in a bright windowless room, the light above you showers down a heavy " + 
				"glow. There is a door with no handles or locks, and a small vent sealed shut with a steel grate above " +
				"a bed that stands on slender metal legs; the sheets are piled in disarray on the mattress. A stifling " +
				"odour spoils the air, you notice the likely source; a steel latrine in one corner of the room; the light " +
				"reflects off its polished surface. Beside the latrine there is a steel basin with a cracked mirror fixed " +
				"to the wall above it.\n\n" +
				"Press [T] to investigate the [T]oilet\n" +
				"Press [B] to investigate the [B]ed\n" +
				"Press [M] to investigate the [M]irror fixed above the basin\n" +
				"Press [D] to investigate the [D]oor\n" +
				"Press [V] to investigate the small [V]entilation shaft";
		
		// set states
		if(Input.GetKeyDown(KeyCode.T)) {
			if(stateToilet_1) { 
				currentState = States.toilet_1; 
			}
			else if(stateToilet_2) { 
				currentState = States.toilet_2; 
			}
			else {
				currentState = States.toilet_0; 
			}
		}
		else if(Input.GetKeyDown(KeyCode.B)) { 
			if(stateBed_1) { 
				currentState = States.bed_1; 
			}
			else {
				currentState = States.bed_0; 
			}
		}
		else if(Input.GetKeyDown(KeyCode.M)) { 
			if(stateMirror_3) { 
				currentState = States.mirror_3; 
			}
			else {
				currentState = States.mirror_0; 
			}
		}
		else if(Input.GetKeyDown(KeyCode.D)) { 
			if(stateDoor_1) { 
				currentState = States.door_1; 
			}
			else {
				currentState = States.door_0; 
			}
		}
		else if(Input.GetKeyDown(KeyCode.V)) { 
			if(stateVent_1) { 
				currentState = States.vent_1; 
			}
			else if(stateVent_2) { 
				currentState = States.vent_2; 
			}
			else {
				currentState = States.vent_0; 
			}
		}
	
	}
	
	void state_toilet_0 () {
		
		varText.text = "You have no doubt, the toilet is definitely the source of the fetid smell. The bowl is " +
				"filled with a thick dark liquid...\n\n" +
				"Press [F] to [F]lush\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.F)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.toilet_0_after_action; 
		}
		
	}
	
	void transientState_toilet_0_after_action () {
		
		toilet_flushed = true;
	
		varText.text = "The toilet flushes and the bowl empties of the fetid liquid. You hear a metallic " +
				"clanging from within the cistern.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.toilet_1; 
		}
		
	}
	
	void state_toilet_1 () {
	
		stateToilet_1 = true;
		
		varText.text = "The bowl of the toilet remains empty, the cistern did not refill. The odour seems to be " +
				"dissapating now.\n\n" +
				"Press [C] to investigate the [C]istern\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.C)) {
			currentState = States.nullState; 
			currentTransientState = transientStates.toilet_1_after_action; 
		}
		
	}
	
	void transientState_toilet_1_after_action () {
	
		/* Investigation creates the need for a new required tool */
		if(additionalOption_state == false) { // additional state option should now be available (for States.bed_0)
			additionalOption_state = true;
		}
	
		varText.text = "The cistern is sealed tightly shut. You could probably pry it open with something " +
				"flat and sturdy.\n\n" +
				"Press [I] to open up [I]nventory\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.toilet_1; 
		}
		else if(Input.GetKeyDown(KeyCode.I)) { 
			returnToState = "toilet_1";
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void state_toilet_2 () {
		
		stateToilet_2 = true;
		
		varText.text = "The bowl and the cistern of the toilet remains empty. You can still smell the faint traces " +
				"of the odour, but it is no longer unbearable.\n\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		
	}
	
	void state_vent_0 () {
		
		varText.text = "Using the bed to elevate yourself, you can peer into the ventilation shaft. You notice the shape of a " +
				"small device with a flashing LED just beyond arm's reach. There seems to be a folded piece of paper attached " +
				"to it as well. But there is no way to extract it with the grate sealing the shaft.\n\n" +
				"Press [G] to examine the [G]rate\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.G)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.vent_0_after_action; 
		}
		
	}
	
	void transientState_vent_0_after_action () {
		
		/* Investigation creates the need for a new required tool */
		if(additionalOption_state == false) { // additional state option should now be available (for States.bed_0)
			additionalOption_state = true;
		}
		
		varText.text = "The grate is sealed tightly shut. But it looks like the screws holding it in place could be loosened\n\n" +
				"Press [I] to open up [I]nventory\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.vent_0; 
		}
		else if(Input.GetKeyDown(KeyCode.I)) { 
			returnToState = "vent_0";
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void state_vent_1 () {
		
		stateVent_1 = true;
		
		varText.text = "With the grate removed, you can now reach into the shaft, but the device is still out of arm's " + 
				"reach. If you had something that could extend your reach you would be able to snag it and drag it towards you.\n\n" +
				"Press [I] to open up [I]nventory\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.I)) { 
			returnToState = "vent_1";
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void state_vent_2 () {
		
		stateVent_2 = true;
		
		varText.text = "Using the bed to elevate yourself, you can peer into the empty ventilation shaft that " + 
				"stretches into darkness. If you listen carefully you can hear faint and distant mecahnical sounds.\n\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		
	}
	
	void state_mirror_0 () {
		
		varText.text = "You don't really recognise the face you see in the mirror. How can that be?\n\n" + 
				"Two small and empty disposable plastic medicine cups sit below each faucet in the sink.\n" +
				"Their placements strike you as curiously intensional.\n\n" +
				"Press [R] to open the tap marked with a [R]ed indicator\n" +
				"Press [B] to open the tap marked with a [B]lue indicator\n" +
				"Press [SPACE] to return to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.R)) { currentState = States.mirror_1_red; }
		else if(Input.GetKeyDown(KeyCode.B)) { currentState = States.mirror_1_blue; }
		
	}
	
	void state_mirror_1_red () {
		
		if(!red_tap_opened && !blue_tap_opened) {
			varText.text = "You open the tap with the red indicator.\n\n" + 
					"The faucet retches up and splutters out just enough water to fill the small medical cup " +
					"before running dry. The tap with the blue indicator has run dry as well. You conclude there " +
					"is no water left running through the pipes.\n" + 
					"The water is room temperature. And now that you regard the clear liquid you consider how " +
					"much time has passed since your last drink. Of course you don't know, but you are parched.\n\n" +
					"Press [D] to [D]rink the water\n" +
					"Press [R] to [R]eturn to roaming the room";
		} else if(blue_tap_opened) {
			varText.text = "You open the tap with the red indicator.\n\n" +
					"...But nothing pours from it.\n\n" +
					"Press [R] to [R]eturn to roaming the room";
		} else {
			varText.text = "The small medical cup remains partially filled with the liquid poured from the red tap.\n" + 
					"The water is room temperature. And now that you regard the clear liquid you consider how " +
					"much time has passed since your last drink. Of course you don't know, but you are parched.\n\n" +
					"Press [D] to [D]rink the water\n" +
					"Press [R] to [R]eturn to roaming the room";
		}
		
		if(Input.GetKeyDown(KeyCode.R)) { 
			if(!blue_tap_opened) {
				red_tap_opened = true;
			}
			currentState = States.room; 
		}
		else if(Input.GetKeyDown(KeyCode.D)) {
			if(!blue_tap_opened) {
				red_tap_opened = true;
			}
			currentState = States.nullState; 
			currentTransientState = transientStates.mirror_1_red_after_action; 
		}
		
	}
	
	void transientState_mirror_1_red_after_action () {
		
		varText.text = "You drink the water...\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			if(!achievement_took_the_red_pill) {
				currentTransientState = transientStates.took_the_red_pill_achievement;
			} else { 
				currentTransientState = transientStates.nullState;
				currentState = States.mirror_2_red; 
			}
		} 
	
	}
	
	void state_mirror_2_red () {
	
		if(!inInventory_wire_coathanger) {
			redFocus_toilet = "Press [T] to focus in on the [T]oilet\n";
		}
		if(!inInventory_shiv) {
			redFocus_bed = "Press [B] to focus in on the [B]ed\n";
		}
		if(!inInventory_handheld_emp) {
			redFocus_vent = "Press [V] to focus in on the small [V]entilation shaft\n";
		}
		
		varText.text = "A change occurs...\n\n" + 
				"Even though it looked and tasted like water you begin to doubt that it was.\n" +
				"Your vision blurs and your perspective begins to shift into a hyper-cognitive focus. You see " +
				"a new picture now through a different frame of mind - shedding wavelengths of light on a spectrum " +
				"you've never distinguished before. You observe the room, things seem different...\n\n" +
				redFocus_toilet +
				redFocus_bed +
				redFocus_vent +
				"Press [D] to focus in on the [D]oor\n" +
				"Press [R] to return to [R]oaming the room";
				
		returnToState = "mirror_2_red";
		
		if(Input.GetKeyDown(KeyCode.R)) {
			currentState = States.nullState;  
			currentTransientState = transientStates.leave_red_focus_confirmation; 
		}
		else if(Input.GetKeyDown(KeyCode.T)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.red_focus_toilet; 
		}
		else if(Input.GetKeyDown(KeyCode.B)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.red_focus_bed; 
		}
		else if(Input.GetKeyDown(KeyCode.D)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.red_focus_door; 
		}
		else if(Input.GetKeyDown(KeyCode.V)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.red_focus_vent; 
		}
		
	}
	
	void state_mirror_1_blue () {
	
		if(!blue_tap_opened && !red_tap_opened) {
			varText.text = "You open the tap with the blue indicator.\n\n" + 
					"The faucet retches up and splutters out just enough water to fill the small medical cup " +
					"before running dry. The tap with the red indicator has run dry as well. You conclude there " +
					"is no water left running through the pipes.\n" + 
					"The water is room temperature. And now that you regard the clear liquid you consider how " +
					"much time has passed since your last drink. Of course you don't know, but you are parched.\n\n" +
					"Press [D] to [D]rink the water\n" +
					"Press [R] to [R]eturn to roaming the room";
		} else if(red_tap_opened) {
			varText.text = "You open the tap with the blue indicator.\n\n" +
					"...But nothing pours from it.\n\n" +
					"Press [R] to [R]eturn to roaming the room";
		} else {
			varText.text = "The small medical cup remains partially filled with the liquid poured from the blue tap.\n" +
					"The water is room temperature. And now that you regard the clear liquid you consider how " +
					"much time has passed since your last drink. Of course you don't know, but you are parched.\n\n" +
					"Press [D] to [D]rink the water\n" +
					"Press [R] to [R]eturn to roaming the room";
		}
		
		if(Input.GetKeyDown(KeyCode.R)) { 
			if(!red_tap_opened) {
				blue_tap_opened = true;
			}
			currentState = States.room; 
		}
		else if(Input.GetKeyDown(KeyCode.D)) {
			if(!red_tap_opened) {
				blue_tap_opened = true;
			}
			currentState = States.nullState; 
			currentTransientState = transientStates.mirror_1_blue_after_action; 
		}
		
	}
	
	void transientState_mirror_1_blue_after_action () {
		
		varText.text = "You drink the water...\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			if(!achievement_took_the_blue_pill) {
				currentTransientState = transientStates.took_the_blue_pill_achievement; 
			} else { 
				currentTransientState = transientStates.nullState;
				currentState = States.mirror_2_blue; 
			}
		} 
		
	}
	
	void state_mirror_2_blue () {
		
		varText.text = "A change occurs...\n\n" + 
				"Even though it looked and tasted like water you begin to doubt that it was.\n" +
				"Your vision blurs and your cognition begins to shift into a hyper stimulated sensation. You see " +
				"a new picture now through a different frame of mind - as though through a blue filter that allows you " +
				"to observe the world without attachment. There are no more distractions, sensations or emotions. Only " + 
				"abstract thought and silence.\n" + 
				"The door to the room swings open...\n\n" + 
				"Press [E] to [E]xit the room";
		
		if(Input.GetKeyDown(KeyCode.E)) { 
			if(!achievement_game_over_bad_ending) {
				currentState = States.nullState; 
				currentTransientState = transientStates.game_over_bad_ending_achievement; 
			} else { 
				currentTransientState = transientStates.nullState;
				currentState = States.game_over; 
			}
		}
		
	}
	
	void state_mirror_3 () {
		
		varText.text = "You still don't recognise the face you see in the mirror. It doesn't bother you anymore.\n\n" + 
				"The empty disposable plastic medicine cups lie in the sink.\n\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		
	}
	
	void state_bed_0 () {
		
		/* Investigation creates the need for a new required tool */
		if(additionalOption_state) { // additional state option should will become available on condition
			state_additional_option = "Press [I] to investigate the bed.\n";
		}
	
		varText.text = "The matress and sheets are bleach-white, as though they have never been used before.\n\n" +
				state_additional_option +
				"Press [S] to [S]it down\n" +
				"Press [L] to [L]ie\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.S)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.bed_0_after_action_0; 
		}
		else if(Input.GetKeyDown(KeyCode.L)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.bed_0_after_action_1; 
		}
		else if(Input.GetKeyDown(KeyCode.I)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.bed_0_after_action_2; 
		}
	
	}
	
	void transientState_bed_0_after_action_0 () {
		
		if(toilet_flushed) {
			varText.text = "You sit down on the edge of the bed and try to recall the last thing you remember. " +
					"But nothing specific surfaces, only a vague and peculiar sense of loss that weighs on you heavily.\n\n" +
					"Press [SPACE] to continue...";
		} else {
			varText.text = "You sit down on the edge of the bed and try to recall the last thing you remember. " +
					"But the only thing on your mind is that fetid odour.\n\n" +
					"Press [SPACE] to continue...";
		}
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			if(inInventory_shiv) {
				currentTransientState = transientStates.nullState;
				currentState = States.bed_1; 
			} else {
				currentTransientState = transientStates.nullState;
				currentState = States.bed_0; 
			}
		}
		
	}
	
	void transientState_bed_0_after_action_1 () {
		
		if(toilet_flushed) {
			varText.text = "You don't feel tired. And even if you did, you don't think you could rest easy with " +
					"a certain sense of urgency to escape from the irregular situation you find yourself in.\n\n" +
					"Press [SPACE] to continue...";
		} else {
			varText.text = "You don't feel tired. And even if you did, you don't think you could rest with " +
					"that foul odour hanging in the air, or the bright light pouring from the ceiling above.\n\n" +
					"Press [SPACE] to continue...";
		}
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			if(inInventory_shiv) {
				currentTransientState = transientStates.nullState;
				currentState = States.bed_1; 
			} else {
				currentTransientState = transientStates.nullState;
				currentState = States.bed_0; 
			}
		}
		
	}
	
	void transientState_bed_0_after_action_2 () {
		
		if(redFocus_ItemReveal_bed) {
			varText.text = "You detected an item stashed beneath the bed through the expanded perception of the " +
					"red focus.\n\n" +
					"A makeshift shiv is found in this location. You regard the thin but sturdy blade of the shiv, " +
					"it won't bend easily. One man's insecurity is another man's improvised tool.\n" + 
					"You take the shiv...\n\n" +
					"Press [SPACE] to continue...";
		} else {
			varText.text = "Perhaps you could salvage components from the bed that could be fashioned into some " +
					"sort of rudimentary tool.\n\n" +
					"After a few minutes and a thorough search, you find a makeshift shiv tucked underneath " +
					"the bedframe against a discrete fold. You regard the thin but sturdy blade of the shiv, it " +
					"won't bend easily. One man's insecurity is another man's improvised tool.\n" + 
					"You take the shiv...\n\n" +
					"Press [SPACE] to continue...";
		}
		
		inInventory_shiv = true;
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.bed_1; 
		}
		
	}
	
	void state_bed_1 () {
		
		stateBed_1 = true;
		
		varText.text = "The matress is left flipped and the sheets tossed aside after your investigation.\n\n" +
				"Press [S] to [S]it down\n" +
				"Press [L] to [L]ie\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.S)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.bed_0_after_action_0; 
		}
		else if(Input.GetKeyDown(KeyCode.L)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.bed_0_after_action_1; 
		}
		
	}
	
	void state_door_0 () {
		
		varText.text = "The door is sealed shut. There are no visible handles or locking mechanisms. A familiar " +
				"ambigram symbol embellishes the door, but you can't recall what it's intended to signify.\n\n" +
				"Press [P] to push the door\n" +
				"Press [I] to open up [I]nventory\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.P)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.door_0_after_action; 
		}
		else if(Input.GetKeyDown(KeyCode.I)) { 
			returnToState = "door_0";
			currentState = States.inventory;
		}
		
	}
	
	void transientState_door_0_after_action () {
		
		varText.text = "The door won't budge, even with all your strength exerted...\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.door_0; 
		}
		
	}
	
	void state_door_1 () {
		
		stateDoor_1 = true;
		
		varText.text = "The door is sealed shut. There are no visible handles or locking mechanisms. A familiar " +
				"ambigram symbol embellishes the door, but you can't recall what it's intended to signify.\n\n" +
				"Press [P] to push the door\n" +
				"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		else if(Input.GetKeyDown(KeyCode.P)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.door_1_after_action; 
		}
		
	}
	
	void transientState_door_1_after_action () {
		
		varText.text = "The door swings open ponderously. There is a soft glow illuminating the hallway beyond...\n\n" +
			"Press [SPACE] to exit the room...\n" +
			"Press [R] to [R]eturn to roaming the room";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			if(!achievement_escape_artist) {
				currentTransientState = transientStates.exit_room_achievement;
			} else { 
				currentTransientState = transientStates.nullState;
				currentState = States.exit_room; 
			}
		} 
		else if(Input.GetKeyDown(KeyCode.R)) { currentState = States.room; }
		
	}
	
	void state_exit_room () {
		
		varText.text = "To your left and right a hallway extends. To the left there is a deep darkness and a sinister " + 
				"sense of nihilism, you see no end to that path.\n" +
				"To the right you see the source of the soft glow, a lightbulb above a doorway with a sign that reads 'EXIT' " +
				"below it. The same ambigram symbol you noticed before embellishes this door as well...\n\n" +
				"Press [L] to proceed [L]eft\n" +
				"Press [R] to proceed [R]ight";
		
		if(Input.GetKeyDown(KeyCode.L)) { currentState = States.hallway_left; }
		else if(Input.GetKeyDown(KeyCode.R)) { currentState = States.hallway_right; }
		
	}
	
	void state_hallway_left () {
		
		varText.text = "You proceed up the hallway to the curious darkness, only to take a closer look. There is something " +
				"about this lack of transparency that intrigues you against your better judgement. The shadow appears to " + 
				"reach out towards you as you approach - then, quite suddenly, snatches you up. Your feet leave the ground " + 
				"as you are slowly hoisted up, the shadows around you take form and slowly squeeze tighter, and tighter, and " + 
				"tighter. Your breath begins to stifle as an unbearable pressure wraps your body. You see a disdainful gaze " + 
				"emerge from the shadow, it regards you with contempt for a while - holding you in suspended suffering, " +
				"squeezing out the remnants of life and replacing it with its own dark form.\n" + 
				"A dark form that quickly fills up the empty vessel you leave behind.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.death; 
		}
		
	}
	
	void state_hallway_right () {
		
		varText.text = "You proceed down the hallway towards the exit. But you are not alone, something is following " +
			"you. You turn around and see a humanoid form charging you from the darkness. It doesn't make a sound as it " +
			"moves towards you with menacing intent, but you feel the vibration of its approach through the floor.\n\n" +
			"Flight or flight?\n\n" +
			"Press [H] to [H]it\n" +
			"Press [R] to [R]un";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.hallway_flight_0; }
		else if(Input.GetKeyDown(KeyCode.H)) { currentState = States.hallway_fight_0; }
		
	}
	
	void state_hallway_fight_0 () {
		
		varText.text = "You stand your ground, but you'll need something to give you the edge in this fight.\n\n" +
				"Press [I] to open up [I]nventory\n" +
				"Press [R] to turn and [R]un";
				
		if(Input.GetKeyDown(KeyCode.R)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.hallway_abandon_fight_0; 
		}
		else if(Input.GetKeyDown(KeyCode.I)) { 
			returnToState = "hallway_fight_0";
			currentState = States.inventory;
		}
				
	}
	
	void state_hallway_flight_0 () {
		
		varText.text = "You don't risk an unknown threat. You turn and make for the door in a sprint.\n" +
				"You can feel the thudding footfalls of the threat at your heels.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentState = States.hallway_exit; }
		
	}
	
	void transientState_hallway_abandon_fight_0 () {
		
		varText.text = "You turn to run. But you don't get far before you are knocked to the ground.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentTransientState = transientStates.hallway_fight_0_death; }
		
	}
	
	void transientState_hallway_fight_0_death () {
		
		varText.text = "A numbing sensation washes over your body, robbing you of the will you have over your own " +
				"muscles. You're pulled backwards with consecutive jerks as you are dragged into the complete darkness " +
				"of the hallway, leaving a red track in your wake as you fade to black...\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentTransientState = transientStates.death; }
		
	}
	
	void transientState_hallway_fight_0_weapon_use () {
		
		varText.text = "The humanoid figure is upon you alarmingly fast but you extract the shiv in time and, without " +
				"a second thought, plunge the blade into the neck of the figure, stepping aside to allow its forward " +
				"momentum to extract the blade after a deep cut. The figure collapses on its knees, its arms shake as " +
				"it struggles to keep itself from going prone. A dark viscous blood begins to spout from the wound, " +
				"splashing on the smooth floors. You take this opportunity to finish it; forcing it to the ground with a " +
				"stomp, keeping it pinned beneath your knee, you stab it, persistently, in the neck - you stop when you " +
				"realise that you are perforating unmoving dead meat. You flip over the body; it was once man, like you, " +
				"he wore a bloodied name tag that read: 'Ethan Herd'\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.hallway_encounter; 
		}
		
	}
	
	void state_hallway_encounter () {
		
		varText.text = "This commotion has captured undesired attention. You know this even before you turn around to " +
				"see the eyes of a tall and menacing nebulous form casting a leering gaze upon you from a distance down " +
				"the opposite end of the hallway, where the shadows linger. It stands there unmoving. The shadows around it " +
				"grow, the dark creeps along the walls and ceiling and over the floor of the hallway - without moving " +
				"a limb it draws closer. The shadow's advance persists...\n\n" +
				"Flight or flight?\n\n" +
				"Press [H] to [H]it\n" +
				"Press [R] to [R]un";
		
		if(Input.GetKeyDown(KeyCode.R)) { currentState = States.hallway_flight_1; }
		else if(Input.GetKeyDown(KeyCode.H)) { currentState = States.hallway_fight_1; }
		
	}
	
	void state_hallway_fight_1 () {
		
		varText.text = "You stand your ground, but you'll need something to give you the edge in this fight.\n\n" +
				"Press [I] to open up [I]nventory\n" +
				"Press [R] to turn and [R]un";
		
		if(Input.GetKeyDown(KeyCode.R)) { 
			currentState = States.nullState; 
			currentTransientState = transientStates.hallway_abandon_fight_1; 
		}
		else if(Input.GetKeyDown(KeyCode.I)) { 
			returnToState = "hallway_fight_1";
			currentState = States.inventory;
		}
		
	}
	
	void state_hallway_flight_1 () {
		
		varText.text = "You don't risk an unknown threat. You turn and make for the door in a sprint.\n" +
				"The shadow's reach extends around you, like grapsing fingers.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentState = States.hallway_exit; }
		
	}
	
	void transientState_hallway_abandon_fight_1 () {
		
		varText.text = "You turn to run. But the shadow's reach has you in its grip before you get far.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentTransientState = transientStates.hallway_fight_1_death; }
		
	}
	
	void transientState_hallway_fight_1_death () {
		
		varText.text = "The darkness surrounds you faster than you can escape it, the visible world retreats before you, it slips " +
				"away swiftly and quietly. You feel the shadow's embrace, it's cold and contemptuous. You are merely an empty " +
				"vessel in its scornful and menacing eyes - an empty vessel to occupy and assimilate into the vast expanse " + 
				"of its abyss. The lights don't just fade out, the bulb bursts and the retinas burn.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentTransientState = transientStates.death; }
		
	}
	
	void transientState_hallway_fight_1_weapon_use () {
		
		varText.text = "You step back over the body of the slain aggressor to allow yourself enough room and to avoid a " +
				"tripping hazard. Blood soaks your hand and much of your forearm. Despite this, your grip around the " +
				"rudimentary hilt of the shiv tightens and your knuckles whiten as you observe the approaching threat.\n" + 
				"The shadow reaches the corpse and pulls it into the depths of its nebulous expanse with a sudden violent " + 
				"force. Everything it comes into contact with it consumes. You regard your shiv as you realise you cannot kill " + 
				"this thing, this abyssal form. When you look up again the form is upon you, towering over you, it allows " +
				"you a moment to reflect, then... the shadow consumes you too.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { currentTransientState = transientStates.death; }
		
	}
	
	void state_hallway_exit () {
		
		varText.text = "The shape of the door at the end of the hallway, and the 'Exit' sign above it, grow as you rush " +
				"towards it. You hope it is unlocked. Putting your shoulder behind the effort you force the door open and " +
				"sprint though into a vast open space. You stop running when you realise nothing has followed you through. " +
				"You turn around to see the door shut; it is the only feature in a barren wall so high you cannot see the top.\n" +
				"You can see the breath leave your mouth but not much else. All around you a mist drifts shrouding the distance " +
				"and smothering it in a vast silence. A low light permeates the air but the source is unapparent, it feels " +
				"artificial, like the artificially smooth floor beneath your feet, it wouldn't surprise you if a sphere would " +
				"lay motionless upon it.\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) { 
			if(!achievement_game_over_good_ending) {
				currentState = States.nullState; 
				currentTransientState = transientStates.game_over_good_ending_achievement; 
			} else {
				currentTransientState = transientStates.nullState;
				currentState = States.game_finish; 
			}
		}
		
	}
	#endregion
	
	#region Red Focus State Handler Methods
	void transientState_red_focus_toilet () {
		
		varText.text = "There is something in the cistern of the toilet...\n\n" +
			"Press [SPACE] to continue...\n";
				
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			return_to_state();
		}
		
	}
	
	void transientState_red_focus_bed () {
		
		/* Investigation creates the need for a new required tool */
		if(additionalOption_state == false) { // additional state option should now be available (for States.bed_0)
			additionalOption_state = true;
			redFocus_ItemReveal_bed = true;
		}
		
		varText.text = "Something appears to be lodged beneath bed against the frame...\n\n" +
			"Press [SPACE] to continue...\n";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			return_to_state();
		}
		
	}
	
	void transientState_red_focus_door () {
		
		varText.text = "A electronic mechanism seems to power the lock of the door...\n\n" +
			"Something else is looking at you from other side. It sees you too; a basic black form, tall and menacing. It " + 
			"remains unmoving. Then it darts up what appears to be a hallway with no end.\n\n" +
			"Gone, but its presence lingers.\n\n" +
			"Press [SPACE] to continue...\n";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			return_to_state();
		}
		
	}
	
	void transientState_red_focus_vent () {
		
		varText.text = "A subtle electronic disturbance reverberates from the ventilastion shaft...\n\n" +
			"Press [SPACE] to continue...\n";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			return_to_state();
		}
		
	}
	
	void transientState_leave_red_focus_confirmation () {
		
		stateMirror_3 = true;
		
		varText.text = "You step away from the basin, and as you do you feel the red focus waning. You you will lose " +
			"this sight if you continue...\n\n" +
			"Press [Y] to step away\n" +
			"Press [N] to remain";
		
		if(Input.GetKeyDown(KeyCode.Y)) {
			currentTransientState = transientStates.nullState;
			currentState = States.room; 
		}
		else if(Input.GetKeyDown(KeyCode.N)) {
			currentTransientState = transientStates.nullState;
			return_to_state();
		}
		
	}
	#endregion
	
	#region Achievement Alert State Handler Methods
	void transientState_exit_room_achievement () {
		
		achievement_escape_artist = true;
		
		varText.text = "Achievement: Escape Artist\n\n" +
			"Press [ENTER] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.exit_room; 
		}
		
	}
	
	void transientState_took_the_red_pill_achievement () {
		
		achievement_took_the_red_pill = true;
		
		varText.text = "Achievement: Took The Red Pill\n\n" +
			"Press [ENTER] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.mirror_2_red; 
		}
		
	}
	
	void transientState_took_the_blue_pill_achievement () {
		
		achievement_took_the_blue_pill = true;
		
		varText.text = "Achievement: Took The Blue Pill\n\n" +
			"Press [ENTER] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.mirror_2_blue; 
		}
		
	}
	
	void transientState_game_over_bad_ending_achievement () {
		
		achievement_game_over_bad_ending = true;
		
		varText.text = "Achievement: The Story Ends\n" +
			"You wake up in your bed and believe whatever you want to believe...\n\n" +
			"Press [ENTER] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.game_over; 
		}
		
	}
	
	void transientState_game_over_good_ending_achievement () {
		
		achievement_game_over_good_ending = true;
		
		varText.text = "Achievement: Deeper Down The Rabbit Hole\n\n" +
			"Press [ENTER] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			if(!achievement_game_completed) {
				currentTransientState = transientStates.game_completion_achievement;
			} else {
				currentTransientState = transientStates.nullState;
				currentState = States.game_finish; 
			}
		}
		
	}
	
	void transientState_game_completion_achievement () {
		
		achievement_game_completed = true;
		
		varText.text = "Achievement: You stay in Wonderland\n\n" +
			"Press [ENTER] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.game_finish; 
		}
		
	}
	#endregion
	
	#region Achievement Description State Handler Methods
	void transientState_achievement_escape_artist_description () {
		
		varText.text = "Escape Artist:\n\n" +
			"Escape the room.\n\n" +
			"Press [ENTER] to return to Achievements menu...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.achievements; 
		}
		
	}
	
	void transientState_achievement_took_the_red_pill_description () {
		
		varText.text = "Took The Red Pill:\n\n" +
			"Drink the water poured from the red tap and experience the Red Focus.\n\n" +
			"Press [ENTER] to return to Achievements menu...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.achievements; 
		}
		
	}
	
	void transientState_achievement_took_the_blue_pill_description () {
		
		varText.text = "Took The Blue Pill:\n\n" +
			"Drink the water poured from the blue tap and experience the Blue Vague.\n\n" +
			"Press [ENTER] to return to Achievements menu...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.achievements; 
		}
		
	}
	
	void transientState_achievement_game_over_bad_ending_description () {
		
		varText.text = "The Story Ends:\n\n" +
			"Fade away into the Blue Vague (False Ending).\n\n" +
			"Press [ENTER] to return to Achievements menu...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.achievements; 
		}
		
	}
	
	void transientState_achievement_game_over_good_ending_description () {
		
		varText.text = "Deeper Down The Rabbit Hole:\n\n" +
			"Survive and escape (True ending).\n\n" +
			"Press [ENTER] to return to Achievements menu...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.achievements; 
		}
		
	}
	
	void transientState_achievement_game_completed_description () {
		
		varText.text = "You stay in Wonderland:\n\n" +
			"Complete the game.\n\n" +
			"Press [ENTER] to return to Achievements menu...";
		
		if(Input.GetKeyDown(KeyCode.Return)) { 
			currentTransientState = transientStates.nullState;
			currentState = States.achievements; 
		}
		
	}
	#endregion
	
	#region Item Use Transient State Handler Methods
	void transientState_inapplicable_item_use () {
	
		varText.text = "This item can't be used here...\n\n" +
			"Press [R] to [R]eturn to inventory";
		
		if(Input.GetKeyDown(KeyCode.R)) {
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void transientState_shiv_item_use_cistern () {
		
		varText.text = "The shiv's thin and sturdy blade allows you to slip in between the join and remove the top cover.\n" +
			"You see the source of the metallic clanging heard as the cistern emptied while flushing: a wire coathanger.\n" + 
			"You wonder if this item was intentionally hidden because it was considered useful. There may be some utility to " + 
			"be had from it, so you take the wire coathanger...\n\n" +
			"Press [SPACE] to continue...";
			
			inInventory_wire_coathanger = true;
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			currentState = States.toilet_2;
		}
		
	}
	
	void transientState_shiv_item_use_vent () {
		
		varText.text = "The tipped edge of the shiv's blade allows you to loosen the screws fastening the " +
			"grate to the vent.\n\n" +
			"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			currentState = States.vent_1;
		}
		
	}
	
	void transientState_shiv_item_use_fight_0 () {
		
		varText.text = "The tipped edge of the shiv's blade should be just the edge you need...\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.hallway_fight_0_weapon_use;
		}
		
	}
	
	void transientState_shiv_item_use_fight_1 () {
		
		varText.text = "Your blade has not failed you - yet...\n\n" +
				"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.hallway_fight_1_weapon_use;
		}
		
	}
	
	void transientState_wire_hook_item_use () {
		
		varText.text = "Using the wire hook you fashioned from the wire coathanger you can now reach the device. " +
			"You manage to hook it and drag it out. It's an electronic device of some sort housed in a plastic " +
			"casing with a switch on one side and a flashing LED.\n\n" +
			"There is a folded note taped to the device as well...\n\n" +
			"Press [SPACE] to continue...";
				
		inInventory_handheld_emp = true;
		inInventory_instructions_note = true;
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			currentState = States.vent_2;
		}
		
	}
	
	void transientState_instructions_note_item_examine () {
		
		varText.text = "The folded note has the word 'Instructions' scrawled on it...\n\n" +
			"press [U] to unfold and read the note\n" +
			"Press [R] to [R]eturn to inventory";
		
		if(Input.GetKeyDown(KeyCode.U)) {
			
			currentTransientState = transientStates.instructions_note_item_read;
			
		} else if(Input.GetKeyDown(KeyCode.R)) {
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void transientState_instructions_note_item_read () {
	
		varText.text = "This will help you get out. Flip the switch when you are close to the door. Be careful, " +
			"once you leave you won't be safe anymore.\n\n" +
			"P.S Stay out of the Shadow's reach.\n" +
			"And don't drink the water\n" +
			"~ E.H (Ethan Herd)\n\n" +
			"Press [R] to [R]eturn to inventory";
		
		if(Input.GetKeyDown(KeyCode.R)) {
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void transientState_wire_coathanger_item_modify () {
	
		varText.text = "It looks like you could bend this wire coathanger into a hook to extend your reach.\n\n" +
			"Press [M] to [M]odify item\n" +
			"Press [R] to [R]eturn to inventory";
		
		if(Input.GetKeyDown(KeyCode.M)) {
			
			inInventory_wire_coathanger = false;
			inInventory_wire_hook = true;
			
			currentTransientState = transientStates.wire_coathanger_item_modified;
			
		} else if(Input.GetKeyDown(KeyCode.R)) {
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void transientState_wire_coathanger_item_modified () {
		
		varText.text = "You've fashioned a wire hook from the coathanger that partially extends your reach...\n\n" +
			"Press [R] to [R]eturn to inventory";
		
		if(Input.GetKeyDown(KeyCode.R)) {
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void transientState_inapplicable_handheld_emp_item_use () {
		
		varText.text = "You flip the switch on the device...\n" +
			"Nothing happens...\n\n" +
			"Press [R] to [R]eturn to inventory";
		
		if(Input.GetKeyDown(KeyCode.R)) {
			currentTransientState = transientStates.nullState;
			currentState = States.inventory;
		}
		
	}
	
	void transientState_handheld_emp_item_use () {
		
		varText.text = "You flip the switch on the device...\n" +
			"You hear clicking mechanisms from within the door...\n\n" +
			"Press [SPACE] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTransientState = transientStates.nullState;
			currentState = States.door_1;
		}
		
	}
	#endregion
	
	#region Death Transient State Handler Method
	void transientState_death () {
		
		varText.text = "You die here.\n" +
			"The shadow's reach has claimed you...\n\n" +
			"Press [ENTER] to continue...";
		
		if(Input.GetKeyDown(KeyCode.Return)) {
			currentTransientState = transientStates.nullState;
			currentState = States.game_over;
		}
		
	}
	#endregion
	
	#endregion
}

/* 
Author Credits

Written and developed by: 

	Nathan Shepherd, 
	Cape Town, 
	South Africa, 
	Feb 2018
	
*/