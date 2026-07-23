@tool
extends EditorPlugin

var csharp_plugin: EditorPlugin
var mono_plugin: EditorPlugin
var build_button: Button

func _enter_tree():
	# Find the built-in C# support plugin.
	var plugins = get_parent().get_children()
	for plugin in plugins:
		if plugin.has_method("BuildProjectPressed"):
			mono_plugin = plugin
			break
	# Press the "Build Project (Alt + B)" button.(or well, call the method that pressing it calls)
	# for the case of files being modified while the editor is not open
	mono_plugin.BuildProjectPressed()
	
	var script = load("res://addons/BuildOnChangeOnly/BuildOnChangeOnly.cs")
	csharp_plugin = script.new()
	add_child(csharp_plugin)
	csharp_plugin.call_deferred("Start")
	
	# Connect the "Build Project (Alt + B)" button to the plugin instead
	build_button = find_build_button(get_editor_interface().get_base_control())
	build_button.pressed.disconnect(build_button.pressed.get_connections()[0]["callable"])
	build_button.pressed.connect(Build)

func _exit_tree() -> void:
	csharp_plugin.End()
	build_button.pressed.disconnect(Build)
	build_button.pressed.connect(mono_plugin.BuildProjectPressed)

func find_build_button(In: Node) -> Button:
	var children = In.get_children()
	for child in children:
		if child is Button and child.icon == get_editor_interface().get_editor_theme().get_icon("BuildCSharp", "EditorIcons"):
			return child
		var potential = find_build_button(child)
		if potential != null:
			return potential
	return null

func Build():
	csharp_plugin.End()
	remove_child(csharp_plugin)
	# Press the "Build Project (Alt + B)" button.(or well, call the method that pressing it calls)
	mono_plugin.BuildProjectPressed()
	var script = load("res://addons/BuildOnChangeOnly/BuildOnChangeOnly.cs")
	csharp_plugin = script.new()
	add_child(csharp_plugin)
	csharp_plugin.call_deferred("Start");
