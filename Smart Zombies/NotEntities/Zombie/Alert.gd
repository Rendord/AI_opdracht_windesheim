extends State
class_name Alert

onready var Zombie = owner
onready var Human = get_tree().current_scene.get_node("Human")

func Enter() -> void:
	Zombie.max_speed = 50
	print("enter Alert")
	
func Do() -> void:
	Zombie.steering += Zombie.WanderBehaviour(50)
	if Zombie.LineOfSight.get_collider() == Zombie.target:
		stateMachine.SwitchState("Pursuit")
	if Zombie.AgressionScore < 40:
		stateMachine.SwitchState("Wander")

func Exit() -> void:
	print("Alert exit")
	
func get_class() -> String:
	return "AlertState"
