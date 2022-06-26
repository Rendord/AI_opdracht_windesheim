extends State
class_name PursuitState

onready var Zombie = owner
onready var Human = get_tree().current_scene.get_node("Human")

func Enter() -> void:
	print("pursuit enter")
	
func Do() -> void:
	Zombie.steering += Zombie.PursuitBehaviour(Human)

func Exit() -> void:
	print("pursuit exit")
