extends Node2D

func get_args() -> Array:
	return [Vector2i(randf()*100, randf()*100), Vector2i(randf()*100, randf()*100)]

var DESCRIPTION : String = "Drawing a line"
var REQUIRE_MS : int = 30;
var REQUIRE_LINES : int = 6;
var REQUIRE_CHARS : int = 170;
var COMPARASION : String = "StartEnd"

func solution(start: Vector2i, end: Vector2i) -> Array:
	var difference : Vector2i = start - end
	var result = []
	for i in start.distance_to(end):
		var point = start + Vector2i(difference * i / start.distance_to(end))
		if point not in result:
			result.append(point)
	return result
