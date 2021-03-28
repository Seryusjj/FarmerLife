# PerlinNoise3D.gd
tool
extends VisualShaderNodeCustom
class_name PositionObjectSpaceTransform


func _get_name():
	return "PositionObjectSpaceTransform"


func _get_category():
	return "CustomTools"


func _get_description():
	return "Return the current vertex position in object space when in a vertex shader"


func _get_return_icon_type():
	return VisualShaderNode.PORT_TYPE_VECTOR


func _get_input_port_count():
	return 1


func _get_input_port_name(port):
	return "Position"


func _get_input_port_type(port):
	return VisualShaderNode.PORT_TYPE_VECTOR


func _get_output_port_count():
	return 1


func _get_output_port_name(port):
	return "ObjectPosition"


func _get_output_port_type(port):
	return VisualShaderNode.PORT_TYPE_VECTOR


func _get_global_code(mode):
	return """"""


func _get_code(input_vars, output_vars, mode, type):
	return output_vars[0] + " = (inverse(WORLD_MATRIX) * vec4(%s, 1.0)).xyz;" % [input_vars[0]]
