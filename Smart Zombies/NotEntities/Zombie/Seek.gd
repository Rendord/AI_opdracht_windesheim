extends State
class_name SeekState

onready var Zombie = owner
onready var Human = get_tree().current_scene.get_node("Human")

func Enter() -> void:
	Zombie.max_speed = 75;
	print("Seek enter")
	
func Do() -> void:
	Zombie.steering += Zombie.ArriveBehaviour(Zombie.lastSeenPosition, 1)
	if Zombie.LineOfSight.get_collider() == Zombie.target:
		stateMachine.SwitchState("Pursuit")
	if Zombie.position == Zombie.lastSeenPosition:
		if Zombie.AgressionScore > 40:
			stateMachine.SwitchState("Alert")
		if Zombie.AgressionScore > 60:
			stateMachine.SwitchState("Agressive")
		else:
			stateMachine.SwitchState("Wander")

func Exit() -> void:
	print("Seek exit")
	
func get_class() -> String:
	return "SeekState"
