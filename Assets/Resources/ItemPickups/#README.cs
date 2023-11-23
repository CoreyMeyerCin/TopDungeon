/*
 * HOW TO CREATE ITEM PICKUPS
 *  
 * UNIVERSAL SETTINGS 
 * Tag: Collectable
 * Layer: Item
 * 
 * Create a new prefab and add:
 *		Box Collider 2D
 *			Is Trigger = true
 *			adjust trigger radius to approprate size
 * 
 *		Sprite Renderer
 *			Select sprite
 *			Sorting Layer: Interactable
 *	
 * SCRIPT COMPOSITION
 * Create a script of the same name. At a minimum a script should have:
 *		-Monobehaviour + ICollectable inheritance
 *		-OnCollect() method that handles everything necessary when the item is picked up
 *		-if it is a stat boosting item, set the increase parameters in here
 *		-an event action that can be invoked if necessary. We may not use most of these but it doesn't hurt to have them.
 *		
 *		
 * 
 * WEAPONS
 *		If creating a weapon pickup, also add a Weapon object from the Weapons prefab folder into the Weapon variable
 * 
 * 
 */