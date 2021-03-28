# PerlinNoise3D.gd
tool
extends VisualShaderNodeCustom
class_name PositionObjectSpace


func _get_name():
	return "PositionObjectSpace"


func _get_category():
	return "CustomTools"


func _get_description():
	return "Return the current object position"


func _get_return_icon_type():
	return VisualShaderNode.PORT_TYPE_VECTOR


func _get_input_port_count():
	return 0


func _get_input_port_name(port):
	return ""


func _get_input_port_type(port):
	return ""


func _get_output_port_count():
	return 1


func _get_output_port_name(port):
	return "ObjectPosition"


func _get_output_port_type(port):
	return VisualShaderNode.PORT_TYPE_VECTOR


func _get_global_code(mode):
	return """"""


func _get_code(input_vars, output_vars, mode, type):
	return output_vars[0] + " = (MODELVIEW_MATRIX * vec4(VERTEX, 1.0)).xyz;"
