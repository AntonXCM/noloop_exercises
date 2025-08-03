extends Node

func solution(pos : Vector2i) -> Array:
	return [pos + Vector2i(-1,-1), pos + Vector2i(-1,0), pos + Vector2i(-1,1), pos + Vector2i(0,-1),pos + Vector2i(0, 1), pos + Vector2i(1,-1), pos + Vector2i(1, 0), pos + Vector2i(1, 1)]
