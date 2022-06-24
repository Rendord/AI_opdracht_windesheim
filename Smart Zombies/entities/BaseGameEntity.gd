class_name BaseGameEntity

var position: Vector2
var world: WorldSpace
 
func _init(position : Vector2, world : WorldSpace):
	self.position = position
	self.world = world

func update():
	pass

func _ready():
	pass 

