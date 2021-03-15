# Case One

1. Immutability : PREFER IMMUTABILITY FOR VALUE TYPES
	Immutable : After they are created, they are constant
	Thread safe | Easy export
	immutable objects don't have writes.
	Structs are not necessarily immutable, but mutable structs are evil.

2. DISTINGUISH BETWEEN VALUE TYPES AND REFERENCE TYPES
	Value Type store values : Struct
	Reference Type defines behavior
	
	○ Classes : Define behavior
	○ C++ : All passed by value
	○ Java : all user defined types are reference types
	C# : struct : value | class : reference type

	If MyData is a value type, the content of the return is copied into the storage for v. 
	However, if MyData is a reference type, you’ve exported a reference to an internal variable—and 
	you’ve violated the principle of encapsulation

	When in doubt about the expected use, use a reference type.

3. Class, structs, tuple types, or anonymous types

	more ceremony for simple designs
	Anonymous : 
		Anonymous types are compiler-generated immutable reference types.
		You’ve indicated that you need a new internal sealed class.
		You’ve told the compiler that this new type is an immutable type and that it has 
		two public read-only properties surrounding two backing fields (X, Y).
		
		Great for storing interim results.
		
		anonymous types incur less runtime cost than you might imagine
		
		simple storage types that hold data but do not define any behavior.
		
		Tuples are preferred for method return types and method parameters because they follow structural typing.
		
		Anonymous types are better for composite keys in collections because they are immutable

		Tuples : advantage of value types
		Anonymous : reference types

4. Tasks

5. Events
    Event based async pattern

All things which everyone should know.
