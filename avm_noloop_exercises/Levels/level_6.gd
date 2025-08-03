extends Node

func get_args() -> Array:
	return [400, randf_range(1, 99)]
	
var DESCRIPTION : String = "Points on a circle" 
var REQUIRE_MS : int = 150;
var REQUIRE_LINES : int = 11;
var REQUIRE_CHARS : int = 250;
var COMPARASION : String = "Same"

func solution(count : int, radius : float) -> Array: 
	#Count is always divisible by 4
	var points = []
	for i in count:
		points.append(Vector2(cos(i / count * TAU) * radius, sin(i / count * TAU) * radius))
	return points
