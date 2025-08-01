extends Node

var ARGS : Array = [1000000]
var DESCRIPTION : String = "Sum of numbers from 1 to n"
var REQUIRE_MS : int = 50;
var REQUIRE_LINES : int = 1;
var REQUIRE_CHARS : int = 25;

func solution(n: int) -> int:
	var sum = n - 1;
	for i in n - 1:
		sum += i;
	return sum;
