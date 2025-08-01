extends Node

var ARGS : Array = [11,10.75]
var DESCRIPTION : String = "Root" 
var REQUIRE_MS : int = 10;
var REQUIRE_LINES : int = 1;
var REQUIRE_CHARS : int = 25;

func solution(value : float, power : float) -> float:
	var result = value / power
	for i in 100:
		result -= (result ** power - value) / (power * result ** (power - 1))
	return result
