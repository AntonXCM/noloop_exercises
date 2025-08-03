extends Node

func get_args() -> Array:
	return [randi_range(200,10000), randf_range(200, 500), randf_range(10000, 30000)]
	
var DESCRIPTION : String = "Machine gun problem" 
var REQUIRE_MS : int = 15;
var REQUIRE_LINES : int = 4;
var REQUIRE_CHARS : int = 130;

func solution(ammo : int, timeToFire : float, holdTime : int ) -> Dictionary:
	var bullets = []
	while ammo > 0 && holdTime - timeToFire > 0:
		ammo -= 1
		holdTime -= timeToFire
		bullets.append(null) #Like we are shooting but dont see it
	return {"ammo": ammo, "bullets": bullets}
