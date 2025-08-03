extends Node

func get_args() -> Array:
	return ["
	.....
	.0#v.
	-###-
	.v#0.
	.|.|.".replace("\n","")]
	
var DESCRIPTION : String = "Parse ASCII art" 
var REQUIRE_MS : int = 20;
var REQUIRE_LINES : int = 3;
var REQUIRE_CHARS : int = 60;

func solution(text: String) -> String: 
	for i in len(text):
		if i % 5 == 0:
			text.insert(i, "\n")
	return text;
