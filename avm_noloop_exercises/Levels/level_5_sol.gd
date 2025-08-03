extends Node

func solution(ammo : int, timeToFire : float, holdTime : int ) -> Dictionary:
	var bullets = []
	var shots = min(ammo, int(holdTime / timeToFire))
	bullets.resize(shots)
	return {"ammo": ammo - shots, "bullets": bullets}
