extends Node

func solution(text: String) -> String: 
	for i in len(text) / 5:
		text.insert(i * 5, "\n")
	return text;