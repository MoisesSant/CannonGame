class_name SpawnComponent
extends Node

## EXPORTS
@export var entity_scene: PackedScene
@export var spawn_cooldown: float = 2.0

## INTERNALS
var cooldown_timer: float = 0.0
var can_spawn: bool = true

## METHODS
func spawn_at_border():
	var entity_instance = entity_scene.instantiate()
	var screen_size = get_viewport().get_visible_rect().size
	var side = randi() % 4

	match side:
		# topo
		0: entity_instance.position = Vector2(randf_range(0, screen_size.x), 0)
		# direita
		1: entity_instance.position = Vector2(screen_size.x, randf_range(0, screen_size.y))
		# baixo
		2: entity_instance.position = Vector2(randf_range(0, screen_size.x), screen_size.y)
		# esquerda
		3: entity_instance.position = Vector2(0, randf_range(0, screen_size.y))

	if entity_instance:
		get_tree().root.add_child(entity_instance)
		can_spawn = false
		cooldown_timer = spawn_cooldown

func reset_cooldown():
	can_spawn = true
	cooldown_timer = 0.0

## UTILS
func _process(delta: float) -> void:
	if not can_spawn:
		cooldown_timer -= delta
		if cooldown_timer <= 0:
			reset_cooldown()
	else:
		spawn_at_border()
