extends Node

func get_args() -> Array:
	var result : Array[Basis] = []
	for i in 300:
		result.append(Basis(Vector3(randf()*2,randf()*2,randf()*2),Vector3(randf()*2,randf()*2,randf()*2),Vector3(randf()*2,randf()*2,randf()*2)))
	return [Vector3(randf()*2,randf()*2,randf()*2),result]
	
var DESCRIPTION : String = "Multiply vector by matrixes" 
var REQUIRE_MS : int = 45;
var REQUIRE_LINES : int = 3;
var REQUIRE_CHARS : int = 45;

func solution(vector : Vector3, matrixes : Array[Basis]) -> Vector3: 
	var superMatrix = Basis()
	for i in matrixes:
		superMatrix *= i
	return vector * superMatrix;
