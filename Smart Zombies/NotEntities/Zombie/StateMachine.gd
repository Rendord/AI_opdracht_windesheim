class_name StateMachine
extends Node

export var initialState := NodePath()

var previousState: State = null
onready var currentState: State = get_node(initialState)

func _ready() -> void:
	yield(owner, "ready")
	currentState.Enter()

func StateAction() -> void:
	currentState.Do()

func SwitchState(nextState: NodePath) -> void:
	if not has_node(nextState):
		return

	previousState = currentState
	previousState.Exit()
	currentState = get_node(nextState)
	currentState.Enter()

