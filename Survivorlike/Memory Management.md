[[General Programming Concepts]]

Any time you have a new() statement, make sure there is a part in the process where that object is cleaned up.

In Godot, a Node is only deleted if it is in the tree. If a node is made with new() and never attached as a child, [[Free()]] or [[QueueFree()]] must be manually called on it.