extends Node

func solution(start: Vector2i, end: Vector2i) -> Array:
	var difference = start - end
	var pixels = max(abs(difference.x), abs(difference.y))
	var result = []
	for i in pixels:
		result.append(start + difference * i / pixels)
	return result
