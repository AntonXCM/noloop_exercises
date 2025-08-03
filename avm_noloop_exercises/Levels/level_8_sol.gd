extends Node

func solution(vector : Vector3, matrixes : Array[Basis]) -> Vector3: 
	for i in matrixes:
		vector *= i
	return vector;
