extends State
class_name PursuitState

onready var Zombie = owner
onready var Human = get_tree().current_scene.get_node("Human")

func Enter() -> void:
	Zombie.max_speed = 100;
	print("pursuit enter")
	
func Do() -> void:
	Zombie.steering += Zombie.PursuitBehaviour(Human)
	if Zombie.LineOfSight.get_collider() != Zombie.target:
		stateMachine.SwitchState("Seek")
	if Zombie.AgressionScore < 50:
		stateMachine.SwitchState("Alert")
	if Zombie.AgressionScore > 60:
		stateMachine.SwitchState("Agressive")

func Exit() -> void:
	Zombie.last_seen_time = OS.get_unix_time()
	Zombie.lastSeenPosition = Human.position
	print("pursuit exit")
	
func get_class() -> String:
	return "PursuitState"
