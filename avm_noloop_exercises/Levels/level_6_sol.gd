extends Node

func solution(count : int, radius : float) -> Array: 
	var points = []
	points.resize(count)
	var η = TAU / count
	for i in count / 4:
		var point = Vector2(cos(i * η), sin(i * η)) * radius
		var perp = point * Vector2(1, -1)
		points[i] = point
		points[i+1] = -point
		points[i+2] = perp
		points[i+3] = -perp
	return points
