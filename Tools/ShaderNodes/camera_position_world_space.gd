# PerlinNoise3D.gd
tool
extends VisualShaderNodeCustom
class_name CameraPositionWorldSpace


func _get_name():
	return "CameraPositionWorldSpace"


func _get_category():
	return "CustomTools"


func _get_description():
	return "Return camera world position"


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
	return "CameraWorldPosition"


func _get_output_port_type(port):
	return VisualShaderNode.PORT_TYPE_VECTOR


func _get_global_code(mode):
	return """"""


func _get_code(input_vars, output_vars, mode, type):
	# INV_CAMERA_MATRIX -> World space to view space transform.
	return output_vars[0] + " = vec3(INV_CAMERA_MATRIX[0][3], INV_CAMERA_MATRIX[1][3], INV_CAMERA_MATRIX[2][3]);"
