extends Node

func get_args() -> Array:
	return [Vector2i(randi_range(1, 100), randi_range(1, 9))]
	
var DESCRIPTION : String = "Neighbors of cell" 
var REQUIRE_MS : int = 10;
var REQUIRE_LINES : int = 1;
var REQUIRE_CHARS : int = 160;
var COMPARASION : String = "Same"

func solution(pos : Vector2i) -> Array:
	var result = []
	for y in range(-1,2):
		for x in range(-1,2):
			if(x!=0 || y!=0):
				result.append(pos + Vector2i(x,y))
	return result #Please don't care about yellow text in characters graph. Its bug
