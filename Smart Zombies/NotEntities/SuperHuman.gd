extends Character

var MyPath : Dictionary = {}
var traversing = false;

func _input(event):
	if event is InputEventMouseButton:
		if not traversing:
			

func _ready():
	pass
	
func _physics_process(delta):
	
	pass
	
func setupAstar(from : Vector2, to : Vector2):
	
