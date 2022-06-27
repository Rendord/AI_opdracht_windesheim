extends "../Character.gd"

var rng = RandomNumberGenerator.new() 

var wanderJitter : float = 4
var wanderRadius : float = 5
var wanderPoint : Vector2
var AgressionScore : float = 20
var lastSeenPosition : Vector2


var target : Character
var last_seen_time = -INF
onready var circle = get_node("Node2D/circle")
onready var wandercast = get_node("Wander")
onready var velocityRayCast = get_node("Velocity")
onready var LineOfSight = get_node("LineOfSight")

onready var stateMachine = get_node("StateMachine")

onready var StateLabel = get_node("StateLabel")

var steering = Vector2.ZERO

func _ready():
	wanderPoint = position
	
func _process(delta):
	updateAggressionScore()
	if not target == null:
		LineOfSight.cast_to = to_local(target.position);
	steering = Vector2.ZERO
	stateMachine.StateAction();
	steering += ObstacleAvoidanceBehaviour()
	velocity += steering.clamped(max_steering)
	
	velocityRayCast.cast_to = velocity
	._physics_process(delta)
	
func _unhandled_input(event):
	if event is InputEventKey:
			if event.pressed and event.scancode == KEY_TAB:
				StateLabel.visible = true
				var previousState = ""
				if(stateMachine.previousState):
					previousState = stateMachine.previousState.get_class()
				
				StateLabel.text = "Previous State: " + previousState + "\n"
				StateLabel.text += "Current State: " + stateMachine.currentState.get_class()
			else:
				StateLabel.hide()

func updateAggressionScore():
		var time_elapsed = OS.get_unix_time() - last_seen_time
		var distance = position.distance_to(target.position)
		AgressionScore = get_parent().CalculateAggression(distance, time_elapsed)
		#print(AgressionScore)


func PursuitBehaviour(var evader : Character):
	var ToEvader = evader.position - position
	var Relativeheading = self.heading.dot(evader.heading)
	if ToEvader.dot(self.heading) > 0 and Relativeheading < -0.95:
		return SeekBehaviour(evader.position)

	var LookAheadTime = ToEvader.length() / max_speed + evader.velocity.length()
	return SeekBehaviour(evader.position + evader.velocity * LookAheadTime)

func WanderBehaviour(wanderDistance : float):
		var x = rng.randf_range(-1, 1) * wanderJitter
		var y = rng.randf_range(-1, 1) * wanderJitter

		var wanderDisplacement = Vector2(x, y)
		wanderPoint += wanderDisplacement;
		wanderPoint = wanderPoint.normalized();
		wanderPoint *= wanderRadius;
		circle.position = heading * wanderDistance;
		var wanderTarget = (heading * wanderDistance) + wanderPoint;
		wandercast.cast_to = wanderTarget;

		return wanderTarget
