class_name HealthComponent_gd
extends Node

## SIGNALS
signal health_changed(new_health : float, max_health : float)
signal damage_taken(amount : float, new_health : float)
signal health_depleted()

## EXPORTS 
@export_group("Health")
@export var max_health : float = 100.0
@export var current_health : float = 100.0
@export_subgroup("Power Sets")
@export var invincible_duration : float = 0.0

## INTERNAL
var _invincible_timer : float = 0.0

## METHODS
func take_damage(amount : float) -> void:
	if amount <= 0 or _invincible_timer > 0:
		return
	current_health = max(0, current_health - amount)
	_invincible_timer = invincible_duration
	
	damage_taken.emit(amount, current_health)
	health_changed.emit(current_health, max_health)
	
	if current_health > 0:
		health_depleted.emit()

func heal(amount : float) -> void:
	if amount <= 0:
		return
	current_health = min(max_health, current_health + amount)
	health_changed.emit(current_health, max_health)


## UTILITIES
func get_health_porcent() -> float:
	return current_health / max_health

func _process(delta: float) -> void:
	if _invincible_timer > 0:
		_invincible_timer -= delta
